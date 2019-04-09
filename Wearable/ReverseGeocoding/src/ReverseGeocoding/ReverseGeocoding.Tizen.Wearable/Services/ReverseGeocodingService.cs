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
using ReverseGeocoding.Common;
using ReverseGeocoding.Tizen.Wearable.Services;
using ReverseGeocoding.ViewModels;
using System;
using Tizen.Maps;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ReverseGeocodingService))]

namespace ReverseGeocoding.Tizen.Wearable.Services
{
    /// <summary>
    /// Provides methods to use the maps and their services.
    /// </summary>
    public class ReverseGeocodingService : IReverseGeocodingService
    {
        #region fields

        /// <summary>
        /// MapService class instance used for getting the map service data.
        /// </summary>
        private MapService _mapService;

        #endregion

        #region properties

        /// <summary>
        /// Notifies about user consent.
        /// </summary>
        public event EventHandler<IReverseGeocodingUserConsentArgs> UserConsent;

        /// <summary>
        /// Notifies about receiving response.
        /// </summary>
        public event EventHandler<IReverseGeocodingResponseArgs> ResponseReceived;

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public ReverseGeocodingService()
        {
             _mapService = DependencyService.Get<IMapServiceProvider>().GetService();
        }

        /// <summary>
        /// Requests to check user consent.
        /// </summary>
        public async void RequestUserConsent()
        {
            bool consent = await _mapService.RequestUserConsent();
            UserConsent?.Invoke(this, new ReverseGeocodingUserConsentArgs(consent));
        }

        /// <summary>
        /// Creates and sends request to reverse geocode service.
        /// </summary>
        /// <param name="pointGeocoordinates">Coordinates of the point.</param>
        public async void CreateReverseGeocodeRequest(PointGeocoordinates pointGeocoordinates)
        {
            try
            {
                var request = _mapService.CreateReverseGeocodeRequest(pointGeocoordinates.Latitude, pointGeocoordinates.Longitude);
                var response = await request.GetResponseAsync();

                foreach (var result in response)
                {
                    ResponseReceived?.Invoke(this, new ReverseGeocodingResponseArgs(true, result.ToString()));
                }
            }
            catch (Exception)
            {
                ResponseReceived?.Invoke(this, new ReverseGeocodingResponseArgs(false));
            }
        }

        #endregion
    }
}
