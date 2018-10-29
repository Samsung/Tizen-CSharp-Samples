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
using Xamarin.Forms;
using RotaryTimer.Interfaces;
using RotaryTimer.Views;

[assembly: Dependency(typeof(ViewNavigation))]
namespace RotaryTimer.Views
{
    /// <summary>
    /// Application navigation class.
    /// </summary>
    public class ViewNavigation : IViewNavigation
    {
        #region properties

        /// <summary>
        /// The application's navigation instance.
        /// </summary>
        private NavigationPage Navigation { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Returns initial page.
        /// </summary>
        /// <returns>Initial page.</returns>
        public NavigationPage GetInitialPage()
        {
            Navigation = new NavigationPage(new SetTimeView());

            return Navigation;
        }

        /// <summary>
        /// Changes page to timer page.
        /// </summary>
        public void ShowTimerPage()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Navigation.PushAsync(new TimerView());
            });
        }

        /// <summary>
        /// Changes page to the previous one.
        /// </summary>
        public void ShowPreviousPage()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Navigation.PopAsync();
            });
        }

        #endregion
    }
}
