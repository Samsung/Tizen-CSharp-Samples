using System;
using Tizen.NUI;
using Tizen.NUI.Components;

namespace Downloader
{
    /// <summary>
    /// Static class to hold common resources, styles, and constants for the application
    /// </summary>
    internal static class Resources
    {
        // Common Colors
        public static readonly Color PrimaryColor = new Color(0.1294f, 0.5882f, 0.9529f, 1.0f); // #2196F3
        public static readonly Color PrimaryDarkColor = new Color(0.094f, 0.4627f, 0.8235f, 1.0f); // #1976D2
        public static readonly Color PrimaryLightColor = new Color(0.2f, 0.7f, 1.0f, 1.0f); // #33B2FF
        public static readonly Color AccentColor = new Color(0.0f, 0.7373f, 0.8314f, 1.0f); // #00BCE5
        public static readonly Color BackgroundColor = new Color(0.98f, 0.98f, 0.98f, 1.0f); // #FAFAFA
        public static readonly Color SurfaceColor = Color.White;
        public static readonly Color CardColor = Color.White;
        public static readonly Color TextColor = new Color(0.13f, 0.13f, 0.13f, 1.0f); // #212121
        public static readonly Color TextColorSecondary = new Color(0.6f, 0.6f, 0.6f, 1.0f); // #999999
        public static readonly Color TextColorLight = new Color(0.87f, 0.87f, 0.87f, 1.0f); // #DEDEDE
        public static readonly Color DisabledColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
        public static readonly Color SuccessColor = new Color(0.298f, 0.686f, 0.314f, 1.0f); // #4CAF50
        public static readonly Color ErrorColor = new Color(0.957f, 0.263f, 0.212f, 1.0f); // #F44336
        public static readonly Color WarningColor = new Color(1.0f, 0.6f, 0.0f, 1.0f); // #FF9800
        public static readonly Color BorderColor = new Color(0.88f, 0.88f, 0.88f, 1.0f); // #E0E0E0
        public static readonly Color ShadowColor = new Color(0.0f, 0.0f, 0.0f, 0.1f); // Subtle shadow
        public static readonly Color WhiteColor = Color.White;
        public static readonly Color TrackColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        public static readonly Color TransparentColor = Color.Transparent;

        // Common Sizes
        public const float TextSizeTitle = 28.0f;
        public const float TextSizeLarge = 24.0f;
        public const float TextSizeMedium = 20.0f;
        public const float TextSizeSmall = 16.0f;
        public const float TextSizeExtraSmall = 14.0f;

        // Common Spacing
        public const int SpacingExtraSmall = 4;
        public const int SpacingSmall = 8;
        public const int SpacingMedium = 16;
        public const int SpacingLarge = 24;
        public const int SpacingExtraLarge = 32;

        // Component Heights
        public const int ButtonHeight = 56;
        public const int TextFieldHeight = 56;
        public const int ProgressBarHeight = 8;
        public const int TableHeight = 200;

        // Component Widths
        public const int NameLabelWidth = 200;

        // Layout Spacing for specific components
        public static readonly Size2D ItemLayoutSpacing = new Size2D(16, 0);
        public static readonly Size2D ZeroSpacing = new Size2D(0, 0);

        // Border Radius
        public static readonly Vector4 CornerRadiusSmall = new Vector4(4, 4, 4, 4);
        public static readonly Vector4 CornerRadiusMedium = new Vector4(8, 8, 8, 8);
        public static readonly Vector4 CornerRadiusLarge = new Vector4(12, 12, 12, 12);

        // Button Styles
        public static readonly ButtonStyle PrimaryButtonStyle = new ButtonStyle
        {
            CornerRadius = CornerRadiusMedium,
            Padding = new Extents(24, 24, 16, 16)
        };

        public static readonly ButtonStyle SecondaryButtonStyle = new ButtonStyle
        {
            CornerRadius = CornerRadiusMedium,
            Padding = new Extents(24, 24, 16, 16)
        };

        public static readonly ButtonStyle AppBarButtonStyle = new ButtonStyle
        {
            CornerRadius = CornerRadiusSmall,
            Padding = new Extents(16, 16, 8, 8)
        };

        // Layout Spacing
        public static readonly Size2D LayoutSpacingExtraSmall = new Size2D(0, SpacingExtraSmall);
        public static readonly Size2D LayoutSpacingSmall = new Size2D(0, SpacingSmall);
        public static readonly Size2D LayoutSpacingMedium = new Size2D(0, SpacingMedium);
        public static readonly Size2D LayoutSpacingLarge = new Size2D(0, SpacingLarge);

        // Padding
        public static readonly Extents PagePadding = new Extents(SpacingLarge, SpacingLarge, SpacingLarge, SpacingLarge);
        public static readonly Extents FormPadding = new Extents(SpacingLarge, SpacingLarge, SpacingMedium, SpacingMedium);
        public static readonly Extents CardPadding = new Extents(SpacingMedium, SpacingMedium, SpacingMedium, SpacingMedium);
        public static readonly Extents SectionPadding = new Extents(SpacingSmall, SpacingSmall, SpacingSmall, SpacingSmall);
        public static readonly Extents TextFieldPadding = new Extents(16, 16, 12, 12);
        public static readonly Extents AppBarButtonPadding = new Extents(40, 40, 8, 8);
    }
}
