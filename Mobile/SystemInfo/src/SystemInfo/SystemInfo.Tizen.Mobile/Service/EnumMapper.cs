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

using System.Collections.Generic;
using SystemInfo.Model.Settings;
using Tizen.System;

namespace SystemInfo.Tizen.Mobile.Service
{
    /// <summary>
    /// Class that creates mapping between platform and portable enumerators.
    /// </summary>
    public static class EnumMapper
    {
        #region fields

        /// <summary>
        /// Dictionary of display states.
        /// </summary>
        private static readonly Dictionary<DisplayState, Model.Display.DisplayState> DisplayStateDictionary;

        /// <summary>
        /// Dictionary of battery level statuses.
        /// </summary>
        private static readonly Dictionary<BatteryLevelStatus, Model.Battery.BatteryLevelStatus>
            BatteryLevelStatusDictionary;


        /// <summary>
        /// Dictionary of font sizes.
        /// </summary>
        private static readonly Dictionary<SystemSettingsFontSize, Model.Settings.FontSize> FontSizeDictionary;

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        static EnumMapper()
        {
            DisplayStateDictionary = new Dictionary<DisplayState, Model.Display.DisplayState>
            {
                {DisplayState.Normal, Model.Display.DisplayState.Normal},
                {DisplayState.Dim, Model.Display.DisplayState.Dim},
                {DisplayState.Off, Model.Display.DisplayState.Off}
            };

            BatteryLevelStatusDictionary = new Dictionary<BatteryLevelStatus, Model.Battery.BatteryLevelStatus>
            {
                {BatteryLevelStatus.Empty, Model.Battery.BatteryLevelStatus.Empty},
                {BatteryLevelStatus.Critical, Model.Battery.BatteryLevelStatus.Critical},
                {BatteryLevelStatus.Low, Model.Battery.BatteryLevelStatus.Low},
                {BatteryLevelStatus.High, Model.Battery.BatteryLevelStatus.High},
                {BatteryLevelStatus.Full, Model.Battery.BatteryLevelStatus.Full}
            };

            FontSizeDictionary = new Dictionary<SystemSettingsFontSize, FontSize>
            {
                {SystemSettingsFontSize.Small, FontSize.Small},
                {SystemSettingsFontSize.Normal, FontSize.Normal},
                {SystemSettingsFontSize.Large, FontSize.Large},
                {SystemSettingsFontSize.Huge, FontSize.Huge},
                {SystemSettingsFontSize.Giant, FontSize.Giant}
            };
        }

        /// <summary>
        /// Gets unified display state, common for all supported platform.
        /// </summary>
        /// <param name="displayState">Platform depended display state.</param>
        /// <returns>Common for all supported platform display state.</returns>
        public static Model.Display.DisplayState DisplayStateMapper(DisplayState displayState)
        {
            return DisplayStateDictionary[displayState];
        }

        /// <summary>
        /// Gets unified battery level state, common for all supported platform.
        /// </summary>
        /// <param name="batteryLevelStatus">Platform depended battery level state.</param>
        /// <returns>Common for all supported platform battery level state.</returns>
        public static Model.Battery.BatteryLevelStatus BatteryLevelStatusMapper(BatteryLevelStatus batteryLevelStatus)
        {
            return BatteryLevelStatusDictionary[batteryLevelStatus];
        }

        /// <summary>
        /// Gets unified font size, common for all supported platform.
        /// </summary>
        /// <param name="systemSettingsFontSize">Platform depended font size.</param>
        /// <returns>Common for all supported platform font size.</returns>
        public static FontSize FontSizeMapper(SystemSettingsFontSize systemSettingsFontSize)
        {
            return FontSizeDictionary[systemSettingsFontSize];
        }

        #endregion
    }
}