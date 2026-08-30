using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NetworkApp
{
    /// <summary>
    /// Centralized class for defining UI raw style data for a modern, clean application.
    /// </summary>
    public static class AppStyles
    {
        // Color Palette - Refined for elegance
        public static readonly Color PrimaryColor = new Color(0.2f, 0.5f, 0.9f, 1.0f); // A softer, more sophisticated blue
        public static readonly Color PrimaryColorDark = new Color(0.15f, 0.4f, 0.75f, 1.0f); // Darker blue for hover/pressed states
        public static readonly Color SecondaryColor = new Color(0.97f, 0.97f, 0.99f, 1.0f); // A very clean, soft off-white for cards/sections
        public static readonly Color PageBackgroundColor = new Color(0.99f, 0.99f, 1.0f, 1.0f); // Barely off-white for page backgrounds
        public static readonly Color TextColorPrimary = new Color(0.15f, 0.15f, 0.2f, 1.0f); // Softer black for primary text
        public static readonly Color TextColorSecondary = new Color(0.55f, 0.55f, 0.6f, 1.0f); // Muted grey for secondary text
        public static readonly Color AppBarTextColor = Color.White;
        public static readonly Color ButtonTextColor = Color.White;
        public static readonly Color ListBackgroundColor = Color.White; // White for list item backgrounds
        public static readonly Color BorderColor = new Color(0.88f, 0.88f, 0.92f, 1.0f); // A very light, subtle border color

        // Typography - Reduced for better readability
        public static readonly float TitlePointSize = 28.0f; // For main page titles
        public static readonly float HeaderPointSize = 24.0f; // For section headers or list item titles
        public static readonly float BodyPointSize = 20.0f; // For standard body text, labels
        public static readonly float DetailPointSize = 18.0f; // For smaller details, status text

        // Spacing & Sizing
        public static readonly Extents PagePadding = new Extents(24, 24, 24, 24);
        public static readonly Extents ComponentPadding = new Extents(20, 20, 20, 20);
        public static readonly Extents ListElementPadding = new Extents(16, 16, 16, 16);
        public static readonly Extents ListElementMargin = new Extents(0, 0, 12, 0); // Vertical spacing between list items
        public static readonly Size2D LayoutCellPadding = new Size2D(0, 20); // Vertical spacing in layouts
        public static readonly Extents ButtonMargin = new Extents(0, 0, 16, 0); // Margin at the bottom of buttons
        public static readonly float CornerRadius = 10.0f; // Slightly smaller for a more refined look
        public static readonly float TextFieldBorderWidth = 1.5f; // For text fields (if using a View as a border)
    }
}
