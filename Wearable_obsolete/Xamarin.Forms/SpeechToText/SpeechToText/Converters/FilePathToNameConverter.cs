//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.


using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace SpeechToText.Converters
{
    /// <summary>
    /// Converter class to convert file path to corresponding file name.
    /// </summary>
    class FilePathToNameConverter : IValueConverter
    {
        #region fields

        /// <summary>
        /// Value which should be returned when specified path cannot be converted.
        /// </summary>
        private string _notConvertibleValue = "";

        #endregion

        #region methods

        /// <summary>
        /// The converter constructor.
        /// </summary>
        /// <param name="notConvertibleValue">Value which should be returned when specified path cannot be converted.</param>
        public FilePathToNameConverter(string notConvertibleValue)
        {
            _notConvertibleValue = notConvertibleValue;
        }

        /// <summary>
        /// Converts file path to corresponding file name.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null)
                {
                    return _notConvertibleValue;
                }

                return Path.GetFileNameWithoutExtension((string)value);
            }
            catch (Exception)
            {
                return _notConvertibleValue;
            }
        }

        /// <summary>
        /// Converts back file name to corresponding file path.
        ///
        /// Not required by the application (not implemented).
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
