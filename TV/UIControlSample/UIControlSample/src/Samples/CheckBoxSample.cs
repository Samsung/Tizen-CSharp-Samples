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
using Tizen;
using System.Runtime.InteropServices;
using Tizen.NUI;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace UIControlSample
{
    /// <summary>
    /// A sample of CheckBoxButton.
    /// </summary>
    class CheckBoxSample : IExample
    {
        private View checkBoxGroupView;

        private TextLabel guide;

        /// <summary>
        /// The constructor
        /// </summary>
        public CheckBoxSample()
        {
        }

        /// <summary>
        /// Text Sample Application initialisation.
        /// </summary>
        private void Initialize()
        {
            Window.Instance.BackgroundColor = Color.White;
            guide = new TextLabel();
            guide.HorizontalAlignment = HorizontalAlignment.Begin;
            guide.VerticalAlignment = VerticalAlignment.Center;
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = ParentOrigin.TopLeft;
            guide.PivotPoint = PivotPoint.TopLeft;
            guide.Position = new Position(700, 200, 0);
            guide.MultiLine = true;
            guide.PointSize = 20.0f;
            guide.Text = "CheckBox Sample \n";
            guide.TextColor = Color.Black;
            Window.Instance.GetDefaultLayer().Add(guide);

            CheckBoxGroup checkBoxGroup = new CheckBoxGroup();
            checkBoxGroupView = checkBoxGroup.GetCheckBoxGroup();
            CheckBoxButton checkbox1 = checkBoxGroup.GetCheckBox1();
            CheckBoxButton checkbox2 = checkBoxGroup.GetCheckBox2();
            CheckBoxButton checkbox3 = checkBoxGroup.GetCheckBox3();
            checkbox1.StateChanged += CheckBoxStateChanged;
            checkbox2.StateChanged += CheckBoxStateChanged;
            checkbox3.StateChanged += CheckBoxStateChanged;
            checkBoxGroupView.Position = new Position(800, 500, 0);
            Window.Instance.GetDefaultLayer().Add(checkBoxGroupView);
            FocusManager.Instance.SetCurrentFocusView(checkbox1);
        }

        /// <summary>
        /// Dispose checkBoxGroup and guide.
        /// </summary>
        public void Deactivate()
        {

            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;

            Window.Instance.GetDefaultLayer().Remove(checkBoxGroupView);
            checkBoxGroupView.Dispose();
            checkBoxGroupView = null;
        }

        /// <summary>
        /// The event will be triggered when  have key clicked and focus on spin.
        /// </summary>
        /// <param name="source">checkbox.</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool CheckBoxStateChanged(object source, EventArgs e)
        {
            CheckBoxButton checkbox = source as CheckBoxButton;
            // Show guide's shadow or not.
            if (checkbox.LabelText == "Shadow")
            {
                if (checkbox.Selected)
                {
                    guide.ShadowOffset = new Vector2(3.0f, 3.0f);
                    guide.ShadowColor = Color.Black;
                    guide.ShadowOffset = new Vector2(3.0f, 3.0f);
                    guide.ShadowColor = Color.Black;
                }
                else
                {
                    guide.ShadowOffset = new Vector2(0, 0);
                    guide.ShadowOffset = new Vector2(0, 0);
                }
            }
            // The guide auto scroll or not.
            else if (checkbox.LabelText == "Color")
            {
                if (checkbox.Selected)
                {
                    guide.TextColor = Color.Red;
                }
                else
                {
                    guide.TextColor = Color.Blue;
                }
            }
            // Show guide's underline or not.
            else if (checkbox.LabelText == "Underline")
            {
                PropertyMap underlineMapSet = new PropertyMap();
                if (checkbox.Selected)
                {
                    underlineMapSet.Add("enable", new PropertyValue("true"));
                    underlineMapSet.Add("color", new PropertyValue("black"));
                    underlineMapSet.Add("height", new PropertyValue("3.0f"));
                }
                else
                {
                    underlineMapSet.Add("enable", new PropertyValue("false"));
                }

                guide.Underline = underlineMapSet;
            }

            return true;
        }

        /// <summary>
        /// Activate this class.
        /// </summary>
        public void Activate()
        {
            Tizen.Log.Fatal("NUI", "Activate");
            Initialize();
        }
    }
}