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


using SpeechToText.Model;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace SpeechToText.Converters
{
    /// <summary>
    /// Converter class to convert STT recognition type (enum) to display name.
    /// </summary>
    public class RecognitionTypeToDisplayNameConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts recognition type (enum) to corresponding display name.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "Unknown";
            }

            switch ((RecognitionType)value)
            {
                case RecognitionType.Free:
                    return "Free";
                case RecognitionType.Partial:
                    return "Partial";
                case RecognitionType.Map:
                    return "Map";
                case RecognitionType.Search:
                    return "Search";
                case RecognitionType.WebSearch:
                    return "Web search";
                default:
                    return "Unknown";

            }
        }

        /// <summary>
        /// Converts back recognition type display name to corresponding enum value.
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
