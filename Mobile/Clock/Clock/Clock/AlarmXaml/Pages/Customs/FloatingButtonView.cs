using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Clock.Pages.Customs
{
    public class FloatingButtonView : View
    {
        public static readonly BindableProperty ImageSourceProperty  = BindableProperty.Create(nameof(Image), typeof(string), typeof(FloatingButtonView), false);
        public static readonly BindableProperty IsShowingProperty = BindableProperty.Create(nameof(IsShowing), typeof(bool), typeof(FloatingButtonView), false);

        public bool IsShowing
        {
            get { return (bool)GetValue(IsShowingProperty); }
            set { SetValue(IsShowingProperty, value); }
        }

        public string Image
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public event EventHandler Clicked;
        
        public FloatingButtonView()
        {
        }
    }
}
