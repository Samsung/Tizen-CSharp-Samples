using System;
using AccountManager.Tizen.Port;

namespace SampleAccount.Tizen.Mobile
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            // Register Tizen Account Manager API Port
            global::Xamarin.Forms.DependencyService.Register<AccountManagerPort>();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            app.Run(args);
        }
    }
}
