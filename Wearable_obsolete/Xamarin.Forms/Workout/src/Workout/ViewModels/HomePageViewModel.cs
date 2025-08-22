using System;
using System.Windows.Input;
using Workout.Services;
using Xamarin.Forms;

namespace Workout.ViewModels
{
    /// <summary>
    /// Home view model.
    /// </summary>
    public class HomePageViewModel : BaseViewModel, IDisposable
    {
        #region fields

        /// <summary>
        /// Disposed flag.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Instance of LocationService.
        /// </summary>
        private readonly LocationService _locationService;

        /// <summary>
        /// Location state.
        /// </summary>
        private bool _isLocationDisabled;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets is location enabled flag.
        /// </summary>
        public bool IsLocationDisabled
        {
            get => _isLocationDisabled;
            set => SetProperty(ref _isLocationDisabled, value);
        }

        /// <summary>
        /// Navigates to workout countdown page.
        /// </summary>
        public ICommand ChangePage { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Handles "SettingChanged" event of LocationService.
        /// Updates location state and optionally starts location service.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="locationState">Location state.</param>
        private void OnLocationServiceSettingChanged(object sender, bool locationState)
        {
            IsLocationDisabled = !locationState;

            if (!_isLocationDisabled)
            {
                _locationService.Start();
            }
        }

        /// <summary>
        /// Sets up events listeners.
        /// </summary>
        private void AddEventListeners()
        {
            _locationService.SettingChanged += OnLocationServiceSettingChanged;
        }

        /// <summary>
        /// Removes events listeners.
        /// </summary>
        private void RemoveEventListeners()
        {
            _locationService.SettingChanged -= OnLocationServiceSettingChanged;
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        private void InitCommands()
        {
            ChangePage = new Command<int>(ExecuteGoToPage);
        }

        /// <summary>
        /// Handles execution of "ChangePage".
        /// <param name="index">Page index.</param>
        /// </summary>
        private void ExecuteGoToPage(int index)
        {
            if (index == 1 && !_isLocationDisabled)
            {
                PageNavigationService.Instance.GoToWorkoutCountdownPage();
            }
        }

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public HomePageViewModel()
        {
            _locationService = LocationService.Instance;

            IsLocationDisabled = !_locationService.IsGPSLocationEnabled();

            InitCommands();
            AddEventListeners();

            if (!_isLocationDisabled)
            {
                _locationService.Start();
            }
        }

        /// <summary>
        /// Releases all resources currently used by this instance.
        /// </summary>
        /// <param name="disposing">
        /// True if managed resources should be disposed, false otherwise.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            RemoveEventListeners();
            _disposed = true;
        }

        /// <summary>
        /// Destroys the current object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Destroys the HomePageViewModel object.
        /// </summary>
        ~HomePageViewModel()
        {
            Dispose(false);
        }

        #endregion
    }
}
