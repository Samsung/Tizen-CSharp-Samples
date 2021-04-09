using System;
using Workout.ViewModels.Workout;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Workout.Views.Workout
{
    /// <summary>
    /// Workout details page view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPageView : ContentPage
    {
        #region fields

        /// <summary>
        /// An instance of workout view model.
        /// </summary>
        private DetailsPageViewModel _viewModel;

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public DetailsPageView()
        {
            InitializeComponent();

            _viewModel = BindingContext as DetailsPageViewModel;

            foreach (BindableObject item in listView.ItemsSource)
            {
                item.BindingContext = _viewModel;
            }
        }

        /// <summary>
        /// Overrides method handling hardware "back" button press.
        /// Finishes workout.
        /// Navigates to the home page.
        /// </summary>
        protected override bool OnBackButtonPressed()
        {
            _viewModel.Finish();

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
