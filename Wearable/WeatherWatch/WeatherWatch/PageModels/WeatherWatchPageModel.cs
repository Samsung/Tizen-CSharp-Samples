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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Tizen.Location;
using WeatherWatch.Services;
using Xamarin.Forms;

namespace WeatherWatch.PageModels
{
    /// <summary>
    /// This is WeatherWatchPageModel class that
    /// extends BasePageModel class
    /// </summary>
    public class WeatherWatchPageModel : BasePageModel
    {
        private DateTime _time;
        private double _hours, _minutes, _seconds;
        private string _day, _month;

        // Services
        private NetworkService _network;
        private LocationService _location;
        private ForecastService _forecast;
        private BatteryInfoService _battery;

        // AQI
        string _aqiIconPath, _aqiIndicatorPath, _aqiText;
        bool _aqiTextIsVisible;

        // Battery
        string _batteryIconPath, _batteryIndicatorPath, _batteryPercent;
        bool _batteryTextIsVisible;

        // Weather
        string _weatherIconPath, _WeatherText;
        ImageSource _WeatherIcon;
        bool _weatherInfoIsVisible;

        // Time & Date
        public DateTime Time
        {
            get => _time;
            set
            {
                if (_time == value)
                {
                    return;
                }

                Hours = 30 * (_time.Hour % 12) + 0.5f * _time.Minute;
                Minutes = 6 * _time.Minute + 0.1f * _time.Second;
                Seconds = 6 * _time.Second + (0.006f * _time.Millisecond);
                Month = _time.ToString("MMMM", CultureInfo.InvariantCulture).ToUpper();
                Day = _time.ToString("dd");
                SetProperty(ref _time, value, "Time");
            }
        }

        // hour
        public double Hours
        {
            get
            {
                return _hours;
            }

            private set
            {
                SetProperty(ref _hours, value, "Hours");
            }
        }

        // minute
        public double Minutes
        {
            get
            {
                return _minutes;
            }

            private set
            {
                SetProperty(ref _minutes, value, "Minutes");
            }
        }

        // second
        public double Seconds
        {
            get
            {
                return _seconds;
            }

            private set
            {
                SetProperty(ref _seconds, value, "Seconds");
            }
        }

        // month
        public string Month
        {
            get
            {
                return _month;
            }

            private set
            {
                SetProperty(ref _month, value, "Month");
            }
        }

        // day
        public string Day
        {
            get
            {
                return _day;
            }

            private set
            {
                SetProperty(ref _day, value, "Day");
            }
        }

        // Battery Percentage
        public string BatteryPercent
        {
            get
            {
                return _batteryPercent;
            }

            set
            {
                SetProperty(ref _batteryPercent, value, "BatteryPercent");
            }
        }

        // the path of battery icon
        public string BatteryIconPath
        {
            get
            {
                return _batteryIconPath;
            }

            set
            {
                SetProperty(ref _batteryIconPath, value, "BatteryIconPath");
            }
        }

        // the path of battery indicator
        public string BatteryIndicatorPath
        {
            get
            {
                return _batteryIndicatorPath;
            }

            set
            {
                SetProperty(ref _batteryIndicatorPath, value, "BatteryIndicatorPath");
            }
        }

        // text for battery percentage
        public bool BatteryTextIsVisible
        {
            get => _batteryTextIsVisible;
            set
            {
                SetProperty(ref _batteryTextIsVisible, value, "BatteryTextIsVisible");
            }
        }

        // the path of weather icon
        public string WeatherIconPath
        {
            get => _weatherIconPath;
            set
            {
                bool changed = SetProperty(ref _weatherIconPath, value, "WeatherIconPath");
                if (changed && WeatherIconPath != WebSiteInfo.WEATEHR_INFO_NOT_AVAILABLE)
                {
                    WeatherIcon = ImageSource.FromUri(new Uri(WeatherIconPath));
                    //Console.WriteLine("[Update] WeatherIcon : " + WeatherIcon);
                }
            }
        }

        // the path of weather data
        public ImageSource WeatherIcon
        {
            get => _WeatherIcon;
            set
            {
                SetProperty(ref _WeatherIcon, value, "WeatherIcon");
            }
        }

        // the title of weather data (Rain, Sunny, etc)
        public string WeatherText
        {
            get => _WeatherText;
            set
            {
                SetProperty(ref _WeatherText, value, "WeatherText");
            }
        }

        // weather information is visible or not
        public bool WeatherInfoIsVisible
        {
            get => _weatherInfoIsVisible;
            set
            {
                SetProperty(ref _weatherInfoIsVisible, value, "WeatherInfoIsVisible");
            }
        }

        // the path of Air pollution Quality Index(AQI) icon
        public string AqiIconPath
        {
            get => _aqiIconPath;
            set
            {
                SetProperty(ref _aqiIconPath, value, "AqiIconPath");
            }
        }

        // the path of Air pollution Quality Index(AQI) indicator image
        public string AqiIndicatorPath
        {
            get => _aqiIndicatorPath;
            set
            {
                SetProperty(ref _aqiIndicatorPath, value, "AqiIndicatorPath");
            }
        }

        // the value of Air pollution Quality Index(AQI)
        public string AqiText
        {
            get => _aqiText;
            set
            {
                SetProperty(ref _aqiText, value, "AqiText");
            }
        }

        // AQI text is visible or not
        public bool AqiTextIsVisible
        {
            get => _aqiTextIsVisible;
            set
            {
                SetProperty(ref _aqiTextIsVisible, value, "AqiTextIsVisible");
            }
        }

        // Indicate if an error occurs or not
        bool _errorViewIsVisible;
        public bool ErrorViewIsVisible
        {
            get => _errorViewIsVisible;
            set
            {
                SetProperty(ref _errorViewIsVisible, value, "ErrorViewIsVisible");
            }
        }

        public ICommand DismissErrorViewCommand => new Command(c =>
        {
            ErrorViewIsVisible = false;
        });

        /// <summary>
        /// Constructor
        /// </summary>
        public WeatherWatchPageModel()
        {
            UpdateWithoutInformation();

            // NEED TO REMOVE THE BELOW BLOCK AFTER WebSiteInfo.AIR_POLLUTION_API_KEY & WebSiteInfo.WEATHER_API_KEY ARE UPDATED.
            // {
            if (WebSiteInfo.AIR_POLLUTION_API_KEY == WebSiteInfo.INVALID_API_KEY || WebSiteInfo.WEATHER_API_KEY == WebSiteInfo.INVALID_API_KEY)
            {
                ErrorViewIsVisible = true;
            }
            // }

            _network = new NetworkService(this);
            _battery = new BatteryInfoService(this);
            _location = new LocationService(this);
            _forecast = new ForecastService(this);
            if (Tizen.System.Display.State == Tizen.System.DisplayState.Normal)
            {
                RegisterEvents();
            }
        }

        /// <summary>
        /// For listening events
        /// </summary>
        public void RegisterEvents()
        {
            //_network.RegisterEvent();
            _battery.RegisterBatteryEvent();
        }

        /// <summary>
        /// Stop listening events
        /// </summary>
        public void UnregisterEvents()
        {
            //_network.UnregisterEvent();
            _battery.UnregisterBatteryEvent();
        }

        /// <summary>
        /// Update UI without information
        /// Information about current location is not available because GPS or network is not provided.
        /// </summary>
        public void UpdateWithoutInformation()
        {
            AqiIconPath = "no_gps_icon.png";
            AqiIndicatorPath = "status_indicator.png";
            AqiText = "";
            WeatherInfoIsVisible = false;
        }

        /// <summary>
        /// Update UI with information
        /// The current location information is available.
        /// Weather and AQI information based on the current location can be obtained.
        /// </summary>
        /// <param name="position">Location</param>
        public void UpdateInformation(Location position)
        {
            if (!_network.IsConnected || position == null || WebSiteInfo.AIR_POLLUTION_API_KEY == WebSiteInfo.INVALID_API_KEY || WebSiteInfo.WEATHER_API_KEY == WebSiteInfo.INVALID_API_KEY)
            {
                // case 1: information is not availble
                //
                // Reason
                //
                // Network is not available
                // Position information is not available
                // API key is invalid
                //Console.WriteLine("WeatherWatchPageModel.UpdateInformation() " + position + " , Network connection : " + _network.IsConnected);
                UpdateWithoutInformation();
            }
            else
            {
                // Case 2: information is available
                //Console.WriteLine("WeatherWatchPageModel.UpdateInformation() " + position.Latitude + ", " + position.Longitude + " , Network connection : " + _network.IsConnected);
                _forecast.UpdateAQI(position);
                _forecast.UpdateWeather(position);
            }
        }

        async public void UpdateInformation()
        {
            Console.WriteLine("[UpdateInformation()] LocationEnabled : " + _location.LocationEnabled);
            Location position = await _location.GetCurrentPosition();
            if (position == null)
            {
                UpdateWithoutInformation();
                return;
            }

            Console.WriteLine("[UpdateInformation()] POSITION : [" + position.Latitude + ", " + position.Longitude + "]");
            UpdateInformation(position);
        }
    }
}
