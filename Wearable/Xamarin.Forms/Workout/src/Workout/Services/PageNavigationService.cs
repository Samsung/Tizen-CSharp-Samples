using System;
using Workout.Views;
using Xamarin.Forms;

namespace Workout.Services
{
    /// <summary>
    /// Provides navigation between pages across application.
    /// This is singleton. Instance accessible via <see cref = Instance></cref> property.
    /// </summary>
    public sealed class PageNavigationService
    {
        #region fields

        /// <summary>
        /// Backing field of Instance property.
        /// </summary>
        private static PageNavigationService _instance;

        #endregion

        #region properties

        /// <summary>
        /// PageNavigationService instance accessor.
        /// </summary>
        public static PageNavigationService Instance
        {
            get => _instance ?? (_instance = new PageNavigationService());
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes PageNavigationService class instance.
        /// </summary>
        private PageNavigationService()
        {
        }

        /// <summary>
        /// Creates welcome page.
        /// </summary>
        public void GoToWelcomePage()
        {
            Application.Current.MainPage = new WelcomePageView();
        }

        /// <summary>
        /// Creates privilege denied page.
        /// </summary>
        public void GoToPrivilegeDeniedPage()
        {
            Application.Current.MainPage = new PrivilegeDeniedPageView();
        }

        /// <summary>
        /// Creates home page and sets it as active.
        /// </summary>
        public void GoToHomePage()
        {
            ContentPage homePageView = new HomePageView();

            Application.Current.MainPage = new NavigationPage(homePageView);
        }

        /// <summary>
        /// Creates workout countdown page and navigates to it.
        /// </summary>
        public void GoToWorkoutCountdownPage()
        {
            Application.Current.MainPage?.Navigation.PushAsync(new Views.Workout.CountdownPageView());
        }

        /// <summary>
        /// Creates workout main page and navigates to it.
        /// </summary>
        public void GoToWorkoutMainPage()
        {
            Application.Current.MainPage?.Navigation.PushAsync(new Views.Workout.MainView());
        }

        /// <summary>
        /// Creates workout pause page and navigates to it.
        /// </summary>
        public void GoToWorkoutPausePage()
        {
            Application.Current.MainPage?.Navigation.PushAsync(new Views.Workout.PausePageView());
        }

        /// <summary>
        /// Creates workout completed page and navigates to it.
        /// </summary>
        public void GoToWorkoutCompletedPage()
        {
            Application.Current.MainPage?.Navigation.PushAsync(new Views.Workout.CompletedPageView());
        }

        /// <summary>
        /// Creates workout details page and navigates to it.
        /// </summary>
        public void GoToWorkoutDetailsPage()
        {
            Application.Current.MainPage?.Navigation.PushAsync(new Views.Workout.DetailsPageView());
        }

        /// <summary>
        /// Navigates to previous page and removes current page from navigation stack.
        /// </summary>
        public void GoToPreviousPage()
        {
            Application.Current.MainPage?.Navigation.PopAsync();
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        public void CloseApplication()
        {
            try
            {
                global::Tizen.Applications.Application.Current.Exit();
            }
            catch (Exception)
            {
                global::Tizen.Log.Error("Workout", "Unable to close the application");
            }
        }

        #endregion
    }
}
