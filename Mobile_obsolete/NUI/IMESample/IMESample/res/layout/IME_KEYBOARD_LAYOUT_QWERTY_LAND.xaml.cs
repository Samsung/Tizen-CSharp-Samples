using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace IMESample
{
    /// <summary>
    /// Content page of landscape keyboard
    /// </summary>
    public partial class IME_KEYBOARD_LAYOUT_QWERTY_LAND : View
    {
        /// <summary>
        /// Landscape keyboard constructor
        /// </summary>
        public IME_KEYBOARD_LAYOUT_QWERTY_LAND()
        {
            InitializeComponent();
            BindingContext = MainViewModel.Instance;
        }
    }
}
