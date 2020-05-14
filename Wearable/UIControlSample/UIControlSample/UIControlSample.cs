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

namespace UIControlSample
{
    /// <summary>
    /// A sample of UIControl
    /// </summary>
    class UIControlSample : NUIApplication
    {
        // Resource path Url
        private static string mResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images";

        // TextLabel for checkBox
        TextLabel mtextLabelCheckBox;
        // TextLabel for RadioButton
        TextLabel mtextLabelRadio;
        // Slider Control
        Slider mSlider;
        // ProgressBar Control
        Progress mProgress;

        // A string list of sample cases
        private string[] mCaseString =
        {
             "CheckBox Button",
             "Radio Button",
             "Slider",
             "Progress Bar"
        };
        // A number of sample cases
        private uint mCaseCount = 4;
        private int mCurruntCaseIndex;

        // A View contains every Controls.
        private View mControlsView;

        private Size mControlSize = new Size(150, 150);

        // UI properties
        private Position mScreenStartPosition = new Position(0, 0, 0);
        private Animation[] mScreenAnimation;

        private Size mButtonSize = new Size(230, 35);

        private int mTouchableArea = 260;
        private bool mTouched = false;

        private Size mWindowSize;
        private float mLargePointSize = 10.0f;
        private float mMiddlePointSize = 5.0f;
        private float mSmallPointSize = 3.0f;
        private Vector2 mTouchedPosition;

        /// <summary>
        /// The constructor with null
        /// </summary>
        public UIControlSample() : base()
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
        /// UI Control Sample Application initialization.
        /// </summary>
        void Initialize()
        {
            // Set the background Color of Window.
            Window.Instance.BackgroundColor = Color.Black;
            mWindowSize = new Size(Window.Instance.Size.Width, Window.Instance.Size.Height);

            // Create Title TextLabel
            TextLabel title = new TextLabel("UI Control");
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;
            // Set Text color to White
            title.TextColor = Color.White;
            title.PositionUsesPivotPoint = true;
            title.ParentOrigin = ParentOrigin.TopCenter;
            title.PivotPoint = PivotPoint.TopCenter;
            title.Position = new Position(0, mWindowSize.Height / 10);
            // Use Samsung One 600 font
            title.FontFamily = "Samsung One 600";
            // Set MultiLine to false
            title.MultiLine = false;
            title.PointSize = mLargePointSize;
            Window.Instance.GetDefaultLayer().Add(title);

            // Create controls.
            CreateControls();

            // Create subTitle TextLabel
            TextLabel subTitle1 = new TextLabel("Swipe Here");
            subTitle1.HorizontalAlignment = HorizontalAlignment.Center;
            subTitle1.VerticalAlignment = VerticalAlignment.Center;
            // Set Text color to White
            subTitle1.TextColor = Color.White;
            subTitle1.PositionUsesPivotPoint = true;
            subTitle1.ParentOrigin = ParentOrigin.BottomCenter;
            subTitle1.PivotPoint = PivotPoint.BottomCenter;
            subTitle1.Position = new Position(0, -40);
            // Use Samsung One 600 font
            subTitle1.FontFamily = "Samsung One 600";
            // Set MultiLine to false
            subTitle1.MultiLine = false;
            subTitle1.PointSize = mMiddlePointSize;
            Window.Instance.GetDefaultLayer().Add(subTitle1);

            // Create subTitle2 TextLabel
            TextLabel subTitle2 = new TextLabel("for the other sample");
            subTitle2.HorizontalAlignment = HorizontalAlignment.Center;
            subTitle2.VerticalAlignment = VerticalAlignment.Center;
            // Set Text color to White
            subTitle2.TextColor = Color.White;
            subTitle2.PositionUsesPivotPoint = true;
            subTitle2.ParentOrigin = ParentOrigin.BottomCenter;
            subTitle2.PivotPoint = PivotPoint.BottomCenter;
            subTitle2.Position = new Position(0, -25);
            // Use Samsung One 600 font
            subTitle2.FontFamily = "Samsung One 600";
            // Set MultiLine to false
            subTitle2.MultiLine = false;
            subTitle2.PointSize = mSmallPointSize;
            Window.Instance.GetDefaultLayer().Add(subTitle2);

            // Animation setting for the control animation
            mScreenAnimation = new Animation[2];
            mScreenAnimation[0] = new Animation();
            mScreenAnimation[0].Duration = 100;
            mScreenAnimation[0].AnimateBy(mControlsView, "Position", new Vector3(-360, 0, 0));
            mScreenAnimation[1] = new Animation();
            mScreenAnimation[1].Duration = 100;
            mScreenAnimation[1].AnimateBy(mControlsView, "Position", new Vector3(360, 0, 0));

            // Add Signal Callback functions
            Window.Instance.TouchEvent += OnWindowTouched;
            Window.Instance.KeyEvent += OnKey;
        }

        /// <summary>
        /// Create Controls
        /// </summary>
        private void CreateControls()
        {
            // Create New View to contain controls
            mControlsView = new View();
            // Set the Position of control view
            mControlsView.PositionUsesPivotPoint = true;
            mControlsView.PivotPoint = PivotPoint.CenterLeft;
            mControlsView.ParentOrigin = ParentOrigin.CenterLeft;
            // Set Control view size
            mControlsView.Size = new Size((int)mCaseCount * 360, mControlSize.Height);
            Window.Instance.GetDefaultLayer().Add(mControlsView);

            // Create controls
            Position stp = new Position(0, 0, 0);
            for (uint i = 0; i < mCaseCount; ++i)
            {
                // Create a Control with string and position.
                View control = CreateControl(mCaseString[i], stp + new Position(i * 360, 0, 0));
                mControlsView.Add(control);
            }
        }

        /// <summary>
        /// Create a Control
        /// </summary>
        /// <param name="text">case string of the created control</param>
        /// <param name="position">A position the created control will be located</param>
        /// <returns>View for the created control</returns>
        private View CreateControl(string text, Position position)
        {
            // A View that contain a control element
            View view = new View();
            // CheckBox sample
            if (text == mCaseString[0])
            {
                // TextLabel for the checkbox
                mtextLabelCheckBox = new TextLabel("CheckBox");
                mtextLabelCheckBox.Size = new Size(250, 60);
                mtextLabelCheckBox.HorizontalAlignment = HorizontalAlignment.Center;
                mtextLabelCheckBox.VerticalAlignment = VerticalAlignment.Center;
                // Set the position of checkbox.
                mtextLabelCheckBox.PositionUsesPivotPoint = true;
                mtextLabelCheckBox.PivotPoint = PivotPoint.Center;
                mtextLabelCheckBox.ParentOrigin = ParentOrigin.Center;
                mtextLabelCheckBox.Position = new Position(0, -55, 0);
                mtextLabelCheckBox.PointSize = mLargePointSize;
                mtextLabelCheckBox.BackgroundColor = Color.White;
                mtextLabelCheckBox.FontFamily = "SamsungOneUI_200";

                view.Add(mtextLabelCheckBox);

                // Create three CheckBoxs
                Position checkBoxStartPosition = new Position(100, 0, 0);
                CheckBox[] checkBox = new CheckBox[3];
                for (int i = 0; i < 3; ++i)
                {
                    // New CheckBox
                    checkBox[i] = new CheckBox();
                    // Set the CheckBox position
                    checkBox[i].Position = checkBoxStartPosition + new Position(0, 40, 0) * i;
                    checkBox[i].PositionUsesPivotPoint = true;
                    checkBox[i].PivotPoint = PivotPoint.CenterLeft;
                    checkBox[i].ParentOrigin = ParentOrigin.CenterLeft;

                    if (checkBox[i].IsSelected)
                    {
                        // Make Image Visual for the selected checkbox visual
                        checkBox[i].BackgroundImage = mResourceUrl + "/CheckBox/Selected.png";
                    }
                    else
                    {
                        // Make Image Visual for the unselected checkbox visual
                        checkBox[i].BackgroundImage = mResourceUrl + "/CheckBox/Unselected.png";
                    }
                    
                    checkBox[i].Scale = new Vector3(0.8f, 0.8f, 0.8f);
                    // Add a callback function for the StateChanged signal
                    checkBox[i].StateChangedEvent += OnCheckBoxChanged;
                    view.Add(checkBox[i]);
                }
                // Make Text Visual
                // Set the Label of Checkbox
                checkBox[0].Text = "Shadow";
                checkBox[0].PointSize = mMiddlePointSize + 2;
                checkBox[0].TextColor = Color.White;

                checkBox[1].Text = "Color";
                checkBox[1].PointSize = mMiddlePointSize + 2;
                checkBox[1].TextColor = Color.White;

                checkBox[2].Text = "Underline";
                checkBox[2].PointSize = mMiddlePointSize + 2;
                checkBox[2].TextColor = Color.White;
            }
            else if (text == mCaseString[1])
            {
                // TextLabel for the RadioButton
                mtextLabelRadio = new TextLabel("Radio Button");
                mtextLabelRadio.Size = new Size(250, 60);
                mtextLabelRadio.HorizontalAlignment = HorizontalAlignment.Center;
                mtextLabelRadio.VerticalAlignment = VerticalAlignment.Center;
                // Set the position of textLabel.
                mtextLabelRadio.PositionUsesPivotPoint = true;
                mtextLabelRadio.PivotPoint = PivotPoint.Center;
                mtextLabelRadio.ParentOrigin = ParentOrigin.Center;
                mtextLabelRadio.Position = new Position(0, -55, 0);
                mtextLabelRadio.PointSize = mLargePointSize;
                mtextLabelRadio.BackgroundColor = Color.White;
                mtextLabelRadio.FontFamily = "SamsungOneUI_200";

                view.Add(mtextLabelRadio);

                // Create three RadioButton
                Position checkBoxStartPosition = new Position(100, 0, 0);
                RadioButton[] radioButton = new RadioButton[3];
                for (int i = 0; i < 3; ++i)
                {
                    // New RadioButton
                    radioButton[i] = new RadioButton();
                    // Set the RadioButton position
                    radioButton[i].Position = checkBoxStartPosition + new Position(0, 40, 0) * i;
                    radioButton[i].PositionUsesPivotPoint = true;
                    radioButton[i].PivotPoint = PivotPoint.CenterLeft;
                    radioButton[i].ParentOrigin = ParentOrigin.CenterLeft;
                    if (radioButton[i].IsSelected)
                    {
                        // Make Image Visual for the selected RadioButton visual
                        radioButton[i].BackgroundImage = mResourceUrl + "/RadioButton/Selected.png";
                    }
                    else
                    {
                        // Make Image Visual for the unselected RadioButton visual
                        radioButton[i].BackgroundImage = mResourceUrl + "/RadioButton/Unselected.png";
                    }
                    
                    radioButton[i].Scale = new Vector3(0.5f, 0.5f, 0.5f);
                    // Add a callback function for the StateChanged signal
                    radioButton[i].StateChangedEvent += OnRadioButtonChanged;
                    view.Add(radioButton[i]);
                }
                // Make Text Visual
                // Set the Label of RadioButton
                radioButton[0].Text = "Red";
                radioButton[0].PointSize = mMiddlePointSize * 2.0f;
                radioButton[0].Color = Color.White;

                radioButton[1].Text = "Green";
                radioButton[1].PointSize = mMiddlePointSize * 2.0f;
                radioButton[1].Color = Color.White;

                radioButton[2].Text = "Blue";
                radioButton[2].PointSize = mMiddlePointSize * 2.0f;
                radioButton[2].Color = Color.White;
            }
            else if (text == mCaseString[2])
            {
                // TextLabel for Slider
                TextLabel mtextLabelSlider = new TextLabel("Slider");
                mtextLabelSlider.Size = new Size(250, 60);
                mtextLabelSlider.HorizontalAlignment = HorizontalAlignment.Center;
                mtextLabelSlider.VerticalAlignment = VerticalAlignment.Center;
                // Set the position of textLabel.
                mtextLabelSlider.PositionUsesPivotPoint = true;
                mtextLabelSlider.PivotPoint = PivotPoint.Center;
                mtextLabelSlider.ParentOrigin = ParentOrigin.Center;
                mtextLabelSlider.Position = new Position(0, -55, 0);
                mtextLabelSlider.PointSize = mLargePointSize;
                mtextLabelSlider.BackgroundColor = Color.White;
                mtextLabelSlider.FontFamily = "SamsungOneUI_200";

                view.Add(mtextLabelSlider);

                // A new Slider
                mSlider = new Slider();
                // Set the Slider position
                mSlider.PositionUsesPivotPoint = true;
                mSlider.PivotPoint = PivotPoint.Center;
                mSlider.ParentOrigin = ParentOrigin.Center;
                mSlider.Size = new Size(300, 25);
                // Set the Lower Bound and Upper Bound values of the Slider
                mSlider.MinValue = 0.0f;
                mSlider.MaxValue = 100.0f;
                // Set the start value
                mSlider.CurrentValue = 50;

                // Create progress visual map.
                ImageView progressImage = new ImageView()
                {
                    Size = new Size(10, 4),
                    BackgroundImage = mResourceUrl + "/Slider/img_slider_progress.png"
                };
                mSlider.Add(progressImage);

                // Create track visual map.
                ImageView trackImage = new ImageView()
                {
                    Size = new Size(10, 4),
                    BackgroundImage = mResourceUrl + "/Slider/img_slider_track.png"
                };
                mSlider.Add(trackImage);

                view.Add(mSlider);
            }
            else if (text == mCaseString[3])
            {
                // TextLabel for ProgressBar
                TextLabel mtextLabelProgressBar = new TextLabel("Progress Bar");
                mtextLabelProgressBar.Size = new Size(250, 60);
                mtextLabelProgressBar.HorizontalAlignment = HorizontalAlignment.Center;
                mtextLabelProgressBar.VerticalAlignment = VerticalAlignment.Center;
                // Set the position of textLabel.
                mtextLabelProgressBar.PositionUsesPivotPoint = true;
                mtextLabelProgressBar.PivotPoint = PivotPoint.Center;
                mtextLabelProgressBar.ParentOrigin = ParentOrigin.Center;
                mtextLabelProgressBar.Position = new Position(0, -55, 0);
                mtextLabelProgressBar.PointSize = mLargePointSize;
                mtextLabelProgressBar.BackgroundColor = Color.White;
                mtextLabelProgressBar.FontFamily = "SamsungOneUI_200";

                view.Add(mtextLabelProgressBar);

                // A new ProgressBar
                mProgress = new Progress();
                // Set the Slider position
                mProgress.PositionUsesPivotPoint = true;
                mProgress.PivotPoint = PivotPoint.Center;
                mProgress.ParentOrigin = ParentOrigin.Center;
                mProgress.Size = new Size(300, 4);

                // Create ProgressVisual
                ImageView progressImage = new ImageView()
                { 
                    BackgroundImage = mResourceUrl + "/ProgressBar/img_viewer_progress_0_129_198_100.9.png"
                };
                mProgress.Add(progressImage);

                // Create TrackVisual
                ImageView trackImage = new ImageView()
                { 
                    BackgroundImage = mResourceUrl + "/ProgressBar/img_viewer_progress_255_255_255_100.9.png"
                };
                mProgress.Add(trackImage);

                mProgress.MinValue = 0.0f;
                mProgress.MaxValue = 100.0f;
                mProgress.ProgressState = Tizen.NUI.Components.Progress.ProgressStatusType.Indeterminate;
                mProgress.TooltipText = "progressBar";

                view.Add(mProgress);

                // Create TextLabel that notices current progress
                TextLabel progressText = new TextLabel();
                // Set TextLabel position
                progressText.PositionUsesPivotPoint = true;
                progressText.HorizontalAlignment = HorizontalAlignment.Center;
                progressText.VerticalAlignment = VerticalAlignment.Center;
                progressText.ParentOrigin = ParentOrigin.Center;
                progressText.PivotPoint = PivotPoint.Center;
                progressText.FontFamily = "Samsung One 400";
                progressText.Position = new Position(150, 20);
                progressText.PointSize = mMiddlePointSize;
                // Set the default Text
                progressText.Text = (mProgress.CurrentValue * 100).ToString() + "%";
                // Set the text color
                progressText.TextColor = Color.White;
                view.Add(progressText);

                // Create Timer to show the intermediate progress state
                Timer timer = new Timer(50);
                // Timer Callback function
                timer.Tick += (obj, e) =>
                {
                    if (mProgress != null)
                    {
                        // Compute current progress
                        float progress = (float)Math.Round(mProgress.CurrentValue, 2);

                        // If the progress is complete
                        // Reset the progressValue to 0.0f
                        if (progress == 1.0f)
                        {
                            mProgress.CurrentValue = 0.0f;
                            progressText.Text = (mProgress.CurrentValue * 100).ToString() + "%";
                            return true;
                        }
                        // For the every step, add progressValue uniformly
                        else
                        {
                            mProgress.CurrentValue = progress + 0.01f;
                            progressText.Text = (mProgress.CurrentValue * 100).ToString() + "%";
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                };
                timer.Start();
            }
            // Set the common properties
            view.Size = mWindowSize;
            view.PositionUsesPivotPoint = true;
            view.PivotPoint = PivotPoint.CenterLeft;
            view.ParentOrigin = ParentOrigin.CenterLeft;
            view.Position = position;

            return view;
        }

        /// <summary>
        /// StateChanged event handling of CheckBox
        /// </summary>
        /// <param name="source">CheckBoxButton</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private void OnCheckBoxChanged(object source, EventArgs e)
        {
            // Get the source who trigger this event.
            CheckBox checkBoxButton = source as CheckBox;
            // Change the shadow state of TextLabel
            if (checkBoxButton.Text == "Shadow")
            {
                // Make a shadow for the TextLabel
                // Shadow offset is 3.0f
                // Shadow color is Black
                if (checkBoxButton.IsSelected)
                {
                    mtextLabelCheckBox.Position = new Position(3.0f, 3.0f);
                    mtextLabelCheckBox.Color = Color.Black;
                }
                else
                {
                    // Delete the shadow of the TextLabel
                    mtextLabelCheckBox.Position = new Position(0, 0);
                }
            }
            // Change the color of the TextLabel
            else if (checkBoxButton.Text == "Color")
            {
                // Set the color of the TextLabel to Red
                if (checkBoxButton.IsSelected)
                {
                    mtextLabelCheckBox.TextColor = Color.Red;
                }
                // Set the color of the TextLabel to Black
                else
                {
                    mtextLabelCheckBox.TextColor = Color.Black;
                }
            }
            // Change the Underline state of the TextLabel
            else if (checkBoxButton.Text == "Underline")
            {
                // Create a PropertyMap to change the underline status
                PropertyMap underlineMapSet = new PropertyMap();
                // Make a underline propertymap.
                if (checkBoxButton.IsSelected)
                {
                    // Underline color is black.
                    // Underline height is 3.0
                    underlineMapSet.Add("enable", new PropertyValue("true"));
                    underlineMapSet.Add("color", new PropertyValue("black"));
                    underlineMapSet.Add("height", new PropertyValue("3.0f"));
                }
                // Delete UnderLine property
                else
                {
                    underlineMapSet.Add("enable", new PropertyValue("false"));
                }

                mtextLabelCheckBox.Underline = underlineMapSet;
            }
        }

        /// <summary>
        /// StateChanged event handling of RadioButton
        /// </summary>
        /// <param name="source">RadioButton</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private void OnRadioButtonChanged(object source, EventArgs e)
        {
            // Get the source who trigger this event.
            RadioButton radioButton = source as RadioButton;
            // If the radio button is Red
            // Change the color of LabelText to Red
            if (radioButton.Text == "Red")
            {
                if (radioButton.IsSelected)
                {
                    mtextLabelRadio.TextColor = Color.Red;
                }
            }
            // If the radio button is Green
            // Change the color of LabelText to Green
            else if (radioButton.Text == "Green")
            {
                if (radioButton.IsSelected)
                {
                    mtextLabelRadio.TextColor = Color.Green;
                }
            }
            // If the radio button is Blue
            // Change the color of LabelText to Blue
            else if (radioButton.Text == "Blue")
            {
                if (radioButton.IsSelected)
                {
                    mtextLabelRadio.TextColor = Color.Blue;
                }
            }
        }

        /// <summary>
        /// Create an Text visual.
        /// </summary>
        /// <param name="text">The text of the Text visual</param>
        /// <param name="pointSize">The pointSize of the text</param>
        /// <param name="color">The color of the text</param>
        /// <returns>return a map which contain the properties of the text</returns>
        private PropertyMap CreateTextVisual(string text, float pointSize, Color color)
        {
            PropertyMap map = new PropertyMap();
            // Text Visual
            // Add each property of TextVisual
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            map.Add(TextVisualProperty.Text, new PropertyValue(text));
            // Set text color
            map.Add(TextVisualProperty.TextColor, new PropertyValue(color));
            // Set text pointSize
            map.Add(TextVisualProperty.PointSize, new PropertyValue(pointSize));
            map.Add(TextVisualProperty.FontFamily, new PropertyValue("Samsung One 400"));
            map.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
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
            // Add each property of ColorVisual
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Color));
            map.Add(ColorVisualProperty.MixColor, new PropertyValue(color));
            return map;
        }

        /// <summary>
        /// Create an Image visual.
        /// </summary>
        /// <param name="imagePath">The url of the image</param>
        /// <returns>return a map which contain the properties of the image</returns>
        private PropertyMap CreateImageVisual(string imagePath)
        {
            PropertyMap map = new PropertyMap();
            // Add each property of ImageVisual
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            map.Add(ImageVisualProperty.URL, new PropertyValue(imagePath));
            return map;
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
                case PointStateType.Down:
                {
                    mTouchedPosition = e.Touch.GetScreenPosition(0);
                    mTouched = true;
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
        /// Animate the tableView to the Negative direction
        /// </summary>
        private void AnimateAStepNegative()
        {
            // If the state is not the first one, move ImageViews and PushButton a step.
            if (mCurruntCaseIndex > 0)
            {
                mCurruntCaseIndex--;

                mScreenAnimation[1].Play();
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

                mScreenAnimation[0].Play();
            }
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
            Log.Info("Tag", "========== Hello, UIControlSample ==========");
            new UIControlSample().Run(args);
        }
    }
}
