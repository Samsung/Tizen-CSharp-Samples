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

namespace ImageSample
{
    /// <summary>
    /// A sample of 9patch, to show ninepatch file is supported in dali.
    /// </summary>
    class NinePatchSample : IExample
    {
        private const string resources = "/home/owner/apps_rw/org.tizen.example.ImageSample/res/images/";
        private string imagePath = resources + "heartsframe.9.png";
        private TextLabel guide, userGuide;

        private ImageView small, middle, large;

        /// <summary>
        /// The constructor
        /// </summary>
        public NinePatchSample()
        {
        }

        /// <summary>
        /// Text Sample Application initialisation.
        /// </summary>
        private void Initialize()
        {
            Window.Instance.BackgroundColor = Color.Black;
            //Title to show the sample name.
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
            guide.Text = "NinePatch Sample \n";
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
            userGuide.Position = new Position(30, 275, 0);
            userGuide.MultiLine = true;
            //userGuide.PointSize = 10.0f;
            userGuide.PointSize = DeviceCheck.PointSize10;
            userGuide.Text = "The following three ImageView are created with the same image file but with different size.\n";
            userGuide.TextColor = Color.White;
            Window.Instance.GetDefaultLayer().Add(userGuide);

            //Create three imageView with different size, samll, middle, and large, to show the same 9patch file.
            //This imageView's size is (124, 85). it's the small one.
            small = new ImageView(imagePath);
            small.ParentOrigin = ParentOrigin.TopLeft;
            small.PivotPoint = PivotPoint.TopLeft;
            small.Size2D = new Size2D(124, 85);
            small.Position2D = new Position2D(898, 330);
            Window.Instance.GetDefaultLayer().Add(small);
            //This imageView's size is (249, 169). it's the middle one.
            middle = new ImageView(imagePath);
            middle.ParentOrigin = ParentOrigin.TopLeft;
            middle.PivotPoint = PivotPoint.TopLeft;
            middle.Size2D = new Size2D(249, 169);
            middle.Position2D = new Position2D(835, 450);
            Window.Instance.GetDefaultLayer().Add(middle);
            //This imageView's size is (498, 338). it's the large one.
            large = new ImageView(imagePath);
            large.ParentOrigin = ParentOrigin.TopLeft;
            large.PivotPoint = PivotPoint.TopLeft;
            large.Size2D = new Size2D(498, 338);
            large.Position2D = new Position2D(711, 670);
            Window.Instance.GetDefaultLayer().Add(large);

        }

        /// <summary>
        /// Remove all the view from window, and Dispose three imageView and guide.
        /// </summary>
        public void Deactivate()
        {

            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;

            Window.Instance.GetDefaultLayer().Remove(userGuide);
            userGuide.Dispose();
            userGuide = null;

            Window.Instance.GetDefaultLayer().Remove(small);
            small.Dispose();
            small = null;

            Window.Instance.GetDefaultLayer().Remove(middle);
            middle.Dispose();
            middle = null;

            Window.Instance.GetDefaultLayer().Remove(large);
            large.Dispose();
            large = null;
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