using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace IMESample
{
    /// <summary>
    /// Content page of portrait keyboard
    /// </summary>
    public partial class IME_KEYBOARD_LAYOUT_QWERTY_PORT : View
    {
        /// <summary>
        /// Portrait keyboard constructor
        /// </summary>
        public IME_KEYBOARD_LAYOUT_QWERTY_PORT()
        {
            InitializeComponent();
            BindingContext = MainViewModel.Instance;
        }
    }
}
