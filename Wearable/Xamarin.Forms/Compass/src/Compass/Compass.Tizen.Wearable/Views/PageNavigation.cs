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
using Compass.Tizen.Wearable.Views;
using Compass.Views;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(PageNavigation))]

namespace Compass.Tizen.Wearable.Views
{
    /// <summary>
    /// Page navigation implementation for wearable profile.
    /// </summary>
    public class PageNavigation : IPageNavigation
    {
        #region methods

        /// <summary>
        /// Creates and sets the main page.
        /// </summary>
        public void CreateMainPage()
        {
            Application.Current.MainPage = new CompassPage();
        }

        #endregion
    }
}
