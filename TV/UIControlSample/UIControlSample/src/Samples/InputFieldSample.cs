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
    /// A sample of InputFieldSample
    /// </summary>
    class InputFieldSample : IExample
    {
        private TextField textField;
        private View textView;
        private TextLabel guide;
        /// <summary>
        /// The constructor
        /// </summary>
        public InputFieldSample()
        {
        }

        /// <summary>
        /// Text Sample Application initialisation.
        /// </summary>
        private void Initialize()
        {
            Window.Instance.BackgroundColor = Color.Black;

            guide = new TextLabel();
            guide.HorizontalAlignment = HorizontalAlignment.Begin;
            guide.VerticalAlignment = VerticalAlignment.Center;
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = ParentOrigin.TopLeft;
            guide.PivotPoint = PivotPoint.TopLeft;
            guide.Position = new Position(100, 100, 0);
            guide.MultiLine = true;
            guide.PointSize = 10.0f;
            guide.Text = "InputField Sample Guide\n" +
                            "Please press Esc + Backspace to go back to sample main after finish test.\n";
            guide.TextColor = Color.White;
            Window.Instance.GetDefaultLayer().Add(guide);

            InputField textFieldSample = new InputField();
            textView = textFieldSample.GetView();
            textView.Position = new Position(500, 400, 0);
            textField = textFieldSample.GetTextField();
            Window.Instance.GetDefaultLayer().Add(textView);
            FocusManager.Instance.SetCurrentFocusView(textField);
        }

        /// <summary>
        /// Dispose textView
        /// </summary>
        public void Deactivate()
        {
            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;
            Window.Instance.GetDefaultLayer().Remove(textView);
            textView.Dispose();
            textView = null;
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