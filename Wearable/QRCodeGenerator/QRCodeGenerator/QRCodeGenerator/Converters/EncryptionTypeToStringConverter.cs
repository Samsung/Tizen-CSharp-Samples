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
using QRCodeGenerator.Utils;
using System.Globalization;
using Xamarin.Forms;

namespace QRCodeGenerator.Converters
{
    /// <summary>
    /// Converter class to convert encryption type enum to string.
    /// </summary>
    class EncryptionTypeToStringConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts EncryptionType value to string value.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string encryptionType = string.Empty;

            if (value is EncryptionType encryption)
            {
                switch (encryption)
                {
                    case EncryptionType.WPA:
                        encryptionType = "WPA";
                        break;
                    case EncryptionType.WEP:
                        encryptionType = "WEP";
                        break;
                    case EncryptionType.None:
                        encryptionType = "None";
                        break;
                }
            }

            return encryptionType;
        }

        /// <summary>
        /// Converts string value to EncryptionType.
        /// Not required by the application (not implemented).
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
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