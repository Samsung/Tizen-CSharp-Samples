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
    /// Class containing weather data.
    /// </summary>
    public class WeatherData
    {
        #region properties

        /// <summary>
        /// Temperature in degrees Celsius.
        /// </summary>
        [JsonProperty(PropertyName = "temp")]
        public double Temperature { get; set; }

        /// <summary>
        /// Minimum temperature at the moment. Significant for large cities.
        /// </summary>
        [JsonProperty(PropertyName = "temp_min")]
        public double MinimumTemperature { get; set; }

        /// <summary>
        /// Maximum temperature at the moment. Significant for large cities.
        /// </summary>
        [JsonProperty(PropertyName = "temp_max")]
        public double MaximumTemperature { get; set; }

        /// <summary>
        /// Atmospheric pressure in hPa.
        /// </summary>
        [JsonProperty(PropertyName = "pressure")]
        public double Pressure { get; set; }

        #endregion
    }
}