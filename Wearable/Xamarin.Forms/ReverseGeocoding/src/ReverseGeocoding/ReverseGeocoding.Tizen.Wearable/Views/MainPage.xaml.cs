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
using ReverseGeocoding.Tizen.Wearable.Services;
using Tizen.Maps;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Xaml;
using ReverseGeocoding.Common;
using ReverseGeocoding.Tizen.Wearable.Controls;

namespace ReverseGeocoding.Tizen.Wearable.Views
{
    /// <summary>
    /// Main page of the application displaying the map.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        #region fields

        /// <summary>
        /// Initial map latitude.
        /// </summary>
        private const double MAP_CENTER_LATITUDE = 52.229676;

        /// <summary>
        /// Initial map longitude.
        /// </summary>
        private const double MAP_CENTER_LONGITUDE = 21.012229;

        /// <summary>
        /// Default zoom level.
        /// </summary>
        private const int DEFAULT_ZOOM_LEVEL = 14;

        /// <summary>
        /// Device screen size.
        /// </summary>
        private const int SCREEN_SIZE = 360;

        /// <summary>
        /// Represents the pin on the map.
        /// </summary>
        private Pin _pin;

        /// <summary>
        /// MapService class instance used for getting the map service data.
        /// </summary>
        private MapService _mapService;

        /// <summary>
        /// TizenMapView class instance used for interacting with the map view.
        /// </summary>
        private TizenMapView _mapView;

        /// <summary>
        /// Indicates if map was displayed.
        /// </summary>
        private bool _mapAppeared;

        #endregion

        #region properties

        /// <summary>
        /// Bindable property definition for pin selected.
        /// </summary>
        public static readonly BindableProperty PinPlacedProperty = BindableProperty.Create(
                                                    nameof(PinPlaced),
                                                    typeof(bool),
                                                    typeof(MainPage),
                                                    false);

        /// <summary>
        /// Gets or sets information if pin is placed.
        /// </summary>
        public bool PinPlaced
        {
            get => (bool)GetValue(PinPlacedProperty);
            set => SetValue(PinPlacedProperty, value);
        }

        /// <summary>
        /// Bindable property definition for point coordinates.
        /// </summary>
        public static readonly BindableProperty PointProperty = BindableProperty.Create(
                                                    nameof(Point),
                                                    typeof(PointGeocoordinates),
                                                    typeof(MainPage),
                                                    null);

        /// <summary>
        /// Gets or sets information about current coordinates of the pin.
        /// </summary>
        public PointGeocoordinates Point
        {
            get => (PointGeocoordinates)GetValue(PointProperty);
            set => SetValue(PointProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            _mapService = DependencyService.Get<IMapServiceProvider>().GetService();

            PinPlaced = false;

            _mapAppeared = false;

            _mapView = new TizenMapView
            {
                Service = _mapService,
                ControlWidth = SCREEN_SIZE,
                ControlHeight = SCREEN_SIZE
            };

            Content = _mapView;
        }

        /// <summary>
        /// Handles the long pressed event of the MapView object.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMapLongPressed(object sender, MapGestureEventArgs e)
        {
            if (_pin == null)
            {
                _pin = new Pin(new Geocoordinates(MAP_CENTER_LATITUDE, MAP_CENTER_LONGITUDE),
                                ResourcePath.GetPath("images/indicator.png"));
                _mapView.Map.Add(_pin);
                Point = new PointGeocoordinates(MAP_CENTER_LATITUDE, MAP_CENTER_LONGITUDE);
            }

            PinPlaced = true;
            _pin.Coordinates = e.Geocoordinates;
            Point.Latitude = e.Geocoordinates.Latitude;
            Point.Longitude = e.Geocoordinates.Longitude;
        }

        /// <summary>
        /// Zooms in/out map on rotary event.
        /// </summary>
        /// <param name="args">Rotary arguments.</param>
        private void OnRotaryChange(ElmSharp.Wearable.RotaryEventArgs args)
        {
            if (args.IsClockwise)
            {
                _mapView.Map.ZoomLevel++;
            }
            else
            {
                _mapView.Map.ZoomLevel--;
            }
        }

        /// <summary>
        /// Overrides OnAppearing method.
        /// Registers event handlers and sets map properties.
        /// </summary>
        protected override void OnAppearing()
        {
            if (!_mapAppeared)
            {
                _mapView.Map.MapType = MapTypes.Normal;
                _mapView.Map.ZoomLevel = DEFAULT_ZOOM_LEVEL;
                _mapView.Map.Center = new Geocoordinates(MAP_CENTER_LATITUDE, MAP_CENTER_LONGITUDE);
                _mapAppeared = true;
            }


            ElmSharp.Wearable.RotaryEventManager.Rotated += OnRotaryChange;
            _mapView.Map.LongPressed += OnMapLongPressed;
        }

        /// <summary>
        /// Overrides OnDisappearing method.
        /// Unregisters event handlers.
        /// </summary>
        protected override void OnDisappearing()
        {
            ElmSharp.Wearable.RotaryEventManager.Rotated -= OnRotaryChange;
            _mapView.Map.LongPressed -= OnMapLongPressed;
        }

        #endregion
    }
}