using CalendarComponent.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(IAppPage))]
namespace CalendarComponent.Interfaces
{
    /// <summary>
    /// Interface for obtaining project's view using Dependency Injection.
    /// </summary>
    ///
    public interface IAppPage
    {
    }
}