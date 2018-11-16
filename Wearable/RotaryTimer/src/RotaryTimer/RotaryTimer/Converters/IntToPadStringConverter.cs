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

namespace RotaryTimer.Converters
{
    /// <summary>
    /// Converts integer to string (with leading zero if needed) and back.
    /// </summary>
    public class IntToPadStringConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts integer value to string.
        /// If value is lower than 10, adds leading "0".
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>Value converted to string.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value).ToString("00");
        }

        /// <summary>
        /// Converts string to integer value.
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>Integer value converted from string.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Int32.Parse((string)value);
        }

        #endregion
    }
}

