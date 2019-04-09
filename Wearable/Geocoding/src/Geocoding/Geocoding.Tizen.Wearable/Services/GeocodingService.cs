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
using Geocoding.Tizen.Wearable.Services;
using Geocoding.ViewModels;
using System;
using System.Collections.Generic;
using Tizen.Maps;
using Xamarin.Forms;
using System.Linq;

[assembly: Xamarin.Forms.Dependency(typeof(GeocodingService))]
namespace Geocoding.Tizen.Wearable.Services
{
    /// <summary>
    /// Service which allows to manage geolocation data provided by the Tizen maps.
    /// </summary>
    class GeocodingService : IGeocodingService
    {
        #region fields

        /// <summary>
        /// Not found error message.
        /// </summary>
        private const string NOT_FOUND_ERROR = "NotFound";

        /// <summary>
        /// Network unreachable error message.
        /// </summary>
        private const string NETWORK_UNREACHABLE_ERROR = "NetworkUnreachable";

        /// <summary>
        /// Invalid operation error message.
        /// </summary>
        private const string INVALID_OPERATION_ERROR = "InvalidOperation";

        /// <summary>
        /// MapService class instance used for getting the map service data.
        /// </summary>
        public MapService _mapService;

        /// <summary>
        /// The last response containing geocoding data.
        /// </summary>
        private IEnumerable<Geocoordinates> _latestGeocodeResponse;

        #endregion

        #region properties

        /// <summary>
        /// Notifies about user consent.
        /// </summary>
        public event EventHandler<IGeocodingUserConsentArgs> UserConsent;

        /// <summary>
        /// Notifies about geocode request success.
        /// </summary>
        public event EventHandler GeocodeRequestSuccess;

        /// <summary>
        /// Notifies about lack of results of the geocode request.
        /// </summary>
        public event EventHandler GeocodeRequestNotFound;

        /// <summary>
        /// Notifies about lack of connection to the map provider during the geocode request.
        /// </summary>
        public event EventHandler GeocodeRequestConnectionFailed;

        /// <summary>
        /// Notifies about received coordinates.
        /// </summary>
        public event EventHandler<IGeocodingCoordinatesArgs> CoordinatesReceived;

        /// <summary>
        /// Notifies about center point coordinates.
        /// </summary>
        public event EventHandler<IGeocodingCoordinatesArgs> CenterPointCalculated;

        #endregion

        #region methods

        /// <summary>
        /// MagnetometerService class constructor.
        /// Creates instance of internal Magnetometer class.
        /// </summary>
        public GeocodingService()
        {
            _mapService = DependencyService.Get<IMapServiceProvider>().GetService();
        }

        /// <summary>
        /// Parses data provided with the last geocoding request.
        /// </summary>
        public void ParseLastGeocodeResponse()
        {
            double latitudeSum = 0;
            double longitudeSum = 0;

            int count = _latestGeocodeResponse.Count();

            if (count == 0)
            {
                return;
            }

            foreach (var result in _latestGeocodeResponse)
            {
                CoordinatesReceived?.Invoke(this, new GeocodingCoordinatesArgs(result.Latitude, result.Longitude));
                latitudeSum += result.Latitude;
                longitudeSum += result.Longitude;
            }

            CenterPointCalculated?.Invoke(this,
                new GeocodingCoordinatesArgs(latitudeSum / count, longitudeSum / count));
        }

        /// <summary>
        /// Requests user's consent to the map provider's license terms.
        /// </summary>
        public async void RequestUserConsent()
        {
            bool isConsent = await _mapService.RequestUserConsent();
            UserConsent?.Invoke(this, new GeocodingUserConsentArgs(isConsent));
        }

        /// <summary>
        /// Creates request to translate given address to its geographical location.
        /// </summary>
        /// <param name="address">Address value.</param>
        public async void CreateGeocodeRequestMethod(string address)
        {
            try
            {
                var request = _mapService.CreateGeocodeRequest(address);
                _latestGeocodeResponse = await request.GetResponseAsync();

                GeocodeRequestSuccess?.Invoke(this, new EventArgs());
            }
            catch (Exception e)
            {
                if (e.Message.Contains(NETWORK_UNREACHABLE_ERROR) || e.Message.Contains(INVALID_OPERATION_ERROR))
                {
                    GeocodeRequestConnectionFailed?.Invoke(this, new EventArgs());
                }

                if (e.Message.Contains(NOT_FOUND_ERROR))
                {
                    GeocodeRequestNotFound?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}
