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
        private static string mResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images";
        // Pixel Area Image Url
        private static string mPixelAreaImageUrl = mResourceUrl + "/gallery-1.jpg";
        // SVG Image Url
        private static string mSVGImageUrl = mResourceUrl + "/Kid1.svg";
        // GIF Image Url
        private static string mGIFImageUrl = mResourceUrl + "/dog-anim.gif";
        // Nine patch Image Url
        private static string mNinePatchImageUrl = mResourceUrl + "/heartsframe.9.png";
        // Mask Image Url
        private static string mMaskImageUrl = mResourceUrl + "/mc_playlist_thumbnail_play.png";
        // Mask background Image Url
        private static string mMaskBackgroundImageUrl = mResourceUrl + "/gallery-large-20.jpg";
        // Image Url for the FittingMode
        private static string mFittingModeImageUrl = mResourceUrl + "/dali-logo.png";

        // A string list of sample cases
        private string[] mCaseString =
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
        private bool[] mNeedButton =
        {
             true,
             true,
             false,
             true,
             true,
             true
        };
        private uint mCaseCount = 6;
        private int mCurruntCaseIndex;

        // Main view.
        private View root;

        // A View contains every ImageViews.
        private View mImagesView;

        // UI properties
        private Animation[] mImageTableViewAnimation;
        private Size mImageSize = new Size(150, 150);

        private TableView mButtonTableView;

        private Position mButtonTableViewStartPosition = new Position(65, 90, 0);
        private Animation[] mButtonTableViewAnimation;
        private Size mButtonSize = new Size(230, 35);

        private bool mTouched = false;
        private bool mTouchedInButton = false;

        private float mLargePointSize = 10.0f;
        private float mMiddlePointSize = 5.0f;
        private float mSmallPointSize = 3.0f;
        private Vector2 mTouchedPosition;

        bool mMasked = false;
        int mFittingMode = 0;

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
            title.PointSize = mLargePointSize;
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
            subTitle.PointSize = mSmallPointSize;
            root.Add(subTitle);

            // Create each Image sample cases.
            CreateImages();
            // Create Buttons.
            CreateButtons();

            // Animation setting for the image animation
            mImageTableViewAnimation = new Animation[2];
            mImageTableViewAnimation[0] = new Animation();
            mImageTableViewAnimation[0].Duration = 100;
            mImageTableViewAnimation[0].AnimateBy(mImagesView, "Position", new Vector3(-360, 0, 0));
            mImageTableViewAnimation[1] = new Animation();
            mImageTableViewAnimation[1].Duration = 100;
            mImageTableViewAnimation[1].AnimateBy(mImagesView, "Position", new Vector3(360, 0, 0));

            // Animation setting for the button animation
            mButtonTableViewAnimation = new Animation[2];
            mButtonTableViewAnimation[0] = new Animation();
            mButtonTableViewAnimation[0].Duration = 100;
            mButtonTableViewAnimation[0].AnimateBy(mButtonTableView, "Position", new Vector3(-360, 0, 0));
            mButtonTableViewAnimation[1] = new Animation();
            mButtonTableViewAnimation[1].Duration = 100;
            mButtonTableViewAnimation[1].AnimateBy(mButtonTableView, "Position", new Vector3(360, 0, 0));

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
            mImagesView = new View();
            mImagesView.PositionUsesPivotPoint = true;
            mImagesView.PivotPoint = PivotPoint.CenterLeft;
            mImagesView.ParentOrigin = ParentOrigin.CenterLeft;
            // Set imageview size
            mImagesView.Size = new Size((int)mCaseCount * 360, mImageSize.Height);
            root.Add(mImagesView);

            Position stp = new Position(180, 0, 0);
            for (uint i = 0; i < mCaseCount; ++i)
            {
                // Create a Image
                View image = CreateImage(mCaseString[i], stp + new Position(i * 360, 0, 0));
                // Add image to ImagesView
                mImagesView.Add(image);
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
            if (text == mCaseString[0])
            {
                TableView pixelAreaTableView = new TableView(2, 2);

                // Create 4 images and add them to tableView
                ImageView[] imageView = new ImageView[4];
                for (uint i = 0; i < 2; i++)
                {
                    for (uint j = 0; j < 2; j++)
                    {
                        // Create new ImageView to contain each part of Image
                        imageView[i * 2 + j] = new ImageView(mPixelAreaImageUrl);
                        imageView[i * 2 + j].HeightResizePolicy = ResizePolicyType.FillToParent;
                        imageView[i * 2 + j].WidthResizePolicy = ResizePolicyType.FillToParent;
                        // Put these imageView at different position.
                        pixelAreaTableView.AddChild(imageView[i * 2 + j], new TableView.CellPosition(i, j));
                    }
                }

                view = pixelAreaTableView;
            }
            else if (text == mCaseString[1])
            {
                //Create an imageView to show the SVG file.
                ImageView imageView = new ImageView(mSVGImageUrl);
                view = imageView;
            }
            else if (text == mCaseString[2])
            {
                //Create an imageView to show the GIF file.
                ImageView imageView = new ImageView(mGIFImageUrl);
                view = imageView;
            }
            else if (text == mCaseString[3])
            {
                //Create an imageView to show the NinePatch file.
                ImageView imageView = new ImageView(mNinePatchImageUrl);
                view = imageView;
            }
            else if (text == mCaseString[4])
            {
                //Create an imageView to show the Mask.
                ImageView imageView = new ImageView();
                PropertyMap propertyMap = new PropertyMap();
                // Image Visual with MaskBackgroundImage
                // Set Image properties of the Image View
                propertyMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                           .Add(ImageVisualProperty.URL, new PropertyValue(mMaskBackgroundImageUrl));
                imageView.Image = propertyMap;
                view = imageView;
                mMasked = false;
            }
            else if (text == mCaseString[5])
            {
                //Create an imageView to show the Fitting Mode.
                mFittingMode = (int)FittingModeType.ShrinkToFit;
                ImageView imageView = new ImageView();
                PropertyMap propertyMap = new PropertyMap();
                // Image Visual with default FittingMode
                // Set Image properties of the Image View
                propertyMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                           .Add(ImageVisualProperty.URL, new PropertyValue(mFittingModeImageUrl))
                           .Add(ImageVisualProperty.DesiredWidth, new PropertyValue(mImageSize.Width))
                           .Add(ImageVisualProperty.DesiredHeight, new PropertyValue(mImageSize.Height))
                           .Add(ImageVisualProperty.FittingMode, new PropertyValue(mFittingMode))
                           .Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.Box));
                imageView.Image = propertyMap;
                view = imageView;
            }

            if (view != null) {
                // View position setting
                view.Size = mImageSize;
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
            mButtonTableView = new TableView(1, mCaseCount);
            // Set the position of tableView.
            mButtonTableView.PositionUsesPivotPoint = true;
            mButtonTableView.PivotPoint = PivotPoint.CenterLeft;
            mButtonTableView.ParentOrigin = ParentOrigin.CenterLeft;
            mButtonTableView.Position = mButtonTableViewStartPosition;
            // Width of each cell is set to window's width
            for (uint i = 0; i < mCaseCount; ++i)
            {
                mButtonTableView.SetFixedWidth(i, 360);
            }

            root.Add(mButtonTableView);

            // Create button for the each case.
            for (uint i = 0; i < mCaseCount; ++i)
            {
                // Create a button with string.
                Button mButton = CreateButton(mCaseString[i], mNeedButton[i]);
                // Bind PushButton's click event to ButtonClick.
                mButton.TouchEvent += OnButtonTouched;
                mButton.PointSize = 3;
                mButtonTableView.AddChild(mButton, new TableView.CellPosition(0, i));
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
            map.Add(TextVisualProperty.PointSize, new PropertyValue(mMiddlePointSize));
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
            button.Size = mButtonSize;
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
                // - Set the mTouched to true
                // - Set the mTouchedInButton to false
                case PointStateType.Down:
                {
                    mTouchedPosition = e.Touch.GetScreenPosition(0);
                    mTouched = true;
                    mTouchedInButton = false;
                    break;
                }
                // If State is Motion
                // - Check the touched position is in the touchable position.
                // - Check the Motion is about Horizontal movement.
                // - If the amount of movement is larger than threshold, run the swipe animation(left or right).
                case PointStateType.Motion:
                {
                    if (!mTouched)
                    {
                        break;
                    }

                    // If the vertical movement is large, the gesture is ignored.
                    Vector2 displacement = e.Touch.GetScreenPosition(0) - mTouchedPosition;
                    if (Math.Abs(displacement.Y) > 20)
                    {
                        mTouched = false;
                        break;
                    }
                    // If displacement is larger than threshold
                    // Play Negative directional animation.
                    if (displacement.X > 30)
                    {
                        AnimateAStepNegative();
                        mTouched = false;
                    }
                    // If displacement is smaller than threshold
                    // Play Positive directional animation.
                    if (displacement.X < -30)
                    {
                        AnimateAStepPositive();
                        mTouched = false;
                    }

                    break;
                }
                // If State is Up
                // - Reset the mTouched flag
                case PointStateType.Up:
                {
                    mTouched = false;
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
                // - Set the mTouched to true
                // - Set the mTouchedInButton to true
                case PointStateType.Down:
                {
                    mTouchedPosition = e.Touch.GetScreenPosition(0);
                    mTouched = true;
                    mTouchedInButton = true;
                    break;
                }
                // If State is Motion
                // - Check the touched position is in the touchable position.
                // - Check the Motion is about Horizontal movement.
                // - If the amount of movement is larger than threshold, run the swipe animation(left or right).
                case PointStateType.Motion:
                {
                    if (!mTouched)
                    {
                        break;
                    }

                    // If the vertical movement is large, the gesture is ignored.
                    Vector2 displacement = e.Touch.GetScreenPosition(0) - mTouchedPosition;
                    if (Math.Abs(displacement.Y) > 20)
                    {
                        mTouched = false;
                        break;
                    }

                    // If displacement is larger than threshold
                    // Play Negative directional animation.
                    if (displacement.X > 30)
                    {
                        AnimateAStepNegative();
                        mTouched = false;
                    }
                    // If displacement is smaller than threshold
                    // Play Positive directional animation.
                    if (displacement.X < -30)
                    {
                        AnimateAStepPositive();
                        mTouched = false;
                    }

                    break;
                }
                // If State is Up
                // - If both of mTouched and mTouchedInButton flags are true, run the ButtonClick function.
                // - Reset the mTouched flag
                case PointStateType.Up:
                {
                    if (mTouched && mTouchedInButton)
                    {
                        ButtonClick(source);
                    }

                    mTouched = false;
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
            if (mCurruntCaseIndex > 0)
            {
                mCurruntCaseIndex--;

                mImageTableViewAnimation[1].Play();
                mButtonTableViewAnimation[1].Play();
            }
        }

        /// <summary>
        /// Animate the tableView to the Positive direction
        /// </summary>
        private void AnimateAStepPositive()
        {

            // If the state is not the last one, move ImageViews and PushButton a step.
            if (mCurruntCaseIndex < mCaseCount - 1)
            {
                mCurruntCaseIndex++;

                mImageTableViewAnimation[0].Play();
                mButtonTableViewAnimation[0].Play();
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
            if (button.Name == mCaseString[0])
            {
                Animation animation = new Animation(2000);
                for (uint i = 0; i < 2; i++)
                {
                    for (uint j = 0; j < 2; j++)
                    {
                        // animate the pixel area property on image view,
                        // the animatable pixel area property is registered on the actor,
                        // which overwrites the property on the renderer
                        TableView tableView = (TableView)mImagesView.GetChildAt(0);
                        animation.AnimateTo(tableView.GetChildAt(new TableView.CellPosition(j, i)), "pixelArea", new Vector4(0.33f * i, 0.33f * j, 0.33f, 0.33f), new AlphaFunction(AlphaFunction.BuiltinFunctions.Bounce));
                    }
                }
                // play animation.
                animation.Play();
            }
            else if (button.Name == mCaseString[1])
            {
                // Change the SVG image size to show the characteristics of SVG.
                if (((ImageView)mImagesView.GetChildAt(1)).Size.Width == mImageSize.Width * 4 / 3)
                {
                    // Change the LabelText.
                    button.Text = "SVG, Zoom Out";
                    // Size down the SVG image.
                    ((ImageView)mImagesView.GetChildAt(1)).Size = mImageSize;
                }
                else
                {
                    // Change the LabelText.
                    button.Text = "SVG, Zoom In";
                    // Size up the SVG image.
                    ((ImageView)mImagesView.GetChildAt(1)).Size = mImageSize * 4 / 3;
                }
            }
            else if (button.Name == mCaseString[3])
            {
                // Change the nine patch image size to show the characteristics of nine patch.
                if (((ImageView)mImagesView.GetChildAt(3)).Size.Width == mImageSize.Width * 4 / 3)
                {
                    // Change the LabelText.
                    button.Text = "Nine Patch, Zoom Out";
                    // Size down the nine patch image.
                    ((ImageView)mImagesView.GetChildAt(3)).Size = mImageSize;
                }
                else
                {
                    // Change the LabelText.
                    button.Text = "Nine Patch, Zoom In";
                    // Size up the nine patch image.
                    ((ImageView)mImagesView.GetChildAt(3)).Size = mImageSize * 4 / 3;
                }
            }
            else if (button.Name == mCaseString[4])
            {
                // Create new PropertyMap to show the change(on/off) of mask Property.
                if (mMasked)
                {
                    // Create new PropertyMap with Image Mask off.
                    button.Text = "Image Mask, Off";
                    PropertyMap propertyMap = new PropertyMap();
                    propertyMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                               .Add(ImageVisualProperty.URL, new PropertyValue(mMaskBackgroundImageUrl));
                    ((ImageView)mImagesView.GetChildAt(4)).Image = propertyMap;
                }
                else
                {
                    // Create new PropertyMap with Image Mask on.
                    button.Text = "Image Mask, On";
                    PropertyMap propertyMap = new PropertyMap();
                    // Set the AlphaMaskURL properties as mask image's Url
                    propertyMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                               .Add(ImageVisualProperty.URL, new PropertyValue(mMaskBackgroundImageUrl))
                               .Add(ImageVisualProperty.AlphaMaskURL, new PropertyValue(mMaskImageUrl));
                    ((ImageView)mImagesView.GetChildAt(4)).Image = propertyMap;
                }

                mMasked = !mMasked;
            }
            else if (button.Name == mCaseString[5])
            {
                // Create new PropertyMap that contain information of fittingMode.
                // ShrinkToFit = 0,
                // ScaleToFill = 1
                // FitWidth = 2
                // FitHeight = 3
                PropertyMap propertyMap = new PropertyMap();
                propertyMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image))
                           .Add(ImageVisualProperty.URL, new PropertyValue(mFittingModeImageUrl))
                           .Add(ImageVisualProperty.DesiredWidth, new PropertyValue(mImageSize.Width))
                           .Add(ImageVisualProperty.DesiredHeight, new PropertyValue(mImageSize.Height))
                           .Add(ImageVisualProperty.FittingMode, new PropertyValue((++mFittingMode) % 4))
                           .Add(ImageVisualProperty.SamplingMode, new PropertyValue((int)SamplingModeType.Box));
                ((ImageView)mImagesView.GetChildAt(5)).Image = propertyMap;
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
