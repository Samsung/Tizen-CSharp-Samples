using Xamarin.Forms;
using IMESample.ViewModels;

namespace IMESample.Views
{
    public partial class IME_KEYBOARD_LAYOUT_QWERTY_PORT : ContentPage
    {
        public IME_KEYBOARD_LAYOUT_QWERTY_PORT()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = MainPageViewModel.Instance;
        }
    }
}