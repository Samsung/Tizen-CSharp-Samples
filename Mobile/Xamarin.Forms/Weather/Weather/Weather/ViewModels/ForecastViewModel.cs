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
using System.Collections.ObjectModel;
using System.Linq;
using Weather.Models.Weather;
using Weather.Utils;
using Xamarin.Forms;

namespace Weather.ViewModels
{
    /// <summary>
    /// ViewModel class for forecast root page.
    /// </summary>
    public class ForecastViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Local storage of all view models for forecast pages.
        /// </summary>
        private ObservableCollection<OneDayForecastViewModel> _pagesViewModels;

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
        /// Bindable property that allows to set timezone offset property.
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
        /// Gets or sets forecast data.
        /// </summary>
        public Forecast Forecast
        {
            get => (Forecast)GetValue(ForecastProperty);
            set => SetValue(ForecastProperty, value);
        }

        /// <summary>
        /// Gets or sets timezone offset property.
        /// </summary>
        public string CityName
        {
            get => GetValue(CityNameProperty).ToString();
            set => SetValue(CityNameProperty, value);
        }

        /// <summary>
        /// Gets or sets all view models for forecast pages.
        /// </summary>
        public ObservableCollection<OneDayForecastViewModel> PagesViewModels
        {
            get => _pagesViewModels;
            set => SetProperty(ref _pagesViewModels, value);
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
        /// </summary>
        private void PrepareViewModels()
        {
            PagesViewModels = new ObservableCollection<OneDayForecastViewModel>();

            var dictionary = SortViewModelsByDate();

            foreach (var list in dictionary.Values)
            {
                PagesViewModels.Add(new OneDayForecastViewModel(list) {Offset = Offset});
            }
        }

        /// <summary>
        /// Sorts forecast data by date and packs it into separate lists.
        /// </summary>
        /// <returns>Lists of sorted forecast data.</returns>
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

        #endregion
    }
}