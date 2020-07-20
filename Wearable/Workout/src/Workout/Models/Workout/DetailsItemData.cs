namespace Workout.Models.Workout
{
    /// <summary>
    /// Details item data class.
    /// Used as one element of the details page list.
    /// </summary>
    public class DetailsItemData
    {
        #region properties

        /// <summary>
        /// Workout detail name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Workout detail value.
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// Workout detail icon.
        /// </summary>
        public string Icon
        {
            get;
            set;
        }

        /// <summary>
        /// Workout detail action button visibility flag.
        /// </summary>
        public bool IsActionButtonVisible
        {
            get;
            set;
        }

        #endregion
    }
}
