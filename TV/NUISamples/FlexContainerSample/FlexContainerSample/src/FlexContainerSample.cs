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
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace FlexContainerSample
{
    /// <summary>
    /// This example demonstrates a proof of concept for FlexContainer UI control.
    /// The flexbox properties can be changed by pressing different buttons in the
    /// toolbar.
    /// </summary>
    class FlexContainerSample : NUIApplication
    {
        private View flexContainer;
        private const int NUM_FLEX_ITEMS = 8;
        private TableView tableView;
        private TextLabel guide;
        private FlexLayout flexlayout;
        private static string resource = "/home/owner/apps_rw/org.tizen.example.FlexContainerSample/res";
        private string normalImagePath = resource + "/images/Button/btn_bg_25_25_25_95.9.png";
        private string focusImagePath = resource + "/images/Button/btn_bg_255_255_255_200.9.png";
        private string pressImagePath = resource + "/images/Button/btn_bg_0_129_198_100.9.png";

        /// <summary>
        /// The constructor with null
        /// </summary>
        public FlexContainerSample() : base()
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
        /// Flex Container Sample Application initialisation.
        /// </summary>
        public void Initialize()
        {
            Window.Instance.BackgroundColor = Color.Black;
            View focusIndicator = new View();
            FocusManager.Instance.FocusIndicator = focusIndicator;

            //Create a title, and add it to window
            guide = new TextLabel();
            guide.HorizontalAlignment = HorizontalAlignment.Center;
            guide.VerticalAlignment = VerticalAlignment.Center;
            guide.PositionUsesPivotPoint = true;
            guide.ParentOrigin = ParentOrigin.TopLeft;
            guide.PivotPoint = PivotPoint.TopLeft;
            guide.Size = new Size(1920, 96);
            guide.FontFamily = "Samsung One 600";
            guide.Position = new Position(0, 94);
            guide.MultiLine = false;
            //guide.PointSize = 15.0f;
            guide.PointSize = DeviceCheck.PointSize15;
            guide.Text = "FlexContainer Sample \n";
            guide.TextColor = Color.White;
            //guide.BackgroundColor = new Color(43.0f / 255.0f, 145.0f / 255.0f, 175.0f / 255.0f, 1.0f);
            Window.Instance.GetDefaultLayer().Add(guide);

            // Create FlexContainer
            flexContainer = new View()
            {
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.TopLeft,
                ParentOrigin = ParentOrigin.TopLeft,
                Position = new Position(710, 275, 0),
                Size = new Size(400, 400),
                BackgroundColor = Color.White,
                LayoutDirection = ViewLayoutDirectionType.LTR,
            };
            flexContainer.Layout = new FlexLayout()
            { 
                Direction = FlexLayout.FlexDirection.Column,
                Justification = FlexLayout.FlexJustification.Center,
            };
            flexlayout = flexContainer.Layout as FlexLayout;

            Window.Instance.GetDefaultLayer().Add(flexContainer);

            // Create flex items and add them to the container
            for (int i = 0; i < NUM_FLEX_ITEMS; i++)
            {
                TextLabel flexItem = new TextLabel();
                flexItem.PositionUsesPivotPoint = true;
                flexItem.PivotPoint = PivotPoint.TopLeft;
                flexItem.ParentOrigin = ParentOrigin.TopLeft;
                // Set different background colour to help to identify different items
                flexItem.BackgroundColor = new Vector4(((float)i) / NUM_FLEX_ITEMS, ((float)(NUM_FLEX_ITEMS - i)) / NUM_FLEX_ITEMS, 1.0f, 1.0f);
                //flexItem.Text = i + "";
                flexItem.Name = i + "";
                flexItem.HorizontalAlignment = HorizontalAlignment.Center;
                flexItem.VerticalAlignment = VerticalAlignment.Center;
                // Set a fixed size to the items so that we can wrap the line and test these
                // flex properties that only work when there are multiple lines in the layout
                flexItem.WidthResizePolicy = ResizePolicyType.Fixed;
                flexItem.HeightResizePolicy = ResizePolicyType.Fixed;
                // Make sure there are still extra space in the line after wrapping
                flexItem.SizeWidth = flexContainer.SizeWidth / NUM_FLEX_ITEMS * 1.25f;
                flexItem.SizeHeight = flexContainer.SizeHeight / NUM_FLEX_ITEMS * 1.25f;
                flexContainer.Add(flexItem);
            }

            // Create the tableView which is the parent of the Pushbuttons
            // which can change the flexcontainer's properties
            tableView = new TableView(2, 3);
            tableView.Position = new Position(80, 880);
            tableView.SizeWidth = 1800;
            tableView.SizeHeight = 180;
            tableView.PivotPoint = PivotPoint.TopLeft;
            tableView.ParentOrigin = ParentOrigin.TopLeft;
            //tableView.CellPadding = new Vector2(15, 10);
            Window.Instance.GetDefaultLayer().Add(tableView);

            string[] btnName = { "ContentDirection", "FlexDirection", "FlexWrap", "JustifyContent", "AlignItems", "AlignContent" };
            string[] btnLabel = { "ContentDirection : Inherit", "FlexDirection : Column", "FlexWrap : NoWrap", "JustifyContent : JustifyFlexStart", "AlignItems : AlignStretch", "AlignContent : AlignFlexStart" };

            for (uint row = 0; row < 2; row++)
            {
                for (uint column = 0; column < 3; column++)
                {
                    Button button = CreateButton(btnName[row * 3 + column], btnLabel[row * 3 + column]);
                    button.SizeWidth = 560;
                    button.SizeHeight = 80;
                    button.Focusable = true;
                    button.Clicked += ButtonClick;
                    tableView.AddChild(button, new TableView.CellPosition(row, column));
                    if (0 == row && column == 0)
                    {
                        FocusManager.Instance.SetCurrentFocusView(button);
                    }

                    buttonArray[row * 3 + column] = button;

                }

            }

            buttonArray[5].Opacity = 0.0f;
            Window.Instance.KeyEvent += AppBack;
        }

        private Button[] buttonArray = new Button[6];

        /// <summary>
        /// The event will be triggered when pushbutton clicked
        /// </summary>
        /// <param name="source">The clicked PushButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private void ButtonClick(object source, EventArgs e)
        {
            Button button = source as Button;

            // Judge the button can change flexContainer's property of ContentDirection.
            // If true, change the value of ContentDirection circularly.
            // At the same time, changed the pushbutton's LabelText to the current ContentDirection's value
            // The property change the primary direction in which content is ordered
            //
            // LTR : From left to right
            // RTL : From right to left
            // Inherit : Inherits the same direction from the parent
            if (button.Name == "ContentDirection")
            {
                if (flexContainer.LayoutDirection == ViewLayoutDirectionType.LTR)
                {
                    flexContainer.LayoutDirection = ViewLayoutDirectionType.RTL;
                    button.Text = "ContentDirection : RTL";
                }
                else if (flexContainer.LayoutDirection == ViewLayoutDirectionType.RTL)
                {
                    flexContainer.LayoutDirection = ViewLayoutDirectionType.LTR;
                    button.Text = "ContentDirection : LTR";
                }
            }

            // Judge the button can change flexContainer's property of FlexDirection.
            // If true, change the value of FlexDirection circularly.
            // At the same time, changed the pushbutton's LabelText to the current FlexDirection's value
            // The property change the direction of the main-axis which determines the direction that flex items are laid out
            //
            // ColumnReverse : The flexible items are displayed vertically as a column, but in reverse order
            // Row : The flexible items are displayed horizontally as a row
            // RowReverse : The flexible items are displayed horizontally as a row, but in reverse order
            // Column : The flexible items are displayed vertically as a column
            else if (button.Name == "FlexDirection")
            {
                if (flexlayout.Direction == FlexLayout.FlexDirection.Column)
                {
                    flexlayout.Direction = FlexLayout.FlexDirection.ColumnReverse;
                    button.Text = "FlexDirection : ColumnReverse";
                }
                else if (flexlayout.Direction == FlexLayout.FlexDirection.ColumnReverse)
                {
                    flexlayout.Direction = FlexLayout.FlexDirection.Row;
                    button.Text = "FlexDirection : Row";
                }
                else if (flexlayout.Direction == FlexLayout.FlexDirection.Row)
                {
                    flexlayout.Direction = FlexLayout.FlexDirection.RowReverse;
                    button.Text = "FlexDirection : RowReverse";
                }
                else
                {
                    flexlayout.Direction = FlexLayout.FlexDirection.Column;
                    button.Text = "FlexDirection : Column";
                }
            }

            // Judge the button can change flexContainer's property of FlexWrap.
            // If true, change the value of FlexWrap circularly.
            // At the same time, changed the pushbutton's LabelText to the current FlexWrap's value
            // The property show whether the flex items should wrap or not if there is no enough room for them on one flex line
            // Wrap : Flex items laid out in single line (shrunk to fit the flex container along the main axis)
            // NoWrap : Flex items laid out in multiple lines if needed
            else if (button.Name == "FlexWrap")
            {
                if (flexlayout.WrapType == FlexLayout.FlexWrapType.NoWrap)
                {
                    flexlayout.WrapType = FlexLayout.FlexWrapType.Wrap;
                    button.Text = "FlexWrap : Wrap";
                    buttonArray[5].Opacity = 1.0f;
                }
                else
                {
                    flexlayout.WrapType = FlexLayout.FlexWrapType.NoWrap;
                    button.Text = "FlexWrap : NoWrap";
                    buttonArray[5].Opacity = 0.0f;
                }
            }

            // Judge the button can change flexContainer's property of JustifyContent.
            // If true, change the value of JustifyContent circularly.
            // At the same time, changed the pushbutton's LabelText to the current JustifyContent's value
            // The property show whether the flex items should wrap or not if there is no enough room for them on one flex line
            //
            // JustifyFlexEnd : FItems are positioned at the end of the container
            // JustifyFlexStart : Items are positioned at the beginning of the container
            // JustifySpaceAround : Items are positioned with equal space before, between, and after the lines
            // JustifySpaceBetween : Items are positioned with equal space between the lines
            // JustifyCenter : Items are positioned at the center of the container
            else if (button.Name == "JustifyContent")
            {
                if (flexlayout.Justification == FlexLayout.FlexJustification.Center)
                {
                    flexlayout.Justification = FlexLayout.FlexJustification.FlexEnd;
                    button.Text = "JustifyContent : JustifyFlexEnd";
                }
                else if (flexlayout.Justification == FlexLayout.FlexJustification.FlexEnd)
                {
                    flexlayout.Justification = FlexLayout.FlexJustification.FlexStart;
                    button.Text = "JustifyContent : JustifyFlexStart";
                }
                else if (flexlayout.Justification == FlexLayout.FlexJustification.FlexStart)
                {
                    flexlayout.Justification = FlexLayout.FlexJustification.SpaceAround;
                    button.Text = "JustifyContent : JustifySpaceAround";
                }
                else if (flexlayout.Justification == FlexLayout.FlexJustification.SpaceAround)
                {
                    flexlayout.Justification = FlexLayout.FlexJustification.SpaceBetween;
                    button.Text = "JustifyContent : JustifySpaceBetween";
                }
                else
                {
                    flexlayout.Justification = FlexLayout.FlexJustification.Center;
                    button.Text = "JustifyContent : JustifyCenter";
                }
            }

            // Judge the button can change flexContainer's property of AlignItems.
            // If true, change the value of AlignItems circularly.
            // At the same time, changed the pushbutton's LabelText to the current AlignItems's value
            // The property show the alignment of flex items when the items do not use all available space on the cross-axis
            //
            // AlignCenter :  At the center of the container
            // AlignFlexEnd : At the end of the container
            // AlignFlexStart : At the beginning of the container
            // AlignStretch : Stretch to fit the container
            // AlignAuto : Inherits the same alignment from the parent
            else if (button.Name == "AlignItems")
            {
                if (flexlayout.ItemsAlignment == FlexLayout.AlignmentType.Auto)
                {
                    flexlayout.ItemsAlignment = FlexLayout.AlignmentType.Center;
                    button.Text = "AlignItems : AlignCenter";
                }
                else if (flexlayout.ItemsAlignment == FlexLayout.AlignmentType.Center)
                {
                    flexlayout.ItemsAlignment = FlexLayout.AlignmentType.FlexEnd;
                    button.Text = "AlignItems : AlignFlexEnd";
                }
                else if (flexlayout.ItemsAlignment == FlexLayout.AlignmentType.FlexEnd)
                {
                    flexlayout.ItemsAlignment = FlexLayout.AlignmentType.FlexStart;
                    button.Text = "AlignItems : AlignFlexStart";
                }
                else if (flexlayout.ItemsAlignment == FlexLayout.AlignmentType.FlexStart)
                {
                    flexlayout.ItemsAlignment = FlexLayout.AlignmentType.Stretch;
                    button.Text = "AlignItems : AlignStretch";
                }
                else
                {
                    flexlayout.ItemsAlignment = FlexLayout.AlignmentType.Auto;
                    button.Text = "AlignItems : AlignAuto";
                }
            }

            // Judge the button can change flexContainer's property of AlignItems.
            // If true, change the value of AlignItems circularly.
            // At the same time, changed the pushbutton's LabelText to the current AlignItems's value
            // The property is similar to "alignItems", but it aligns flex lines,
            // so only works when there are multiple lines
            else if (button.Name == "AlignContent")
            {
                if (flexlayout.Alignment == FlexLayout.AlignmentType.Auto)
                {
                    flexlayout.Alignment = FlexLayout.AlignmentType.Center;
                    button.Text = "AlignContent : AlignCenter";
                }
                else if (flexlayout.Alignment == FlexLayout.AlignmentType.Center)
                {
                    flexlayout.Alignment = FlexLayout.AlignmentType.FlexEnd;
                    button.Text = "AlignContent : AlignFlexEnd";
                }
                else if (flexlayout.Alignment == FlexLayout.AlignmentType.FlexEnd)
                {
                    flexlayout.Alignment = FlexLayout.AlignmentType.FlexStart;
                    button.Text = "AlignContent : AlignFlexStart";
                }
                else if (flexlayout.Alignment == FlexLayout.AlignmentType.FlexStart)
                {
                    flexlayout.Alignment = FlexLayout.AlignmentType.Stretch;
                    button.Text = "AlignContent : AlignStretch";
                }
                else
                {
                    flexlayout.Alignment = FlexLayout.AlignmentType.Auto;
                    button.Text = "AlignContent : AlignAuto";
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
            button.Text = text;
            button.TextColor = Color.White;
            button.BackgroundImage = normalImagePath;

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
        /// Creates propertyMap used t set button.Label
        /// </summary>
        /// <param name="text">text</param>
        ///<returns>The created propertyMap</returns>
        private PropertyMap CreateText(string text)
        {
            PropertyMap textVisual = new PropertyMap();
            textVisual.Add(Visual.Property.Type, new PropertyValue((int)Visual.Type.Text));
            textVisual.Add(TextVisualProperty.Text, new PropertyValue(text));
            textVisual.Add(TextVisualProperty.TextColor, new PropertyValue(Color.Black));
            //textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(7));
            textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(DeviceCheck.PointSize7));
            textVisual.Add(TextVisualProperty.HorizontalAlignment, new PropertyValue("CENTER"));
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