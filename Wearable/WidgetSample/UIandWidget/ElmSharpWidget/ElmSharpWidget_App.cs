using Tizen.Applications;
using ElmSharp;

namespace ElmSharpWidget
{
    class App
    {
        static void Main(string[] args)
        {
            Elementary.Initialize();
            Elementary.ThemeOverlay();
            WidgetApplication app = new WidgetApplication(typeof(MyWidget));
            app.Run(args);
        }
    }
}
