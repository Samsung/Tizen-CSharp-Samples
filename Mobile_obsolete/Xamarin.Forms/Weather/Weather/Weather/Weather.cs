/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

using Weather.Config;
using Weather.Views;
using Xamarin.Forms;

namespace Weather
{
    /// <summary>
    /// Portable application main class.
    /// </summary>
    public class App : Application
    {
        #region properties

        /// <summary>
        /// Gets or sets value that indicates if weather and forecast for selected city is initialized.
        /// </summary>
        public bool IsInitialized { get; set; }

        #endregion


        #region methods

        /// <summary>
        /// Portable application constructor method.
        /// </summary>
        public App()
        {
            MainPage = new NavigationPage();

            if (!ApiConfig.IsApiKeyDefined())
            {
                MainPage.Navigation.PushAsync(new MissingKeyErrorPage(), false);
            }
            else
            {
                MainPage.Navigation.PushAsync(new MainPage(), false);
            }
        }

        #endregion
    }
}