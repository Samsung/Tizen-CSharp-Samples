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

using ElmSharp;
using Xamarin.Forms;

namespace Puzzle.Tizen.Mobile
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// This is called when the application is firstly created.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            //Load the application
            LoadApplication(new App());
            //Disable app's Landscape mode
            MainWindow.AvailableRotations = DisplayRotation.Degree_0;
            MainWindow.StatusBarMode = ElmSharp.StatusBarMode.Transparent;
        }

        /// <summary>
        /// The main entrance of the application.
        /// </summary>
        /// <param name="args">The <see cref="string"/> arguments.</param>
        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            app.Run(args);
        }
    }
}
