using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AmbientWatch
{
    /// <summary>
    /// AmbientWatchApplication class
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AmbientWatchApplication : Application
    {
		public AmbientWatchApplication()
		{
			InitializeComponent();
		}
	}
}