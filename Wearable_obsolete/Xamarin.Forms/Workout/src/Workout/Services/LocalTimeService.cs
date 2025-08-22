using System;
using System.Timers;

namespace Workout.Services
{
    /// <summary>
    /// Provides local time logic.
    /// </summary>
    public static class LocalTimeService
    {
        #region fields

        /// <summary>
        /// Internal timer instance.
        /// </summary>
        private static readonly Timer _timer = new Timer(1000);

        /// <summary>
        /// DateTime on last timer tick.
        /// </summary>
        private static DateTime _previousDateTime = DateTime.Now;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked when value of minutes or hours have changed since last tick.
        /// </summary>
        public static event EventHandler<DateTime> Updated;

        #endregion

        #region methods

        /// <summary>
        /// Handles "Elapsed" event of the timer.
        /// Invokes "Updated" event if values of minutes have changed since last tick.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="args">Event arguments. Not used.</param>
        private static void OnElapsed(object sender, EventArgs args)
        {
            DateTime _now = DateTime.Now;

            if (_previousDateTime.Minute != _now.Minute)
            {
                Updated?.Invoke(null, _now);
                _previousDateTime = _now;
            }
        }

        /// <summary>
        /// Sets up event emitter and starts timer.
        /// </summary>
        static LocalTimeService()
        {
            _timer.Elapsed += OnElapsed;

            _timer.Start();
        }

        #endregion
    }
}
