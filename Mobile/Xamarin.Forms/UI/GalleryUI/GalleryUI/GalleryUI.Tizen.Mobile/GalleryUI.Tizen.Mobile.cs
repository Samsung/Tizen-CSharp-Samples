/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;

namespace GalleryUI.Tizen.Mobile
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            App app = new App(MainWindow.ScreenSize.Width, MainWindow.ScreenSize.Height);
            LoadApplication(app);

            // Set rotation changed callback to main window.
            SetRotationChangedCb(app, MainWindow);
        }

        /// <summary>
        /// Set rotation changed callback to main window.
        /// </summary>
        /// <param name="app">Current app.</param>
        /// <param name="window">Main window of current app.</param>
        private void SetRotationChangedCb(App app, ElmSharp.Window window)
        {
            window.RotationChanged += (s, e) =>
            {
                // landscape mode
                if (window.Rotation == 0 || window.Rotation == 180)
                {
                    app.MainPage = new MainPage(false);
                }
                // portrait mode
                else
                {
                    app.MainPage = new MainPage(true);
                }
            };
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            app.Run(args);
        }
    }
}
