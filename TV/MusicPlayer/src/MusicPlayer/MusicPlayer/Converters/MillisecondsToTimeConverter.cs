/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Xamarin.Forms;

namespace MusicPlayer.Converters
{
    /// <summary>
    /// Allows to convert milliseconds to time string.
    /// </summary>
    public class MillisecondsToTimeConverter : IValueConverter
    {
        /// <summary>
        /// Converts milliseconds to time string.
        /// </summary>
        /// <param name="value">Number of milliseconds.</param>
        /// <param name="targetType">Target type of conversion.</param>
        /// <param name="parameter">Parameters of conversion.</param>
        /// <param name="culture">Provides information about a specific culture.</param>
        /// <returns>String representing time (in format "hh:mm:ss").</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return TimeSpan.FromMilliseconds((int)value).ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// Converts time string to milliseconds.
        ///
        /// Not required by the application (not implemented).
        /// </summary>
        /// <param name="value">Time string.</param>
        /// <param name="targetType">Target type of conversion.</param>
        /// <param name="parameter">Parameters of conversion.</param>
        /// <param name="culture">Provides information about a specific culture.</param>
        /// <returns>Integer representing time in milliseconds.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
