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

namespace RotaryTimer.Interfaces
{
    /// <summary>
    /// Interface implemented by classes handling navigation over views.
    /// </summary>
    public interface IViewNavigation
    {
        #region methods

        /// <summary>
        /// Returns initial page.
        /// </summary>
        /// <returns>Initial page.</returns>
        NavigationPage GetInitialPage();

        /// <summary>
        /// Changes page to timer page.
        /// </summary>
        void ShowTimerPage();

        /// <summary>
        /// Changes page to the previous one.
        /// </summary>
        void ShowPreviousPage();

        #endregion
    }
}
