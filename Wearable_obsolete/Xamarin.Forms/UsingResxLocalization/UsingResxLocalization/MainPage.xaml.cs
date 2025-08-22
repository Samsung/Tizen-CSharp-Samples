using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsingResxLocalization
{
    /// <summary>
    /// MainPage class
    /// It defines what to do when images are tapped.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : CirclePage
    {
		public MainPage()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
		}

        async void LeftImage_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FirstPage());
        }

        async void RightImage_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FirstPageXaml());
        }
    }
}