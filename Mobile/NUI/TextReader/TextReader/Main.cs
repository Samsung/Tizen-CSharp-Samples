using System;
using TextReader_UI.views;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace TextReader
{
    internal class Program : NUIApplication
    {
        private Window window;
        private Navigator navigator;
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
            ContentPage mainPage = new MainPage(window.Size.Height, window.Size.Width);
            navigator.Push(mainPage);


        }

        void Initialize()
        {
            window = GetDefaultWindow();
            window.Title = "IMEManager";
            window.KeyEvent += OnKeyEvent;
            navigator = window.GetDefaultNavigator();
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
