using System;
using Workout.Services;
using Workout.Services.Settings;

namespace Workout.Models.Workout
{
    /// <summary>
    /// Provides workout related data.
    /// This is singleton. Instance accessible via <see cref = Instance></cref> property.
    /// </summary>
    public sealed class WorkoutModel
    {
        #region fields

        /// <summary>
        /// Instance of LocationModel.
        /// </summary>
        private readonly LocationModel _locationModel;

        /// <summary>
        /// Reference to the object of the HeartRateMonitorModel class.
        /// </summary>
        private readonly HeartRateMonitorModel _heartRateMonitorModel;

        /// <summary>
        /// Backing field of Instance property.
        /// </summary>
        private static WorkoutModel _instance;

        /// <summary>
        /// Instance of StopWatchService.
        /// </summary>
        private readonly StopWatchService _stopWatchService;

        #endregion

        #region properties

        /// <summary>
        /// WorkoutModel instance accessor.
        /// </summary>
        public static WorkoutModel Instance
        {
            get => _instance ?? (_instance = new WorkoutModel());
        }

        /// <summary>
        /// Gets workout data.
        /// </summary>
        public WorkoutData WorkoutData { get; private set; }

        /// <summary>
        /// Event invoked on data update.
        /// </summary>
        public event EventHandler<WorkoutUpdatedEventArgs> Updated;

        /// <summary>
        /// Event invoked on workout pause.
        /// </summary>
        public event EventHandler Paused;

        #endregion

        #region methods

        /// <summary>
        /// Sets up events listeners.
        /// </summary>
        private void AddEventListeners()
        {
            LocalTimeService.Updated += OnLocalTimeServiceUpdated;
            _locationModel.Updated += OnLocationModelUpdated;
            _heartRateMonitorModel.Updated += OnHeartRateMonitorModelUpdated;
            _stopWatchService.Updated += OnStopWatchServiceUpdated;
        }

        /// <summary>
        /// Emits event with current workout data.
        /// </summary>
        private void EmitUpdate()
        {
            Updated?.Invoke(this, new WorkoutUpdatedEventArgs(WorkoutData));
        }

        /// <summary>
        /// Handles "Updated" event of the LocalTimeService class.
        /// Updates the value of the local time property.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="localTime">Local time.</param>
        private void OnLocalTimeServiceUpdated(object sender, DateTime localTime)
        {
            WorkoutData.LocalTime = localTime;

            EmitUpdate();
        }

        /// <summary>
        /// Handles "Updated" event of the LocationModel object.
        /// Updates the value of distance.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="args">Updated values from location model.</param>
        private void OnLocationModelUpdated(object sender, LocationUpdatedEventArgs args)
        {
            WorkoutData.Distance = args.Data.Distance;
            WorkoutData.IsGPSEnabled = args.Data.IsGPSEnabled;

            EmitUpdate();
        }

        /// <summary>
        /// Handles "Updated" event of the HeartRateMonitorModel object.
        /// Updates the value of the bpm property.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="args">Updated values from heart rate monitor model.</param>
        private void OnHeartRateMonitorModelUpdated(object sender, HeartRateMonitorUpdatedEventArgs args)
        {
            WorkoutData.Bpm = args.Data.Bpm;
            WorkoutData.BpmRange = args.Data.BpmRange;
            WorkoutData.BpmRangeOccurrences = args.Data.BpmRangeOccurrences;
            WorkoutData.NormalizedBpm = args.Data.NormalizedBpm;

            EmitUpdate();
        }

        /// <summary>
        /// Handles "Updated" event of the StopWatchService object.
        /// Calculates the value of average time per one distance unit.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="elapsedTime">Elapsed time.</param>
        private void OnStopWatchServiceUpdated(object sender, TimeSpan elapsedTime)
        {
            WorkoutData.ElapsedTime = elapsedTime;

            if (WorkoutData.Distance > 0)
            {
                WorkoutData.Pace = WorkoutData.ElapsedTime.TotalMinutes / WorkoutData.Distance * 1000;
            }

            EmitUpdate();
        }

        /// <summary>
        /// Sets starting data values.
        /// </summary>
        private void SetInitialData()
        {
            WorkoutData = new WorkoutData
            {
                Bpm = 0,
                BpmRange = 0,
                Distance = 0,
                DistanceUnit = SettingsService.Instance.Distance.Unit,
                ElapsedTime = default(TimeSpan),
                IsGPSEnabled = false,
                StartTime = DateTime.Now,
                LocalTime = DateTime.Now,
                Pace = 0,
                NormalizedBpm = 0
            };
        }

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        private WorkoutModel()
        {
            SetInitialData();

            _heartRateMonitorModel = new HeartRateMonitorModel();
            _locationModel = new LocationModel();
            _stopWatchService = new StopWatchService();

            AddEventListeners();
        }

        /// <summary>
        /// Starts workout.
        /// </summary>
        public void StartWorkout()
        {
            _heartRateMonitorModel.Start();
            _locationModel.UnlockMeasurement();
            _stopWatchService.StartMeasurement();
        }

        /// <summary>
        /// Pauses workout.
        /// Emits "Paused" event.
        /// </summary>
        public void PauseWorkout()
        {
            _heartRateMonitorModel.Pause();
            _locationModel.LockMeasurement();
            _stopWatchService.StopMeasurement();

            Paused?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Finishes workout. Stops models providing data.
        /// </summary>
        public void FinishWorkout()
        {
            _heartRateMonitorModel.Stop();
            _locationModel.Stop();
        }

        /// <summary>
        /// Clears workout.
        /// </summary>
        public void ClearWorkout()
        {
            _stopWatchService.Reset();
            _locationModel.Reset();
            _heartRateMonitorModel.Reset();

            SetInitialData();
        }

        #endregion
    }
}
