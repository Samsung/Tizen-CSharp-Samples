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

using Badges.Tizen.Mobile.Views;
using Badges.Interfaces;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(MobileViewResolver))]

namespace Badges.Tizen.Mobile.Views
{
    /// <summary>
    /// View resolver class to obtain views for selected device.
    /// </summary>
    public class MobileViewResolver : IViewResolver
    {
        #region fields

        /// <summary>
        /// MainPage reference.
        /// </summary>
        private ContentPage _mainPage;

        #endregion fields

        #region methods

        /// <summary>
        /// Get root page view for this particular device.
        /// </summary>
        /// <returns>Root page view</returns>
        public Page GetRootPage()
        {
            return _mainPage ?? (_mainPage = new MobileMainPage());
        }

        #endregion methods
    }
}
