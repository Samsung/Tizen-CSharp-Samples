/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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
using Xamarin.Forms.Maps;

namespace Maps.Tizen.TV.Views
{
    /// <summary>
    /// Wrapper for map component.
    /// </summary>
    public partial class MapWrapper
    {
        #region fields

        /// <summary>
        /// Stores reference to common PageViewModel instance.
        /// </summary>
        private readonly ViewModels.PageViewModel _viewModel = ViewModels.ViewModelLocator.ViewModel;

        #endregion

        #region methods

        /// <summary>
        /// Initializes component.
        /// </summary>
        public MapWrapper()
        {
            InitializeComponent();
            BindingContext = _viewModel as object;

            AddPinsToMap();
            SetMapOnFirstPin();
            AddEventHandlers();
        }

        /// <summary>
        /// Adds handlers for View Model events.
        /// </summary>
        private void AddEventHandlers()
        {
            _viewModel.OnZoomChanged += (sender, args) => SetMapCenter(MapComponent.VisibleRegion.Center);
            _viewModel.OnPinRequest += SetMapToPinLocation;
        }

        /// <summary>
        /// Adds Pins to the map.
        /// </summary>
        private void AddPinsToMap()
        {
            foreach (Models.Pin pin in _viewModel.PinPoints)
            {
                MapComponent.Pins.Add(new Pin
                {
                    Type = PinType.SearchResult,
                    Position = new Position(pin.Position.Latitude, pin.Position.Longitude),
                    Label = pin.DisplayName
                });
            }
        }

        /// <summary>
        /// Sets map center to pin location.
        /// </summary>
        /// <param name="sender">Event sender. Not used.</param>
        /// <param name="pin">Pin.</param>
        public void SetMapToPinLocation(object sender, Models.Pin pin)
        {
            SetMapCenter(new Position(pin.Position.Latitude, pin.Position.Longitude));
        }

        /// <summary>
        /// Sets map center to provided position.
        /// </summary>
        /// <param name="position">Requested position.</param>
        private void SetMapCenter(Position position)
        {
            double latlongdegrees = GetLatLonDegress();

            MapComponent.MoveToRegion(new MapSpan(position, latlongdegrees, latlongdegrees));
        }

        /// <summary>
        /// Sets map center to location of first Pin.
        /// </summary>
        private void SetMapOnFirstPin()
        {
            Models.Pin firstPinOnList = _viewModel.GetFirstPin();
            SetMapCenter(new Position(firstPinOnList.Position.Latitude, firstPinOnList.Position.Longitude));
        }

        /// <summary>
        /// Calculates degrees that map has to display.
        /// </summary>
        /// <returns>Calculated degrees value.</returns>
        private double GetLatLonDegress()
        {
            return 360 / Math.Pow(2, _viewModel.ZoomLevel);
        }

        #endregion
    }
}