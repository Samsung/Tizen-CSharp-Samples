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
    /// A sample of mask, add the mask on a uisual image.
    /// </summary>
    class MaskSample : IExample
    {
        private const string resources = "/home/owner/apps_rw/org.tizen.example.ImageSample/res/images/";
        private string maskPath = resources + "mc_playlist_thumbnail_play.png";
        private string imagePath = resources + "gallery-large-20.jpg";

        private TextLabel guide, userGuide;
        private ImageView bgImage;
        private ImageView imageView;

        /// <summary>
        /// The constructor
        /// </summary>
        public MaskSample()
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
            guide.Text = "Mask Sample \n";
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
            userGuide.Text = "The following two ImageView are created with the same image file, and right one with mask effect.\n";
            userGuide.TextColor = Color.White;
            Window.Instance.GetDefaultLayer().Add(userGuide);


            //Create ImageView to show the normal image
            imageView = new ImageView();
            imageView.Size2D = new Size2D(418, 418);
            imageView.Position = new Position(400, 380, 0);
            imageView.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            imageView.Opacity = 1.0f;
            PropertyMap propertyMap = new PropertyMap();
            propertyMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                      .Add(ImageVisualProperty.URL, new PropertyValue(imagePath))
                      .Add(ImageVisualProperty.DesiredHeight, new PropertyValue(418))
                      .Add(ImageVisualProperty.DesiredWidth, new PropertyValue(418));
            imageView.Image = propertyMap;
            //Add the image to window
            Window.Instance.GetDefaultLayer().Add(imageView);

            //Create image to show the image which with mask effect.
            bgImage = new ImageView();
            bgImage.Size2D = new Size2D(418, 418);
            bgImage.Position = new Position(1102, 380, 0);
            bgImage.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;
            bgImage.Opacity = 1.0f;
            PropertyMap maskMap = new PropertyMap();
            maskMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                      .Add(ImageVisualProperty.URL, new PropertyValue(imagePath))
                      .Add(ImageVisualProperty.AlphaMaskURL, new PropertyValue(maskPath))
                      .Add(ImageVisualProperty.DesiredHeight, new PropertyValue(418))
                      .Add(ImageVisualProperty.DesiredWidth, new PropertyValue(418));
            bgImage.Image = maskMap;
            //Add the image to window.
            Window.Instance.GetDefaultLayer().Add(bgImage);
        }

        /// <summary>
        /// Dispose the two imageView and guide.
        /// </summary>
        public void Deactivate()
        {

            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;

            Window.Instance.GetDefaultLayer().Remove(userGuide);
            userGuide.Dispose();
            userGuide = null;

            Window.Instance.GetDefaultLayer().Remove(imageView);
            imageView.Dispose();
            imageView = null;

            Window.Instance.GetDefaultLayer().Remove(bgImage);
            bgImage.Dispose();
            bgImage = null;
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