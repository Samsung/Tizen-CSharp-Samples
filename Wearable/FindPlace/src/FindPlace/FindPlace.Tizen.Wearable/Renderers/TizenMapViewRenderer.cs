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
using FindPlace.Tizen.Wearable.Controls;
using FindPlace.Tizen.Wearable.Renderers;
using Tizen.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(TizenMapView), typeof(TizenMapViewRenderer))]
namespace FindPlace.Tizen.Wearable.Renderers
{
    /// <summary>
    /// Tizen map view renderer.
    /// </summary>
    public class TizenMapViewRenderer : ViewRenderer<TizenMapView, MapView>
    {
        #region fields

        /// <summary>
        /// MapView class instance used for interacting with the map view.
        /// </summary>
        private MapView _mapView;

        #endregion

        #region methods

        /// <summary>
        /// Overridden OnElementChanged method which initializes new control as a Tizen map view.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<TizenMapView> e)
        {
            if (Control == null)
            {
                _mapView = new MapView(Forms.NativeParent, Element.Service);
                SetNativeControl(_mapView);

                Control.Resize(Element.ControlHeight, Element.ControlWidth);
                Control.MapType = MapTypes.Normal;
                Control.ZoomLevel = Element.ZoomLevel;
                Control.Center = new Geocoordinates(Element.Center.Latitude, Element.Center.Longitude);
                Control.Show();

                Element.Map = Control;
            }

            base.OnElementChanged(e);
        }

        #endregion
    }
}