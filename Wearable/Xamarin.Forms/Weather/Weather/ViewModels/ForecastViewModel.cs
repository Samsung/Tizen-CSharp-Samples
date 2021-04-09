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
using System.Collections.ObjectModel;
using System.Linq;
using Weather.Models.Weather;
using Weather.Utils;
using Xamarin.Forms;
using Tizen.System;
using Tizen;

namespace Weather.ViewModels
{
    /// <summary>
    /// ViewModel class for forecast root page.
    /// </summary>
    public class ForecastViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Local storage of all the forecasts (in CurrentWeather format)
        /// </summary>
        private ObservableCollection<CurrentWeather> _forecastsModels;

        /// <summary>
        /// Command to handle UI request for a previous forecast display
        /// </summary>
        private Command _previousForecastCommand;

        /// <summary>
        /// Command to handle UI request for next forecast display
        /// </summary>
        private Command _nextForecastCommand;

        #endregion

        #region properties

        /// <summary>
        /// Bindable property that allows to set forecast data.
        /// </summary>
        public static readonly BindableProperty ForecastProperty =
            BindableProperty.Create(nameof(Forecast), typeof(Forecast), typeof(ForecastViewModel), default(Forecast),
                propertyChanged: ForecastPropertyChanged);

        /// <summary>
        /// Bindable property that allows to set timezone offset property.
        /// </summary>
        public static readonly BindableProperty OffsetProperty =
            BindableProperty.Create(nameof(Offset), typeof(int), typeof(ForecastViewModel), default(int));

        /// <summary>
        /// Bindable property that allows to set City Name property.
        /// </summary>
        public static readonly BindableProperty CityNameProperty =
            BindableProperty.Create(nameof(CityName), typeof(string), typeof(ForecastViewModel), "");

        /// <summary>
        /// Gets or sets timezone offset property.
        /// </summary>
        public int Offset
        {
            get => (int)GetValue(OffsetProperty);
            set => SetValue(OffsetProperty, value);
        }

        /// <summary>
        /// Identifier of current forecast being presented in UI (id of an element in _forecastsModels table)
        /// </summary>
        public int CurrentForecastId { get; private set; } = 0;

        /// <summary>
        /// Readonly property referencing currently selected forecast
        /// </summary>
        public CurrentWeather SelectedForecast
        {
            get
            {
                if (ForecastsModels != null)
                {
                    return ForecastsModels[CurrentForecastId];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Get or set forecast data.
        /// </summary>
        public Forecast Forecast
        {
            get => (Forecast)GetValue(ForecastProperty);
            set => SetValue(ForecastProperty, value);
        }

        /// <summary>
        /// Get or set CityName property
        /// </summary>
        public string CityName
        {
            get => GetValue(CityNameProperty).ToString();
            set => SetValue(CityNameProperty, value);
        }

        /// <summary>
        /// Get or set _forecastsModels => Local storage of all the forecasts (in CurrentWeather format)
        /// </summary>
        public ObservableCollection<CurrentWeather> ForecastsModels
        {
            get => _forecastsModels;
            set => SetProperty(ref _forecastsModels, value);
        }

        /// <summary>
        /// Get or set _nextForecastCommand => Command to handle UI request for next forecast display
        /// </summary>
        public Command NextForecastCommand
        {
            get => _nextForecastCommand;
            set => SetProperty(ref _nextForecastCommand, value);
        }

        /// <summary>
        /// Get or set _previousForecastCommand => Command to handle UI request for a previous forecast display
        /// </summary>
        public Command PreviousForecastCommand
        {
            get => _previousForecastCommand;
            set => SetProperty(ref _previousForecastCommand, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Callback method invoked on Forecast property change.
        /// </summary>
        /// <param name="bindable">Object that contains property.</param>
        /// <param name="oldValue">Old value of the property.</param>
        /// <param name="newValue">New value of the property.</param>
        private static void ForecastPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var viewModel = (ForecastViewModel)bindable;
                viewModel.PrepareViewModels();
            }
        }

        /// <summary>
        /// Prepares view-models for every day of forecast.
        /// Assigns prev/next forecast handlers
        /// </summary>
        private void PrepareViewModels()
        {
            ForecastsModels = new ObservableCollection<CurrentWeather>();
            var dictionary = SortViewModelsByDate();
            foreach (var list in dictionary.Values)
            {
                foreach (CurrentWeather forecast in list)
                {
                    ForecastsModels.Add(forecast);
                }
            }

            // Previous forecast handler - either navigate down the forecast list or
            // let the user know that the bottom (0) is reached via a simple vibration
            PreviousForecastCommand = new Command(o =>
            {
                if (ForecastsModels != null)
                {
                    if (CurrentForecastId > 0)
                    {
                        CurrentForecastId--;
                        OnPropertyChanged(nameof(SelectedForecast));
                    }
                    else
                    {
                        Vibrate();
                    }
                }
            });

            // Next forecast handler - either navigate up the forecast list or
            // let the user know that the top (most distant forecast) is reached via a simple vibration
            NextForecastCommand = new Command(o =>
            {
                if (ForecastsModels != null)
                {
                    if (CurrentForecastId < _forecastsModels.Count - 1)
                    {
                        CurrentForecastId++;
                        OnPropertyChanged(nameof(SelectedForecast));
                    }
                    else
                    {
                        Vibrate();
                    }
                }
            });
            // Notify all parties interested that the Selected forecast (initially the first one) is
            // ready and waiting to be presented in the relevant View.
            OnPropertyChanged(nameof(SelectedForecast));
        }

        /// <summary>
        /// Sort forecast data by date.
        /// </summary>
        /// <returns>List of sorted forecast data.</returns>
        private Dictionary<int, List<CurrentWeather>> SortViewModelsByDate()
        {
            var dictionary = new Dictionary<int, List<CurrentWeather>>();

            foreach (var weather in Forecast.WeatherList)
            {
                var day = TimeStamp.Convert(weather.TimeStamp).AddSeconds(Offset).DayOfYear;

                if (!dictionary.ContainsKey(day))
                {
                    dictionary.Add(day, new List<CurrentWeather>());
                }

                dictionary[day].Add(weather);
            }

            if (dictionary.Count > 5)
            {
                dictionary.Remove(dictionary.Last().Key);
            }

            return dictionary;
        }


        /// <summary>
        /// Use Tizen.System.Feedback to let user know something went a bit wrong (vibrate)      
        /// </summary>
        private void Vibrate()
        {
            try
            {
                Feedback feedback = new Feedback();
                feedback.Play(FeedbackType.All, "General");
            }
            catch (System.Exception e)
            {
                Log.Debug("WeatherApp", e.Message);
            }
        }

        #endregion
    }
}