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

﻿using System;
using System.Globalization;
using Ultraviolet.Enums;
using Xamarin.Forms;

namespace Ultraviolet.Converters
{
    /// <summary>
    /// Converter class to convert uv level enum to uv index string.
    /// </summary>
    public class UvLevelToIndex : IValueConverter
    {
        /// <summary>
        /// Convert UvLevel value to uv index string value.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UvLevel level)
            {
                return level.GetUvIndex();
            }

            return null;
        }

        /// <summary>
        /// Converts string value to UvLevel enum.
        /// Not required by the application (not implemented).
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
