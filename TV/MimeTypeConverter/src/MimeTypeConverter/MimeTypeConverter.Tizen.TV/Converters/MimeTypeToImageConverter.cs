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
using Xamarin.Forms;

namespace MimeTypeConverter.Tizen.TV.Converters
{
    /// <summary>
    /// Converts string with MIME type to icon file path.
    /// </summary>
    public class MimeTypeToIconPathConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts MIME type to image filepath.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>Icon filepath for given MIME type.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "type-icons/" + value + ".png";
        }

        /// <summary>
        /// Converts icon filename to MIME string.
        /// Not implemented. Application not requires this conversion.
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>MIMEType converted from filepath.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Not implemented");
        }

        #endregion
    }
}

