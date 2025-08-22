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
using ReverseGeocoding.Interfaces;
using System;

namespace ReverseGeocoding.Tizen.Wearable.Services
{
    /// <summary>
    /// Defines properties that are used for the service response.
    /// </summary>
    class ReverseGeocodingResponseArgs : EventArgs, IReverseGeocodingResponseArgs
    {
        #region properties

        /// <summary>
        /// Determines if response was successful.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Response result.
        /// </summary>
        public string Result { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class.
        /// </summary>
        /// <param name="success">Defines if the request was successful.</param>
        /// <param name="result">Defines response result.</param>
        public ReverseGeocodingResponseArgs(bool success, string result = "")
        {
            Success = success;
            Result = result;
        }

        #endregion
    }
}
