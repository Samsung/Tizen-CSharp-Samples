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

namespace TextSample
{
    /// <summary>
    /// This sample application demonstrates each of the text controls and a few common text entry scenarios.
    /// </summary>
    class TextSample : NUIApplication
    {
        // textLabel be used to show the effect of TextLabel.
        private TextLabel textLabel;
        // textLabel be used to show the effect of TextField.
        private TextField textField;
        // textLabel be used to show the effect of TextEditor.
        private TextEditor textEditor;
        // pushButton be used to trigger the effect of Text.
        private Button[] pushButton;
        // checkBoxButton be used to trigger the effect of Text.
        private CheckBox[] checkBoxButton;
        // tableView be used to put pushButton and checkBoxButton.
        private TableView tableView;
        // Some kinds of LANGUAGES.
        private string[] LANGUAGES =
        {
            "العَرَبِيةُ(Arabic)", "অসমীয়া লিপি(Assamese)", "Español(Spanish)", "한국어(Korean)", "漢語(Chinese)",
            "A control which renders a short text string.(English)"
        };
        // The index of LANGUAGES.
        private int itemLanguage = 0;
        // The count of languages.
        private int NumLanguage = 6;
        
        private TextLabel guide;

        private const string resources = "/home/owner/apps_rw/org.tizen.example.TextSample/res/images";
        private string normalImagePath = resources + "/Button/btn_bg_25_25_25_95.9.png";
        private string focusImagePath = resources + "/Button/btn_bg_255_255_255_200.9.png";
        private string pressImagePath = resources + "/Button/btn_bg_0_129_198_100.9.png";
        private string checkBoxNormalImagePath = resources + "/CheckBox/Unselected.png";
        private string checkBoxFocusImagePath = resources + "/CheckBox/Focused_01.png";
        private string checkBoxSelectImagePath = resources + "/CheckBox/Selected.png";
        private string checkBoxFocusSelectImagePath = resources + "/CheckBox/Focused_02.png";

        /// <summary>
        /// The constructor with null
        /// </summary>
        public TextSample() : base()
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
        /// Text Sample Application initialisation.
        /// </summary>
        public void Initialize()
        {
            // Set the background Color of Window.
            Window.Instance.BackgroundColor = Color.Black;
            View focusView = new View();
            FocusManager.Instance.FocusIndicator = focusView;

            guide = new TextLabel();
            guide.HorizontalAlignment = HorizontalAlignment.Center;
            guide.VerticalAlignment = VerticalAlignment.Center;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            guide.TextColor = Color.White;
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = ParentOrigin.TopLeft;
            guide.PivotPoint = PivotPoint.TopLeft;
            guide.Size2D = new Size2D(1920, 96);
            guide.FontFamily = "Samsung One 600";
            guide.Position2D = new Position2D(0, 94);
            guide.MultiLine = false;
            //guide.PointSize = 15.0f;
            guide.PointSize = DeviceCheck.PointSize8;
            guide.Text = "Text Sample";
            Window.Instance.GetDefaultLayer().Add(guide);

            TextLabel label = CreatTextLabel("TextLabel");
            label.Position2D = new Position2D(150, 215);
            Window.Instance.GetDefaultLayer().Add(label);

            TextLabel field = CreatTextLabel("TextField");
            field.Position2D = new Position2D(150, 380);
            Window.Instance.GetDefaultLayer().Add(field);

            TextLabel editor = CreatTextLabel("TextEditor");
            editor.Position2D = new Position2D(150, 615);
            Window.Instance.GetDefaultLayer().Add(editor);

            // Create textLabel.
            CreateTextLabel();
            // Create textfield;
            CreateTextField();
            // Create textEditor;
            CreateTextEditor();
            // Create buttons to show some functions or properties.
            CreateButtons();

            // Set pushButton[0] as original focus view.
            FocusManager.Instance.SetCurrentFocusView(pushButton[0]);

            // Set the rule of focus move.
            pushButton[0].UpFocusableView = textEditor;
            textLabel.DownFocusableView = textField;
            textField.KeyEvent += TextFieldKeyEvent;
            textEditor.KeyEvent += TextEditorKeyEvent;
            Window.Instance.KeyEvent += AppBack;
        }

        private TextLabel CreatTextLabel(string text)
        {
            TextLabel _textLabel = new TextLabel();
            _textLabel.Text = text;
            //_textLabel.PointSize = 8.0f;
            _textLabel.PointSize = DeviceCheck.PointSize8;
            _textLabel.BackgroundColor = Color.White;
            _textLabel.VerticalAlignment = VerticalAlignment.Center;
            _textLabel.HorizontalAlignment = HorizontalAlignment.Center;
            _textLabel.Size2D = new Size2D(200, 80);
            _textLabel.ParentOrigin = ParentOrigin.TopLeft;
            _textLabel.PivotPoint = PivotPoint.TopLeft;
            PropertyMap fontStyle = new PropertyMap();
            fontStyle.Add("weight", new PropertyValue("bold"));
            _textLabel.FontStyle = fontStyle;
            return _textLabel;
        }

        /// <summary>
        /// Callback by textfield when have key be clicked and focus.
        /// Move focus from textfield.
        /// Up to textLabel, Down to textEditor.
        /// </summary>
        /// <param name="source">textfield</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool TextFieldKeyEvent(object source, View.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Down")
                {
                    FocusManager.Instance.SetCurrentFocusView(textEditor);
                }
                else if (e.Key.KeyPressedName == "Up")
                {
                    FocusManager.Instance.SetCurrentFocusView(textLabel);
                }
            }

            return false;
        }

        /// <summary>
        /// Callback by textEditor when have key be clicked and focus.
        /// Move focus from textEditor.
        /// Up to textfield, Down to pushButton[0].
        /// </summary>
        /// <param name="source">textEditor</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool TextEditorKeyEvent(object source, View.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Down")
                {
                    FocusManager.Instance.SetCurrentFocusView(pushButton[0]);
                }
                else if (e.Key.KeyPressedName == "Up")
                {
                    FocusManager.Instance.SetCurrentFocusView(textField);
                }
            }

            return false;
        }

        /// <summary>
        /// Create textLabel.
        /// TextLabel : A control which renders a short text string.
        /// </summary>
        private void CreateTextLabel()
        {
            textLabel = new TextLabel("A control which renders a simple text string. The text could be single line or multiline. You can decide that!");

            // Size of textLabel is 800 * 150;
            textLabel.SizeWidth = 1400;
            textLabel.SizeHeight = 150;
            // Set the position of textLabel.
            textLabel.PositionUsesPivotPoint = true;
            textLabel.PivotPoint = PivotPoint.TopLeft;
            textLabel.ParentOrigin = ParentOrigin.TopLeft;
            textLabel.Position = new Position(370, 180, 0);
            // The text size is 8.0f;
            //textLabel.PointSize = 8.0f;
            textLabel.PointSize = DeviceCheck.PointSize8;
            textLabel.BackgroundColor = Color.White;
            textLabel.Focusable = true;
            textLabel.FontFamily = "SamsungOneUI_200";

            Window.Instance.GetDefaultLayer().Add(textLabel);
        }

        /// <summary>
        /// Creates a textfield.
        /// TextField : A control which provides a single-line editable text field.
        /// </summary>
        private void CreateTextField()
        {
            textField = new TextField();
            // Size of textField is 800 * 150;
            textField.SizeWidth = 1400;
            textField.SizeHeight = 150;
            // Set the position of textField.
            textField.PositionUsesPivotPoint = true;
            textField.PivotPoint = PivotPoint.TopLeft;
            textField.ParentOrigin = ParentOrigin.TopLeft;
            textField.Position = new Position(370, 350, 0);
            // The text size is 8.0f;
            //textField.PointSize = 8.0f;
            textField.PointSize = DeviceCheck.PointSize8;
            textField.Text = "A control which provides a single-line editable text field.";
            textField.BackgroundColor = Color.White;
            textField.Focusable = true;
            // The kind of text is "SamsungOneUI_400"
            textField.FontFamily = "SamsungOneUI_400";
            // The max text length can show in textfield.
            textField.MaxLength = 20;
            // The Primary Cursor Color is blue.
            // The Secondary Cursor Color is Green;
            // The Selection Highlight Color is Cyan;
            textField.PrimaryCursorColor = Color.Blue;
            textField.SecondaryCursorColor = Color.Green;
            textField.SelectionHighlightColor = Color.Cyan;
            textField.EnableCursorBlink = true;

            Window.Instance.GetDefaultLayer().Add(textField);
        }

        /// <summary>
        /// Creates a textEditor.
        /// TextEditor : A control which provides a multi-line editable text editor.
        /// </summary>
        void CreateTextEditor()
        {
            textEditor = new TextEditor();
            // The size of textEditor is 800 * 260.
            textEditor.SizeWidth = 1400;
            textEditor.SizeHeight = 240;
            // Set the position of textEditor.
            textEditor.PositionUsesPivotPoint = true;
            textEditor.PivotPoint = PivotPoint.TopLeft;
            textEditor.ParentOrigin = ParentOrigin.TopLeft;
            textEditor.Position = new Position(370, 525, 0);
            // The text size is 8.
            //textEditor.PointSize = 8;
            textEditor.PointSize = DeviceCheck.PointSize8;
            textEditor.Text = "A control which provides a multi-line editable text editor.";
            textEditor.BackgroundColor = Color.White;
            textEditor.Focusable = true;

            // Set the kind of text is "SamsungOneUI_200"
            textEditor.FontFamily = "SamsungOneUI_600";

            Window.Instance.GetDefaultLayer().Add(textEditor);
        }

        /// <summary>
        /// Create buttons which control properties of textfield/textLabel/textEditor
        /// </summary>
        private void CreateButtons()
        {
            // Create tableView used to put pushButton.
            tableView = new TableView(3, 4);
            // Set the position of tableView.
            tableView.PositionUsesPivotPoint = true;
            tableView.PivotPoint = PivotPoint.TopLeft;
            tableView.ParentOrigin = ParentOrigin.TopLeft;
            tableView.Position = new Position(85, 800, 0);
            // The size of tableView is 800 * 300;
            tableView.Size2D = new Size2D(1800, 300);
            //tableView.BackgroundColor = Color.Cyan;
            Window.Instance.GetDefaultLayer().Add(tableView);

            pushButton = new Button[6];
            // Creates button[0] which control HorizontalAlignment
            pushButton[0] = CreateButton("HorizontalAlignment", "HorizontalAlignment");
            pushButton[0].SizeWidth = 400;
            // Bind pushButton's click event to ButtonClick.
            pushButton[0].ClickEvent += ButtonClick;
            tableView.AddChild(pushButton[0], new TableView.CellPosition(0, 0));

            // Creates button[1] which control VerticalAlignment
            pushButton[1] = CreateButton("VerticalAlignment", "VerticalAlignment");
            pushButton[1].SizeWidth = 400;
            // Bind pushButton's click event to ButtonClick.
            pushButton[1].ClickEvent += ButtonClick;
            tableView.AddChild(pushButton[1], new TableView.CellPosition(0, 1));

            // Creates button[2] which control Color
            pushButton[2] = CreateButton("Color", "Color");
            pushButton[2].SizeWidth = 400;
            // Bind pushButton's click event to ButtonClick.
            pushButton[2].ClickEvent += ButtonClick;
            tableView.AddChild(pushButton[2], new TableView.CellPosition(0, 2));

            // Creates button[3] which control pointSize
            pushButton[3] = CreateButton("Size", "Size");
            pushButton[3].SizeWidth = 400;
            // Bind pushButton's click event to ButtonClick.
            pushButton[3].ClickEvent += ButtonClick;
            tableView.AddChild(pushButton[3], new TableView.CellPosition(0, 3));

            // Creates button[5] which control scroll text
            pushButton[5] = CreateButton("Scroll", "Scroll");
            pushButton[5].SizeWidth = 400;
            // Bind pushButton's click event to ButtonClick.
            pushButton[5].ClickEvent += ButtonClick;
            tableView.AddChild(pushButton[5], new TableView.CellPosition(1, 0));

            // Creates button[6] which control language
            pushButton[4] = CreateButton("Language", "Language");
            pushButton[4].SizeWidth = 400;
            // Bind pushButton's click event to ButtonClick.
            pushButton[4].ClickEvent += ButtonClick;
            tableView.AddChild(pushButton[4], new TableView.CellPosition(1, 1));

            checkBoxButton = new CheckBox[6];
            // Creates checkBoxButton[0] which control multiline
            checkBoxButton[0] = CreateCheckBoxButton("Multiline");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[0].ClickEvent += CheckButtonClick;
            tableView.AddChild(checkBoxButton[0], new TableView.CellPosition(1, 2));

            // Creates checkBoxButton[1] which control shadow
            checkBoxButton[1] = CreateCheckBoxButton("Shadow");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[1].ClickEvent += CheckButtonClick;
            tableView.AddChild(checkBoxButton[1], new TableView.CellPosition(1, 3));

            // Creates checkBoxButton[2] which control underline
            checkBoxButton[2] = CreateCheckBoxButton("Underline");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[2].ClickEvent += CheckButtonClick;
            tableView.AddChild(checkBoxButton[2], new TableView.CellPosition(2, 0));

            // Creates checkBoxButton[3] which control ellipsis
            checkBoxButton[3] = CreateCheckBoxButton("Ellipsis");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[3].ClickEvent += CheckButtonClick;
            tableView.AddChild(checkBoxButton[3], new TableView.CellPosition(2, 1));

            // Creates checkBoxButton[4] which control bold
            checkBoxButton[4] = CreateCheckBoxButton("Bold");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[4].ClickEvent += CheckButtonClick;
            tableView.AddChild(checkBoxButton[4], new TableView.CellPosition(2, 2));

            // Creates checkBoxButton[5] which control condensed
            checkBoxButton[5] = CreateCheckBoxButton("Condensed");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[5].ClickEvent += CheckButtonClick;
            tableView.AddChild(checkBoxButton[5], new TableView.CellPosition(2, 3));

        }

        /// <summary>
        /// Called by buttons
        /// </summary>
        /// <param name="source">The clicked button</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private void ButtonClick(object source, EventArgs e)
        {
            // Get the source who trigger this event.
            Button button = source as Button;
            // Change textLabel/textfield/textEditor's HorizontalAlignment.
            if (button.Text == "HorizontalAlignment")
            {
                // Begin : Texts place at the begin of horizontal direction.
                if (textLabel.HorizontalAlignment == HorizontalAlignment.Begin)
                {
                    textLabel.HorizontalAlignment = HorizontalAlignment.Center;
                    textField.HorizontalAlignment = HorizontalAlignment.Center;
                    textEditor.HorizontalAlignment = HorizontalAlignment.Center;
                }
                // Center : Texts place at the center of horizontal direction.
                else if (textLabel.HorizontalAlignment == HorizontalAlignment.Center)
                {
                    textLabel.HorizontalAlignment = HorizontalAlignment.End;
                    textField.HorizontalAlignment = HorizontalAlignment.End;
                    textEditor.HorizontalAlignment = HorizontalAlignment.End;
                }
                // End : Texts place at the end of horizontal direction.
                else
                {
                    textLabel.HorizontalAlignment = HorizontalAlignment.Begin;
                    textField.HorizontalAlignment = HorizontalAlignment.Begin;
                    textEditor.HorizontalAlignment = HorizontalAlignment.Begin;
                }
            }
            // Change textLabel/textfield's VerticalAlignment.
            // TextEditor don't have this property.
            else if (button.Text == "VerticalAlignment")
            {
                // Top : Texts place at the top of vertical direction.
                if (textLabel.VerticalAlignment == VerticalAlignment.Top)
                {
                    textLabel.VerticalAlignment = VerticalAlignment.Center;
                    textField.VerticalAlignment = VerticalAlignment.Center;
                }
                // Center : Texts place at the center of vertical direction.
                else if (textLabel.VerticalAlignment == VerticalAlignment.Center)
                {
                    textLabel.VerticalAlignment = VerticalAlignment.Bottom;
                    textField.VerticalAlignment = VerticalAlignment.Bottom;
                }
                // Bottom : Texts place at the bottom of vertical direction.
                else
                {
                    textLabel.VerticalAlignment = VerticalAlignment.Top;
                    textField.VerticalAlignment = VerticalAlignment.Top;
                }
            }
            // Change textLabel/textfield/textEditor's text color.
            else if (button.Text == "Color")
            {
                // Judge the textColor is Black or not.
                // It true, change text color to blue.
                // It not, change text color to black.
                if (textLabel.TextColor.A == Color.Black.A && textLabel.TextColor.B == Color.Black.B &&
                    textLabel.TextColor.G == Color.Black.G && textLabel.TextColor.R == Color.Black.R)
                {
                    textLabel.TextColor = Color.Blue;
                    textField.TextColor = Color.Blue;
                    textEditor.TextColor = Color.Blue;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
                else
                {
                    textLabel.TextColor = Color.Black;
                    textField.TextColor = Color.Black;
                    textEditor.TextColor = Color.Black;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
            }
            // Change textLabel/textfield/textEditor's text size.
            else if (button.Text == "Size")
            {
                if (textLabel.PointSize == DeviceCheck.PointSize12)
                {
                    textLabel.PointSize = DeviceCheck.PointSize8;
                    textField.PointSize = DeviceCheck.PointSize8;
                    textEditor.PointSize = DeviceCheck.PointSize8;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
                else
                {
                    textLabel.PointSize = DeviceCheck.PointSize12;
                    textField.PointSize = DeviceCheck.PointSize12;
                    textEditor.PointSize = DeviceCheck.PointSize12;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }

                //if (textLabel.PointSize == 12.0f)
                //{
                //    textLabel.PointSize = 8.0f;
                //    textField.PointSize = 8.0f;
                //    textEditor.PointSize = 8.0f;
                //    textField.Text = textField.Text;
                //    textEditor.Text = textEditor.Text;
                //}
                //else
                //{
                //    textLabel.PointSize = 12.0f;
                //    textField.PointSize = 12.0f;
                //    textEditor.PointSize = 12.0f;
                //    textField.Text = textField.Text;
                //    textEditor.Text = textEditor.Text;
                //}
            }

            // Make textLabel's text auto scroll.
            // Starts auto scrolling.
            // Gap before scrolling wraps is 500.
            // The speed of scrolling in pixels per second is 500.
            // Number of complete loops when scrolling enabled is 1.
            else if (button.Text == "Scroll")
            {
                textLabel.VerticalAlignment = VerticalAlignment.Top;
                textLabel.EnableAutoScroll = true;
                textLabel.AutoScrollGap = 500;
                textLabel.AutoScrollSpeed = 500;
                textLabel.AutoScrollLoopCount = 1;
                textLabel.AutoScrollStopMode = AutoScrollStopMode.FinishLoop;
            }
            // Change different language on textLabel/textField/textEditor.
            else if (button.Text == "Language")
            {
                textLabel.Text = LANGUAGES[itemLanguage];
                textField.Text = LANGUAGES[itemLanguage];
                textEditor.Text = LANGUAGES[itemLanguage];
                itemLanguage++;
                // If the index of LANGUAGES in end, move index to 0.
                if (itemLanguage == NumLanguage)
                {
                    itemLanguage = 0;
                }
            }
        }

        /// <summary>
        /// Called by checkBoxButton<br>
        /// </summary>
        /// <param name="source">The clicked checkBoxButton</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private void CheckButtonClick(object source, EventArgs e)
        {
            // Get the source who trigger this event.
            CheckBox button = source as CheckBox;

            // Set the text on textLabel show in single-line or multi-line layout option.
            // If multiline is true , the text length more than the textLabel's length and
            // textLabel's high can show multiline, text will shown in multiline.
            // If not, The more text over textLabel length, will show "...".
            if (button.Text == "Multiline")
            {
                textLabel.Text = "TextLabel : A control which renders a text string, it could be single line or multiline. You can decide that!";
                if (button.IsSelected)
                {
                    textLabel.MultiLine = true;
                }
                else
                {
                    textLabel.MultiLine = false;
                }
            }
            // Set the text on textLabel/textfield have shadow or not.
            else if (button.Text == "Shadow")
            {
                textField.Text = textField.Text;
            }
            // Set the text on textLabel have Underline or not.
            else if (button.Text == "Underline")
            {
                if (button.IsSelected)
                {
                    // Show the underline.
                    PropertyMap underlineMap = new PropertyMap();
                    underlineMap.Insert("enable", new PropertyValue("true"));
                    underlineMap.Insert("color", new PropertyValue("black"));
                    underlineMap.Insert("height", new PropertyValue("3"));

                    // Set the underline property
                    textLabel.Underline = underlineMap;
                    textField.Underline = underlineMap;
                    textEditor.Underline = underlineMap;
                    textLabel.Text = textLabel.Text;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
                else
                {
                    // Hide the underline.
                    PropertyMap underlineMap = new PropertyMap();
                    underlineMap.Insert("enable", new PropertyValue("false"));

                    // Check the underline property
                    textLabel.Underline = underlineMap;
                    textField.Underline = underlineMap;
                    textEditor.Underline = underlineMap;
                    textLabel.Text = textLabel.Text;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
            }
            // Set textLabel is enable or disable the ellipsis.
            else if (button.Text == "Ellipsis")
            {
                if (button.IsSelected)
                {
                    textLabel.Ellipsis = true;
                    textLabel.Text = textLabel.Text;
                }
                else
                {
                    textLabel.Ellipsis = false;
                    textLabel.Text = textLabel.Text;
                }
            }
            // Set textLabel/textfield/textEditor text is bold or not.
            else if (button.Text == "Bold")
            {
                if (button.IsSelected)
                {
                    // The weight of text is bold.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("weight", new PropertyValue("bold"));
                    textLabel.FontStyle = fontStyle;
                    textField.FontStyle = fontStyle;
                    textEditor.FontStyle = fontStyle;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
                else
                {
                    // The weight of text is normal.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("weight", new PropertyValue("normal"));
                    textLabel.FontStyle = fontStyle;
                    textField.FontStyle = fontStyle;
                    textEditor.FontStyle = fontStyle;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
            }
            // Set textLabel/textField/textEditor text is condensed or not.
            else if (button.Text == "Condensed")
            {
                if (button.IsSelected)
                {
                    // The width of text is condensed.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("width", new PropertyValue("condensed"));
                    textLabel.FontStyle = fontStyle;
                    textField.FontStyle = fontStyle;
                    textEditor.FontStyle = fontStyle;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
                else
                {
                    // The width of text is normal.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("width", new PropertyValue("normal"));
                    textLabel.FontStyle = fontStyle;
                    textField.FontStyle = fontStyle;
                    textEditor.FontStyle = fontStyle;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
            }
            // Set textLabel/textField/textEditor text is italic or not.
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
            //textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(7));
            textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(DeviceCheck.PointSize7));
            // Texts place at the center of horizontal direction.
            textVisual.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
            // Texts place at the center of vertical direction.
            textVisual.Add(TextVisualProperty.VerticalAlignment, new PropertyValue("CENTER"));
            return textVisual;
        }

        private CheckBox CreateCheckBoxButton(string text)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Text = text;
            checkBox.TextColor = Color.White;
            checkBox.TextPadding = new Extents(20, 12, 0, 0);
            checkBox.PointSize = DeviceCheck.PointSize7;
            checkBox.Size2D = new Size2D(300, 48);
            checkBox.Focusable = true;
            checkBox.ParentOrigin = ParentOrigin.TopLeft;
            checkBox.PivotPoint = PivotPoint.TopLeft;
            checkBox.CheckImageURLSelector = new StringSelector()
            {
                Normal = checkBoxNormalImagePath,
                Selected = checkBoxSelectImagePath,
                Focused = checkBoxFocusImagePath,
                SelectedFocused = checkBoxFocusSelectImagePath
            };

            return checkBox;
        }
        /// <summary>
        /// Create an Image visual.
        /// </summary>
        /// <param name="imagePath">The url of the image</param>
        /// <returns>return a map which contain the properties of the image</returns>
        private PropertyMap CreateImageVisual(string imagePath)
        {
            PropertyMap map = new PropertyMap();
            map.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Image));
            map.Add(ImageVisualProperty.URL, new PropertyValue(imagePath));
            return map;
        }

        private Button CreateButton(string name, string text)
        {
            Button button = new Button();
            button.Focusable = true;
            button.Size2D = new Size2D(400, 80);
            button.Focusable = true;
            button.Name = name;
            button.Text = text;
            button.PointSize = DeviceCheck.PointSize8;
            button.BackgroundImage = normalImagePath;
            button.TextColor = Color.White;

            // Chang background Visual and Label when focus gained.
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