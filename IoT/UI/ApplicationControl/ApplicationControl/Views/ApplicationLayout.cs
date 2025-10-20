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
using System.ComponentModel;
using System.Threading.Tasks;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using ApplicationControl.ViewModels;
using ApplicationControl.Helpers;
using ApplicationControl.Models;

namespace ApplicationControl.Views
{
    /// <summary>
    /// A class for an application list layout as a part of the main layout
    /// </summary>
    public class ApplicationLayout : View
    {
        private MainViewModel _viewModel;
        private ScrollableBase _scrollView;
        private View _applicationsContainer;
        private TextLabel _emptyStateLabel;

        public ApplicationLayout(MainViewModel viewModel, int screenWidth, int screenHeight)
        {
            _viewModel = viewModel;
            
            BackgroundColor = Resources.CardBackground;
            CornerRadius = Resources.Dimensions.CornerRadius;
            BoxShadow = new Shadow(Resources.Dimensions.CardElevation, new Color(0, 0, 0, 0.1f), new Vector2(0, 2));
            
            Layout = new LinearLayout
            {
                LinearOrientation = LinearLayout.Orientation.Vertical,
                CellPadding = new Size2D(0, Resources.Dimensions.Padding)
            };
            
            Padding = new Extents((ushort)Resources.Dimensions.Padding, (ushort)Resources.Dimensions.Padding,
                                 (ushort)Resources.Dimensions.Padding, (ushort)Resources.Dimensions.Padding);
            
            SetPropertyChangeListener();
            InitializeComponent();
        }

        void InitializeComponent()
        {
            // Header
            var header = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent
            };

            var title = new TextLabel
            {
                Text = "Applications",
                PointSize = Resources.FontSizes.Title,
                TextColor = Resources.TextPrimary,
                FontStyle = new PropertyMap().Add("weight", new PropertyValue("bold")),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Weight = 1.0f
            };
            header.Add(title);
            Add(header);

            // Scrollable container for applications
            _scrollView = new ScrollableBase
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Weight = 1.0f,
                ScrollingDirection = ScrollableBase.Direction.Vertical,
                HideScrollbar = false
            };

            _applicationsContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = new Size2D(0, Resources.Dimensions.PaddingSmall)
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent
            };

            _scrollView.Add(_applicationsContainer);
            Add(_scrollView);

            // Empty state
            _emptyStateLabel = new TextLabel
            {
                Text = "Select an operation type to view applications",
                PointSize = Resources.FontSizes.Body,
                TextColor = Resources.TextHint,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                MultiLine = true
            };
            _applicationsContainer.Add(_emptyStateLabel);

            // Action buttons
            var buttonContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    CellPadding = new Size2D(Resources.Dimensions.Padding, 0)
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent
            };

            var executeButton = Resources.CreateSecondaryButton("Execute", Resources.AccentGreen);
            executeButton.Weight = 1.0f;
            executeButton.Clicked += (s, e) =>
            {
                _viewModel.ExecuteSelectedApplication();
            };

            var killButton = Resources.CreateSecondaryButton("Kill", Resources.AccentRed);
            killButton.Weight = 1.0f;
            killButton.Clicked += (s, e) =>
            {
                _viewModel.KillSelectedApplication();
            };

            buttonContainer.Add(executeButton);
            buttonContainer.Add(killButton);
            Add(buttonContainer);
        }

        void SetPropertyChangeListener()
        {
            _viewModel.PropertyChanged += OnPropertyChanged;
        }

        async void OnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedAppControlType")
            {
                _viewModel.Applications.Clear();
                await UpdateContentLayout();
            }
        }

        Task UpdateContentLayout()
        {
            return Task.Run(() =>
            {
                // Clear existing items
                _applicationsContainer.Children.Clear();

                var selected = _viewModel.SelectedAppControlType;
                if (selected == AppControlType.Unknown)
                {
                    // Show empty state
                    _applicationsContainer.Add(_emptyStateLabel);
                    return;
                }

                var apps = ApplicationControlHelper.GetApplicationIdsForSpecificAppControlType(selected);
                
                foreach (var app in apps)
                {
                    var iconPath = ApplicationControlHelper.GetApplicationIconPath(app);
                    
                    var appItem = new ApplicationListItem
                    {
                        Id = app,
                        IconPath = iconPath,
                        BlendColor = Resources.Gray,
                    };
                    _viewModel.Applications.Add(appItem);

                    var appLayoutItem = CreateApplicationLayoutItem(appItem);
                    _applicationsContainer.Add(appLayoutItem);
                }
            });
        }

        View CreateApplicationLayoutItem(ApplicationListItem item)
        {
            var container = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    LinearAlignment = LinearLayout.Alignment.CenterVertical,
                    CellPadding = new Size2D(Resources.Dimensions.Padding, 0)
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 72,
                BackgroundColor = Resources.CardBackground,
                Padding = new Extents((ushort)Resources.Dimensions.Padding, (ushort)Resources.Dimensions.Padding,
                                     (ushort)Resources.Dimensions.PaddingSmall, (ushort)Resources.Dimensions.PaddingSmall)
            };

            // Icon
            View iconContainer;
            if (!string.IsNullOrEmpty(item.IconPath))
            {
                iconContainer = new ImageView
                {
                    ResourceUrl = item.IconPath,
                    WidthSpecification = 48,
                    HeightSpecification = 48,
                    CornerRadius = 24
                };
            }
            else
            {
                // Placeholder icon
                iconContainer = new View
                {
                    BackgroundColor = Resources.LightGray,
                    WidthSpecification = 48,
                    HeightSpecification = 48,
                    CornerRadius = 24
                };
            }

            // Text container
            var textContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = new Size2D(0, 2)
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Weight = 1.0f
            };

            var appIdLabel = new TextLabel
            {
                Text = item.Id ?? "Unknown",
                PointSize = Resources.FontSizes.Body,
                TextColor = Resources.TextPrimary,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Ellipsis = true
            };

            textContainer.Add(appIdLabel);

            container.Add(iconContainer);
            container.Add(textContainer);

            // Selection indicator
            var indicator = new View
            {
                BackgroundColor = Resources.Transparent,
                WidthSpecification = 4,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                CornerRadius = 2
            };
            container.Add(indicator);

            // Touch handling
            container.TouchEvent += (sender, e) =>
            {
                if (e.Touch.GetState(0) == PointStateType.Down)
                {
                    container.BackgroundColor = Resources.HoverColor;
                }
                else if (e.Touch.GetState(0) == PointStateType.Up)
                {
                    container.BackgroundColor = Resources.CardBackground;
                    
                    // Update selection
                    foreach (var app in _viewModel.Applications)
                    {
                        if (app.Id == item.Id)
                        {
                            _viewModel.SelectedItem = app;
                            indicator.BackgroundColor = Resources.PrimaryBlue;
                        }
                    }
                }
                return false;
            };

            return container;
        }
    }
}
