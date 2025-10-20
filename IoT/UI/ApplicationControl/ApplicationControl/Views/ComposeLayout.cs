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

using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using ApplicationControl.ViewModels;

namespace ApplicationControl.Views
{
    /// <summary>
    /// A class for a compose layout as a part of the main layout
    /// </summary>
    public class ComposeLayout : View
    {
        private MainViewModel _viewModel;

        public ComposeLayout(MainViewModel viewModel)
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
            
            InitializeComponent();
        }

        void InitializeComponent()
        {
            // Header
            var title = new TextLabel
            {
                Text = "Compose Message",
                PointSize = Resources.FontSizes.Title,
                TextColor = Resources.TextPrimary,
                FontStyle = new PropertyMap().Add("weight", new PropertyValue("bold")),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent
            };
            Add(title);

            // Message preview
            var messageContainer = new View
            {
                BackgroundColor = Resources.SectionBackground,
                CornerRadius = Resources.Dimensions.CornerRadius / 2,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                Weight = 1.0f,
                Padding = new Extents((ushort)Resources.Dimensions.Padding, (ushort)Resources.Dimensions.Padding,
                                     (ushort)Resources.Dimensions.Padding, (ushort)Resources.Dimensions.Padding)
            };

            var messageLabel = new TextLabel
            {
                Text = _viewModel.Message.Text,
                TextColor = Resources.TextSecondary,
                PointSize = Resources.FontSizes.Caption,
                MultiLine = true,
                LineWrapMode = LineWrapMode.Word,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent
            };
            messageContainer.Add(messageLabel);
            Add(messageContainer);

            // Input section
            var inputContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                    CellPadding = new Size2D(Resources.Dimensions.Padding, 0)
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent
            };

            var addressEntry = new TextField
            {
                PlaceholderText = "Recipient email address",
                BackgroundColor = Resources.LightGray,
                TextColor = Resources.TextPrimary,
                PlaceholderTextColor = Resources.TextHint,
                PointSize = Resources.FontSizes.Body,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = Resources.Dimensions.ButtonHeight,
                Weight = 1.0f,
                Padding = new Extents((ushort)Resources.Dimensions.Padding, (ushort)Resources.Dimensions.Padding, 0, 0),
                CornerRadius = Resources.Dimensions.CornerRadius / 2
            };

            addressEntry.TextChanged += (s, e) =>
            {
                _viewModel.Message.To = e.TextField.Text;
            };

            var sendButton = Resources.CreatePrimaryButton("Send");
            sendButton.WidthSpecification = 120;
            sendButton.Clicked += (s, e) =>
            {
                _viewModel.SendMessage();
            };

            inputContainer.Add(addressEntry);
            inputContainer.Add(sendButton);
            Add(inputContainer);
        }
    }
}
