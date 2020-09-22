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
    /// A sample of Image FittingMode Property.
    /// </summary>
    class FittingModeSample : IExample
    {
        //image resources
        private const string resources = "/home/owner/apps_rw/org.tizen.example.ImageSample/res/images/";
        private string normalImagePath = resources + "Button/btn_bg_25_25_25_95.9.png";
        private string focusImagePath = resources + "Button/btn_bg_255_255_255_200.9.png";
        private string pressImagePath = resources + "Button/btn_bg_0_129_198_100.9.png";
        private String image_png = resources + "dali-logo.png";
        private Button fittingModeButton;

        private TextLabel guide;
        private ImageView image;
        private PropertyMap pngImageMap;
        private int fittingMode;

        /// <summary>
        /// The constructor
        /// </summary>
        public FittingModeSample()
        {
        }

        /// <summary>
        /// Text Sample Application initialisation.
        /// </summary>
        private void Initialize()
        {
            Window.Instance.BackgroundColor = Color.Black;

            //Title show the sample name.
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
            guide.Text = "FittingMode Sample \n";
            guide.TextColor = Color.White;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            Window.Instance.GetDefaultLayer().Add(guide);

            //Create an imageView instance to show the fittingMode property.
            image = new ImageView();
            image.Size2D = new Size2D(200, 200);
            pngImageMap = new PropertyMap();
            pngImageMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                       .Add(ImageVisualProperty.URL, new PropertyValue(image_png))
                       .Add(ImageVisualProperty.DesiredWidth, new PropertyValue(200))
                       .Add(ImageVisualProperty.DesiredHeight, new PropertyValue(200))
                       .Add(ImageVisualProperty.FittingMode, new PropertyValue((int)FittingModeType.ShrinkToFit))
                       .Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.Box));
            fittingMode = (int)FittingModeType.ShrinkToFit;
            image.Image = pngImageMap;
            image.PositionUsesPivotPoint = true;
            image.PivotPoint = PivotPoint.TopLeft;
            image.ParentOrigin = ParentOrigin.TopLeft;
            image.Position2D = new Position2D(860, 400);
            //Add the imageView to window.
            Window.Instance.GetDefaultLayer().Add(image);


            // Create fittingModeButton which can change image's FittingMode
            fittingModeButton = CreateButton("FittingMode : ShrinkToFit", "FittingMode : ShrinkToFit");
            fittingModeButton.SizeWidth = 600;
            fittingModeButton.PositionUsesPivotPoint = true;
            fittingModeButton.PivotPoint = PivotPoint.TopLeft;
            fittingModeButton.ParentOrigin = ParentOrigin.TopLeft;
            fittingModeButton.Position2D = new Position2D(660, 700);
            //fittingModeButton.Position2D = image.Position2D + new Position2D(-150, (int)image.SizeHeight / 2 + 300);
            fittingModeButton.ClickEvent += FittingModeButtonClick;
            fittingModeButton.Focusable = true;
            Window.Instance.GetDefaultLayer().Add(fittingModeButton);
            FocusManager.Instance.SetCurrentFocusView(fittingModeButton);
        }

        /// <summary>
        /// Dispose checkBoxGroup and guide.
        /// </summary>
        public void Deactivate()
        {

            Window.Instance.GetDefaultLayer().Remove(guide);
            guide.Dispose();
            guide = null;

            Window.Instance.GetDefaultLayer().Remove(image);
            image.Dispose();
            image = null;

            Window.Instance.GetDefaultLayer().Remove(fittingModeButton);
            fittingModeButton.Dispose();
            fittingModeButton = null;
        }

        /// <summary>
        /// The event will be triggered when fittingModeButton clicked
        /// FittingMode converts a scaling mode to the definition of which dimensions matter
        /// when box filtering as a part of that mode.
        //
        /// Shrink to fit attempts to make one or zero dimensions smaller than the
        /// desired dimensions and one or two dimensions exactly the same as the desired
        /// ones, so as long as one dimension is larger than the desired size, box
        /// filtering can continue even if the second dimension is smaller than the
        /// desired dimensions.!
        ///
        /// X Dimension is ignored by definition in FIT_HEIGHT mode
        ///
        /// Y dimension is irrelevant when downscaling in FIT_WIDTH mode
        ///
        /// Scale to fill mode keeps both dimensions at least as large as desired
        /// </summary>
        /// <param name="source">fittingModeButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private void FittingModeButtonClick(object source, EventArgs e)
        {
            switch (fittingMode)
            {
                case (int)FittingModeType.FitHeight:
                    fittingMode = (int)FittingModeType.FitWidth;
                    pngImageMap.Add(ImageVisualProperty.FittingMode, new PropertyValue((int)FittingModeType.FitWidth));
                    fittingModeButton.Text = "FittingMode : FitWidth";
                    break;
                case (int)FittingModeType.FitWidth:
                    fittingMode = (int)FittingModeType.ScaleToFill;
                    pngImageMap.Add(ImageVisualProperty.FittingMode, new PropertyValue((int)FittingModeType.ScaleToFill));
                    fittingModeButton.Text = "FittingMode : ScaleToFill";
                    break;
                case (int)FittingModeType.ScaleToFill:
                    fittingMode = (int)FittingModeType.ShrinkToFit;
                    pngImageMap.Add(ImageVisualProperty.FittingMode, new PropertyValue((int)FittingModeType.ShrinkToFit));
                    fittingModeButton.Text = "FittingMode : ShrinkToFit";
                    break;
                case (int)FittingModeType.ShrinkToFit:
                    fittingMode = (int)FittingModeType.FitHeight;
                    pngImageMap.Add(ImageVisualProperty.FittingMode, new PropertyValue((int)FittingModeType.FitHeight));
                    fittingModeButton.Text = "FittingMode : FitHeight";
                    break;
            }

            image.Image = pngImageMap;
        }

        /// <summary>
        /// Activate this class.
        /// </summary>
        public void Activate()
        {
            Tizen.Log.Fatal("NUI", "Activate");
            Initialize();
        }

        /// <summary>
        /// Create text propertyMap used to set Button.Label
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>The created propertyMap</returns>
        private PropertyMap CreateText(string text)
        {
            PropertyMap textVisual = new PropertyMap();
            textVisual.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            textVisual.Add(TextVisualProperty.Text, new PropertyValue(text));
            textVisual.Add(TextVisualProperty.TextColor, new PropertyValue(Color.Black));
            //textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(8));
            textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(DeviceCheck.PointSize8));
            textVisual.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            textVisual.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            textVisual.Add(TextVisualProperty.FontFamily, new PropertyValue("Samsung One 400"));
            return textVisual;
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

        private Button CreateButton(string name, string text)
        {
            Button button = new Button();
            button.Focusable = true;
            button.Size2D = new Size2D(600, 80);
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
    }
}