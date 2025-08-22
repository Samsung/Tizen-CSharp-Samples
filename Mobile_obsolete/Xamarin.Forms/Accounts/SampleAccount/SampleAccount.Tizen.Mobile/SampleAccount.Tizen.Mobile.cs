using System;
using AccountManager.Tizen.Port;
using Xamarin.Forms;

namespace SampleAccount.Tizen.Mobile
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
        /// Main method of sample account tizen mobile
        /// </summary>
        /// <param name="args"> arguments</param>
        static void Main(string[] args)
        {
            var app = new Program();

            // Register Tizen Account Manager API Port
            global::Xamarin.Forms.DependencyService.Register<AccountManagerPort>();

            // Initiailize App
            Forms.Init(app);
            app.Run(args);
        }
    }
}
