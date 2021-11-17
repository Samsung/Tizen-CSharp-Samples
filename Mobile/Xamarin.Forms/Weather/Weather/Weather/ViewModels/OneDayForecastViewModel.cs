/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using Weather.Models.Weather;

namespace Weather.ViewModels
{
    /// <summary>
    /// View Model class for forecast for one day.
    /// </summary>
    public class OneDayForecastViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Local storage of forecast data for one day.
        /// </summary>
        private readonly List<CurrentWeather> _forecast;

        /// <summary>
        /// Local storage of time for which forecast is displayed.
        /// </summary>
        private int _timeValue;

        /// <summary>
        /// Local storage of forecast data for selected time.
        /// </summary>
        private CurrentWeather _currentWeather;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets timezone offset.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets forecast data for selected time.
        /// </summary>
        public CurrentWeather CurrentWeather
        {
            get => _currentWeather;
            set => SetProperty(ref _currentWeather, value);
        }

        /// <summary>
        /// Gets size of the list with forecast.
        /// </summary>
        public int Count => _forecast.Count - 1;

        /// <summary>
        /// Gets timestamp of first forecast date of one day.
        /// </summary>
        public ulong FirstTime => _forecast[0].TimeStamp;

        /// <summary>
        /// Gets timestamp of last forecast date of one day.
        /// </summary>
        public ulong LastTime => _forecast[_forecast.Count - 1].TimeStamp;

        /// <summary>
        /// Gets or sets time for which forecast is displayed.
        /// </summary>
        public int TimeValue
        {
            get => _timeValue;
            set
            {
                SetProperty(ref _timeValue, value);
                UpdateForecastTime(value);
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Parametrized constructor that allows to set forecast for each hour in one day.
        /// </summary>
        /// <param name="forecast">Forecast data for one day.</param>
        public OneDayForecastViewModel(List<CurrentWeather> forecast)
        {
            _timeValue = 0;
            _forecast = forecast;
            CurrentWeather = _forecast[0];
        }

        /// <summary>
        /// Updates actual forecast data.
        /// </summary>
        /// <param name="index">Index of list with forecast data.</param>
        private void UpdateForecastTime(int index)
        {
            CurrentWeather = _forecast[index];
        }

        #endregion
    }
}