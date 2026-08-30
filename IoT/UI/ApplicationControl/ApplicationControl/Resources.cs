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

namespace ApplicationControl
{
    /// <summary>
    /// Static resource class for ApplicationControl
    /// </summary>
    internal static class Resources
    {
        // Colors - Modern Color Scheme
        public static readonly Color White = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        public static readonly Color Black = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        public static readonly Color Gray = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        public static readonly Color LightGray = new Color(0.95f, 0.95f, 0.95f, 1.0f);
        public static readonly Color DarkGray = new Color(0.3f, 0.3f, 0.3f, 1.0f);
        public static readonly Color Transparent = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        
        // Primary Colors
        public static readonly Color PrimaryBlue = new Color(0.26f, 0.52f, 0.96f, 1.0f); // #4285F4
        public static readonly Color PrimaryBlueDark = new Color(0.2f, 0.4f, 0.8f, 1.0f);
        public static readonly Color PrimaryBlueLight = new Color(0.67f, 0.8f, 0.98f, 1.0f);
        
        // Accent Colors
        public static readonly Color AccentGreen = new Color(0.2f, 0.73f, 0.29f, 1.0f); // #34BA52
        public static readonly Color AccentRed = new Color(0.92f, 0.26f, 0.21f, 1.0f); // #EA4335
        public static readonly Color AccentOrange = new Color(1.0f, 0.67f, 0.0f, 1.0f); // #FFAB00
        
        // Background Colors
        public static readonly Color BackgroundLight = new Color(0.98f, 0.98f, 0.98f, 1.0f);
        public static readonly Color CardBackground = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        public static readonly Color SectionBackground = new Color(0.96f, 0.96f, 0.96f, 1.0f);
        
        // Text Colors
        public static readonly Color TextPrimary = new Color(0.13f, 0.13f, 0.13f, 1.0f);
        public static readonly Color TextSecondary = new Color(0.45f, 0.45f, 0.45f, 1.0f);
        public static readonly Color TextHint = new Color(0.62f, 0.62f, 0.62f, 1.0f);
        
        // State Colors
        public static readonly Color PressedColor = new Color(0.0f, 0.0f, 0.0f, 0.1f);
        public static readonly Color SelectedColor = new Color(0.26f, 0.52f, 0.96f, 0.15f);
        public static readonly Color HoverColor = new Color(0.0f, 0.0f, 0.0f, 0.05f);

        // Dimensions
        public static class Dimensions
        {
            public const int Padding = 16;
            public const int PaddingSmall = 8;
            public const int PaddingLarge = 24;
            public const int CornerRadius = 8;
            public const int ButtonHeight = 48;
            public const int CardElevation = 4;
        }

        // Font Sizes
        public static class FontSizes
        {
            public const float Title = 24.0f;
            public const float Subtitle = 18.0f;
            public const float Body = 16.0f;
            public const float Caption = 14.0f;
            public const float Small = 12.0f;
        }

        // Helper Methods
        public static Shadow CreateShadow()
        {
            return new Shadow(2.0f, new Color(0, 0, 0, 0.2f), new Vector2(0, 2));
        }
        
        public static View CreateCard()
        {
            return new View
            {
                BackgroundColor = CardBackground,
                CornerRadius = Dimensions.CornerRadius,
                BoxShadow = new Shadow(Dimensions.CardElevation, new Color(0, 0, 0, 0.1f), new Vector2(0, 2))
            };
        }
        
        public static Button CreatePrimaryButton(string text)
        {
            return new Button
            {
                Text = text,
                BackgroundColor = PrimaryBlue,
                TextColor = White,
                CornerRadius = Dimensions.CornerRadius / 2,
                HeightSpecification = Dimensions.ButtonHeight,
                PointSize = FontSizes.Caption,
            };
        }
        
        public static Button CreateSecondaryButton(string text, Color color)
        {
            return new Button
            {
                Text = text,
                BackgroundColor = color,
                TextColor = White,
                CornerRadius = Dimensions.CornerRadius / 2,
                HeightSpecification = Dimensions.ButtonHeight,
                PointSize = FontSizes.Body
            };
        }
    }
}
