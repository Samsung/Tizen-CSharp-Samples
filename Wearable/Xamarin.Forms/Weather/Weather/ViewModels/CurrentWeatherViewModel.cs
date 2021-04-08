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

using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;
using Weather.Config;
using Weather.Models.Location;
using Weather.Models.Weather;
using Weather.Utils;
using Xamarin.Forms;

namespace Weather.ViewModels
{
    /// <summary>
    /// ViewModel class for CurrentWeatherPage.
    /// </summary>
    public class CurrentWeatherViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Local storage of command that initializes weather data.
        /// </summary>
        private Command _initializeCommand;

        /// <summary>
        /// Local storage of task that obtains current weather.
        /// </summary>
        private NotificationTask<CurrentWeather> _currentWeather;

        /// <summary>
        /// Local storage of city time zone.
        /// </summary>
        private NotificationTask<Models.Location.TimeZone> _cityTimeZone;

        /// <summary>
        /// Local storage of command that shows screen with forecast.
        /// </summary>
        private Command _checkForecastCommand;

        /// <summary>
        /// Local storage of forecast data.
        /// </summary>
        private NotificationTask<Forecast> _forecast;

        #endregion

        #region properties

        /// <summary>
        /// Bindable property that allows to set city data.
        /// </summary>
        public static readonly BindableProperty CityDataProperty =
            BindableProperty.Create(nameof(CityData), typeof(City), typeof(CurrentWeatherViewModel), default(City));

        /// <summary>
        /// Bindable property that allows to set navigation context.
        /// </summary>
        public static readonly BindableProperty NavigationProperty =
            BindableProperty.Create(nameof(Navigation), typeof(INavigation), typeof(MainPageViewModel), default(Type));

        /// <summary>
        /// Gets or sets city data.
        /// View model holds weather data for this city.
        /// </summary>
        public City CityData
        {
            get => (City)GetValue(CityDataProperty);
            set => SetValue(CityDataProperty, value);
        }

        /// <summary>
        /// Gets or sets task that obtains current weather.
        /// </summary>
        public NotificationTask<CurrentWeather> CurrentWeather
        {
            get => _currentWeather;
            set => SetProperty(ref _currentWeather, value);
        }

        /// <summary>
        /// Gets or sets city time zone.
        /// </summary>
        public NotificationTask<Models.Location.TimeZone> CityTimeZone
        {
            get => _cityTimeZone;
            set => SetProperty(ref _cityTimeZone, value);
        }

        /// <summary>
        /// Gets or sets command that initializes weather data.
        /// </summary>
        public Command InitializeCommand
        {
            get => _initializeCommand;
            set => SetProperty(ref _initializeCommand, value);
        }

        /// <summary>
        /// Gets or sets command that shows screen with forecast.
        /// </summary>
        public Command CheckForecastCommand
        {
            get => _checkForecastCommand;
            set => SetProperty(ref _checkForecastCommand, value);
        }

        /// <summary>
        /// Gets or sets navigation context of application.
        /// </summary>
        public INavigation Navigation
        {
            get => (INavigation)GetValue(NavigationProperty);
            set => SetValue(NavigationProperty, value);
        }

        /// <summary>
        /// Gets or sets forecast data.
        /// </summary>
        public NotificationTask<Forecast> Forecast
        {
            get => _forecast;
            set => SetProperty(ref _forecast, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public CurrentWeatherViewModel()
        {
            InitializeCommand = new Command(() =>
            {
                CityTimeZone = new NotificationTask<Models.Location.TimeZone>(InitializeTimeZone());
                CurrentWeather = new NotificationTask<CurrentWeather>(InitializeWeather());
                Forecast = new NotificationTask<Forecast>(InitializeForecast());

                CurrentWeather.PropertyChanged += CurrentWeatherOnPropertyChanged;
                Forecast.PropertyChanged += ForecastOnPropertyChanged;
            });

            CheckForecastCommand = new Command(CheckForecast);
        }

        /// <summary>
        /// Pushes page with forecast data to navigation stack.
        /// </summary>
        /// <param name="param">Page to push to navigation stack.</param>
        private async void CheckForecast(object param)
        {
            if (param is Page page)
            {
                await Navigation.PushAsync(page);
            }
        }

        /// <summary>
        /// Initializes current weather class.
        /// Sends GET request to server.
        /// </summary>
        /// <returns>Async task with current weather.</returns>
        private async Task<CurrentWeather> InitializeWeather()
        {
            var request = new Request<CurrentWeather>(ApiConfig.WEATHER_URL);

            request.AddParameter("appid", ApiConfig.API_KEY);
            request.AddParameter("id", CityData.Id.ToString());
            request.AddParameter("units", RegionInfo.CurrentRegion.IsMetric ? "metric" : "imperial");

            return await request.Get();
        }

        /// <summary>
        /// Initializes forecast class.
        /// Sends GET request to server.
        /// </summary>
        /// <returns>Async task with forecast data.</returns>
        private async Task<Forecast> InitializeForecast()
        {
            var request = new Request<Forecast>(ApiConfig.FORECAST_URL);

            request.AddParameter("appid", ApiConfig.API_KEY);
            request.AddParameter("id", CityData.Id.ToString());
            request.AddParameter("units", RegionInfo.CurrentRegion.IsMetric ? "metric" : "imperial");

            return await request.Get();
        }

        /// <summary>
        /// Initializes time zone for city.
        /// </summary>
        /// <returns>Async task with time zone.</returns>
        private async Task<Models.Location.TimeZone> InitializeTimeZone()
        {
            var request = new Request<Models.Location.TimeZone>(ApiConfig.TIMEZONE_URL);

            request.AddParameter("location", $"{CityData.Coordinates.Latitude},{CityData.Coordinates.Longitude}");
            request.AddParameter("timestamp",
                DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString());
            request.AddParameter("sensor", "false");

            return await request.Get();
        }

        /// <summary>
        /// Callback method that is invoked on Forecast property change.
        /// </summary>
        /// <param name="sender">Object that sent event.</param>
        /// <param name="propertyChangedEventArgs">Arguments of the event.</param>
        private async void ForecastOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var propertyName = propertyChangedEventArgs.PropertyName;
            if (propertyName == nameof(NotificationTask<Forecast>.IsFaulted))
            {
                if (Forecast.InnerException is HttpException exception)
                {
                    await ErrorHandler.HandleException((int)exception.StatusCode, exception.StatusCode.ToString());
                }
            }

            if (propertyName == nameof(NotificationTask<Forecast>.IsSuccessfullyCompleted))
            {
                foreach (var currentWeather in Forecast.Result.WeatherList)
                {
                    if (currentWeather != null)
                    {
                        currentWeather.CityName = CityData.Name;
                    }
                }
            }
        }

        /// <summary>
        /// Callback method that is invoked on Current Weather property change.
        /// </summary>
        /// <param name="sender">Object that sent event.</param>
        /// <param name="propertyChangedEventArgs">Arguments of the event.</param>
        private async void CurrentWeatherOnPropertyChanged(object sender,
            PropertyChangedEventArgs propertyChangedEventArgs)
        {
            var propertyName = propertyChangedEventArgs.PropertyName;
            if (propertyName == nameof(NotificationTask<CurrentWeather>.IsFaulted))
            {
                if (CurrentWeather.InnerException is HttpException exception)
                {
                    await ErrorHandler.HandleException((int)exception.StatusCode, exception.StatusCode.ToString());
                }
            }
        }

        #endregion
    }
}