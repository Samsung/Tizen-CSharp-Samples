using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NetworkApp
{
    class Program : NUIApplication
    {

        private Window window;
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            window = NUIApplication.GetDefaultWindow();
            var navigator = window.GetDefaultNavigator(); ;

            // Create the initial page for the application
            var initialPage = new MainMenuPage();
            navigator.Push(initialPage);
        }

        static void Main(string[] args)
        {
            NUIApplication.IsUsingXaml = false;
            var app = new Program();
            app.Run(args);
        }
    }
}
