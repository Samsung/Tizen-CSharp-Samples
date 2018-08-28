using Xamarin.Forms;

namespace AppCommon.Extensions
{
    /// <summary>
    /// The background class is for providing a background using images.
    /// Background normally works as an image, so it should be used with another layout to be set as a background.
    /// </summary>
    /// <example>
    /// <code>
    /// Background bg = new Background
    /// {
    ///     Image = new FileImageSource { File = "icon.png" },
    ///     Option = BackgroundOptions.Tile,
    /// };
    ///
    /// AbsoluteLayout aLayout = new AbsoluteLayout();
    /// AbsoluteLayout.SetLayoutFlags(bg, AbsoluteLayoutFlags.All);
    /// AbsoluteLayout.SetLayoutBounds(bg, new Rectangle(/*X*/,/*Y*/,/*WIDTH*/,/*HEIGHT*/));
    /// aLayout.Children.Add(bg);
    ///
    /// RelativeLayout rLayout = new RelativeLayout();
    /// rLayout.Children.Add(
    ///     bg,
    ///     Constraint.RelativeToParent((parent) => { return /*X*/ },
    ///     Constraint.RelativeToParent((parent) => { return /*Y*/ },
    ///     Constraint.RelativeToParent((parent) => { return /*WIDTH*/ },
    ///     Constraint.RelativeToParent((parent) => { return /*HEIGHT*/ }
    /// );
    /// </code>
    /// </example>
    public class Background : View
    {
        /// <summary>
        /// BindableProperty. Identifies the image bindable property.
        /// </summary>
        public static readonly BindableProperty ImageProperty = BindableProperty.Create("Image", typeof(FileImageSource), typeof(Background), default(FileImageSource));

        /// <summary>
        /// BindableProperty. Identifies the option bindable property.
        /// </summary>
        public static readonly BindableProperty OptionProperty = BindableProperty.Create("Option", typeof(BackgroundOptions), typeof(Background), BackgroundOptions.Scale);

        /// <summary>
        /// Gets or sets the image on the background.
        /// This identifies the image path of the image.
        /// It can be set to null.
        /// </summary>
        public FileImageSource Image
        {
            get { return (FileImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// Gets or sets the BackgroundOption on the background.
        /// This identifies on how a background will display its image.
        /// Default value is BackgroundOptions.Scale.
        /// </summary>
        public BackgroundOptions Option
        {
            get { return (BackgroundOptions)GetValue(OptionProperty); }
            set { SetValue(OptionProperty, value); }
        }
    }
}