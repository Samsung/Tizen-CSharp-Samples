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

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
