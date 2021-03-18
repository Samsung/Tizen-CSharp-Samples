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

namespace ScriptLayoutSample
{
    /// <summary>
    /// Applications often contain multiple controls that have an identical appearance.
    /// Setting the appearance of each individual control can be repetitive and error prone.
    /// Instead, styles can be created that customize control appearance by grouping and settings properties available on the control type.
    /// This sample demonstrates using json file to create styles.
    /// </summary>
    class ScriptLayoutSample : NUIApplication
    {
        /// <summary>
        /// A list of image URL
        /// </summary>
        private static string mResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource;
        private static string mCustomThemeUrl = mResourceUrl + "/styles/CustomTheme.json";
        private static string mDefaultThemeUrl = mResourceUrl + "/styles/DefaultTheme.json";

        // Contains all the RadioButton.
        private RadioButton[] mRadioButtons;
        // Contains all the CheckBoxButton.
        private CheckBoxButton[] mCheckBoxButtons;
        // Contains all the Slider

        private Size2D mWindowSize;
        private float mLargePointSize = 10.0f;
        private float mMiddlePointSize = 5.0f;

        private bool mThemeDefault = false;

        private TextLabel mTitle;
        private TextLabel mRadioButtonTitle;
        private TextLabel mCheckBoxButtonTitle;
        private PushButton mPushButton;

        private string[] ButtonText =
        {
            "Selected",
            "Unselected"
        };

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.BackgroundColor = Color.Black;
            mWindowSize = Window.Instance.Size;

            // Applies a new theme to the application.
            // This will be merged on top of the default Toolkit theme.
            // If the application theme file doesn't style all controls that the
            // application uses, then the default Toolkit theme will be used
            // instead for those controls.
            StyleManager.Get().ApplyTheme(mCustomThemeUrl);

            // Create Title TextLabel
            mTitle = new TextLabel("Script");
            mTitle.HorizontalAlignment = HorizontalAlignment.Center;
            mTitle.VerticalAlignment = VerticalAlignment.Center;
            // Set Text color to White
            mTitle.TextColor = Color.White;
            mTitle.PositionUsesPivotPoint = true;
            mTitle.ParentOrigin = ParentOrigin.TopCenter;
            mTitle.PivotPoint = PivotPoint.TopCenter;
            mTitle.Position2D = new Position2D(0, mWindowSize.Height / 10);
            // Use Samsung One 600 font
            mTitle.FontFamily = "Samsung One 600";
            // Not use MultiLine of TextLabel
            mTitle.MultiLine = false;
            mTitle.PointSize = mLargePointSize;
            Window.Instance.GetDefaultLayer().Add(mTitle);

            // Create subTitle TextLabel
            mRadioButtonTitle = new TextLabel("Radio Button");
            mRadioButtonTitle.HorizontalAlignment = HorizontalAlignment.Center;
            mRadioButtonTitle.VerticalAlignment = VerticalAlignment.Center;
            // Set Text color to White
            mRadioButtonTitle.TextColor = Color.White;
            mRadioButtonTitle.PositionUsesPivotPoint = true;
            mRadioButtonTitle.ParentOrigin = ParentOrigin.TopCenter;
            mRadioButtonTitle.PivotPoint = PivotPoint.TopLeft;
            mRadioButtonTitle.Position2D = new Position2D(-150, 110);
            // Use Samsung One 600 font
            mRadioButtonTitle.FontFamily = "Samsung One 600";
            // Not use MultiLine of TextLabel
            mRadioButtonTitle.MultiLine = false;
            mRadioButtonTitle.PointSize = mMiddlePointSize;
            Window.Instance.GetDefaultLayer().Add(mRadioButtonTitle);

            // Create RadioButtons
            mRadioButtons = new RadioButton[2];
            for (int i = 0; i < 2; ++i)
            {
                // Create new RadioButton
                mRadioButtons[i] = new RadioButton();
                // Set RadioButton size
                mRadioButtons[i].Size2D = new Size2D(300, 65);
                mRadioButtons[i].PositionUsesPivotPoint = true;
                mRadioButtons[i].ParentOrigin = ParentOrigin.Center;
                mRadioButtons[i].PivotPoint = PivotPoint.CenterLeft;
                // Set RadioButton Position
                mRadioButtons[i].Position = new Position(-150, -20, 0) + new Position(160, 0, 0) * i;
                mRadioButtons[i].Label = CreateTextVisual(ButtonText[i], mMiddlePointSize * 2.0f, Color.White);
                mRadioButtons[i].Scale = new Vector3(0.5f, 0.5f, 0.5f);
                // Add click event callback function
                mRadioButtons[i].Clicked += OnRadioButtonClicked;

                Window.Instance.GetDefaultLayer().Add(mRadioButtons[i]);
            }

            mRadioButtons[0].Selected = true;

            // Create TextLabel for the CheckBox Button
            mCheckBoxButtonTitle = new TextLabel("CheckBox Button");
            mCheckBoxButtonTitle.HorizontalAlignment = HorizontalAlignment.Center;
            mCheckBoxButtonTitle.VerticalAlignment = VerticalAlignment.Center;
            mCheckBoxButtonTitle.TextColor = Color.White;
            mCheckBoxButtonTitle.PositionUsesPivotPoint = true;
            mCheckBoxButtonTitle.ParentOrigin = ParentOrigin.TopCenter;
            mCheckBoxButtonTitle.PivotPoint = PivotPoint.TopLeft;
            mCheckBoxButtonTitle.Position2D = new Position2D(-150, 190);
            mCheckBoxButtonTitle.FontFamily = "Samsung One 600";
            mCheckBoxButtonTitle.MultiLine = false;
            mCheckBoxButtonTitle.PointSize = mMiddlePointSize;
            Window.Instance.GetDefaultLayer().Add(mCheckBoxButtonTitle);

            // Create CheckBoxButton
            mCheckBoxButtons = new CheckBoxButton[2];
            for (int i = 0; i < 2; ++i)
            {
                // Create new CheckBox Button
                mCheckBoxButtons[i] = new CheckBoxButton();
                mCheckBoxButtons[i].PositionUsesPivotPoint = true;
                mCheckBoxButtons[i].ParentOrigin = ParentOrigin.Center;
                mCheckBoxButtons[i].PivotPoint = PivotPoint.CenterLeft;
                mCheckBoxButtons[i].Position = new Position(-150, 60, 0) + new Position(160, 0, 0) * i;
                mCheckBoxButtons[i].Label = CreateTextVisual(ButtonText[1], mMiddlePointSize + 1.0f, Color.White);
                mCheckBoxButtons[i].Scale = new Vector3(0.8f, 0.8f, 0.8f);
                mCheckBoxButtons[i].StateChanged += OnCheckBoxButtonClicked;

                Window.Instance.GetDefaultLayer().Add(mCheckBoxButtons[i]);
            }

            // Create PushButton
            mPushButton = new PushButton();
            mPushButton.PositionUsesPivotPoint = true;
            mPushButton.ParentOrigin = ParentOrigin.BottomCenter;
            mPushButton.PivotPoint = PivotPoint.BottomCenter;
            mPushButton.Name = "ChangeTheme";
            mPushButton.Size2D = new Size2D(230, 35);
            mPushButton.ClearBackground();
            mPushButton.Position = new Position(0, -50, 0);
            PropertyMap normalTextMap = CreateTextVisual("Change Theme", mMiddlePointSize, Color.White);
            mPushButton.Label = normalTextMap;
            mPushButton.Clicked += OnPushButtonClicked;
            Window.Instance.GetDefaultLayer().Add(mPushButton);

            Window.Instance.KeyEvent += OnKey;
        }

        /// <summary>
        /// Reset properties for the style change
        /// </summary>
        private void ResetProperties()
        {
            // Reset title pointsize
            mTitle.PointSize = mLargePointSize;
            // Reset radio button title pointsize
            mRadioButtonTitle.PointSize = mMiddlePointSize;
            // Reset checkbox title pointsize
            mCheckBoxButtonTitle.PointSize = mMiddlePointSize;
            // Reset Text visual of each checkbox label
            for (int i = 0; i < 2; ++i)
            {
                mCheckBoxButtons[i].Label = CreateTextVisual(mCheckBoxButtons[i].LabelText, mMiddlePointSize + 1.0f, Color.White);
            }
            // Reset Pushbutton's Background
            mPushButton.ClearBackground();
            // Reset Text properties of label of pushbutton
            PropertyMap normalTextMap = CreateTextVisual("Change Theme", mMiddlePointSize, Color.White);
            mPushButton.Label = normalTextMap;
        }

        /// <summary>
        /// Called by RadioButton
        /// </summary>
        /// <param name="source">The clicked button</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool OnRadioButtonClicked(object source, EventArgs e)
        {
            for (int i = 0; i < 2; ++i)
            {
                // Change the LabelText of each RadioButton
                if (mRadioButtons[i].Selected)
                {
                    mRadioButtons[i].Label = CreateTextVisual("Selected", mMiddlePointSize * 2.0f, Color.White);
                }
                else
                {
                    mRadioButtons[i].Label = CreateTextVisual("Unselected", mMiddlePointSize * 2.0f, Color.White);
                }
            }

            return true;
        }

        /// <summary>
        /// Called by CheckBoxButton
        /// </summary>
        /// <param name="source">The clicked button</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool OnCheckBoxButtonClicked(object source, EventArgs e)
        {
            for (int i = 0; i < 2; ++i)
            {
                // Change the LabelText of each CheckBoxButton
                if (mCheckBoxButtons[i].Selected)
                {
                    mCheckBoxButtons[i].Label = CreateTextVisual("Selected", mMiddlePointSize + 1.0f, Color.White);
                }
                else
                {
                    mCheckBoxButtons[i].Label = CreateTextVisual("Unselected", mMiddlePointSize + 1.0f, Color.White);
                }
            }

            return true;
        }

        /// <summary>
        /// Called by buttons
        /// </summary>
        /// <param name="source">The clicked button</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool OnPushButtonClicked(object source, EventArgs e)
        {
            // Change the Theme
            if (mThemeDefault)
            {
                StyleManager.Get().ApplyTheme(mCustomThemeUrl);
            }
            else
            {
                StyleManager.Get().ApplyTheme(mDefaultThemeUrl);
            }

            mThemeDefault = !mThemeDefault;
            ResetProperties();
            return true;
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
        /// Create an Text visual.
        /// </summary>
        /// <param name="text">The text of the Text visual</param>
        /// <param name="pointSize">The pointSize of the text</param>
        /// <param name="color">The color of the text</param>
        /// <returns>return a map which contain the properties of the text</returns>
        private PropertyMap CreateTextVisual(string text, float pointSize, Color color)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            map.Add(TextVisualProperty.Text, new PropertyValue(text));
            map.Add(TextVisualProperty.TextColor, new PropertyValue(color));
            map.Add(TextVisualProperty.PointSize, new PropertyValue(pointSize));
            map.Add(TextVisualProperty.FontFamily, new PropertyValue("Samsung One 400"));
            map.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            map.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            return map;
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
        /// The enter point of ScriptLayoutSample.
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[] args)
        {
            Log.Info("Tag", "========== Hello, ScriptLayoutSample ==========");
            new ScriptLayoutSample().Run(args);
        }
    }
}
