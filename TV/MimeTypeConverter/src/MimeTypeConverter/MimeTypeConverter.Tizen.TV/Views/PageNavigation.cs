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
using MimeTypeConverter.Views;
using MimeTypeConverter.Tizen.TV.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(PageNavigation))]
namespace MimeTypeConverter.Tizen.TV.Views
{
    /// <summary>
    /// Page navigation implementation for Tizen TV profile.
    /// </summary>
    public class PageNavigation : IPageNavigation
    {
        #region methods

        /// <summary>
        /// Navigates to the welcome page.
        /// </summary>
        public void ShowWelcomePage()
        {
            App.Current.MainPage = new NavigationPage(new WelcomePage());

        }

        /// <summary>
        /// Navigates to the main application page.
        /// </summary>
        public void ShowMainPage()
        {
            App.Current.MainPage = new NavigationPage(new MainAppPage());
        }

        #endregion
    }
}
