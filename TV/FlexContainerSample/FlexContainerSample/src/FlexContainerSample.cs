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
using Tizen;
using System.Runtime.InteropServices;
using Tizen.NUI;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

namespace FlexContainerSample
{
    /// <summary>
    /// This example demonstrates a proof of concept for FlexContainer UI control.
    /// The flexbox properties can be changed by pressing different buttons in the
    /// toolbar.
    /// </summary>
    class FlexContainerSample : NUIApplication
    {
        private FlexContainer flexContainer;
        private const int NUM_FLEX_ITEMS = 8;
        private TableView tableView;

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
            Window.Instance.BackgroundColor = new Color(0.8f, 0.8f, 0.8f, 1.0f);

            // Create FlexContainer
            flexContainer = new FlexContainer();
            flexContainer.PositionUsesPivotPoint = true;
            flexContainer.PivotPoint = PivotPoint.TopLeft;
            flexContainer.ParentOrigin = ParentOrigin.TopLeft;
            flexContainer.Position = new Position(220, 160, 0);
            flexContainer.SizeWidth = 700;
            flexContainer.SizeHeight = 700;
            flexContainer.BackgroundColor = Color.Yellow;
            flexContainer.FlexDirection = FlexContainer.FlexDirectionType.Column;
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
                flexItem.Text = i + "";
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
            tableView = new TableView(6, 1);
            tableView.SizeWidth = 500;
            tableView.SizeHeight = 400;
            tableView.CellPadding = new Vector2(5, 5);
            tableView.PositionUsesPivotPoint = true;
            tableView.PivotPoint = PivotPoint.BottomRight;
            tableView.ParentOrigin = ParentOrigin.BottomRight;
            tableView.Position = new Position(-100,-100, 0);
            Window.Instance.GetDefaultLayer().Add(tableView);

            // Create the pushbutton which can change the property of ContentDirection.
            // The default value of flexcontainer's ContentDirection
            // is FlexContainer.ContentDirectionType.Inherit.
            PushButton button0 = new PushButton();
            button0.Name = "ContentDirection";
            button0.Label = CreateText("ContentDirection : Inherit");
            button0.SizeWidth = tableView.SizeWidth;
            button0.Focusable = true;
            button0.Clicked += ButtonClick;
            tableView.AddChild(button0, new TableView.CellPosition(0, 0));

            // Create the pushbutton which can change the property of FlexDirection.
            // The current value of flexcontainer's FlexDirection
            // is FlexContainer.ContentDirectionType.Column
            PushButton button1 = new PushButton();
            button1.Name = "FlexDirection";
            button1.Label = CreateText("FlexDirection : Column");
            button1.SizeWidth = tableView.SizeWidth;
            button1.Clicked += ButtonClick;
            tableView.AddChild(button1, new TableView.CellPosition(1, 0));

            // Create the pushbutton which can change the property of FlexWrap
            // The current value of flexcontainer's FlexWrap
            // is FlexContainer.WrapType.NoWrap
            PushButton button2 = new PushButton();
            button2.Name = "FlexWrap";
            button2.Label = CreateText("FlexWrap : NoWrap");
            button2.SizeWidth = tableView.SizeWidth;
            button2.Clicked += ButtonClick;
            tableView.AddChild(button2, new TableView.CellPosition(2, 0));

            // Create the pushbutton which can change the property of JustifyContent
            // The current value of flexcontainer's JustifyContent
            // is FlexContainer.Justification.JustifyFlexStart
            PushButton button3 = new PushButton();
            button3.Name = "JustifyContent";
            button3.Label = CreateText("JustifyContent : JustifyFlexStart");
            button3.SizeWidth = tableView.SizeWidth;
            button3.Clicked += ButtonClick;
            tableView.AddChild(button3, new TableView.CellPosition(3, 0));

            // Create the pushbutton which can change the property of AlignItems
            // The current value of flexcontainer's AlignItems
            // is FlexContainer.Alignment.AlignStretch
            PushButton button4 = new PushButton();
            button4.Name = "AlignItems";
            button4.Label = CreateText("AlignItems : AlignStretch");
            button4.SizeWidth = tableView.SizeWidth;
            button4.Clicked += ButtonClick;
            tableView.AddChild(button4, new TableView.CellPosition(4, 0));

            // Create the pushbutton which can change the property of AlignContent
            // The current value of flexcontainer's AlignContent
            // is FlexContainer.Alignment.AlignFlexStart
            PushButton button5 = new PushButton();
            button5.Name = "AlignContent";
            button5.Label = CreateText("AlignContent : AlignFlexStart");
            button5.SizeWidth = tableView.SizeWidth;
            button5.Clicked += ButtonClick;
            tableView.AddChild(button5, new TableView.CellPosition(5, 0));

            FocusManager.Instance.SetCurrentFocusView(button0);
            Window.Instance.KeyEvent += AppBack;
        }

        /// <summary>
        /// The event will be triggered when pushbutton clicked
        /// </summary>
        /// <param name="source">The clicked PushButton.</param>
        /// <param name="e">event</param>
        /// <returns>the consume flag</returns>
        private bool ButtonClick(object source, EventArgs e)
        {
            PushButton button = source as PushButton;

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
                if (flexContainer.ContentDirection == FlexContainer.ContentDirectionType.Inherit)
                {
                    flexContainer.ContentDirection = FlexContainer.ContentDirectionType.LTR;
                    button.Label = CreateText("ContentDirection : LTR");
                }
                else if (flexContainer.ContentDirection == FlexContainer.ContentDirectionType.LTR)
                {
                    flexContainer.ContentDirection = FlexContainer.ContentDirectionType.RTL;
                    button.Label = CreateText("ContentDirection : RTL");
                }
                else
                {
                    flexContainer.ContentDirection = FlexContainer.ContentDirectionType.Inherit;
                    button.Label = CreateText("ContentDirection : Inherit");
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
                if (flexContainer.FlexDirection == FlexContainer.FlexDirectionType.Column)
                {
                    flexContainer.FlexDirection = FlexContainer.FlexDirectionType.ColumnReverse;
                    button.Label = CreateText("FlexDirection : ColumnReverse");
                }
                else if (flexContainer.FlexDirection == FlexContainer.FlexDirectionType.ColumnReverse)
                {
                    flexContainer.FlexDirection = FlexContainer.FlexDirectionType.Row;
                    button.Label = CreateText("FlexDirection : Row");
                }
                else if (flexContainer.FlexDirection == FlexContainer.FlexDirectionType.Row)
                {
                    flexContainer.FlexDirection = FlexContainer.FlexDirectionType.RowReverse;
                    button.Label = CreateText("FlexDirection : RowReverse");
                }
                else
                {
                    flexContainer.FlexDirection = FlexContainer.FlexDirectionType.Column;
                    button.Label = CreateText("FlexDirection : Column");
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
                if (flexContainer.FlexWrap == FlexContainer.WrapType.NoWrap)
                {
                    flexContainer.FlexWrap = FlexContainer.WrapType.Wrap;
                    button.Label = CreateText("FlexWrap : Wrap");
                }
                else
                {
                    flexContainer.FlexWrap = FlexContainer.WrapType.NoWrap;
                    button.Label = CreateText("FlexWrap : NoWrap");
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
                if (flexContainer.JustifyContent == FlexContainer.Justification.JustifyCenter)
                {
                    flexContainer.JustifyContent = FlexContainer.Justification.JustifyFlexEnd;
                    button.Label = CreateText("JustifyContent : JustifyFlexEnd");
                }
                else if (flexContainer.JustifyContent == FlexContainer.Justification.JustifyFlexEnd)
                {
                    flexContainer.JustifyContent = FlexContainer.Justification.JustifyFlexStart;
                    button.Label = CreateText("JustifyContent : JustifyFlexStart");
                }
                else if (flexContainer.JustifyContent == FlexContainer.Justification.JustifyFlexStart)
                {
                    flexContainer.JustifyContent = FlexContainer.Justification.JustifySpaceAround;
                    button.Label = CreateText("JustifyContent : JustifySpaceAround");
                }
                else if (flexContainer.JustifyContent == FlexContainer.Justification.JustifySpaceAround)
                {
                    flexContainer.JustifyContent = FlexContainer.Justification.JustifySpaceBetween;
                    button.Label = CreateText("JustifyContent : JustifySpaceBetween");
                }
                else
                {
                    flexContainer.JustifyContent = FlexContainer.Justification.JustifyCenter;
                    button.Label = CreateText("JustifyContent : JustifyCenter");
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
                if (flexContainer.AlignItems == FlexContainer.Alignment.AlignAuto)
                {
                    flexContainer.AlignItems = FlexContainer.Alignment.AlignCenter;
                    button.Label = CreateText("AlignItems : AlignCenter");
                }
                else if (flexContainer.AlignItems == FlexContainer.Alignment.AlignCenter)
                {
                    flexContainer.AlignItems = FlexContainer.Alignment.AlignFlexEnd;
                    button.Label = CreateText("AlignItems : AlignFlexEnd");
                }
                else if (flexContainer.AlignItems == FlexContainer.Alignment.AlignFlexEnd)
                {
                    flexContainer.AlignItems = FlexContainer.Alignment.AlignFlexStart;
                    button.Label = CreateText("AlignItems : AlignFlexStart");
                }
                else if (flexContainer.AlignItems == FlexContainer.Alignment.AlignFlexStart)
                {
                    flexContainer.AlignItems = FlexContainer.Alignment.AlignStretch;
                    button.Label = CreateText("AlignItems : AlignStretch");
                }
                else
                {
                    flexContainer.AlignItems = FlexContainer.Alignment.AlignAuto;
                    button.Label = CreateText("AlignItems : AlignAuto");
                }
            }

            // Judge the button can change flexContainer's property of AlignItems.
            // If true, change the value of AlignItems circularly.
            // At the same time, changed the pushbutton's LabelText to the current AlignItems's value
            // The property is similar to "alignItems", but it aligns flex lines,
            // so only works when there are multiple lines
            else if (button.Name == "AlignContent")
            {
                if (flexContainer.AlignContent == FlexContainer.Alignment.AlignAuto)
                {
                    flexContainer.AlignContent = FlexContainer.Alignment.AlignCenter;
                    button.Label = CreateText("AlignContent : AlignCenter");
                }
                else if (flexContainer.AlignContent == FlexContainer.Alignment.AlignCenter)
                {
                    flexContainer.AlignContent = FlexContainer.Alignment.AlignFlexEnd;
                    button.Label = CreateText("AlignContent : AlignFlexEnd");
                }
                else if (flexContainer.AlignContent == FlexContainer.Alignment.AlignFlexEnd)
                {
                    flexContainer.AlignContent = FlexContainer.Alignment.AlignFlexStart;
                    button.Label = CreateText("AlignContent : AlignFlexStart");
                }
                else if (flexContainer.AlignContent == FlexContainer.Alignment.AlignFlexStart)
                {
                    flexContainer.AlignContent = FlexContainer.Alignment.AlignStretch;
                    button.Label = CreateText("AlignContent : AlignStretch");
                }
                else
                {
                    flexContainer.AlignContent = FlexContainer.Alignment.AlignAuto;
                    button.Label = CreateText("AlignContent : AlignAuto");
                }
            }

            return true;
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
            textVisual.Add(TextVisualProperty.PointSize, new PropertyValue(7));
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