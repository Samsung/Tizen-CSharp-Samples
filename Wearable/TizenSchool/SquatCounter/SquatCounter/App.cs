using SquatCounter.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SquatCounter
{
    public class App : Application
    {
        public App()
        {
            PageNavigationService.Instance.GoToGuidePage();
        }
    }
}
