using System;
using Xamarin.Forms;

namespace MusicPlayerUI.Page
{
	/// <summary>
	/// Represents a modal play page.
	/// </summary>
	public partial class ModalPlayPage : ContentPage
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ModalPlayPage()
		{
			InitializeComponent();
		}

		private void OnSongValueInit(object sender, EventArgs e)
		{
			var slider = this.FindByName<Slider>("slider");
			if (slider != null)
				slider.Value = 0;
		}
	}
}
