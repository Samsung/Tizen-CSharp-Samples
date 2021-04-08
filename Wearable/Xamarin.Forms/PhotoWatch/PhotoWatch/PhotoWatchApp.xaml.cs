using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotoWatch
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoWatchApp : Application
    {
        public PhotoWatchApp()
        {
            InitializeComponent();
        }
    }
}