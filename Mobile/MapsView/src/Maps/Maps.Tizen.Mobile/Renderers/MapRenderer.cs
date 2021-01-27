
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
            if (e.OldElement is object)
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
                }

                Control.Resized += OnMapViewResized;
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
