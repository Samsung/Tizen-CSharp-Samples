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
using Weather.Models.Location;

namespace Weather.Service
{
    /// <summary>
    /// Interface that contains all necessary tools to manage supported countries.
    /// </summary>
    public interface ICountryProvider
    {
        #region properties

        /// <summary>
        /// List of codes of all supported countries.
        /// </summary>
        IQueryable<Country> CountryList { get; }

        #endregion

        #region methods

        /// <summary>
        /// Checks if given country code is valid.
        /// </summary>
        /// <param name="countryCode">Country code.</param>
        /// <returns>Returns true if code is valid, otherwise returns false.</returns>
        bool Validate(string countryCode);

        #endregion
    }
}