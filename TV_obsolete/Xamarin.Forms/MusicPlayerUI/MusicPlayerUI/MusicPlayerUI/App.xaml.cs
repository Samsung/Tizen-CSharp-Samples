using Xamarin.Forms;

namespace MusicPlayerUI
{
    /// <summary>
    /// Application class
    /// </summary>
    public partial class App : Application
    {
        bool _isMenuOpen = false;

        public bool IsMenuOpen
        {
            get
            {
                return _isMenuOpen;
            }
            set
            {
                _isMenuOpen = value;
                OnPropertyChanged(nameof(IsMenuOpen));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public App()
        {
            InitializeComponent();
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