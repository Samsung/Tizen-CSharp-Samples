using System;
using Xamarin.Forms;

namespace FeedbackApp.Tizen.Mobile
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// This is called when the application is created
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

            // Load the application
            LoadApplication(new App());
        }

        /// <summary>
        /// The main entrance of the application
        /// </summary>
        /// <param name="args">Arguments</param>
        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            app.Run(args);
        }
    }
}
