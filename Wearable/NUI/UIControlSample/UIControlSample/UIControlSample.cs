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
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
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

        // CheckBox and RadioButton texts
        private static string[] mCheckBoxTexts = { "Background", "Color", "Underline" };
        private static string[] mRadioButtonTexts = { "Red", "Green", "Blue" };

        // Title TextLabel for checkBox
        TextLabel mTitleLabelCheckBox;
        // Title TextLabel for RadioButton
        TextLabel mTitleLabelRadio;
        // Slider Control
        Slider mSlider;
        TextLabel mSliderText;
        // ProgressBar Control
        Progress mProgress;
        TextLabel mProgressText;
        // Switch Control
        TextLabel mSwitchText;

        // Timer used for process indicator
        private Timer mTimer;

        // A string list of sample cases
        private string[] mCaseString =
        {
             "CheckBox",
             "Radio Button",
             "Slider",
             "Progress Bar",
             "Switch"
        };
        // A number of sample cases
        private uint mCaseCount = 5;
        private int mCurruntCaseIndex;

        // A View contains every Controls.
        private View mControlsView;

        private readonly Size mControlSize = new Size(150, 150);

        // UI properties
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

            // Create Timer for mProgress
            mTimer = new Timer(50);
            // Timer Callback function
            mTimer.Tick += (obj, e) => {
                if (mProgress != null) {
                    mProgress.CurrentValue += 1.0f;
                    if (Math.Ceiling(mProgress.CurrentValue) >= 100.0f)
                    {
                        mProgress.CurrentValue = 0.0f;
                    }
                    mProgressText.Text = Math.Floor(mProgress.CurrentValue).ToString() + "%";
                }
                return true;
            };
            mTimer.Start();

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

        private TextLabel CreateTitleLabel(string text) {
            TextLabel titleLabel = new TextLabel(text);
            titleLabel.Size = new Size(250, 60);
            titleLabel.HorizontalAlignment = HorizontalAlignment.Center;
            titleLabel.VerticalAlignment = VerticalAlignment.Center;
            titleLabel.PositionUsesPivotPoint = true;
            titleLabel.PivotPoint = PivotPoint.Center;
            titleLabel.ParentOrigin = ParentOrigin.Center;
            titleLabel.Position = new Position(0, -55);
            titleLabel.PointSize = mLargePointSize;
            titleLabel.BackgroundColor = Color.White;
            titleLabel.FontFamily = "SamsungOneUI_200";
            return titleLabel;
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
            
            // TextLabel for the control
            TextLabel titleLabel = CreateTitleLabel(text);
            view.Add(titleLabel);

            // CheckBox sample
            if (text == mCaseString[0])
            {
                // Store title label
                mTitleLabelCheckBox = titleLabel;
                // Create three CheckBoxs
                Position checkBoxStartPosition = new Position(70, 0);
                CheckBox[] checkBox = new CheckBox[3];
                for (int i = 0; i < 3; ++i)
                {
                    // New CheckBox with label
                    checkBox[i] = new CheckBox();
                    checkBox[i].Text = mCheckBoxTexts[i];
                    // Set the CheckBox position
                    checkBox[i].Position = checkBoxStartPosition + new Position(0, 40 * i);
                    checkBox[i].PositionUsesPivotPoint = true;
                    checkBox[i].PivotPoint = PivotPoint.CenterLeft;
                    checkBox[i].ParentOrigin = ParentOrigin.CenterLeft;

                    checkBox[i].Scale = new Vector3(0.8f, 0.8f, 0.8f);
                    checkBox[i].Size = new Size(220, 48);
                    checkBox[i].PointSize = mMiddlePointSize + 2;
                    checkBox[i].TextColor = Color.White;
                    // Add a callback function for the StateChanged signal
                    checkBox[i].StateChangedEvent += OnCheckBoxChanged;
                    view.Add(checkBox[i]);
                }
            }
            else if (text == mCaseString[1])
            {
                // Store title label
                mTitleLabelRadio = titleLabel;

                // NOTE: Note NUI.Components.RadioButton are not grouped in parent view as NUI.UIComponents.RadioButton was.
                // When radioButton is selected, other wont be unselected now.

                // Create three RadioButton
                Position checkBoxStartPosition = new Position(70, 0);
                RadioButton[] radioButton = new RadioButton[3];
                for (int i = 0; i < 3; ++i)
                {
                    // New RadioButton with label
                    radioButton[i] = new RadioButton();
                    radioButton[i].Text = mRadioButtonTexts[i];
                    // Set the RadioButton position
                    radioButton[i].Position = checkBoxStartPosition + new Position(0, 40 * i);
                    radioButton[i].PositionUsesPivotPoint = true;
                    radioButton[i].PivotPoint = PivotPoint.CenterLeft;
                    radioButton[i].ParentOrigin = ParentOrigin.CenterLeft;

                    radioButton[i].Scale = new Vector3(0.8f, 0.8f, 0.8f);
                    radioButton[i].Size = new Size(220, 48);
                    radioButton[i].PointSize = mMiddlePointSize + 2;
                    radioButton[i].TextColor = Color.White;
                    // Add a callback function for the StateChanged signal
                    radioButton[i].StateChangedEvent += OnRadioButtonChanged;
                    view.Add(radioButton[i]);
                }
            }
            else if (text == mCaseString[2])
            {
                // A new Slider
                mSlider = new Slider();
                // Set the Slider position
                mSlider.PositionUsesPivotPoint = true;
                mSlider.PivotPoint = PivotPoint.Center;
                mSlider.ParentOrigin = ParentOrigin.Center;
                mSlider.TrackThickness = 4;
                mSlider.BgTrackColor = new Color(0, 0, 0, 0.1f);
                mSlider.SlidedTrackColor = new Color(0.05f, 0.63f, 0.9f, 1);
                mSlider.ThumbSize = new Size(60, 60);
                mSlider.Size.Width = 300;
                // Set the Lower Bound and Upper Bound values of the Slider
                mSlider.MinValue = 0.0f;
                mSlider.MaxValue = 100.0f;
                // Set the start value
                mSlider.CurrentValue = 50.0f;
                // Add a callback function for the StateChanged signal
                mSlider.ValueChangedEvent += OnSliderValueChanged;
                view.Add(mSlider);

                // Create TextLabel that notices current progress
                mSliderText = new TextLabel();
                mSliderText.PositionUsesPivotPoint = true;
                mSliderText.HorizontalAlignment = HorizontalAlignment.Center;
                mSliderText.VerticalAlignment = VerticalAlignment.Center;
                mSliderText.ParentOrigin = ParentOrigin.Center;
                mSliderText.PivotPoint = PivotPoint.Center;
                mSliderText.FontFamily = "Samsung One 400";
                mSliderText.Position = new Position(0, 60);
                mSliderText.PointSize = mMiddlePointSize + 2;
                mSliderText.TextColor = Color.White;
                mSliderText.Text = Math.Floor(mSlider.CurrentValue).ToString() + "%";
                view.Add(mSliderText);
            }
            else if (text == mCaseString[3])
            {
                // A new ProgressBar
                mProgress = new Progress();
                // Set the Slider position
                mProgress.PositionUsesPivotPoint = true;
                mProgress.PivotPoint = PivotPoint.Center;
                mProgress.ParentOrigin = ParentOrigin.Center;
                mProgress.Size = new Size(300, 5);
                mProgress.MinValue = 0.0f;
                mProgress.MaxValue = 100.0f;
                mProgress.CurrentValue = 20.0f;
                mProgress.TrackColor = new Color(0, 0, 0, 0.1f);
                mProgress.ProgressColor = new Color(0.75f, 0.63f, 0.9f, 1);
                view.Add(mProgress);

                // Create TextLabel that notices current progress
                mProgressText = new TextLabel();
                // Set TextLabel position
                mProgressText.PositionUsesPivotPoint = true;
                mProgressText.HorizontalAlignment = HorizontalAlignment.Center;
                mProgressText.VerticalAlignment = VerticalAlignment.Center;
                mProgressText.ParentOrigin = ParentOrigin.Center;
                mProgressText.PivotPoint = PivotPoint.Center;
                mProgressText.FontFamily = "Samsung One 400";
                mProgressText.Position = new Position(150, 20);
                mProgressText.PointSize = mMiddlePointSize;
                mProgressText.TextColor = Color.White;
                // Set the default Text
                mProgressText.Text = Math.Floor(mProgress.CurrentValue).ToString() + "%";
                view.Add(mProgressText);
            
            }
            else if (text == mCaseString[4])
            {
                // Store title label
                mSwitchText = titleLabel;

                Switch switch_ = new Switch();
                switch_.PositionUsesPivotPoint = true;
                switch_.PivotPoint = PivotPoint.Center;
                switch_.ParentOrigin = ParentOrigin.Center;
                switch_.Position = new Position(0, 30);
                switch_.StateChangedEvent += OnSwitchStateChanged;
                view.Add(switch_);
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
            if (checkBoxButton.Text == "Background")
            {
                // Set the background-color of the TextLabel to Bluish
                if (checkBoxButton.IsSelected)
                {
                    mTitleLabelCheckBox.BackgroundColor = new Color(0.05f, 0.63f, 0.9f, 1);
                }
                // Set the background color of the TextLabel to White
                else
                {
                    mTitleLabelCheckBox.BackgroundColor = Color.White;
                }
            }
            // Change the color of the TextLabel
            else if (checkBoxButton.Text == "Color")
            {
                // Set the color of the TextLabel to Red
                if (checkBoxButton.IsSelected)
                {
                    mTitleLabelCheckBox.TextColor = Color.Red;
                }
                // Set the color of the TextLabel to Black
                else
                {
                    mTitleLabelCheckBox.TextColor = Color.Black;
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

                mTitleLabelCheckBox.Underline = underlineMapSet;
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
                    mTitleLabelRadio.TextColor = Color.Red;
                }
            }
            // If the radio button is Green
            // Change the color of LabelText to Green
            else if (radioButton.Text == "Green")
            {
                if (radioButton.IsSelected)
                {
                    mTitleLabelRadio.TextColor = Color.Green;
                }
            }
            // If the radio button is Blue
            // Change the color of LabelText to Blue
            else if (radioButton.Text == "Blue")
            {
                if (radioButton.IsSelected)
                {
                    mTitleLabelRadio.TextColor = Color.Blue;
                }
            }
        }

        /// <summary>
        /// StateChanged event handling of Slider
        /// </summary>
        /// <param name="source">Slider</param>
        /// <param name="e">event</param>
        private void OnSliderValueChanged(object source, EventArgs e)
        {
            // Get the source who trigger this event.
            Slider slider = source as Slider;
            mSliderText.Text = Math.Floor(slider.CurrentValue).ToString() + "%";
        }

        /// <summary>
        /// StateChanged event handling of Slider
        /// </summary>
        /// <param name="source">Slider</param>
        /// <param name="e">event</param>
        private void OnSwitchStateChanged(object source, EventArgs e)
        {
            // Get the source who trigger this event.
            Switch switch_ = source as Switch;
            mSwitchText.BackgroundColor = switch_.IsSelected ? new Color(0.1f, 0.5f, 0.7f, 1.0f) : Color.White;
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
            Log.Info("Tag", "========== UIControlSample ==========");
            new UIControlSample().Run(args);
        }
    }
}
