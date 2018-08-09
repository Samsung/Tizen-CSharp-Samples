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
using Newtonsoft.Json;
using Weather.Models.Location;

namespace Weather.Models.Weather
{
    /// <summary>
    /// Current weather model.
    /// </summary>
    public class CurrentWeather
    {
        #region properties

        /// <summary>
        /// List of base weather properties, e.g. "Clear sky" etc.
        /// </summary>
        [JsonProperty(PropertyName = "weather")]
        public IList<WeatherBase> Weather { get; set; }

        /// <summary>
        /// Weather data, e.g. temperature, pressure, etc.
        /// </summary>
        [JsonProperty(PropertyName = "main")]
        public WeatherData WeatherData { get; set; }

        /// <summary>
        /// Wind properties, e.g. speed.
        /// </summary>
        [JsonProperty(PropertyName = "wind")]
        public Wind Wind { get; set; }

        /// <summary>
        /// Cloudiness data.
        /// </summary>
        [JsonProperty(PropertyName = "clouds")]
        public Clouds Clouds { get; set; }

        /// <summary>
        /// Time stamp of the weather measurement.
        /// </summary>
        [JsonProperty(PropertyName = "dt")]
        public ulong TimeStamp { get; set; }

        /// <summary>
        /// Sunrise and sunset times.
        /// </summary>
        [JsonProperty(PropertyName = "sys")]
        public SunData SunData { get; set; }

        /// <summary>
        /// City name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string CityName { get; set; }

        #endregion
    }
}