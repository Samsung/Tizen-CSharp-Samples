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
using Geocoding.ViewModels;
using System;

namespace Geocoding.Tizen.Wearable.Services
{
    /// <summary>
    /// GeocodingCoordinatesArgs class.
    /// Defines structure of object being parameter of the CoordinatesReceived and CenterPointCalculated events.
    /// </summary>
    class GeocodingCoordinatesArgs : EventArgs, IGeocodingCoordinatesArgs
    {
        #region properties

        /// <summary>
        /// Latitude value.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Longitude value.
        /// </summary>
        public double Longitude { get; }

        #endregion

        #region methods

        /// <summary>
        /// GeocodingCoordinatesArgs class constructor.
        /// </summary>
        /// <param name="latitude">Latitude value.</param>
        /// <param name="longitude">Longitude value.</param>
        public GeocodingCoordinatesArgs(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        #endregion
    }
}
