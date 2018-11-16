/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using Camera.View;
using Xamarin.Forms;

namespace Camera.Navigation
{
    /// <summary>
    /// Provides commands that allow navigation through the application pages.
    /// </summary>
    public class PageNavigation
    {
        #region methods

        /// <summary>
        /// Creates and sets the main page.
        /// </summary>
        public void CreateMainPage()
        {
            Application.Current.MainPage = new NavigationPage(new CameraPage());
        }

        /// <summary>
        /// Navigates to the previous page.
        /// </summary>
        public void NavigateToPreviousPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// Navigates to the photo preview page.
        /// </summary>
        public void NavigateToPreviewPage()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PhotoPreviewPage());
        }

        /// <summary>
        /// Navigates to the privilege denied page.
        /// </summary>
        public void NavigateToPrivilegeDeniedPage()
        {
            Application.Current.MainPage = new PopupPage();
        }

        #endregion
    }
}
