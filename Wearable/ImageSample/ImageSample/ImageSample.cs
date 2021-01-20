/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd.
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
using System.Runtime.InteropServices;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;
using Tizen;

namespace ImageSample
{
    /// <summary>
    /// A sample demonstrates how to view Image and to use image properties by using ImageView class
    /// </summary>
    class ImageSample : NUIApplication
    {
        /// <summary>
        /// A list of image URL
        /// </summary>
        private static string resourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images";
        // Pixel Area Image Url
        private static string pixelAreaImageUrl = resourceUrl + "/gallery-1.jpg";
        // SVG Image Url
        private static string svgImageUrl = resourceUrl + "/Kid1.svg";
        // GIF Image Url
        private static string gifImageUrl = resourceUrl + "/dog-anim.gif";
        // Nine patch Image Url
        private static string ninePatchImageUrl = resourceUrl + "/heartsframe.9.png";
        // Mask Image Url
        private static string maskImageUrl = resourceUrl + "/mc_playlist_thumbnail_play.png";
        // Mask background Image Url
        private static string maskBackgroundImageUrl = resourceUrl + "/gallery-large-20.jpg";
        // Image Url for the FittingMode
        private static string fittingModeImageUrl = resourceUrl + "/dali-logo.png";

        // A string list of sample cases
        private string[] caseString =
        {
             "Pixel Area",
             "SVG",
             "GIF",
             "Nine Patch",
             "Image Mask",
             "Fitting Mode"
        };

        // A Boolean list for the usage of PushButton
        // - In case of GIF sample, PushButton is not needed.
        private bool[] needButton =
        {
             true,
             true,
             false,
             true,
             true,
             true
        };
        private uint caseCount = 6;
        private int curruntCaseIndex;

        // Main view.
        private View root;

        // A View contains every ImageViews.
        private View imagesView;

        // UI properties
        private Animation[] imageTableViewAnimation;
        private Size imageSize = new Size(150, 150);

        private TableView buttonTableView;

        private Position buttonTableViewStartPosition = new Position(65, 90, 0);
        private Animation[] buttonTableViewAnimation;
        private Size buttonSize = new Size(230, 35);

        private bool touched = false;
        private bool touchedInButton = false;

        private float largePointSize = 10.0f;
        private float middlePointSize = 5.0f;
        private float smallPointSize = 3.0f;
        private Vector2 touchedPosition;

        bool masked = false;
        int fittingMode = 0;

        /// <summary>
        /// The constructor with null
        /// </summary>
        public ImageSample() : base()
        {
        }

        /// <summary>
        /// Overrides this method if want to handle behaviour.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        /// <summary>
        /// ImageView Sample Application initializations.
        /// </summary>
        public void Initialize()
        {
            Window window = NUIApplication.GetDefaultWindow();

            root = new View()
            {
                Size = new Size(window.Size.Width, window.Size.Height),
                BackgroundColor = Color.White
            };

            // Create title TextLabel
            TextLabel title = new TextLabel("Image");
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;
            // Set Text color to White
            title.TextColor = Color.White;
            title.PositionUsesPivotPoint = true;
            title.ParentOrigin = ParentOrigin.TopCenter;
            title.PivotPoint = PivotPoint.TopCenter;
            title.Position = new Position(0, window.Size.Height / 10);
            // Use Samsung One 600 font
            title.FontFamily = "Samsung One 600";
            // Not use MultiLine of TextLabel
            title.MultiLine = false;
            title.PointSize = largePointSize;
            root.Add(title);

            // Create subTitle TextLabel
            TextLabel subTitle = new TextLabel("Swipe and Click the button");
            subTitle.HorizontalAlignment = HorizontalAlignment.Center;
            subTitle.VerticalAlignment = VerticalAlignment.Center;
            // Set Text color to White
            subTitle.TextColor = Color.White;
            subTitle.PositionUsesPivotPoint = true;
            subTitle.ParentOrigin = ParentOrigin.BottomCenter;
            subTitle.PivotPoint = PivotPoint.BottomCenter;
            subTitle.Position = new Position(0, -30);
            // Use Samsung One 600 font
            subTitle.FontFamily = "Samsung One 600";
            // Not use MultiLine of TextLabel
            subTitle.MultiLine = false;
            subTitle.PointSize = smallPointSize;
            root.Add(subTitle);

            // Create each Image sample cases.
            CreateImages();
            // Create Buttons.
            CreateButtons();

            // Animation setting for the image animation
            imageTableViewAnimation = new Animation[2];
            imageTableViewAnimation[0] = new Animation();
            imageTableViewAnimation[0].Duration = 100;
            imageTableViewAnimation[0].AnimateBy(imagesView, "Position", new Vector3(-360, 0, 0));
            imageTableViewAnimation[1] = new Animation();
            imageTableViewAnimation[1].Duration = 100;
            imageTableViewAnimation[1].AnimateBy(imagesView, "Position", new Vector3(360, 0, 0));

            // Animation setting for the button animation
            buttonTableViewAnimation = new Animation[2];
            buttonTableViewAnimation[0] = new Animation();
            buttonTableViewAnimation[0].Duration = 100;
            buttonTableViewAnimation[0].AnimateBy(buttonTableView, "Position", new Vector3(-360, 0, 0));
            buttonTableViewAnimation[1] = new Animation();
            buttonTableViewAnimation[1].Duration = 100;
            buttonTableViewAnimation[1].AnimateBy(buttonTableView, "Position", new Vector3(360, 0, 0));

            window.Add(root);

            // Set callback functions
            window.TouchEvent += OnWindowTouched;
            window.KeyEvent += OnKey;
        }

        /// <summary>
        /// Create Images
        /// </summary>
        private void CreateImages()
        {
            // Construct new View to set each Image.
            imagesView = new View();
            imagesView.PositionUsesPivotPoint = true;
            imagesView.PivotPoint = PivotPoint.CenterLeft;
            imagesView.ParentOrigin = ParentOrigin.CenterLeft;
            // Set imageview size
            imagesView.Size = new Size((int)caseCount * 360, imageSize.Height);
            root.Add(imagesView);

            Position stp = new Position(180, 0, 0);
            for (uint i = 0; i < caseCount; ++i)
            {
                // Create a Image
                View image = CreateImage(caseString[i], stp + new Position(i * 360, 0, 0));
                // Add image to ImagesView
                imagesView.Add(image);
            }
        }

        /// <summary>
        /// Create a Image
        /// </summary>
        /// <param name="text">Case string</param>
        /// <param name="position">A position this view will be located</param>
        /// <returns>View for the created Image</returns>
        private View CreateImage(string text, Position position)
        {
            View view = null;
            if (text == caseString[0])
            {
                TableView pixelAreaTableView = new TableView(2, 2);

                // Create 4 images and add them to tableView
                ImageView[] imageView = new ImageView[4];
                for (uint i = 0; i < 2; i++)
                {
                    for (uint j = 0; j < 2; j++)
                    {
                        // Create new ImageView to contain each part of Image
                        imageView[i * 2 + j] = new ImageView(pixelAreaImageUrl);
                        imageView[i * 2 + j].HeightResizePolicy = ResizePolicyType.FillToParent;
                        imageView[i * 2 + j].WidthResizePolicy = ResizePolicyType.FillToParent;
                        // Put these imageView at different position.
                        pixelAreaTableView.AddChild(imageView[i * 2 + j], new TableView.CellPosition(i, j));
                    }
                }

                view = pixelAreaTableView;
            }
            else if (text == caseString[1])
            {
                //Create an imageView to show the SVG file.
                ImageView imageView = new ImageView(svgImageUrl);
                view = imageView;
            }
            else if (text == caseString[2])
            {
                //Create an imageView to show the GIF file.
                ImageView imageView = new ImageView(gifImageUrl);
                view = imageView;
            }
            else if (text == caseString[3])
            {
                //Create an imageView to show the NinePatch file.
                ImageView imageView = new ImageView(ninePatchImageUrl);
                view = imageView;
            }
            else if (text == caseString[4])
            {
                //Create an imageView to show the Mask.
                ImageView imageView = new ImageView();
                PropertyMap propertyMap = new PropertyMap();
                // Image Visual with MaskBackgroundImage
                // Set Image properties of the Image View
                propertyMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                           .Add(ImageVisualProperty.URL, new PropertyValue(maskBackgroundImageUrl));
                imageView.Image = propertyMap;
                view = imageView;
                masked = false;
            }
            else if (text == caseString[5])
            {
                //Create an imageView to show the Fitting Mode.
                fittingMode = (int)FittingModeType.ShrinkToFit;
                ImageView imageView = new ImageView();
                PropertyMap propertyMap = new PropertyMap();
                // Image Visual with default FittingMode
                // Set Image properties of the Image View
                propertyMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                           .Add(ImageVisualProperty.URL, new PropertyValue(fittingModeImageUrl))
                           .Add(ImageVisualProperty.DesiredWidth, new PropertyValue(imageSize.Width))
                           .Add(ImageVisualProperty.DesiredHeight, new PropertyValue(imageSize.Height))
                           .Add(ImageVisualProperty.FittingMode, new PropertyValue(fittingMode))
                           .Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.Box));
                imageView.Image = propertyMap;
                view = imageView;
            }

            if (view != null) {
                // View position setting
                view.Size = imageSize;
                view.PositionUsesPivotPoint = true;
                view.PivotPoint = PivotPoint.Center;
                view.ParentOrigin = ParentOrigin.CenterLeft;
                view.Position = position;
            }
            return view;
        }

        /// <summary>
        /// Create buttons which control properties of ImageView
        /// </summary>
        private void CreateButtons()
        {
            // Create tableView used to put PushButton.
            buttonTableView = new TableView(1, caseCount);
            // Set the position of tableView.
            buttonTableView.PositionUsesPivotPoint = true;
            buttonTableView.PivotPoint = PivotPoint.CenterLeft;
            buttonTableView.ParentOrigin = ParentOrigin.CenterLeft;
            buttonTableView.Position = buttonTableViewStartPosition;
            // Width of each cell is set to window's width
            for (uint i = 0; i < caseCount; ++i)
            {
                buttonTableView.SetFixedWidth(i, 360);
            }

            root.Add(buttonTableView);

            // Create button for the each case.
            for (uint i = 0; i < caseCount; ++i)
            {
                // Create a button with string.
                Button button = CreateButton(caseString[i], needButton[i]);
                // Bind PushButton's click event to ButtonClick.
                button.TouchEvent += OnButtonTouched;
                button.PointSize = 3;
                buttonTableView.AddChild(button, new TableView.CellPosition(0, i));
            }
        }

        /// <summary>
        /// Create an Text visual.
        /// </summary>
        /// <param name="text">The text of the Text visual</param>
        /// <param name="color">The color of the text</param>
        /// <returns>return a map which contain the properties of the text</returns>
        private PropertyMap CreateTextVisual(string text, Color color)
        {
            PropertyMap map = new PropertyMap();
            // Text Visual
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            map.Add(TextVisualProperty.Text, new PropertyValue(text));
            // Set text color
            map.Add(TextVisualProperty.TextColor, new PropertyValue(color));
            // Set text pointSize
            map.Add(TextVisualProperty.PointSize, new PropertyValue(middlePointSize));
            map.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.FontFamily, new PropertyValue("Samsung One 400"));
            return map;
        }

        /// <summary>
        /// Create an Color visual.
        /// </summary>
        /// <param name="color">The color value of the visual</param>
        /// <returns>return a map which contain the properties of the color</returns>
        private PropertyMap CreateColorVisual(Vector4 color)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Color));
            map.Add(ColorVisualProperty.MixColor, new PropertyValue(color));
            return map;
        }

        /// <summary>
        /// Create an Button.
        /// </summary>
        /// <param name="text">The string to use button's name and Label text</param>
        /// <param name="needButton">If this argument is false, the created button do not show any action</param>
        /// <returns>return a PushButton</returns>
        private Button CreateButton(string text, bool needButton)
        {
            Button button = new Button();
            button.Name = text;
            button.Size = buttonSize;
            button.ClearBackground();

            button.Position = new Position(50, 0, 0);

            // Set each label and text properties.
            button.Text = text;
            button.TextColor = Color.White;
            if (button.IsSelected)
            {
                button.BackgroundColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
            }
            else 
            {
                button.BackgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.9f);
            }

            return button;
        }

        /// <summary>
        /// Touch event handling of Window
        /// </summary>
        /// <param name="sender">Window</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private void OnWindowTouched(object sender, Window.TouchEventArgs e)
        {
            if (e.Touch.GetPointCount() < 1)
            {
                return;
            }

            switch (e.Touch.GetState(0))
            {
                // If State is Down (Touched at the outside of Button)
                // - Store touched position.
                // - Set the touched to true
                // - Set the touchedInButton to false
                case PointStateType.Down:
                {
                    touchedPosition = e.Touch.GetScreenPosition(0);
                    touched = true;
                    touchedInButton = false;
                    break;
                }
                // If State is Motion
                // - Check the touched position is in the touchable position.
                // - Check the Motion is about Horizontal movement.
                // - If the amount of movement is larger than threshold, run the swipe animation(left or right).
                case PointStateType.Motion:
                {
                    if (!touched)
                    {
                        break;
                    }

                    // If the vertical movement is large, the gesture is ignored.
                    Vector2 displacement = e.Touch.GetScreenPosition(0) - touchedPosition;
                    if (Math.Abs(displacement.Y) > 20)
                    {
                        touched = false;
                        break;
                    }
                    // If displacement is larger than threshold
                    // Play Negative directional animation.
                    if (displacement.X > 30)
                    {
                        AnimateAStepNegative();
                        touched = false;
                    }
                    // If displacement is smaller than threshold
                    // Play Positive directional animation.
                    if (displacement.X < -30)
                    {
                        AnimateAStepPositive();
                        touched = false;
                    }

                    break;
                }
                // If State is Up
                // - Reset the touched flag
                case PointStateType.Up:
                {
                    touched = false;
                    break;
                }
            }
        }

        /// <summary>
        /// TouchEvent handling of Button
        /// </summary>
        /// <param name="source">The Touched button</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool OnButtonTouched(object source, View.TouchEventArgs e)
        {
            if (e.Touch.GetPointCount() < 1)
            {
                return true;
            }

            switch (e.Touch.GetState(0))
            {
                // If State is Down (Touched at the inside of Button)
                // - Store touched position.
                // - Set the touched to true
                // - Set the touchedInButton to true
                case PointStateType.Down:
                {
                    touchedPosition = e.Touch.GetScreenPosition(0);
                    touched = true;
                    touchedInButton = true;
                    break;
                }
                // If State is Motion
                // - Check the touched position is in the touchable position.
                // - Check the Motion is about Horizontal movement.
                // - If the amount of movement is larger than threshold, run the swipe animation(left or right).
                case PointStateType.Motion:
                {
                    if (!touched)
                    {
                        break;
                    }

                    // If the vertical movement is large, the gesture is ignored.
                    Vector2 displacement = e.Touch.GetScreenPosition(0) - touchedPosition;
                    if (Math.Abs(displacement.Y) > 20)
                    {
                        touched = false;
                        break;
                    }

                    // If displacement is larger than threshold
                    // Play Negative directional animation.
                    if (displacement.X > 30)
                    {
                        AnimateAStepNegative();
                        touched = false;
                    }
                    // If displacement is smaller than threshold
                    // Play Positive directional animation.
                    if (displacement.X < -30)
                    {
                        AnimateAStepPositive();
                        touched = false;
                    }

                    break;
                }
                // If State is Up
                // - If both of touched and touchedInButton flags are true, run the ButtonClick function.
                // - Reset the touched flag
                case PointStateType.Up:
                {
                    if (touched && touchedInButton)
                    {
                        ButtonClick(source);
                    }

                    touched = false;
                    break;
                }
            }

            return true;
        }

        /// <summary>
        /// Animate the tableView to the Negative direction
        /// </summary>
        private void AnimateAStepNegative()
        {
            // If the state is not the first one, move ImageViews and PushButton a step.
            if (curruntCaseIndex > 0)
            {
                curruntCaseIndex--;

                imageTableViewAnimation[1].Play();
                buttonTableViewAnimation[1].Play();
            }
        }

        /// <summary>
        /// Animate the tableView to the Positive direction
        /// </summary>
        private void AnimateAStepPositive()
        {

            // If the state is not the last one, move ImageViews and PushButton a step.
            if (curruntCaseIndex < caseCount - 1)
            {
                curruntCaseIndex++;

                imageTableViewAnimation[0].Play();
                buttonTableViewAnimation[0].Play();
            }
        }

        /// <summary>
        /// Called by buttons
        /// </summary>
        /// <param name="source">The Touched button</param>
        /// <returns>The consume flag</returns>
        private bool ButtonClick(object source)
        {
            // If each button is clicked,
            // check what the button is clicked
            // and change the properties.
            Button button = source as Button;
            if (button.Name == caseString[0])
            {
                Animation animation = new Animation(2000);
                for (uint i = 0; i < 2; i++)
                {
                    for (uint j = 0; j < 2; j++)
                    {
                        // animate the pixel area property on image view,
                        // the animatable pixel area property is registered on the actor,
                        // which overwrites the property on the renderer
                        TableView tableView = (TableView)imagesView.GetChildAt(0);
                        animation.AnimateTo(tableView.GetChildAt(new TableView.CellPosition(j, i)), "pixelArea", new Vector4(0.33f * i, 0.33f * j, 0.33f, 0.33f), new AlphaFunction(AlphaFunction.BuiltinFunctions.Bounce));
                    }
                }
                // play animation.
                animation.Play();
            }
            else if (button.Name == caseString[1])
            {
                // Change the SVG image size to show the characteristics of SVG.
                if (((ImageView)imagesView.GetChildAt(1)).Size.Width == imageSize.Width * 4 / 3)
                {
                    // Change the LabelText.
                    button.Text = "SVG, Zoom Out";
                    // Size down the SVG image.
                    ((ImageView)imagesView.GetChildAt(1)).Size = imageSize;
                }
                else
                {
                    // Change the LabelText.
                    button.Text = "SVG, Zoom In";
                    // Size up the SVG image.
                    ((ImageView)imagesView.GetChildAt(1)).Size = imageSize * 4 / 3;
                }
            }
            else if (button.Name == caseString[3])
            {
                // Change the nine patch image size to show the characteristics of nine patch.
                if (((ImageView)imagesView.GetChildAt(3)).Size.Width == imageSize.Width * 4 / 3)
                {
                    // Change the LabelText.
                    button.Text = "Nine Patch, Zoom Out";
                    // Size down the nine patch image.
                    ((ImageView)imagesView.GetChildAt(3)).Size = imageSize;
                }
                else
                {
                    // Change the LabelText.
                    button.Text = "Nine Patch, Zoom In";
                    // Size up the nine patch image.
                    ((ImageView)imagesView.GetChildAt(3)).Size = imageSize * 4 / 3;
                }
            }
            else if (button.Name == caseString[4])
            {
                // Create new PropertyMap to show the change(on/off) of mask Property.
                if (masked)
                {
                    // Create new PropertyMap with Image Mask off.
                    button.Text = "Image Mask, Off";
                    PropertyMap propertyMap = new PropertyMap();
                    propertyMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                               .Add(ImageVisualProperty.URL, new PropertyValue(maskBackgroundImageUrl));
                    ((ImageView)imagesView.GetChildAt(4)).Image = propertyMap;
                }
                else
                {
                    // Create new PropertyMap with Image Mask on.
                    button.Text = "Image Mask, On";
                    PropertyMap propertyMap = new PropertyMap();
                    // Set the AlphaMaskURL properties as mask image's Url
                    propertyMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                               .Add(ImageVisualProperty.URL, new PropertyValue(maskBackgroundImageUrl))
                               .Add(ImageVisualProperty.AlphaMaskURL, new PropertyValue(maskImageUrl));
                    ((ImageView)imagesView.GetChildAt(4)).Image = propertyMap;
                }

                masked = !masked;
            }
            else if (button.Name == caseString[5])
            {
                // Create new PropertyMap that contain information of fittingMode.
                // ShrinkToFit = 0,
                // ScaleToFill = 1
                // FitWidth = 2
                // FitHeight = 3
                PropertyMap propertyMap = new PropertyMap();
                propertyMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                           .Add(ImageVisualProperty.URL, new PropertyValue(fittingModeImageUrl))
                           .Add(ImageVisualProperty.DesiredWidth, new PropertyValue(imageSize.Width))
                           .Add(ImageVisualProperty.DesiredHeight, new PropertyValue(imageSize.Height))
                           .Add(ImageVisualProperty.FittingMode, new PropertyValue((++fittingMode) % 4))
                           .Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.Box));
                ((ImageView)imagesView.GetChildAt(5)).Image = propertyMap;
            }

            return false;
        }

        /// <summary>
        /// This Application will be exited when back key entered.
        /// </summary>
        /// <param name="sender">Window.Instance</param>
        /// <param name="e">event</param>
        private void OnKey(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "XF86Back")
                {
                    this.Exit();
                }
            }
        }

        /// <summary>
        /// The enter point of the application.
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[] args)
        {
            Log.Info("Tag", "========== Hello, ImageSample ==========");
            new ImageSample().Run(args);
        }
    }
}
