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
using Weather.Utils;
using Xamarin.Forms;

namespace Weather.Converters
{
    /// <summary>
    /// Class that converts timestamp to time of the day.
    /// </summary>
    public class TimeStampToTimeConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts timestamp to time of the day.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Time of the day.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = string.Empty;

            if (value is ulong time)
            {
                if (parameter is Models.Location.TimeZone timeZone)
                {
                    date = TimeStamp.Convert(time).AddSeconds(timeZone.Offset)
                        .ToString(culture.DateTimeFormat.ShortTimePattern);
                }
                else
                {
                    date = TimeStamp.Convert((ulong)value).ToString(culture.DateTimeFormat.ShortTimePattern);
                }
            }

            return date;
        }

        /// <summary>
        /// Does nothing, but it must be defined, because it is in "IValueConverter" interface.
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