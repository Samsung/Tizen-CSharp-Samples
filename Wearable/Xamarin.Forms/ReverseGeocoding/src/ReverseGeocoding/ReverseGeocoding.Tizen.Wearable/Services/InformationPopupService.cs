/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using ReverseGeocoding.Interfaces;
using ReverseGeocoding.Tizen.Wearable.Services;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Command = Xamarin.Forms.Command;

[assembly: Xamarin.Forms.Dependency(typeof(InformationPopupService))]
namespace ReverseGeocoding.Tizen.Wearable.Services
{
    /// <summary>
    /// Provides methods to use the information popup service.
    /// </summary>
    class InformationPopupService : IInformationPopupService
    {
        #region fields

        /// <summary>
        /// Information popup class instance.
        /// </summary>
        InformationPopup _informationPopup;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets text displayed on the loading popup.
        /// </summary>
        public string LoadingText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the error popup.
        /// </summary>
        public string ErrorPopupText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the error popup button.
        /// </summary>
        public string ErrorPopupButtonText { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Displays loading popup.
        /// </summary>
        public void ShowLoadingPopup()
        {
            _informationPopup = new InformationPopup();
            _informationPopup.Title = LoadingText;
            _informationPopup.Text = LoadingText;
            _informationPopup.IsProgressRunning = true;

            _informationPopup.Show();
        }

        /// <summary>
        /// Dismisses the popup.
        /// </summary>
        public void Dismiss()
        {
            _informationPopup.Dismiss();
        }

        /// <summary>
        /// Shows error popup with action button.
        /// </summary>
        public void ShowErrorPopup()
        {
            InformationPopup informationPopup = new InformationPopup();
            MenuItem  bottomButton = new MenuItem()
            {
                Text = ErrorPopupButtonText,
                Command = new Command(() =>
                {
                    informationPopup.Dismiss();
                })
            };

            informationPopup.Text = ErrorPopupText;
            informationPopup.BottomButton = bottomButton;

            informationPopup.Show();
        }

        #endregion
    }
}
