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
    public class App : Application, IAppConfigurationChanged
    {
        /// <summary>
        /// IMESample's constructor
        /// </summary>
        /// <param name="isLandscape"> A flag whether current display orientation is landscape or not. </param>
        public App(bool isLandscape)
        {
             if (isLandscape)
            {
                // Changed the layout to landscape keyboard
                OnOrientationChanged(AppOrientation.Landscape);
            }
            else
            {
                // Changed the layout to portrait keyboard
                OnOrientationChanged(AppOrientation.Portrait);
            }
        }

        /// <summary>
        /// A method which provides the device orientation change
        /// </summary>
        /// <param name="orientation"> A device orientation value. </param>
        public void OnOrientationChanged(AppOrientation orientation)
        {
            switch (orientation)
            {
                case AppOrientation.Landscape:
                    // Load the landscape keyboard layout
                    MainPage = new IME_KEYBOARD_LAYOUT_QWERTY_LAND();
                    break;

                case AppOrientation.Portrait:
                default:
                    // Load the portrait keyboard layout
                    MainPage = new IME_KEYBOARD_LAYOUT_QWERTY_PORT();
                    break;
            }
        }

        /// <summary>
        /// Handle when your app starts
        /// </summary>
        protected override void OnStart()
        {
            EditorWindow.SetSize(720, 442, 1280, 380);
        }

        /// <summary>
        /// Handle when your app resumes
        /// </summary>
        protected override void OnResume()
        {
        }
    }
}