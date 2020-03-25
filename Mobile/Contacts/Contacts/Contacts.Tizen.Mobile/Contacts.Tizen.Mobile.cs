using System;
using Xamarin.Forms;
using Contacts.Tizen.Port;

namespace Contacts.Tizen.Mobile
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
            DependencyService.Register<ContactsPort>();
            DependencyService.Register<SecurityPort>();
            Forms.Init(app);
            app.Run(args);
        }
    }
}
