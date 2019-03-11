using System;

namespace WorkingWithFonts.Tizen
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Console.WriteLine("this.DirectoryInfo.Resource : " + this.DirectoryInfo.Resource);
            // To use a custom font, you need to add a global font path.
            ElmSharp.Utility.AppendGlobalFontPath(this.DirectoryInfo.Resource);
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            global::Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
