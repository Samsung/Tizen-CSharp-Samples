using Xamarin.Forms;

namespace Location
{
    /// <summary>
    ///  The location application
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Initializes a new instance
        /// </summary>
        public App()
        {
            // The root page of your application
            MainPage = new LocationServices.InitializePage();
        }

        /// <summary>
        /// Called when your app starts.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Called when your app sleeps.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Called when your app resumes.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
