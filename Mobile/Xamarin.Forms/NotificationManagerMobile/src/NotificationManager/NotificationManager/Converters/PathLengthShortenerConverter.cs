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
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace NotificationManager.Converters
{
    /// <summary>
    /// PathLengthShortenerConverter class.
    /// It implements IValueConverter interface.
    /// Shortens path to just beginning directory dots and filename with extension.
    /// </summary>
    public class PathLengthShortenerConverter : IValueConverter
    {
        #region fields

        /// <summary>
        /// Regular expression used to determine if given string is path or not.
        /// </summary>
        private const string PATTERN = @"(\/[^\/]+\/)(?:[^\/]+\/)*(.*)";

        /// <summary>
        /// Regular expression pattern for replacement.
        /// </summary>
        private const string REPLACEMENT = "$1.../$2";

        #endregion

        #region methods

        /// <summary>
        /// Shortens path to first directory and file name with dots between them.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Short path.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path && Regex.IsMatch(path, PATTERN))
            {
                value = Regex.Replace(path, PATTERN, REPLACEMENT);
            }

            return value;
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