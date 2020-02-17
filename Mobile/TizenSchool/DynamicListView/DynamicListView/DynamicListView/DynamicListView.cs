using Xamarin.Forms;

namespace DynamicListView
{
    public partial class App : Application
    {
        public App()
        {
            MainPage = new Views.ListViewDemoPageView();
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
