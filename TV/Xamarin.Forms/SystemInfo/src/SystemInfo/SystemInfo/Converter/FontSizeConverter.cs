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
using SystemInfo.Utils;
using Xamarin.Forms;

namespace SystemInfo.Converter
{
    /// <summary>
    /// Class that converts font size for different devices (mobile or TV).
    /// </summary>
    public class FontSizeConverter : IValueConverter
    {
        /// <summary>
        /// Converts font size.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Converted font size.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var size = int.Parse(parameter.ToString());

            switch (App.Device)
            {
                case TizenDevice.Mobile:
                    return size;
                case TizenDevice.TV:
                    return size * 4;
                default:
                    return parameter;
            }
        }
        /// <summary>
        /// Converts back font size.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Original font size.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}