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
    /// LedSliderValueConverter class.
    /// It implements IValueConverter interface. Perform conversion between 0-1 value
    /// from slider and 0-'parameter' value.
    /// </summary>
    public class SliderValueConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Performs conversion from 0-'parameter' value to 0-1 value from slider.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The value of the slider.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (System.Convert.ToDouble(value) / System.Convert.ToInt32(parameter)).ToString();
        }

        /// <summary>
        /// Performs conversion from 0-1 value from slider to 0-'parameter' value.
        /// </summary>
        /// <param name="value">The value produced by the 'Convert' method.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The time duration value of the binding source.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToInt32(((double)value) * System.Convert.ToInt32(parameter)).ToString();
        }

        #endregion
    }
}