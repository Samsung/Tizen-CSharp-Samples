/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using ApplicationControl.ViewModels;
using ApplicationControl.Views;

namespace ApplicationControl.Pages
{
    /// <summary>
    /// Main page for ApplicationControl
    /// </summary>
    public class MainPage : ContentPage
    {
        private MainViewModel _viewModel;
        private OperationLayout _operationLayout;
        private ApplicationLayout _applicationLayout;
        private ComposeLayout _composeLayout;

        public MainPage()
        {
            _viewModel = new MainViewModel();
            InitializeComponent();
        }

        void InitializeComponent()
        {
            // Set page background
            BackgroundColor = Resources.BackgroundLight;

            // Get screen dimensions
            var window = NUIApplication.GetDefaultWindow();
            var screenWidth = (int)window.WindowSize.Width;
            var screenHeight = (int)window.WindowSize.Height;

            // Horizontal layout for landscape view
            // Left panel: Operation selection (30%)
            // Right panel: Application list and compose (70%)
            var mainLayout = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    CellPadding = new Size2D(Resources.Dimensions.Padding, 0)
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Padding = new Extents((ushort)Resources.Dimensions.Padding, (ushort)Resources.Dimensions.Padding, 
                                     (ushort)Resources.Dimensions.Padding, (ushort)Resources.Dimensions.Padding)
            };

            // Left Panel: Operation Selection
            _operationLayout = new OperationLayout(_viewModel)
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Weight = 0.3f
            };

            // Right Panel Container
            var rightPanel = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = new Size2D(0, Resources.Dimensions.Padding)
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Weight = 0.7f
            };

            // Application List (60% of right panel)
            _applicationLayout = new ApplicationLayout(_viewModel, screenWidth, screenHeight)
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Weight = 0.6f
            };

            // Compose Section (40% of right panel)
            _composeLayout = new ComposeLayout(_viewModel)
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Weight = 0.4f
            };

            rightPanel.Add(_applicationLayout);
            rightPanel.Add(_composeLayout);

            // Add panels to main layout
            mainLayout.Add(_operationLayout);
            mainLayout.Add(rightPanel);

            // Set content
            Content = mainLayout;

            // Set AppBar
            AppBar = new AppBar
            {
                Title = "Application Control",
                AutoNavigationContent = false
            };
        }
    }
}
