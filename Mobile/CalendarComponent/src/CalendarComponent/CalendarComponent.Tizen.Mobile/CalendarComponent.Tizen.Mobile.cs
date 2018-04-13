using ElmSharp;
using Xamarin.Forms.Platform.Tizen;

namespace CalendarComponent.Tizen.Mobile
{
    internal class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        #region methods

        protected override void OnCreate()
        {
            base.OnCreate();
            MainWindow.StatusBarMode = StatusBarMode.Transparent;
            Forms.Context.MainWindow.AvailableRotations = DisplayRotation.Degree_0;

            LoadApplication(new App());
        }

        private static void Main(string[] args)
        {
            var app = new Program();

            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            app.Run(args);
        }

        #endregion
    }
}