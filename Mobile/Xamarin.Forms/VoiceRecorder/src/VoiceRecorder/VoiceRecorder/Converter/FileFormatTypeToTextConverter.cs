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
    /// FileFormatTypeToTextConverter class.
    /// Converts FileFormatType into file format name.
    /// Implements IValueConverter interface.
    /// </summary>
    public class FileFormatTypeToTextConverter : IValueConverter
    {
        #region fields

        /// <summary>
        /// Used when "value" is not FileFormatType.
        /// </summary>
        private const string UNKNOWN_FILE_FORMAT = "File format unknown";

        #endregion

        #region methods

        /// <summary>
        /// Converts FileFormatType into file format name.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>File format name.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FileFormatType fileFormat)
            {
                return fileFormat.ToString();
            }

            return UNKNOWN_FILE_FORMAT;
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