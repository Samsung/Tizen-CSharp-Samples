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
    /// Class containing additional weather data.
    /// </summary>
    public class SunData
    {
        #region properties

        /// <summary>
        /// Time stamp of sunrise.
        /// </summary>
        [JsonProperty(PropertyName = "sunrise")]
        public ulong SunriseTimeStamp { get; set; }

        /// <summary>
        /// Time stamp of sunset.
        /// </summary>
        [JsonProperty(PropertyName = "sunset")]
        public ulong SunsetTimeStamp { get; set; }

        #endregion
    }
}