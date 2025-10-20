using AppCommon.Services;
using System;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using AppCommon.Models;
using AppCommon.Pages;
using AppCommon.Services;

namespace AppCommon
{
    class Program : NUIApplication
    {
        private Window window;
        private Navigator navigator;
        private MainPage mainPage;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            // Set IsUsingXaml to false as per migration requirements
            NUIApplication.IsUsingXaml = false;

            // Initialize service locator
            ServiceLocator.Initialize();

            window = NUIApplication.GetDefaultWindow();
            window.BackgroundColor = Color.White;

            // Get window size
            var windowSize = window.WindowSize;
            int screenWidth = (int)windowSize.Width;
            int screenHeight = (int)windowSize.Height;

            // Get default navigator for page-based navigation
            navigator = window.GetDefaultNavigator();

            // Push main page with tabs
            mainPage = new MainPage(screenWidth, screenHeight);
            navigator.Push(mainPage);
        }

        /// <summary>
        /// Handle LowMemory event
        /// </summary>
        /// <param name="e">Low memory event arguments</param>
        protected override void OnLowMemory(LowMemoryEventArgs e)
        {
            base.OnLowMemory(e);

            // Convert Tizen.Applications.LowMemoryStatus to our LowMemoryStatus enum
            var status = Models.LowMemoryStatus.None;
            switch (e.LowMemoryStatus)
            {
                case Tizen.Applications.LowMemoryStatus.Normal:
                    status = Models.LowMemoryStatus.Normal;
                    break;
                case Tizen.Applications.LowMemoryStatus.SoftWarning:
                    status = Models.LowMemoryStatus.SoftWarning;
                    break;
                case Tizen.Applications.LowMemoryStatus.HardWarning:
                    status = Models.LowMemoryStatus.HardWarning;
                    break;
            }

            // Update ViewModel
            mainPage?.AppInfoPage?.ViewModel?.UpdateLowMemoryLEDColor(status);
        }

        /// <summary>
        /// Handle DeviceOrientation event
        /// </summary>
        /// <param name="e">Device orientation event arguments</param>
        protected override void OnDeviceOrientationChanged(DeviceOrientationEventArgs e)
        {
            base.OnDeviceOrientationChanged(e);

            // Convert Tizen.Applications.DeviceOrientation to our DeviceOrientationStatus enum
            var orientation = Models.DeviceOrientationStatus.Orientation_0;
            switch (e.DeviceOrientation)
            {
                case Tizen.Applications.DeviceOrientation.Orientation_0:
                    orientation = Models.DeviceOrientationStatus.Orientation_0;
                    break;
                case Tizen.Applications.DeviceOrientation.Orientation_90:
                    orientation = Models.DeviceOrientationStatus.Orientation_90;
                    break;
                case Tizen.Applications.DeviceOrientation.Orientation_180:
                    orientation = Models.DeviceOrientationStatus.Orientation_180;
                    break;
                case Tizen.Applications.DeviceOrientation.Orientation_270:
                    orientation = Models.DeviceOrientationStatus.Orientation_270;
                    break;
            }

            // Update ViewModel
            mainPage?.AppInfoPage?.ViewModel?.UpdateDeviceOrientation(orientation);
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
