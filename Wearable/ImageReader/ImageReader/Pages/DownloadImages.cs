using System;
using Xamarin.Forms;

namespace ImageReader.Pages
{
	public class DownloadImages : ContentPage
    {
		public DownloadImages()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "3) ImageSource.FromUri",
                        LineBreakMode = LineBreakMode.CharacterWrap,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        FontAttributes = FontAttributes.Bold
                    },
                    new Image
                    {
                        Source = ImageSource.FromUri(new Uri("https://download.tizen.org/misc/Tizen-Brand/01-Primary-Assets/Logo/On-Dark/01-RGB/Tizen-Logo-On-Dark-RGB.png"))
                    },
                },
                Padding = new Thickness(50, 30, 50, 30),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
        }
	}
}