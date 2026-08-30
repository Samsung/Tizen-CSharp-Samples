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
using ApplicationControl.Helpers;

namespace ApplicationControl.Views
{
    /// <summary>
    /// A class for an operation layout as a part of the main layout
    /// </summary>
    public class OperationLayout : View
    {
        private MainViewModel _viewModel;
        private RadioButtonGroup _radioGroup;

        public OperationLayout(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            BackgroundColor = Resources.CardBackground;
            CornerRadius = Resources.Dimensions.CornerRadius;
            BoxShadow = new Shadow(Resources.Dimensions.CardElevation, new Color(0, 0, 0, 0.1f), new Vector2(0, 2));
            
            Layout = new LinearLayout
            {
                LinearOrientation = LinearLayout.Orientation.Vertical,
                CellPadding = new Size2D(0, Resources.Dimensions.PaddingSmall)
            };
            
            Padding = new Extents((ushort)Resources.Dimensions.Padding, (ushort)Resources.Dimensions.Padding,
                                 (ushort)Resources.Dimensions.Padding, (ushort)Resources.Dimensions.Padding);
            
            // Create RadioButtonGroup for mutual exclusion
            _radioGroup = new RadioButtonGroup();
            
            InitializeComponent();
        }

        void InitializeComponent()
        {
            // Title
            var title = new TextLabel
            {
                Text = "Operation Type",
                PointSize = Resources.FontSizes.Title,
                TextColor = Resources.TextPrimary,
                FontStyle = new PropertyMap().Add("weight", new PropertyValue("bold")),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Margin = new Extents(0, 0, 0, (ushort)Resources.Dimensions.Padding)
            };
            Add(title);

            // Subtitle
            var subtitle = new TextLabel
            {
                Text = "Select an operation to view matching applications",
                PointSize = Resources.FontSizes.Caption,
                TextColor = Resources.TextSecondary,
                MultiLine = true,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Margin = new Extents(0, 0, 0, (ushort)Resources.Dimensions.PaddingLarge)
            };
            Add(subtitle);

            // Create operation items
            Add(CreateOperationItem("View", "Open and view content", AppControlType.View, Resources.PrimaryBlue));
            Add(CreateOperationItem("Pick", "Select files or data", AppControlType.Pick, Resources.AccentOrange));
            Add(CreateOperationItem("Compose", "Create new content", AppControlType.Compose, Resources.AccentGreen));
        }

        View CreateOperationItem(string title, string description, AppControlType type, Color accentColor)
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
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Margin = new Extents(0, 0, 0, (ushort)Resources.Dimensions.PaddingSmall)
            };

            // Radio Button
            var radioButton = new RadioButton
            {
                IsSelected = false,
                WidthSpecification = 24,
                HeightSpecification = 24
            };
            
            // Add to RadioButtonGroup for mutual exclusion
            _radioGroup.Add(radioButton);

            radioButton.SelectedChanged += (s, e) =>
            {
                if (e.IsSelected == false)
                {
                    return;
                }

                // Update selected item if type changes
                if (type != _viewModel.SelectedAppControlType)
                {
                    if (_viewModel.SelectedItem != null)
                    {
                        _viewModel.SelectedItem.Id = null;
                    }
                }

                _viewModel.SelectedAppControlType = type;
            };

            // Text container
            var textContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = new Size2D(0, 4)
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Weight = 1.0f
            };

            // Title
            var titleLabel = new TextLabel
            {
                Text = title,
                PointSize = Resources.FontSizes.Body,
                TextColor = Resources.TextPrimary,
                FontStyle = new PropertyMap().Add("weight", new PropertyValue("medium")),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent
            };

            // Description
            var descLabel = new TextLabel
            {
                Text = description,
                PointSize = Resources.FontSizes.Small,
                TextColor = Resources.TextSecondary,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent
            };

            textContainer.Add(titleLabel);
            textContainer.Add(descLabel);

            // Accent indicator
            var indicator = new View
            {
                BackgroundColor = accentColor,
                WidthSpecification = 4,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                CornerRadius = 2
            };

            container.Add(radioButton);
            container.Add(textContainer);
            container.Add(indicator);

            // Add touch feedback
            container.TouchEvent += (sender, e) =>
            {
                if (e.Touch.GetState(0) == PointStateType.Down)
                {
                    container.BackgroundColor = Resources.HoverColor;
                }
                else if (e.Touch.GetState(0) == PointStateType.Up)
                {
                    container.BackgroundColor = Resources.Transparent;
                    radioButton.IsSelected = true;
                }
                return false;
            };

            return container;
        }
    }
}
