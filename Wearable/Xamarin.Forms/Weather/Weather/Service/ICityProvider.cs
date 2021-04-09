//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System.Collections.Generic;
using System.Linq;
using Weather.Models.Location;

namespace Weather.Service
{
    /// <summary>
    /// Interface that contains all necessary tools to manage supported cities.
    /// </summary>
    public interface ICityProvider
    {
        #region properties

        /// <summary>
        /// List of supported cities with their data.
        /// </summary>
        IQueryable<City> CityList { get; }

        #endregion

        #region methods

        /// <summary>
        /// Indicates if given cityName is in supported city list.
        /// </summary>
        /// <param name="cityName">Name of the city to check.</param>
        /// <returns>True if city is found in the list otherwise returns false.</returns>
        bool Validate(string cityName);

        /// <summary>
        /// Finds first n cities that starts with given text.
        /// </summary>
        /// <param name="text">Search condition.</param>
        /// <param name="n">Maximum number of cities that will be returned.</param>
        /// <returns>List of cities if found otherwise returns null.</returns>
        IList<City> FindCity(string text, int n);

        /// <summary>
        /// Finds first n cities that starts with given text.
        /// Finds cities only in country specified with country code in ISO-3166 format.
        /// </summary>
        /// <param name="text">Search condition.</param>
        /// <param name="countryCode">Country code in ISO-3166 format.</param>
        /// <param name="n">Maximum number of cities that will be returned.</param>
        /// <returns>List of cities if found otherwise returns null.</returns>
        IList<City> FindCity(string text, string countryCode, int n);

        /// <summary>
        /// Gets first occurrence of city with given name.
        /// </summary>
        /// <param name="cityName">Name of the city.</param>
        /// <returns>City with given name otherwise returns null.</returns>
        City GetCiy(string cityName);

        #endregion
    }
}