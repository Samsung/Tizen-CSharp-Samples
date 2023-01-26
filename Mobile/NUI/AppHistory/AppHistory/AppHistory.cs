﻿using System;
using Tizen.NUI;
using Tizen.NUI.Components;

namespace AppHistory
{
    public class Program : NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Window window = Window.Instance;
            window.BackgroundColor = Color.Blue;
            window.KeyEvent += OnKeyEvent;

            Navigator navigator = window.GetDefaultNavigator();

            MainPage page = new MainPage();
            navigator.Push(page);
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
