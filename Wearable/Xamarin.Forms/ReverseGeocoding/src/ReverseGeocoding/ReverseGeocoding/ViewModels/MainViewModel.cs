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
using Xamarin.Forms;
using ReverseGeocoding.Views;
using System.Windows.Input;
using ReverseGeocoding.Interfaces;
using ReverseGeocoding.Common;

namespace ReverseGeocoding.ViewModels
{
    /// <summary>
    /// The application's main view model class (abstraction of the view).
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Reference to object handling navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private readonly IPageNavigation _navigation;

        /// <summary>
        /// Reference to object handling reverse geocoding services obtained in constructor using DependencyService.
        /// </summary>
        private readonly IReverseGeocodingService _reverseGeocodingService;

        /// <summary>
        /// Reference to object handling information popup obtained in constructor using DependencyService.
        /// </summary>
        private readonly IInformationPopupService _informationPopupService;

        /// <summary>
        /// Represents the data about the place which was selected.
        /// </summary>
        private string _place;

        #endregion

        #region properties

        /// <summary>
        /// Navigates to main page.
        /// </summary>
        public ICommand GoToMainPageCommand { get; private set; }

        /// <summary>
        /// Navigates to results page.
        /// </summary>
        public ICommand GoToResultsPageCommand { get; private set; }

        /// <summary>
        /// Navigates to previous page.
        /// </summary>
        public ICommand GoToPreviousPageCommand { get; private set; }

        /// <summary>
        /// Gets or sets data about the place which was selected.
        /// </summary>
        public string Place
        {
            get => _place;
            set => SetProperty(ref _place, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public MainViewModel()
        {
            _navigation = DependencyService.Get<IPageNavigation>();
            _reverseGeocodingService = DependencyService.Get<IReverseGeocodingService>();
            _informationPopupService = DependencyService.Get<IInformationPopupService>();

            _reverseGeocodingService.UserConsent += ServiceOnUserConsented;
            _reverseGeocodingService.ResponseReceived += OnResponseReceived;

            Place = "";

            InitCommands();
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        private void InitCommands()
        {
            GoToMainPageCommand = new Command(ExecuteGoToMainPage);
            GoToResultsPageCommand = new Command<PointGeocoordinates>(ExecuteGoToResultsPage);
            GoToPreviousPageCommand = new Command(ExecuteGoToPreviousPage);
        }

        /// <summary>
        /// Handles execution of "GoToResultsPageCommand".
        /// </summary>
        /// <param name="geocoordinates">Geocoordinates of the selected point.</param>
        private void ExecuteGoToResultsPage(PointGeocoordinates geocoordinates)
        {
            _informationPopupService.ShowLoadingPopup();
            _reverseGeocodingService.CreateReverseGeocodeRequest(geocoordinates);
        }

        /// <summary>
        /// Handles execution of "GoToMainPageCommand".
        /// </summary>
        private void ExecuteGoToMainPage()
        {
            _reverseGeocodingService.RequestUserConsent();
        }

        /// <summary>
        /// Handles "UserConsent" event of the geocoding service.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ServiceOnUserConsented(object sender, IReverseGeocodingUserConsentArgs e)
        {
            if (e.Consent)
            {
                _navigation.CreateMainPage();
            }
            else
            {
                _navigation.Close();
            }
        }

        /// <summary>
        /// Handles the execution of "GoToPreviousPageCommand".
        /// </summary>
        private void ExecuteGoToPreviousPage()
        {
            _navigation.GoBack();
        }

        /// <summary>
        /// Handles "ResponseReceived" event of the geocoding service.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnResponseReceived(object sender, IReverseGeocodingResponseArgs e)
        {
            if (e.Success)
            {
                Place = e.Result;
                _navigation.CreateResultsPage();
                _informationPopupService.Dismiss();
            }
            else
            {
                _informationPopupService.Dismiss();
                _informationPopupService.ShowErrorPopup();
            }
        }

        #endregion
    }
}
