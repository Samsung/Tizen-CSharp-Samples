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
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace TextFieldSample
{
    /// <summary>
    /// This sample application demonstrates TextField and its usability
    /// </summary>
    class TextFieldSample : NUIApplication
    {
        // Main view
        private Window window;
        private View root;
        // textField be used to show the effect of TextField.
        private TextField mTextField;
        // PushButton be used to trigger the effect of Text.
        private Button[] mButton;
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
        private string[] mButtonString =
        {
             "HorizontalAlignment",
             "VerticalAlignment",
             "Color",
             "Size",
             "Language",
             "Shadow",
             "Underline",
             "Bold",
             "Condensed"
        };
        // A number of sample cases
        private uint mButtonCount = 9;
        private int mCurruntButtonIndex;

        // Button state
        private uint[] mButtonState;

        // UI properties
        private int mTouchableArea = 260;
        private bool mTouched = false;
        private bool mTouchedInButton = false;

        // Text point sizes
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
        public TextFieldSample() : base()
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
            window = NUIApplication.GetDefaultWindow();

            root = new View()
            {
                Size = new Size(window.Size.Width, window.Size.Height),
                BackgroundColor = Color.White
            };

            // Create Title TextLabel
            TextLabel Title = new TextLabel("Text Field");
            Title.HorizontalAlignment = HorizontalAlignment.Center;
            Title.VerticalAlignment = VerticalAlignment.Center;
            // Set Text color to White
            Title.TextColor = Color.White;
            Title.PositionUsesPivotPoint = true;
            Title.ParentOrigin = ParentOrigin.TopCenter;
            Title.PivotPoint = PivotPoint.TopCenter;
            Title.Position = new Position(0, window.Size.Height / 10);
            // Use Samsung One 600 font
            Title.FontFamily = "Samsung One 600";
            // Set MultiLine to false;
            Title.MultiLine = false;
            Title.PointSize = mLargePointSize;
            root.Add(Title);

            // Create textField.
            CreateTextField();
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
            subTitle.Position = new Position(0, -30);
            // Use Samsung One 600 font
            subTitle.FontFamily = "Samsung One 600";
            // Set MultiLine to false;
            subTitle.MultiLine = false;
            subTitle.PointSize = mSmallPointSize;
            root.Add(subTitle);

            // Animation setting for the button animation
            mTableViewAnimation = new Animation[2];
            mTableViewAnimation[0] = new Animation();
            mTableViewAnimation[0].Duration = 100;
            mTableViewAnimation[0].AnimateBy(mTableView, "Position", new Vector3(-360, 0, 0));
            mTableViewAnimation[1] = new Animation();
            mTableViewAnimation[1].Duration = 100;
            mTableViewAnimation[1].AnimateBy(mTableView, "Position", new Vector3(360, 0, 0));

            window.Add(root);

            // Add Signal Callback functions
            window.TouchEvent += OnWindowTouched;
            window.KeyEvent += OnKey;
        }

        /// <summary>
        /// Create textField.
        /// TextField : A control which renders a short text string.
        /// </summary>
        private void CreateTextField()
        {
            // Create main textField.
            mTextField = new TextField();
            mTextField.Size = new Size((int)(window.Size.Width * 0.8f), (int)(window.Size.Height * 0.4f));
            // Set the position of textField.
            mTextField.PositionUsesPivotPoint = true;
            mTextField.PivotPoint = PivotPoint.Center;
            mTextField.ParentOrigin = ParentOrigin.Center;
            mTextField.Position = new Position(0, -5, 0);
            mTextField.PointSize = mMiddlePointSize;
            // Default Text
            mTextField.Text = "A control which provides a single-line editable text field.";
            // Text color
            mTextField.BackgroundColor = Color.White;
            // The kind of text is "SamsungOneUI_400"
            mTextField.FontFamily = "SamsungOneUI_400";
            // The max text length can show in TextField.
            mTextField.MaxLength = 20;
            // The Primary Cursor Color is blue.
            // The Secondary Cursor Color is Green;
            // The Selection Highlight Color is Cyan;
            mTextField.PrimaryCursorColor = Color.Blue;
            mTextField.SecondaryCursorColor = Color.Green;
            mTextField.SelectionHighlightColor = Color.Cyan;
            mTextField.EnableCursorBlink = true;

            root.Add(mTextField);
        }

        /// <summary>
        /// Create buttons which control properties of textField
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
            // Set the each cell
            for (uint i = 0; i < mButtonCount; ++i)
            {
                mTableView.SetFixedWidth(i, 360);
            }

            root.Add(mTableView);

            // Create button for the each case.
            mButton = new Button[mButtonCount];
            for (uint i = 0; i < mButtonCount; ++i)
            {
                // Creates button
                mButton[i] = CreateButton(mButtonString[i]);
                // Bind PushButton's click event to ButtonClick.
                mButton[i].TouchEvent += OnButtonTouched;
                mTableView.AddChild(mButton[i], new TableView.CellPosition(0, i));
            }

            // Set the default state of each button property
            mButtonState = new uint[mButtonCount];
            for (uint i = 0; i < mButtonCount; ++i)
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
            if (mCurruntButtonIndex < mButtonCount - 1)
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
            Button button = source as Button;
            // Change textField's HorizontalAlignment.
            if (button.Text == "HorizontalAlignment")
            {
                // Begin : Texts place at the begin of horizontal direction.
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    mTextField.HorizontalAlignment = HorizontalAlignment.Center;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                // Center : Texts place at the center of horizontal direction.
                else if (mButtonState[mCurruntButtonIndex] == 1)
                {
                    mTextField.HorizontalAlignment = HorizontalAlignment.End;
                    mButtonState[mCurruntButtonIndex] = 2;
                }
                // End : Texts place at the end of horizontal direction.
                else
                {
                    mTextField.HorizontalAlignment = HorizontalAlignment.Begin;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Change textField's VerticalAlignment.
            else if (button.Text == "VerticalAlignment")
            {
                // Top : Texts place at the top of vertical direction.
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    mTextField.VerticalAlignment = VerticalAlignment.Center;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                // Center : Texts place at the center of vertical direction.
                else if (mButtonState[mCurruntButtonIndex] == 1)
                {
                    mTextField.VerticalAlignment = VerticalAlignment.Bottom;
                    mButtonState[mCurruntButtonIndex] = 2;
                }
                // Bottom : Texts place at the bottom of vertical direction.
                else
                {
                    mTextField.VerticalAlignment = VerticalAlignment.Top;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Change textField's text color.
            else if (button.Text == "Color")
            {
                // Judge the textColor is Black or not.
                // It true, change text color to blue.
                // It not, change text color to black.
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    mTextField.TextColor = Color.Blue;
                    mTextField.Text = mTextField.Text;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    mTextField.TextColor = Color.Black;
                    mTextField.Text = mTextField.Text;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Change textField's text size.
            else if (button.Text == "Size")
            {
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    mTextField.PointSize = mLargePointSize;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    mTextField.PointSize = mMiddlePointSize;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Change different language on textField.
            else if (button.Text == "Language")
            {
                mTextField.Text = LANGUAGES[mItemLanguage];
                mItemLanguage++;
                // If the index of LANGUAGES in end, move index to 0.
                if (mItemLanguage == mNumLanguage)
                {
                    mItemLanguage = 0;
                }
            }
            // Set the text on textField have shadow or not.
            else if (button.Text == "Shadow")
            {
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    // The drop shadow offset
                    mTextField.Position = new Position(3.0f, 3.0f);
                    mTextField.Color = Color.Black;
                    mTextField.Text = mTextField.Text;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    // The drop shadow offset 0 indicates no shadow.
                    mTextField.Position = new Position(0, 0);
                    mTextField.Text = mTextField.Text;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Set the text on textField have Underline or not.
            else if (button.Text == "Underline")
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
                    mTextField.Underline = underlineMap;
                    mTextField.Text = mTextField.Text;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    // Hide the underline.
                    PropertyMap underlineMap = new PropertyMap();
                    underlineMap.Insert("enable", new PropertyValue("false"));

                    // Check the underline property
                    mTextField.Underline = underlineMap;
                    mTextField.Text = mTextField.Text;
                    mButtonState[mCurruntButtonIndex] = 0;

                }
            }
            // Set textField text is bold or not.
            else if (button.Text == "Bold")
            {
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    // The weight of text is bold.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("weight", new PropertyValue("bold"));
                    mTextField.FontStyle = fontStyle;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    // The weight of text is normal.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("weight", new PropertyValue("normal"));
                    mTextField.FontStyle = fontStyle;
                    mButtonState[mCurruntButtonIndex] = 0;
                }
            }
            // Set textField text is condensed or not.
            else if (button.Text == "Condensed")
            {
                if (mButtonState[mCurruntButtonIndex] == 0)
                {
                    // The width of text is condensed.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("width", new PropertyValue("condensed"));
                    mTextField.FontStyle = fontStyle;
                    mButtonState[mCurruntButtonIndex] = 1;
                }
                else
                {
                    // The width of text is normal.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("width", new PropertyValue("normal"));
                    mTextField.FontStyle = fontStyle;
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
            // Create a Property map for TextVisual
            PropertyMap map = new PropertyMap();
            // Text Visual
            // Set each property of TextVisual
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
            // Create a Property map for ColorVisual
            PropertyMap map = new PropertyMap();
            // Set each property of ColorVisual
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Color));
            map.Add(ColorVisualProperty.MixColor, new PropertyValue(color));
            return map;
        }

        /// <summary>
        /// Create an Button.
        /// </summary>
        /// <param name="text">The string to use button's name and Label text</param>
        /// <returns>return a PushButton</returns>
        private Button CreateButton(string text)
        {
            // New PushButton
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
        /// The enter point of TextFieldSample.
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[] args)
        {
            Log.Info("Tag", "========== Hello, TextFieldSample ==========");
            new TextFieldSample().Run(args);
        }
    }
}
