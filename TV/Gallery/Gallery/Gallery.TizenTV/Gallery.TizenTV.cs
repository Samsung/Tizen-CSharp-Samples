using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace Gallery.TizenTV
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());

            if (Device.Idiom == TargetIdiom.TV)
            {
                MainWindow.KeyGrab("Left", true);
                MainWindow.KeyGrab("Right", true);
                MainWindow.KeyDown += (s, e) =>
                {
                    var target = Application.Current.MainPage.Navigation.NavigationStack.Last() as IRemoteController;
                    if (target == null)
                    {
                        Debug.WriteLine("test fail");
                        return;
                    }
                    if (e.KeyName == "Left")
                    {
                        target.SendLeftButtonDown();
                    }
                    else if (e.KeyName == "Right")
                    {
                        target.SendRightButtonDown();
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
