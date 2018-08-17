//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using System;

namespace MapsView
{
    public class MapPage : CirclePage, IRotaryEventReceiver
    {
        /// <summary>
        /// Reference to Xamarin.Forms.Maps.Map object that occupies whole screen
        /// </summary>
        private Map map;
        /// <summary>
        /// Current zoom level - used for zoom-in and zoom-out operations via bezel rotation
        /// </summary>
        private double zoomLevel = Config.STARTING_ZOOM_LEVEL;
        /// <summary>
        /// When was the last zoom-in or zoom-out that we have handled. 
        /// This is in place to ensure it does not happen to fast.
        /// We allow one zoom action every ZOOM_EVERY_X_MILLISECONDS seconds (see Config.cs)
        /// </summary>
        private long lastZoomMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        public MapPage()
        {
            map = new Map()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                MapType = MapType.Satellite
            };
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
            NavigationPage.SetHasNavigationBar(this, false);
            this.RotaryFocusObject = this;
        }

        /// <summary>
        /// Move the map to a MapSpan that is set via page BindingContent
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            MapSpan region = (MapSpan)this.BindingContext;
            map.MoveToRegion(region);
        }

        /// <summary>
        /// IRotaryEventReceiver interface driven bezel rotation handler
        /// Zoom in and out the map every 1 second (to avoid quick zoom in / zoom out)
        /// </summary>
        public void Rotate(RotaryEventArgs args)
        {
            if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastZoomMillis > Config.ZOOM_EVERY_X_MILLISECONDS)
            {
                zoomLevel = args.IsClockwise ? zoomLevel * 0.80 : zoomLevel * 1.20;                
                map.MoveToRegion(MapSpan.FromCenterAndRadius(map.VisibleRegion.Center, Distance.FromKilometers(zoomLevel)));
                lastZoomMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            }
            else
            {
                Tizen.Log.Debug("MapsViewApp", "To early to start another zoom");
            }
        }
    }
}
