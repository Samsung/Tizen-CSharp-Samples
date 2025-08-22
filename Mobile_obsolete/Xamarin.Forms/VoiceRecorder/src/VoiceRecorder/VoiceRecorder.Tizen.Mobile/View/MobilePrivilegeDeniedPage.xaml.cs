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
    /// Page informing about privilege denied.
    /// </summary>
    public partial class MobilePrivilegeDeniedPage : ContentPage
    {
        #region fields

        /// <summary>
        /// Text displayed on popup button.
        /// </summary>
        private const string BUTTON_TEXT = "Close application";

        /// <summary>
        /// Popup message.
        /// </summary>
        private const string MESSAGE = "Application will be closed due to required privilege denied.";

        /// <summary>
        /// Popup title.
        /// </summary>
        private const string TITLE = "Privilege denied";

        #endregion

        #region properties

        /// <summary>
        /// Bindable property for ConfirmCommand.
        /// </summary>
        public static BindableProperty ConfirmCommandProperty =
            BindableProperty.Create(nameof(ConfirmCommand), typeof(Command), typeof(MobilePrivilegeDeniedPage));

        /// <summary>
        /// Command to execute when popup dismissed.
        /// </summary>
        public Command ConfirmCommand
        {
            get => (Command)GetValue(ConfirmCommandProperty);
            set => SetValue(ConfirmCommandProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor.
        /// </summary>
        public MobilePrivilegeDeniedPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays popup about privilege denied.
        /// Executes Confirm command.
        /// </summary>
        private async void DisplayPopup()
        {
            await DisplayAlert(TITLE, MESSAGE, BUTTON_TEXT);
            ConfirmCommand?.Execute(null);
        }

        /// <summary>
        /// Called when page appears.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            DisplayPopup();
        }

        #endregion
    }
}