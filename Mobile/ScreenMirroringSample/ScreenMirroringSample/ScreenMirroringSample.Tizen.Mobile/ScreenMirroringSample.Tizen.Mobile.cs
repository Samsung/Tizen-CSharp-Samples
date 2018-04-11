using System;

using Tizen.Xamarin.Forms.Extension.Renderer;

namespace ScreenMirroringSample.Tizen.Mobile
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            MainWindow.Opacity = 0;
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();

            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);

            app.Run(args);
        }
    }
}
