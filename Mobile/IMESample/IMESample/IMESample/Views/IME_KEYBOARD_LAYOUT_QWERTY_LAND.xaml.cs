using Xamarin.Forms;
using IMESample.ViewModels;

namespace IMESample.Views
{
    /// <summary>
    /// Content page of landscape keyboard.
    /// </summary>
    public partial class IME_KEYBOARD_LAYOUT_QWERTY_LAND : ContentPage
    {
        /// <summary>
        /// Landscape keyboard constructor.
        /// </summary>
        public IME_KEYBOARD_LAYOUT_QWERTY_LAND()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = MainPageViewModel.Instance;
        }
    }
}