/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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

using AppCommon.Tizen.Mobile;
using AppCommon.Interfaces;
using System;
using TBattery = Tizen.System.Battery;

[assembly: Xamarin.Forms.Dependency(typeof(BatteryInformation))]

namespace AppCommon.Tizen.Mobile
{
    /// <summary>
    /// A interface about battery information
    /// </summary>
    public class BatteryInformation : IBatteryInformation
    {
        public BatteryInformation()
        {
            // TODO : if Levelchannged is working well use LevelChanged event not PercentChanged event.
            //TBattery.LevelChanged += (s, e) =>
            //{
            //    Debug.WriteLine("LevelChanged : " + e.Level.ToString());
            //    OnLevelChanged(new BatteryLevelChangedEventArgs());
            //};
            TBattery.PercentChanged += (s, e) =>
            {
                var status = LowBatteryStatus.None;
                if (e.Percent > 5)
                {
                    status = LowBatteryStatus.None;
                    OnLevelChanged(new BatteryLevelChangedEventArgs(status));
                }
            };
        }

        public event EventHandler<BatteryLevelChangedEventArgs> LevelChanged;

        void OnLevelChanged(BatteryLevelChangedEventArgs e)
        {
            LevelChanged?.Invoke(this, e);
        }
    }
}