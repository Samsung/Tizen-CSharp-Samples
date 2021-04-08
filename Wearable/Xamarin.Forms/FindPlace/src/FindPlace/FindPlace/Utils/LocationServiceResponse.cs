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
using FindPlace.Model;

namespace FindPlace.Utils
{
    /// <summary>
    /// Defines structure of the location service response.
    /// </summary>
    public class LocationServiceResponse
    {
        #region properties

        /// <summary>
        /// Determines if permission was granted.
        /// </summary>
        public bool PermissionGranted { get; }

        /// <summary>
        /// Determines if response was successful.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Response result.
        /// </summary>
        public Geocoordinates Result { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class.
        /// </summary>
        /// <param name="permissionGranted">Defines if the permission was granted.</param>
        /// <param name="success">Value indicating if the request was successful.</param>
        /// <param name="result">Response result object.</param>
        public LocationServiceResponse(bool permissionGranted, bool success = false, Geocoordinates  result = null)
        {
            PermissionGranted = permissionGranted;
            Success = success;
            Result = result;
        }

        #endregion
    }
}
