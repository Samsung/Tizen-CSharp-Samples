using Xamarin.Forms;

namespace MusicPlayerUI
{
    /// <summary>
    /// The main page of the application
    /// </summary>
    public partial class MainPage : FlyoutPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            if (IsPresented == false)
            {
                return false;
            }

            IsPresented = false;
            return true;
        }
    }
}