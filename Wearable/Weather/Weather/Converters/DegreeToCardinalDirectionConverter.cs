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

namespace Weather.Converters
{
    /// <summary>
    /// Class that converts double value to cardinal direction.
    /// </summary>
    public class DegreeToCardinalDirectionConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts double to cardinal value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Cardinal direction.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var directions = new[]
            {
                "North", "North-East", "East", "South-East", "South", "South-West", "West", "North-West", "North"
            };

            return value is double degree ? directions[(int)Math.Round(degree % 360 / 45)] : string.Empty;
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