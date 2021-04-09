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
using Geocoding.Tizen.Wearable.Services;
using Geocoding.ViewModels;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(InformationPopupService))]
namespace Geocoding.Tizen.Wearable.Services
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
        /// Gets or sets text displayed on the no results popup.
        /// </summary>
        public string NoResultsPopupText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the no results popup button.
        /// </summary>
        public string NoResultsPopupButtonText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the connection failed popup.
        /// </summary>
        public string ConnectionFailedPopupText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the connection failed popup button.
        /// </summary>
        public string ConnectionFailedPopupButtonText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the "Find more markers" popup.
        /// </summary>
        public string FindMoreMarkersPopupText { get; set; }

        /// <summary>
        /// Gets or sets text displayed on the "Find more markers" popup button.
        /// </summary>
        public string FindMoreMarkersPopupButtonText { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Dismisses the popup.
        /// </summary>
        public void Dismiss()
        {
            _informationPopup.Dismiss();
        }

        /// <summary>
        /// Shows no results popup.
        /// </summary>
        public void ShowNoResultsPopup()
        {
            MenuItem bottomButton = new MenuItem()
            {
                Text = NoResultsPopupButtonText,
                Command = new Command(() =>
                {
                    Dismiss();
                })
            };

            _informationPopup = new InformationPopup();
            _informationPopup.Text = NoResultsPopupText;
            _informationPopup.BottomButton = bottomButton;
            _informationPopup.Show();
        }

        /// <summary>
        /// Shows connection failed popup.
        /// </summary>
        public void ShowConnectionFailedPopup()
        {
            MenuItem bottomButton = new MenuItem()
            {
                Text = ConnectionFailedPopupButtonText,
                Command = new Command(() =>
                {
                    Dismiss();
                })
            };

            _informationPopup = new InformationPopup();
            _informationPopup.Text = ConnectionFailedPopupText;
            _informationPopup.BottomButton = bottomButton;
            _informationPopup.Show();
        }

        /// <summary>
        /// Shows "Find more markers" popup.
        /// </summary>
        public void ShowFindMoreMarkersPopup()
        {
            MenuItem bottomButton = new MenuItem()
            {
                Text = FindMoreMarkersPopupButtonText,
                Command = new Command(() =>
                {
                    Dismiss();
                })
            };

            _informationPopup = new InformationPopup();
            _informationPopup.Text = FindMoreMarkersPopupText;
            _informationPopup.BottomButton = bottomButton;
            _informationPopup.Show();
        }

        #endregion
    }
}
