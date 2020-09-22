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
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace ImageSample
{
    /// <summary>
    /// A sample to show the svg file.
    /// </summary>
    class SvgSample : IExample
    {
        private const string resources = "/home/owner/apps_rw/org.tizen.example.ImageSample/res/images/";
        private string normalImagePath = resources + "Button/btn_bg_25_25_25_95.9.png";
        private string focusImagePath = resources + "Button/btn_bg_255_255_255_200.9.png";
        private string pressImagePath = resources + "Button/btn_bg_0_129_198_100.9.png";
        private String image_svg = resources + "Kid1.svg";
        private TextLabel guide;
        private float svgScale = 1;
        private ImageView svgImage;
        private Button zoomInButton, zoomOutButton;
        // <summary>
        /// Constructor to create new RadioButtonSample
        /// </summary>
        public SvgSample()
        {
        }

        /// <summary>
        /// RadioButton initialisation.
        /// </summary>
        private void Initialize()
        {
            Window.Instance.BackgroundColor = Color.Black;
            //Create Title to show the sample name.
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
            guide.Text = "Svg Sample \n";
            guide.TextColor = Color.White;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            Window.Instance.GetDefaultLayer().Add(guide);

            //Create an imageView to show the svg file.
            svgImage = new ImageView(image_svg);
            svgImage.SizeWidth = 200;
            svgImage.SizeHeight = 200;
            svgImage.PositionUsesPivotPoint = true ;
            svgImage.PivotPoint = PivotPoint.TopLeft;
            svgImage.ParentOrigin = ParentOrigin.TopLeft;
            svgImage.Position2D = new Position2D(860, 400);
            Window.Instance.GetDefaultLayer().Add(svgImage);

            // Create zoomInButton which can make svgImage bigger
            zoomInButton = CreateButton("Zoom In", "Zoom In");
            zoomInButton.PositionUsesPivotPoint = true;
            zoomInButton.PivotPoint = PivotPoint.TopLeft;
            zoomInButton.ParentOrigin = ParentOrigin.TopLeft;
            zoomInButton.Position2D = new Position2D(460, 900);
            zoomInButton.ClickEvent += ZoomInButtonClick;
            zoomInButton.Focusable = true;
            Window.Instance.GetDefaultLayer().Add(zoomInButton);

            // Create zoomOutButton which can make svgImage smaller
            zoomOutButton = CreateButton("Zoom Out", "Zoom Out");
            zoomOutButton.PositionUsesPivotPoint = true;
            zoomOutButton.PivotPoint = PivotPoint.TopLeft;
            zoomOutButton.ParentOrigin = ParentOrigin.TopLeft;
            zoomOutButton.Position2D = new Position2D(1060, 900);
            zoomOutButton.ClickEvent += ZoomOutButtonClick;
            zoomOutButton.Focusable = true;
            Window.Instance.GetDefaultLayer().Add(zoomOutButton);

            zoomInButton.RightFocusableView = zoomOutButton;
            zoomOutButton.LeftFocusableView = zoomInButton;

            FocusManager.Instance.SetCurrentFocusView(zoomInButton);
        }

        /// <summary>
        /// Dispose radioButtonGroupView and guide
        /// </summary>
        public void Deactivate()
        {

            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;

            Window.Instance.GetDefaultLayer().Remove(svgImage);
            svgImage.Dispose();
            svgImage = null;

            Window.Instance.GetDefaultLayer().Remove(zoomInButton);
            zoomInButton.Dispose();
            zoomInButton = null;

            Window.Instance.GetDefaultLayer().Remove(zoomOutButton);
            zoomOutButton.Dispose();
            zoomOutButton = null;

        }

        /// <summary>
        /// Activate this class.
        /// </summary>
        public void Activate()
        {
            Initialize();
        }

        /// <summary>
        /// The event will be triggered when  have key clicked and focus on spin.
        /// </summary>
        /// <param name="name">the name of the button.</param>
        /// <param name="text">the text of the button</param>
        /// <returns>The consume flag</returns>
        private Button CreateButton(string name, string text)
        {
            Button button = new Button();
            button.Focusable = true;
            button.Size2D = new Size2D(400, 80);
            button.Focusable = true;
            button.Name = name;
            button.TextColor = Color.White;
            button.BackgroundImage = normalImagePath;

            // Chang background Visual and Label when focus gained.
            button.FocusGained += (obj, e) =>
            {
                button.BackgroundImage = focusImagePath;
                button.TextColor = Color.Black;
            };

            // Chang background Visual and Label when focus lost.
            button.FocusLost += (obj, e) =>
            {
                button.BackgroundImage = normalImagePath;
                button.TextColor = Color.White;
            };

            // Chang background Visual when pressed.
            button.KeyEvent += (obj, ee) =>
            {
                if ("Return" == ee.Key.KeyPressedName)
                {
                    if (Key.StateType.Down == ee.Key.State)
                    {
                        button.BackgroundImage = pressImagePath;
                        Tizen.Log.Fatal("NUI", "Press in pushButton sample!!!!!!!!!!!!!!!!");
                    }
                    else if (Key.StateType.Up == ee.Key.State)
                    {
                        button.BackgroundImage = focusImagePath;
                        Tizen.Log.Fatal("NUI", "Release in pushButton sample!!!!!!!!!!!!!!!!");
                    }
                }

                return false;
            };
            return button;
        }

        /// <summary>
        /// Create an Text visual.
        /// </summary>
        /// <param name="text">The text of the Text visual</param>
        /// <param name="color">The color of the text</param>
        /// <returns>return a map which contain the properties of the text visual</returns>
        private PropertyMap CreateTextVisual(string text, Color color)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            map.Add(TextVisualProperty.Text, new PropertyValue(text));
            map.Add(TextVisualProperty.TextColor, new PropertyValue(color));
            //map.Add(TextVisualProperty.PointSize, new PropertyValue(8.0f));
            map.Add(TextVisualProperty.PointSize, new PropertyValue(DeviceCheck.PointSize8));
            map.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.FontFamily, new PropertyValue("Samsung One 400"));
            return map;
        }

        /// <summary>
        /// Create an Image visual.
        /// </summary>
        /// <param name="imagePath">The url of the image</param>
        /// <returns>return a map which contain the properties of the image visual</returns>
        private PropertyMap CreateImageVisual(string imagePath)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            map.Add(ImageVisualProperty.URL, new PropertyValue(imagePath));
            return map;
        }

        /// <summary>
        /// The event will be triggered when zoomInButton clicked
        /// </summary>
        /// <param name="source">zoomInButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private void ZoomInButtonClick(object source, EventArgs e)
        {
            if (svgScale < 3.45)
            {
                svgScale *= 1.1f;
                svgImage.Size2D = (new Vector2(200, 200)) * svgScale;
            }

            Tizen.Log.Fatal("NUI", "Print the svgScale: " + svgScale);
        }

        /// <summary>
        /// The event will be triggered when zoomOutButton clicked
        /// </summary>
        /// <param name="source">zoomOutButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private void ZoomOutButtonClick(object source, EventArgs e)
        {
            if (svgScale > 0.28)
            {
                svgScale /= 1.1f;
                svgImage.Size2D = (new Vector2(200, 200)) * svgScale;
            }

            Tizen.Log.Fatal("NUI", "Print the svgScale: " + svgScale);
        }
    }
}