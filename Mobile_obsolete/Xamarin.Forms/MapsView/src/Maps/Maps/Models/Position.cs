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

namespace Maps.Models
{
    /// <summary>
    /// Defines object with coordinates.
    /// </summary>
    public class Position
    {
        #region properties

        /// <summary>
        /// Latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude.
        /// </summary>
        public double Longitude { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Constructs Position object with data provided as parameters.
        /// </summary>
        /// <param name="lat">Initial value for latitude</param>
        /// <param name="lon">Initial value for longitude</param>
        public Position(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        #endregion
    }
}