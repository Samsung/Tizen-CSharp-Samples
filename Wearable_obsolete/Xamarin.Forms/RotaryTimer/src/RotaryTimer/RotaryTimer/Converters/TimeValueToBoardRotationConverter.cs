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
using System;
using System.Globalization;
using Xamarin.Forms;
using RotaryTimer.Utils;

namespace RotaryTimer.Converters
{
    /// <summary>
    /// Converts time values to board rotation.
    /// </summary>
    public class TimeValueToBoardRotationConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts time value to board rotation.
        /// </summary>
        /// <param name="value">Timer setting object.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>Rotation value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double rotation = 0;

            if (value is TimerSetting timerSetting)
            {
                switch (timerSetting.SettingMode)
                {
                    case SettingMode.HOURS:
                        rotation = (double)timerSetting.Hours / 12 * 360;
                        break;
                    case SettingMode.MINUTES:
                        rotation = (double)timerSetting.Minutes / 60 * 360;
                        break;
                    case SettingMode.SECONDS:
                        rotation = (double)timerSetting.Seconds / 60 * 360;
                        break;
                }
            }

            return rotation;
        }

        /// <summary>
        /// Converts board rotation to time values.
        /// Not required by the application, so it is not implemented.
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>Setting mode representing converted filename.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Not implemented");
        }

        #endregion
    }
}
