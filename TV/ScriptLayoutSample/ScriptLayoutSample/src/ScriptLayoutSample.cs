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
using System.Runtime.InteropServices;
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
        // The path of image.
        private const string resources = "/home/owner/apps_rw/org.tizen.example.ScriptLayoutSample/res";
        private string demoThemeOnePath = resources + "/style/style-example-theme-one.json";
        private string demoThemeTwoPath = resources + "/style/style-example-theme-two.json";
        private string demoThemeThreePath = resources + "/style/style-example-theme-three.json";
        private string demoImageDir = resources + "/images/border-4px.9.png";
        private string smallImage1 = resources + "/images/gallery-small-14.jpg";
        private string smallImage2 = resources + "/images/gallery-small-20.jpg";
        private string smallImage3 = resources + "/images/gallery-small-39.jpg";
        private string bigImage1 = resources + "/images/gallery-large-4.jpg";
        private string bigImage2 = resources + "/images/gallery-large-11.jpg";
        private string bigImage3 = resources + "/images/gallery-large-7.jpg";
        private string  defaultControlAreImagePath =  resources + "/images/popup_button_background.9.png";
        // Pan Container added to the Stage will contain other views.
        private View mContentPane;
        // The imageView will contain big image.
        private ImageView mImagePlacement;
        private PushButton mResetButton, okayButton;
        // The popup will decided that reset value of sliders or not.
        private Popup mResetPopup;
        private RadioButton[] mRadioButtons;
        private CheckBoxButton[] mCheckButtons;
        private Slider[] mChannelSliders;
        private PushButton[] mThemeButtons;

        /// <summary>
        /// The constructor with null
        /// </summary>
        public ScriptLayoutSample() : base()
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
        /// Script Layout Sample Application initialization.
        /// </summary>
        private void Initialize()
        {
            // Create the background view.
            mContentPane = CreateContentPane();
            Window.Instance.GetDefaultLayer().Add(mContentPane);

            // Set the background color.
            Window.Instance.BackgroundColor = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
            // Applies a new theme to the application.
            // This will be merged on top of the default Toolkit theme.
            // If the application theme file doesn't style all controls that the
            // application uses, then the default Toolkit theme will be used
            // instead for those controls.
            StyleManager.Get().ApplyTheme(demoThemeOnePath);

            // Create content Layout used to put other views.
            // Use textLabel can put other views in appropriate position commodiously.
            TableView contentLayout = new TableView(5, 1);
            contentLayout.Name = "ContentLayout";
            // Set the size of contentLayout.
            contentLayout.SizeWidth = Window.Instance.Size.Width - 100;
            contentLayout.SizeHeight = Window.Instance.Size.Height - 100;
            // Set the position of contentLayout.
            contentLayout.PositionUsesPivotPoint = true;
            contentLayout.PivotPoint = PivotPoint.Center;
            contentLayout.ParentOrigin = ParentOrigin.Center;
            contentLayout.CellPadding = new Vector2(10, 10);

            // Assign all rows the size negotiation property of fitting to children
            for (uint i = 0; i < contentLayout.Rows; ++i)
            {
                if (i != 1)
                {
                    // Row 1 should fill.
                    contentLayout.SetFitHeight(i);
                }
            }

            mContentPane.Add(contentLayout);

            // Create a textLabel.
            TextLabel mTitle = new TextLabel("Styling Example");
            mTitle.Name = "Title";
            // Set the style of mTitle.
            // In style one, mTitle.textColor is black.
            // In style two, mTitle.textColor is blue and background is black.
            // In style there, as same as style two.
            mTitle.StyleName = "Title";
            mTitle.PointSize = 10.0f;
            mTitle.PositionUsesPivotPoint = true;
            mTitle.PivotPoint = PivotPoint.TopCenter;
            mTitle.ParentOrigin = ParentOrigin.TopCenter;
            mTitle.WidthResizePolicy = ResizePolicyType.FillToParent;
            mTitle.HeightResizePolicy = ResizePolicyType.UseNaturalSize;
            mTitle.HorizontalAlignment = HorizontalAlignment.Center;
            contentLayout.Add(mTitle);

            // Create a table view, be used to select image.
            // On the left is the radioGroup.
            // On the right is the mImagePlacement.
            TableView imageSelectLayout = new TableView(1, 2);
            imageSelectLayout.Name = "ImageSelectLayout";
            imageSelectLayout.WidthResizePolicy = ResizePolicyType.FillToParent;
            imageSelectLayout.HeightResizePolicy = ResizePolicyType.FillToParent;
            imageSelectLayout.PositionUsesPivotPoint = true;
            imageSelectLayout.PivotPoint = PivotPoint.Center;
            imageSelectLayout.ParentOrigin = ParentOrigin.Center;
            imageSelectLayout.CellPadding = new Vector2(10, 10);

            // Fit radio button column to child width, leave image to fill remainder
            imageSelectLayout.SetFitWidth(0);
            contentLayout.Add(imageSelectLayout);

            // Create a table view, be used to put radioGroup
            // On the left is RadioButtons.
            // On the right is th small images.
            TableView radioButtonsLayout = new TableView(3, 2);
            radioButtonsLayout.Name = "RadioButtonsLayout";
            radioButtonsLayout.HeightResizePolicy = ResizePolicyType.FillToParent;

            // Leave each row to fill to parent height
            // Set each column to fit to child width
            radioButtonsLayout.SetFitWidth(0);
            radioButtonsLayout.SetFitWidth(1);
            radioButtonsLayout.CellPadding = new Vector2(6.0f, 0.0f);

            /// Add RadioButtonsLayout to ImageSelectLayout.
            imageSelectLayout.AddChild(radioButtonsLayout, new TableView.CellPosition(0, 0));
            // The alignment on cell(0, 0) is HorizontalAlignmentType.Left and VerticalAlignmentType.Top.
            imageSelectLayout.SetCellAlignment(new TableView.CellPosition(0, 0), HorizontalAlignmentType.Left,  VerticalAlignmentType.Top);

            string[] images = {smallImage1, smallImage2, smallImage3};
            mRadioButtons = new RadioButton[3];
            // Create image Views whose URL are small Image.
            for (uint i = 0; i < 3; ++i)
            {
                ImageView image = new ImageView(images[i]);
                image.Name = "thumbnail" + (i + 1);
                image.Size2D = new Size2D(60, 60);

                // Create mRadioButtons.
                // Setting mRadioButtons Name to mark it.
                mRadioButtons[i] = new RadioButton((i + 1) + "");
                mRadioButtons[i].Name = "imageSelectButton" + (i + 1);
                mRadioButtons[i].PositionUsesPivotPoint = true;
                mRadioButtons[i].PivotPoint = PivotPoint.TopLeft;
                mRadioButtons[i].ParentOrigin = ParentOrigin.TopLeft;
                mRadioButtons[i].Selected = false;
                mRadioButtons[i].StateChanged += OnButtonStateChange;

                radioButtonsLayout.AddChild(mRadioButtons[i], new TableView.CellPosition(i, 0));
                radioButtonsLayout.AddChild(image, new TableView.CellPosition(i, 1));

                // The alignment on cell is HorizontalAlignmentType.Center and VerticalAlignmentType.Center.
                radioButtonsLayout.SetCellAlignment(new TableView.CellPosition(i, 0), HorizontalAlignmentType.Center, VerticalAlignmentType.Center);
                radioButtonsLayout.SetCellAlignment(new TableView.CellPosition(i, 1), HorizontalAlignmentType.Center, VerticalAlignmentType.Center);
            }

            // Set mRadioButton[0] selected.
            mRadioButtons[0].Selected = true;

            // Create mImagePlacement used to put big image.
            mImagePlacement = new ImageView(bigImage1);
            mImagePlacement.Size2D = new Size2D(400, 400);
            mImagePlacement.PositionUsesPivotPoint = true;
            mImagePlacement.PivotPoint = PivotPoint.Center;
            mImagePlacement.ParentOrigin = ParentOrigin.Center;
            imageSelectLayout.AddChild(mImagePlacement, new TableView.CellPosition(0, 1));
            // The alignment on cell(0, 1) is HorizontalAlignmentType.Center and VerticalAlignmentType.Center.
            imageSelectLayout.SetCellAlignment(new TableView.CellPosition(0, 1), HorizontalAlignmentType.Center, VerticalAlignmentType.Center);

            // Create a small images.
            TableView channelSliderLayout = new TableView(3, 3);
            channelSliderLayout.Name = "ChannelSliderLayout";

            // Contains a column of check buttons and a column of sliders for R/G/B
            channelSliderLayout.WidthResizePolicy = ResizePolicyType.FillToParent;
            channelSliderLayout.HeightResizePolicy = ResizePolicyType.UseNaturalSize;
            channelSliderLayout.PositionUsesPivotPoint = true;
            channelSliderLayout.PivotPoint = PivotPoint.Center;
            channelSliderLayout.ParentOrigin = ParentOrigin.Center;
            channelSliderLayout.CellPadding = new Vector2(10, 10);

            // Set each row to fit to child height
            channelSliderLayout.SetFitHeight(0);
            channelSliderLayout.SetFitHeight(1);
            channelSliderLayout.SetFitHeight(2);

            // Set each column to fit to child width
            channelSliderLayout.SetFitWidth(0);
            channelSliderLayout.SetFitWidth(1);

            // Create checkBoxButtonGroup and mChannelSliders
            // mcheckBoxButtons[i] can turn off/on the mChannelSliders[i]'s contribution.
            // mChannelSliders can change mImagePlacement's R/G/B property.
            contentLayout.Add(channelSliderLayout);
            string[] checkboxLabels = {"R", "G", "B"};
            mCheckButtons = new CheckBoxButton[3];
            mChannelSliders = new Slider[3];
            // Create mcheckBoxButtons.
            for (uint i = 0; i < 3; i++)
            {
                mCheckButtons[i] = new CheckBoxButton();
                mCheckButtons[i].Name = "channelActiveCheckBox" + (i + 1);
                mCheckButtons[i].PositionUsesPivotPoint = true;
                mCheckButtons[i].PivotPoint = PivotPoint.Center;
                mCheckButtons[i].ParentOrigin = ParentOrigin.Center;
                // Set all mcheckBoxButtons are true.
                // So that, mChannelSliders can change the style of mImagePlacement.
                mCheckButtons[i].Selected = true;

                // Bind mcheckBoxButtons to OnCheckButtonChange.
                mCheckButtons[i].StateChanged += OnCheckButtonChange;

                channelSliderLayout.AddChild(mCheckButtons[i], new TableView.CellPosition(i, 0));
                channelSliderLayout.SetCellAlignment(new TableView.CellPosition(i, 0), HorizontalAlignmentType.Center, VerticalAlignmentType.Center);

                // Create TextLabel mark mcheckBoxButtons.
                TextLabel textLabel = new TextLabel(checkboxLabels[i]);
                textLabel.PointSize = 8.0f;
                textLabel.Name = "ColorLabel" + (i + 1);
                // Set the style of textLabel.
                // In style, ColorLabel1's textColor is (1, 0, 0, 1)
                //           ColorLabel1's textColor is (0, 1, 0, 1)
                //           ColorLabel1's textColor is (0.3, 0.3, 1, 1)
                textLabel.StyleName = "ColorLabel" + (i + 1);
                textLabel.PositionUsesPivotPoint = true;
                textLabel.PivotPoint = PivotPoint.Center;
                textLabel.ParentOrigin = ParentOrigin.Center;
                textLabel.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
                textLabel.HeightResizePolicy = ResizePolicyType.UseNaturalSize;

                channelSliderLayout.AddChild(textLabel, new TableView.CellPosition(i, 1));
                channelSliderLayout.SetCellAlignment(new TableView.CellPosition(i, 1), HorizontalAlignmentType.Center, VerticalAlignmentType.Center);

                // Create mChannelSliders which can change mImagePlacement's R/G/B property.
                mChannelSliders[i] = new Slider();
                mChannelSliders[i].Name = "ColorSlider" + (i + 1);
                // Set the style of mChannelSliders.
                // In style one, mChannelSliders use original style and don't show popup.
                // In style two, don't show popup and value
                //               change mcheckBoxButtons' progress Visual
                //               valuePrecision is 0
                //               handleVisual's size is 48 * 48
                //               trackVisual's size is 10 * 10
                // In style three, don't change style.
                mChannelSliders[i].StyleName = "ColorSlider" + (i + 1);
                mChannelSliders[i].PositionUsesPivotPoint = true;
                mChannelSliders[i].PivotPoint = PivotPoint.Center;
                mChannelSliders[i].ParentOrigin = ParentOrigin.Center;
                mChannelSliders[i].WidthResizePolicy = ResizePolicyType.FillToParent;
                mChannelSliders[i].HeightResizePolicy = ResizePolicyType.FillToParent;
                // Set the min value of mChannelSliders;
                mChannelSliders[i].LowerBound = 0.0f;
                // Set the max value of mChannelSliders;
                mChannelSliders[i].UpperBound = 100.0f;
                mChannelSliders[i].ValuePrecision = 0;
                // Set the current value of mChannelSliders;
                mChannelSliders[i].Value = 100;
                mChannelSliders[i].ShowPopup = false;
                mChannelSliders[i].ShowValue = true;
                mChannelSliders[i].Focusable = true;
                mChannelSliders[i].KeyEvent += ChannelSliderKeyEvent;

                channelSliderLayout.AddChild(mChannelSliders[i], new TableView.CellPosition(i, 2));
                channelSliderLayout.SetCellAlignment(new TableView.CellPosition(i, 2), HorizontalAlignmentType.Left, VerticalAlignmentType.Center);

                mChannelSliders[i].ValueChanged += OnSliderChanged;
            }

            // Create mResetButton which can reset mChannelSliders' value to 100.
            mResetButton = new PushButton();
            mResetButton.LabelText = "Reset";
            mResetButton.Name = "ResetButton";
            mResetButton.HeightResizePolicy = ResizePolicyType.UseNaturalSize;
            mResetButton.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
            mResetButton.Clicked += OnResetClicked;

            contentLayout.AddChild(mResetButton, new TableView.CellPosition(3, 0));
            contentLayout.SetCellAlignment(new TableView.CellPosition(3, 0), HorizontalAlignmentType.Center, VerticalAlignmentType.Center);

            // Create themeButtonLayout which will be put on themeButton.
            TableView themeButtonLayout = new TableView(1, 4);
            themeButtonLayout.Name = "ThemeButtonsLayout";
            themeButtonLayout.CellPadding = new Vector2(6.0f, 0.0f);

            // Set the position of themeButtonLayout.
            themeButtonLayout.PositionUsesPivotPoint = true;
            themeButtonLayout.PivotPoint = PivotPoint.Center;
            themeButtonLayout.ParentOrigin = ParentOrigin.Center;
            themeButtonLayout.WidthResizePolicy = ResizePolicyType.FillToParent;
            themeButtonLayout.HeightResizePolicy = ResizePolicyType.FitToChildren;
            themeButtonLayout.CellPadding = new Vector2(10, 10);
            themeButtonLayout.SetFitHeight(0);

            // Create textLabel define styleButton.
            TextLabel label = new TextLabel("Theme: ");
            label.HeightResizePolicy = ResizePolicyType.UseNaturalSize;
            label.WidthResizePolicy = ResizePolicyType.UseNaturalSize;
            label.PointSize = 8.0f;
            // Change the style of label.
            // In style, textColor is (0, 1, 1, 1).
            label.StyleName = "ThemeLabel";
            label.PositionUsesPivotPoint = true;
            label.PivotPoint = PivotPoint.TopCenter;
            label.ParentOrigin = ParentOrigin.TopCenter;
            themeButtonLayout.AddChild(label, new TableView.CellPosition(0, 0));
            themeButtonLayout.SetCellAlignment(new TableView.CellPosition(0, 0), HorizontalAlignmentType.Left, VerticalAlignmentType.Center);

            mThemeButtons = new PushButton[3];
            for (uint i = 0; i < 3; ++i)
            {
                // Create mThemeButtons which can change the style of this application.
                mThemeButtons[i] = new PushButton();
                mThemeButtons[i].Name = "ThemeButton";
                mThemeButtons[i].StyleName = "ThemeButton";
                mThemeButtons[i].PositionUsesPivotPoint = true;
                mThemeButtons[i].PivotPoint = PivotPoint.Center;
                mThemeButtons[i].ParentOrigin = ParentOrigin.Center;
                mThemeButtons[i].WidthResizePolicy = ResizePolicyType.FillToParent;
                mThemeButtons[i].HeightResizePolicy = ResizePolicyType.UseNaturalSize;
                // Bind mThemeButtons to OnThemeButtonClicked.
                mThemeButtons[i].Clicked += OnThemeButtonClicked;
                themeButtonLayout.AddChild(mThemeButtons[i], new TableView.CellPosition(0, 1 + i));
            }

            // Set the label of mThemeButtons.
            mThemeButtons[0].Label = CreateText("Lite");
            mThemeButtons[1].Label = CreateText("Style1");
            mThemeButtons[2].Label = CreateText("Style2");

            contentLayout.Add(themeButtonLayout);

            // Move focus to mRadioButtons[0]
            // Control the direction of focus.
            FocusManager.Instance.SetCurrentFocusView(mRadioButtons[0]);
            mRadioButtons[0].DownFocusableView = mRadioButtons[1];
            mRadioButtons[1].DownFocusableView = mRadioButtons[2];
            mRadioButtons[2].DownFocusableView = mCheckButtons[0];
            mRadioButtons[1].UpFocusableView = mRadioButtons[0];
            mRadioButtons[2].UpFocusableView = mRadioButtons[1];

            mCheckButtons[0].RightFocusableView = mChannelSliders[0];
            mCheckButtons[1].RightFocusableView = mChannelSliders[1];
            mCheckButtons[2].RightFocusableView = mChannelSliders[2];
            mCheckButtons[0].DownFocusableView = mCheckButtons[1];
            mCheckButtons[1].DownFocusableView = mCheckButtons[2];
            mCheckButtons[2].DownFocusableView = mResetButton;
            mCheckButtons[0].UpFocusableView = mRadioButtons[2];
            mCheckButtons[1].UpFocusableView = mCheckButtons[0];
            mCheckButtons[2].UpFocusableView = mCheckButtons[1];

            mResetButton.UpFocusableView = mCheckButtons[2];
            mResetButton.DownFocusableView = mThemeButtons[0];
            mThemeButtons[0].UpFocusableView = mResetButton;
            mThemeButtons[1].UpFocusableView = mResetButton;
            mThemeButtons[2].UpFocusableView = mResetButton;
            mThemeButtons[0].RightFocusableView = mThemeButtons[1];
            mThemeButtons[1].RightFocusableView = mThemeButtons[2];
            mThemeButtons[2].RightFocusableView = mThemeButtons[0];
            mThemeButtons[0].LeftFocusableView = mThemeButtons[2];
            mThemeButtons[1].LeftFocusableView = mThemeButtons[0];
            mThemeButtons[2].LeftFocusableView = mThemeButtons[1];

            Window.Instance.KeyEvent += AppBack;
        }

        /// <summary>
        /// Callback  by slider.
        /// </summary>
        /// <param name="source">slider.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private bool ChannelSliderKeyEvent(object source, View.KeyEventArgs e)
        {
            // Judge which slider is focused.
            Slider slider = source as Slider;
            int index = -1;
            if (slider.Name == "ColorSlider1")
            {
                index = 0;
            }
            else if (slider.Name == "ColorSlider2")
            {
                index = 1;
            }
            else if (slider.Name == "ColorSlider3")
            {
                index = 2;
            }

            // If Left/Right key clicked, change the value of the slider.
            // If left and Right key clicked, change the slider's value.
            // If return clicked, move to mCheckButton.
            if (e.Key.State == Key.StateType.Down)
            {
                if (index != -1)
                {
                    if (e.Key.KeyPressedName == "Left")
                    {
                        slider.Value--;
                    }
                    else if (e.Key.KeyPressedName == "Right")
                    {
                        slider.Value++;
                    }
                    else if (e.Key.KeyPressedName == "Up")
                    {
                        if (index == 0)
                        {
                            FocusManager.Instance.SetCurrentFocusView(mRadioButtons[2]);
                        }
                        else
                        {
                            FocusManager.Instance.SetCurrentFocusView(mChannelSliders[--index]);
                        }
                    }
                    else if (e.Key.KeyPressedName == "Down")
                    {
                        if (index == 2)
                        {
                            FocusManager.Instance.SetCurrentFocusView(mResetButton);
                        }
                        else
                        {
                            FocusManager.Instance.SetCurrentFocusView(mChannelSliders[++index]);
                        }
                    }
                    else if (e.Key.KeyPressedName == "Return")
                    {
                        FocusManager.Instance.SetCurrentFocusView(mCheckButtons[index]);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// The event will be triggered when mRadioButton state changed
        /// On button press, called twice, once to tell new button it's selected,
        /// once to tell old button it isn't selected?
        /// Save / restore slider states per image
        /// </summary>
        /// <param name="source">mRadioButton.</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool OnButtonStateChange(object source, EventArgs e)
        {
            if (mImagePlacement != null)
            {
                if (mRadioButtons[0].Selected)
                {
                    mImagePlacement.SetImage(bigImage1);
                }
                else if (mRadioButtons[1].Selected)
                {
                    mImagePlacement.SetImage(bigImage2);
                }
                else if (mRadioButtons[2].Selected)
                {
                    mImagePlacement.SetImage(bigImage3);
                }
            }

            return true;
        }

        /// <summary>
        /// The event will be triggered when mCheckButton state changed
        /// If state is unselected, turn off the channel's contribution
        /// </summary>
        /// <param name="source">mCheckButton.</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool OnCheckButtonChange(object source, EventArgs e)
        {
            CheckBoxButton checkBoxButton = source as CheckBoxButton;
            if (checkBoxButton.Name == "channelActiveCheckBox1")
            {
                if (mCheckButtons[0].Selected)
                {
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorRed"), new PropertyValue(mChannelSliders[0].Value / 100));
                }
                else
                {
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorRed"), new PropertyValue(0));
                }
            }
            else if (checkBoxButton.Name == "channelActiveCheckBox2")
            {
                if (mCheckButtons[1].Selected)
                {
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorGreen"), new PropertyValue(mChannelSliders[1].Value / 100));
                }
                else
                {
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorGreen"), new PropertyValue(0));
                }
            }
            else if (checkBoxButton.Name == "channelActiveCheckBox3")
            {
                if (mCheckButtons[2].Selected)
                {
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorBlue"), new PropertyValue(mChannelSliders[2].Value / 100));
                }
                else
                {
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorBlue"), new PropertyValue(0));
                }
            }

            return true;
        }

        /// <summary>
        /// The event will be triggered when slide's value is changed.
        /// Change color channel's saturation.
        /// </summary>
        /// <param name="source">slider.</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool OnSliderChanged(object source, EventArgs e)
        {
            Slider slider = source as Slider;
            if (slider.Name == "ColorSlider1")
            {
                if (mCheckButtons[0].Selected)
                {
                    // Change colorRed' value to mChannelSliders[0].value / 100.
                    // colorRed.value rang 0 - 1.
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorRed"), new PropertyValue(mChannelSliders[0].Value / 100));
                }
            }
            else if (slider.Name == "ColorSlider2")
            {
                if (mCheckButtons[1].Selected)
                {
                    // Change colorGreen' value to mChannelSliders[0].value / 100.
                    // colorGreen.value rang 0 - 1.
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorGreen"), new PropertyValue(mChannelSliders[1].Value / 100));
                }
            }
            else if (slider.Name == "ColorSlider3")
            {
                if (mCheckButtons[2].Selected)
                {
                    // Change colorBlue'value to mChannelSliders[0].value / 100.
                    // colorBlue.value rang 0 - 1.
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorBlue"), new PropertyValue(mChannelSliders[2].Value / 100));
                }
            }

            return true;
        }

        /// <summary>
        /// The event will be triggered when mResetButton is clicked.
        /// If mResetButton is created, Show the popup and move focus to abutting,
        /// If not, create popup, then show it.
        /// </summary>
        /// <param name="source">mResetButton.</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool OnResetClicked(object source, EventArgs e)
        {
            if (!mResetPopup)
            {
                mResetPopup = CreateResetPopup();
            }

            Window.Instance.GetDefaultLayer().Add(mResetPopup);

            mResetPopup.DisplayState = Popup.DisplayStateType.Shown;
            FocusManager.Instance.SetCurrentFocusView(okayButton);
            return true;
        }

        /// <summary>
        /// The event will be triggered when themeButton clicked.
        /// </summary>
        /// <param name="source">mResetButton.</param>
        /// <param name="e">event</param>
        /// <returns>The consuming flag.</returns>
        private bool OnThemeButtonClicked(object source, EventArgs e)
        {
            string themePath = null;
            PushButton button = source as PushButton;

            // Judge which style button is clicked.
            if (button.LabelText == "Lite")
            {
                themePath = demoThemeOnePath;
            }
            else if (button.LabelText == "Style1")
            {
                themePath = demoThemeTwoPath;
            }
            else if (button.LabelText == "Style2")
            {
                themePath = demoThemeThreePath;
            }

            // Change the style.
            StyleManager.Get().ApplyTheme(themePath);

            return true;
        }

        /// <summary>
        /// Create the reset popup.
        /// The popup content two buttons(okaybutton and cancelButton)
        /// Just okaybutton can reset the value of slider. Both can hide popup.
        /// </summary>
        /// <returns>
        /// The popup created in this function.
        /// </returns>
        Popup CreateResetPopup()
        {
            // Create the popup.
            Popup popup = new Popup();
            popup.Name = "Reset Popup";
            // Set the position of popup.
            popup.PositionUsesPivotPoint = true;
            popup.PivotPoint = PivotPoint.Center;
            popup.ParentOrigin = ParentOrigin.Center;
            // Set the size of popup. Size will adjust to wrap around all children.
            popup.HeightResizePolicy = ResizePolicyType.FitToChildren;
            popup.WidthResizePolicy = ResizePolicyType.FitToChildren;
            popup.TailVisibility = false;
            // When popup is touched, hide popup.
            popup.TouchedOutside += HidePopup;

            // Set title of popup.
            popup.SetTitle(CreateTitle("Reset channels"));

            TextLabel text = new TextLabel("This will reset the channel data to full value. Are you sure?");
            text.Name = "PopupContentText";
            // Set the style of text.
            // In style, the textColor is (1, 1, 0, 1).
            text.StyleName = "PopupBody";
            text.WidthResizePolicy = ResizePolicyType.FillToParent;
            text.HeightResizePolicy = ResizePolicyType.DimensionDependency;
            text.MultiLine = true;
            text.PointSize = 10.0f;
            text.Padding = new Vector4(10.0f, 10.0f, 20.0f, 0.0f);
            // Set Content of popup.
            popup.SetContent(text);

            // Create footer will be used to popup.footer.
            ImageView footer = new ImageView(defaultControlAreImagePath);
            footer.Name = "PopupFooter";
            // Size is to fill up to the actor's parent's bounds.
            // Aspect ratio is not maintained.
            footer.WidthResizePolicy = ResizePolicyType.FillToParent;
            // Size is fixed as set by SetSize.
            footer.HeightResizePolicy = ResizePolicyType.Fixed;
            footer.Size2D = new Size2D(0, 100);
            footer.PositionUsesPivotPoint = true;
            footer.PivotPoint = PivotPoint.Center;
            footer.ParentOrigin = ParentOrigin.Center;

            // Create tableView Layout will be added to footer.
            TableView footerLayout = new TableView(1, 2);
            footerLayout.PositionUsesPivotPoint = true;
            footerLayout.PivotPoint = PivotPoint.Center;
            footerLayout.ParentOrigin = ParentOrigin.Center;
            footerLayout.WidthResizePolicy = ResizePolicyType.FillToParent;
            footerLayout.HeightResizePolicy = ResizePolicyType.FillToParent;

            // Create okaybutton which can reset mChannelSliders' value to 100.
            okayButton = new PushButton();
            okayButton.Name = "PopupControlOk";
            okayButton.Label = CreateText("OK");
            okayButton.Clicked += OnReset;
            okayButton.PositionUsesPivotPoint = true;
            okayButton.PivotPoint = PivotPoint.Center;
            okayButton.ParentOrigin = ParentOrigin.Center;
            // The actors size will be ( ParentSize + SizeRelativeToParentFactor ).
            okayButton.HeightResizePolicy = ResizePolicyType.SizeFixedOffsetFromParent;
            okayButton.WidthResizePolicy = ResizePolicyType.SizeFixedOffsetFromParent;
            okayButton.SizeModeFactor = new Vector3(-20.0f, -20.0f, 0.0f);

            // Create cancelPushButton which just can hide popup.
            PushButton cancelButton = new PushButton();
            cancelButton.Name = "PopupControlCancel";
            cancelButton.StyleName = "PopupControlCancel";
            cancelButton.Label = CreateText("Cancel");
            cancelButton.Clicked += OnResetCancelled;
            cancelButton.PositionUsesPivotPoint = true;
            cancelButton.PivotPoint = PivotPoint.Center;
            cancelButton.ParentOrigin = ParentOrigin.Center;
            cancelButton.HeightResizePolicy = ResizePolicyType.SizeFixedOffsetFromParent;
            cancelButton.WidthResizePolicy = ResizePolicyType.SizeFixedOffsetFromParent;
            cancelButton.SizeModeFactor = new Vector3(-20.0f, -20.0f, 0.0f);

            footerLayout.SetCellAlignment(new TableView.CellPosition(0, 0), HorizontalAlignmentType.Center, VerticalAlignmentType.Center);
            footerLayout.SetCellAlignment(new TableView.CellPosition(0, 1), HorizontalAlignmentType.Center, VerticalAlignmentType.Center);
            footerLayout.AddChild(okayButton, new TableView.CellPosition(0, 0));
            footerLayout.AddChild(cancelButton, new TableView.CellPosition(0, 1));
            footerLayout.CellPadding = new Vector2(10, 10);
            footer.Add(footerLayout);

            popup.SetFooter(footer);
            return popup;
        }

        /// <summary>
        /// Create the textLabel which be used to set popup.Title
        /// </summary>
        /// <param name="title">title.</param>
        /// <returns>
        /// The textLabel created in this function.
        /// </returns>
        TextLabel CreateTitle(string title)
        {
            TextLabel titleActor = new TextLabel(title);
            titleActor.Name = "titleActor";
            // Set titleActor's style.
            // The titleActor's textColor is black.
            titleActor.StyleName = "PopupTitle";
            titleActor.PointSize = 15.0f;
            titleActor.HorizontalAlignment = HorizontalAlignment.Center;
            titleActor.MultiLine = false;

            return titleActor;
        }

        /// <summary>
        /// The event will be triggered when popup clicked.
        /// Hide the mResetPopup and move focus to mResetButton.
        /// </summary>
        /// <param name="source">mResetPopup.</param>
        /// <param name="e">event</param>
        private void HidePopup(object source, Popup.TouchedOutsideEventArgs e)
        {
            if (mResetPopup != null)
            {
                mResetPopup.DisplayState = Popup.DisplayStateType.Hidden;
                FocusManager.Instance.SetCurrentFocusView(mResetButton);
            }
        }

        /// <summary>
        /// The event will be triggered when okayButton clicked
        /// Reset the sliders for this image
        /// Hide popup and move focus to mResetButton
        /// </summary>
        /// <param name="source">okayButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private bool OnReset(object source, EventArgs e)
        {
            for (uint i = 0; i < 3; i++)
            {
                mChannelSliders[i].Value = 100;
            }

            mResetPopup.DisplayState = Popup.DisplayStateType.Hidden;
            FocusManager.Instance.SetCurrentFocusView(mResetButton);
            return true;
        }

        /// <summary>
        /// The event will be triggered when cancelButton clicked
        /// Hide popup
        /// </summary>
        /// <param name="source">cancelButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private bool OnResetCancelled(object source, EventArgs e)
        {
            mResetPopup.DisplayState = Popup.DisplayStateType.Hidden;
            return true;
        }

        /// <summary>
        /// Create content pane.
        /// </summary>
        /// <returns>
        /// content pane
        /// </returns>
        private View CreateContentPane()
        {
            ImageView contentPane = new ImageView(demoImageDir);
            contentPane.Name = "ContentPane";
            contentPane.PositionUsesPivotPoint = true;
            contentPane.ParentOrigin = ParentOrigin.Center;
            contentPane.PivotPoint = PivotPoint.Center;
            contentPane.Padding = new Vector4(4, 4, 4, 4);
            contentPane.Size2D = Window.Instance.Size;
            return contentPane;
        }

        /// <summary>
        /// Create text propertyMap used to set Button.Label
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>The created propertyMap</returns>
        private PropertyMap CreateText(string text)
        {
            PropertyMap textVisual = new PropertyMap();
            // Visual's type is textVisual;
            textVisual.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            // Set textVisual's text.
            textVisual.Add(TextVisualProperty.Text, new PropertyValue(text));
            // Text color is black.
            textVisual.Add(TextVisualProperty.TextColor, new PropertyValue(Color.Black));
            // The text size is 7.
            textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(7));
            // Texts place at the center of horizontal direction.
            textVisual.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            // Texts place at the center of vertical direction.
            textVisual.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            textVisual.Add(TextVisualProperty.FontFamily, new PropertyValue("Samsung One 400"));
            return textVisual;
        }

        /// <summary>
        /// This Application will be exited when back key entered.
        /// </summary>
        /// <param name="source">Window.Instance</param>
        /// <param name="e">event</param>
        private void AppBack(object source, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "XF86Back")
                {
                    this.Exit();
                }
            }
        }
    }
}