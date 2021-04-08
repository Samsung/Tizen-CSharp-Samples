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
using Xamarin.Forms;

namespace VoiceRecorder.Converter
{
    /// <summary>
    /// FullPathToNameConverter class.
    /// Extracts name of a file from a given path.
    /// Implements IValueConverter interface.
    /// </summary>
    public class FullPathToNameConverter : IValueConverter
    {
        #region fields

        /// <summary>
        /// Used as a name of a file if extraction from a path failed.
        /// </summary>
        private const string UNKNOWN_NAME = "Unknown";

        #endregion

        #region methods

        /// <summary>
        /// Extracts a name of a file from a given path.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Name of the file.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return Path.GetFileName((string)value);
            }
            catch
            {
                return UNKNOWN_NAME;
            }
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
