/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BadgeCounter.Controls
{
    /// <summary>
    /// Control which behaves like button but uses images to define its appearance.
    /// Two images need to be specified, one for default/normal state, second one for pressed state.
    /// Pressed state is simulated by changing image for specified amount of time after tap on the control.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageButton : ContentView
    {
        #region fields

        /// <summary>
        /// Default timeout for control's pressed state (in milliseconds).
        /// After that time, the control returns to normal state.
        /// </summary>
        private const int DEFAULT_TIMEOUT = 300;

        /// <summary>
        /// Flag indicating if control is in disabled state.
        /// </summary>
        private bool _disabled = false;

        /// <summary>
        /// Flag indicating if press animation (displaying image for pressed state) is in progress.
        /// </summary>
        private bool _pressed = false;

        #endregion

        #region properties

        /// <summary>
        /// Bindable property definition for normal state image source.
        /// </summary>
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            "Source", typeof(ImageSource), typeof(ImageButton));

        /// <summary>
        /// Bindable property definition for pressed state image source.
        /// </summary>
        public static readonly BindableProperty PressedSourceProperty = BindableProperty.Create(
            "PressedSource", typeof(ImageSource), typeof(ImageButton));

        /// <summary>
        /// Bindable property definition for pressed state timeout (in milliseconds).
        /// </summary>
        public static readonly BindableProperty PressedTimeoutProperty = BindableProperty.Create(
            "PressedTimeout", typeof(int), typeof(ImageButton), DEFAULT_TIMEOUT);

        /// <summary>
        /// Bindable property definition for control's default command (executed when control is tapped).
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            "Command", typeof(ICommand), typeof(ImageButton));

        /// <summary>
        /// Image source for control's default/normal state.
        /// </summary>
        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary>
        /// Image source for control's pressed state.
        /// Image is displayed for specified amount of time when control is tapped.
        /// </summary>
        public ImageSource PressedSource
        {
            get => (ImageSource)GetValue(PressedSourceProperty);
            set => SetValue(PressedSourceProperty, value);
        }

        /// <summary>
        /// Timeout for displaying pressed state image (pressed state simulation).
        /// </summary>
        public int PressedTimeout
        {
            get => (int)GetValue(PressedTimeoutProperty);
            set => SetValue(PressedTimeoutProperty, value);
        }

        /// <summary>
        /// Control's default command.
        /// Executed when control is tapped.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// The control constructor.
        /// </summary>
        public ImageButton()
        {
            InitializeComponent();
            InitializeEventsListeners();
        }

        /// <summary>
        /// Initializes event listeners of the control.
        /// </summary>
        private void InitializeEventsListeners()
        {
            ((AbsoluteLayout)Content).GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(OnTapped)
            });
        }

        /// <summary>
        /// Handles tap on the control body.
        /// Simulates pressed state by changing control appearance (proper images) for specified time.
        /// When press animation ends, the control's default command is executed.
        /// </summary>
        private void OnTapped()
        {
            if (PressedSource != null && !_disabled && !_pressed)
            {
                SourceImage.IsVisible = false;
                _pressed = true;
                Device.StartTimer(TimeSpan.FromMilliseconds(PressedTimeout), () =>
                {
                    _pressed = false;
                    SourceImage.IsVisible = true;

                    if (!_disabled)
                    {
                        Command?.Execute(null);
                    }

                    return false;
                });
            }
        }

        /// <summary>
        /// Handles control's properties change.
        /// Forwards the action to property specific method.
        /// </summary>
        /// <param name="propertyName">Name of property which changed.</param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == CommandProperty.PropertyName)
            {
                OnCommandPropertyChanged();
            }
        }

        /// <summary>
        /// Handles change of the command property.
        /// Updates disabled state and start listening for command's execution possibility change.
        /// </summary>
        private void OnCommandPropertyChanged()
        {
            if (Command != null)
            {
                _disabled = !Command.CanExecute(null);
                Command.CanExecuteChanged += CommandOnCanExecuteChanged;
            }
            else
            {
                _disabled = false;
            }
        }

        /// <summary>
        /// Handles change of command's execution possibility.
        /// Updates disabled state of the control.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void CommandOnCanExecuteChanged(object sender, EventArgs eventArgs)
        {
            _disabled = !Command.CanExecute(null);
        }

        #endregion
    }
}