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
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Geocoding.Tizen.Wearable.Controls;
using Geocoding.Tizen.Wearable.Services;
using Geocoding.ViewModels;
using Tizen.Maps;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Xaml;

namespace Geocoding.Tizen.Wearable.Views
{
    /// <summary>
    /// Map page.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : CirclePage
    {
        #region fields

        /// <summary>
        /// Geocoding service interface used to manage geolocation data provided by the Tizen maps.
        /// </summary>
        private IGeocodingService _iGeocodingService;

        /// <summary>
        /// MapService class instance used for getting the map service data.
        /// </summary>
        private MapService _mapService;

        /// <summary>
        /// TizenMapView class instance used for interacting with the map view.
        /// </summary>
        private TizenMapView _mapView;

        /// <summary>
        /// List of pins' screen coordinates.
        /// </summary>
        private List<Point> _pinScreenCoordinates = new List<Point>();

        /// <summary>
        /// Recently received geographical coordinates.
        /// </summary>
        private Geocoordinates _recentlyReceivedCoordinates;

        /// <summary>
        /// Default zoom value.
        /// </summary>
        private const int DEFAULT_ZOOM_VALUE = 2;

        /// <summary>
        /// Zoom value used to display one marked in the middle of the map view.
        /// </summary>
        private const int ZOOM_VALUE_FOR_ONE_MARKER = 12;

        /// <summary>
        /// Maximum distance between markers.
        /// </summary>
        private const int MAX_DISTANCE_BETWEEN_MARKERS = 300;

        /// <summary>
        /// Device screen size.
        /// </summary>
        private const int SCREEN_SIZE = 360;

        /// <summary>
        /// Calculated distance in pixels between most distant markers.
        /// </summary>
        private double _distance = 0;

        #endregion

        #region properties

        /// <summary>
        /// "Find more markers" bindable property definition.
        /// </summary>
        public static BindableProperty FindMoreMarkersCommandProperty =
            BindableProperty.Create("FindMoreMarkersCommand",
            typeof(ICommand), typeof(MapPage), default(ICommand));

        /// <summary>
        /// "Find more markers" command.
        /// </summary>
        public ICommand FindMoreMarkersCommand
        {
            set => SetValue(FindMoreMarkersCommandProperty, value);
            get => (ICommand)GetValue(FindMoreMarkersCommandProperty);
        }

        #endregion

        #region methods

        /// <summary>
        /// MapPage class constructor.
        /// </summary>
        public MapPage()
        {
            InitializeComponent();

            _iGeocodingService = DependencyService.Get<IGeocodingService>();
            _mapService = DependencyService.Get<IMapServiceProvider>().GetService();

            _iGeocodingService.CoordinatesReceived += ServiceOnCoordinatesReceived;
            _iGeocodingService.CenterPointCalculated += ServiceOnCenterPointCalculated;

            NavigationPage.SetHasNavigationBar(this, false);

            _mapView = new TizenMapView
            {
                Service = _mapService,
                ControlWidth = SCREEN_SIZE,
                ControlHeight = SCREEN_SIZE
            };

            _mapView.MapViewSet += OnMapViewSet;

            Content = _mapView;

            ElmSharp.Wearable.RotaryEventManager.Rotated += OnRotaryChange;
        }

        /// <summary>
        /// Handles "Rotated" event of the rotary event manager.
        /// Zooms in/out map on rotary event.
        /// </summary>
        /// <param name="e">Rotary event arguments.</param>
        private void OnRotaryChange(ElmSharp.Wearable.RotaryEventArgs e)
        {
            if (e.IsClockwise)
            {
                _mapView.Map.ZoomLevel++;
            }
            else
            {
                _mapView.Map.ZoomLevel--;
            }
        }

        /// <summary>
        /// Handles "CenterPointCalculated" event of the geocoding service.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ServiceOnCenterPointCalculated(object sender, IGeocodingCoordinatesArgs e)
        {
            _mapView.Map.Center = new Geocoordinates(e.Latitude, e.Longitude);
            InitializeZoom();
            CenterMap(e.Latitude, e.Longitude);
        }

        /// <summary>
        /// Centers the map view.
        /// </summary>
        /// <param name="latitude">Latitude value.</param>
        /// <param name="longitude">Longitude value.</param>
        private void CenterMap(double latitude, double longitude)
        {
            if (_distance >= MAX_DISTANCE_BETWEEN_MARKERS)
            {
                _mapView.Map.Center = _recentlyReceivedCoordinates;
                FindMoreMarkersCommand?.Execute(null);
            }
            else
            {
                _mapView.Map.Center = new Geocoordinates(latitude, longitude);
            }
        }

        /// <summary>
        /// Initializes the zoom value of the map view.
        /// </summary>
        private void InitializeZoom()
        {
            double minX = Double.MaxValue;
            double maxX = 0;
            double minY = Double.MaxValue;
            double maxY = 0;

            if (_pinScreenCoordinates.Count == 1)
            {
                _mapView.Map.ZoomLevel = ZOOM_VALUE_FOR_ONE_MARKER;
                return;
            }

            foreach (var data in _pinScreenCoordinates)
            {
                double x = data.X;
                double y = data.Y;

                minX = Math.Min(minX, x);
                maxX = Math.Max(maxX, x);
                minY = Math.Min(minY, y);
                maxY = Math.Max(maxY, y);
            }

            double horizontalDistance = maxX - minX;
            double verticalDistance = maxY - minY;

            _distance = Math.Sqrt(Math.Pow(horizontalDistance, 2) + Math.Pow(verticalDistance, 2));

            _mapView.Map.ZoomLevel = AdjustZoom();
        }

        /// <summary>
        /// Adjusts the zoom value of the map view so that the specified distance between markers
        /// fits on the device screen.
        /// </summary>
        /// <returns>Zoom value.</returns>
        private int AdjustZoom()
        {
            int zoom = DEFAULT_ZOOM_VALUE;
            double distance = _distance;

            if (distance >= MAX_DISTANCE_BETWEEN_MARKERS)
            {
                return ZOOM_VALUE_FOR_ONE_MARKER;
            }

            while (distance < MAX_DISTANCE_BETWEEN_MARKERS / 2)
            {
                distance *= 2;
                zoom += 1;
            }

            return zoom;
        }

        /// <summary>
        /// Handles "CoordinatesReceived" event of the geocoding service.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ServiceOnCoordinatesReceived(object sender, IGeocodingCoordinatesArgs e)
        {
            _recentlyReceivedCoordinates = new Geocoordinates(e.Latitude, e.Longitude);

            _mapView.Map.Add(new Pin(_recentlyReceivedCoordinates, ResourcePath.GetPath("pin.png")));

            ElmSharp.Point screen = _mapView.Map.GeolocationToScreen(_recentlyReceivedCoordinates);
            _pinScreenCoordinates.Add(new Point(screen.X, screen.Y));
        }

        /// <summary>
        /// Handles "OnMapViewSet" event of the map view.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMapViewSet(object sender, EventArgs e)
        {
            _iGeocodingService.ParseLastGeocodeResponse();
        }

        /// <summary>
        /// Overrides OnDisappearing method.
        /// Unregisters event handlers.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _iGeocodingService.CoordinatesReceived -= ServiceOnCoordinatesReceived;
            _iGeocodingService.CenterPointCalculated -= ServiceOnCenterPointCalculated;
            _mapView.MapViewSet -= OnMapViewSet;
        }

        #endregion
    }
}
