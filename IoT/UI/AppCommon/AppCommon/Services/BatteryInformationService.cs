/*
 * Copyright (c) 2025 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using AppCommon.Models;
using TBattery = Tizen.System.Battery;

namespace AppCommon.Services
{
    /// <summary>
    /// Implementation of battery information service
    /// </summary>
    public class BatteryInformationService : IBatteryInformationService
    {
        public BatteryInformationService()
        {
            TBattery.PercentChanged += (s, e) =>
            {
                var status = LowBatteryStatus.None;
                if (e.Percent > 5)
                {
                    status = LowBatteryStatus.None;
                }
                else if (e.Percent > 0)
                {
                    status = LowBatteryStatus.CriticalLow;
                }
                else
                {
                    status = LowBatteryStatus.PowerOff;
                }
                OnLevelChanged(new BatteryLevelChangedEventArgs(status));
            };
        }

        public event EventHandler<BatteryLevelChangedEventArgs> LevelChanged;

        void OnLevelChanged(BatteryLevelChangedEventArgs e)
        {
            LevelChanged?.Invoke(this, e);
        }
    }
}
