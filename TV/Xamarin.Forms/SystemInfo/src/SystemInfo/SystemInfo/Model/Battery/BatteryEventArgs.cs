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

namespace SystemInfo.Model.Battery
{
    /// <summary>
    /// Class that is passed with BatteryInfoChanged event.
    /// </summary>
    public class BatteryEventArgs : System.EventArgs
    {
        #region properties

        /// <summary>
        /// Gets battery level.
        /// </summary>
        public int BatteryLevel { get; }

        /// <summary>
        /// Gets charging status.
        /// </summary>
        public bool IsCharging { get; }

        /// <summary>
        /// Gets battery level status.
        /// </summary>
        public BatteryLevelStatus BatteryLevelStatus { get; }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor that allows to set values describing battery information.
        /// </summary>
        /// <param name="batteryLevel">Battery level.</param>
        /// <param name="isCharging">Charging status.</param>
        /// <param name="batteryLevelStatus">Battery level status.</param>
        public BatteryEventArgs(int batteryLevel, bool isCharging, BatteryLevelStatus batteryLevelStatus)
        {
            BatteryLevel = batteryLevel;
            IsCharging = isCharging;
            BatteryLevelStatus = batteryLevelStatus;
        }

        #endregion
    }
}