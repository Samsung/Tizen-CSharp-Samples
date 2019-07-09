using TextReader.Views;
using Xamarin.Forms;

namespace TextReader
{
    /// <summary>
    /// Application main class.
    /// </summary>
    public class App : Application
    {
        #region methods

        /// <summary>
        /// Initializes application.
        /// </summary>
        public App()
        {
            DependencyService.Get<IPageNavigation>().NavigateToWelcomePage();
        }

        /// <summary>
        /// Publishes a 'sleep' message.
        /// </summary>
        protected override void OnSleep()
        {
            base.OnSleep();

            MessagingCenter.Send<Application>(this, "sleep");
        }

        /// <summary>
        /// Publishes a 'resume' message.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();

            MessagingCenter.Send<Application>(this, "resume");
        }

        #endregion
    }
}
