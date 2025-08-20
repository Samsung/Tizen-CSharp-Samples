/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using ElmSharp;
using Xamarin.Forms;

namespace SpeechToText.Tizen.Mobile
{
    /// <summary>
    /// SpeechToText forms application class for Tizen Mobile profile.
    /// </summary>
    class Program : Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        #region methods

        /// <summary>
        /// Handles creation phase of the forms application.
        /// Sets up window settings and loads Xamarin application.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            MainWindow.StatusBarMode = StatusBarMode.Transparent;
            MainWindow.AvailableRotations = DisplayRotation.Degree_0;
            LoadApplication(new App());
        }

        /// <summary>
        /// Entry method of the program/application.
        /// </summary>
        /// <param name="args">Launch arguments.</param>
        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            app.Run(args);
        }

        #endregion
    }
}