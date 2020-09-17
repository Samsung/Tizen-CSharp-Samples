using System.Threading.Tasks;
using Workout.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Workout.Views.Workout
{
    /// <summary>
    /// Workout countdown page view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountdownPageView : ContentPage
    {
        #region fields

        /// <summary>
        /// Page visibility indicator.
        /// </summary>
        private bool _isVisible;

        /// <summary>
        /// Instance of LocationService.
        /// </summary>
        private LocationService _locationService;

        #endregion

        #region methods

        /// <summary>
        /// Waits and depending on the GPS location state navigates to workout main page or previous page if countdown is still visible.
        /// </summary>
        private async Task GoToNextView()
        {
            await Task.Delay(5000).ConfigureAwait(true);

            if (_isVisible)
            {
                if (_locationService.IsGPSLocationEnabled())
                {
                    PageNavigationService.Instance.GoToWorkoutMainPage();
                }
                else
                {
                    PageNavigationService.Instance.GoToPreviousPage();
                }
            }
        }

        /// <summary>
        /// Initializes class instance.
        /// </summary>

        public CountdownPageView()
        {
            InitializeComponent();

            _locationService = LocationService.Instance;
        }

        /// <summary>
        /// Overrides method called when the page appears.
        /// </summary>
        protected override async void OnAppearing()
        {
            _isVisible = true;
            base.OnAppearing();

            await GoToNextView().ConfigureAwait(false);
        }

        /// <summary>
        /// Overrides method called when the page disappears.
        /// </summary>
        protected override void OnDisappearing()
        {
            _isVisible = false;
            base.OnDisappearing();
        }

        #endregion
    }
}
