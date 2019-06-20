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
using IMESample.Views;
using Tizen.Uix.InputMethod;

namespace IMESample
{
    /// <summary>
    /// Application app class
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            MainPage = new IME_KEYBOARD_LAYOUT_QWERTY_PORT();
        }

        /// <summary>
        /// Called when your app starts.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
            EditorWindow.SetSize(360,240,360,240);
        }

        /// <summary>
        /// Called when your app resumes.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
            MainPage = new IME_KEYBOARD_LAYOUT_QWERTY_PORT();
        }
    }
}
