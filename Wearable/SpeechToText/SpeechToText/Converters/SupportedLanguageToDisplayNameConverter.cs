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
    /// Converter class to convert STT supported language code to corresponding display name.
    /// </summary>
    public class SupportedLanguageToDisplayNameConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts STT supported language code to corresponding display name.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "en_US":
                    return "English US";
                case "es_US":
                    return "Spanish US";
                case "fr_FR":
                    return "French";
                case "ja_JP":
                    return "Japanese";
                case "ko_KR":
                    return "Korean";
                case "zh_CN":
                    return "Chinese";
                case "zh_TW":
                    return "Chinese Taiwan";
                case "zh_SG":
                    return "Chinese Singapore";
                case "zh_HK":
                    return "Chinese Hong Kong";
                case "de_DE":
                    return "German";
                case "ru_RU":
                    return "Russian";
                case "pt_BR":
                    return "Portuguese Brasil";
                case "es_ES":
                    return "Spanish";
                case "en_GB":
                    return "English GB";
                case "it_IT":
                    return "Italian";
                default:
                    return value;
            }
        }

        /// <summary>
        /// Converts back STT supported language display name to corresponding code (string).
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
