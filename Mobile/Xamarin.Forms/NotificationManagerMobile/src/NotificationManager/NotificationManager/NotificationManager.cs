/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using NotificationManager.ViewModels;
using NotificationManager.Views;
using Xamarin.Forms;

namespace NotificationManager
{
    /// <summary>
    /// Portable application part main class.
    /// </summary>
    public class App : Application
    {
        #region properties

        /// <summary>
        /// An instance of the AppNotificationManagerViewModel class.
        /// </summary>
        public NotificationManagerViewModel AppNotificationManagerViewModel { get; }

        #endregion

        #region methods

        /// <summary>
        /// Portable application part constructor method. The root page of application (creating MainPage object).
        /// </summary>
        public App()
        {
            var mainPage = new MainPage();

            AppNotificationManagerViewModel = new NotificationManagerViewModel(
                new Navigation.PageNavigation(mainPage.Navigation));

            MainPage = new NavigationPage(mainPage);
        }

        #endregion
    }
}