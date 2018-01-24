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

using SystemInfo.Utils;
using SystemInfo.View;
using Xamarin.Forms;

namespace SystemInfo
{
    /// <summary>
    /// Portable application main class.
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Indicates if Tizen OS runs on mobile device or TV.
        /// </summary>
        public static TizenDevice Device;

        /// <summary>
        /// Portable application constructor method.
        /// </summary>
        /// <param name="device">Indicates if Tizen OS runs on mobile device or TV.</param>
        public App(TizenDevice device)
        {
            Device = device;

            MainPage = new NavigationPage();
            MainPage.Navigation.PushAsync(DependencyService.Get<IPageResolver>().MainPage, false);
        }
    }
}