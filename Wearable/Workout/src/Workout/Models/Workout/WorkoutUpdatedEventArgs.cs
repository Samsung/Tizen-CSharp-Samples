using System;

namespace Workout.Models.Workout
{
    /// <summary>
    /// Event arguments class for workout updated event.
    /// Provides workout data.
    /// </summary>
    public class WorkoutUpdatedEventArgs : EventArgs
    {
        #region properties

        /// <summary>
        /// Workout data.
        /// </summary>
        public WorkoutData Data { get; }

        #endregion

        #region methods

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="data">Workout data.</param>
        public WorkoutUpdatedEventArgs(WorkoutData data)
        {
            Data = data;
        }

        #endregion
    }
}
