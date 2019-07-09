using TextReader.Tizen.Wearable.Views;
using TextReader.Views;
using Xamarin.Forms;

[assembly: Dependency(typeof(PageNavigation))]

namespace TextReader.Tizen.Wearable.Views
{
    /// <summary>
    /// Page navigation implementation for Tizen wearable profile.
    /// </summary>
    public class PageNavigation : IPageNavigation
    {
        #region methods

        /// <summary>
        /// Navigates to the 'how-to' page.
        /// </summary>
        public void NavigateToHowToPage()
        {
            Application.Current.MainPage = new NavigationPage(new HowToPage());
        }

        /// <summary>
        /// Navigates to the text reader page.
        /// </summary>
        public void NavigateToTextReader()
        {
            Application.Current.MainPage?.Navigation.PushAsync(new TextReaderPage());
        }

        /// <summary>
        /// Navigates to the welcome page.
        /// </summary>
        public void NavigateToWelcomePage()
        {
            Application.Current.MainPage = new WelcomePage();
        }

        #endregion
    }
}
