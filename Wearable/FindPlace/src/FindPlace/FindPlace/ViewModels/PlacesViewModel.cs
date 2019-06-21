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
using FindPlace.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace FindPlace.ViewModels
{
    /// <summary>
    /// Provides places page view abstraction.
    /// </summary>
    public class PlacesViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private readonly IPageNavigation _pageNavigation;

        /// <summary>
        /// Reference to object handling location obtained in constructor using DependencyService.
        /// </summary>
        private readonly ILocationService _locationService;

        /// <summary>
        /// Backing field of SelectedPlaceType property.
        /// </summary>
        private PlaceTypeViewModel _selectedPlaceType;

        /// <summary>
        /// Backing field for place type value.
        /// </summary>
        private PlaceType _placeType;

        #endregion

        #region properties

        /// <summary>
        /// Command for hiding loading.
        /// </summary>
        public ICommand HideLoading { get; set; }

        /// <summary>
        /// Command for showing loading.
        /// </summary>
        public ICommand ShowLoading { get; set; }

        /// <summary>
        /// Command for showing information about GPS not available.
        /// </summary>
        public ICommand ShowNotAvailableInformation { get; set; }

        /// <summary>
        /// Command for showing information about GPS access not granted.
        /// </summary>
        public ICommand ShowNotGrantedInformation { get; set; }

        /// <summary>
        /// List of available place types.
        /// </summary>
        public List<PlaceTypeViewModel> PlaceTypeList { get; }

        /// <summary>
        /// Selected place type.
        /// Setter navigates to next page.
        /// </summary>
        public PlaceTypeViewModel SelectedPlaceType
        {
            get => _selectedPlaceType;
            set
            {
                SetProperty(ref _selectedPlaceType, value);
                if (!value.IsEmpty)
                {
                    _placeType = value.PlaceType;
                    _selectedPlaceType = PlaceTypeViewModel.Empty;
                    GetLocationAsync();
                }
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes PlacesViewModel class instance.
        /// </summary>
        public PlacesViewModel()
        {
            _locationService = DependencyService.Get<ILocationService>();
            _pageNavigation = DependencyService.Get<IPageNavigation>();
            PlaceTypeList = new List<PlaceTypeViewModel>();

            InitializeCollecition();
        }

        /// <summary>
        /// Initializes collection of places.
        /// </summary>
        private void InitializeCollecition()
        {
            PlaceTypeList.AddRange(Enum.GetValues(typeof(PlaceType))
                .Cast<PlaceType>()
                .Select(p => new PlaceTypeViewModel(p)));
        }

        /// <summary>
        /// Searches for location.
        /// </summary>
        private async void GetLocationAsync()
        {
            ShowLoading?.Execute(null);
            var locationResponse = await _locationService.GetLocationAsync();
            HideLoading?.Execute(null);
            if (locationResponse.PermissionGranted)
            {
                if (!locationResponse.Success)
                {
                    ShowNotAvailableInformation?.Execute(null);
                }
            }
            else
            {
                ShowNotGrantedInformation?.Execute(null);
            }

            MapViewModel mapViewModel = new MapViewModel(_placeType, locationResponse.Result);

            _pageNavigation.NavigateToMapPage(mapViewModel);
        }

        #endregion
    }
}
