﻿/*
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Weather.Models.Location;
using Weather.Service;
using Weather.Utils;
using Xamarin.Forms;

namespace Weather.ViewModels
{
    /// <summary>
    /// ViewModel class for Main Page.
    /// </summary>
    public class MainPageViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Bindable property that allows to set navigation context.
        /// </summary>
        public static readonly BindableProperty NavigationProperty =
            BindableProperty.Create(nameof(Navigation), typeof(INavigation), typeof(MainPageViewModel), default(Type));

        /// <summary>
        /// Maximum number of items that will be displayed on the list.
        /// </summary>
        private const int MAX_ITEMS_ON_LIST = 10;

        /// <summary>
        /// Contains all supported cities.
        /// </summary>
        private CityProvider _provider;

        /// <summary>
        /// Local storage of collection of displayed cities.
        /// </summary>
        private ObservableCollection<City> _cities;

        /// <summary>
        /// Local storage of city name entered by user.
        /// </summary>
        private string _enteredCity = "";

        /// <summary>
        /// Local storage of city selected by user.
        /// </summary>
        private City _selectedCity;

        /// <summary>
        /// Local storage of command that opens page provided in command parameter.
        /// </summary>
        private Command _checkWeatherCommand;

        /// <summary>
        /// Local storage of country code.
        /// </summary>
        private string _enteredCountry;

        /// <summary>
        /// Local storage of command that will be executed, when input will be completed.
        /// </summary>
        private Command _onCityEnteredCommand;

        /// <summary>
        /// Local storage flag indicating if city entry is visible.
        /// </summary>
        private bool _isCityEntryVisible;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets flag indicating if city entry is visible.
        /// </summary>
        public bool IsCityEntryVisible
        {
            get => _isCityEntryVisible;
            private set => SetProperty(ref _isCityEntryVisible, value);
        }

        /// <summary>
        /// Gets or sets collection of available cities.
        /// </summary>
        public ObservableCollection<City> Cities
        {
            get => _cities;
            set
            {
                IsCityEntryVisible = value.Any()  || !string.IsNullOrEmpty(EnteredCity);
                SetProperty(ref _cities, value);
            }
        }

        /// <summary>
        /// Gets or sets city name entered by user.
        /// </summary>
        public string EnteredCity
        {
            get => _enteredCity;
            set
            {
                SetProperty(ref _enteredCity, value);
                FilterCities();
                ValidateInput();
            }
        }

        /// <summary>
        /// Gets or sets city selected by user.
        /// </summary>
        public City SelectedCity
        {
            get => _selectedCity;
            set
            {
                SetProperty(ref _selectedCity, value);

                if (value != null)
                {
                    ((App)Application.Current).IsInitialized = false;
                    EnteredCity = value.Name;
                    CheckWeatherCommand.ChangeCanExecute();
                }
            }
        }

        /// <summary>
        /// Gets or sets command that opens page provided in command parameter.
        /// </summary>
        public Command CheckWeatherCommand
        {
            get => _checkWeatherCommand;
            set => SetProperty(ref _checkWeatherCommand, value);
        }

        /// <summary>
        /// Command that will be executed when input will be completed.
        /// </summary>
        public Command OnCityEnteredCommand
        {
            get => _onCityEnteredCommand;
            set => SetProperty(ref _onCityEnteredCommand, value);
        }

        /// <summary>
        /// Country code in ISO-3166 format.
        /// </summary>
        public string EnteredCountry
        {
            get => _enteredCountry;
            set
            {
                SetProperty(ref _enteredCountry, value);
                FilterCities();
            }
        }

        /// <summary>
        /// Gets or sets navigation context.
        /// </summary>
        public INavigation Navigation
        {
            get => (INavigation)GetValue(NavigationProperty);
            set => SetValue(NavigationProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public MainPageViewModel()
        {
            LoadCityList();

            Cities = new ObservableCollection<City>(_provider.FindCity("", MAX_ITEMS_ON_LIST));

            CheckWeatherCommand = new Command<Page>(ExecuteCheckWeatherCommand, CanExecuteCheckWeatherCommand);

            OnCityEnteredCommand = new Command(() => SelectedCity = Cities.FirstOrDefault());
        }

        /// <summary>
        /// Loads list of cities from JSON file.
        /// </summary>
        private void LoadCityList()
        {
            var jsonFileReader = new JsonFileReader<IList<City>>("Weather.Data.", "city.list.json");
            jsonFileReader.Read();
            _provider = new CityProvider(jsonFileReader.Result.AsQueryable());
        }

        /// <summary>
        /// Filters city list using text entered by user.
        /// </summary>
        private void FilterCities()
        {
            Cities = new ObservableCollection<City>(
                _provider.FindCity(_enteredCity, _enteredCountry, MAX_ITEMS_ON_LIST));
        }

        /// <summary>
        /// Validates city name entered by user.
        /// </summary>
        private void ValidateInput()
        {
            if (!_provider.Validate(EnteredCity))
            {
                SelectedCity = null;
                CheckWeatherCommand.ChangeCanExecute();
            }
        }

        /// <summary>
        /// Checks if CheckWeather command could be executed.
        /// Page parameter and selected city can't be null.
        /// </summary>
        /// <param name="page">Page that will be shown.</param>
        /// <returns>
        /// Returns true if city is selected.
        /// If no city is selected, or it is not valid method returns false.
        /// </returns>
        private bool CanExecuteCheckWeatherCommand(Page page)
        {
            return page != null && SelectedCity != null;
        }

        /// <summary>
        /// Executes CheckWeather command.
        /// Pushes page given as command parameter to navigation stack.
        /// </summary>
        /// <param name="page">Page that will be opened.</param>
        private void ExecuteCheckWeatherCommand(Page page)
        {
            Navigation.PushAsync(page);
        }

        #endregion
    }
}