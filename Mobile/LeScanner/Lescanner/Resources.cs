using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace Lescanner
{
    /// <summary>
    /// Static helper class for creating pre-styled UI components using raw data from AppStyles.
    /// This promotes consistency and reduces boilerplate code in page definitions.
    /// </summary>
    public static class Resources
    {
        /// <summary>
        /// Creates a TextLabel styled as a main title.
        /// </summary>
        /// <param name="text">The text for the label.</param>
        /// <returns>A styled TextLabel.</returns>
        public static TextLabel CreateTitleLabel(string text)
        {
            return new TextLabel
            {
                Text = text,
                TextColor = AppStyles.TextColorPrimary,
                PointSize = AppStyles.TitlePointSize,
                HorizontalAlignment = HorizontalAlignment.Center,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };
        }

        /// <summary>
        /// Creates a TextLabel styled for headers or list item titles.
        /// </summary>
        /// <param name="text">The text for the label.</param>
        /// <returns>A styled TextLabel.</returns>
        public static TextLabel CreateHeaderLabel(string text)
        {
            return new TextLabel
            {
                Text = text,
                TextColor = AppStyles.TextColorPrimary,
                PointSize = AppStyles.HeaderPointSize,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };
        }

        /// <summary>
        /// Creates a TextLabel styled for body text.
        /// </summary>
        /// <param name="text">The text for the label.</param>
        /// <returns>A styled TextLabel.</returns>
        public static TextLabel CreateBodyLabel(string text)
        {
            return new TextLabel
            {
                Text = text,
                TextColor = AppStyles.TextColorPrimary,
                PointSize = AppStyles.BodyPointSize,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };
        }
        
        /// <summary>
        /// Creates a TextLabel styled for detail or secondary text.
        /// </summary>
        /// <param name="text">The text for the label.</param>
        /// <returns>A styled TextLabel.</returns>
        public static TextLabel CreateDetailLabel(string text)
        {
            return new TextLabel
            {
                Text = text,
                TextColor = AppStyles.TextColorSecondary,
                PointSize = AppStyles.DetailPointSize,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };
        }

        /// <summary>
        /// Creates a Button styled as a primary action button.
        /// </summary>
        /// <param name="text">The text for the button.</param>
        /// <returns>A styled Button.</returns>
        public static Button CreatePrimaryButton(string text)
        {
            var button = new Button
            {
                Text = text,
                TextColor = AppStyles.ButtonTextColor,
                PointSize = AppStyles.BodyPointSize,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 72, // Fixed height for better touch experience
                Margin = AppStyles.ButtonMargin,
                BackgroundColor = AppStyles.PrimaryColor,
                CornerRadius = AppStyles.CornerRadius,
            };
            return button;
        }

        /// <summary>
        /// Creates a Button styled as a secondary action button.
        /// </summary>
        /// <param name="text">The text for the button.</param>
        /// <returns>A styled Button.</returns>
        public static Button CreateSecondaryButton(string text)
        {
            var button = new Button
            {
                Text = text,
                TextColor = AppStyles.PrimaryColor,
                PointSize = AppStyles.BodyPointSize,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 72,
                Margin = AppStyles.ButtonMargin,
                BackgroundColor = Color.Transparent, // Transparent background
                CornerRadius = AppStyles.CornerRadius,
            };
            // To create a border effect for a secondary button, we can wrap it in a View
            // or use a background image. For simplicity, we'll keep it without a border for now.
            return button;
        }

        /// <summary>
        /// Creates a View styled as a container for list items or cards.
        /// </summary>
        /// <returns>A styled View.</returns>
        public static View CreateListItemContainer()
        {
            return new View
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                BackgroundColor = AppStyles.ListBackgroundColor,
                CornerRadius = AppStyles.CornerRadius,
                Margin = AppStyles.ListElementMargin,
                // Padding = AppStyles.ListElementPadding // Padding will be handled by child elements or specific cases
            };
        }

        /// <summary>
        /// Creates a ScrollableBase styled for displaying lists.
        /// </summary>
        /// <returns>A styled ScrollableBase.</returns>
        public static ScrollableBase CreateScrollableList()
        {
            return new ScrollableBase
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                ScrollingDirection = ScrollableBase.Direction.Vertical,
                HideScrollbar = false,
                BackgroundColor = Color.Transparent,
            };
        }

        /// <summary>
        /// Creates a main content View with a vertical linear layout.
        /// </summary>
        /// <param name="padding">Optional padding to override the default page padding.</param>
        /// <returns>A styled View.</returns>
        public static View CreateMainLayoutContainer(Extents padding = null)
        {
            var layout = new LinearLayout
            {
                LinearOrientation = LinearLayout.Orientation.Vertical,
                CellPadding = AppStyles.LayoutCellPadding
            };
            var view = new View
            {
                Layout = layout,
                BackgroundColor = AppStyles.PageBackgroundColor,
                Padding = padding ?? AppStyles.PagePadding
            };
            view.WidthSpecification = LayoutParamPolicies.MatchParent;
            view.HeightSpecification = LayoutParamPolicies.MatchParent;
            return view;
        }

        /// <summary>
        /// Creates a TextField with a modern look, wrapped in a View to simulate a border.
        /// </summary>
        /// <param name="placeholderText">The placeholder text.</param>
        /// <returns>A View containing the styled TextField.</returns>
        public static View CreateStyledTextField(string placeholderText)
        {
            var textField = new TextField
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 64, // Fixed height
                PlaceholderText = placeholderText,
                BackgroundColor = Color.Transparent, // Transparent, border is handled by parent
                TextColor = AppStyles.TextColorPrimary,
                PointSize = AppStyles.BodyPointSize,
                Padding = new Extents(16, 16, 16, 16), // Internal text padding
                // Margin is handled by the container
            };

            var borderContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 64, // Match TextField height
                BackgroundColor = AppStyles.ListBackgroundColor, // White background for the field
                CornerRadius = AppStyles.CornerRadius,
                Margin = new Extents(0, 0, 16, 0), // Bottom margin for the container
                // To simulate a border, we can use a slightly larger background View with a different color
                // or use a BorderImage if available. For simplicity, we'll rely on CornerRadius for now.
                // A more robust border would require a custom drawing or a 9-patch image.
            };
            borderContainer.Add(textField);
            return borderContainer;
        }
    }
}
