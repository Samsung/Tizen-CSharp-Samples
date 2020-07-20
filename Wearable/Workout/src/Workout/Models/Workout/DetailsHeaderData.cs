using System;
using Xamarin.Forms;

namespace Workout.Models.Workout
{
    /// <summary>
    /// Details header data class.
    /// </summary>
    public class DetailsHeaderData : BindableObject
    {
        #region properties

        /// <summary>
        /// Bindable property definition for local time.
        /// </summary>
        public static readonly BindableProperty LocalTimeProperty =
            BindableProperty.Create("LocalTime", typeof(DateTime), typeof(DetailsHeaderData), default(DateTime));

        /// <summary>
        ///Header title.
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        ///Start time.
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// Local time.
        /// </summary>
        public DateTime LocalTime
        {
            get => (DateTime)GetValue(LocalTimeProperty);
            set => SetValue(LocalTimeProperty, value);
        }

        #endregion
    }
}
