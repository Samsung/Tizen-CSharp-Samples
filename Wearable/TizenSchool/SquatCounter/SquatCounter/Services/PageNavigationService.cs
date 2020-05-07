using SquatCounter.Views;
using Xamarin.Forms;

namespace SquatCounter.Services
{
    public sealed class PageNavigationService
    {
		private static PageNavigationService _instance;

		public static PageNavigationService Instance
		{
			get => _instance ?? (_instance = new PageNavigationService());
		}

		public void GoToGuidePage()
		{
			Application.Current.MainPage = new NavigationPage(new GuidePage());
		}

		public async void GoToSquatCounterPage()
		{
			await Application.Current.MainPage.Navigation.PushAsync(new SquatCounterPage());
		}
	}
}
