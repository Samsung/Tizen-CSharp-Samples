using System;
using Calendar.Tizen.Port;
using Xamarin.Forms;

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
            DependencyService.Register<CalendarPort>();
            DependencyService.Register<SecurityPort>();
            Forms.Init(app);
            app.Run(args);
        }
    }
}
