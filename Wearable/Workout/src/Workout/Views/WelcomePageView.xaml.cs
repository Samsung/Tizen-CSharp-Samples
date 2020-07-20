using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace Workout.Views
{
    /// <summary>
    /// Welcome page view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePageView : CirclePage
    {
        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public WelcomePageView()
        {
            InitializeComponent();
        }

        #endregion
    }
}