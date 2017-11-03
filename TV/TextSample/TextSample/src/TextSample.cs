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
        private PushButton[] pushButton;
        // checkBoxButton be used to trigger the effect of Text.
        private CheckBoxButton[] checkBoxButton;
        // tableView be used to put pushButton and checkBoxButton.
        private TableView tableView;
        // Some kinds of LANGUAGES.
        private string[] LANGUAGES =
        {
            "العَرَبِيةُ(Arabic)", "অসমীয়া লিপি(Assamese)", "Español(Spanish)", "한국어(Korean)", "漢語(Chinese)",
            "TextLabel : A control which renders a short text string.(English)"
        };
        // The index of LANGUAGES.
        private int itemLanguage = 0;
        // The count of languages.
        private int NumLanguage = 6;

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
            Window.Instance.BackgroundColor = new Color(0.8f, 0.8f, 0.8f, 1.0f);
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
            textLabel = new TextLabel("TextLabel : A control which renders a short text string.");

            // Size of textLabel is 800 * 150;
            textLabel.SizeWidth = 800;
            textLabel.SizeHeight = 150;
            // Set the position of textLabel.
            textLabel.PositionUsesPivotPoint = true;
            textLabel.PivotPoint = PivotPoint.TopCenter;
            textLabel.ParentOrigin = ParentOrigin.TopCenter;
            textLabel.Position = new Position(0, 80, 0);
            // The text size is 8.0f;
            textLabel.PointSize = 8.0f;
            textLabel.BackgroundColor = Color.White;
            textLabel.Focusable = true;
            // The kind of text is "SamsungOneUI_200"
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
            textField.SizeWidth = 800;
            textField.SizeHeight = 150;
            // Set the position of textField.
            textField.PositionUsesPivotPoint = true;
            textField.PivotPoint = PivotPoint.TopCenter;
            textField.ParentOrigin = ParentOrigin.TopCenter;
            textField.Position = new Position(0, 250, 0);
            // The text size is 8.0f;
            textField.PointSize = 8.0f;
            textField.Text = "TextField : A control which provides a single-line editable text field.";
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
            textEditor.SizeWidth = 800;
            textEditor.SizeHeight = 260;
            // Set the position of textEditor.
            textEditor.PositionUsesPivotPoint = true;
            textEditor.PivotPoint = PivotPoint.TopCenter;
            textEditor.ParentOrigin = ParentOrigin.TopCenter;
            textEditor.Position = new Position(0, 420, 0);
            // The text size is 8.
            textEditor.PointSize = 8;
            textEditor.Text = "TextEditor : A control which provides a multi-line editable text editor.";
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
            tableView = new TableView(4, 4);
            // Set the position of tableView.
            tableView.PositionUsesPivotPoint = true;
            tableView.PivotPoint = PivotPoint.TopCenter;
            tableView.ParentOrigin = ParentOrigin.TopCenter;
            tableView.Position = new Position(0, 700, 0);
            // The size of tableView is 800 * 300;
            tableView.Size2D = new Size2D(800, 300);
            tableView.BackgroundColor = Color.Cyan;
            Window.Instance.GetDefaultLayer().Add(tableView);

            pushButton = new PushButton[7];
            // Creates button[0] which control HorizontalAlignment
            pushButton[0] = new PushButton();
            pushButton[0].Label = CreateText("HorizontalAlignment");
            pushButton[0].SizeWidth = tableView.SizeWidth / 2;
            // Bind pushButton's click event to ButtonClick.
            pushButton[0].Clicked += ButtonClick;
            tableView.AddChild(pushButton[0], new TableView.CellPosition(0, 0));

            // Creates button[1] which control VerticalAlignment
            pushButton[1] = new PushButton();
            pushButton[1].Label = CreateText("VerticalAlignment");
            pushButton[1].SizeWidth = tableView.SizeWidth / 2;
            // Bind pushButton's click event to ButtonClick.
            pushButton[1].Clicked += ButtonClick;
            tableView.AddChild(pushButton[1], new TableView.CellPosition(0, 2));

            // Creates button[2] which control Color
            pushButton[2] = new PushButton();
            pushButton[2].Label = CreateText("Color");
            pushButton[2].SizeWidth = tableView.SizeWidth / 4;
            // Bind pushButton's click event to ButtonClick.
            pushButton[2].Clicked += ButtonClick;
            tableView.AddChild(pushButton[2], new TableView.CellPosition(1, 0));

            // Creates button[3] which control pointSize
            pushButton[3] = new PushButton();
            pushButton[3].Label = CreateText("Size");
            pushButton[3].SizeWidth = tableView.SizeWidth / 4;
            // Bind pushButton's click event to ButtonClick.
            pushButton[3].Clicked += ButtonClick;
            tableView.AddChild(pushButton[3], new TableView.CellPosition(1, 1));

            // Creates button[4] which control lineSpacing
            pushButton[4] = new PushButton();
            pushButton[4].Label = CreateText("LineSpacing");
            pushButton[4].SizeWidth = tableView.SizeWidth / 4;
            // Bind pushButton's click event to ButtonClick.
            pushButton[4].Clicked += ButtonClick;
            tableView.AddChild(pushButton[4], new TableView.CellPosition(1, 2));

            // Creates button[5] which control scroll text
            pushButton[5] = new PushButton();
            pushButton[5].Label = CreateText("Scroll");
            pushButton[5].SizeWidth = tableView.SizeWidth / 4;
            // Bind pushButton's click event to ButtonClick.
            pushButton[5].Clicked += ButtonClick;
            tableView.AddChild(pushButton[5], new TableView.CellPosition(1, 3));

            // Creates button[6] which control language
            pushButton[6] = new PushButton();
            pushButton[6].Label = CreateText("Language");
            pushButton[6].SizeWidth = tableView.SizeWidth / 4;
            // Bind pushButton's click event to ButtonClick.
            pushButton[6].Clicked += ButtonClick;
            tableView.AddChild(pushButton[6], new TableView.CellPosition(2, 0));

            checkBoxButton = new CheckBoxButton[8];
            // Creates checkBoxButton[0] which control multiline
            checkBoxButton[0] = new CheckBoxButton();
            checkBoxButton[0].Label = CreateText("Multiline");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[0].Clicked += CheckButtonClick;
            tableView.AddChild(checkBoxButton[0], new TableView.CellPosition(2, 1));

            // Creates checkBoxButton[1] which control shadow
            checkBoxButton[1] = new CheckBoxButton();
            checkBoxButton[1].Label = CreateText("Shadow");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[1].Clicked += CheckButtonClick;
            tableView.AddChild(checkBoxButton[1], new TableView.CellPosition(2, 2));

            // Creates checkBoxButton[2] which control underline
            checkBoxButton[2] = new CheckBoxButton();
            checkBoxButton[2].Label = CreateText("Underline");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[2].Clicked += CheckButtonClick;
            tableView.AddChild(checkBoxButton[2], new TableView.CellPosition(2, 3));

            // Creates checkBoxButton[3] which control ellipsis
            checkBoxButton[3] = new CheckBoxButton();
            checkBoxButton[3].Label = CreateText("Ellipsis");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[3].Clicked += CheckButtonClick;
            tableView.AddChild(checkBoxButton[3], new TableView.CellPosition(3, 0));

            // Creates checkBoxButton[4] which control bold
            checkBoxButton[4] = new CheckBoxButton();
            checkBoxButton[4].Label = CreateText("Bold");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[4].Clicked += CheckButtonClick;
            tableView.AddChild(checkBoxButton[4], new TableView.CellPosition(3, 1));

            // Creates checkBoxButton[5] which control condensed
            checkBoxButton[5] = new CheckBoxButton();
            checkBoxButton[5].Label = CreateText("Condensed");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[5].Clicked += CheckButtonClick;
            tableView.AddChild(checkBoxButton[5], new TableView.CellPosition(3, 2));

            // Creates checkBoxButton[6] which control italic
            checkBoxButton[6] = new CheckBoxButton();
            checkBoxButton[6].Label = CreateText("Italic");
            // Bind checkBoxButton's click event to CheckButtonClick.
            checkBoxButton[6].Clicked += CheckButtonClick;
            tableView.AddChild(checkBoxButton[6], new TableView.CellPosition(3, 3));
        }

        /// <summary>
        /// Called by buttons
        /// </summary>
        /// <param name="source">The clicked button</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool ButtonClick(object source, EventArgs e)
        {
            // Get the source who trigger this event.
            PushButton button = source as PushButton;
            // Change textLabel/textfield/textEditor's HorizontalAlignment.
            if (button.LabelText == "HorizontalAlignment")
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
            else if (button.LabelText == "VerticalAlignment")
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
            else if (button.LabelText == "Color")
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
            else if (button.LabelText == "Size")
            {
                if (textLabel.PointSize == 12.0f)
                {
                    textLabel.PointSize = 8.0f;
                    textField.PointSize = 8.0f;
                    textEditor.PointSize = 8.0f;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
                else
                {
                    textLabel.PointSize = 12.0f;
                    textField.PointSize = 12.0f;
                    textEditor.PointSize = 12.0f;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
            }
            // Change textLabel/textEditor's line spacing.
            // The default extra space between lines in points.
            else if (button.LabelText == "LineSpacing")
            {
                if (textLabel.LineSpacing == 1.0f)
                {
                    textLabel.LineSpacing = 5.0f;
                    textEditor.LineSpacing = 5.0f;
                }
                else
                {
                    textLabel.LineSpacing = 1.0f;
                    textEditor.LineSpacing = 1.0f;
                }
            }
            // Make textLabel's text auto scroll.
            // Starts auto scrolling.
            // Gap before scrolling wraps is 500.
            // The speed of scrolling in pixels per second is 500.
            // Number of complete loops when scrolling enabled is 1.
            else if (button.LabelText == "Scroll")
            {
                textLabel.VerticalAlignment = VerticalAlignment.Top;
                textLabel.EnableAutoScroll = true;
                textLabel.AutoScrollGap = 500;
                textLabel.AutoScrollSpeed = 500;
                textLabel.AutoScrollLoopCount = 1;
                textLabel.AutoScrollStopMode = AutoScrollStopMode.FinishLoop;
            }
            // Change different language on textLabel/textField/textEditor.
            else if (button.LabelText == "Language")
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

            return true;
        }

        /// <summary>
        /// Called by checkBoxButton<br>
        /// </summary>
        /// <param name="source">The clicked checkBoxButton</param>
        /// <param name="e">event</param>
        /// <returns>The consume flag</returns>
        private bool CheckButtonClick(object source, EventArgs e)
        {
            // Get the source who trigger this event.
            CheckBoxButton button = source as CheckBoxButton;

            // Set the text on textLabel show in single-line or multi-line layout option.
            // If multiline is true , the text length more than the textLabel's length and
            // textLabel's high can show multiline, text will shown in multiline.
            // If not, The more text over textLabel length, will show "...".
            if (button.LabelText == "Multiline")
            {
                if (button.Selected)
                {
                    textLabel.MultiLine = true;
                }
                else
                {
                    textLabel.MultiLine = false;
                }
            }
            // Set the text on textLabel/textfield have shadow or not.
            else if (button.LabelText == "Shadow")
            {
                if (button.Selected)
                {
                    textLabel.ShadowOffset = new Vector2(3.0f, 3.0f);
                    textLabel.ShadowColor = Color.Black;
                    textField.ShadowOffset = new Vector2(3.0f, 3.0f);
                    textField.ShadowColor = Color.Black;
                    textField.Text = textField.Text;
                }
                else
                {
                    // The drop shadow offset 0 indicates no shadow.
                    textLabel.ShadowOffset = new Vector2(0, 0);
                    textField.ShadowOffset = new Vector2(0, 0);
                    textField.Text = textField.Text;
                }
            }
            // Set the text on textLabel have Underline or not.
            else if (button.LabelText == "Underline")
            {
                if (button.Selected)
                {
                    // Show the underline.
                    textLabel.UnderlineEnabled = true;
                    // Set underline's color.
                    textLabel.UnderlineColor = Color.Black;
                    textLabel.UnderlineHeight = 3.0f;
                }
                else
                {
                    // Hide the underline.
                    textLabel.UnderlineEnabled = false;
                }
            }
            // Set textLabel is enable or disable the ellipsis.
            else if (button.LabelText == "Ellipsis")
            {
                if (button.Selected)
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
            else if (button.LabelText == "Bold")
            {
                if (button.Selected)
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
            else if (button.LabelText == "Condensed")
            {
                if (button.Selected)
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
            else if (button.LabelText == "Italic")
            {
                if (button.Selected)
                {
                    // The slant of text is italic.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("slant", new PropertyValue("italic"));
                    textLabel.FontStyle = fontStyle;
                    textField.FontStyle = fontStyle;
                    textEditor.FontStyle = fontStyle;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
                else
                {
                    // The slant of text is normal.
                    PropertyMap fontStyle = new PropertyMap();
                    fontStyle.Add("slant", new PropertyValue("normal"));
                    textLabel.FontStyle = fontStyle;
                    textField.FontStyle = fontStyle;
                    textEditor.FontStyle = fontStyle;
                    textField.Text = textField.Text;
                    textEditor.Text = textEditor.Text;
                }
            }

            return true;
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