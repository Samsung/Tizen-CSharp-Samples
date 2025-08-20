/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using FindPlace.Enums;
using FindPlace.Interfaces;
using FindPlace.Model;
using FindPlace.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace FindPlace.ViewModels
{
    /// <summary>
    /// The application's map view model class (abstraction of the view).
    /// </summary>
    public class MapViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Default latitude value.
        /// </summary>
        private const double DefaultLatitude = 52.232277;

        /// <summary>
        /// Default longitude value.
        /// </summary>
        private const double DefaultLongitude = 20.984245;

        /// <summary>
        /// Reference to object handling find place service obtained in constructor using DependencyService.
        /// </summary>
        private readonly IFindPlaceService _findPlaceService;

        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private IPageNavigation _pageNavigation;

        /// <summary>
        /// Reference to object handling logger service obtained in constructor using DependencyService.
        /// </summary>
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Field backing Location property.
        /// </summary>
        private Geocoordinates _location;

        /// <summary>
        /// Backing field for place type value.
        /// </summary>
        private PlaceType _placeType;

        #endregion

        #region properties

        /// <summary>
        /// Searches for places.
        /// </summary>
        public ICommand SearchForPlacesCommand { get; }

        /// <summary>
        /// Confirms area selection.
        /// </summary>
        public ICommand ConfirmAreaSelection { get; set; }

        /// <summary>
        /// Location value.
        /// </summary>
        public Geocoordinates Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        /// <summary>
        /// Area property.
        /// </summary>
        public Area Area { get; set; }

        /// <summary>
        /// Command for hiding loading.
        /// </summary>
        public ICommand HideLoading { get; set; }

        /// <summary>
        /// Command for showing loading.
        /// </summary>
        public ICommand ShowLoading { get; set; }

        /// <summary>
        /// Command for showing information about no places found.
        /// </summary>
        public ICommand ShowNoPlacesInformation { get; set; }

        /// <summary>
        /// Command for showing information about error.
        /// </summary>
        public ICommand ShowErrorInformation { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        /// <param name="placeType">Type of place.</param>
        /// <param name="location">Location geocoordinates.</param>
        public MapViewModel(PlaceType placeType, Geocoordinates location = null)
        {
            Location = location ?? new Geocoordinates(DefaultLatitude, DefaultLongitude);
            _placeType = placeType;

            _pageNavigation = DependencyService.Get<IPageNavigation>();
            _findPlaceService = DependencyService.Get<IFindPlaceService>();
            _loggerService = DependencyService.Get<ILoggerService>();

            SearchForPlacesCommand = new Command(ExecuteSearchForPlacesAsync);
        }

        /// <summary>
        /// Handles execution of SearchForPlacesCommand.
        /// </summary>
        private async void ExecuteSearchForPlacesAsync()
        {
            ShowLoading?.Execute(null);
            ConfirmAreaSelection?.Execute(null);

            Location = Area.CenterPoint;

            var result = await _findPlaceService.GetPlacesAsync(Area, _placeType);
            _loggerService.Verbose($"FindPlace Service response success: {result.Success}");

            HideLoading?.Execute(null);

            if (result.Success)
            {
                if (result.Results != null)
                {
                    result.Results.ForEach(r => _loggerService.Verbose($"{r}"));
                    ResultsViewModel resultsViewModel = new ResultsViewModel(_placeType, result.Results);
                    _pageNavigation.NavigateToResultsPage(resultsViewModel);
                }
                else
                {
                    ShowNoPlacesInformation?.Execute(null);
                }
            }
            else
            {
                ShowErrorInformation?.Execute(null);
            }
        }

        #endregion
    }
}
