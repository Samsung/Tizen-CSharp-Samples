namespace Workout.Models.Settings
{
    /// <summary>
    /// Provides distance related data.
    /// </summary>
    public class Distance
    {
        #region properties

        /// <summary>
        /// Unit.
        /// </summary>
        public string Unit
        {
            get; set;
        }

        /// <summary>
        /// Unit to kilometer ratio.
        /// Defines how many times the unit is larger than 1 kilometer.
        /// </summary>
        public double UnitToKmRatio
        {
            get; set;
        }

        #endregion
    }
}
