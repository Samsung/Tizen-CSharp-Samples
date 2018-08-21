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

using Newtonsoft.Json;

namespace Weather.Models.Location
{
    /// <summary>
    /// Coordinates of a city.
    /// </summary>
    public class Coordinates
    {
        #region properties

        /// <summary>
        /// North-South position of a city on the Earth's surface.
        /// </summary>
        [JsonProperty(PropertyName = "lon")]
        public double Longitude { get; set; }

        /// <summary>
        /// East-west position of a city on the Earth's surface.
        /// </summary>
        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }

        #endregion
    }
}