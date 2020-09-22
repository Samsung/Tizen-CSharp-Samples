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
    /// A sample to show the gif, gif is supported in dali.
    /// </summary>
    class GifSample : IExample
    {
        private TextLabel guide;
        private const string resources = "/home/owner/apps_rw/org.tizen.example.ImageSample/res/images/";
        private String image_gif = resources + "dog-anim.gif";
        private ImageView animatedImage;

        /// <summary>
        /// The constructor
        /// </summary>
        public GifSample()
        {
        }

        /// <summary>
        /// Text Sample Application initialisation.
        /// </summary>
        private void Initialize()
        {
            Window.Instance.BackgroundColor = Color.Black;
            //Create a title to show the sample name.
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
            guide.Text = "Gif Sample \n";
            guide.TextColor = Color.White;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            Window.Instance.GetDefaultLayer().Add(guide);

            // Create a imageView who will show gif image
            animatedImage = new ImageView();
            animatedImage.SizeWidth = 200;
            animatedImage.SizeHeight = 200;
            animatedImage.PositionUsesPivotPoint = true;
            animatedImage.PivotPoint = PivotPoint.TopLeft;
            animatedImage.ParentOrigin = ParentOrigin.TopLeft;
            animatedImage.Position2D = new Position2D(860, 490);
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.AnimatedImage))
               .Add(ImageVisualProperty.URL, new PropertyValue(image_gif))
               .Add(ImageVisualProperty.WrapModeU, new PropertyValue((int)WrapModeType.Repeat))
               .Add(ImageVisualProperty.WrapModeV, new PropertyValue((int)WrapModeType.Default));
            animatedImage.Image = map;
            Window.Instance.GetDefaultLayer().Add(animatedImage);
        }

        /// <summary>
        /// Dispose checkBoxGroup and guide.
        /// </summary>
        public void Deactivate()
        {

            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;

            Window.Instance.GetDefaultLayer().Remove(animatedImage);
            animatedImage.Dispose();
            animatedImage = null;
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