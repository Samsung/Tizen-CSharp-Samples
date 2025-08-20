using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Workout.Views
{
    /// <summary>
    /// Welcome page view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePageView : ContentPage
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
