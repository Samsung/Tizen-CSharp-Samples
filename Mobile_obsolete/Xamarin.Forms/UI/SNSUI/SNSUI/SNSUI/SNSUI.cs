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

namespace SNSUI
{
    /// <summary>
    /// A launcher class declare.
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Get and Set the screen width.
        /// </summary>
        public static int ScreenWidth { get; set; }

        /// <summary>
        /// Get and Set the screen height.
        /// </summary>
        public static int ScreenHeight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="screenWidth">The screen width</param>
        /// <param name="screenHeight">The screen height</param>
        public App(int screenWidth, int screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;

            /// The root page of your application
            var main = new MainPage { };
            MainPage = main;
        }

        /// <summary>
        /// override method for the class of SNSUI app
        /// </summary>
        protected override void OnStart()
        {
            /// Handle when your app starts
        }

        /// <summary>
        /// override method for the class of SNSUI app
        /// </summary>
        protected override void OnSleep()
        {
            /// Handle when your app sleeps
        }

        /// <summary>
        /// override method for the class of SNSUI app
        /// </summary>
        protected override void OnResume()
        {
            /// Handle when your app resumes
        }
    }
}
