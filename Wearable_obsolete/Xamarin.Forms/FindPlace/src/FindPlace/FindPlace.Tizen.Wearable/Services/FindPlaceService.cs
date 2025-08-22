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
using FindPlace.Enums;
using FindPlace.Interfaces;
using FindPlace.Model;
using FindPlace.Tizen.Wearable.Services;
using FindPlace.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tizen.Maps;
using Xamarin.Forms;
using Area = Tizen.Maps.Area;
using Geocoordinates = Tizen.Maps.Geocoordinates;

[assembly: Dependency(typeof(FindPlaceService))]

namespace FindPlace.Tizen.Wearable.Services
{
    /// <summary>
    /// Provides methods to use maps and their services.
    /// </summary>
    public class FindPlaceService : IFindPlaceService
    {
        #region fields

        /// <summary>
        /// Not found error message.
        /// </summary>
        private const string NotFoundError = "NotFound";

        /// <summary>
        /// MapService class instance used for getting the map service data.
        /// </summary>
        private MapService _mapService;

        /// <summary>
        /// Logger service.
        /// </summary>
        private readonly ILoggerService _loggerService;

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public FindPlaceService()
        {
            _mapService = DependencyService.Get<IMapServiceProvider>().GetService();
            _loggerService = DependencyService.Get<ILoggerService>();
        }

        /// <summary>
        /// Searches for places of specified type in given area.
        /// </summary>
        /// <param name="area">Area to look for places.</param>
        /// <param name="placeType">Type of place.</param>
        /// <returns>Response from find place service.</returns>
        public async Task<FindPlaceResponse> GetPlacesAsync(Model.Area area, PlaceType placeType)
        {
            try
            {
                var placeCategory = new PlaceCategory();
                placeCategory.Id = placeType.GetPlaceCategoryId();

                using (_mapService.PlaceSearchFilter = new PlaceFilter())
                {
                    _mapService.PlaceSearchFilter.Category = placeCategory;

                    Geocoordinates geoTopLeft = new Geocoordinates(
                        area.TopLeftPoint.Latitude, area.TopLeftPoint.Longitude);
                    Geocoordinates geoBottomRight = new Geocoordinates(
                        area.BottomRightPoint.Latitude, area.BottomRightPoint.Longitude);

                    var request = _mapService.CreatePlaceSearchRequest(new Area(geoTopLeft, geoBottomRight));
                    _loggerService.Verbose($"{request}");

                    var response = await request.GetResponseAsync();
                    _loggerService.Verbose($"{response}");

                    var results = response
                        .Select(r => new PlaceSearchResult(r.Name, AddressToString(r.Address)))
                        .ToList();
                    results.ForEach(result => _loggerService.Verbose($"{result}"));

                    return new FindPlaceResponse(true, results);
                }
            }
            catch (Exception e)
            {
                bool notFound = e.Message.Contains(NotFoundError);

                return new FindPlaceResponse(notFound);
            }
        }

        /// <summary>
        /// Gets user's consent to the map provider's license terms.
        /// </summary>
        /// <returns>Information about user consent.</returns>
        public async Task<bool> GetUserConsentAsync()
        {
            return await _mapService.RequestUserConsent();
        }

        /// <summary>
        /// Converts place address to a short string which can be displayed in results list.
        /// </summary>
        /// <param name="placeAddress">Place address.</param>
        /// <returns>String with short address.</returns>
        private string AddressToString(PlaceAddress placeAddress)
        {
            return $"{placeAddress.Street} {placeAddress.Building}\n{placeAddress.PostalCode} {placeAddress.City}";
        }

        #endregion
    }
}
