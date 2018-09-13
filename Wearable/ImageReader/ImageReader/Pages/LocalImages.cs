using Xamarin.Forms;

namespace ImageReader.Pages
{
    /// <summary>
    /// LocalImages class
    /// It shows how to use the image shipped with the application
    /// </summary>
    public class LocalImages : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
	public LocalImages()
	{
            // Make NavigationPage have no navigation bar
            NavigationPage.SetHasNavigationBar(this, false);

            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "1) Image FileSource",
                        // wrap at character boundaries
                        LineBreakMode = LineBreakMode.CharacterWrap,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        FontAttributes = FontAttributes.Bold,
                    },
                    new Image
                    {
                        // '1.png' image should be placed in the 'res' resource directory of this application.
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
