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
using Xamarin.Forms;

namespace SpeechToText.Converters
{
    /// <summary>
    /// Converter class to convert boolean value to string.
    /// </summary>
    public class BoolToStringConverter : IValueConverter
    {
        #region fields

        /// <summary>
        /// String value for the "true" value of the input.
        /// </summary>
        private readonly string _trueValue;

        /// <summary>
        /// String value for the "false" value of the input.
        /// </summary>
        private readonly string _falseValue;

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public BoolToStringConverter()
        {
            _trueValue = "True";
            _falseValue = "False";
        }

        /// <summary>
        /// Class constructor which allows to specify string values for the "true" and "false"
        /// boolean states.
        /// </summary>
        /// <param name="trueValue">String value for the "true" value of the input.</param>
        /// <param name="falseValue">String value for the "false" value of the input.</param>
        public BoolToStringConverter(string trueValue, string falseValue)
        {
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        /// <summary>
        /// Converts boolean value to string value.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? _trueValue : _falseValue;
        }

        /// <summary>
        /// Converts back string value to boolean value.
        ///
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
