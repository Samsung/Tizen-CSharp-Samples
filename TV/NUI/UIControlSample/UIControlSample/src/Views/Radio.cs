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
    /// A sample of RadioButton
    /// </summary>
    public class Radio
    {
        private RadioButton _radiobutton;
        private const string resources = "/home/owner/apps_rw/org.tizen.example.UIControlSample/res/images";
        private string normalImagePath = resources + "/RadioButton/Unselected.png";
        private string focusedImagePath = resources + "/RadioButton/Focused_01.png";
        private string focusedSelectImagePath = resources + "/RadioButton/Focused_02.png";
        private string selectImagePath = resources + "/RadioButton/Selected.png";

        /// <summary>
        /// Constructor to create new RadioButtonSample with text
        /// </summary>
        /// <param name="text">text</param>
        public Radio(string text)
        {
            OnIntialize(text);
        }

        /// <summary>
        /// RadioButton initialisation.
        /// </summary>
        /// <param name="text">text</param>
        private void OnIntialize(string text)
        {
            _radiobutton = new RadioButton();
            _radiobutton.Text = text;
            _radiobutton.TextColor = Color.White;
            _radiobutton.PointSize = DeviceCheck.PointSize8;
            _radiobutton.TextPadding = new Extents(20, 12, 0, 0);
            _radiobutton.Size2D = new Size2D(300, 48);
            _radiobutton.Focusable = true;
            _radiobutton.ParentOrigin = ParentOrigin.TopLeft;
            _radiobutton.PivotPoint = PivotPoint.TopLeft;
            _radiobutton.IconURLSelector = new StringSelector()
            {
                Normal = normalImagePath,
                Focused = focusedImagePath,
                Selected = selectImagePath,
                SelectedFocused = focusedSelectImagePath
            };
        }

        /// <summary>
        /// Create a PropertyMap which be used to set _radiobutton.Label
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>
        /// the created PropertyMap
        /// </returns>
        private PropertyMap CreateLabel(string text)
        {
            PropertyMap textVisual = new PropertyMap();
            textVisual.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            textVisual.Add(TextVisualProperty.Text, new PropertyValue(text));
            textVisual.Add(TextVisualProperty.FontFamily, new PropertyValue("Samsung One 400"));
            textVisual.Add(TextVisualProperty.TextColor, new PropertyValue(Color.White));
            //textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(8.0f));
            textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(DeviceCheck.PointSize8));
            textVisual.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("TOP"));
            return textVisual;
        }

        /// <summary>
        /// Get the initialized RadioButton
        /// </summary>
        /// <returns>
        /// The RadioButton which be created in this class
        /// </returns>
        public RadioButton GetRadioButton()
        {
            return _radiobutton;
        }
    }
}