using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace PhotoWatch
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingView : ContentView
    {
        public SettingView ()
        {
            InitializeComponent();
        }

        public IRotaryFocusable RotaryFocusObject => BgList;
    }
}