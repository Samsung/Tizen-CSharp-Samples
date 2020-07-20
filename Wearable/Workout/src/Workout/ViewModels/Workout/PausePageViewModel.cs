using System;
using System.Windows.Input;
using Workout.Models.Workout;
using Workout.Services;
using Xamarin.Forms;

namespace Workout.ViewModels.Workout
{
    public class PausePageViewModel : BaseViewModel, IDisposable
    {
        #region fields

        /// <summary>
        /// Disposed flag.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Local time.
        /// </summary>
        private DateTime _localTime = DateTime.Now;

        /// <summary>
        /// Elapsed time.
        /// </summary>
        private TimeSpan _elapsedTime = default(TimeSpan);

        /// <summary>
        /// Reference of WorkoutMOdel.
        /// </summary>
        private readonly WorkoutModel _workoutModel;

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
        /// Gets or sets local time.
        /// </summary>
        public DateTime LocalTime
        {
            get => _localTime;
            set => SetProperty(ref _localTime, value);
        }

        /// <summary>
        /// Gets or sets stop watch.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            set => SetProperty(ref _elapsedTime, value);
        }

        /// <summary>
        /// Gets or sets _isLocationDisabled flag.
        /// </summary>
        public bool IsLocationDisabled
        {
            get => _isLocationDisabled;
            set => SetProperty(ref _isLocationDisabled, value);
        }

        /// <summary>
        /// Navigates to workout main page.
        /// </summary>
        public ICommand GoToWorkoutCompletedCommand { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes properties.
        /// </summary>
        private void InitProperties()
        {
            WorkoutData data = _workoutModel.WorkoutData;

            LocalTime = data.LocalTime;
            ElapsedTime = data.ElapsedTime;
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        private void InitCommands()
        {
            GoToWorkoutCompletedCommand = new Command(ExecuteGoToWorkoutCompleted);
        }

        /// <summary>
        /// Handles "Updated" event of the WorkoutModel object.
        /// Updates values of workout properties.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="workoutData">Workout data.</param>
        private void OnWorkoutModelUpdated(object sender, WorkoutUpdatedEventArgs workoutData)
        {
            LocalTime = workoutData.Data.LocalTime;
        }

        /// <summary>
        /// Handles "SettingChanged" event of LocationService.
        /// Updates location state and starts location service if location is not disabled.
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
        /// Handles execution of "GoToWorkoutCompletedCommand".
        /// </summary>
        private void ExecuteGoToWorkoutCompleted()
        {
            PageNavigationService.Instance.GoToWorkoutCompletedPage();
        }

        /// <summary>
        /// Sets up events listeners.
        /// </summary>
        private void AddEventListeners()
        {
            _workoutModel.Updated += OnWorkoutModelUpdated;
            _locationService.SettingChanged += OnLocationServiceSettingChanged;
        }

        /// <summary>
        /// Removes events listeners.
        /// </summary>
        private void RemoveEventListeners()
        {
            _workoutModel.Updated -= OnWorkoutModelUpdated;
            _locationService.SettingChanged -= OnLocationServiceSettingChanged;
        }

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public PausePageViewModel()
        {
            _workoutModel = WorkoutModel.Instance;
            _locationService = LocationService.Instance;

            IsLocationDisabled = !_locationService.IsGPSLocationEnabled();

            InitProperties();
            InitCommands();
            AddEventListeners();
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
        /// Destroys the PausePageViewModel object.
        /// </summary>
        ~PausePageViewModel()
        {
            Dispose(false);
        }

        #endregion
    }
}
