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

using SystemInfo.Model.Battery;
using SystemInfo.Model.Display;

namespace SystemInfo.ViewModel
{
    /// <summary>
    /// ViewModel class for footer control.
    /// </summary>
    public class FooterViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Local storage of battery's percent value.
        /// </summary>
        private string _batteryPercent;

        /// <summary>
        /// Local storage of display's brightness value.
        /// </summary>
        private string _displayPercent;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets battery's percent value.
        /// </summary>
        public string BatteryPercent
        {
            get => _batteryPercent;
            set
            {
                if (_batteryPercent != null && _batteryPercent.Equals(value))
                {
                    return;
                }

                _batteryPercent = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets display's brightness value.
        /// </summary>
        public string DisplayPercent
        {
            get => _displayPercent;
            set
            {
                if (_displayPercent != null && _displayPercent.Equals(value))
                {
                    return;
                }

                _displayPercent = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public FooterViewModel()
        {
            var battery = new Battery();
            var display = new Display();

            BatteryPercent = battery.Level.ToString();

            double displayMaxBrightness = display.MainDisplayMaxBrightess;

            DisplayPercent = (display.MainDisplayBrightness / displayMaxBrightness * 100.0).ToString();

            battery.BatteryChanged += (s, e) => { BatteryPercent = e.BatteryLevel.ToString(); };

            display.DisplayBrightnessChanged +=
                (s, e) => { DisplayPercent = (e.Brightness / displayMaxBrightness * 100.0).ToString(); };
        }

        #endregion
    }
}