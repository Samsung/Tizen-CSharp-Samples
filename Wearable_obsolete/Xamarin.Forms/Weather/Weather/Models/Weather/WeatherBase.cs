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

namespace Weather.Models.Weather
{
    /// <summary>
    /// Class containing basic weather information.
    /// </summary>
    public class WeatherBase
    {
        #region properties

        /// <summary>
        /// Weather condition id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Weather condition (e.g. Rain, Snow, Clear).
        /// </summary>
        [JsonProperty(PropertyName = "main")]
        public string Condition { get; set; }

        /// <summary>
        /// Weather condition within the group.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Weather icon id.
        /// </summary>
        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }

        #endregion
    }
}