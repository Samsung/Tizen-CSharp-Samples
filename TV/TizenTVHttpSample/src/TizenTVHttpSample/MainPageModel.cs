/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Tizen.Network.Connection;
using Xamarin.Forms;

namespace TizenTVHttpSample
{
    class MainPageModel : INotifyPropertyChanged
    {
        const string LogTag = "TizenTVHttpSample";
        public const string WEATHER_URL = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}&units=imperial";
        public const string WEATHER_API_KEY = "PUT YOUR API KEY HERE";
        public const string WEATHER_ICON = "http://openweathermap.org/img/w/{0}.png";

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPageModel()
        {
            WeatherInfo = "?";
            InfoText = "Let's get the current weather info!\n";
            //string BuildDate, BuildInfo;
            //Tizen.System.Information.TryGetValue<string>("http://tizen.org/system/build.date", out BuildDate);
            //Tizen.System.Information.TryGetValue<string>("http://tizen.org/system/build.string", out BuildInfo);
            //InfoText = "Let's get the current weather info!\nBuildInfo : " + BuildInfo +",  date: " + BuildDate;
        }

        /// <summary>
        /// Weather Info text
        /// </summary>
        string _weatherinfo;
        public string WeatherInfo
        {
            get => _weatherinfo;
            set => SetProperty(ref _weatherinfo, value);
        }

        /// <summary>
        /// Information text
        /// </summary>
        string _infoText;
        public string InfoText
        {
            get => _infoText;
            set => SetProperty(ref _infoText, value);
        }

        /// <summary>
        /// Result text
        /// </summary>
        string _iconpath;
        public string WeatherIconPath
        {
            get => _iconpath;
            set
            {
                bool changed = SetProperty(ref _iconpath, value);
                if (changed && WeatherIconPath != "")
                {
                    WeatherIcon = ImageSource.FromUri(new Uri(WeatherIconPath));
                    InfoText = "[Update] WeatherIcon : " + WeatherIcon + "\n" + InfoText;
                }
            }
                
        }

        ImageSource _WeatherIcon;
        public ImageSource WeatherIcon
        {
            get => _WeatherIcon;
            set
            {
                SetProperty(ref _WeatherIcon, value, "WeatherIcon");
            }
        }

        public ICommand GetWeatherCommand => new Command(GetWeather);

        async void GetWeather()
        {
            try
            {
                HttpClient _httpWeatherClient = new HttpClient();
                // Samsung Seoul R&D Campus : 37.4666° N, 127.0228° E
                string url = string.Format(WEATHER_URL, 37.4666, 127.0228, WEATHER_API_KEY);
                dynamic result = await GetDataFromWeb(url);

                if (result != null)
                {
                    if (result["weather"] != null)
                    {
                        WeatherInfo = (string)result["weather"][0]["main"];
                        WeatherIcon = string.Format(WEATHER_ICON, (string)result["weather"][0]["icon"]);
                        InfoText = "Weather : " + WeatherInfo + ", Icon : " + WeatherIcon + "\n" + InfoText;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                InfoText = " UpdateWeather() Exception : " + ex.Message + ", " + ex.StackTrace + ", " + ex.InnerException + "\n" + InfoText;
            }
        }

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
                InfoText = " GetDataFromWeb() Exception : " + ex.Message + ", " + ex.StackTrace + ", " + ex.InnerException + "\n" + InfoText;
            }

            client.Dispose();
            InfoText = "[GetDataFromWeb]  data : " + data + "\n" + InfoText;
            return data;
        }

        /// <summary>
        /// Command to get web data
        /// </summary>
        public ICommand GetDataCommand => new Command(GetData);

        void GetData()
        {
            try
            {
                ConnectionItem currentConnection = ConnectionManager.CurrentConnection;
                InfoText = "Connection(" + currentConnection.Type + ", " + currentConnection.State + ")\n";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://samsung.github.io/Tizen.NET/");
                // When watch is paired with a mobile device, we can use WebProxy.
                if (currentConnection.Type == ConnectionType.Disconnected)
                {
                    InfoText = InfoText + "There's no available data connectivity!!";
                    return;
                }

                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                InfoText = InfoText + "\nHttpWebResponse : status - " + ((HttpWebResponse)response).StatusDescription + "\n";
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content and print log.
                InfoText += responseFromServer;
                // Clean up the streams and the response.
                reader.Close();
                response.Close();
            }
            catch (Exception e)
            {
                InfoText += "An error occurs : " + e.GetType() + " , " + e.Message;
            }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Called to notify that a change of property happened
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
