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
using ElmSharp.Wearable;
using FindPlace.Interfaces;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Point = ElmSharp.Point;
using FindPlace.Model;
using System.Windows.Input;

namespace FindPlace.Tizen.Wearable.Views
{
    /// <summary>
    /// Page of the application displaying the map.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : CirclePage
    {
        #region fields

        /// <summary>
        /// Top left point of searching area.
        /// </summary>
        private readonly Point _topLeft;

        /// <summary>
        /// Bottom right point of searching area.
        /// </summary>
        private readonly Point _bottomRight;

        #endregion

        #region properties

        /// <summary>
        /// Bindable property definition for location.
        /// </summary>
        public static readonly BindableProperty LocationProperty = BindableProperty.Create(
            nameof(Location),
            typeof(Geocoordinates),
            typeof(MapPage),
            null);

        /// <summary>
        /// Location received from location service.
        /// </summary>
        public Geocoordinates Location
        {
            get => (Geocoordinates)GetValue(LocationProperty);
            set => SetValue(LocationProperty, value);
        }

        /// <summary>
        /// Bindable property definition for area.
        /// </summary>
        public static readonly BindableProperty AreaProperty = BindableProperty.Create(
            nameof(Area),
            typeof(Area),
            typeof(MapPage),
            null);

        /// <summary>
        /// Area of the search.
        /// </summary>
        public Area Area
        {
            get => (Area)GetValue(AreaProperty);
            set => SetValue(AreaProperty, value);
        }

        /// <summary>
        /// Bindable property definition for ConfirmAreaSelectionCommand property.
        /// </summary>
        public static readonly BindableProperty ConfirmAreaSelectionCommandProperty = BindableProperty.Create(
            nameof(ConfirmAreaSelectionCommand),
            typeof(ICommand),
            typeof(MapPage),
            null);

        /// <summary>
        /// Confirms area selection.
        /// </summary>
        public ICommand ConfirmAreaSelectionCommand
        {
            get => (ICommand)GetValue(ConfirmAreaSelectionCommandProperty);
            set => SetValue(ConfirmAreaSelectionCommandProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public MapPage()
        {
            _topLeft = new Point() { X = 50, Y = 86 };
            _bottomRight = new Point() { X = 309, Y = 260 };

            ConfirmAreaSelectionCommand = new Command(ExecuteConfirmAreaSelection);

            InitializeComponent();
        }

        /// <summary>
        /// Handles execution of ConfirmAreaSelectionCommand.
        /// </summary>
        private void ExecuteConfirmAreaSelection()
        {
            Area = new Area(
                MapView.ScreenToPointGeocoordinates(_topLeft),
                MapView.ScreenToPointGeocoordinates(_bottomRight));
        }

        #endregion
    }
}
