using Xamarin.Forms;

namespace ImageReader.Pages
{
	public class LocalImages : ContentPage
    {
		public LocalImages()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "1) Image FileSource",
                        LineBreakMode = LineBreakMode.CharacterWrap,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        FontAttributes = FontAttributes.Bold,
                    },
                    new Image
                    {
                        Source = "1.png"
                    },
                },
                Padding = new Thickness(50, 30, 50, 30),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
        }
	}
}