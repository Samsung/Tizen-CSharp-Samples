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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BadgeCounter.Controls
{
    /// <summary>
    /// Button control which toggles between two states (toggle switch).
    /// Images for the states and button text are defined as bindable properties.
    /// The control provides simple images morphing animation when state is toggling (only when control is tapped).
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToggleButton : ContentView
    {
        #region fields

        /// <summary>
        /// Toggling state animation time (in milliseconds).
        /// </summary>
        private const int ANIMATION_TIME = 1000;

        #endregion

        #region properties

        /// <summary>
        /// Bindable property definition for an image displayed as control's "off" state.
        /// </summary>
        public static readonly BindableProperty OffStateSourceProperty = BindableProperty.Create(
            "OffStateSource", typeof(ImageSource), typeof(ToggleButton));

        /// <summary>
        /// Bindable property definition for an image displayed as control's "on" state.
        /// </summary>
        public static readonly BindableProperty OnStateImageProperty = BindableProperty.Create(
            "OnStateSource", typeof(ImageSource), typeof(ToggleButton));

        /// <summary>
        /// Bindable property definition for the control's state.
        /// </summary>
        public static readonly BindableProperty CheckedProperty = BindableProperty.Create(
            "Checked", typeof(bool), typeof(ToggleButton), false, BindingMode.TwoWay);

        /// <summary>
        /// Bindable property definition for the text displayed in the foreground of the control.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            "Text", typeof(string), typeof(ToggleButton), default(string));

        /// <summary>
        /// Source of an image displayed as control's "off" state.
        /// </summary>
        public ImageSource OffStateSource
        {
            get => (ImageSource)GetValue(OffStateSourceProperty);
            set => SetValue(OffStateSourceProperty, value);
        }

        /// <summary>
        /// Source of an image displayed as control's "on" state.
        /// </summary>
        public ImageSource OnStateSource
        {
            get => (ImageSource)GetValue(OnStateImageProperty);
            set => SetValue(OnStateImageProperty, value);
        }

        /// <summary>
        /// State of the toggle button.
        /// True value represents "on" state, false value represents "off" state respectively.
        /// </summary>
        public bool Checked
        {
            get => (bool)GetValue(CheckedProperty);
            set => SetValue(CheckedProperty, value);
        }

        /// <summary>
        /// Text displayed in the foreground of the control.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Creates instance of the control.
        /// </summary>
        public ToggleButton()
        {
            InitializeComponent();
            InitializeEventsListeners();
            SyncImagesWithState();
        }

        /// <summary>
        /// Synchronizes images appearance with control's state.
        /// The appearance of the control is based on changing images opacity
        /// so it updates only this value.
        /// </summary>
        private void SyncImagesWithState()
        {
            OffStateImage.Opacity = Checked ? 0 : 1;
            OnStateImage.Opacity = Checked ? 1 : 0;
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
        /// Performs the state change animation (simple images morphing).
        /// </summary>
        /// <param name="targetState">Animation direction (target state after animation completes).</param>
        /// <returns>A task that represents the animation operation.</returns>
        private Task AnimateStateChange(bool targetState)
        {
            return Task.WhenAll(new List<Task>
            {
                OffStateImage.FadeTo(targetState ? 0 : 1, ANIMATION_TIME),
                OnStateImage.FadeTo(targetState ? 1 : 0, ANIMATION_TIME)
            });
        }

        /// <summary>
        /// Cancels the state change animation.
        /// </summary>
        private void CancelStateChangeAnimation()
        {
            ViewExtensions.CancelAnimations(OffStateImage);
            ViewExtensions.CancelAnimations(OnStateImage);
        }

        /// <summary>
        /// Returns true if state change animation is running, false otherwise.
        /// </summary>
        /// <returns>True in case of running animation, false otherwise.</returns>
        private bool StateChangeAnimationIsRunning()
        {
            return OnStateImage.AnimationIsRunning("FadeTo") ||
                OffStateImage.AnimationIsRunning("FadeTo");
        }

        /// <summary>
        /// Handles tap on the control body.
        /// Performs tap animation (simple images morphing)
        /// which finally results in toggling the control's state.
        /// </summary>
        private async void OnTapped()
        {
            if (StateChangeAnimationIsRunning())
            {
                return;
            }

            await AnimateStateChange(!Checked);
            Checked = !Checked;
        }

        /// <summary>
        /// Handles bound property change.
        /// If "Checked" property was changed, it updates the control's appearance.
        /// This is crucial when state was changed by changing
        /// the property directly (not by the user).
        /// </summary>
        /// <param name="propertyName">Name of the property which was changed.</param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Checked))
            {
                CancelStateChangeAnimation();
                SyncImagesWithState();
            }
        }

        #endregion
    }
}