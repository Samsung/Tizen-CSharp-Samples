using ImageReader.Pages;
using Xamarin.Forms;

namespace ImageReader
{
    public class App : Application
    {
        public App()
        {
            MainListViewPage main = new MainListViewPage();
            MainPage = new NavigationPage(main);
        }

        /// <summary>
        /// Called when the application starts.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Called when the application enters the sleeping state.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Called when the application resumes from a sleeping state.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
