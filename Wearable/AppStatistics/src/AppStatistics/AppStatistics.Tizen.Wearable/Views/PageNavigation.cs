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
using AppStatistics.Views;
using Xamarin.Forms;
using AppStatistics.Tizen.Wearable.Views;
using System;

[assembly: Dependency(typeof(PageNavigation))]

namespace AppStatistics.Tizen.Wearable.Views
{
    /// <summary>
    /// Provides methods which allow to navigate between pages.
    /// </summary>
    public class PageNavigation : IPageNavigation
    {
        #region methods

        /// <summary>
        /// Creates application menu page and sets it as active.
        /// </summary>
        public void CreateMenuPage()
        {
            Application.Current.MainPage = new NavigationPage(new MenuPage());
        }

        /// <summary>
        /// Creates range selection page and sets it as active.
        /// </summary>
        public void CreateRangeSelectionPage()
        {
            Application.Current.MainPage?.Navigation.PushAsync(new RangeSelectionPage());
        }

        /// <summary>
        /// Creates battery page and sets it as active.
        /// </summary>
        /// <param name="context">The page binding context.</param>
        public void CreateBatteryPage(object context)
        {
            Application.Current.MainPage?.Navigation.PushAsync(new BatteryPage(context));
        }

        /// <summary>
        /// Creates launch count page and sets it as active.
        /// </summary>
        /// <param name="context">The page binding context.</param>
        public void CreateLaunchCountPage(object context)
        {
            Application.Current.MainPage?.Navigation.PushAsync(new LaunchCountPage(context));
        }

        /// <summary>
        /// Creates launch count details page and sets it as active.
        /// </summary>
        /// <param name="context">The page binding context.</param>
        public void CreateLaunchCountDetailsPage(object context)
        {
            Application.Current.MainPage?.Navigation.PushAsync(new LaunchCountDetailsPage(context));
        }

        /// <summary>
        /// Creates welcome page and sets it as active.
        /// </summary>
        public void CreateWelcomePage()
        {
            Application.Current.MainPage = new WelcomePage();
        }

        /// <summary>
        /// Creates privilege denied page and sets it as active.
        /// </summary>
        public void CreatePrivilegeDeniedPage()
        {
            Application.Current.MainPage = new NavigationPage(new PrivilegeDeniedPage());
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
                global::Tizen.Log.Error("AppStatistics", "Unable to close the application");
            }
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
