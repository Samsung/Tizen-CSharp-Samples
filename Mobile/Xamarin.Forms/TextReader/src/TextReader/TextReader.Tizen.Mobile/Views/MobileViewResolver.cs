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

using TextReader.Tizen.Mobile.Views;
using TextReader.Views;
using Xamarin.Forms;

[assembly:Xamarin.Forms.Dependency(typeof(MobileViewResolver))]
namespace TextReader.Tizen.Mobile.Views
{
    /// <summary>
    /// Tizen mobile view resolver class.
    /// Allows to obtain views for mobile device.
    /// </summary>
    public class MobileViewResolver : IViewResolver
    {
        #region fields

        /// <summary>
        /// Root page instance.
        /// </summary>
        private NavigationPage _rootPage;

        #endregion

        #region methods

        /// <summary>
        /// Returns root page for Tizen mobile application.
        /// </summary>
        /// <returns>Root page of the application.</returns>
        public Page GetRootPage()
        {
            if (_rootPage != null)
            {
                return _rootPage;
            }

            MainPage main = new MainPage();

            object color;
            if (main.Resources == null)
            {
                color = Color.White;
            }
            else
            {
                color = main.Resources["MainColor"];
            }

            _rootPage = new NavigationPage(main)
            {
                BarBackgroundColor = (Color)color
            };

            return _rootPage;
        }

        #endregion
    }
}
