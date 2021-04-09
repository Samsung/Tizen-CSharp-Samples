using System;
using Workout.Models.Workout;
using Workout.Services;

namespace Workout.ViewModels.Workout
{
    /// <summary>
    /// Workout main view model.
    /// </summary>
    public class MainViewModel : BaseViewModel, IDisposable
    {
        #region fields

        /// <summary>
        /// Disposed flag.
        /// </summary>
        private bool _disposed = false;

        /// <summary>
        /// Beats per minute.
        /// </summary>
        private int _bpm;

        /// <summary>
        /// Distance.
        /// </summary>
        private string _distance;

        /// <summary>
        /// GPS state.
        /// </summary>
        private bool _isGPSEnabled;

        /// <summary>
        /// Heart rate indicator image.
        /// </summary>
        private string _heartRateIndicatorImage = "";

        /// <summary>
        /// Heart rate indicator rotation.
        /// </summary>
        private string _heartRateIndicatorRotation;

        /// <summary>
        /// Distance unit.
        /// </summary>
        private string _distanceUnit;

        /// <summary>
        /// Average time for distance unit.
        /// </summary>
        private string _pace;

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

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets beats per minute.
        /// </summary>
        public int Bpm
        {
            get => _bpm;
            set => SetProperty(ref _bpm, value);
        }

        /// <summary>
        /// Gets or sets local time.
        /// </summary>
        public DateTime LocalTime
        {
            get => _localTime;
            set => SetProperty(ref _localTime, value);
        }

        /// <summary>
        /// Total distance.
        /// </summary>
        public string Distance
        {
            get => _distance;
            set => SetProperty(ref _distance, value);
        }

        /// <summary>
        /// GPS state.
        /// </summary>
        public bool IsGPSEnabled
        {
            get => _isGPSEnabled;
            set => SetProperty(ref _isGPSEnabled, value);
        }

        /// <summary>
        /// Heart rate indicator image.
        /// </summary>
        public string HeartRateIndicatorImage
        {
            get => _heartRateIndicatorImage;
            set => SetProperty(ref _heartRateIndicatorImage, value);
        }

        /// <summary>
        /// Heart rate indicator rotation.
        /// </summary>
        public string HeartRateIndicatorRotation
        {
            get => _heartRateIndicatorRotation;
            set => SetProperty(ref _heartRateIndicatorRotation, value);
        }

        /// <summary>
        /// Distance unit.
        /// </summary>
        public string DistanceUnit
        {
            get => _distanceUnit;
            set => SetProperty(ref _distanceUnit, value);
        }

        /// <summary>
        /// Average time for 1 distance unit.
        /// </summary>
        public string Pace
        {
            get => _pace;
            set => SetProperty(ref _pace, value);
        }

        /// <summary>
        /// Gets or sets elapsed time.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            set => SetProperty(ref _elapsedTime, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Updates values of workout properties.
        /// </summary>
        /// <param name="data">Workout data.</param>
        private void UpdateWorkoutProperties(WorkoutData data)
        {
            LocalTime = data.LocalTime;
            Distance = (data.Distance / 1000).ToString("F2");
            DistanceUnit = data.DistanceUnit;
            Pace = data.Pace.ToString("F2");
            Bpm = data.Bpm;
            ElapsedTime = data.ElapsedTime;
            IsGPSEnabled = data.IsGPSEnabled;

            HeartRateIndicatorImage = "images/hrm_indicator/" + data.BpmRange.ToString() + ".png";
            HeartRateIndicatorRotation = ((1 - data.NormalizedBpm) * 180).ToString();
        }

        /// <summary>
        /// Handles "Updated" event of the WorkoutModel object.
        /// Updates values of workout properties.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="args">Event arguments.</param>
        private void OnWorkoutModelUpdated(object sender, WorkoutUpdatedEventArgs args)
        {
            UpdateWorkoutProperties(args.Data);
        }

        /// <summary>
        /// Handles "Paused" event of the WorkoutModel object.
        /// Navigates to pause page.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="args">Event arguments. Not used.</param>
        private void OnWorkoutModelPaused(object sender, EventArgs args)
        {
            PageNavigationService.Instance.GoToWorkoutPausePage();
        }

        /// <summary>
        /// Handles "SettingChanged" event of LocationService.
        /// Navigates to pause page if GPS location is disabled.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="locationState">Location state.</param>
        private void OnLocationServiceSettingChanged(object sender, bool locationState)
        {
            if (!locationState)
            {
                PauseWorkout();
            }
        }

        /// <summary>
        /// Sets up events listeners.
        /// </summary>
        private void AddEventListeners()
        {
            _workoutModel.Updated += OnWorkoutModelUpdated;
            _workoutModel.Paused += OnWorkoutModelPaused;
            _locationService.SettingChanged += OnLocationServiceSettingChanged;
        }

        /// <summary>
        /// Removes events listeners.
        /// </summary>
        private void RemoveEventListeners()
        {
            _workoutModel.Updated -= OnWorkoutModelUpdated;
            _workoutModel.Paused -= OnWorkoutModelPaused;
            _locationService.SettingChanged -= OnLocationServiceSettingChanged;
        }

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public MainViewModel()
        {
            _workoutModel = WorkoutModel.Instance;
            _locationService = LocationService.Instance;

            UpdateWorkoutProperties(_workoutModel.WorkoutData);

            AddEventListeners();
        }

        /// <summary>
        /// Starts workout.
        /// </summary>
        public void StartWorkout()
        {
            _workoutModel.StartWorkout();
        }

        /// <summary>
        /// Pauses workout.
        /// </summary>
        public void PauseWorkout()
        {
            _workoutModel.PauseWorkout();
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
        /// Destroys the MainViewModel object.
        /// </summary>
        ~MainViewModel()
        {
            Dispose(false);
        }

        #endregion
    }
}
