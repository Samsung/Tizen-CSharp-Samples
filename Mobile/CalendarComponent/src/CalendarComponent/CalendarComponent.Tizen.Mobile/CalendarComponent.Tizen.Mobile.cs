using ElmSharp;
using Xamarin.Forms;

namespace CalendarComponent.Tizen.Mobile
{
    internal class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        #region methods

        protected override void OnCreate()
        {
            base.OnCreate();
            MainWindow.StatusBarMode = StatusBarMode.Transparent;
            MainWindow.AvailableRotations = DisplayRotation.Degree_0;
            

            LoadApplication(new App());
        }

        private static void Main(string[] args)
        {
            var app = new Program();

            Forms.Init(app);
            app.Run(args);
        }

        #endregion
    }
}