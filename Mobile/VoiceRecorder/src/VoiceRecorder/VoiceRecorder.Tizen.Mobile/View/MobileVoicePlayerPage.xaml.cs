/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Xamarin.Forms;

namespace VoiceRecorder.Tizen.Mobile.View
{
    /// <summary>
    /// MobileVoicePlayerPage class.
    /// </summary>
    public partial class MobileVoicePlayerPage : ContentPage
    {
        #region properties

        /// <summary>
        /// Bindable property for BackButtonPressed Command.
        /// </summary>
        public static BindableProperty BackButtonPressedProperty =
            BindableProperty.Create(nameof(BackButtonPressed), typeof(Command), typeof(MobileVoicePlayerPage));

        /// <summary>
        /// Command to execute by pressing the back button.
        /// </summary>
        public Command BackButtonPressed
        {
            get => (Command)GetValue(BackButtonPressedProperty);
            set => SetValue(BackButtonPressedProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// MobileVoicePlayerPage class constructor.
        /// </summary>
        public MobileVoicePlayerPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method called when the back button is pressed.
        /// Executes BackButtonPressed command.
        /// Calls "base.OnBackButtonPressed" method.
        /// </summary>
        /// <returns>Result of "base.OnBackButtonPressed" method.</returns>
        protected override bool OnBackButtonPressed()
        {
            BackButtonPressed?.Execute(null);
            return base.OnBackButtonPressed();
        }

        #endregion
    }
}
