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
using Geocoding.Tizen.Wearable.Views;
using Geocoding.Views;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(PageNavigation))]
namespace Geocoding.Tizen.Wearable.Views
{
    /// <summary>
    /// Provides methods which allow to navigate between pages.
    /// </summary>
    public class PageNavigation : IPageNavigation
    {
        #region methods

        /// <summary>
        /// Creates welcome page and sets it as active.
        /// </summary>
        public void CreateWelcomePage()
        {
            Application.Current.MainPage = new WelcomePage();
        }

        /// <summary>
        /// Creates search page and sets it as active.
        /// </summary>
        public void CreateSearchPage()
        {
            Application.Current.MainPage = new NavigationPage(new SearchPage());
        }

        /// <summary>
        /// Creates map page and sets it as active.
        /// </summary>
        public void CreateMapPage()
        {
            Application.Current.MainPage?.Navigation.PushAsync(new MapPage());
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
                global::Tizen.Log.Error("Geocoding", "Unable to close the application");
            }
        }

        #endregion
    }
}
