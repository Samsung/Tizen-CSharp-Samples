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
    /// A sample of radioButton
    /// </summary>
    class RadioButtonSample : IExample
    {
        private View radioButtonGroupView;

        private TextLabel guide;
        private TextLabel testText;
        // <summary>
        /// Constructor to create new RadioButtonSample
        /// </summary>
        public RadioButtonSample()
        {
        }

        /// <summary>
        /// RadioButton initialisation.
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
            //guide.PointSize = 15.0f;
            guide.PointSize = DeviceCheck.PointSize15;
            guide.Text = "RadioButton Sample \n";
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
            testText.PointSize = 15.0f;
            testText.PointSize = DeviceCheck.PointSize15;
            testText.Text = "Test Text \n";
            testText.TextColor = Color.White;
            //testText.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            Window.Instance.GetDefaultLayer().Add(testText);

            RadioButtonGroup radioButtonGroup = new RadioButtonGroup();
            radioButtonGroupView = radioButtonGroup.GetRadioButtonGroup();
            RadioButton radioButton1 = radioButtonGroup.GetRadioButton1();
            RadioButton radioButton2 = radioButtonGroup.GetRadioButton2();
            RadioButton radioButton3 = radioButtonGroup.GetRadioButton3();
            radioButton1.StateChanged += RadioButtonStateChanged;
            radioButton2.StateChanged += RadioButtonStateChanged;
            radioButton3.StateChanged += RadioButtonStateChanged;
            radioButtonGroupView.Position = new Position(810, 400, 0);
            Window.Instance.GetDefaultLayer().Add(radioButtonGroupView);
            FocusManager.Instance.SetCurrentFocusView(radioButton1);
        }

        /// <summary>
        /// Dispose radioButtonGroupView and guide
        /// </summary>
        public void Deactivate()
        {

            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;

            Window.Instance.GetDefaultLayer().Remove(testText);
            testText.Dispose();
            testText = null;

            Window.Instance.GetDefaultLayer().Remove(radioButtonGroupView);
            radioButtonGroupView.Dispose();
            radioButtonGroupView = null;
        }

        /// <summary>
        /// Callback by radioButton.
        /// </summary>
        /// <param name="source">radioButton.</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool RadioButtonStateChanged(object source, EventArgs e)
        {
            RadioButton radioButton = source as RadioButton;
            // Change the testText's textColor to red.
            if (radioButton.LabelText == "Red")
            {
                if (radioButton.Selected)
                {
                    testText.TextColor = Color.Red;
                }
            }
            // Change the testText's textColor to green.
            else if (radioButton.LabelText == "Green")
            {
                if (radioButton.Selected)
                {
                    testText.TextColor = Color.Green;
                }
            }
            // Change the testText's textColor to blue.
            else if (radioButton.LabelText == "Blue")
            {
                if (radioButton.Selected)
                {
                    testText.TextColor = Color.Blue;
                }
            }
            else
            {
                testText.TextColor = Color.Black;
            }

            return true;
        }

        /// <summary>
        /// Activate this class.
        /// </summary>
        public void Activate()
        {
            Initialize();
        }
    }
}