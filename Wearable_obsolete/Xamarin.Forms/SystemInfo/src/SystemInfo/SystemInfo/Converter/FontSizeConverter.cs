/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using SystemInfo.Utils;
using Xamarin.Forms;

namespace SystemInfo.Converter
{
    /// <summary>
    /// Class that converts font size for different devices (mobile or TV).
    /// </summary>
    public class FontSizeConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts font size.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
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
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Original font size.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}