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

using System;
using System.IO;
using Xamarin.Forms;

namespace MusicPlayer.Converters
{
    /// <summary>
    /// Allows to convert image in byte array to image source.
    /// </summary>
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        /// <summary>
        /// Converts byte array to image source.
        /// </summary>
        /// <param name="value">Value of the byte array.</param>
        /// <param name="targetType">Target type of conversion.</param>
        /// <param name="parameter">Parameters of conversion.</param>
        /// <param name="culture">Provides information about a specific culture.</param>
        /// <returns>Image source object from byte array or cover for unknown artwork.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;
                var stream = new MemoryStream(imageAsBytes);
                return ImageSource.FromStream(() => stream);
            }
            else
            {
                return ImageSource.FromFile((string)parameter);
            }
        }

        /// <summary>
        /// Converts image source to byte array.
        ///
        /// Not required by the application (not implemented).
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <param name="targetType">Target type of conversion.</param>
        /// <param name="parameter">Parameters of conversion.</param>
        /// <param name="culture">Provides information about a specific culture.</param>
        /// <returns>Converted object.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
