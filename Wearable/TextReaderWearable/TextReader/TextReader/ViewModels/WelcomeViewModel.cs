using System.Windows.Input;
using TextReader.Views;
using Xamarin.Forms;

namespace TextReader.ViewModels
{
    /// <summary>
    /// Provides commands and methods responsible for welcome page view model state.
    /// </summary>
    public class WelcomeViewModel
    {
        #region properties

        /// <summary>
        /// Navigates to 'how-to' page.
        /// </summary>
        public ICommand StartApplicationCommand { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes the view model for welcome page.
        /// </summary>
        public WelcomeViewModel()
        {
            StartApplicationCommand = new Command(ExecuteStartApplicationCommand);
        }

        /// <summary>
        /// Handles execution of StartApplicationCommand.
        /// Navigates to 'how-to' page.
        /// </summary>
        private void ExecuteStartApplicationCommand()
        {
            DependencyService.Get<IPageNavigation>().NavigateToHowToPage();
        }

        #endregion
    }
}
