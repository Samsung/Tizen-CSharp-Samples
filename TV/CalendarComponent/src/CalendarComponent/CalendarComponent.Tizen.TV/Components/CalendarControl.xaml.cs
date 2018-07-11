using CalendarComponent.Tizen.TV.Components;
using Xamarin.Forms;

[assembly: Dependency(typeof(CalendarControl))]
namespace CalendarComponent.Tizen.TV.Components
{
    /// <summary>
    /// Calendar component.
    /// </summary>
    public partial class CalendarControl : ContentView
    {
        #region methods

        /// <summary>
        /// CalendarControl construction.
        /// Initializes component.
        /// </summary>
        public CalendarControl()
        {
            InitializeComponent();
        }

        #endregion
    }
}