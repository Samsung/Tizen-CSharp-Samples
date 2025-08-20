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

namespace Alarms.Views
{
    /// <summary>
    /// Page navigation interface.
    /// </summary>
    public interface IPageNavigation
    {
        #region methods

        /// <summary>
        /// Creates and sets main application page.
        /// </summary>
        void CreateMainPage();

        /// <summary>
        /// Goes to page with application choice for new alarm.
        /// </summary>
        void GoToAppSelectPage();

        /// <summary>
        /// Goes to page with new alarm type choice.
        /// </summary>
        /// <param name="bindingContext">Binding context.</param>
        void GoToAlarmTypeSelectPage(object bindingContext);

        /// <summary>
        /// Goes to page with new alarm details settings for delay type alarm.
        /// </summary>
        /// <param name="bindingContext">Binding context.</param>
        void GoToDelayAlarmSettingsPage(object bindingContext);

        /// <summary>
        /// Goes to page with new alarm details settings for exact date type alarm.
        /// </summary>
        /// <param name="bindingContext">Binding context.</param>
        void GoToDateAlarmSettingsPage(object bindingContext);

        /// <summary>
        /// Goes to page with list of alarms.
        /// </summary>
        void GoToAlarmListPage();

        /// <summary>
        /// Goes to page with no alarms information.
        /// </summary>
        void GoToNoAlarmsPage();

        #endregion
    }
}
