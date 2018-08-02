using Xamarin.Forms;

namespace UsingResxLocalization.ViewExtensions
{
    public class NoContentView : View
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(NoContentView), default(string));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
    }
}