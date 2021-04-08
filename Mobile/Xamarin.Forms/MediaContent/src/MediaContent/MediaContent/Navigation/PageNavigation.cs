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

﻿using MediaContent.Views;
using Xamarin.Forms;

namespace MediaContent.Navigation
{
    /// <summary>
    /// Provides commands that allows navigation through the application pages.
    /// </summary>
    public class PageNavigation
    {
        #region methods

        /// <summary>
        /// Creates and sets the main page.
        /// </summary>
        public void CreateMainPage()
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        /// <summary>
        /// Navigates to the file info page.
        /// </summary>
        public void NavigateToFileInfoPage()
        {
            Application.Current.MainPage.Navigation.PushAsync(new FileInfoPage());
        }

        /// <summary>
        /// Creates and sets the privilege denied page.
        /// </summary>
        public void NavigateToPrivilegeDeniedPage()
        {
            Application.Current.MainPage = new PrivilegeDeniedPage();
        }

        #endregion
    }
}
