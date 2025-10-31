/*
 * Copyright (c) 2025 Samsung Electronics Co., Ltd All Rights Reserved
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

namespace AppCommon.Pages
{
    /// <summary>
    /// Main page with tab navigation (simulating ColoredTabbedPage)
    /// </summary>
    public class MainPage : ContentPage
    {
        private View _currentPageContent;
        private ApplicationInformationPage _appInfoPage;
        private PathsPage _pathsPage;
        private Button _appInfoTab;
        private Button _pathsTab;
        private View _tabBar;

        private int _screenWidth;
        private int _screenHeight;
        private bool _isLandscape;

        public ApplicationInformationPage AppInfoPage => _appInfoPage;

        public MainPage(int screenWidth = 720, int screenHeight = 1280)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _isLandscape = screenWidth > screenHeight;

            InitializeComponent();
            ShowPage(0); // Show first page by default
        }

        private void InitializeComponent()
        {
            // Create main layout
            var mainLayout = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent
            };

            // Create tab bar (adaptive height based on orientation)
            int tabBarHeight = _isLandscape ? 60 : 80;
            _tabBar = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = tabBarHeight,
                BackgroundColor = Resources.TabbedPageBarColor
            };

            // Create tab buttons
            _appInfoTab = new Button
            {
                Text = "Application Information",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Weight = 1.0f,
                BackgroundColor = Resources.TabbedPageBarColor,
                TextColor = Resources.White
            };
            _appInfoTab.Clicked += (s, e) => ShowPage(0);

            _pathsTab = new Button
            {
                Text = "Paths",
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Weight = 1.0f,
                BackgroundColor = Resources.TabbedPageBarColor,
                TextColor = Resources.White
            };
            _pathsTab.Clicked += (s, e) => ShowPage(1);

            _tabBar.Add(_appInfoTab);
            _tabBar.Add(_pathsTab);

            // Create content container
            _currentPageContent = new View
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent
            };

            // Add tab bar and content to main layout
            mainLayout.Add(_tabBar);
            mainLayout.Add(_currentPageContent);

            // Set content
            Content = mainLayout;

            // Create pages
            _appInfoPage = new ApplicationInformationPage(_screenWidth, _screenHeight);
            _pathsPage = new PathsPage(_screenWidth, _screenHeight);
        }

        private void ShowPage(int index)
        {
            // Clear current content
            if (_currentPageContent.ChildCount > 0)
            {
                _currentPageContent.Remove(_currentPageContent.GetChildAt(0));
            }

            // Update tab button states
            _appInfoTab.BackgroundColor = (index == 0) 
                ? new Color(0.6f, 0.15f, 0.4f, 1.0f) // Darker shade for selected
                : Resources.TabbedPageBarColor;
            
            _pathsTab.BackgroundColor = (index == 1)
                ? new Color(0.6f, 0.15f, 0.4f, 1.0f) // Darker shade for selected
                : Resources.TabbedPageBarColor;

            // Show selected page content
            switch (index)
            {
                case 0:
                    if (_appInfoPage.Content != null)
                    {
                        _currentPageContent.Add(_appInfoPage.Content);
                    }
                    break;
                case 1:
                    if (_pathsPage.Content != null)
                    {
                        _currentPageContent.Add(_pathsPage.Content);
                    }
                    break;
            }
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (type == DisposeTypes.Explicit)
            {
                _appInfoPage?.Dispose();
                _pathsPage?.Dispose();
            }
            base.Dispose(type);
        }
    }
}
