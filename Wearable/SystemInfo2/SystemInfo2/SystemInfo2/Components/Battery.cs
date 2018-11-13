//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Xamarin.Forms;
using TBattery = Tizen.System.Battery;

namespace SystemInfo.Components
{
    /// <summary>
    /// An easy to access fascade for Tizen.System.Battery with information like battery level, charging status and more. 
    /// </summary>
    public class Battery : BindableObject
    {
        #region properties

        /// <summary>
        /// Gets battery level (percentage) using Tizen.System.Battery 
        /// </summary>
        public int BatteryLevel => TBattery.Percent;

        /// <summary>
        /// Gets charging status using Tizen.System.Battery 
        /// </summary>
        public bool IsCharging => TBattery.IsCharging;

        /// <summary>
        /// Gets battery level status (high / low / critical) using Tizen.System.Battery 
        /// </summary>
        public string BatteryLevelStatus => TBattery.Level.ToString();

        #endregion

        /// <summary>
        /// Creates an instance of Battery class and ensures relevant Tizen.Battery events are handled 
        /// </summary>
        public Battery()
        {
            // Make sure to capture battery level and charging state changes
            // Two events of interest - coming from using Tizen.System.Battery 
            TBattery.PercentChanged += OnPercentChanged;
            TBattery.ChargingStateChanged += OnChargingStateChanged;
        }

        /// <summary>
        /// Cleans up the events assignment
        /// </summary>
        ~Battery()
        {            
            TBattery.PercentChanged -= OnPercentChanged;
            TBattery.ChargingStateChanged -= OnChargingStateChanged;
        }

        /// <summary>
        /// Handle TizenBatteryPercentChagedEvent by letting others know they need to check the levels again.
        /// </summary>
        private void OnPercentChanged (object sender, Tizen.System.BatteryPercentChangedEventArgs args)
        {
            // If this happens - two properties could have changed
            OnPropertyChanged(nameof(BatteryLevel));
            OnPropertyChanged(nameof(BatteryLevelStatus));
        }

        /// <summary>
        /// Handle ChargingStateChangedEvent by letting others know they need to check relevant property state again.
        /// </summary>
        private void OnChargingStateChanged(object sender, Tizen.System.BatteryChargingStateChangedEventArgs args)
        {
            OnPropertyChanged(nameof(IsCharging));
        }

    }
}
