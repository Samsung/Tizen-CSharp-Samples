using Xamarin.Forms;
using IMESample.ViewModels;

namespace IMESample.Views
{
    public partial class IME_KEYBOARD_LAYOUT_QWERTY_LAND : ContentPage
    {
        public IME_KEYBOARD_LAYOUT_QWERTY_LAND()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = MainPageViewModel.Instance;
        }
    }
}