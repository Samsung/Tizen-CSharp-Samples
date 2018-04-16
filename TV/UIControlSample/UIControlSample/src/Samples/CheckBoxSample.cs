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
        private TextLabel testText;

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
            Window.Instance.BackgroundColor = Color.Black;
            guide = new TextLabel();
            guide.HorizontalAlignment = HorizontalAlignment.Center;
            guide.VerticalAlignment = VerticalAlignment.Center;
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = ParentOrigin.TopLeft;
            guide.PivotPoint = PivotPoint.TopLeft;
            guide.Size2D = new Size2D(1920, 96);
            guide.FontFamily = "Samsung One 600";
            guide.Position2D = new Position2D(0, 94);
            guide.MultiLine = false;
            guide.PointSize = DeviceCheck.PointSize15;
            //guide.PointSize = 15.0f;
            guide.Text = "CheckBox Sample \n";
            guide.TextColor = Color.White;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            Window.Instance.GetDefaultLayer().Add(guide);

            testText = new TextLabel();
            testText.HorizontalAlignment = HorizontalAlignment.Center;
            testText.VerticalAlignment = VerticalAlignment.Center;
            testText.PositionUsesPivotPoint = true;
            testText.ParentOrigin = ParentOrigin.TopLeft;
            testText.PivotPoint = PivotPoint.TopLeft;
            testText.Size2D = new Size2D(300, 100);
            testText.FontFamily = "Samsung One 600";
            testText.Position2D = new Position2D(810, 200);
            testText.MultiLine = false;
            //testText.PointSize = 15.0f;
            testText.PointSize = DeviceCheck.PointSize15;
            testText.Text = "Test Text \n";
            testText.TextColor = Color.Black;
            testText.BackgroundColor = Color.White;
            Window.Instance.GetDefaultLayer().Add(testText);


            CheckBoxGroup checkBoxGroup = new CheckBoxGroup();
            checkBoxGroupView = checkBoxGroup.GetCheckBoxGroup();
            CheckBoxButton checkbox1 = checkBoxGroup.GetCheckBox1();
            CheckBoxButton checkbox2 = checkBoxGroup.GetCheckBox2();
            CheckBoxButton checkbox3 = checkBoxGroup.GetCheckBox3();
            checkbox1.StateChanged += CheckBoxStateChanged;
            checkbox2.StateChanged += CheckBoxStateChanged;
            checkbox3.StateChanged += CheckBoxStateChanged;
            checkBoxGroupView.Position = new Position(810, 400, 0);
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

            Window.Instance.GetDefaultLayer().Remove(testText);
            testText.Dispose();
            testText = null;

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
            // Show testText's shadow or not.
            if (checkbox.LabelText == "Shadow")
            {
                if (checkbox.Selected)
                {
                    testText.ShadowOffset = new Vector2(3.0f, 3.0f);
                    testText.ShadowColor = Color.Black;
                    testText.ShadowOffset = new Vector2(3.0f, 3.0f);
                    testText.ShadowColor = Color.Black;
                }
                else
                {
                    testText.ShadowOffset = new Vector2(0, 0);
                    testText.ShadowOffset = new Vector2(0, 0);
                }
            }
            // The testText auto scroll or not.
            else if (checkbox.LabelText == "Color")
            {
                if (checkbox.Selected)
                {
                    testText.TextColor = Color.Red;
                }
                else
                {
                    testText.TextColor = Color.White;
                }
            }
            // Show testText's underline or not.
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

                testText.Underline = underlineMapSet;
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