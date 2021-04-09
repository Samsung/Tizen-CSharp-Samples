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
using Geocoding.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Geocoding.ViewModels
{
    /// <summary>
    /// The application's main view model class (abstraction of the view).
    /// </summary>
    class MainViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private readonly IPageNavigation _navigation;

        /// <summary>
        /// Map service which allows to obtain the geolocation data.
        /// </summary>
        private IGeocodingService _iGeocodingService;

        /// <summary>
        /// Information popup service which allows to use the information popup.
        /// </summary>
        private IInformationPopupService _iInformationPopupService;

        /// <summary>
        /// Backing field of the SearchValue property.
        /// </summary>
        private string _searchValue = "";

        /// <summary>
        /// Backing field of the IsSearchValueValid property.
        /// </summary>
        private bool _isSearchValueValid = false;

        /// <summary>
        /// Backing field of the IsSearchInputVisible property.
        /// </summary>
        private bool _isSearchInputVisible = true;

        #endregion

        #region properties

        /// <summary>
        /// Property storing value used as a parameter during translating to geographical coordinates.
        /// </summary>
        public string SearchValue
        {
            set
            {
                SetProperty(ref _searchValue, value);
                IsSearchValueValid = this._searchValue.Trim().Length > 0;
            }

            get => _searchValue;
        }

        /// <summary>
        /// Property indicating whether the search value is valid or not.
        /// </summary>
        public bool IsSearchValueValid
        {
            set => SetProperty(ref _isSearchValueValid, value);
            get => _isSearchValueValid;
        }

        /// <summary>
        /// Property indicating whether the search process is in progress.
        /// </summary>
        public bool IsSearchInputVisible
        {
            set => SetProperty(ref _isSearchInputVisible, value);
            get => _isSearchInputVisible;
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        public ICommand StartCommand { get; private set; }

        /// <summary>
        /// Starts searching.
        /// </summary>
        public ICommand StartSearchingCommand { get; private set; }

        /// <summary>
        /// Shows information that not all markers are displayed on the map.
        /// </summary>
        public ICommand ShowFindMoreMarkersInfoCommand { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public MainViewModel()
        {
            _navigation = DependencyService.Get<IPageNavigation>();
            _iInformationPopupService = DependencyService.Get<IInformationPopupService>();
            _iGeocodingService = DependencyService.Get<IGeocodingService>();

            _iGeocodingService.UserConsent += ServiceOnUserConsent;
            _iGeocodingService.GeocodeRequestSuccess += ServiceOnGeocodeRequestSuccess;
            _iGeocodingService.GeocodeRequestNotFound += ServiceOnGeocodeRequestNotFound;
            _iGeocodingService.GeocodeRequestConnectionFailed += ServiceOnGeocodeRequestConnectionFailed;

            InitCommands();
        }

        /// <summary>
        /// Handles "GeocodeRequestConnectionFailed" event of the geocoding service.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ServiceOnGeocodeRequestConnectionFailed(object sender, EventArgs e)
        {
            IsSearchInputVisible = true;
            _iInformationPopupService.ShowConnectionFailedPopup();
        }

        /// <summary>
        /// Handles "GeocodeRequestNotFound" event of the geocoding service.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ServiceOnGeocodeRequestNotFound(object sender, EventArgs e)
        {
            IsSearchInputVisible = true;
            _iInformationPopupService.ShowNoResultsPopup();
        }

        /// <summary>
        /// Handles "GeocodeRequestSuccess" event of the geocoding service.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ServiceOnGeocodeRequestSuccess(object sender, EventArgs e)
        {
            IsSearchInputVisible = true;
            _navigation.CreateMapPage();
        }

        /// <summary>
        /// Handles "UserConsent" event of the geocoding service.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ServiceOnUserConsent(object sender, IGeocodingUserConsentArgs e)
        {
            if (e.IsConsent)
            {
                _navigation.CreateSearchPage();
            }
            else
            {
                _navigation.Close();
            }
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        private void InitCommands()
        {
            StartCommand = new Command(ExecuteStart);
            StartSearchingCommand = new Command(ExecuteStartSearching);
            ShowFindMoreMarkersInfoCommand = new Command(ExecuteShowFindMoreMarkersInfo);
        }

        /// <summary>
        /// Handles execution of "ShowFindMoreMarkersInfoCommand".
        /// </summary>
        private void ExecuteShowFindMoreMarkersInfo()
        {
            _iInformationPopupService.ShowFindMoreMarkersPopup();
        }

        /// <summary>
        /// Handles execution of "StartCommand".
        /// </summary>
        private void ExecuteStart()
        {
            _iGeocodingService.RequestUserConsent();
        }

        /// <summary>
        /// Handles execution of "StartSearchingCommand".
        /// </summary>
        private void ExecuteStartSearching()
        {
            IsSearchInputVisible = false;
            _iGeocodingService.CreateGeocodeRequestMethod(SearchValue);
        }

        #endregion
    }
}
