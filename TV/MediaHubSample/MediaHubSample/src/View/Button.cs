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
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace Tizen.NUI.MediaHub
{
    /// <summary>
    /// A sampe of PushButton
    /// </summary>
    public class CustomButton
    {
        private PushButton _pushbutton;
        private string normalImagePath = CommonResource.GetLocalReosurceURL() + "/Button/btn_bg_25_25_25_95.9.png";
        private string focusImagePath = CommonResource.GetLocalReosurceURL() + "/Button/btn_bg_255_255_255_200.9.png";
        private string pressImagePath = CommonResource.GetLocalReosurceURL() + "/Button/btn_bg_0_129_198_100.9.png";
        /// <summary>
        /// Constructor to create new PushButtonSample
        /// </summary>
        /// <param name="text">text</param>
        public CustomButton()
        {
            OnIntialize();
        }

        /// <summary>
        /// PushButton initialisation.
        /// </summary>
        private void OnIntialize()
        {
            _pushbutton = new PushButton();
            _pushbutton.Focusable = true;
            _pushbutton.Focusable = true;
            _pushbutton.ParentOrigin = ParentOrigin.TopLeft;
            _pushbutton.PivotPoint = PivotPoint.TopLeft;

            // Create normal backgroud visual.
            PropertyMap normalMap = new PropertyMap();
            normalMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            normalMap.Add(ImageVisualProperty.URL, new PropertyValue(normalImagePath));

            // Create focused backgroud visual.
            PropertyMap focusMap = new PropertyMap();
            focusMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            focusMap.Add(ImageVisualProperty.URL, new PropertyValue(focusImagePath));

            // Create pressed backgroud visual.
            PropertyMap pressMap = new PropertyMap();
            pressMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            pressMap.Add(ImageVisualProperty.URL, new PropertyValue(pressImagePath));

            _pushbutton.UnselectedBackgroundVisual = normalMap;
            _pushbutton.SelectedVisual = pressMap;

            // Chang backgroudVisul and Label when focued gained.
            _pushbutton.FocusGained += (obj, e) =>
            {
               _pushbutton.UnselectedBackgroundVisual = focusMap;
                _pushbutton.Label = textVisualFocused;
            };

            // Chang backgroudVisul and Label when focued losted.
            _pushbutton.FocusLost += (obj, e) =>
            {
                _pushbutton.UnselectedBackgroundVisual = normalMap;
                _pushbutton.Label = textVisualNormal;
            };

            // Chang backgroudVisul when pressed.
            _pushbutton.KeyEvent += (obj, ee) =>
            {
                if ("Return" == ee.Key.KeyPressedName)
                {
                    if (Key.StateType.Down == ee.Key.State)
                    {
                        _pushbutton.UnselectedBackgroundVisual = pressMap;
                        Tizen.Log.Fatal("NUI", "Press in pushButton sample!!!!!!!!!!!!!!!!");
                    }
                    else if (Key.StateType.Up == ee.Key.State)
                    {
                        _pushbutton.UnselectedBackgroundVisual = focusMap;
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
            //Create the label which will show when _pushbutton focused.
            textVisualNormal = new PropertyMap();
            textVisualNormal.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            textVisualNormal.Add(TextVisualProperty.Text, new PropertyValue(text));
            textVisualNormal.Add(TextVisualProperty.TextColor, new PropertyValue(Color.White));
            textVisualNormal.Add(TextVisualProperty.PointSize, new PropertyValue(8.0f));
            textVisualNormal.Add(TextVisualProperty.FontFamily, new PropertyValue("SamsungOneUI_400"));
            textVisualNormal.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            textVisualNormal.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));

            //Create the label which will show when _pushbutton unfocused.
            textVisualFocused = new PropertyMap();
            textVisualFocused.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            textVisualFocused.Add(TextVisualProperty.Text, new PropertyValue(text));
            textVisualFocused.Add(TextVisualProperty.FontFamily, new PropertyValue("SamsungOneUI_400"));
            textVisualFocused.Add(TextVisualProperty.TextColor, new PropertyValue(Color.Black));
            textVisualFocused.Add(TextVisualProperty.PointSize, new PropertyValue(8.0f));
            textVisualFocused.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            textVisualFocused.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            if(_pushbutton.HasFocus())
            {
                _pushbutton.Label = textVisualFocused;
            }
            else
            {
                _pushbutton.Label = textVisualNormal;
            }

            
        }

        /// <summary>
        /// Get the initialised pushButton
        /// </summary>
        /// <returns>
        /// The pushButton which be create in this class
        /// </returns>
        public PushButton GetPushButton()
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
        private PropertyMap textVisualNormal;
        private PropertyMap textVisualFocused;
    }
}