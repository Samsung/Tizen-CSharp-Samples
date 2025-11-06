/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.Components;

namespace Calculator.Tizen
{
    /// <summary>
    /// Calculator application main entry point.
    /// </summary>
    /// <remarks>
    /// This main entry checks device orientation and let Calculator implementation knows which layout should be displayed.
    /// </remarks>
    class Program : NUIApplication
    {
        /// <summary>
        /// A Calculator App instance.</summary>
        private Calculator app;

        /// <summary>
        /// A AppResourcePath which is used by making full path of the app resources.</summary>
        public static string AppResourcePath
        {
            get;
            private set;
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            AppResourcePath = DirectoryInfo.Resource;
            DebuggingPort.MainWindow = Window.Default;

            app = new Calculator(IsLandscape(Window.Default.GetCurrentOrientation()));

            Window.Default.GetDefaultNavigator().Push(app.MainPage);

            List<Window.WindowOrientation> list = new List<Window.WindowOrientation>
            {
                Window.WindowOrientation.Landscape,
                Window.WindowOrientation.LandscapeInverse,
                Window.WindowOrientation.Portrait,
                Window.WindowOrientation.PortraitInverse,
            };
            Window.Default.SetAvailableOrientations(list);

            Window.Default.KeyEvent += WindowKeyEvent;

            // Registration for device orientation changing detection.
            Window.Default.OrientationChanged += (s, e) =>
            {
                if (IsLandscape(e.WindowOrientation))
                {
                    app.OnOrientationChanged(AppOrientation.Landscape);
                }
                else
                {
                    app.OnOrientationChanged(AppOrientation.Portrait);
                }
            };
        }

        /// <summary>
        /// Checking whether current device orientation is landscape. </summary>
        /// <returns>
        /// true : Landscape orientation, false : Portrait orientation. </returns>
        private bool IsLandscape(Window.WindowOrientation o)
        {
            return (o == Window.WindowOrientation.Landscape || o == Window.WindowOrientation.LandscapeInverse);
        }

        /// <summary>
        /// Called when the chosen key event is received.
        /// Use to exit the application
        /// </summary>
        /// <param name="sender"> Event sender </param>
        /// <param name="e"> Event argument </param>
        private void WindowKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Escape" || e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "BackSpace")
                {
                    Exit();
                }
            }
        }

        /// <summary>
        /// The entry point for the application.
        /// </summary>
        /// <param name="args"> A list of command line arguments.</param>
        static void Main(string[] args)
        {
            IsUsingXaml = false;
            var program = new Program();
            program.Run(args);
        }
    }
}
