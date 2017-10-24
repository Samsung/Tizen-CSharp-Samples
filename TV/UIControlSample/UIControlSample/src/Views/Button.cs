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

namespace UIControlSample
{
    /// <summary>
    /// A sample of PushButton
    /// </summary>
    public class Button
    {
        private PushButton _pushbutton;
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
            _pushbutton = new PushButton();
            _pushbutton.Focusable = true;
            _pushbutton.Size2D = new Size2D(300,80);
            _pushbutton.Focusable = true;
            _pushbutton.ParentOrigin = ParentOrigin.TopLeft;
            _pushbutton.PivotPoint = PivotPoint.TopLeft;

            // Create the label which will show when _pushbutton focused.
            PropertyMap textVisualW = new PropertyMap();
            textVisualW.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            textVisualW.Add(TextVisualProperty.Text, new PropertyValue(text));
            textVisualW.Add(TextVisualProperty.TextColor, new PropertyValue(Color.White));
            textVisualW.Add(TextVisualProperty.PointSize, new PropertyValue(8.0f));
            textVisualW.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            textVisualW.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));

            // Create the label which will show when _pushbutton unfocused.
            PropertyMap textVisualB = new PropertyMap();
            textVisualB.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            textVisualB.Add(TextVisualProperty.Text, new PropertyValue(text));
            textVisualB.Add(TextVisualProperty.TextColor, new PropertyValue(Color.Black));
            textVisualB.Add(TextVisualProperty.PointSize, new PropertyValue(8.0f));
            textVisualB.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            textVisualB.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            _pushbutton.Label = textVisualW;

            // Create normal background visual.
            PropertyMap normalMap = new PropertyMap();
            normalMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            normalMap.Add(ImageVisualProperty.URL, new PropertyValue(normalImagePath));

            // Create focused background visual.
            PropertyMap focusMap = new PropertyMap();
            focusMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            focusMap.Add(ImageVisualProperty.URL, new PropertyValue(focusImagePath));

            // Create pressed background visual.
            PropertyMap pressMap = new PropertyMap();
            pressMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            pressMap.Add(ImageVisualProperty.URL, new PropertyValue(pressImagePath));

            _pushbutton.UnselectedBackgroundVisual = normalMap;
            _pushbutton.SelectedVisual = pressMap;

            // Chang background Visual and Label when focus gained.
            _pushbutton.FocusGained += (obj, e) =>
            {
               _pushbutton.UnselectedBackgroundVisual = focusMap;
               _pushbutton.Label = textVisualB;
            };

            // Chang background Visual and Label when focus lost.
            _pushbutton.FocusLost += (obj, e) =>
            {
                _pushbutton.UnselectedBackgroundVisual = normalMap;
                _pushbutton.Label = textVisualW;
            };

            // Chang background Visual when pressed.
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
        /// Get the initialized pushButton
        /// </summary>
        /// <returns>
        /// The pushButton which be create in this class
        /// </returns>
        public PushButton GetPushButton()
        {
            return _pushbutton;
        }
    }
}