using Xamarin.Forms;

namespace MusicPlayerUI.Page
{
	/// <summary>
	/// Represents the detail page.
	/// </summary>
	public partial class DetailPage : TabbedPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public DetailPage()
		{
			InitializeComponent();
			ApplyMusicBar();
		}

		private void OnCurrentPageChanged(object sender, System.EventArgs e)
		{
			ApplyMusicBar();
		}

		private void OnExcuteToolbarMenu(object sender, System.EventArgs e)
		{
			(Parent as FlyoutPage).IsPresented = !(Parent as FlyoutPage).IsPresented;
		}

		/// <summary>
		/// Applying the music bar to layout
		/// </summary>
		private void ApplyMusicBar()
		{
			var grid = (CurrentPage as TabItemPage)?.Content.FindByName<Grid>("root");
			if (grid == null)
			{
				return;
			}

			if (!grid.Children.Contains(TabItemPage.MusicBar))
			{
				grid.Children.Add(TabItemPage.MusicBar);
				Grid.SetRow(TabItemPage.MusicBar, 1);
			}
		}
	}
}
