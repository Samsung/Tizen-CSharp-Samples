using Xamarin.Forms;
using IMESample.ViewModels;

namespace IMESample.Views
{
    /// <summary>
    /// Content page of keyboard layout.
    /// </summary>
    public partial class IME_KEYBOARD_LAYOUT_QWERTY_PORT : ContentPage
    {
        /// <summary>
        /// Keyboard layout constructor.
        /// </summary>
        public IME_KEYBOARD_LAYOUT_QWERTY_PORT()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = MainPageViewModel.Instance;
        }
    }
}