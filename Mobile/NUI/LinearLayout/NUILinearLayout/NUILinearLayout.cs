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

namespace NUILinearLayout
{
    class Program : NUIApplication
    {
        /// <summary>
        /// View representing the root view of the application
        /// </summary>
        private View RootView;
        /// <summary>
        /// Views used in the application to create layouts
        /// </summary>
        private View View1, View2, View3;
        /// <summary>
        /// Text labels used in the application
        /// </summary>
        private TextLabel TextLeft, TextRight;
        /// <summary>
        /// Images used in the application
        /// </summary>
        private ImageView Img1, Img2, Img3;
        /// <summary>
        /// Different colors used in the application
        /// </summary>
        private View Color1, Color2, Color3, Color4;
        /// <summary>
        /// String holding the location of the resource files
        /// </summary>
        private static string ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/";

        /// <summary>
        /// Overridden method that is called after app launches
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        /// <summary>
        /// Method that initializes views
        /// </summary>
        void Initialize()
        {
            // Initialize app
            // Add all necessary Views
            Window.Instance.BackgroundColor = Color.Red;
            Size2D ScreenSize = Window.Instance.WindowSize;
            RootView = new View();
            RootView.BackgroundColor = Color.White;
            RootView.Size2D = ScreenSize;

            // Add linear layout that will serve as a root for other layouts
            LinearLayout RootLayout = new LinearLayout();
            RootLayout.LinearOrientation = LinearLayout.Orientation.Vertical;

            RootView.Layout = RootLayout;
            RootView.Padding = new Extents(20, 20, 20, 20);

            // Create the first View
            View1 = new View();
            View1.BackgroundColor = new Color(0.8f, 0.7f, 0.3f, 1.0f);
            View1.WidthSpecification = LayoutParamPolicies.MatchParent;
            View1.Weight = 0.45f;
            View1.Padding = new Extents(10, 10, 10, 10);
            View1.Margin = new Extents(5, 5, 5, 5);

            // Create LinearLayout
            LinearLayout Layout1 = new LinearLayout();
            Layout1.LinearAlignment = LinearLayout.Alignment.Begin;
            Layout1.LinearOrientation = LinearLayout.Orientation.Horizontal;
            View1.Layout = Layout1;
            AddText(View1);

            // Create second View
            View2 = new View();
            View2.BackgroundColor = new Color(0.8f, 0.7f, 0.3f, 1.0f);
            View2.WidthSpecification = LayoutParamPolicies.MatchParent;
            View2.Weight = 0.25f;
            View2.Margin = new Extents(5, 5, 5, 5);

            // Create LinearLayout
            LinearLayout Layout2 = new LinearLayout();
            Layout2.LinearAlignment = LinearLayout.Alignment.Center;
            Layout2.LinearOrientation = LinearLayout.Orientation.Horizontal;
            View2.Layout = Layout2;
            AddImages(View2);

            // Create third View
            View3 = new View();
            View3.BackgroundColor = new Color(0.8f, 0.7f, 0.3f, 1.0f);
            View3.WidthSpecification = LayoutParamPolicies.MatchParent;
            View3.Weight = 0.3f;
            View3.Margin = new Extents(5, 5, 5, 5);

            // Create LinearLayout
            LinearLayout Layout3 = new LinearLayout();
            Layout3.LinearAlignment = LinearLayout.Alignment.Begin;
            Layout3.LinearOrientation = LinearLayout.Orientation.Horizontal;
            View3.Layout = Layout3;
            AddColors(View3);

            // Add child views
            RootView.Add(View1);
            RootView.Add(View2);
            RootView.Add(View3);

            Window.Instance.Add(RootView);

            Window.Instance.KeyEvent += OnKeyEvent;
        }

        /// <summary>
        /// Method that adds text field to passed view
        /// </summary>
        /// <param name="view">View</param>
        private void AddText(View view)
        {
            // Add text on the left side
            TextLeft = new TextLabel();
            TextLeft.BackgroundColor = new Color(0.8f, 0.1f, 0.1f, 1.0f);
            TextLeft.HeightSpecification = LayoutParamPolicies.MatchParent;
            TextLeft.Weight = 0.3f;
            TextLeft.TextColor = new Color(0.8f, 0.85f, 0.9f, 1.0f);
            TextLeft.Text = "0.3";
            TextLeft.HorizontalAlignment = HorizontalAlignment.Center;
            TextLeft.VerticalAlignment = VerticalAlignment.Center;
            TextLeft.Margin = new Extents(10, 10, 10, 10);

            // Add text on the right side
            TextRight = new TextLabel();
            TextRight.BackgroundColor = new Color(0.8f, 0.1f, 0.1f, 1.0f);
            TextRight.HeightSpecification = LayoutParamPolicies.MatchParent;
            TextRight.Weight = 0.7f;
            TextRight.TextColor = new Color(0.8f, 0.85f, 0.9f, 1.0f);
            TextRight.Text = "0.7";
            TextRight.HorizontalAlignment = HorizontalAlignment.Center;
            TextRight.VerticalAlignment = VerticalAlignment.Center;
            TextRight.Margin = new Extents(10, 10, 10, 10);

            // Add child views
            view.Add(TextLeft);
            view.Add(TextRight);
        }

        /// <summary>
        /// Method that adds images to passed view
        /// </summary>
        /// <param name="view">View</param>
        private void AddImages(View view)
        {
            // Load first image
            Img1 = new ImageView();
            Img1.ResourceUrl = ResourceUrl + "img1.svg";
            Img1.Size2D = new Size2D(100, 100);
            Img1.Margin = new Extents(20, 20, 20, 20);

            // Load second image
            Img2 = new ImageView();
            Img2.ResourceUrl = ResourceUrl + "img2.svg";
            Img2.Size2D = new Size2D(100, 100);
            Img2.Margin = new Extents(20, 20, 20, 20);

            // Load third image
            Img3 = new ImageView();
            Img3.ResourceUrl = ResourceUrl + "img3.svg";
            Img3.Size2D = new Size2D(100, 100);
            Img3.Margin = new Extents(20, 20, 20, 20);

            // Add child views
            view.Add(Img1);
            view.Add(Img2);
            view.Add(Img3);
        }

        /// <summary>
        /// Method that adds colors to passed view
        /// </summary>
        /// <param name="view">View</param>
        private void AddColors(View view)
        {
            Color1 = new View();
            Color1.BackgroundColor = new Color(0.1f, 0.1f, 0.1f, 1.0f);
            Color1.HeightSpecification = LayoutParamPolicies.MatchParent;
            Color1.Weight = 0.25f;
            Color1.Margin = new Extents(10, 10, 10, 10);

            Color2 = new View();
            Color2.BackgroundColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
            Color2.HeightSpecification = LayoutParamPolicies.MatchParent;
            Color2.Weight = 0.25f;
            Color2.Margin = new Extents(10, 10, 10, 10);

            Color3 = new View();
            Color3.BackgroundColor = new Color(0.1f, 0.1f, 0.1f, 1.0f);
            Color3.HeightSpecification = LayoutParamPolicies.MatchParent;
            Color3.Weight = 0.25f;
            Color3.Margin = new Extents(10, 10, 10, 10);

            Color4 = new View();
            Color4.BackgroundColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
            Color4.HeightSpecification = LayoutParamPolicies.MatchParent;
            Color4.Weight = 0.25f;
            Color4.Margin = new Extents(10, 10, 10, 10);

            view.Add(Color1);
            view.Add(Color2);
            view.Add(Color3);
            view.Add(Color4);
        }

        /// <summary>
        /// The method called when key pressed down
        /// </summary>
        /// <param name="sender">Key instance</param>
        /// <param name="e">Key's args</param>
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
