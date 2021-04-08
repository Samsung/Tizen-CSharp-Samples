namespace Workout.Models
{
    /// <summary>
    /// Heart rate monitor data class.
    /// </summary>
    public class HeartRateMonitorData
    {
        #region properties

        /// <summary>
        /// Bpm value.
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
        /// Bpm on scale. From 0 to 1.
        /// </summary>
        public double NormalizedBpm
        {
            get; set;
        }

        #endregion
    }
}
