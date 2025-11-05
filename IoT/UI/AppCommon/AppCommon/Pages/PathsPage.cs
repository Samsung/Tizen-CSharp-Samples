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
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using AppCommon.Models;
using AppCommon.ViewModels;

namespace AppCommon.Pages
{
    /// <summary>
    /// Paths information page
    /// </summary>
    public class PathsPage : ContentPage
    {
        private PathsPageViewModel _viewModel;
        private ScrollableBase _scrollView;
        private View _listContainer;
        private Button _floatingButton;
        private AlertDialog _popup;

        private int _screenWidth;
        private int _screenHeight;
        private double _horizontalScale;
        private bool _isLandscape;

        public PathsPage(int screenWidth = 720, int screenHeight = 1280)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _isLandscape = screenWidth > screenHeight;
            _horizontalScale = (double)screenWidth / 720.0;

            // Create ViewModel
            _viewModel = new PathsPageViewModel();

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Set page properties
            AppBar = new AppBar
            {
                Title = "Paths"
            };

            // Create main layout with simple background
            var mainLayout = new View
            {
                Layout = new AbsoluteLayout(),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundColor = new Color(0.95f, 0.95f, 0.95f, 1.0f) // Light gray background
            };

            // Create scrollable list container
            _scrollView = new ScrollableBase
            {
                ScrollingDirection = ScrollableBase.Direction.Vertical,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                HideScrollbar = false
            };

            _listContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            // Add path items to list
            foreach (var pathInfo in _viewModel.Paths)
            {
                var itemView = CreatePathItemView(pathInfo);
                _listContainer.Add(itemView);
            }

            _scrollView.Add(_listContainer);
            mainLayout.Add(_scrollView);

            // Create floating button (simple text button)
            _floatingButton = new Button
            {
                Text = "↑",
                Size = new Size((float)(100 * _horizontalScale), (float)(100 * _horizontalScale)),
                Position = new Position(_screenWidth * 0.8194f, _screenHeight * 0.8948f),
                BackgroundColor = Resources.TabbedPageBarColor,
                TextColor = Color.White,
                CornerRadius = (float)(50 * _horizontalScale)
            };
            _floatingButton.Clicked += OnFloatingButtonClicked;
            mainLayout.Add(_floatingButton);

            // Set content
            Content = mainLayout;
        }

        private View CreatePathItemView(PathInformation pathInfo)
        {
            int itemHeight = (int)(227.0 * _horizontalScale);

            var itemView = new View
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = itemHeight,
                BackgroundColor = Color.White,
                Margin = new Extents(0, 0, 2, 2),
                Focusable = true,
                FocusableInTouch = true,
                BoxShadow = new Shadow(5.0f, new Color(0, 0, 0, 0.2f), new Vector2(0, 2))
            };

            var itemLayout = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = new Size2D(10, 10),
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Top
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Padding = new Extents(20, 20, 10, 10)
            };

            // Title
            var titleLabel = new TextLabel
            {
                Text = pathInfo.Title,
                PixelSize = 30.0f,
                TextColor = Resources.Black,
                FontStyle = new PropertyMap().Add("weight", new PropertyValue("bold")),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent
            };
            itemLayout.Add(titleLabel);

            // Path
            var pathLabel = new TextLabel
            {
                Text = pathInfo.Path,
                PixelSize = Resources.NormalFontSize,
                TextColor = Resources.DarkGray,
                MultiLine = true,
                Ellipsis = true,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent
            };
            itemLayout.Add(pathLabel);

            itemView.Add(itemLayout);

            // Add touch event
            itemView.TouchEvent += (sender, e) =>
            {
                if (e.Touch.GetState(0) == PointStateType.Up)
                {
                    OnListItemSelected(pathInfo);
                    return true;
                }
                return false;
            };

            return itemView;
        }

        private void OnListItemSelected(PathInformation selectedItem)
        {
            var title = selectedItem.Title;
            var path = selectedItem.Path;
            var count = 0;

            try
            {
                count = _viewModel.GetFilesCount(path);
            }
            catch
            {
                count = 0;
            }

            // Create popup dialog
            ShowPathDetailsDialog(title, path, count);
        }

        private void ShowPathDetailsDialog(string title, string path, int count)
        {
            _popup = new AlertDialog
            {
                Title = title,
            };

            var contentView = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = new Size2D(10, 10)
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Padding = new Extents(20, 20, 20, 20)
            };

            var pathLabel = new TextLabel
            {
                Text = path,
                PixelSize = Resources.NormalFontSize,
                TextColor = Resources.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                MultiLine = true,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent
            };
            contentView.Add(pathLabel);

            var countLabel = new TextLabel
            {
                Text = $"File count: {count}",
                PixelSize = Resources.NormalFontSize,
                TextColor = Resources.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent
            };
            contentView.Add(countLabel);

            _popup.Content = contentView;

            var closeButton = new Button
            {
                Text = "CLOSE",
            };
            closeButton.Clicked += (sender, e) =>
            {
                _popup.Hide();
            };

            _popup.Actions = new View[] { closeButton };
            _popup.Show();
        }

        private void OnFloatingButtonClicked(object sender, ClickedEventArgs e)
        {
            // Scroll to top
            _scrollView.ScrollTo(0, true);
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (type == DisposeTypes.Explicit)
            {
                if (_floatingButton != null)
                {
                    _floatingButton.Clicked -= OnFloatingButtonClicked;
                }
            }
            base.Dispose(type);
        }
    }
}
