/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
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

using System;
using Xamarin.Forms;

namespace XStopWatch
{
    /// <summary>
    /// This class has a main function, which is the starting point of the program.
    /// It is a class that controls the lifecycle of the application.
    /// You must use FormsApplication to control the lifecycle.
    /// This class is a class that makes use of Xamarin.Forms.Application by inheriting CoreUIApplication with definition of Lifecycle of Tizen C # Application.
    /// </summary>
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        /// <summary>
        /// Called when this application is launched.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            var stopwatch = new StopWatchApplication();
            LoadApplication(stopwatch);

            // If AlwaysOn is enable, the screen will not turn off as long as the window is on the screen.
            stopwatch.AlwaysOnRequest += (s, e) => MainWindow.SetAlwaysOn(e);
        }


        static void Main(string[] args)
        {
            var app = new Program();
            // It's mandatory to initialize Xamarin.Forms
            Forms.Init(app);
            // It's mandatory to initialize Circular UI for using Tizen Wearable Circular UI API
            global::Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}