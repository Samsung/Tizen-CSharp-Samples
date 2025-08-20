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
using Geocoding.ViewModels;
using Geocoding.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Geocoding
{
    /// <summary>
    /// Main application class.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        #region fields

        /// <summary>
        /// Text displayed on the no results popup.
        /// </summary>
        private const string NO_RESULTS_POPUP_TEXT = "No results found.";

        /// <summary>
        /// Text displayed on the connection failed popup.
        /// </summary>
        private const string CONNECTION_FAILED_POPUP_TEXT = "Connection failed.";

        /// <summary>
        /// Text displayed on the "Find more markers" popup.
        /// </summary>
        private const string FIND_MORE_MARKERS_POPUP_TEXT =
            "There are more than one location found. Scroll the map or use the bezel to find them.";

        /// <summary>
        /// OK text of the button displayed on the popup.
        /// </summary>
        private const string OK_POPUP_BUTTON_TEXT = "OK";

        #endregion

        #region methods

        /// <summary>
        /// Initializes application.
        /// </summary>
        public App()
        {
            InitializeComponent();
            InitializeApplicationPopups();

            DependencyService.Get<IPageNavigation>().CreateWelcomePage();
        }

        /// <summary>
        /// Initializes application popups.
        /// </summary>
        private void InitializeApplicationPopups()
        {
            IInformationPopupService popupService = DependencyService.Get<IInformationPopupService>();

            popupService.NoResultsPopupText = NO_RESULTS_POPUP_TEXT;
            popupService.NoResultsPopupButtonText = OK_POPUP_BUTTON_TEXT;

            popupService.ConnectionFailedPopupText = CONNECTION_FAILED_POPUP_TEXT;
            popupService.ConnectionFailedPopupButtonText = OK_POPUP_BUTTON_TEXT;

            popupService.FindMoreMarkersPopupText = FIND_MORE_MARKERS_POPUP_TEXT;
            popupService.FindMoreMarkersPopupButtonText = OK_POPUP_BUTTON_TEXT;
        }

        #endregion
    }
}