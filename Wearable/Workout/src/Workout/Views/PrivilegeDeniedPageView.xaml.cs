using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace Workout.Views
{
    /// <summary>
    /// Privilege denied page view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivilegeDeniedPageView : CirclePage
    {
        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public PrivilegeDeniedPageView()
        {
            InitializeComponent();
        }

        #endregion
    }
}