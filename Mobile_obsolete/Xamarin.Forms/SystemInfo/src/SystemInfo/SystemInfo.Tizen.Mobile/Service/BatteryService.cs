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

using System;
using SystemInfo.Model.Battery;
using SystemInfo.Tizen.Mobile.Service;
using Battery = Tizen.System.Battery;

[assembly: Xamarin.Forms.Dependency(typeof(BatteryService))]

namespace SystemInfo.Tizen.Mobile.Service
{
    /// <summary>
    /// Provides methods that allow to obtain information about device's battery.
    /// </summary>
    public class BatteryService : IBattery
    {
        #region properties

        /// <summary>
        /// Gets battery level.
        /// </summary>
        public int BatteryLevel => Battery.Percent;

        /// <summary>
        /// Gets charging status.
        /// </summary>
        public bool IsCharging => Battery.IsCharging;

        /// <summary>
        /// Gets battery level status.
        /// </summary>
        public BatteryLevelStatus BatteryLevelStatus => EnumMapper.BatteryLevelStatusMapper(Battery.Level);

        /// <summary>
        /// Event invoked when some information about battery was changed.
        /// </summary>
        public event EventHandler<BatteryEventArgs> BatteryInfoChanged;

        #endregion

        #region methods

        /// <summary>
        /// Starts observing battery information for changes.
        /// </summary>
        /// <remarks>
        /// Event BatteryInfoChanged will be never invoked before calling this method.
        /// </remarks>
        public void StartListening()
        {
            Battery.PercentChanged += (s, e) =>
            {
                var batteryEventArgs = new BatteryEventArgs(e.Percent, IsCharging, BatteryLevelStatus);
                OnBatteryChanged(batteryEventArgs);
            };

            Battery.ChargingStateChanged += (s, e) =>
            {
                var batteryEventArgs = new BatteryEventArgs(BatteryLevel, e.IsCharging, BatteryLevelStatus);
                OnBatteryChanged(batteryEventArgs);
            };

            Battery.LevelChanged += (s, e) =>
            {
                var batteryEventArgs = new BatteryEventArgs(BatteryLevel, IsCharging,
                    EnumMapper.BatteryLevelStatusMapper(e.Level));
                OnBatteryChanged(batteryEventArgs);
            };
        }

        /// <summary>
        /// Battery property change handler.
        /// </summary>
        /// <param name="e">Event parameters.</param>
        protected virtual void OnBatteryChanged(BatteryEventArgs e)
        {
            BatteryInfoChanged?.Invoke(this, e);
        }

        #endregion
    }
}