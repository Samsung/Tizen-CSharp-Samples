using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace Lescanner
{
    class Lescanner : NUIApplication
    {
        private TizenBLEService _bleService;

        /// <summary>
        /// Overrides the base class method to create the window and initialize the application.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            NUIApplication.IsUsingXaml = false;

            Window window = Window.Default;
            window.WindowSize = new Size2D(720, 1280); // Default size, can be adjusted
            window.Title = "LE Scanner";
            window.SetFullScreen(true); // Make the application fullscreen

            _bleService = new TizenBLEService();

            // Create a Navigator and push the HomePage as the initial page
            var navigator = window.GetDefaultNavigator();
            navigator.Push(new HomePage(_bleService, navigator));
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Arguments.</param>
        static void Main(string[] args)
        {
            var app = new Lescanner();
            app.Run(args);
        }
    }
}
