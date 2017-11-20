using System;
using Calendar.Tizen.Port;

namespace Calendar.Tizen.Mobile
{
    /// <summary>
    /// Represents xamarin forms of tizen platform app
    /// </summary>
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// On create method
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        /// <summary>
        /// Main method of sample calendar tizen mobile
        /// </summary>
        /// <param name="args"> arguments</param>
        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.DependencyService.Register<CalendarPort>();
            global::Xamarin.Forms.DependencyService.Register<SecurityPort>();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            app.Run(args);
        }
    }
}
