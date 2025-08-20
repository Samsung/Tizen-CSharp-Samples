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
    /// Converts time values to rotation.
    /// </summary>
    public class TimeValueToIndicatorRotationConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts time values to rotation value for time unit indicators.
        /// </summary>
        /// <param name="value">Timer setting object.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Indicator type.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>Rotation value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double indicatorRotation = 0;
            double boardRotation = 0;

            if (value is TimerSetting ts && parameter is SettingMode indicator)
            {
                switch (ts.SettingMode)
                {
                    case SettingMode.HOURS:
                        boardRotation = (double)ts.Hours / 12 * 360;
                        if (indicator == SettingMode.HOURS)
                        {
                            indicatorRotation = 0;
                        }
                        else if (indicator == SettingMode.MINUTES)
                        {
                            indicatorRotation = -((double)ts.Minutes) / 60 * 360 + boardRotation;
                        }
                        else if (indicator == SettingMode.SECONDS)
                        {
                            indicatorRotation = -((double)ts.Seconds) / 60 * 360 + boardRotation;
                        }

                        break;

                    case SettingMode.MINUTES:
                        boardRotation = (double)ts.Minutes / 60 * 360;
                        if (indicator == SettingMode.HOURS)
                        {
                            indicatorRotation = -((double)ts.Hours) / 12 * 360 + boardRotation;
                        }
                        else if (indicator == SettingMode.MINUTES)
                        {
                            indicatorRotation = 0;
                        }
                        else if (indicator == SettingMode.SECONDS)
                        {
                            indicatorRotation = -((double)ts.Seconds) / 60 * 360 + boardRotation;
                        }

                        break;

                    case SettingMode.SECONDS:
                        boardRotation = (double)ts.Seconds / 60 * 360;
                        if (indicator == SettingMode.HOURS)
                        {
                            indicatorRotation = -((double)ts.Hours) / 12 * 360 + boardRotation;
                        }
                        else if (indicator == SettingMode.MINUTES)
                        {
                            indicatorRotation = -((double)ts.Minutes) / 60 * 360 + boardRotation;
                        }
                        else if (indicator == SettingMode.SECONDS)
                        {
                            indicatorRotation = 0;
                        }

                        break;
                }
            }

            return indicatorRotation;
        }

        /// <summary>
        /// Converts rotation to time values.
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