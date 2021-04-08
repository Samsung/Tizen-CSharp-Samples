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

namespace MetalDetector.Converters
{
    /// <summary>
    /// Class to convert strength level to corresponding image file name.
    /// </summary>
    public class StrengthLevelToFileNameConverter : IValueConverter
    {
        #region fields

        /// <summary>
        /// Filename template.
        /// </summary>
        private const string FILENAME_TEMPLATE = "signal_strength_{0}.png";

        #endregion

        #region methods

        /// <summary>
        /// Converts strength level to corresponding image file name.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>String representing converted date.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return String.Format(FILENAME_TEMPLATE, (int)value);
        }

        /// <summary>
        /// Converts back image file name to strength level.
        /// Not required by the application, so it is not implemented.
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>Date object representing converted string.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
