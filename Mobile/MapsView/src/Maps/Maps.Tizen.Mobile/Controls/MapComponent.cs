using System;

using Tizen.Maps;

using Xamarin.Forms;

namespace Maps.Tizen.Mobile.Controls
{
    public class MapComponent : View
    {
        public static readonly BindableProperty MapProperty = BindableProperty.Create(
            propertyName: nameof(Map),
            returnType: typeof(MapView),
            declaringType: typeof(MapComponent),
            defaultValue: default);

        public event EventHandler MapInitialized;

        public MapView Map
        {
            get => (MapView)GetValue(MapProperty);
            set => SetValue(MapProperty, value);
        }

        public void InvokeMapInitialized()
        {
            Map.Resize((int)Width, (int)Height);
            Map.Show();
            MapInitialized?.Invoke(this, EventArgs.Empty);
        }
    }
}
