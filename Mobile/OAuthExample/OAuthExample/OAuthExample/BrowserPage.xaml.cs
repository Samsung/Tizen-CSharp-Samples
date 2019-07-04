using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OAuthExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BrowserPage : ContentPage
	{
		public BrowserPage(string url)
		{
			InitializeComponent();
            ToolbarItems.Add(new ToolbarItem("Cancel", null, () =>
            {
                Navigation.PopAsync();
            }, ToolbarItemOrder.Secondary));
            browser.Source = url;
		}
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}