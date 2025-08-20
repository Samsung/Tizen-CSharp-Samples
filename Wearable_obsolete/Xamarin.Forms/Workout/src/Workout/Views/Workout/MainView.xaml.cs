using System;
using Workout.ViewModels.Workout;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Workout.Views.Workout
{
    /// <summary>
    /// Workout main view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        #region fields

        /// <summary>
        /// An instance of workout view model.
        /// </summary>
        private MainViewModel _viewModel;

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public MainView()
        {
            InitializeComponent();

            _viewModel = BindingContext as MainViewModel;
        }

        /// <summary>
        /// Overrides method called when the page appears.
        /// Starts workout.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.StartWorkout();
        }

        /// <summary>
        /// Overrides method handling hardware "back" button press.
        /// Pauses workout.
        /// </summary>
        protected override bool OnBackButtonPressed()
        {
            _viewModel.PauseWorkout();

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
