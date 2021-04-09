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
using FindPlace.Interfaces;
using FindPlace.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace FindPlace.ViewModels
{
    /// <summary>
    /// The application's welcome page view model class (abstraction of the view).
    /// </summary>
    public class WelcomeViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private readonly IPageNavigation _pageNavigation;

        /// <summary>
        /// Reference to object handling find place service obtained in constructor using DependencyService.
        /// </summary>
        private readonly IFindPlaceService _findPlaceService;

        #endregion

        #region properties

        /// <summary>
        /// Navigates to places page.
        /// </summary>
        public ICommand GoToPlacesPageCommand { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public WelcomeViewModel()
        {
            _pageNavigation = DependencyService.Get<IPageNavigation>();
            _findPlaceService = DependencyService.Get<IFindPlaceService>();

            GoToPlacesPageCommand = new Command(ExecuteGoToPlacesPageAsync);
        }

        /// <summary>
        /// Handles execution of "GoToPlacesPageCommand".
        /// </summary>
        private async void ExecuteGoToPlacesPageAsync()
        {
            var isConsent = _findPlaceService.GetUserConsentAsync();
            if (await isConsent)
            {
                _pageNavigation.NavigateToPlacesPage(new PlacesViewModel());
            }
            else
            {
                _pageNavigation.Close();
            }
        }

        #endregion
    }
}
