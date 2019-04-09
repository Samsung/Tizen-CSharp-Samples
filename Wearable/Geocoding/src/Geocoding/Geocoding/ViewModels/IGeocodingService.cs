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

namespace Geocoding.ViewModels
{
    /// <summary>
    /// IGeocodingService interface class.
    /// It defines methods that should be implemented by class used to providing geocoding functionality.
    /// </summary>
    public interface IGeocodingService
    {
        #region properties

        /// <summary>
        /// Notifies about user consent.
        /// </summary>
        event EventHandler<IGeocodingUserConsentArgs> UserConsent;

        /// <summary>
        /// Notifies about geocode request success.
        /// </summary>
        event EventHandler GeocodeRequestSuccess;

        /// <summary>
        /// Notifies about lack of results of the geocode request.
        /// </summary>
        event EventHandler GeocodeRequestNotFound;

        /// <summary>
        /// Notifies about lack of connection to the map provider during the geocode request.
        /// </summary>
        event EventHandler GeocodeRequestConnectionFailed;

        /// <summary>
        /// Notifies about received coordinates.
        /// </summary>
        event EventHandler<IGeocodingCoordinatesArgs> CoordinatesReceived;

        /// <summary>
        /// Notifies about center point coordinates.
        /// </summary>
        event EventHandler<IGeocodingCoordinatesArgs> CenterPointCalculated;

        #endregion

        #region methods

        /// <summary>
        /// Parses data provided with the last geocoding request.
        /// </summary>
        void ParseLastGeocodeResponse();

        /// <summary>
        /// Requests user's consent to the map provider's license terms.
        /// </summary>
        void RequestUserConsent();

        /// <summary>
        /// Creates request to translate given address to its geographical location.
        /// </summary>
        /// <param name="address">Address value.</param>
        void CreateGeocodeRequestMethod(string address);

        #endregion
    }
}
