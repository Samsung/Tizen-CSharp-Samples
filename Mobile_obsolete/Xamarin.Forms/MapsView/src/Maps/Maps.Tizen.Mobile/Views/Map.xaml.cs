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
using System.Linq;

using Tizen.Maps;

namespace Maps.Tizen.Mobile.Views
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
            MapComponent.MapInitialized += OnMapInitialized;
        }

        private void OnMapInitialized(object sender, EventArgs e)
        {
            AddPinsToMap();
            SetMapOnFirstPin();
            AddEventHandlers();

            MapComponent.MapInitialized -= OnMapInitialized;
        }

        /// <summary>
        /// Adds handlers for View Model events.
        /// </summary>
        private void AddEventHandlers()
        {
            _viewModel.OnPinRequest += SetMapToPinLocation;
            _viewModel.OnZoomChanged += SetMapZoom;
        }

        private void SetMapZoom(object sender, EventArgs e)
        {
            MapComponent.Map.ZoomLevel = _viewModel.ZoomLevel;
        }

        /// <summary>
        /// Adds Pins to the map.
        /// </summary>
        private void AddPinsToMap()
        {
            var pins = _viewModel.PinPoints.Select(pin =>
                new Pin(new Geocoordinates(pin.Position.Latitude, pin.Position.Longitude)));

            foreach (var pin in pins)
            {
                MapComponent.Map.Add(pin);
            }
        }

        /// <summary>
        /// Sets map center to pin location.
        /// </summary>
        /// <param name="sender">Event sender. Not used.</param>
        /// <param name="pin">Pin.</param>
        public void SetMapToPinLocation(object sender, Models.Pin pin)
        {
            SetMapCenter(pin.Position.Latitude, pin.Position.Longitude);
        }

        /// <summary>
        /// Sets map center to provided position.
        /// </summary>
        /// <param name="latitude">Latitude value.</param>
        /// <param name="longitude">Longitude value.</param>
        private void SetMapCenter(double latitude, double longitude)
        {
            MapComponent.Map.Center = new Geocoordinates(latitude, longitude);
        }

        /// <summary>
        /// Sets map center to location of first Pin.
        /// </summary>
        private void SetMapOnFirstPin()
        {
            Models.Pin firstPinOnList = _viewModel.GetFirstPin();
            SetMapCenter(firstPinOnList.Position.Latitude, firstPinOnList.Position.Longitude);
        }

        #endregion
    }
}
