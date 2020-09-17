using System.Threading.Tasks;
using Workout.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Workout.Views.Workout
{
    /// <summary>
    /// Workout completed page view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompletedPageView : ContentPage
    {
        #region methods

        /// <summary>
        /// Navigates to workout details page after delay.
        /// </summary>
        private async Task GoToWorkoutDetails()
        {
            await Task.Delay(3000).ConfigureAwait(true);

            PageNavigationService.Instance.GoToWorkoutDetailsPage();
        }

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public CompletedPageView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Overrides method called when the page appears.
        /// Finishes workout.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await GoToWorkoutDetails().ConfigureAwait(false);
        }

        /// <summary>
        /// Overrides method handling hardware "back" button press.
        /// </summary>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        #endregion
    }
}
