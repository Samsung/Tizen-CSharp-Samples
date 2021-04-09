namespace Workout.Models
{
    /// <summary>
    /// Location data class.
    /// </summary>
    public class LocationData
    {
        #region properties

        /// <summary>
        /// Distance in meters.
        /// </summary>
        public double Distance
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

        #endregion
    }
}
