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

using BasicCalculator.Tizen.TV.Views;
using BasicCalculator.Views;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(TvViewResolver))]

namespace BasicCalculator.Tizen.TV.Views
{
    /// <summary>
    /// Page resolver implementation for Tizen.TV.
    /// </summary>
    public class TvViewResolver : IViewResolver
    {
        #region fields

        /// <summary>
        /// Main page object reference used by <see cref="GetRootPage"/> method.
        /// </summary>
        private ContentPage _mainPage;

        #endregion fields

        #region methods

        /// <summary>
        /// Get root page view for this particular device.
        /// Initializes root page if not defined.
        /// </summary>
        /// <returns>MainPage object.</returns>
        public Page GetRootPage()
        {
            return _mainPage ?? (_mainPage = new TvMainView());
        }

        #endregion methods
    }
}
