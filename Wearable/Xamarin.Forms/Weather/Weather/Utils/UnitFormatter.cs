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

namespace Weather.Utils
{
    /// <summary>
    /// Class that provide custom formatter for string.
    /// </summary>
    public class UnitFormatter : IFormatProvider, ICustomFormatter
    {
        #region methods

        /// <summary>
        /// Gets an object that provides formatting services for the specified type.
        /// </summary>
        /// <param name="formatType">An object that specifies the type of format object to return.</param>
        /// <returns>An instance of the object specified by formatType.</returns>
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation.
        /// </summary>
        /// <param name="fmt">A format string containing formatting specifications.</param>
        /// <param name="arg">An object to format.</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance.</param>
        /// <returns>
        /// The string representation of the arg, formatted as specified by format and formatProvider.
        /// </returns>
        public string Format(string fmt, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
            {
                return string.Empty;
            }

            switch (fmt)
            {
                case "temp":
                {
                    var sign = RegionInfo.CurrentRegion.IsMetric ? "°C" : "°F";
                    return $"{arg:0.0}{sign}";
                }

                case "speed":
                {
                    var sign = RegionInfo.CurrentRegion.IsMetric ? " m/s" : " mph";
                    return $"{arg:0.00}{sign}";
                }

                default:
                {
                    return arg.ToString();
                }
            }
        }

        #endregion
    }
}