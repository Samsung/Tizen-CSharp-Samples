using Xamarin.Forms;
using CalendarComponent.Interfaces;
using CalendarComponent.Tizen.TV.Components;
using CalendarComponent.Tizen.TV.Views;

[assembly: Dependency(typeof(CalendarPage))]
namespace CalendarComponent.Tizen.TV.Views
{
    /// <summary>
    /// CalendarPage class.
    /// Main application view.
    /// </summary>
    public partial class CalendarPage : IAppPage
    {
        #region methods

        /// <summary>
        /// Application main page constructor.
        /// Sets binding context and adds Calendar component.
        /// </summary>
        public CalendarPage()
        {
            InitializeComponent();

            BindingContext = ViewModels.ViewModelLocator.ViewModel;
            CalendarWrapper.Children.Add(new CalendarControl());
        }

        #endregion
    }
}