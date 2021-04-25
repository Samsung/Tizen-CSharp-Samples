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
using Tizen.NUI;
using Tizen.NUI.Components;

namespace UIControlSample
{
    /// <summary>
    /// A sample of checkboxbutton
    /// </summary>
    public class CheckBox
    {
         private Tizen.NUI.Components.CheckBox _checkboxbutton;
         private const string resources = "/home/owner/apps_rw/org.tizen.example.UIControlSample/res/images";
         private string normalImagePath = resources + "/CheckBox/Unselected.png";
         private string focusImagePath = resources + "/CheckBox/Focused_01.png";
         private string selectImagePath = resources + "/CheckBox/Selected.png";
         private string focusSelectImagePath = resources + "/CheckBox/Focused_02.png";

         /// <summary>
         /// Constructor to create new CheckBoxButtonSample with text
         /// </summary>
         /// <param name="text">text</param>
         public CheckBox(string text)
         {
             OnIntialize(text);
         }

         /// <summary>
         /// checkboxbutton initialisation.
         /// </summary>
         /// <param name="text">text</param>
         private void OnIntialize(string text)
         {
            _checkboxbutton = new Tizen.NUI.Components.CheckBox();
            _checkboxbutton.Text = text;
            _checkboxbutton.TextColor = Color.White;
            _checkboxbutton.PointSize = DeviceCheck.PointSize8;
            _checkboxbutton.TextPadding = new Extents(20, 12, 0, 0);
            _checkboxbutton.Size2D = new Size2D(300, 48);
            _checkboxbutton.Focusable = true;
            _checkboxbutton.ParentOrigin = ParentOrigin.TopLeft;
            _checkboxbutton.PivotPoint = PivotPoint.TopLeft;
            _checkboxbutton.CheckImageURLSelector = new StringSelector()
            {
                Normal = normalImagePath,
                Selected = selectImagePath,
                Focused = focusImagePath,
                SelectedFocused = focusSelectImagePath
            };
         }

        /// <summary>
        /// Create a PropertyMap which be used to set _checkboxbutton.Label
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
            textVisual.Add(TextVisualProperty.TextColor, new PropertyValue(Color.White));
            //textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(8.0f));
            textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(DeviceCheck.PointSize8));
            textVisual.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("TOP"));
            return textVisual;
        }

        /// <summary>
        /// Get the initialized CheckBoxButton
        /// </summary>
        /// <returns>
        /// The checkboxbutton which be created in this class
        /// </returns>
        public Tizen.NUI.Components.CheckBox GetCheckBoxButton()
        {
             return _checkboxbutton;
        }
    }
}