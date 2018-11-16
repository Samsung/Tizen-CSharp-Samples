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

namespace Camera.Converter
{
    /// <summary>
    /// TimerIndicatorPositionConverter class.
    /// Uses slider value to calculate vertical position of the TimerIndicator image.
    /// Implements IValueConverter interface.
    /// </summary>
    public class TimerIndicatorPositionConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Uses slider value to calculate vertical position of the TimerIndicator image.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Rectangle(287, (976 - (int)(454 * (double)value)), 146, 146);
        }

        /// <summary>
        /// Uses vertical position of the TimerIndicator image to set slider value.
        /// Currently not implemented.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
