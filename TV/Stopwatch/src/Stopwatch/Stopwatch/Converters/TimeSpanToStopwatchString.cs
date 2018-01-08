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
using System.Globalization;
using Xamarin.Forms;

namespace Stopwatch.Converters
{
    /// <summary>
    /// Converter class to convert TimeSpan class instance to human readable string
    /// using stopwatch format.
    /// </summary>
    class TimeSpanToStopwatchString : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts TimeSpan class instance to human readable string
        /// using stopwatch format.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan))
            {
                return String.Empty;
            }

            TimeSpan timeSpanValue = (TimeSpan)value;
            int totalMinutes = (int)timeSpanValue.TotalMinutes;

            return (totalMinutes < 10 ? "0" + totalMinutes : totalMinutes.ToString()) +
                   timeSpanValue.ToString(@"\:ss\:ff");
        }

        /// <summary>
        /// Converts back time string (stopwatch format) to TimeSpan class instance.
        ///
        /// Not required by the application (not implemented).
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converted parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
