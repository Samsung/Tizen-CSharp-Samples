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
using Compass.Views;
using Xamarin.Forms;

namespace Compass
{
    /// <summary>
    /// App class.
    /// </summary>
    public class App : Application
    {
        #region methods

        /// <summary>
        /// App class constructor.
        /// Calls platform specific page manager to create main page.
        /// </summary>
        public App()
        {
            DependencyService.Get<IPageNavigation>().CreateMainPage();
        }

        #endregion
    }
}
