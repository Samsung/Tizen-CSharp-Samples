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

namespace Weather.Config
{
    /// <summary>
    /// Foreign API configuration.
    /// </summary>
    public static class ApiConfig
    {
        #region fields

        /// <summary>
        /// Open Weather Map API key.
        /// </summary>
        /// <remarks>
        /// To get your API key please visit http://openweathermap.org/appid
        /// </remarks>
        public const string API_KEY = "";

        /// <summary>
        /// "OpenWeatherMap" current weather resource.
        /// </summary>
        public const string WEATHER_URL = "https://api.openweathermap.org/data/2.5/weather";

        /// <summary>
        /// "OpenWeatherMap" forecast resource.
        /// </summary>
        public const string FORECAST_URL = "https://api.openweathermap.org/data/2.5/forecast";

        /// <summary>
        /// "OpenWeatherMap" icon location template.
        /// </summary>
        public const string WEATHER_ICON = "http://openweathermap.org/img/w/{0}.png";

        /// <summary>
        /// Google API URL for getting timezones information.
        /// </summary>
        public const string TIMEZONE_URL = "https://maps.googleapis.com/maps/api/timezone/json";

        #endregion

        #region methods

        /// <summary>
        /// Checks if API key is defined.
        /// </summary>
        /// <returns>True if API key is set, otherwise false.</returns>
        public static bool IsApiKeyDefined()
        {    
            return !string.IsNullOrEmpty(API_KEY);
        }

        #endregion
    }
}
