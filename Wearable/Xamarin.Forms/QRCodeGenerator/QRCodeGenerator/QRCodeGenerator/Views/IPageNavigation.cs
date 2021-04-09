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
namespace QRCodeGenerator.Views
{
    /// <summary>
    /// Provides methods which allow to navigate between pages.
    /// </summary>
    public interface IPageNavigation
    {
        #region methods

        /// <summary>
        /// Creates application main page and set it as active.
        /// </summary>
        void CreateMainPage();

        /// <summary>
        /// Navigates to settings page.
        /// </summary>
        void GoToSettings();

        /// <summary>
        /// Navigates to page with generated QR code.
        /// </summary>
        void GoToQR();

        /// <summary>
        /// Navigates to previous page.
        /// </summary>
        void GoToPreviousPage();

        /// <summary>
        /// Navigates to page with SSID settings.
        /// </summary>
        void GoToSSIDPage();

        /// <summary>
        /// Navigates to page with password settings.
        /// </summary>
        void GoToPasswordPage();

        /// <summary>
        /// Navigates to page with encryption settings.
        /// </summary>
        void GoToEncryptionPage();

        #endregion
    }
}