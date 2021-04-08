using System.Reflection;
using Xamarin.Forms;

namespace ImageReader.Pages
{
    /// <summary>
    /// EmbeddedImages class
    /// It shows how to load and display images embedded as an assembly resource.
    /// The image file is embedded in the assembly as a resource.
    /// 
    /// - How to embed 
    ///   To embed an image, the image needs to be set to 'Build Action - EmbeddedResource.'
    ///   Please check File property of '2.png' image file (Advanced > Build Action)
    ///   
    ///   VS IDE generates the Resource ID of this embedded image 
    ///     by concatenating application's namespace and image file name using a period(.)
    ///   In this case, 
    ///    app's namespace + . + file name 
    ///           ImageReader.2.png
    /// 
    /// - How to load the embedded images
    /// call ImageSource.FromResource() method with Resource ID
    /// </summary>
    public class EmbeddedImages : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
		public EmbeddedImages()
		{
            // Make NavigationPage have no navigation bar
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
                        // a resource identifier to an image file embedded in the application or PCL, with a Build Action:EmbeddedResource.
                        // Please check File property of '2.png' image file (Advanced > Build Action)
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
