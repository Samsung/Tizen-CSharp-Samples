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

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tizen.Location;
using WeatherWatch.PageModels;

namespace WeatherWatch.Services
{
    /// <summary>
    /// ForecastService class
    /// It provides information about weather and AQI(Air pollution Quality Index) based on the current location
    /// </summary>
    public class ForecastService
    {
        private WeatherWatchPageModel _viewModel;
        private string[] levels = { "red", "orange", "yellow", "green", "blue" };
        private int AQI;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_vm">WeatherWatchPageModel</param>
        public ForecastService(WeatherWatchPageModel _vm)
        {
            _viewModel = _vm;
        }

        /// <summary>
        /// Get data by sending a GET request to the specified Uri
        /// </summary>
        /// <param name="url">Uri string</param>
        /// <returns>dynamic</returns>
        private async Task<dynamic> GetDataFromWeb(string url)
        {
            
            HttpClient client = new HttpClient();
            dynamic data = null;
            try
            {
                // Send a Get request to get data
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string dataStr = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject(dataStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n  GetDataFromWeb() Exception Caught!");
                Console.WriteLine("Message: {0}, StackTrace: {1}, InnerException: {2}", ex.Message, ex.StackTrace, ex.InnerException.Message);
            }

            client.Dispose();
            //Console.WriteLine("[GetDataFromWeb]  data : " + data);
            return data;
        }

        /// <summary>
        /// Update AQI related data based on the current location information
        /// By requesting WAQI public api
        /// </summary>
        /// <param name="position">Location</param>
        public async void UpdateAQI(Location position)
        {
            try
            {
                string url = WebSiteInfo.AIR_POLLUTION_URL + position.Latitude + ";" + position.Longitude + "/?token=" + WebSiteInfo.AIR_POLLUTION_API_KEY;
                Console.WriteLine("[UpdateAQI] url : " + url);
                dynamic result = await GetDataFromWeb(url);
                if (result != null)
                {
                    if (result["data"] != null)
                    {
                        AQI = (int)result["data"]["aqi"];
                        //Console.WriteLine("Air Pollution Quality Index : " + AQI);
                    }
                }
                else
                {
                    // In case that getting information about AQI is failed.
                    AQI = WebSiteInfo.AQI_INFO_NOT_AVAILABLE;
                    //Console.WriteLine("Air Pollution Quality Index : Not available...");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" UpdateAQI() Exception : " + e.Message + ", " + e.StackTrace + ", " + e.InnerException);
                AQI = WebSiteInfo.AQI_INFO_NOT_AVAILABLE;
            }

            UpdateAirPolution();
        }

        /// <summary>
        /// Update Weather related data by requesting openweathermap API
        /// </summary>
        /// <param name="location">Location</param>
        public async void UpdateWeather(Location location)
        {
            try
            {
                string url = string.Format(WebSiteInfo.WEATHER_URL, location.Latitude, location.Longitude, WebSiteInfo.WEATHER_API_KEY);
                dynamic result = await GetDataFromWeb(url);
                if (result != null)
                {
                    if (result["weather"] != null)
                    {
                        _viewModel.WeatherText = (string)result["weather"][0]["main"];
                        _viewModel.WeatherIconPath = string.Format(WebSiteInfo.WEATHER_ICON, (string)result["weather"][0]["icon"]);
                        Console.WriteLine("Weather : " + _viewModel.WeatherText + ", Icon : " + _viewModel.WeatherIconPath);
                        _viewModel.WeatherInfoIsVisible = true;
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(" UpdateWeather() Exception : " + e.Message + ", " + e.StackTrace + ", " + e.InnerException);
            }

            // In case that weather data is not available.
            _viewModel.WeatherInfoIsVisible = false;
            // In case that getting information about Weather is failed.
            _viewModel.WeatherText = WebSiteInfo.WEATEHR_INFO_NOT_AVAILABLE;
            _viewModel.WeatherIconPath = WebSiteInfo.WEATEHR_INFO_NOT_AVAILABLE;
            Console.WriteLine("Weather information : Not available...");
        }

        /// <summary>
        /// Update Air pollution Quality Index related data
        /// </summary>
        void UpdateAirPolution()
        {
            //Console.WriteLine("--------UpdateAirPolution  apqi = " + AQI);
            int index = 0;
            if (AQI == WebSiteInfo.AQI_INFO_NOT_AVAILABLE)
            {
                _viewModel.UpdateWithoutInformation();
                return;
            }

            if (AQI < 50)
            {
                index = 4;
            }
            else if (AQI < 150)
            {
                index = 3;
            }
            else if (AQI < 200)
            {
                index = 2;
            }
            else if (AQI < 300)
            {
                index = 1;
            }
            else
            {
                index = 0;
            }

            _viewModel.AqiIconPath = "color_status/air_pollution_icon_" + levels[index] + ".png";
            _viewModel.AqiIndicatorPath = "color_status/" + levels[index] + "_indicator.png";
            _viewModel.AqiText = AQI + "AQI";
            //Console.WriteLine("AQI Icon Path : " + _viewModel.AqiIconPath);
            //Console.WriteLine("AQI IndicatorIconPath : " + _viewModel.AqiIndicatorPath);
        }
    }
}