using System;
using Tizen.Wearable.CircularUI.Forms;
using Workout.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Workout.Views.Workout
{
    /// <summary>
    /// Workout pause page view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PausePageView : ContentPage
    {
        #region fields

        /// <summary>
        /// Toast message.
        /// </summary>
        private const string _toastMessage = "Please, make GPS enabled before you resume the workout.";

        /// <summary>
        /// Toast duration in milliseconds.
        /// </summary>
        private const int _toastDuration = 1500;

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public PausePageView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Overrides method handling hardware "back" button press.
        /// Depending on GPS location state, navigates to workout main page or displays toast popup.
        /// </summary>
        protected override bool OnBackButtonPressed()
        {
            if (LocationService.Instance.IsGPSLocationEnabled())
            {
                PageNavigationService.Instance.GoToWorkoutMainPage();
            }
            else
            {
                Toast.DisplayText(_toastMessage, _toastDuration);
            }

            return true;
        }

        /// <summary>
        /// Overrides method called when the page disappears.
        /// Disposes binding context.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IDisposable disposableBindingContext)
            {
                disposableBindingContext.Dispose();
                BindingContext = null;
            }
        }

        #endregion
    }
}
