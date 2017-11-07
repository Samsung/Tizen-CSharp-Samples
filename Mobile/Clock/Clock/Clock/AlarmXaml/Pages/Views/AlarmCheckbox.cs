using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Clock.Pages.Views
{
    public class AlarmCheckbox : View
    {

        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create("IsChecked", typeof(bool), typeof(Switch), false, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ((AlarmCheckbox)bindable).Checked?.Invoke(bindable, new CheckedEventArgs((bool)newValue));
        },
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty IsFavoriteStyleProperty = BindableProperty.Create("IsFavoriteStyle", typeof(bool), typeof(AlarmCheckbox), false);

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public AlarmCheckbox()
        {
        }

        public event EventHandler<CheckedEventArgs> Checked;
    }
}
