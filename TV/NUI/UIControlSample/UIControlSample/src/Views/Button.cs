/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
 *
 */

using System;
using System.Runtime.InteropServices;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace UIControlSample
{
    /// <summary>
    /// A sample of PushButton
    /// </summary>
    public class Button
    {
        private Tizen.NUI.Components.Button _pushbutton;
        private const string resources = "/home/owner/apps_rw/org.tizen.example.UIControlSample/res/images";
        private string normalImagePath = resources + "/Button/btn_bg_25_25_25_95.9.png";
        private string focusImagePath = resources + "/Button/btn_bg_255_255_255_200.9.png";
        private string pressImagePath = resources + "/Button/btn_bg_0_129_198_100.9.png";
        /// <summary>
        /// Constructor to create new PushButtonSample
        /// </summary>
        /// <param name="text">text</param>
        public Button(string text)
        {
            OnIntialize(text);
        }

        /// <summary>
        /// PushButton initialisation.
        /// </summary>
        /// <param name="text">text</param>
        private void OnIntialize(string text)
        {
            _pushbutton = new Tizen.NUI.Components.Button();
            _pushbutton.Focusable = true;
            _pushbutton.Size2D = new Size2D(300,80);
            _pushbutton.Focusable = true;
            _pushbutton.ParentOrigin = ParentOrigin.TopLeft;
            _pushbutton.PivotPoint = PivotPoint.TopLeft;
            _pushbutton.Text = text;
            _pushbutton.TextColor = Color.White;
            _pushbutton.BackgroundImage = normalImagePath;

            // Chang background Visual and Label when focus gained.
            _pushbutton.FocusGained += (obj, e) =>
            {
               _pushbutton.BackgroundImage = focusImagePath;
                _pushbutton.TextColor = Color.Black;
            };

            // Chang background Visual and Label when focus lost.
            _pushbutton.FocusLost += (obj, e) =>
            {
                _pushbutton.BackgroundImage = normalImagePath;
                _pushbutton.TextColor = Color.White;
            };

            // Chang background Visual when pressed.
            _pushbutton.KeyEvent += (obj, ee) =>
            {
                if ("Return" == ee.Key.KeyPressedName)
                {
                    if (Key.StateType.Down == ee.Key.State)
                    {
                        _pushbutton.BackgroundImage = pressImagePath;
                        Tizen.Log.Fatal("NUI", "Press in pushButton sample!!!!!!!!!!!!!!!!");
                    }
                    else if (Key.StateType.Up == ee.Key.State)
                    {
                        _pushbutton.BackgroundImage = focusImagePath;
                        Tizen.Log.Fatal("NUI", "Release in pushButton sample!!!!!!!!!!!!!!!!");
                    }
                }

                return false;
            };

        }

        /// <summary>
        /// Get the initialized pushButton
        /// </summary>
        /// <returns>
        /// The pushButton which be create in this class
        /// </returns>
        public Tizen.NUI.Components.Button GetPushButton()
        {
            return _pushbutton;
        }
    }
}