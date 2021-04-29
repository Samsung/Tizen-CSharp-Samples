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
        private View RootView, TopView, ButtonView, FlexView;
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
            RootView = new View();
            RootView.PositionUsesPivotPoint = true;
            RootView.PivotPoint = PivotPoint.Center;
            RootView.ParentOrigin = ParentOrigin.Center;
            RootView.WidthResizePolicy = ResizePolicyType.FillToParent;
            RootView.HeightResizePolicy = ResizePolicyType.FillToParent;
            RootView.BackgroundColor = Color.Black;

            // Initialize Top View
            TopView = new View();
            TopView.PositionUsesPivotPoint = true;
            TopView.PivotPoint = PivotPoint.TopCenter;
            TopView.ParentOrigin = new Vector3(0.5f, 0.05f, 0.0f);
            TopView.WidthResizePolicy = ResizePolicyType.FillToParent;
            TopView.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            TopView.SetSizeModeFactor(new Vector3(0.0f, 0.7f, 0.0f));
            TopView.Padding = new Extents(20, 20, 20, 20);
            TopView.BackgroundColor = Color.Black;

            // Initialize Button View
            ButtonView = new View();
            ButtonView.PositionUsesPivotPoint = true;
            ButtonView.PivotPoint = PivotPoint.TopCenter;
            ButtonView.ParentOrigin = new Vector3(0.5f, 0.85f, 0.0f);
            ButtonView.WidthResizePolicy = ResizePolicyType.FillToParent;
            ButtonView.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            ButtonView.SetSizeModeFactor(new Vector3(0.0f, 0.2f, 0.0f));
            ButtonView.Padding = new Extents(20, 20, 20, 20);
            ButtonView.BackgroundColor = Color.Black;


            // Add child views.
            RootView.Add(TopView);
            RootView.Add(ButtonView);
            Window.Instance.Add(RootView);
            InitFlex();
            Window.Instance.KeyEvent += OnKeyEvent;
        }

        /// <summary>
        /// Initialize flex layout and fill it with text labels
        /// </summary>
        private void InitFlex()
        {   
            // Initialize Flex View
            FlexView = new View();
            FlexView.PositionUsesPivotPoint = true;
            FlexView.PivotPoint = PivotPoint.Center;
            FlexView.ParentOrigin = ParentOrigin.Center;
            FlexView.HeightSpecification = LayoutParamPolicies.MatchParent;
            FlexView.WidthSpecification = LayoutParamPolicies.MatchParent;
            FlexView.BackgroundColor = Color.Black;
            FlexView.Layout = new FlexLayout() {WrapType = FlexLayout.FlexWrapType.Wrap};
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
            Button1 = new Button();
            Button1.BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f);
            Button1.HeightResizePolicy = ResizePolicyType.FillToParent;
            Button1.Text = "Dir";
            Button1.TextColor = Color.White;
            Button1.Margin = new Extents(10, 10, 10, 10);
            Button1.Weight = 0.25f;

            // Initialize Button2
            Button2 = new Button();
            Button2.BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f);
            Button2.HeightResizePolicy = ResizePolicyType.FillToParent;
            Button2.Text = "Just";
            Button2.TextColor = Color.White;
            Button2.Margin = new Extents(10, 10, 10, 10);
            Button2.Weight = 0.25f;

            // Initialize Button3
            Button3 = new Button();
            Button3.BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f);
            Button3.HeightResizePolicy = ResizePolicyType.FillToParent;
            Button3.Text = "Align";
            Button3.TextColor = Color.White;
            Button3.Margin = new Extents(10, 10, 10, 10);
            Button3.Weight = 0.25f;

            // Initialize Button4
            Button4 = new Button();
            Button4.BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f);
            Button4.HeightResizePolicy = ResizePolicyType.FillToParent;
            Button4.Text = "Wrap";
            Button4.TextColor = Color.White;
            Button4.Margin = new Extents(10, 10, 10, 10);
            Button4.Weight = 0.25f;

            // Add Buttons to parent view
            ButtonView.Add(Button1);
            ButtonView.Add(Button2);
            ButtonView.Add(Button3);
            ButtonView.Add(Button4);

            // Register button click events
            Button1.ClickEvent += Button1Clicked;
            Button2.ClickEvent += Button2Clicked;
            Button3.ClickEvent += Button3Clicked;
            Button4.ClickEvent += Button4Clicked;
        }

        /// <summary>
        /// The method called when the button 1 is clicked
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void Button1Clicked(object sender, Button.ClickEventArgs e)
        {
            FlexLayout Layout = (FlexLayout)FlexView.Layout;
            FlexLayout.FlexDirection newDirection = Layout.Direction + 1;
            if (newDirection > FlexLayout.FlexDirection.RowReverse) {
                newDirection = FlexLayout.FlexDirection.Column;
            }
            Layout.Direction = newDirection;
            FlexView.Layout = Layout;
        }

        /// <summary>
        /// The method called when the button 2 is clicked
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void Button2Clicked(object sender, Button.ClickEventArgs e)
        {
            FlexLayout Layout = (FlexLayout)FlexView.Layout;
            FlexLayout.FlexJustification newJust = Layout.Justification + 1;
            if (newJust > FlexLayout.FlexJustification.SpaceAround) {
                newJust = FlexLayout.FlexJustification.FlexStart;
            }
            Layout.Justification = newJust;
            FlexView.Layout = Layout;
        }

        /// <summary>
        /// The method called when the button 3 is clicked
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void Button3Clicked(object sender, Button.ClickEventArgs e)
        {
            FlexLayout Layout = (FlexLayout)FlexView.Layout;
            FlexLayout.AlignmentType newAlign = Layout.Alignment + 1;
            if (newAlign > FlexLayout.AlignmentType.Stretch) {
                newAlign = FlexLayout.AlignmentType.Auto;
            }
            Layout.Alignment = newAlign;
            FlexView.Layout = Layout;
        }

        /// <summary>
        /// The method called when the button 4 is clicked
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void Button4Clicked(object sender, Button.ClickEventArgs e)
        {
            FlexLayout Layout = (FlexLayout)FlexView.Layout;
            if (Layout.WrapType == FlexLayout.FlexWrapType.Wrap) {
                Layout.WrapType = FlexLayout.FlexWrapType.NoWrap;
            }
            else {
                Layout.WrapType = FlexLayout.FlexWrapType.Wrap;
            }
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
