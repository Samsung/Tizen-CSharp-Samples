using System.Windows.Input;
using TextReader.Views;
using Xamarin.Forms;

namespace TextReader.ViewModels
{
    /// <summary>
    /// Provides commands and methods responsible for 'how-to' page view model state.
    /// </summary>
    public class HowToViewModel
    {
        #region properties

        /// <summary>
        /// Navigates to text reader page.
        /// </summary>
        public ICommand NavigateToTextReaderCommand { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes the view model for 'how-to' page.
        /// </summary>
        public HowToViewModel()
        {
            NavigateToTextReaderCommand = new Command(ExecuteNavigateToTextReaderCommand);
        }

        /// <summary>
        /// Handles execution of NavigateToTextReaderCommand.
        /// Navigates to text reader page.
        /// </summary>
        private void ExecuteNavigateToTextReaderCommand()
        {
            DependencyService.Get<IPageNavigation>().NavigateToTextReader();
        }

        #endregion
    }
}
