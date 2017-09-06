using Xamarin.Forms;

namespace MusicPlayerUI.Tizen
{
    class Program : Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());

            if (Device.Idiom == TargetIdiom.TV)
            {
                MainWindow.KeyGrab(ElmSharp.EvasKeyEventArgs.PlatformMenuButtonName, true);
                MainWindow.KeyDown += (s, e) =>
                {
                    if (e.KeyName == ElmSharp.EvasKeyEventArgs.PlatformMenuButtonName)
                    {
                        var target = Application.Current as App;
                        if (target != null)
                        {
                            target.IsMenuOpen = !target.IsMenuOpen;
                        }
                    }
                };
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            app.Run(args);
        }
    }
}