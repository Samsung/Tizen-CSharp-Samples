using Xamarin.Forms;

namespace Workout.Models.Workout
{
    /// <summary>
    /// Details item data class.
    /// Used as one element of the details page list.
    /// </summary>
    public class DetailsItemData : BindableObject
    {
        #region properties

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create("Value", typeof(string), typeof(DetailsItemData), default(string));

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
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
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
        /// Item layout value bounds.
        /// </summary>
        public Rectangle ValueBounds
        {
            get;
            set;
        }

        /// <summary>
        /// Item layout name bounds.
        /// </summary>
        public Rectangle NameBounds
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
