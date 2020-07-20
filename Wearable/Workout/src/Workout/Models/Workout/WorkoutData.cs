using System;

namespace Workout.Models.Workout
{
    /// <summary>
    /// Workout data class.
    /// </summary>
    public class WorkoutData
    {
        #region properties

        /// <summary>
        /// Start time.
        /// </summary>
        public DateTime StartTime
        {
            get; set;
        }

        /// <summary>
        /// Local time.
        /// </summary>
        public DateTime LocalTime
        {
            get; set;
        }

        /// <summary>
        /// Beats per minute.
        /// </summary>
        public int Bpm
        {
            get; set;
        }

        /// <summary>
        /// Current Bpm range.
        /// </summary>
        public int BpmRange
        {
            get; set;
        }

        /// <summary>
        /// Array of Bpm range occurrences.
        /// </summary>
        public int[] BpmRangeOccurrences
        {
            get; set;
        }

        /// <summary>
        /// Bpm converted to value from 0 to 1 for current Bpm range.
        /// </summary>
        public double NormalizedBpm
        {
            get; set;
        }

        /// <summary>
        /// Distance.
        /// </summary>
        public double Distance
        {
            get; set;
        }

        /// <summary>
        /// Distance unit.
        /// </summary>
        public string DistanceUnit
        {
            get; set;
        }

        /// <summary>
        /// Average time for 1 distance unit.
        /// </summary>
        public double Pace
        {
            get; set;
        }

        /// <summary>
        /// GPS state.
        /// </summary>
        public bool IsGPSEnabled
        {
            get; set;
        }

        /// <summary>
        /// Stop watch.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get; set;
        }

        #endregion
    }
}
