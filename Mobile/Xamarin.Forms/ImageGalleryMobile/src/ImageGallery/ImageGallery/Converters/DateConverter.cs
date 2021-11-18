/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ImageGallery.Converters
{
    /// <summary>
    /// DateConverter class to convert date to suitable string.
    /// </summary>
    public class DateConverter : IValueConverter
    {
        #region fields

        /// <summary>
        /// Today constant text value.
        /// </summary>
        private const string TODAY = "Today";

        /// <summary>
        /// Yesterday constant text value.
        /// </summary>
        private const string YESTERDAY = "Yesterday";

        #endregion

        #region methods

        /// <summary>
        /// Converts date to string value.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>String representing converted date.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeOffset date = (DateTimeOffset)value;
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset yesterday = now.AddDays(-1);

            string param = System.Convert.ToString(parameter);
            string result;

            if (date.Date == now.Date)
            {
                result = TODAY;
            }
            else if (date.Date == yesterday.Date)
            {
                result = YESTERDAY;
            }
            else
            {
                result = $"{date.Day} {date:MMMM}";
            }

            switch (param.ToUpper())
            {
                case "U":
                    return ((string)result).ToUpper();
                case "L":
                    return ((string)result).ToLower();
                case "P":
                    return $"{date.Day}. {date:MMMM} {date:yyyy}";
                default:
                    return ((string)result);
            }
        }

        /// <summary>
        /// Converts back string value to date.
        /// Not required by the application, so it is not implemented.
        /// </summary>
        /// <param name="value">Value to be converted back.</param>
        /// <param name="targetType">Type of the data binding target property.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">CultureInfo object allowing culture specific conversion.</param>
        /// <returns>Date object representing converted string.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
