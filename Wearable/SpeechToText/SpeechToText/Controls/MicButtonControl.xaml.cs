//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpeechToText.Controls
{
    /// <summary>
    /// The mic button control class.
    /// The control is a two state (recording, not recording) button with proper images
    /// and animations.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MicButtonControl : ContentView
    {
        #region fields

        /// <summary>
        /// Default tap animation time (in milliseconds).
        /// </summary>
        private const int TAP_ANIMATION_TIME = 600;

        /// <summary>
        /// Flag indicating if button is in recording mode.
        /// </summary>
        private bool _isRecording = false;

        /// <summary>
        /// Flag indicating if there is ongoing recording on animation.
        /// </summary>
        private bool _isRecordingOnAnimationInProgress = false;

        /// <summary>
        /// Flag indicating if there is ongoing recording off animation.
        /// </summary>
        private bool _isRecordingOffAnimationInProgress = false;

        #endregion

        #region properties

        /// <summary>
        /// "Recording" bindable property definition.
        /// The property is a flag indicating if button is in recording state.
        /// </summary>
        public static readonly BindableProperty RecordingProperty =
            BindableProperty.Create("Recording", typeof(bool), typeof(MicButtonControl), false);

        /// <summary>
        /// "TurnOnCommand" bindable property definition.
        /// The command is executed when button is tapped in no-recording state.
        /// </summary>
        public static readonly BindableProperty TurnOnCommandProperty =
            BindableProperty.Create("TurnOnCommand", typeof(ICommand), typeof(MicButtonControl));

        /// <summary>
        /// "TurnOffCommand" bindable property definition.
        /// The command is executed when button is tapped in recording state.
        /// </summary
        public static readonly BindableProperty TurnOffCommandProperty =
            BindableProperty.Create("TurnOffCommand", typeof(ICommand), typeof(MicButtonControl));

        /// <summary>
        /// Flag indicating if button is in recording state.
        /// </summary>
        public bool Recording
        {
            get => (bool)GetValue(RecordingProperty);
            set => SetValue(RecordingProperty, value);
        }

        /// <summary>
        /// Command executed when button is tapped in no-recording state.
        /// </summary>
        public ICommand TurnOnCommand
        {
            get => (ICommand)GetValue(TurnOnCommandProperty);
            set => SetValue(TurnOnCommandProperty, value);
        }

        /// <summary>
        /// Command executed when button is tapped in recording state.
        /// </summary>
        public ICommand TurnOffCommand
        {
            get => (ICommand)GetValue(TurnOffCommandProperty);
            set => SetValue(TurnOffCommandProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// The control constructor.
        /// </summary>
        public MicButtonControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles tap on the button (captured by the tap layer).
        /// Executes proper command.
        /// </summary>
        /// <param name="sender">The tapped view reference.</param>
        /// <param name="e">Event arguments</param>
        private void OnTapped(object sender, EventArgs e)
        {
            if (Recording)
            {
                TurnOffCommand?.Execute(null);
            }
            else
            {
                TurnOnCommand?.Execute(null);
            }
        }

        /// <summary>
        /// Animates the button state change.
        /// </summary>
        /// <returns>Animation task.</returns>
        private async Task AnimateChangeState()
        {
            if (_isRecording)
            {
                _isRecording = false;

                ViewExtensions.CancelAnimations(DefaultBackgroundImage);

                if (_isRecordingOnAnimationInProgress)
                {
                    ViewExtensions.CancelAnimations(DefaultForegroundImage);
                    ViewExtensions.CancelAnimations(PressedForegroundImage);
                    DefaultForegroundImage.Opacity = 1;
                    PressedForegroundImage.Opacity = 0;
                    _isRecordingOnAnimationInProgress = false;
                }
                else
                {
                    _isRecordingOffAnimationInProgress = true;

                    bool[] canceled = await Task.WhenAll(new List<Task<bool>>
                    {
                        DefaultForegroundImage.FadeTo(1, TAP_ANIMATION_TIME),
                        PressedForegroundImage.FadeTo(0, TAP_ANIMATION_TIME),
                        DefaultBackgroundImage.ScaleTo(1, TAP_ANIMATION_TIME)
                    });

                    if (!canceled[0] && !canceled[1] && !canceled[2])
                    {
                        _isRecordingOffAnimationInProgress = false;
                    }
                }
            }
            else
            {
                _isRecording = true;

                if (_isRecordingOffAnimationInProgress)
                {
                    ViewExtensions.CancelAnimations(DefaultForegroundImage);
                    ViewExtensions.CancelAnimations(PressedForegroundImage);
                    DefaultForegroundImage.Opacity = 0;
                    PressedForegroundImage.Opacity = 1;
                }
                else
                {
                    _isRecordingOnAnimationInProgress = true;

                    bool[] canceled = await Task.WhenAll(new List<Task<bool>>
                    {
                        DefaultForegroundImage.FadeTo(0, TAP_ANIMATION_TIME),
                        PressedForegroundImage.FadeTo(1, TAP_ANIMATION_TIME)
                    });

                    if (!canceled[0] && !canceled[1])
                    {
                        _isRecordingOnAnimationInProgress = false;
                    }
                }
            }
        }

        /// <summary>
        /// Animates the button in recording state.
        /// </summary>
        /// <returns>Animation task.</returns>
        private async Task AnimateRecording()
        {
            while (true)
            {
                bool canceled = await DefaultBackgroundImage.ScaleTo(1.25, 600);
                if (canceled)
                {
                    break;
                }

                canceled = await DefaultBackgroundImage.ScaleTo(1, 600);
                if (canceled)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Handles property change of the button.
        /// If the "Recording" property was changed, it starts proper animations.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected override async void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == RecordingProperty.PropertyName)
            {
                var tasksList = new List<Task>();
                if (_isRecording != Recording)
                {
                    tasksList.Add(AnimateChangeState());
                }

                if (Recording)
                {
                    tasksList.Add(AnimateRecording());
                }

                if (tasksList.Count > 0)
                {
                    await Task.WhenAll(tasksList);
                }
            }
        }

        #endregion
    }
}