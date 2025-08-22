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

namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// A shape of PushButton
    /// </summary>
    public class CustomButton
    {
        private Button _pushbutton;
        private string normalImagePath = CommonResource.GetLocalReosurceURL() + "/Button/btn_bg_25_25_25_95.9.png";
        private string focusImagePath = CommonResource.GetLocalReosurceURL() + "/Button/btn_bg_255_255_255_200.9.png";
        private string pressImagePath = CommonResource.GetLocalReosurceURL() + "/Button/btn_bg_0_129_198_100.9.png";
        /// <summary>
        /// Constructor to create new PushButtonSample
        /// </summary>
        public CustomButton()
        {
            OnIntialize();
        }

        /// <summary>
        /// PushButton initialisation.
        /// </summary>
        private void OnIntialize()
        {
            _pushbutton = new Button();
            _pushbutton.Focusable = true;
            _pushbutton.ParentOrigin = ParentOrigin.TopLeft;
            _pushbutton.PivotPoint = PivotPoint.TopLeft;
            _pushbutton.PointSize = DeviceCheck.PointSize8;
            _pushbutton.FontFamily = "SamsungOneUI_400";
            _pushbutton.TextAlignment = HorizontalAlignment.Center;
            if (_pushbutton.HasFocus())
            {
                _pushbutton.BackgroundImage = focusImagePath;
                _pushbutton.TextColor = Color.Black;
            }
            else
            {
                _pushbutton.BackgroundImage = normalImagePath;
                _pushbutton.TextColor = Color.White;
            }

            // Change backgroudVisul and Label when focus gained.
            _pushbutton.FocusGained += (obj, e) =>
            {
               _pushbutton.BackgroundImage = focusImagePath;
                _pushbutton.TextColor = Color.Black;
            };

            // Change backgroudVisul and Label when focus lost.
            _pushbutton.FocusLost += (obj, e) =>
            {
                _pushbutton.BackgroundImage = normalImagePath;
                _pushbutton.TextColor = Color.White;
            };

            // Change backgroudVisul when pressed.
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
        /// Set the text on the button
        /// </summary>
        /// <param name="text">the text value of the button</param>
        public void SetText(string text)
        {
            _pushbutton.Text = text;
            if (_pushbutton.HasFocus())
            {
                _pushbutton.BackgroundImage = focusImagePath;
                _pushbutton.TextColor = Color.Black;
            }
            else
            {
                _pushbutton.BackgroundImage = normalImagePath;
                _pushbutton.TextColor = Color.White;
            }


        }

        /// <summary>
        /// Get the initialised pushButton
        /// </summary>
        /// <returns>
        /// The pushButton which be create in this class
        /// </returns>
        public Button GetPushButton()
        {
            return _pushbutton;
        }

        /// <summary>
        /// Get/Set the text value on the button
        /// </summary>
        public string Text
        {
            get
            {
                return buttonTextLabel;
            }

            set
            {
                buttonTextLabel = value;
                SetText(buttonTextLabel);
            }

        }

        private string buttonTextLabel; 
    }
}