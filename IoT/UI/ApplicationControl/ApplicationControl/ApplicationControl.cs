using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using ApplicationControl.Pages;

namespace ApplicationControl
{
    class Program : NUIApplication
    {
        private Window window;
        private Navigator navigator;

        protected override void OnCreate()
        {
            base.OnCreate();
            // Disable XAML usage
            IsUsingXaml = false;
            Initialize();
        }

        void Initialize()
        {
            window = NUIApplication.GetDefaultWindow();
            window.BackgroundColor = Color.White;

            // Get default navigator and push MainPage
            navigator = window.GetDefaultNavigator();
            var mainPage = new MainPage();
            navigator.Push(mainPage);
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
