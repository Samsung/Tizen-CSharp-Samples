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
    /// Converter class to convert STT silence detection (enum) to its display name.
    /// </summary>
    public class SilenceDetectionToDisplayNameConverter 
    {
        #region methods

        /// <summary>
        /// Converts STT silence detection (enum) to corresponding display name.
        /// </summary>
        /// <param name="value">Value that need to be converted.</param>
        /// <returns>Converted value.</returns>
        public string Convert(SilenceDetection value)
        {
            switch (value)
            {
                case SilenceDetection.Auto:
                    return "Auto";
                case SilenceDetection.False:
                    return "Off";
                case SilenceDetection.True:
                    return "On";
                default:
                    return "Auto";
            }
        }

        /// <summary>
        /// Converts back STT silence detection display name to corresponding enum value.
        ///
        /// Not required by the application (not implemented).
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <returns>Converted value.</returns>
        public SilenceDetection ConvertBack(string value)
        {
            switch (value)
            {
                case "Auto":
                    return SilenceDetection.Auto;
                case "Off":
                    return SilenceDetection.False;
                case "On":
                    return SilenceDetection.True;
                default:
                    return SilenceDetection.Auto;
            }
        }
        #endregion
    }
}
