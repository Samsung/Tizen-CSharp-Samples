using System;
using Tizen.Sensor;
using HRM = Tizen.Sensor.HeartRateMonitor;

namespace Workout.Services
{
    /// <summary>
    /// Provides access to Tizen HRM API.
    /// </summary>
    public class HeartRateMonitorService
    {
        #region fields

        /// <summary>
        /// An instance of the HeartRateMonitor class provided by the Tizen Sensor API.
        /// </summary>
        private HRM _hrm;

        #endregion

        #region properties

        /// <summary>
        /// DataUpdated event.
        /// Notifies UI about heart rate value update.
        /// </summary>
        public event EventHandler<int> DataUpdated;

        /// <summary>
        /// HeartRateSensorNotSupported event.
        /// Notifies application about lack of heart rate sensor.
        /// </summary>
        public event EventHandler NotSupported;

        #endregion

        #region methods

        /// <summary>
        /// Handles "DataUpdated" event of the HeartRateMonitor object provided by the Tizen Sensor API.
        /// Invokes "DataUpdated" event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="heartRateMonitorData">Heart rate monitor event data.</param>
        private void OnDataUpdated(object sender, HeartRateMonitorDataUpdatedEventArgs heartRateMonitorData)
        {
            DataUpdated?.Invoke(this, heartRateMonitorData.HeartRate);
        }

        /// <summary>
        /// Initializes HeartRateMonitorService class.
        /// Invokes NotSupported event if heart rate sensor is not supported.
        /// </summary>
        public void Init()
        {
            try
            {
                _hrm = new HRM
                {
                    Interval = 1000,
                    PausePolicy = SensorPausePolicy.None
                };

                _hrm.DataUpdated += OnDataUpdated;
            }
            catch (Exception)
            {
                NotSupported?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Starts measuring the heart rate.
        /// </summary>
        public void Start()
        {
            _hrm.Start();
        }

        /// <summary>
        /// Stops measuring the heart rate.
        /// </summary>
        public void Stop()
        {
            _hrm.Stop();
        }

        #endregion
    }
}
