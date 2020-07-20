using System.Windows.Input;
using Workout.Services;
using Xamarin.Forms;

namespace Workout.ViewModels
{
    /// <summary>
    /// Welcome view model.
    /// </summary>
    public class WelcomePageViewModel
    {
        #region properties

        /// <summary>
        /// Navigates to home pages.
        /// </summary>
        public ICommand GoToHomeCommand { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes commands.
        /// </summary>
        private void InitCommands()
        {
            GoToHomeCommand = new Command(ExecuteGoToHome);
        }

        /// <summary>
        /// Handles execution of "GoToHomeCommand".
        /// </summary>
        private void ExecuteGoToHome()
        {
            PageNavigationService.Instance.GoToHomePage();
        }

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public WelcomePageViewModel()
        {
            InitCommands();
        }

        #endregion
    }
}
