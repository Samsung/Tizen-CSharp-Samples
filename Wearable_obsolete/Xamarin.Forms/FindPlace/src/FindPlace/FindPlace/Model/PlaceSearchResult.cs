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
namespace FindPlace.Model
{
    /// <summary>
    /// Stores information about result.
    /// </summary>
    public class PlaceSearchResult
    {
        #region properties

        /// <summary>
        /// Name of the result.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Address of the result.
        /// </summary>
        public string Address { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes Result class.
        /// </summary>
        /// <param name="name">Place name.</param>
        /// <param name="address">Place address.</param>
        public PlaceSearchResult(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(PlaceSearchResult)}: {Name}, {Address}";
        }

        #endregion
    }
}
