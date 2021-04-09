
using Xamarin.Forms;
using Calendar.Views;

namespace Calendar
{
    /// <summary>
    /// Sample Account Application Class
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            /// Locd main page
            MainPage = new NavigationPage(new MonthPage());
        }

        /// <summary>
        /// On start method
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// On sleep method
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// On resume method
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
