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
using ReverseGeocoding.Tizen.Wearable.Views;
using ReverseGeocoding.Views;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(PageNavigation))]

namespace ReverseGeocoding.Tizen.Wearable.Views
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
        /// Creates main page and sets it as active.
        /// </summary>
        public void CreateMainPage()
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
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
                global::Tizen.Log.Error("ReverseGeocoding", "Unable to close the application");
            }
        }

        /// <summary>
        /// Creates results page and sets it as active.
        /// </summary>
        public void CreateResultsPage()
        {
            Application.Current.MainPage?.Navigation.PushAsync(new ResultsPage());
        }

        /// <summary>
        /// Navigates to previous page.
        /// </summary>
        public void GoBack()
        {
            Application.Current.MainPage?.Navigation.PopAsync();
        }

        #endregion
    }
}
