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
using System.Globalization;
using Xamarin.Forms;

namespace MusicPlayer.Converters
{
    /// <summary>
    /// Provides methods to negate boolean value.
    /// </summary>
    class NegateBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts boolean value to opposite value.
        /// </summary>
        /// <param name="value">Boolean value.</param>
        /// <param name="targetType">Target type of conversion.</param>
        /// <param name="parameter">Parameters of conversion.</param>
        /// <param name="culture">Provides information about a specific culture.</param>
        /// <returns>Opposite boolean value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        /// <summary>
        /// Converts boolean value to opposite value.
        ///
        /// Not required by the application (not implemented).
        /// </summary>
        /// <param name="value">Boolean value.</param>
        /// <param name="targetType">Target type of conversion.</param>
        /// <param name="parameter">Parameters of conversion.</param>
        /// <param name="culture">Provides information about a specific culture.</param>
        /// <returns>Opposite boolean value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
