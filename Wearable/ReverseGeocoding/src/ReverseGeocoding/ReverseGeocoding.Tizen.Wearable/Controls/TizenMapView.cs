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
using Tizen.Maps;
using Xamarin.Forms;

namespace ReverseGeocoding.Tizen.Wearable.Controls
{
    /// <summary>
    /// TizenMapView control class.
    /// Provides base functionality of the TizenMapView.
    /// </summary>
    public class TizenMapView : View
    {
        #region fields

        /// <summary>
        /// Backing field of the Map property.
        /// </summary>
        private MapView _map;

        #endregion

        #region properties

        /// <summary>
        /// Notifies about map view initialization success.
        /// </summary>
        public event EventHandler MapViewSet;

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
        public MapView Map
        {
            get => _map;
            set
            {
                _map = value;
                MapViewSet?.Invoke(this, null);
            }
        }

        #endregion
    }
}
