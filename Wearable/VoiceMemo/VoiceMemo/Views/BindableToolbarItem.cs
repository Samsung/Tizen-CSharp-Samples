using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace VoiceMemo.Views
{
    /// <summary>
    /// class custom circle toolbar item
    /// Its visibility can be changeable.
    /// </summary>
    public class BindableToolbarItem : CircleToolbarItem
    {
        public static readonly BindableProperty IsVisibleProperty =
            BindableProperty.Create("BindableToolbarItem", typeof(bool), typeof(ToolbarItem),
                true, BindingMode.TwoWay, propertyChanged: OnIsVisibleChanged);

        public BindableToolbarItem()
        {
            InitVisibility();
        }

        /// <summary>
        /// ToolbarItem's visibility
        /// </summary>
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        private void InitVisibility()
        {
            OnIsVisibleChanged(this, false, IsVisible);
        }

        private static void OnIsVisibleChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var item = bindable as BindableToolbarItem;

            if (item != null && item.Parent == null)
            {
                return;
            }

            if (item != null)
            {
                var items = ((ContentPage)item.Parent).ToolbarItems;

                // case : add to toolbar items
                if ((bool)newvalue && !items.Contains(item))
                {
                    Device.BeginInvokeOnMainThread(() => { items.Add(item); });
                }
                // case : remove from toolbar items
                else if (!(bool)newvalue && items.Contains(item))
                {
                    Device.BeginInvokeOnMainThread(() => { items.Remove(item); });
                }
            }
        }
    }
}
