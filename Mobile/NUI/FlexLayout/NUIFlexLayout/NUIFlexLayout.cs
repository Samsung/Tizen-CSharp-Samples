/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NUIFlexLayout
{
    class Program : NUIApplication
    {
        /// <summary>
        /// All views present in the application
        /// </summary>
        private View RootView, TopView, ButtonView, FlexView, Description;
        /// <summary>
        /// Labels corresponding to the properties changed with a given button
        /// </summary>
        private TextLabel Label1, Label2, Label3, Label4;
        /// <summary>
        /// Buttons used to change properties of flex view
        /// </summary>
        private Button Button1, Button2, Button3, Button4;
        /// <summary>
        /// Number of items used in
        private static int ItemsCnt = 20;

        /// <summary>
        /// Overridden method that is called on app creation
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        /// <summary>
        /// Initialize all views, layouts and buttons used in application
        /// </summary>
        void Initialize()
        {
            // Initialize Views
            InitViews();
            // Initialize Flex Layout
            InitFlex();
            // Initialize Buttons
            InitButtons();
        }

        /// <summary>
        /// Initialize all views
        /// </summary>
        private void InitViews()
        {
            // Initialize Root View
            RootView = new View()
            {
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundColor = Color.Black,
            };

            // Initialize Top View
            TopView = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Padding = new Extents(20, 20, 20, 20),
                BackgroundColor = Color.Black,
            };

            // Initialize Button View
            ButtonView = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Padding = new Extents(20, 20, 20, 20),
                BackgroundColor = Color.Black,
            };

            // Initialize Description View
            Label1 = InitLabel(0.0f);
            Label2 = InitLabel(0.25f);
            Label3 = InitLabel(0.5f);
            Label4 = InitLabel(0.75f);
            Description = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 200,
            };
            Description.Add(Label1);
            Description.Add(Label2);
            Description.Add(Label3);
            Description.Add(Label4);

            // Add child views.
            RootView.Add(TopView);
            RootView.Add(ButtonView);
            RootView.Add(Description);
            Window.Instance.Add(RootView);
            InitFlex();
            Window.Instance.KeyEvent += OnKeyEvent;
        }

        /// <summary>
        /// Initialize a label and put it on the given coordinate inside a parent
        /// </summary>
        /// <param name="y">A vertical coordinate of the ParentOrigin</param>
        private TextLabel InitLabel(float y)
        {
            TextLabel Label = new TextLabel()
            {
                PositionUsesPivotPoint = true,
                PivotPoint = PivotPoint.TopCenter,
                ParentOrigin = new Vector3(0.5f, y, 0.0f),
                TextColor = Color.White,
            };
            return Label;
        }

        /// <summary>
        /// Initialize flex layout and fill it with text labels
        /// </summary>
        private void InitFlex()
        {
            // Initialize Flex View
            FlexView = new View()
            {
                Layout = new FlexLayout()
                {
                    WrapType = FlexLayout.FlexWrapType.Wrap
                },
                HeightSpecification = LayoutParamPolicies.MatchParent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                BackgroundColor = Color.Black,
            };
            Label1.Text = "Direction type: " + ((FlexLayout)FlexView.Layout).Direction;
            Label2.Text = "Justification type: " + ((FlexLayout)FlexView.Layout).Justification;
            Label3.Text = "Alignment type: " + ((FlexLayout)FlexView.Layout).Alignment;
            Label4.Text = "Wrap type: " + ((FlexLayout)FlexView.Layout).WrapType;

            TopView.Add(FlexView);

            // Add elements to Flex View
            for (int i = 0; i < ItemsCnt; i++)
            {
                TextLabel t = new TextLabel();
                t.Margin = new Extents(10, 10, 10, 10);
                t.Text = "X " + i;
                t.BackgroundColor = new Color(0.8f, 0.1f, 0.2f, 1.0f);
                FlexView.Add(t);
            }
        }

        /// <summary>
        /// Initialize buttons that change flex layout properties
        /// </summary>
        private void InitButtons()
        {
            LinearLayout ButtonLayout = new LinearLayout();
            ButtonLayout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            ButtonView.Layout = ButtonLayout;

            // Initialize Button1
            Button1 = new Button()
            {
                BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Text = "Dir",
                TextColor = Color.White,
                Margin = new Extents(10, 10, 10, 10),
            };

            // Initialize Button2
            Button2 = new Button()
            {
                BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Text = "Just",
                TextColor = Color.White,
                Margin = new Extents(10, 10, 10, 10),
            };

            // Initialize Button3
            Button3 = new Button()
            {
                BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Text = "Align",
                TextColor = Color.White,
                Margin = new Extents(10, 10, 10, 10),
            };

            // Initialize Button4
            Button4 = new Button()
            {
                BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Text = "Wrap",
                TextColor = Color.White,
                Margin = new Extents(10, 10, 10, 10),
            };

            // Add Buttons to parent view
            ButtonView.Add(Button1);
            ButtonView.Add(Button2);
            ButtonView.Add(Button3);
            ButtonView.Add(Button4);

            // Register button click events
            Button1.Clicked += Button1Clicked;
            Button2.Clicked += Button2Clicked;
            Button3.Clicked += Button3Clicked;
            Button4.Clicked += Button4Clicked;
        }

        /// <summary>
        /// The method called when the button 1 is clicked
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void Button1Clicked(object sender, ClickedEventArgs e)
        {
            FlexLayout Layout = (FlexLayout)FlexView.Layout;
            FlexLayout.FlexDirection newDirection = Layout.Direction + 1;
            if (newDirection > FlexLayout.FlexDirection.RowReverse) {
                newDirection = FlexLayout.FlexDirection.Column;
            }
            Label1.Text = "Direction type: " + newDirection;
            Layout.Direction = newDirection;
            FlexView.Layout = Layout;
        }

        /// <summary>
        /// The method called when the button 2 is clicked
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void Button2Clicked(object sender, ClickedEventArgs e)
        {
            FlexLayout Layout = (FlexLayout)FlexView.Layout;
            FlexLayout.FlexJustification newJust = Layout.Justification + 1;
            if (newJust > FlexLayout.FlexJustification.SpaceEvenly) {
                newJust = FlexLayout.FlexJustification.FlexStart;
            }
            Label2.Text = "Justification type: " + newJust;
            Layout.Justification = newJust;
            FlexView.Layout = Layout;
        }

        /// <summary>
        /// The method called when the button 3 is clicked
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void Button3Clicked(object sender, ClickedEventArgs e)
        {
            FlexLayout Layout = (FlexLayout)FlexView.Layout;
            FlexLayout.AlignmentType newAlign = Layout.Alignment + 1;
            if (newAlign > FlexLayout.AlignmentType.Stretch) {
                newAlign = FlexLayout.AlignmentType.Auto;
            }
            Label3.Text = "Alignment type: " + newAlign;
            Layout.Alignment = newAlign;
            FlexView.Layout = Layout;
        }

        /// <summary>
        /// The method called when the button 4 is clicked
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void Button4Clicked(object sender, ClickedEventArgs e)
        {
            FlexLayout Layout = (FlexLayout)FlexView.Layout;
            if (Layout.WrapType == FlexLayout.FlexWrapType.Wrap) {
                Layout.WrapType = FlexLayout.FlexWrapType.NoWrap;
            }
            else {
                Layout.WrapType = FlexLayout.FlexWrapType.Wrap;
            }
            Label4.Text = "Wrap type: " + Layout.WrapType;
            FlexView.Layout = Layout;
        }

        /// <summary>
        /// Method which is called when key event happens
        /// </summary>
        /// <param name="sender">Window instance</param>
        /// <param name="e">Event arguments</param>
        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
            app.Dispose();
        }
    }
}
