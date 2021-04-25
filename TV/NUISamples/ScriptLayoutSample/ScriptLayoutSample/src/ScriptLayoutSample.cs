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
using Tizen.NUI.Components;
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
        // The respurce path.
        private const string resources = "/home/owner/apps_rw/org.tizen.example.ScriptLayoutSample/res";
        private string defaultThemePath = resources + "/style/DefaultTheme.json";
        private string customThemePath = resources + "/style/CustomTheme.json";
        private string demoImageDir = resources + "/images/border-4px.9.png";
        private string smallImage1 = resources + "/images/gallery-small-14.jpg";
        private string smallImage2 = resources + "/images/gallery-small-20.jpg";
        private string smallImage3 = resources + "/images/gallery-small-39.jpg";
        private string bigImage1 = resources + "/images/gallery-large-4.jpg";
        private string bigImage2 = resources + "/images/gallery-large-11.jpg";
        private string bigImage3 = resources + "/images/gallery-large-7.jpg";


        // The imageView which will contain the big image.
        private ImageView mImagePlacement;

        // For the title
        private TextLabel guide;

        // Containe all the RadioButto.
        private RadioButton[] mRadioButtons;
        // Containe all the CheckBoxButton.
        private CheckBox[] mCheckButtons;
        // Containe all the Slider
        private Slider[] mChannelSliders;

        //The default theme is Tizen4.0 theme
        private Button defaultThemeButton;
        //The custom theme is designed by apps, it could be anything if you want.
        private Button customThemeButton;

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
            //Window.Instance.BackgroundColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
            Window.Instance.BackgroundColor = Color.Black;
            //Window.Instance.BackgroundColor = Color.White;
            View focusIndicator = new View();
            FocusManager.Instance.FocusIndicator = focusIndicator;
            themePath = defaultThemePath;

            // Applies a new theme to the application.
            // This will be merged on top of the default Toolkit theme.
            // If the application theme file doesn't style all controls that the
            // application uses, then the default Toolkit theme will be used
            // instead for those controls.
            Tizen.NUI.StyleManager.Get().ApplyTheme(defaultThemePath);

            //Create the title
            guide = new TextLabel();
            guide.StyleName = "Title";
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
            guide.Text = "Script Sample \n";
            guide.TextColor = Color.White;
            guide.BackgroundColor = Color.Black;//new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            Window.Instance.GetDefaultLayer().Add(guide);

            //The container of the RadioButtons, CheckBoxButtons, Sliders, and the big ImageView
            TableView content = new TableView(6, 6);
            //content.BackgroundColor = Color.Black;
            content.Position2D = new Position2D(100, 275);
            content.Size2D = new Size2D(1000, 500);
            content.PositionUsesPivotPoint = true;
            content.ParentOrigin = ParentOrigin.TopLeft;
            content.PivotPoint = PivotPoint.TopLeft;
            content.CellPadding = new Vector2(0, 5);
            for (uint i = 0; i < content.Rows; ++i)
            {
                content.SetFitHeight(i);
            }

            for (uint i = 0; i < content.Columns; ++i)
            {
                content.SetFitWidth(i);
            }

            Window.Instance.GetDefaultLayer().Add(content);
            string[] images = { smallImage1, smallImage2, smallImage3 };
            mRadioButtons = new RadioButton[3];
            for (uint i = 0; i <= 2; i++)
            {
                ImageView image = new ImageView(images[i]);
                image.Name = "thumbnail" + (i + 1);
                image.Size2D = new Size2D(60, 60);

                mRadioButtons[i] = CreateRadioButton((i + 1).ToString());

                if (DeviceCheck.IsSREmul())
                {
                    mRadioButtons[i].StyleName = "RadioButton";
                }
                else
                {
                    mRadioButtons[i].StyleName = "TVRadioButton";
                }
                content.AddChild(mRadioButtons[i], new TableView.CellPosition(i, 0));
                content.AddChild(image, new TableView.CellPosition(i, 1));
                content.SetCellAlignment(new TableView.CellPosition(i, 0), HorizontalAlignmentType.Left, VerticalAlignmentType.Center);
                content.SetCellAlignment(new TableView.CellPosition(i, 1), HorizontalAlignmentType.Left, VerticalAlignmentType.Center);
            }

            //Set the focus to the first radioButton when the apps loaded
            FocusManager.Instance.SetCurrentFocusView(mRadioButtons[0]);

            string[] checkboxLabels = { "R", "G", "B" };
            mCheckButtons = new CheckBox[3];
            mChannelSliders = new Slider[3];
            for (uint i = 3; i <= 5;i++)
            {
                mCheckButtons[i - 3] = CreateCheckBox(checkboxLabels[i - 3]);
                mCheckButtons[i - 3].Name = "channelActiveCheckBox" + (i - 2).ToString();
                mChannelSliders[i - 3] = CreateSlider();
                mChannelSliders[i - 3].Name = "ColorSlider" + (i - 2);
                mChannelSliders[i - 3].Size2D = new Size2D(800, 80);
                // Set the current value of mChannelSliders;
                mChannelSliders[i - 3].KeyEvent += ChannelSliderKeyEvent;
                mChannelSliders[i - 3].ValueChangedEvent += OnSliderChanged;

                // Set the style according to the target devices
                if (DeviceCheck.IsSREmul())
                {
                    mCheckButtons[i - 3].StyleName = "CheckBoxButton";
                }
                else
                {
                    mCheckButtons[i - 3].StyleName = "TVCheckBoxButton";
                }

                content.AddChild(mCheckButtons[i - 3], new TableView.CellPosition(i, 0));
                content.AddChild(mChannelSliders[i - 3], new TableView.CellPosition(i, 1));
                content.SetCellAlignment(new TableView.CellPosition(i, 0), HorizontalAlignmentType.Left, VerticalAlignmentType.Center);
                content.SetCellAlignment(new TableView.CellPosition(i, 1), HorizontalAlignmentType.Left, VerticalAlignmentType.Center);
            }

            mImagePlacement = new ImageView(bigImage1);
            mImagePlacement.Size2D = new Size2D(500, 500);
            mImagePlacement.PositionUsesPivotPoint = true;
            mImagePlacement.PivotPoint = PivotPoint.TopLeft;
            mImagePlacement.ParentOrigin = ParentOrigin.TopLeft;
            mImagePlacement.Position2D = new Position2D(1320, 230);
            Window.Instance.GetDefaultLayer().Add(mImagePlacement);

            defaultThemeButton = CreateButton("DefaultTheme", "DefaultTheme", new Size2D(400, 80));
            defaultThemeButton.Position2D = new Position2D(373, 900);
            defaultThemeButton.ClickEvent += OnThemeButtonClicked;
            Window.Instance.GetDefaultLayer().Add(defaultThemeButton);
            customThemeButton = CreateButton("CustomTheme", "CustomTheme", new Size2D(400, 80));
            customThemeButton.Position2D = new Position2D(1146, 900);
            customThemeButton.ClickEvent += OnThemeButtonClicked;
            Window.Instance.GetDefaultLayer().Add(customThemeButton);

            if (DeviceCheck.IsSREmul())
            {
                defaultThemeButton.StyleName = "PushButton";
                customThemeButton.StyleName = "PushButton";
            }
            else
            {
                defaultThemeButton.StyleName = "TVPushButton";
                customThemeButton.StyleName = "TVPushButton";
            }

            defaultThemeButton.RightFocusableView = customThemeButton;
            customThemeButton.LeftFocusableView = defaultThemeButton;
            defaultThemeButton.UpFocusableView = mCheckButtons[2];
            mCheckButtons[2].DownFocusableView = defaultThemeButton;
            customThemeButton.UpFocusableView = mChannelSliders[2];
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
                        slider.CurrentValue--;
                    }
                    else if (e.Key.KeyPressedName == "Right")
                    {
                        slider.CurrentValue++;
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
                            FocusManager.Instance.SetCurrentFocusView(defaultThemeButton);
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
        private void OnButtonStateChange(object source, EventArgs e)
        {
            if (mImagePlacement != null)
            {
                if (mRadioButtons[0].IsSelected)
                {
                    mImagePlacement.SetImage(bigImage1);
                }
                else if (mRadioButtons[1].IsSelected)
                {
                    mImagePlacement.SetImage(bigImage2);
                }
                else if (mRadioButtons[2].IsSelected)
                {
                    mImagePlacement.SetImage(bigImage3);
                }
            }
        }

        /// <summary>
        /// The event will be triggered when mCheckButton state changed
        /// If state is unselected, turn off the channel's contribution
        /// </summary>
        /// <param name="source">mCheckButton.</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private void OnCheckButtonChange(object source, EventArgs e)
        {
            CheckBox checkBoxButton = source as CheckBox;
            if (checkBoxButton.Name == "channelActiveCheckBox1")
            {
                if (mCheckButtons[0].IsSelected)
                {
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorRed"), new PropertyValue(mChannelSliders[0].CurrentValue / 100));
                }
                else
                {
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorRed"), new PropertyValue(0));
                }
            }
            else if (checkBoxButton.Name == "channelActiveCheckBox2")
            {
                if (mCheckButtons[1].IsSelected)
                {
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorGreen"), new PropertyValue(mChannelSliders[1].CurrentValue / 100));
                }
                else
                {
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorGreen"), new PropertyValue(0));
                }
            }
            else if (checkBoxButton.Name == "channelActiveCheckBox3")
            {
                if (mCheckButtons[2].IsSelected)
                {
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorBlue"), new PropertyValue(mChannelSliders[2].CurrentValue / 100));
                }
                else
                {
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorBlue"), new PropertyValue(0));
                }
            }
        }

        /// <summary>
        /// The event will be triggered when slide's value is changed.
        /// Change color channel's saturation.
        /// </summary>
        /// <param name="source">slider.</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private void OnSliderChanged(object source, EventArgs e)
        {
            Slider slider = source as Slider;
            if (slider.Name == "ColorSlider1")
            {
                if (mCheckButtons[0].IsSelected)
                {
                    // Change colorRed' value to mChannelSliders[0].value / 100.
                    // colorRed.value rang 0 - 1.
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorRed"), new PropertyValue(mChannelSliders[0].CurrentValue / 100));
                }
            }
            else if (slider.Name == "ColorSlider2")
            {
                if (mCheckButtons[1].IsSelected)
                {
                    // Change colorGreen' value to mChannelSliders[0].value / 100.
                    // colorGreen.value rang 0 - 1.
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorGreen"), new PropertyValue(mChannelSliders[1].CurrentValue / 100));
                }
            }
            else if (slider.Name == "ColorSlider3")
            {
                if (mCheckButtons[2].IsSelected)
                {
                    // Change colorBlue'value to mChannelSliders[0].value / 100.
                    // colorBlue.value rang 0 - 1.
                    mImagePlacement.SetProperty(mImagePlacement.GetPropertyIndex("colorBlue"), new PropertyValue(mChannelSliders[2].CurrentValue / 100));
                }
            }
        }

        /// <summary>
        /// The event will be triggered when themeButton clicked.
        /// </summary>
        /// <param name="source">mResetButton.</param>
        /// <param name="e">event</param>
        /// <returns>The consuming flag.</returns>
        private void OnThemeButtonClicked(object source, EventArgs e)
        {
            Button button = source as Button;

            // Judge which style button is clicked.
            if (button.Text == "DefaultTheme")
            {
                Tizen.Log.Fatal("NUI", "Change the theme to Default: " + defaultThemePath);
                themePath = defaultThemePath;
            }
            else if (button.Text == "CustomTheme")
            {
                Tizen.Log.Fatal("NUI", "Change the theme to Custom: " + customThemePath);
                themePath = customThemePath;
            }
            // Change the style.
            Tizen.NUI.StyleManager.Get().ApplyTheme(themePath);
            button.TextColor = Color.Black;
            button.BackgroundImage = focusImagePath;
        }

        /// <summary>
        /// The event will be triggered when  have key clicked and focus on spin.
        /// </summary>
        /// <param name="source">checkbox.</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool CheckBoxStateChanged(object source, EventArgs e)
        {
            CheckBox checkbox = source as CheckBox;
            // Show testText's shadow or not.
            if (checkbox.Text == "Shadow")
            {
            }
            // The testText auto scroll or not.
            else if (checkbox.Text == "Color")
            {
            }
            // Show testText's underline or not.
            else if (checkbox.Text == "Underline")
            {
            }

            return true;
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
            //titleActor.PointSize = 15.0f;
            titleActor.PointSize = DeviceCheck.PointSize15;
            titleActor.HorizontalAlignment = HorizontalAlignment.Center;
            titleActor.MultiLine = false;

            return titleActor;
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
            contentPane.Padding = new Extents(4, 4, 4, 4);
            contentPane.Size2D = Window.Instance.Size;
            return contentPane;
        }

        ///// <summary>
        ///// Create text propertyMap used to set Button.Label
        ///// </summary>
        ///// <param name="text">text</param>
        ///// <returns>The created propertyMap</returns>
        //private PropertyMap CreateText(string text)
        //{
        //    PropertyMap textVisual = new PropertyMap();
        //    // Visual's type is textVisual;
        //    textVisual.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
        //    // Set textVisual's text.
        //    textVisual.Add(TextVisualProperty.Text, new PropertyValue(text));
        //    // Text color is black.
        //    textVisual.Add(TextVisualProperty.TextColor, new PropertyValue(Color.Black));
        //    // The text size is 7.
        //    //textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(7));
        //    textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(DeviceCheck.PointSize7));
        //    // Texts place at the center of horizontal direction.
        //    textVisual.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
        //    // Texts place at the center of vertical direction.
        //    textVisual.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
        //    textVisual.Add(TextVisualProperty.FontFamily, new PropertyValue("Samsung One 400"));
        //    return textVisual;
        //}

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

        protected Button CreateButton(string name, string text, Size2D size = null)
        {
            Button button = new Button();
            if (null != size)
            {
                button.Size2D = size;
            }
            button.Focusable = true;
            button.Name = name;
            button.BackgroundImage = normalImagePath;
            button.TextColor = Color.White;

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

        /// <summary>
        /// Create an Image visual.
        /// </summary>
        /// <param name="imagePath">The url of the image</param>
        /// <returns>return thr radioButton instance</returns>
        private PropertyMap CreateImageVisual(string imagePath)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            map.Add(ImageVisualProperty.URL, new PropertyValue(imagePath));
            return map;
        }

        /// <summary>
        /// Create a RadioButton.
        /// </summary>
        /// <param name="text">The text of the radioButton</param>
        /// <returns>return the radioButton instance</returns>
        private RadioButton CreateRadioButton(string text)
        {
            RadioButton _radiobutton = new RadioButton();
            _radiobutton = new RadioButton();
            _radiobutton.TextPadding = new Extents(10, 12, 0, 0);
            _radiobutton.PointSize = DeviceCheck.PointSize8;
            _radiobutton.Text = text;
            _radiobutton.TextColor = Color.White;
            _radiobutton.Size2D = new Size2D(120, 48);
            _radiobutton.Focusable = true;
            _radiobutton.ParentOrigin = ParentOrigin.TopLeft;
            _radiobutton.PivotPoint = PivotPoint.TopLeft;
            _radiobutton.StateChangedEvent += OnButtonStateChange;

            // Create unfocused and unselected visual.
            PropertyMap unselectedMap = new PropertyMap();
            unselectedMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            // Create focused and unselected visual.
            PropertyMap focusUnselectedMap = new PropertyMap();
            focusUnselectedMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            // Create focused and selected visual.
            PropertyMap focusSelectedMap = new PropertyMap();
            focusSelectedMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            // Create unfocused and selected visual.
            PropertyMap selectedMap = new PropertyMap();
            selectedMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            unselectedMap.Add(ImageVisualProperty.URL, new PropertyValue(normalRadioImagePath));
            focusUnselectedMap.Add(ImageVisualProperty.URL, new PropertyValue(focusedRadioImagePath));
            focusSelectedMap.Add(ImageVisualProperty.URL, new PropertyValue(focusedSelectRadioImagePath));
            selectedMap.Add(ImageVisualProperty.URL, new PropertyValue(selectRadioImagePath));

            PropertyMap unselectedTheme2Map = new PropertyMap();
            unselectedTheme2Map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            // Create focused and unselected visual.
            PropertyMap focusUnselectedTheme2Map = new PropertyMap();
            focusUnselectedTheme2Map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            // Create focused and selected visual.
            PropertyMap focusSelectedTheme2Map = new PropertyMap();
            focusSelectedTheme2Map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            // Create unfocused and selected visual.
            PropertyMap selectedTheme2Map = new PropertyMap();
            selectedTheme2Map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            unselectedTheme2Map.Add(ImageVisualProperty.URL, new PropertyValue(normalRadioTheme2ImagePath));
            focusUnselectedTheme2Map.Add(ImageVisualProperty.URL, new PropertyValue(focusedRadioTheme2ImagePath));
            focusSelectedTheme2Map.Add(ImageVisualProperty.URL, new PropertyValue(focusedSelectRadioTheme2ImagePath));
            selectedTheme2Map.Add(ImageVisualProperty.URL, new PropertyValue(selectRadioTheme2ImagePath));

            // Change the image when focus changed.
            _radiobutton.FocusGained += (obj, e) =>
            {
                if (themePath == defaultThemePath)
                {
                    _radiobutton.BackgroundImage = focusedSelectRadioImagePath;
                }
                else if (themePath == customThemePath)
                {
                    _radiobutton.BackgroundImage = focusedSelectRadioTheme2ImagePath;
                }
            };

            // Change the image when focus changed.
            _radiobutton.FocusLost += (obj, e) =>
            {

                if (themePath == defaultThemePath)
                {
                    _radiobutton.BackgroundImage = selectRadioImagePath;

                }
                else if (themePath == customThemePath)
                {
                    _radiobutton.BackgroundImage = selectRadioTheme2ImagePath;
                }
            };
            return _radiobutton;
        }

        private CheckBox CreateCheckBox(string text)
        {
            CheckBox _checkboxbutton = new CheckBox();
            _checkboxbutton.Text = text;
            if (text == "R")
            {
                _checkboxbutton.TextColor = Color.Red;
            }
            else if (text == "G")
            {
                _checkboxbutton.TextColor = Color.Green;
            }
            else if (text == "B")
            {
                _checkboxbutton.TextColor = Color.Blue;
            }

            _checkboxbutton.TextPadding = new Extents(10, 12, 0, 0);
            _checkboxbutton.Size2D = new Size2D(120, 48);
            _checkboxbutton.Focusable = true;
            _checkboxbutton.ParentOrigin = ParentOrigin.TopLeft;
            _checkboxbutton.PivotPoint = PivotPoint.TopLeft;
            _checkboxbutton.StateChangedEvent += OnCheckButtonChange;

            // Create unfocused and unselected visual.
            PropertyMap unselectedMap = new PropertyMap();
            unselectedMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            // Create unfocused and selected visual.
            PropertyMap selectedMap = new PropertyMap();
            selectedMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            PropertyMap focusUnselectedMap = new PropertyMap();
            focusUnselectedMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            PropertyMap focusSelectedMap = new PropertyMap();
            focusSelectedMap.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            focusUnselectedMap.Add(ImageVisualProperty.URL, new PropertyValue(focusCheckImagePath));
            focusSelectedMap.Add(ImageVisualProperty.URL, new PropertyValue(focusSelectCheckImagePath));
            unselectedMap.Add(ImageVisualProperty.URL, new PropertyValue(normalCheckImagePath));
            selectedMap.Add(ImageVisualProperty.URL, new PropertyValue(selectCheckImagePath));


            PropertyMap unselectedTheme2Map = new PropertyMap();
            unselectedTheme2Map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            // Create unfocused and selected visual.
            PropertyMap selectedTheme2Map = new PropertyMap();
            selectedTheme2Map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            PropertyMap focusUnselectedTheme2Map = new PropertyMap();
            focusUnselectedTheme2Map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));

            PropertyMap focusSelectedTheme2Map = new PropertyMap();
            focusSelectedTheme2Map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            focusUnselectedTheme2Map.Add(ImageVisualProperty.URL, new PropertyValue(focusCheckTheme2ImagePath));
            focusSelectedTheme2Map.Add(ImageVisualProperty.URL, new PropertyValue(focusSelectCheckTheme2ImagePath));
            unselectedTheme2Map.Add(ImageVisualProperty.URL, new PropertyValue(normalCheckTheme2ImagePath));
            selectedTheme2Map.Add(ImageVisualProperty.URL, new PropertyValue(selectCheckTheme2ImagePath));

            // Change the image when focus changed.
            _checkboxbutton.FocusGained += (obj, e) =>
            {

                if (themePath == defaultThemePath)
                {
                    _checkboxbutton.BackgroundImage = focusCheckImagePath;
                }
                else if (themePath == customThemePath)
                {
                    _checkboxbutton.BackgroundImage = focusCheckTheme2ImagePath;
                }
            };

            // Change the image when focus changed.
            _checkboxbutton.FocusLost += (obj, e) =>
            {
                if (themePath == defaultThemePath)
                {
                    _checkboxbutton.BackgroundImage = selectCheckImagePath;
                }
                else if (themePath == customThemePath)
                {
                    _checkboxbutton.BackgroundImage = selectCheckTheme2ImagePath;
                }
            };
            return _checkboxbutton;
        }

        private Slider CreateSlider()
        {
            Slider horizontalSlider = new Slider();
            horizontalSlider.ParentOrigin = ParentOrigin.TopLeft;
            horizontalSlider.PositionUsesPivotPoint = true;
            horizontalSlider.PivotPoint = PivotPoint.TopLeft;
            //horizontalSlider.Position2D = new Position2D(25, 12);
            horizontalSlider.Size2D = new Size2D(800, 80);
            horizontalSlider.Focusable = true;
            horizontalSlider.MinValue = 0.0f;
            horizontalSlider.MaxValue = 100.0f;
            horizontalSlider.CurrentValue = 100;
            horizontalSlider.ThumbImageURLSelector = new StringSelector()
            {
                Normal = handleUnSelectedVisual,
                Pressed = handleSelectedVisual
            };
            horizontalSlider.ThumbImageBackgroundURLSelector = new StringSelector()
            {
                All = trackVisual
            };
            horizontalSlider.ValueChangedEvent += OnSliderChanged;
            return horizontalSlider;
        }

        private string normalCheckImagePath = resources + "/images/CheckBox/Unselected.png";
        private string focusCheckImagePath = resources + "/images/CheckBox/Focused_01.png";
        private string selectCheckImagePath = resources + "/images/CheckBox/Selected.png";
        private string focusSelectCheckImagePath = resources + "/images/CheckBox/Focused_02.png";

        private string normalCheckTheme2ImagePath = resources + "/images/CustomeTheme/checkbox-unselected-disabled.png";
        private string focusCheckTheme2ImagePath = resources + "/images/CustomeTheme/checkbox-unselected.png";
        private string selectCheckTheme2ImagePath = resources + "/images/CustomeTheme/checkbox-selected-disabled.png";
        private string focusSelectCheckTheme2ImagePath = resources + "/images/CustomeTheme/checkbox-selected.png";

        private string normalRadioImagePath = resources + "/images/RadioButton/Unselected.png";
        private string focusedRadioImagePath = resources + "/images/RadioButton/Focused_01.png";
        private string focusedSelectRadioImagePath = resources + "/images/RadioButton/Focused_02.png";
        private string selectRadioImagePath = resources + "/images/RadioButton/Selected.png";

        private string normalRadioTheme2ImagePath = resources + "/images/CustomeTheme/radio-button-unselected-disabled.png";
        private string focusedRadioTheme2ImagePath = resources + "/images/CustomeTheme/radio-button-unselected.png";
        private string focusedSelectRadioTheme2ImagePath = resources + "/images/CustomeTheme/radio-button-selected.png";
        private string selectRadioTheme2ImagePath = resources + "/images/CustomeTheme/radio-button-selected-disabled.png";

        string handleUnSelectedVisual = resources + "/images/Slider/img_slider_handler_h_unselected.png";
        string handleSelectedVisual = resources + "/images/Slider/img_slider_handler_h_selected.png";
        string trackVisual = resources + "/images/Slider/img_slider_track.png";
        //string progressVisual = resources + "/images/Slider/img_slider_progress.png";

        private string normalImagePath = resources + "/images/Button/btn_bg_25_25_25_95.9.png";
        private string focusImagePath = resources + "/images/Button/btn_bg_255_255_255_200.9.png";
        private string pressImagePath = resources + "/images/Button/btn_bg_0_129_198_100.9.png";

        private string themePath = null;
    }
}