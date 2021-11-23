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

namespace NotificationManager.Converters
{
    /// <summary>
    /// SliderValueToMsConverter class.
    /// It implements IValueConverter interface. Performs conversion from 0-1 range to 0-'parameter' ms string.
    /// </summary>
    public class SliderValueToMsConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Performs conversion from 0-1 range to 0-'parameter' ms string.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Text describing slider's value in milliseconds.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{System.Convert.ToInt32((double)value * 5000)} ms";
        }

        /// <summary>
        /// Unused.
        /// </summary>
        /// <param name="value">The value produced by the 'Convert' method.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <exception cref="NotImplementedException">Methods is not used, so there is no need to implement it.</exception>
        /// <returns>Never used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}