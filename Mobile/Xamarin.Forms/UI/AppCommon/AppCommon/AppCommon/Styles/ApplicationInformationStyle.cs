using Xamarin.Forms;

namespace AppCommon.Styles
{
    /// <summary>
    /// A style for the contents of the application information page
    /// </summary>
    class ApplicationInformationStyle
    {
        public static Style ContentStyle = new Style(typeof(Label))
        {
            Setters  =
            {
                new Setter { Property = Label.FontSizeProperty, Value = 25 },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromRgb(146, 146, 146) },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromRgb(146, 146, 146) },
                new Setter { Property = Label.LineBreakModeProperty , Value = LineBreakMode.CharacterWrap },
                new Setter { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.StartAndExpand },
            }
        };

        public static Style LargerContentStyle = new Style(typeof(Label))
        {
            Setters  =
            {
                new Setter { Property = Label.FontSizeProperty, Value = 130 },
                new Setter { Property = Label.TextColorProperty, Value = Color.FromRgb(0, 0, 0) },
                new Setter { Property = Label.HorizontalOptionsProperty, Value = LayoutOptions.StartAndExpand },
                new Setter { Property = Label.VerticalOptionsProperty, Value = LayoutOptions.StartAndExpand },
            }
        };
    }
}