using System;

namespace Workout.Models
{
    /// <summary>
    /// Event arguments class for location updated event.
    /// Provides location data.
    /// </summary>
    public class LocationUpdatedEventArgs : EventArgs
    {
        #region properties

        /// <summary>
        /// Location data.
        /// </summary>
        public LocationData Data { get; }

        #endregion

        #region methods

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="data">Location data.</param>
        public LocationUpdatedEventArgs(LocationData data)
        {
            Data = data;
        }

        #endregion
    }
}
