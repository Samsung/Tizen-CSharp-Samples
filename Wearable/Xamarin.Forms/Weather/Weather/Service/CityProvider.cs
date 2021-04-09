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
    /// Class that manages supported cities.
    /// </summary>
    public class CityProvider : ICityProvider
    {
        #region properties

        /// <summary>
        /// List of supported cities with their data.
        /// </summary>
        public IQueryable<City> CityList { get; }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor which allows to set supported cities.
        /// </summary>
        /// <param name="cityList">Supported city list.</param>
        public CityProvider(IQueryable<City> cityList)
        {
            CityList = cityList;
        }

        /// <summary>
        /// Indicates if given cityName is in supported city list.
        /// </summary>
        /// <param name="cityName">Name of the city to check.</param>
        /// <returns>True if city is found in the list, otherwise false is returned.</returns>
        public bool Validate(string cityName)
        {
            return CityList.Any(x => x.Name.Equals(cityName));
        }

        /// <summary>
        /// Finds first n cities that start with given text.
        /// </summary>
        /// <param name="text">Search condition.</param>
        /// <param name="n">Maximum number of cities that will be returned.</param>
        /// <returns>List of cities if found, otherwise null is returned.</returns>
        public IList<City> FindCity(string text, int n)
        {
            return CityList.Where(cityItem => cityItem.Name.ToLower().StartsWith(text.ToLower())).Take(n).ToList();
        }

        /// <summary>
        /// Finds first n cities that starts with given text.
        /// Finds cities only in country specified with country code in ISO-3166 format.
        /// </summary>
        /// <param name="text">Search condition.</param>
        /// <param name="countryCode">Country code in ISO-3166 format.</param>
        /// <param name="n">Maximum number of cities that will be returned.</param>
        /// <returns>List of cities if found otherwise returns null.</returns>
        public IList<City> FindCity(string text, string countryCode, int n)
        {
            return CityList
                .Where(cityItem => cityItem.Name.ToLower().StartsWith(text.ToLower()) && cityItem.CountryCode.Equals(countryCode)).Take(n)
                .ToList();
        }


        /// <summary>
        /// Gets first occurrence of city with given name.
        /// </summary>
        /// <param name="cityName">Name of the city.</param>
        /// <returns>City with given name otherwise returns null.</returns>
        public City GetCiy(string cityName) => CityList.FirstOrDefault(x => x.Name.Equals(cityName));

        #endregion
    }
}