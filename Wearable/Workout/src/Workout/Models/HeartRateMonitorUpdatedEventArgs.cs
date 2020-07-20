using System;

namespace Workout.Models
{
    /// <summary>
    /// Event arguments class for heart rate monitor updated event.
    /// Provides heart rate monitor data.
    /// </summary>
    public class HeartRateMonitorUpdatedEventArgs : EventArgs
    {
        #region properties

        /// <summary>
        /// Heart rate monitor data.
        /// </summary>
        public HeartRateMonitorData Data { get; }

        #endregion

        #region methods

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="data">Heart rate monitor data.</param>
        public HeartRateMonitorUpdatedEventArgs(HeartRateMonitorData data)
        {
            Data = data;
        }

        #endregion
    }
}
