using Xamarin.Forms;

namespace CalendarComponent
{
    public class App : Application
    {
        #region methods

        /// <summary>
        /// Application constructor.
        /// Sets active page using Dependency Injection.
        /// </summary>
        public App()
        {
            MainPage = new NavigationPage(DependencyService.Get<Interfaces.IAppPage>() as Page);
        }

        #endregion
    }
}