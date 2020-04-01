/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
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
using SquatCounter.Views;
using Xamarin.Forms;

namespace SquatCounter.Services
{
    /// <summary>
    /// Provides navigation between pages across application.
    /// This is singleton. Instance accessible via <see cref = Instance></cref> property.
    /// </summary>
    public sealed class PageNavigationService
    {
        /// <summary>
        /// Backing field of Instance property.
        /// </summary>
        private static PageNavigationService _instance;

        /// <summary>
        /// MainModel instance accessor.
        /// </summary>
        public static PageNavigationService Instance
        {
            get => _instance ?? (_instance = new PageNavigationService());
        }

        /// <summary>
        /// Initializes PageNavigationService class instance.
        /// </summary>
        private PageNavigationService()
        {
        }

        /// <summary>
        /// Creates application welcome page and sets it as active.
        /// </summary>
        public void GoToWelcomePage()
        {
            Application.Current.MainPage = new NavigationPage(new WelcomePage());
        }

        /// <summary>
        /// Navigates to guide index page.
        /// </summary>
        public void GoToGuidePage()
        {
            Application.Current.MainPage.Navigation.PushAsync(new GuidePage());
        }

        /// <summary>
        /// Navigates to squat counter page.
        /// </summary>
        public void GoToSquatCounterPage()
        {
            Application.Current.MainPage.Navigation.PushAsync(new SquatCounterPage());
        }
    }
}
