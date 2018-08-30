/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tizen.System;
using WeatherWatch.PageModels;

namespace WeatherWatch.Services
{
    /// <summary>
    /// BatteryInfoService class
    /// Notify
    /// </summary>
    public class BatteryInfoService
    {
        private WeatherWatchPageModel _viewModel;
        private string[] levels = { "red", "orange", "yellow", "green", "blue" };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_vm">WeatherWatchPageModel</param>
        public BatteryInfoService(WeatherWatchPageModel _vm)
        {
            _viewModel = _vm;
            Update(Battery.Percent);
        }

        /// <summary>
        /// Start listening for the remaining battery percentage information
        /// </summary>
        public void RegisterBatteryEvent()
        {
            Battery.PercentChanged += Battery_PercentChanged;
        }

        /// <summary>
        /// Stop listening for the remaining battery percentage information
        /// </summary>
        public void UnregisterBatteryEvent()
        {
            Battery.PercentChanged -= Battery_PercentChanged;
        }

        /// <summary>
        /// Called whenever the remaining battery percentage changes
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">BatteryPercentChangedEventArgs</param>
        private void Battery_PercentChanged(object sender, BatteryPercentChangedEventArgs e)
        {
            //Update UI with the remaining battery percentage
            Update(e.Percent);
        }

        /// <summary>
        /// Update battery UI
        /// </summary>
        /// <param name="percent">remaining battery percentage</param>
        void Update(int percent)
        {
            Console.WriteLine("[Battery.Update] " + percent + "%");
            int index = (percent == 0) ? 0 : (percent / 20) - 1;
            _viewModel.BatteryPercent = percent + "%";
            _viewModel.BatteryIconPath = "color_status/battery_icon_" + levels[index] + ".png";
            _viewModel.BatteryIndicatorPath = "color_status/" + levels[index] + "_indicator.png";
        }
    }
}
