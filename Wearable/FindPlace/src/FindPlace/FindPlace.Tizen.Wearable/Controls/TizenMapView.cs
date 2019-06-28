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
using System.Runtime.CompilerServices;
using FindPlace.Interfaces;
using Tizen.Maps;
using Xamarin.Forms;

namespace FindPlace.Tizen.Wearable.Controls
{
    /// <summary>
    /// TizenMapView control class.
    /// Provides base functionality of the TizenMapView.
    /// </summary>
    public class TizenMapView : View
    {
        #region fields

        /// <summary>
        /// Default zoom level.
        /// </summary>
        private const int DefaultZoomLevel = 14;

        /// <summary>
        /// Device screen size.
        /// </summary>
        private const int ScreenSize = 360;

        #endregion

        #region properties

        /// <summary>
        /// Control width.
        /// </summary>
        public int ControlWidth { get; set; }

        /// <summary>
        /// Control height.
        /// </summary>
        public int ControlHeight { get; set; }

        /// <summary>
        /// An instance of the Tizen MapService class.
        /// </summary>
        public MapService Service { get; set; }

        /// <summary>
        /// An instance of the Tizen MapView class.
        /// </summary>
        public MapView Map { get; set; }

        /// <summary>
        /// Bindable property definition for zoom level.
        /// </summary>
        public static readonly BindableProperty ZoomLevelProperty = BindableProperty.Create(
            nameof(ZoomLevel),
            typeof(int),
            typeof(TizenMapView),
            DefaultZoomLevel);

        /// <summary>
        /// Zoom level.
        /// </summary>
        public int ZoomLevel
        {
            get => (int)GetValue(ZoomLevelProperty);
            set
            {
                SetValue(ZoomLevelProperty, value);
                if (Map != null)
                {
                    Map.ZoomLevel = value;
                }
            }
        }

        /// <summary>
        /// Bindable property definition for center map position.
        /// </summary>
        public static readonly BindableProperty CenterProperty = BindableProperty.Create(
            nameof(Center),
            typeof(Model.Geocoordinates),
            typeof(TizenMapView),
            null);

        /// <summary>
        /// Map center position.
        /// </summary>
        public Model.Geocoordinates Center
        {
            get => (Model.Geocoordinates)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class.
        /// </summary>
        public TizenMapView()
        {
            Service = DependencyService.Get<IMapServiceProvider>().GetService();
            ControlHeight = ScreenSize;
            ControlWidth = ScreenSize;
        }

        /// <summary>
        /// Changes the screen coordinates to point geocoordinates.
        /// </summary>
        /// <param name="screenCoordinates">Screen coordinates.</param>
        /// <returns>Returns point geocoordinates.</returns>
        public Model.Geocoordinates  ScreenToPointGeocoordinates(ElmSharp.Point screenCoordinates)
        {
            var geolocation = Map.ScreenToGeolocation(screenCoordinates);
            return new Model.Geocoordinates(geolocation.Latitude, geolocation.Longitude);
        }

        /// <summary>
        /// Overridden OnPropertyChanged method which refreshes properties values.
        /// </summary>
        /// <param name="propertyName">Name of property which changes.</param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(ZoomLevel) && Map != null)
            {
                Map.ZoomLevel = ZoomLevel;
            }

            base.OnPropertyChanged(propertyName);
        }

        #endregion
    }
}
