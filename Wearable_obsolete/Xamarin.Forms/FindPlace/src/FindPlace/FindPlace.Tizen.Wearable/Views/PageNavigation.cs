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
using FindPlace.Tizen.Wearable.Views;
using FindPlace.ViewModels;
using FindPlace.Views;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(PageNavigation))]
namespace FindPlace.Tizen.Wearable.Views
{
    /// <summary>
    /// Provides methods which allow to navigate between pages.
    /// </summary>
    public class PageNavigation : IPageNavigation
    {
        #region fields

        /// <summary>
        /// Field for map page, as creating new instance is very costly.
        /// </summary>
        private MapPage _mapPage;

        #endregion

        #region methods

        /// <summary>
        /// Navigates to welcome page and sets it as active.
        /// </summary>
        /// <param name="welcomeViewModel">Welcome page view model.</param>
        public void NavigateToWelcomePage(WelcomeViewModel welcomeViewModel)
        {
            var welcomePage = new WelcomePage();
            welcomePage.BindingContext = welcomeViewModel;
            App.Current.MainPage = welcomePage;
        }

        /// <summary>
        /// Navigates to places page and sets it as active.
        /// </summary>
        /// <param name="placesViewModel">Places page view model.</param>
        public void NavigateToPlacesPage(PlacesViewModel placesViewModel)
        {
            var placesPage = new PlacesPage();
            placesPage.BindingContext = placesViewModel;
            App.Current.MainPage = new NavigationPage(placesPage);
        }

        /// <summary>
        /// Navigates to map page and sets it as active.
        /// </summary>
        /// <param name="mapViewModel">Map page view model.</param>
        public void NavigateToMapPage(MapViewModel mapViewModel)
        {
            _mapPage = _mapPage ?? new MapPage();
            _mapPage.BindingContext = mapViewModel;
            App.Current.MainPage?.Navigation.PushAsync(_mapPage);
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        public void Close()
        {
            try
            {
                global::Tizen.Applications.Application.Current.Exit();
            }
            catch (Exception)
            {
                global::Tizen.Log.Error("FindPlace", "Unable to close the application");
            }
        }

        /// <summary>
        /// Navigates to results page and sets it as active.
        /// </summary>
        /// <param name="resultsViewModel">Results page view model.</param>
        public void NavigateToResultsPage(ResultsViewModel resultsViewModel)
        {
            var resultsPage = new ResultsPage();
            resultsPage.BindingContext = resultsViewModel;
            App.Current.MainPage?.Navigation.PushAsync(resultsPage);
        }

        #endregion
    }
}
