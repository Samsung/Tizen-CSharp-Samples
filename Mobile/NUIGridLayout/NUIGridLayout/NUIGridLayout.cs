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
 */﻿

using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NUIGridLayout
{
    class Program : NUIApplication
    {
        /// <summary>
        /// View representing the root for all other views
        /// </summary>
        private View RootView;
        /// <summary>
        /// View covering upper part of the screen
        /// </summary>
        private View TopView;
        /// <summary>
        /// View covering bottom part of the screen
        /// </summary>
        private View ButtonView;
        /// <summary>
        /// Buttons to change number of columns placed at the bottom of the screen
        /// </summary>
        private Button Button1, Button2;
        /// <summary>
        /// Items in view count
        /// </summary>
        private static int ItemsCnt = 40;
        /// <summary>
        /// Items in one column count
        /// </summary>
        private static int ColumnCnt = 5;

        /// <summary>
        /// Overriden method that is called after app launch
        /// <summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        /// <summary>
        /// Initialize all views, layout and buttons
        /// </summary>
        void Initialize()
        {
            InitViews();
            InitGrid();
            InitButtons();
        }

        /// <summary>
        /// Initialize all views
        /// </summary>
        private void InitViews()
        {
            RootView = new View();
            RootView.PositionUsesPivotPoint = true;
            RootView.PivotPoint = PivotPoint.Center;
            RootView.ParentOrigin = ParentOrigin.Center;
            RootView.WidthResizePolicy = ResizePolicyType.FillToParent;
            RootView.HeightResizePolicy = ResizePolicyType.FillToParent;

            TopView = new View();
            TopView.PositionUsesPivotPoint = true;
            TopView.PivotPoint = PivotPoint.TopCenter;
            TopView.ParentOrigin = new Vector3(0.5f, 0.05f, 0.0f);
            TopView.WidthResizePolicy = ResizePolicyType.FillToParent;
            TopView.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            TopView.SetSizeModeFactor(new Vector3(0.0f, 0.7f, 0.0f));
            TopView.Padding = new Extents(20, 20, 20, 20);

            GridLayout MyGridLayout = new GridLayout();
            MyGridLayout.Columns = ColumnCnt;
            TopView.Layout = MyGridLayout;
            InitGrid();

            ButtonView = new View();
            ButtonView.PositionUsesPivotPoint = true;
            ButtonView.PivotPoint = PivotPoint.TopCenter;
            ButtonView.ParentOrigin = new Vector3(0.5f, 0.85f, 0.0f);
            ButtonView.WidthResizePolicy = ResizePolicyType.FillToParent;
            ButtonView.HeightResizePolicy = ResizePolicyType.SizeRelativeToParent;
            ButtonView.SetSizeModeFactor(new Vector3(0.0f, 0.2f, 0.0f));
            ButtonView.Padding = new Extents(20, 20, 20, 20);

            RootView.Add(TopView);
            RootView.Add(ButtonView);
            Window.Instance.Add(RootView);
            Window.Instance.KeyEvent += OnKeyEvent;
        }

        /// <summary>
        /// Initialize Grid Layout with text labels
        /// </summary>
        private void InitGrid()
        {
            for (int i = 0; i < ItemsCnt; i++)
            {
                TextLabel t = new TextLabel();
                t.Margin = new Extents(10, 10, 10, 10);
                t.Text = "X";
                t.BackgroundColor = new Color(0.8f, 0.2f, 0.2f, 1.0f);
                TopView.Add(t);
            }
        }

        /// <summary>
        /// Initialize Buttons used in the application
        /// </summary>
        private void InitButtons()
        {
            LinearLayout ButtonLayout = new LinearLayout();
            ButtonLayout.LinearOrientation = LinearLayout.Orientation.Horizontal;
            ButtonView.Layout = ButtonLayout;

            Button1 = new Button();
            Button1.BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f);
            Button1.HeightResizePolicy = ResizePolicyType.FillToParent;
            Button1.Text = "Remove";
            Button1.TextColor = Color.White;
            Button1.Margin = new Extents(10, 10, 10, 10);
            Button1.Weight = 0.5f;

            Button2 = new Button();
            Button2.BackgroundColor = new Color(0.25f, 0.25f, 0.25f, 1.0f);
            Button2.HeightResizePolicy = ResizePolicyType.FillToParent;
            Button2.Text = "Add";
            Button2.TextColor = Color.White;
            Button2.Margin = new Extents(10, 10, 10, 10);
            Button2.Weight = 0.5f;

            ButtonView.Add(Button1);
            ButtonView.Add(Button2);

            Button1.ClickEvent += Button1Clicked;
            Button2.ClickEvent += Button2Clicked;
        }

        /// <summary>
        /// The method called when the left button is clicked
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void Button1Clicked(object sender, Button.ClickEventArgs e)
        {
            ColumnCnt = ColumnCnt - 1 > 0 ? ColumnCnt - 1 : 1;
            GridLayout MyGridLayout = new GridLayout();
            MyGridLayout.Columns = ColumnCnt;
            TopView.Layout = MyGridLayout;
        }

        /// <summary>
        /// The method called when the right button is clicked
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void Button2Clicked(object sender, Button.ClickEventArgs e)
        {
            ++ColumnCnt;
            GridLayout MyGridLayout = new GridLayout();
            MyGridLayout.Columns = ColumnCnt;
            TopView.Layout = MyGridLayout;
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
