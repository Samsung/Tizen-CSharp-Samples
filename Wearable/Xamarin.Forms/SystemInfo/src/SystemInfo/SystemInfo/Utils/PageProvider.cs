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
using SystemInfo.View;
using Xamarin.Forms;

namespace SystemInfo.Utils
{
    /// <summary>
    /// Helper class that provides application's pages.
    /// </summary>
    public static class PageProvider
    {
        #region methods

        /// <summary>
        /// Method that provides platform-specific pages.
        /// </summary>
        /// <param name="s">Title of page.</param>
        /// <returns>Returns page by its title. In case of unsupported title empty ContentPage will be returned.</returns>
        public static Page CreatePage(string s)
        {
            switch (s)
            {
                case "Battery":
                    return DependencyService.Get<IPageResolver>().BatteryPage;
                case "Display":
                    return DependencyService.Get<IPageResolver>().DisplayPage;
                case "USB":
                    return DependencyService.Get<IPageResolver>().UsbPage;
                case "Capabilities":
                    return DependencyService.Get<IPageResolver>().CapabilitiesPage;
                case "Settings":
                    return DependencyService.Get<IPageResolver>().SettingsPage;
                case "LED":
                    return DependencyService.Get<IPageResolver>().LedPage;
                case "Vibrator":
                    return DependencyService.Get<IPageResolver>().VibratorPage;
                default:
                    return new ContentPage { Title = "Default " + s };
            }
        }

        #endregion
    }
}