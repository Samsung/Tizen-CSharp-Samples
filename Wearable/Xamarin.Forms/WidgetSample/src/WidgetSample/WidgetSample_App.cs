using Tizen.Applications;
using ElmSharp;

namespace WidgetSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Elementary.Initialize();
            Elementary.ThemeOverlay();
            WidgetApplication app = new WidgetApplication(typeof(MyFirstWidget));
            app.Run(args);
        }
    }
}
