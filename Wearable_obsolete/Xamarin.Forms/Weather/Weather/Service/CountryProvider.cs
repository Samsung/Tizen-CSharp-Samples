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

using System.Linq;
using System.Text.RegularExpressions;
using Weather.Models.Location;

namespace Weather.Service
{
    /// <summary>
    /// Class that manages supported countries.
    /// </summary>
    public class CountryProvider : ICountryProvider
    {
        #region fields

        /// <summary>
        /// Country code validation rule.
        /// </summary>
        private const string COUNTRY_REGEX = "^[A-Z]{2}$";

        #endregion

        #region properties

        /// <summary>
        /// Supported country list.
        /// </summary>
        public IQueryable<Country> CountryList { get; }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor which allows to set supported country list.
        /// </summary>
        /// <param name="countryList">Supported country list.</param>
        public CountryProvider(IQueryable<Country> countryList)
        {
            CountryList = countryList;
        }

        /// <summary>
        /// Checks if given countryCode is in accordance with ISO 3166 standard.
        /// </summary>
        /// <param name="countryCode">Country Code.</param>
        /// <returns>True if code is valid, otherwise false is returned.</returns>
        public bool Validate(string countryCode)
        {
            return Regex.IsMatch(countryCode, COUNTRY_REGEX) && CountryList.Any(x => x.Code.Equals(countryCode));
        }

        #endregion
    }
}