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
using System.IO;
using VoiceRecorder.Utils;
using Xamarin.Forms;

namespace VoiceRecorder.Converter
{
    /// <summary>
    /// AudioBitRateTypeToTextConverter class.
    /// Converts AudioBitRate into information about given audio bit rate.
    /// Implements IValueConverter interface.
    /// </summary>
    public class AudioBitRateTypeToTextConverter : IValueConverter
    {
        #region fields

        /// <summary>
        /// Audio bit rate high.
        /// </summary>
        private const string AUDIO_BIT_RATE_HIGH = "High / 256 kbps";

        /// <summary>
        /// Audio bit rate medium.
        /// </summary>
        private const string AUDIO_BIT_RATE_MEDIUM = "Mid / 128 kbps";

        /// <summary>
        /// Audio bit rate low.
        /// </summary>
        private const string AUDIO_BIT_RATE_LOW = "Low / 64 kbps";

        /// <summary>
        /// Audio bit rate unknown.
        /// </summary>
        private const string UNKNOWN_AUDIO_BIT_RATE = "Unknown quality";

        #endregion

        #region methods

        /// <summary>
        /// Converts AudioBitRate into information about given audio bit rate.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Information about selected audio bit rate.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is AudioBitRateType audioBitRateType)
            {
                switch (audioBitRateType)
                {
                    case AudioBitRateType.High:
                        return AUDIO_BIT_RATE_HIGH;
                    case AudioBitRateType.Medium:
                        return AUDIO_BIT_RATE_MEDIUM;
                    case AudioBitRateType.Low:
                        return AUDIO_BIT_RATE_LOW;
                    default:
                        return UNKNOWN_AUDIO_BIT_RATE;
                }
            }

            return UNKNOWN_AUDIO_BIT_RATE;
        }

        /// <summary>
        /// Does nothing, but it must be defined because it is in "IValueConverter" interface.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}