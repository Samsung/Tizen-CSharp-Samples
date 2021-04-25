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
    /// A sample to show the pixel area feature.
    /// </summary>
    class PixelAreaSample : IExample
    {
        private TextLabel guide;
        private Button pushButton;
        private ImageView[] imageView;
        private TableView tableView;

        private const string resources = "/home/owner/apps_rw/org.tizen.example.ImageSample/res/images/";
        private string normalImagePath = resources + "Button/btn_bg_25_25_25_95.9.png";
        private string focusImagePath = resources + "Button/btn_bg_255_255_255_200.9.png";
        private string pressImagePath = resources + "Button/btn_bg_0_129_198_100.9.png";
        private String image_jpg = resources + "gallery-1.jpg";

        /// <summary>
        /// The constructor
        /// </summary>
        public PixelAreaSample()
        {
        }

        /// <summary>
        /// Text Sample Application initialisation.
        /// </summary>
        private void Initialize()
        {
            Window.Instance.BackgroundColor = Color.Black;
            //Title to show the sampel name
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
            guide.Text = "PixelArea Sample \n";
            guide.TextColor = Color.White;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            Window.Instance.GetDefaultLayer().Add(guide);

            //Create one tableView as a container of all the image.
            tableView = new TableView(3, 3);
            tableView.Size2D = new Size2D(600, 600);
            tableView.PositionUsesPivotPoint = true;
            tableView.PivotPoint = PivotPoint.TopLeft;
            tableView.ParentOrigin = ParentOrigin.TopLeft;
            tableView.Position2D = new Position2D(660, 275);
            Window.Instance.GetDefaultLayer().Add(tableView);

            //Create p image and add them to tableView
            imageView = new ImageView[9];
            for (uint i = 0; i < 3; i++)
            {
                for (uint j = 0; j < 3; j++)
                {
                    imageView[i * 3 + j] = new ImageView(image_jpg);
                    imageView[i * 3 + j].HeightResizePolicy = ResizePolicyType.FillToParent;
                    imageView[i * 3 + j].WidthResizePolicy = ResizePolicyType.FillToParent;
                    // Put these imageView at different position.
                    tableView.AddChild(imageView[i * 3 + j], new TableView.CellPosition(i, j));
                }
            }

            //Create button to trrigger the PixelArea animation
            pushButton = CreateButton("PixelArea", "PixelArea");
            pushButton.PivotPoint = PivotPoint.Center;
            pushButton.ParentOrigin = ParentOrigin.TopLeft;
            pushButton.Position2D = tableView.Position2D + new Position2D((int)tableView.SizeWidth / 2 - 200, (int)tableView.SizeHeight + 60);
            pushButton.ClickEvent += PixelAreaButtonClick;
            pushButton.Focusable = true;
            Window.Instance.GetDefaultLayer().Add(pushButton);
            FocusManager.Instance.SetCurrentFocusView(pushButton);
        }

        /// <summary>
        /// The event will be triggered when pushButton clicked.
        /// Play the pixelArea animation of imageView.
        /// </summary>
        /// <param name="source">pushButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private void PixelAreaButtonClick(object source, EventArgs e)
        {
            Animation animation = new Animation(10000);
            for (uint i = 0; i < 3; i++)
            {
                for (uint j = 0; j < 3; j++)
                {
                    // animate the pixel area property on image view,
                    // the animatable pixel area property is registered on the actor,
                    // which overwrites the property on the renderer
                    animation.AnimateTo(imageView[j * 3 + i], "pixelArea", new Vector4(0.33f * i, 0.33f * j, 0.33f, 0.33f), new AlphaFunction(AlphaFunction.BuiltinFunctions.Bounce));
                }
            }

            animation.Play();
        }



        /// <summary>
        /// Remove all the view from window. and Dispose puahButton, imageViews, tableView and guide.
        /// </summary>
        public void Deactivate()
        {

            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;

            Window.Instance.GetDefaultLayer().Remove(pushButton);
            pushButton.Dispose();
            pushButton = null;

            for (uint i = 0; i < 3; i++)
            {
                for (uint j = 0; j < 3; j++)
                {
                    tableView.Remove(imageView[i * 3 + j]);
                    imageView[i * 3 + j].Dispose();
                    imageView[i * 3 + j] = null;
                }
            }

            Window.Instance.GetDefaultLayer().Remove(tableView);
            tableView.Dispose();
            tableView = null;
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
            button.BackgroundImage = normalImagePath;
            button.TextColor = Color.White;

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
        /// Activate this class.
        /// </summary>
        public void Activate()
        {
            Tizen.Log.Fatal("NUI", "Activate");
            Initialize();
        }
    }
}