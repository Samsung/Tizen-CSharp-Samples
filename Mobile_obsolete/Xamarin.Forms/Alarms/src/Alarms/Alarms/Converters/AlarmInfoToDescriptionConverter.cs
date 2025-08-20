/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

using Alarms.ViewModels;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Alarms.Converters
{
    /// <summary>
    /// Class that converts AlarmInfo to description of alarm.
    /// </summary>
    public class AlarmInfoToDescriptionConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts AlarmInfo to description of alarm.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>String with description of alarm.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string retString = string.Empty;

            if (value is AlarmInfoViewModel alarm)
            {
                retString = alarm.Date.Date == DateTime.Today
                    ? alarm.Date.ToString("H:mm")
                    : alarm.Date.ToString("H:mm, d MMMM yyyy");

                if (alarm.DaysFlags.IsAny())
                {
                    retString += "\nrepeated every " + alarm.DaysFlags;
                }

                return retString;
            }

            return retString;
        }

        /// <summary>
        /// Does nothing, but it must be defined, because it is in "IValueConverter" interface.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Private method which generates time period description from number of seconds.
        /// </summary>
        /// <param name="seconds">Number of seconds.</param>
        /// <returns>Text describing number of minutes and seconds.</returns>
        private string FormatSeconds(int seconds)
        {
            if (seconds >= 60)
            {
                string retVal = $"{seconds / 60} minutes";

                if (seconds % 60 > 0)
                {
                    retVal += $" and {seconds % 60} seconds";
                }

                return retVal;
            }
            else
            {
                return $"{seconds} seconds";
            }
        }

        #endregion
    }
}