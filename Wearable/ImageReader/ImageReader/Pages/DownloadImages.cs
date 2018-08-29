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
                        Source = ImageSource.FromUri(new Uri("http://pe.tedcdn.com/images/ted/2e306b9655267cee35e45688ace775590b820510_615x461.jpg"))
                    },
                },
                Padding = new Thickness(50, 30, 50, 30),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
        }
	}
}