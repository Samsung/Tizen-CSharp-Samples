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
using ReverseGeocoding.Tizen.Wearable.Services;
using Tizen.Maps;

[assembly: Xamarin.Forms.Dependency(typeof(MapServiceProvider))]

namespace ReverseGeocoding.Tizen.Wearable.Services
{
    /// <summary>
    /// Manages map service.
    /// </summary>
    public class MapServiceProvider : IMapServiceProvider
    {
        #region fields

        /// <summary>
        /// MapService class instance.
        /// </summary>
        private readonly MapService _mapService;

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public MapServiceProvider()
        {
            _mapService = new MapService(Config.ProviderName, Config.AuthenticationToken);
        }

        /// <summary>
        /// Gets MapService class instance.
        /// </summary>
        /// <returns>MapService instance.</returns>
        public MapService GetService()
        {
            return _mapService;
        }

        #endregion
    }
}
