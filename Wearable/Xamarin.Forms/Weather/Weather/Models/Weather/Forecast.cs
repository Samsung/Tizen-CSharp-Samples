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

namespace Weather.Models.Weather
{
    /// <summary>
    /// Forecast model.
    /// </summary>
    public class Forecast
    {
        /// <summary>
        /// List of weather data for next days.
        /// Obtains data from "OpenWeatherMap" API.
        /// </summary>
        [JsonProperty(PropertyName = "list")]
        public IList<CurrentWeather> WeatherList { get; set; }
    }
}