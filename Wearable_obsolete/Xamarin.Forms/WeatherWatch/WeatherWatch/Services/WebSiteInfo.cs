/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherWatch.Services
{
    /// <summary>
    /// WebSiteInfo class
    /// </summary>
    public static class WebSiteInfo
    {
        public const string INVALID_API_KEY = "PUT YOUR API KEY HERE";
        public const string WEATHER_API_KEY = INVALID_API_KEY;
        public const string AIR_POLLUTION_API_KEY = INVALID_API_KEY;
        // openweathermap public api to provide weather data
        public const string WEATHER_URL = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}&units=imperial";
        public const string WEATHER_ICON = "http://openweathermap.org/img/w/{0}.png";
        // waqi public api to provide AQI data
        public const string AIR_POLLUTION_URL = "http://api.waqi.info/feed/geo:";
        public const int AQI_INFO_NOT_AVAILABLE = -100;
        public const string WEATEHR_INFO_NOT_AVAILABLE = "";
    }
}
