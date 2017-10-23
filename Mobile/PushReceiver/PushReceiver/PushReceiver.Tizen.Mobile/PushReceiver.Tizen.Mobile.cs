using System;
using PushReceiver.Tizen.Port;
using PushReceiver.Utils;

namespace PushReceiver.Tizen.Mobile
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// An interface for the  platform event
        /// </summary>
        IPlatformEvent pEvent;

        protected override void OnCreate()
        {
            base.OnCreate();
            var app = new App();
            pEvent = app;
            LoadApplication(app);

            PushPort.Register(pEvent);
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.DependencyService.Register<PushPort>();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            app.Run(args);
        }
    }
}
