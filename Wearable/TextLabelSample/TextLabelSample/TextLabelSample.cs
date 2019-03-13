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
using Tizen;
using Tizen.NUI;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace TextLabelSample
{
    /// <summary>
    /// This sample application demonstrates TextLabel and its usability
    /// </summary>
    class TextLabelSample : NUIApplication
    {
        // textLabel be used to show the effect of TextLabel.
        private TextLabel mTextLabel;
        // PushButton be used to trigger the effect of Text.
        private PushButton[] mPushButton;
        // tableView be used to put PushButton.
        private TableView mTableView;
        // Some kinds of LANGUAGES.
        private string[] LANGUAGES =
        {
            "العَرَبِيةُ(Arabic)", "অসমীয়া লিপি(Assamese)", "Español(Spanish)", "한국어(Korean)", "漢語(Chinese)",
            "A control which renders a short text string.(English)"
        };
        // The index of LANGUAGES.
        private int mItemLanguage = 0;
        // The count of languages.
        private int mNumLanguage = 6;

        // A string list of sample cases
        private string[] mPushButtonString =
        {
             "HorizontalAlignment",
             "VerticalAlignment",
             "Color",
             "Size",
             "Scroll",
             "Language",
             "Multiline",
             "Shadow",
             "Underline",
             "Ellipsis",
             "Bold",
             "Condensed"
        };
        // A number of sample cases
        private uint mPushButtonCount = 12;
        private int mCurruntButtonIndex;

        // Button state
        private uint[] mButtonState;

        // UI properties
        private int mTouchableArea = 260;
        private bool mTouched = false;
        private bool mTouchedInButton = false;

        // Window Size
        private Size2D mWindowSize;
        // Text point sizes
        private float mLargePointSize = 10.0f;
        private float mMiddlePointSize = 5.0f;
        private float mSmallPointSize = 3.0f;
        private Vector2 mTouchedPosition;

        private Position mTableViewStartPosition = new Position(65, 90, 0);
        private Animation[] mTableViewAnimation;
        private Size2D mButtonSize = new Size2D(230, 35);

        /// <summary>
        /// The constructor with null
        /// </summary>
        public TextLabelSample() : base()
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
        /// Text Sample Application initialization.
        /// </summary>
        public void Initialize()
        {
            // Set the background Color of Window.
            Window.Instance.BackgroundColor = Color.Black;
            mWindowSize = Window.Instance.Size;

            // Create Title TextLabel
            TextLabel Title = new TextLabel("Text Label");
            Title.HorizontalAlignment = HorizontalAlignment.Center;
            Title.VerticalAlignment = VerticalAlignment.Center;
            // Set Text color to White
            Title.TextColor = Color.White;
            Title.PositionUsesPivotPoint = true;
            Title.ParentOrigin = ParentOrigin.TopCenter;
            Title.PivotPoint = PivotPoint.TopCenter;
            Title.Position2D = new Position2D(0, mWindowSize.Height / 10);
            // Use Samsung One 600 font
            Title.FontFamily = "Samsung One 600";
            // Set MultiLine false
            Title.MultiLine = false;
            Title.PointSize = mLargePointSize;
            Window.Instance.GetDefaultLayer().Add(Title);

            // Create textLabel.
            CreateTextLabel();
            // Create buttons to show some functions or properties.
            CreateButtons();
            mCurruntButtonIndex = 0;

            // Create subTitle TextLabel
            TextLabel subTitle = new TextLabel("Swipe and Click the button");
            subTitle.HorizontalAlignment = HorizontalAlignment.Center;
            subTitle.VerticalAlignment = VerticalAlignment.Center;
            // Set Text color to White
            subTitle.TextColor = Color.White;
            subTitle.PositionUsesPivotPoint = true;
            subTitle.ParentOrigin = ParentOrigin.BottomCenter;
            subTitle.PivotPoint = PivotPoint.BottomCenter;
            subTitle.Position2D = new Position2D(0, -30);
            // Use Samsung One 600 font
            subTitle.FontFamily = "Samsung One 600";
            // Set MultiLine false
            subTitle.MultiLine = false;
            subTitle.PointSize = mSmallPointSize;
            Window.Instance.GetDefaultLayer().Add(subTitle);

            // Animation setting for the button animation
            mTableViewAnimation = new Animation[2];
            mTableViewAnimation[0] = new Animation();
            mTableViewAnimation[0].Duration = 100;
            mTableViewAnimation[0].AnimateBy(mTableView, "Position", new Vector3(-360, 0, 0));
            mTableViewAnimation[1] = new Animation();
            mTableViewAnimation[1].Duration = 100;
            mTableViewAnimation[1].AnimateBy(mTableView, "Position", new Vector3(360, 0, 0));

            // Add Signal Callback functions
            Window.Instance.TouchEvent += OnWindowTouched;
            Window.Instance.KeyEvent += OnKey;
        }

        /// <summary>
        /// Create textLabel.
        /// TextLabel : A control which renders a short text string.
        /// </summary>
        private void CreateTextLabel()
        {
            // Create main textLabel.
            mTextLabel = new TextLabel("A control which renders a simple text string. The text could be single line or multiline. You can decide that!");
            mTextLabel.Size2D = new Size2D((int)(mWindowSize.Width * 0.8f), (int)(mWindowSize.Height * 0.4f));
            // Set the position of textLabel.
            mTextLabel.PositionUsesPivotPoint = true;
            mTextLabel.PivotPoint = PivotPoint.Center;
            mTextLabel.ParentOrigin = ParentOrigin.Center;
            mTextLabel.Position = new Position(0, -5, 0);
            mTextLabel.PointSize = mMiddlePointSize;
            // Text color
            mTextLabel.BackgroundColor = Color.White;
            mTextLabel.FontFamily = "SamsungOneUI_200";

            Window.Instance.GetDefaultLayer().Add(mTextLabel);
        }

        /// <summary>
        /// Create buttons which control properties of textLabel
        /// </summary>
        private void CreateButtons()
        {
            // Create tableView used to put PushButton.
            mTableView = new TableView(1, mPushButtonCount);
            // Set the position of tableView.
            mTableView.PositionUsesPivotPoint = true;
            mTableView.PivotPoint = PivotPoint.CenterLeft;
            mTableView.ParentOrigin = ParentOrigin.CenterLeft;
            mTableView.Position = mTableViewStartPosition;
            // Set the each cell
            for (uint i = 0; i < mPushButtonCount; ++i)
            {
                mTableView.SetFixedWidth(i, 360);
            }

            mTableView.CellHorizontalAlignment = HorizontalAlignmentType.Center;
            mTableView.CellVerticalAlignment = VerticalAlignmentType.Center;

            Window.Instance.GetDefaultLayer().Add(mTableView);

            // Create button for the each case.
            mPushButton = new PushButton[mPushButtonCount];
            for (uint i = 0; i < mPushButtonCount; ++i)
            {
                // Creates button
                mPushButton[i] = CreateButton(mPushButtonString[i]);
                // Bind PushButton's click event to ButtonClick.
                mPushButton[i].TouchEvent += OnButtonTouched;
                mTableView.AddChild(mPushButton[i], new TableView.CellPosition(0, i));
            }

            // Set the default state of each button property
            mButtonState = new uint[mPushButtonCount];
            for (uint i = 0; i < mPushButtonCount; ++i)
            {
                mButtonState[i] = 0;
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

            // If State is Down (Touched at the inside of Button)
            // - Store touched position.
            // - Set the mTouched to true
            // - Set the mTouchedInButton to true
            switch (e.Touch.GetState(0))
            {
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
            // If the state is not the last one, move ImageViews and PushButton a step.
            if (mCurruntButtonIndex < mPushButtonCount - 1)
            {
                mCurruntButtonIndex++;

                mTableViewAnimation[0].Play();
            }
        }

        /// <summary>
        /// Called by buttons
        /// </summary>
        /// <param name="source">The clicked button</param>
        /// <returns>The consume flag</returns>
        private bool ButtonClick(object source)
        {
            // Get the source who trigger this event.
            PushButton button = source as PushButton;
            // Change textLabel's HorizontalAlignment.
            if (button.LabelText == "HorizontalAlignment")
            {
                // Begin : Texts place at the begin of horizontal direction.
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    // Set the HorizontalAlignment to Center
                    mTextLabel.HorizontalAlignment = HorizontalAlignment.Center;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                // Center : Texts place at the center of horizontal direction.
                else if (mButtonState[mCurruntButtonIndex] == 1)
                {
                    // Set the HorizontalAlignment to End
                    mTextLabel.HorizontalAlignment = HorizontalAlignment.End;
                    mButtonState[mCurruntButtonIndex] = 2;
                }
                // End : Texts place at the end of horizontal direction.
                else
                {
                    // Set the HorizontalAlignment to Begin
                    mTextLabel.HorizontalAlignment = HorizontalAlignment.Begin;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Change textLabel's VerticalAlignment.
            else if (button.LabelText == "VerticalAlignment")
            {
                // Top : Texts place at the top of vertical direction.
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    // Set the VerticalAlignment to Center
                    mTextLabel.VerticalAlignment = VerticalAlignment.Center;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                // Center : Texts place at the center of vertical direction.
                else if (mButtonState[mCurruntButtonIndex] == 1)
                {
                    // Set the VerticalAlignment to Bottom
                    mTextLabel.VerticalAlignment = VerticalAlignment.Bottom;
                    mButtonState[mCurruntButtonIndex] = 2;
                }
                // Bottom : Texts place at the bottom of vertical direction.
                else
                {
                    // Set the VerticalAlignment to Top
                    mTextLabel.VerticalAlignment = VerticalAlignment.Top;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Change textLabel's text color.
            else if (button.LabelText == "Color")
            {
                // Judge the textColor is Black or not.
                // It true, change text color to blue.
                // It not, change text color to black.
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    // Set the TextColor to Blue
                    mTextLabel.TextColor = Color.Blue;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    // Set the TextColor to Black
                    mTextLabel.TextColor = Color.Black;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Change textLabel's text size.
            else if (button.LabelText == "Size")
            {
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    // Set the PointSize to Large
                    mTextLabel.PointSize = mLargePointSize;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    // Set the PointSize to normal
                    mTextLabel.PointSize = mMiddlePointSize;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Make textLabel's text auto scroll.
            // Starts auto scrolling.
            // Gap before scrolling wraps is 500.
            // The speed of scrolling in pixels per second is 500.
            // Number of complete loops when scrolling enabled is 1.
            else if (button.LabelText == "Scroll")
            {
                mTextLabel.VerticalAlignment = VerticalAlignment.Top;
                mTextLabel.EnableAutoScroll = true;
                mTextLabel.AutoScrollGap = 500;
                mTextLabel.AutoScrollSpeed = 500;
                mTextLabel.AutoScrollLoopCount = 1;
                mTextLabel.AutoScrollStopMode = AutoScrollStopMode.FinishLoop;
            }
            // Change different language on textLabel.
            else if (button.LabelText == "Language")
            {
                mTextLabel.Text = LANGUAGES[mItemLanguage];
                mItemLanguage++;
                // If the index of LANGUAGES in end, move index to 0.
                if (mItemLanguage == mNumLanguage)
                {
                    mItemLanguage = 0;
                }
            }
            // Set the text on textLabel show in single-line or multi-line layout option.
            // If multiline is true , the text length more than the textLabel's length and
            // textLabel's high can show multiline, text will shown in multiline.
            // If not, The more text over textLabel length, will show "...".
            else if (button.LabelText == "Multiline")
            {
                mTextLabel.Text = "TextLabel : A control which renders a text string, it could be single line or multiline. You can decide that!";
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    // Set the Multiline property to true
                    mTextLabel.MultiLine = true;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    // Set the Multiline property to false
                    mTextLabel.MultiLine = false;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Set the text on textLabel have shadow or not.
            else if (button.LabelText == "Shadow")
            {
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    mTextLabel.ShadowOffset = new Vector2(3.0f, 3.0f);
                    mTextLabel.ShadowColor = Color.Black;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    // The drop shadow offset 0 indicates no shadow.
                    mTextLabel.ShadowOffset = new Vector2(0, 0);
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Set the text on textLabel have Underline or not.
            else if (button.LabelText == "Underline")
            {
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    // Show the underline.
                    // Underline color is black
                    // Underline height is 3
                    PropertyMap underlineMap = new PropertyMap();
                    underlineMap.Insert("enable", new PropertyValue("true"));
                    underlineMap.Insert("color", new PropertyValue("black"));
                    underlineMap.Insert("height", new PropertyValue("3"));

                    // Set the underline property
                    mTextLabel.Underline = underlineMap;
                    mTextLabel.Text = mTextLabel.Text;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    // Hide the underline.
                    PropertyMap underlineMap = new PropertyMap();
                    underlineMap.Insert("enable", new PropertyValue("false"));

                    // Check the underline property
                    mTextLabel.Underline = underlineMap;
                    mTextLabel.Text = mTextLabel.Text;
                    mButtonState[mCurruntButtonIndex] = 0;

                }
            }
            // Set textLabel is enable or disable the ellipsis.
            else if (button.LabelText == "Ellipsis")
            {
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    // Set the Ellipsis property to false
                    mTextLabel.Ellipsis = false;
                    mTextLabel.Text = mTextLabel.Text;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    // Set the Ellipsis property to true
                    mTextLabel.Ellipsis = true;
                    mTextLabel.Text = mTextLabel.Text;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Set textLabel text is bold or not.
            else if (button.LabelText == "Bold")
            {
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    // The weight of text is bold.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("weight", new PropertyValue("bold"));
                    mTextLabel.FontStyle = fontStyle;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    // The weight of text is normal.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("weight", new PropertyValue("normal"));
                    mTextLabel.FontStyle = fontStyle;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Set textLabel text is condensed or not.
            else if (button.LabelText == "Condensed")
            {
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    // The width of text is condensed.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("width", new PropertyValue("condensed"));
                    mTextLabel.FontStyle = fontStyle;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    // The width of text is normal.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("width", new PropertyValue("normal"));
                    mTextLabel.FontStyle = fontStyle;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }

            return true;
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
            // Create Property map and add each property of TextVisual
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
            // Create Property map and add each property of ColorVisual
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Color));
            map.Add(ColorVisualProperty.MixColor, new PropertyValue(color));
            return map;
        }

        /// <summary>
        /// Create an Button.
        /// </summary>
        /// <param name="text">The string to use button's name and Label text</param>
        /// <returns>return a PushButton</returns>
        private PushButton CreateButton(string text)
        {
            // New PushButton
            PushButton button = new PushButton();
            button.Name = text;
            button.Size2D = mButtonSize;
            button.ClearBackground();

            button.Position = new Position(50, 0, 0);

            // Create text map for the selected state.
            PropertyMap unSelectedTextMap = CreateTextVisual(text, Color.White);
            // Create ColorVisual property for the unselected states.
            PropertyMap unSelectedMap = CreateColorVisual(new Vector4(0.1f, 0.1f, 0.1f, 0.9f));

            // Create ColorVisual property for the selected states
            PropertyMap selectedMap = CreateColorVisual(new Vector4(0.5f, 0.5f, 0.5f, 1.0f));

            // Set each label and text properties.
            button.Label = unSelectedTextMap;
            button.SelectedBackgroundVisual = selectedMap;
            button.UnselectedBackgroundVisual = unSelectedMap;

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
        /// The enter point of TextLabelSample.
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[] args)
        {
            Log.Info("Tag", "========== Hello, TextLabelSample ==========");
            new TextLabelSample().Run(args);
        }
    }
}
