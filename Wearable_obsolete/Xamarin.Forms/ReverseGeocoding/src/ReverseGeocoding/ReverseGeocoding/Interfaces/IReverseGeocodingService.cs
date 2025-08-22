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
using ReverseGeocoding.Common;
using ReverseGeocoding.ViewModels;
using System;

namespace ReverseGeocoding.Interfaces
{
    /// <summary>
    /// Provides methods to use the maps and their services.
    /// </summary>
    public interface IReverseGeocodingService
    {
        #region properties

        /// <summary>
        /// Notifies about user consent.
        /// </summary>
        event EventHandler<IReverseGeocodingUserConsentArgs> UserConsent;

        /// <summary>
        /// Notifies about receiving response.
        /// </summary>
        event EventHandler<IReverseGeocodingResponseArgs> ResponseReceived;

        #endregion

        #region methods

        /// <summary>
        /// Requests to check user consent.
        /// </summary>
        void RequestUserConsent();

        /// <summary>
        /// Creates and sends request to reverse geocode service.
        /// </summary>
        /// <param name="pointGeocoordinates">Coordinates of the point.</param>
        void CreateReverseGeocodeRequest(PointGeocoordinates pointGeocoordinates);

        #endregion
    }
}
