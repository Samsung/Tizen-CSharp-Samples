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
        private static int ItemsCnt = 55;
        /// <summary>
        /// Items in one column count
        /// </summary>
        private static int ColumnCnt = 6;
        /// <summary>
        /// Width/Height of the single item
        /// </summary>
        private static int ItemSide = 55;
        /// <summary>
        /// Right/Left/Top/Bottom single item margin
        /// </summary>
        private static ushort ItemMargin = 10;

        /// <summary>
        /// Custom style for a button
        /// <summary>
        ButtonStyle Style = new ButtonStyle
        {
            Size = new Size(300, 100),
            CornerRadius = 28.0f,
            BackgroundColor = new Selector<Color>()
            {
                Other = new Color(0.25f, 0.25f, 0.25f, 1.0f),
                Disabled = new Color(0.8f, 0.8f, 0.8f, 1.0f),
            },
        };

        /// <summary>
        /// Overridden method that is called after app launch
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
            RootView = new View()
            {
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
            };

            TopView = new View()
            {
                Layout = new GridLayout()
                {
                    Columns = ColumnCnt,
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Padding = new Extents(20, 20, 20, 20),
            };

            ButtonView = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Padding = new Extents(20, 20, 20, 20),
            };

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
                t.Margin = new Extents(ItemMargin, ItemMargin, ItemMargin, ItemMargin);
                t.Text = "X";
                t.HorizontalAlignment = HorizontalAlignment.Center;
                t.VerticalAlignment = VerticalAlignment.Center;
                t.Size2D = new Size2D(ItemSide, ItemSide);
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

            Button1 = new Button(Style)
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Text = "Remove",
                TextColor = Color.White,
                TextAlignment = HorizontalAlignment.Center,
                Margin = new Extents(10, 10, 10, 10),
            };

            Button2 = new Button(Style)
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Text = "Add",
                TextColor = Color.White,
                TextAlignment = HorizontalAlignment.Center,
                Margin = new Extents(10, 10, 10, 10),
            };

            ButtonView.Add(Button1);
            ButtonView.Add(Button2);

            Button1.Clicked += Button1Clicked;
            Button2.Clicked += Button2Clicked;
        }

        /// <summary>
        /// The method called when the left button is clicked
        /// It dos not allow to go outside the window boundaries - disables the left button
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void Button1Clicked(object sender, ClickedEventArgs e)
        {
            ColumnCnt = ColumnCnt - 1 > 0 ? ColumnCnt - 1 : 1;
            var MyGridLayout = TopView.Layout as GridLayout;
            MyGridLayout.Columns = ColumnCnt;

            if (!Button2.IsEnabled)
            {
                Button2.IsEnabled = true;
            }

            var RowCnt = Math.Ceiling((double)(ItemsCnt) / (MyGridLayout.Columns - 1));
            int NextItemsHeight = (int)((ItemSide + 2 * ItemMargin) * RowCnt + TopView.Padding.Top + TopView.Padding.Bottom);
            if (NextItemsHeight > TopView.SizeHeight)
            {
                Button1.IsEnabled = false;
            }
        }

        /// <summary>
        /// The method called when the right button is clicked.
        /// It dos not allow to go outside the window boundaries - disables the right button
        /// </summary>
        /// <param name="sender">Button instance</param>
        /// <param name="e">Event arguments</param>
        private void Button2Clicked(object sender, ClickedEventArgs e)
        {
            ++ColumnCnt;
            var MyGridLayout = TopView.Layout as GridLayout;
            MyGridLayout.Columns = ColumnCnt;

            if (!Button1.IsEnabled)
            {
                Button1.IsEnabled = true;
            }

            int NextItemsWidth = (int)((ItemSide + 2 * ItemMargin) * (ColumnCnt + 1) + TopView.Padding.Start + TopView.Padding.End);
            if (NextItemsWidth > TopView.SizeWidth)
            {
                Button2.IsEnabled = false;
            }
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
