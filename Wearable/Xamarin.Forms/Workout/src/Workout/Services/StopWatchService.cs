using System;
using System.Diagnostics;
using System.Timers;

namespace Workout.Services
{
    /// <summary>
    /// Provides stop watch related data.
    /// </summary>
    public class StopWatchService : Stopwatch
    {
        #region fields

        /// <summary>
        /// Internal timer instance.
        /// </summary>
        private readonly Timer _timer;

        /// <summary>
        /// TimeSpan on last tick.
        /// </summary>
        private TimeSpan _previousElapsedTime = default(TimeSpan);

        #endregion

        #region properties

        /// <summary>
        /// Event invoked on stop watch update.
        /// </summary>
        public event EventHandler<TimeSpan> Updated;

        #endregion

        #region methods

        /// <summary>
        /// Handles "Elapsed" event of the timer.
        /// Invokes "Updated" event when value of seconds have changed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="args">Event arguments. Not used.</param>
        private void OnElapsed(object sender, EventArgs args)
        {
            TimeSpan elapsed = Elapsed;

            if (_previousElapsedTime.Seconds != elapsed.Seconds)
            {
                Updated?.Invoke(this, elapsed);
                _previousElapsedTime = elapsed;
            }
        }

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public StopWatchService()
        {
            _timer = new Timer(100);

            _timer.Elapsed += OnElapsed;
        }

        /// <summary>
        /// Starts time measurement.
        /// </summary>
        public void StartMeasurement()
        {
            _timer.Start();
            Start();
        }

        /// <summary>
        /// Stops time measurement.
        /// </summary>
        public void StopMeasurement()
        {
            _timer.Stop();
            Stop();
        }

        #endregion
    }
}
