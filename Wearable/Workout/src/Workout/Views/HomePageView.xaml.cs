using System;
using Tizen.Wearable.CircularUI.Forms;
using Workout.ViewModels;
using Xamarin.Forms.Xaml;

namespace Workout.Views
{
    /// <summary>
    /// Home page view.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageView : IndexPage
    {
        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public HomePageView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Overrides method called when the page appears.
        /// Initializes binding context.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new HomePageViewModel();
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
