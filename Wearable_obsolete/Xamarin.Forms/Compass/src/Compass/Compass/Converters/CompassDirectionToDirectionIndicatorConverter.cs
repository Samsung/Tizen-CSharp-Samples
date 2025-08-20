/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using Compass.Utils;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Compass.Converters
{
    /// <summary>
    /// Converts the compass direction to direction indicator.
    /// </summary>
    public class CompassDirectionToDirectionIndicatorConverter : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts the compass direction to direction indicator.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Direction indicator.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CompassDirections compassDirection)
            {
                switch (compassDirection)
                {
                    case CompassDirections.North:
                        return "N";
                    case CompassDirections.NorthEast:
                        return "NE";
                    case CompassDirections.East:
                        return "E";
                    case CompassDirections.SouthEast:
                        return "SE";
                    case CompassDirections.South:
                        return "S";
                    case CompassDirections.SouthWest:
                        return "SW";
                    case CompassDirections.West:
                        return "W";
                    case CompassDirections.NorthWest:
                        return "NW";
                }
            }

            return "";
        }

        /// <summary>
        /// Converts direction indicator to compass direction.
        /// Not required by the application, so it is not implemented.
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
