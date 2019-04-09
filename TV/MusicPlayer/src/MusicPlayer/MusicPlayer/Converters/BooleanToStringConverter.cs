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
    /// Allows to convert boolean value to one of the strings from array.
    /// </summary>
    public class BooleanToStringConverter : IValueConverter
    {
        /// <summary>
        /// Converters boolean value to string (possible values passed as parameter).
        /// </summary>
        /// <param name="value">Boolean value.</param>
        /// <param name="targetType">Target type of conversion.</param>
        /// <param name="parameter">String array with possible values.</param>
        /// <param name="culture">Provides information about a specific culture.</param>
        /// <returns>Proper string for given boolean value.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string[] possibleStrings = parameter as string[];
            if (possibleStrings.Length < 2)
            {
                return string.Empty;
            }

            if ((bool)value)
            {
                return possibleStrings[0];
            }
            else
            {
                return possibleStrings[1];
            }
        }

        /// <summary>
        /// Converts string to boolean value.
        ///
        /// Not required by the application (not implemented).
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <param name="targetType">Target type of conversion.</param>
        /// <param name="parameter">String array with possible values.</param>
        /// <param name="culture">Provides information about a specific culture.</param>
        /// <returns>Proper boolean value for given string.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
