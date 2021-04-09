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
using System.IO;
using Xamarin.Forms;

namespace AppInfo.Converters
{
    /// <summary>
    /// FileExistenceConverter class to convert path of non-existent file to string given as parameter.
    /// </summary>
    class FileExistenceConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts path of non-existent file to string given as parameter.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>String representing converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = value as string;
            string defaultPath = parameter as string;

            if (!File.Exists(path))
            {
                path = defaultPath;
            }

            return path;
        }

        /// <summary>
        /// Converts back string value.
        /// Not required by the application, so it is not implemented.
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>Converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}