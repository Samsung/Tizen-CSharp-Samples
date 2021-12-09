/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

using Maps.Tizen.Mobile.Controls;
using Maps.Tizen.Mobile.Renderers;

using Tizen.Maps;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(MapComponent), typeof(MapRenderer))]
namespace Maps.Tizen.Mobile.Renderers
{
    public class MapRenderer : ViewRenderer<MapComponent, MapView>
    {
        protected override async void OnElementChanged(ElementChangedEventArgs<MapComponent> e)
        {
            if (e.OldElement is object && Control != null)
            {
                Control.Resized -= OnMapViewResized;
            }

            if (e.NewElement is object)
            {
                if (Control is null)
                {
                    var mapService = new MapService(Config.ProviderName, Config.AuthenticationToken);
                    bool consentGiven = await mapService.RequestUserConsent();
                    if (consentGiven)
                    {
                        var control = new MapView(Forms.NativeParent, mapService)
                        {
                            MapType = MapTypes.Normal,
                            ScaleBarEnabled = false,
                            MinimumZoomLevel = Config.MinimumMapZoom,
                            MaximumZoomLevel = Config.MaximumMapZoom,
                            ZoomLevel = Config.InitialMapZoom,
                        };

                        SetNativeControl(control);
                    }

                    mapService?.Dispose();
                }

                if (Control != null)
                {
                    Control.Resized += OnMapViewResized;
                }

                Element.Map = Control;
                Element.InvokeMapInitialized();
            }

            base.OnElementChanged(e);
        }

        private void OnMapViewResized(object sender, EventArgs e)
        {
            Element.NativeSizeChanged();
        }
    }
}
