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

using Tizen.Uix.Stt;

namespace SpeechToText.Converters
{
    /// <summary>
    /// Converter class to convert STT recognition type (enum) to display name.
    /// </summary>
    public class RecognitionTypeToDisplayNameConverter
    {
        #region methods

        /// <summary>
        /// Converts recognition type (enum) to corresponding display name.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <returns>Converted value.</returns>
        public string Convert(RecognitionType value)
        {
            switch (value)
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
        /// <returns>Converted value.</returns>
        public RecognitionType ConvertBack(string value)
        {
            switch (value)
            {
                case "Free":
                    return RecognitionType.Free;
                case "Partial":
                    return RecognitionType.Partial;
                case "Map":
                    return RecognitionType.Map;
                case "Search":
                    return RecognitionType.Search;
                case "Web search":
                    return RecognitionType.WebSearch;
                default:
                    return RecognitionType.Free;
            }
        }
        #endregion
    }
}
