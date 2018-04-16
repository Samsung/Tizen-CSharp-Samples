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

        private TextLabel userGuide;
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
            guide.Text = "Slider Sample \n";
            guide.TextColor = Color.White;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            Window.Instance.GetDefaultLayer().Add(guide);

            userGuide = new TextLabel();
            userGuide.HorizontalAlignment = HorizontalAlignment.Begin;
            userGuide.VerticalAlignment = VerticalAlignment.Top;
            userGuide.PositionUsesPivotPoint = true;
            userGuide.ParentOrigin = ParentOrigin.TopLeft;
            userGuide.PivotPoint = PivotPoint.TopLeft;
            userGuide.FontFamily = "Samsung One 400";
            userGuide.Position = new Position(200, 200, 0);
            userGuide.MultiLine = true;
            //userGuide.PointSize = 10.0f;
            userGuide.PointSize = DeviceCheck.PointSize10;
            userGuide.Text = "KEY Left: Decrease the value.\n" +
                             "KEY Right: Increase the value.\n";
            userGuide.TextColor = Color.White;
            Window.Instance.GetDefaultLayer().Add(userGuide);

            Sliders sliderSample = new Sliders();
            sliderView = sliderSample.GetHorizentalSlider();
            sliderView.Position = new Position(760, 550, 0);
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

            Window.Instance.GetDefaultLayer().Remove(userGuide);
            userGuide.Dispose();
            userGuide = null;

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