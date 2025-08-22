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
using QRCodeGenerator.Tizen.Wearable.Views;
using QRCodeGenerator.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(PageNavigation))]

namespace QRCodeGenerator.Tizen.Wearable.Views
{
    /// <summary>
    /// Application navigation class.
    /// </summary>
    class PageNavigation : IPageNavigation
    {
        #region methods

        /// <summary>
        /// Creates application main page and set it as active.
        /// </summary>
        public void CreateMainPage()
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        /// <summary>
        /// Navigates to settings.
        /// </summary>
        public void GoToSettings()
        {
            Application.Current.MainPage.Navigation.PushAsync(new SettingsPage());
        }

        /// <summary>
        /// Navigates to page with generated QR code.
        /// </summary>
        public void GoToQR()
        {
            Application.Current.MainPage.Navigation.PushAsync(new QRPage());
        }

        /// <summary>
        /// Navigates to previous page.
        /// </summary>
        public void GoToPreviousPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// Navigates to page with SSID settings.
        /// </summary>
        public void GoToSSIDPage()
        {
            Application.Current.MainPage.Navigation.PushAsync(new SSIDPage());
        }

        /// <summary>
        /// Navigates to page with password settings.
        /// </summary>
        public void GoToPasswordPage()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PasswordPage());
        }

        /// <summary>
        /// Navigates to page with encryption settings.
        /// </summary>
        public void GoToEncryptionPage()
        {
            Application.Current.MainPage.Navigation.PushAsync(new EncryptionPage());
        }

        #endregion
    }
}