using System.Reflection;
using Xamarin.Forms;

namespace ImageReader.Pages
{
	public class EmbeddedImages : ContentPage
    {
		public EmbeddedImages()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "2) ImageSource.FromResource",
                        LineBreakMode = LineBreakMode.CharacterWrap,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        FontAttributes = FontAttributes.Bold
                    },
                    new Image
                    {
                        Source = ImageSource.FromResource("ImageReader.2.png", typeof(EmbeddedImages).GetTypeInfo().Assembly),
                    },
                },
                Padding = new Thickness(50, 30, 50, 30),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
        }
	}
}