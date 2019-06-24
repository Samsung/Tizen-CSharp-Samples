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
using FindPlace.Model;
using FindPlace.Utils;
using System.Threading.Tasks;

namespace FindPlace.Interfaces
{
    /// <summary>
    /// Provides methods to use the maps and their services.
    /// </summary>
    public interface IFindPlaceService
    {
        #region methods

        /// <summary>
        /// Searches for places of specified type in given area.
        /// </summary>
        /// <param name="area">Area to look for places.</param>
        /// <param name="placeType">Type of place.</param>
        /// <returns>Response from find place service.</returns>
        Task<FindPlaceResponse> GetPlacesAsync(Area area, PlaceType placeType);

        /// <summary>
        /// Gets user's consent to the map provider's license terms.
        /// </summary>
        /// <returns>Information about user consent.</returns>
        Task<bool> GetUserConsentAsync();

        #endregion
    }
}
