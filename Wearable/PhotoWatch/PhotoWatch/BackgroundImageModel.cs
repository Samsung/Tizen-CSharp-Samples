using Xamarin.Forms;

namespace PhotoWatch
{
    public class BackgroundImageModel
    {
        public string Path { get; set; }
        public string DisplayName { get; set; }
        public Command<string> DeleteCommand { get; set; }
    }
}
