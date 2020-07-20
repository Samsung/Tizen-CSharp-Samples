using System.Windows.Input;
using Workout.Services;
using Xamarin.Forms;

namespace Workout.ViewModels
{
    /// <summary>
    /// Privilege denied page view model.
    /// </summary>
    public class PrivilegeDeniedPageViewModel
    {
        #region properties

        /// <summary>
        /// Closes application.
        /// </summary>
        public ICommand CloseApplicationCommand { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes commands.
        /// </summary>
        private void InitCommands()
        {
            CloseApplicationCommand = new Command(ExecuteCloseApplication);
        }

        /// <summary>
        /// Handles execution of "CloseApplicationCommand".
        /// </summary>
        private void ExecuteCloseApplication()
        {
            PageNavigationService.Instance.CloseApplication();
        }

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public PrivilegeDeniedPageViewModel()
        {
            InitCommands();
        }

        #endregion
    }
}
