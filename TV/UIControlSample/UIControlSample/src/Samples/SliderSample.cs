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
    class SliderSample : IExample
    {
        private Slider slider;
        private View sliderView;
        private TextLabel guide;
        /// <summary>
        /// Constructor to create new SliderSample
        /// </summary>
        public SliderSample()
        {
        }

        /// <summary>
        /// RadioButton initialisation.
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
            guide.Position = new Position(650, 200, 0);
            guide.MultiLine = true;
            guide.PointSize = 10.0f;
            guide.Text = "Slider Sample Guide\n" +
                            "KEY Left: Decrease the value.\n" +
                            "KEY Right: Increase the value.\n";
            guide.TextColor = Color.White;
            Window.Instance.GetDefaultLayer().Add(guide);

            Sliders sliderSample = new Sliders();
            sliderView = sliderSample.GetHorizentalSlider();
            sliderView.Position = new Position(650, 600, 0);
            slider = sliderSample.GetSlider();
            Window.Instance.GetDefaultLayer().Add(sliderView);
            FocusManager.Instance.SetCurrentFocusView(slider);
        }

        /// <summary>
        /// Dispose sliderView
        /// </summary>
        public void Deactivate()
        {
            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;
            Window.Instance.GetDefaultLayer().Remove(sliderView);
            sliderView.Dispose();
            sliderView = null;
        }

        /// <summary>
        /// Activate sliderSample
        /// </summary>
        public void Activate()
        {
            Initialize();
        }
    }
}