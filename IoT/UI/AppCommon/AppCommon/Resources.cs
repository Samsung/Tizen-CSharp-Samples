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

using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace AppCommon
{
    /// <summary>
    /// Common resources for the application (colors, styles, etc.)
    /// </summary>
    internal static class Resources
    {
        // Colors
        public static readonly Color White = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        public static readonly Color Black = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        public static readonly Color Gray = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        public static readonly Color LightGray = new Color(0.875f, 0.875f, 0.875f, 1.0f); // RGB(223, 223, 223)
        public static readonly Color DarkGray = new Color(0.573f, 0.573f, 0.573f, 1.0f); // RGB(146, 146, 146)
        public static readonly Color TabbedPageBarColor = new Color(0.706f, 0.204f, 0.498f, 1.0f); // RGB(180, 52, 127)
        public static readonly Color Red = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        public static readonly Color Green = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        public static readonly Color Yellow = new Color(1.0f, 1.0f, 0.0f, 1.0f);

        // Font sizes
        public const float SmallFontSize = 20.0f;
        public const float NormalFontSize = 25.0f;
        public const float LargeFontSize = 130.0f;

        // TextLabel Styles
        public static readonly TextLabelStyle ContentTextStyle = new TextLabelStyle
        {
            PixelSize = NormalFontSize,
            TextColor = DarkGray,
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Begin,
            MultiLine = true,
            Ellipsis = false,
        };

        public static readonly TextLabelStyle LargeContentTextStyle = new TextLabelStyle
        {
            PixelSize = LargeFontSize,
            TextColor = Black,
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalAlignment = HorizontalAlignment.Begin,
        };

        // Button Styles
        public static readonly ButtonStyle PrimaryButtonStyle = new ButtonStyle
        {
            BackgroundColor = new Selector<Color>
            {
                Normal = TabbedPageBarColor,
                Pressed = new Color(0.6f, 0.15f, 0.4f, 1.0f),
            },
            Text = new TextLabelStyle
            {
                TextColor = White,
                PixelSize = NormalFontSize,
            }
        };

        // Helper method to create TextLabel with content style
        public static TextLabel CreateContentLabel(string text)
        {
            return new TextLabel
            {
                Text = text,
                PixelSize = NormalFontSize,
                TextColor = DarkGray,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Begin,
                MultiLine = true,
                Ellipsis = false,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };
        }

        // Helper method to create TextLabel with large content style
        public static TextLabel CreateLargeContentLabel(string text)
        {
            return new TextLabel
            {
                Text = text,
                PixelSize = LargeFontSize,
                TextColor = Black,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Begin,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };
        }
    }
}
