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
using Tizen;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace AnimationsSample
{
    /// <summary>
    /// This sample demonstrates how to use the Animation class to create and cancel animations.
    /// </summary>
    class AnimationsSample : NUIApplication
    {
        private static string mResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images";
        private static string mImageUrl = mResourceUrl + "/gallery-2.jpg";

        // Main view.
        private View root;
        private ImageView mImageView;
        // Animations that change properties.
        private Animation[] mAnimations;
        private uint mAnimationCount = 6;
        private Size mImageSize = new Size(150, 150);

        // PushButton be used to trigger the effect of Text.
        private Button[] mButton;
        // tableView be used to put PushButton.
        private TableView mTableView;

        // String array of each Animations
        private string[] mButtonString =
        {
             "Position Animation",
             "Size Animation",
             "Scale Animation",
             "Orientation Animation",
             "Opacity Animation",
             "PixelArea Animation",
             "Position+Size+Opacity"
        };
        private uint mButtonCount = 7;
        private int mCurruntButtonIndex;

        // UI properties
        private int mTouchableArea = 260;
        private bool mTouched = false;
        private bool mTouchedInButton = false;

        private float mLargePointSize = 10.0f;
        private float mMiddlePointSize = 5.0f;
        private float mSmallPointSize = 3.0f;
        private Vector2 mTouchedPosition;

        private Position mTableViewStartPosition = new Position(65, 90, 0);
        private Animation[] mTableViewAnimation;
        private Size mButtonSize = new Size(230, 35);

        /// <summary>
        /// The constructor with null
        /// </summary>
        public AnimationsSample() : base()
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
        /// Stop all animation.
        /// </summary>
        private void StopAnimations()
        {
            for (uint i = 0; i < mAnimationCount; ++i)
            {
                mAnimations[i].Stop();
            }
        }

        /// <summary>
        /// Animation Sample Application initialization.
        /// </summary>
        private void Initialize()
        {
            Window window = NUIApplication.GetDefaultWindow();

            root = new View()
            {
                Size = new Size(window.Size.Width, window.Size.Height),
                BackgroundColor = Color.White
            };
            
            TextLabel title = new TextLabel("Animation");
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;
            title.TextColor = Color.White;
            title.PositionUsesPivotPoint = true;
            title.ParentOrigin = ParentOrigin.TopCenter;
            title.PivotPoint = PivotPoint.TopCenter;
            title.Position = new Position(0, window.Size.Height / 10);
            title.FontFamily = "Samsung One 600";
            title.MultiLine = false;
            title.PointSize = mLargePointSize;
            root.Add(title);

            // Create the view to animate.
            mImageView = new ImageView();
            mImageView.Size = mImageSize;
            mImageView.PositionUsesPivotPoint = true;
            mImageView.PivotPoint = PivotPoint.Center;
            mImageView.ParentOrigin = ParentOrigin.Center;
            mImageView.ResourceUrl = mImageUrl;

            // Add view on Window.
            root.Add(mImageView);

            // Create Animations
            CreateAnimations();

            // Create Buttons for the UI
            CreateButtons();
            mCurruntButtonIndex = 0;

            TextLabel subTitle = new TextLabel("Swipe and Click the button");
            subTitle.HorizontalAlignment = HorizontalAlignment.Center;
            subTitle.VerticalAlignment = VerticalAlignment.Center;
            subTitle.TextColor = Color.White;
            subTitle.PositionUsesPivotPoint = true;
            subTitle.ParentOrigin = ParentOrigin.BottomCenter;
            subTitle.PivotPoint = PivotPoint.BottomCenter;
            subTitle.Position = new Position(0, -30);
            subTitle.FontFamily = "Samsung One 600";
            subTitle.MultiLine = false;
            subTitle.PointSize = mSmallPointSize;
            root.Add(subTitle);

            // Animations for the swipe action.
            mTableViewAnimation = new Animation[2];
            mTableViewAnimation[0] = new Animation();
            mTableViewAnimation[0].Duration = 100;
            mTableViewAnimation[0].AnimateBy(mTableView, "Position", new Vector3(-360, 0, 0));
            mTableViewAnimation[1] = new Animation();
            mTableViewAnimation[1].Duration = 100;
            mTableViewAnimation[1].AnimateBy(mTableView, "Position", new Vector3(360, 0, 0));

            window.TouchEvent += OnWindowTouched;
            window.KeyEvent += OnKey;

            window.Add(root);
        }

        /// <summary>
        /// Create Animations which animate properties of ImageView
        /// </summary>
        private void CreateAnimations()
        {
            mAnimations = new Animation[mAnimationCount];

            // Position animation
            mAnimations[0] = new Animation();
            // The duration of the animation is 1.5s;
            mAnimations[0].Duration = 1500;
            // Create the position animation using AnimateTo.
            mAnimations[0].AnimateTo(mImageView, "Position", new Position(-mImageSize.Width / 2, 0, 0), 0, 500);
            mAnimations[0].AnimateTo(mImageView, "Position", new Position(mImageSize.Width / 2, 0, 0), 501, 1000);
            mAnimations[0].AnimateTo(mImageView, "Position", new Position(0, 0, 0), 1001, 1500);
            // StopFinal: If the animation is stopped, the animated property values are saved as if the animation had run to completion
            mAnimations[0].EndAction = Animation.EndActions.StopFinal;

            // Size animation
            mAnimations[1] = new Animation();
            // The duration of the animation is 1.0s;
            mAnimations[1].Duration = 1000;
            // Create the size animation using AnimateTo.
            mAnimations[1].AnimateTo(mImageView, "size", new Vector3(mImageSize.Width, mImageSize.Width, 0) * 1.25f, 0, 500);
            mAnimations[1].AnimateTo(mImageView, "size", new Vector3(mImageSize.Width, mImageSize.Width, 0), 501, 1000);
            // StopFinal: If the animation is stopped, the animated property values are saved as if the animation had run to completion
            mAnimations[1].EndAction = Animation.EndActions.StopFinal;

            // Scale animation
            mAnimations[2] = new Animation();
            // The duration of the animation is 1.5s;
            mAnimations[2].Duration = 1500;
            // Create the scale animation using AnimateTo.
            mAnimations[2].AnimateTo(mImageView, "scale", new Vector3(1.2f, 1.2f, 1.0f), 0, 200, new AlphaFunction(AlphaFunction.BuiltinFunctions.Sin));
            mAnimations[2].AnimateTo(mImageView, "scaleX", 1.3f, 200, 500, new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear));
            mAnimations[2].AnimateTo(mImageView, "ScaleY", 1.3f, 500, 800, new AlphaFunction(AlphaFunction.BuiltinFunctions.Bounce));
            mAnimations[2].AnimateTo(mImageView, "scaleX", 1.0f, 800, 1200, new AlphaFunction(AlphaFunction.BuiltinFunctions.Bounce));
            mAnimations[2].AnimateTo(mImageView, "scaleX", 1.0f, 1200, 1500, new AlphaFunction(AlphaFunction.BuiltinFunctions.Linear));
            // StopFinal: If the animation is stopped, the animated property values are saved as if the animation had run to completion
            mAnimations[2].EndAction = Animation.EndActions.StopFinal;

            // Orientation animation
            mAnimations[3] = new Animation();
            // The duration of the animation is 2.2s;
            mAnimations[3].Duration = 2200;
            // Create the orientation animation using AnimateTo.
            mAnimations[3].AnimateTo(mImageView, "Orientation", new Rotation(new Radian(new Degree(60.0f)), PositionAxis.X), 0, 400);
            mAnimations[3].AnimateTo(mImageView, "Orientation", new Rotation(new Radian(new Degree(60.0f)), PositionAxis.Y), 400, 800);
            mAnimations[3].AnimateTo(mImageView, "Orientation", new Rotation(new Radian(new Degree(60.0f)), PositionAxis.Z), 800, 1000);
            mAnimations[3].AnimateTo(mImageView, "Orientation", new Rotation(new Radian(0.0f), PositionAxis.X), 1000, 1400);
            mAnimations[3].AnimateTo(mImageView, "Orientation", new Rotation(new Radian(0.0f), PositionAxis.Y), 1400, 1800);
            mAnimations[3].AnimateTo(mImageView, "Orientation", new Rotation(new Radian(0.0f), PositionAxis.Z), 1800, 2200);
            // StopFinal: If the animation is stopped, the animated property values are saved as if the animation had run to completion
            mAnimations[3].EndAction = Animation.EndActions.StopFinal;

            // Opacity animation
            mAnimations[4] = new Animation();
            // The duration of the animation is 1.5s;
            mAnimations[4].Duration = 1500;
            // Create the Opacity animation using AnimateTo.
            mAnimations[4].AnimateTo(mImageView, "Opacity", 0.5f, 0, 400);
            mAnimations[4].AnimateTo(mImageView, "Opacity", 0.0f, 400, 800);
            mAnimations[4].AnimateTo(mImageView, "Opacity", 0.5f, 800, 1250);
            mAnimations[4].AnimateTo(mImageView, "Opacity", 1.0f, 1250, 1500);
            // StopFinal: If the animation is stopped, the animated property values are saved as if the animation had run to completion
            mAnimations[4].EndAction = Animation.EndActions.StopFinal;

            // PixelArea animation
            mAnimations[5] = new Animation();
            // The duration of the animation is 2.0s;
            mAnimations[5].Duration = 2000;
            // Create the pixelArea animation using AnimateTo.
            RelativeVector4 vec1 = new RelativeVector4(0.0f, 0.0f, 0.5f, 0.5f);
            RelativeVector4 vec2 = new RelativeVector4(0.0f, 0.0f, 0.0f, 0.0f);
            RelativeVector4 vec3 = new RelativeVector4(0.0f, 0.0f, 1.0f, 1.0f);
            mAnimations[5].AnimateTo(mImageView, "pixelArea", vec1, 0, 500);
            mAnimations[5].AnimateTo(mImageView, "pixelArea", vec2, 500, 1000);
            mAnimations[5].AnimateTo(mImageView, "pixelArea", vec1, 1000, 1500);
            mAnimations[5].AnimateTo(mImageView, "pixelArea", vec3, 1500, 2000);
            // StopFinal: If the animation is stopped, the animated property values are saved as if the animation had run to completion
            mAnimations[5].EndAction = Animation.EndActions.StopFinal;
        }

        /// <summary>
        /// Create buttons which run each animation
        /// </summary>
        private void CreateButtons()
        {
            // Create tableView used to put PushButton.
            mTableView = new TableView(1, mButtonCount);
            // Set the position of tableView.
            mTableView.PositionUsesPivotPoint = true;
            mTableView.PivotPoint = PivotPoint.CenterLeft;
            mTableView.ParentOrigin = ParentOrigin.CenterLeft;
            mTableView.Position = mTableViewStartPosition;
            for (uint i = 0; i < mButtonCount; ++i)
            {
                mTableView.SetFixedWidth(i, 360);
            }

            mTableView.CellHorizontalAlignment = HorizontalAlignmentType.Center;
            mTableView.CellVerticalAlignment = VerticalAlignmentType.Center;

            root.Add(mTableView);

            mButton = new Button[mButtonCount]; 
            for (uint i = 0; i < mButtonCount; ++i)
            {
                // CreateButton with string array
                mButton[i] = CreateButton(mButtonString[i], mButtonString[i]);
                // Bind PushButton's click event to TouchEvent.
                mButton[i].TouchEvent += OnButtonTouched;
                mTableView.AddChild(mButton[i], new TableView.CellPosition(0, i));
            }
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
                    if (!mTouched ||
                       mTouchedPosition.Y < mTouchableArea)
                    {
                        break;
                    }

                    Vector2 displacement = e.Touch.GetScreenPosition(0) - mTouchedPosition;
                    if (Math.Abs(displacement.Y) > 20)
                    {
                        mTouched = false;
                        break;
                    }

                    if (displacement.X > 30)
                    {
                        AnimateAStepNegative();
                        mTouched = false;
                    }

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
                    if (!mTouched ||
                       mTouchedPosition.Y < mTouchableArea)
                    {
                        break;
                    }

                    Vector2 displacement = e.Touch.GetScreenPosition(0) - mTouchedPosition;
                    if (Math.Abs(displacement.Y) > 20)
                    {
                        mTouched = false;
                        break;
                    }

                    if (displacement.X > 30)
                    {
                        AnimateAStepNegative();
                        mTouched = false;
                    }

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
                        ButtonClick();
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
            if (mCurruntButtonIndex > 0)
            {
                mCurruntButtonIndex--;

                mTableViewAnimation[1].Play();
            }
        }

        /// <summary>
        /// Animate the tableView to the Positive direction
        /// </summary>
        private void AnimateAStepPositive()
        {
            if (mCurruntButtonIndex < mButtonCount - 1)
            {
                mCurruntButtonIndex++;

                mTableViewAnimation[0].Play();
            }
        }

        /// <summary>
        /// Called by buttons
        /// </summary>
        /// <returns>The consume flag</returns>
        private bool ButtonClick()
        {
            if (mCurruntButtonIndex == 0)
            {
                // Stop all the animation and Play position Animation.
                StopAnimations();
                // Play position animation
                mAnimations[0].Play();
            }
            else if (mCurruntButtonIndex == 1)
            {
                // Stop all the animation and Play position Animation.
                StopAnimations();
                // Play size animation
                mAnimations[1].Play();
            }
            else if (mCurruntButtonIndex == 2)
            {
                // Stop all the animation and Play position Animation.
                StopAnimations();
                // Play scale animation
                mAnimations[2].Play();
            }
            else if (mCurruntButtonIndex == 3)
            {
                // Stop all the animation and Play position Animation.
                StopAnimations();
                // Play orientation animation
                mAnimations[3].Play();
            }
            else if (mCurruntButtonIndex == 4)
            {
                // Stop all the animation and Play position Animation.
                StopAnimations();
                // Play opacity animation
                mAnimations[4].Play();
            }
            else if (mCurruntButtonIndex == 5)
            {
                // Stop all the animation and Play position Animation.
                StopAnimations();
                // Play pixel area animation
                mAnimations[5].Play();
            }
            else if (mCurruntButtonIndex == 6)
            {
                // Stop all the animation and Play position, scale opacity Animation at the same time.
                StopAnimations();
                // Play position, scale, and opacity animations
                mAnimations[0].Play();
                mAnimations[2].Play();
                mAnimations[4].Play();
            }

            return false;
        }

        /// <summary>
        /// Create a Text visual.
        /// </summary>
        /// <param name="text">The text of the Text visual</param>
        /// <param name="color">The color of the text</param>
        /// <returns>return a map which contain the properties of the text</returns>
        private PropertyMap CreateTextVisual(string text, Color color)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            map.Add(TextVisualProperty.Text, new PropertyValue(text));
            map.Add(TextVisualProperty.TextColor, new PropertyValue(color));
            map.Add(TextVisualProperty.PointSize, new PropertyValue(mMiddlePointSize));
            map.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.FontFamily, new PropertyValue("Samsung One 400"));
            return map;
        }

        /// <summary>
        /// Create a Color visual.
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
        /// Create a Button.
        /// </summary>
        /// <param name="name">The name of button</param>
        /// <param name="text">The string value that will be used for the label text</param>
        /// <returns>return a PushButton</returns>
        //private PushButton CreateButton(string name, string text)
        private Button CreateButton(string name, string text)
        {
            Button button = new Button();
            button.Name = name;
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
            Log.Info("Tag", "========== Hello, AnimationsSample ==========");
            new AnimationsSample().Run(args);
        }
    }
}