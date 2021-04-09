/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
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

using TextReader.Tizen.Wearable.Views;
using TextReader.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(PageNavigation))]

namespace TextReader.Tizen.Wearable.Views
{
    /// <summary>
    /// Page navigation implementation for Tizen wearable profile.
    /// </summary>
    public class PageNavigation : IPageNavigation
    {
        #region methods

        /// <summary>
        /// Navigates to the 'how-to' page.
        /// </summary>
        public void NavigateToHowToPage()
        {
            Application.Current.MainPage = new NavigationPage(new HowToPage());
        }

        /// <summary>
        /// Navigates to the text reader page.
        /// </summary>
        public void NavigateToTextReader()
        {
            Application.Current.MainPage?.Navigation.PushAsync(new TextReaderPage());
        }

        /// <summary>
        /// Navigates to the welcome page.
        /// </summary>
        public void NavigateToWelcomePage()
        {
            Application.Current.MainPage = new WelcomePage();
        }

        #endregion
    }
}
