using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace NetworkApp
{
    /// <summary>
    /// The main menu page for navigating to different network functionalities.
    /// </summary>
    class MainMenuPage : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainMenuPage()
        {
            AppBar = new AppBar { Title = "Network App Menu" };
            InitializeComponent();
        }

        /// <summary>
        /// Initialize MainMenuPage. Add components and events.
        /// </summary>
        private void InitializeComponent()
        {
            var mainLayoutContainer = Resources.CreateMainLayoutContainer();
            // AppBar background is typically handled by the theme, but we can set it if needed.
            // AppBar.BackgroundColor = AppStyles.PrimaryColor; 

            var titleLabel = Resources.CreateTitleLabel("Select a Network Option");
            mainLayoutContainer.Add(titleLabel);

            var menuOptions = new List<string>
            {
                "Connection",
                "Wi-Fi",
                "Wi-Fi Direct"
            };

            var listContentContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = new Size2D(0, 12) // Spacing between menu items
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            foreach (var optionText in menuOptions)
            {
                var itemContainer = Resources.CreateListItemContainer();
                // Customize list item for menu: use primary color for background to make it pop
                itemContainer.BackgroundColor = AppStyles.PrimaryColor;
                itemContainer.Padding = AppStyles.ListElementPadding; // Add padding inside the item

                var itemLabel = Resources.CreateHeaderLabel(optionText); // Use Header style for menu items
                itemLabel.TextColor = AppStyles.AppBarTextColor; // Ensure text is white on blue background
                itemLabel.HorizontalAlignment = HorizontalAlignment.Center; // Center text in menu items
                itemLabel.Name = optionText; 
                
                itemContainer.Add(itemLabel);
                itemContainer.TouchEvent += OnMenuItemTapped; // Attach event to container
                listContentContainer.Add(itemContainer);
            }
            
            var scrollableList = Resources.CreateScrollableList();
            scrollableList.Add(listContentContainer);
            mainLayoutContainer.Add(scrollableList);

            Content = mainLayoutContainer;
        }

        /// <summary>
        /// Handles the tap event on a menu item container.
        /// </summary>
        /// <param name="source">The View container that was tapped.</param>
        /// <param name="e">Touch event arguments.</param>
        /// <returns>True if the event was consumed, false otherwise.</returns>
        private bool OnMenuItemTapped(object source, TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Down)
            {
                var tappedContainer = source as View;
                if (tappedContainer != null && tappedContainer.ChildCount > 0)
                {
                    var tappedLabel = tappedContainer.GetChildAt(0) as TextLabel;
                    if (tappedLabel != null)
                    {
                        var selectedItemText = tappedLabel.Name;
                        Tizen.Log.Info("MainMenuPage", $"Item tapped: {selectedItemText}");

                        var navigator = NUIApplication.GetDefaultWindow().GetDefaultNavigator();
                        if (navigator != null)
                        {
                            switch (selectedItemText)
                            {
                                case "Connection":
                                    navigator.Push(new ConnectionPage());
                                    break;
                                case "Wi-Fi":
                                    navigator.Push(new WiFiPage());
                                    break;
                                case "Wi-Fi Direct":
                                    navigator.Push(new WiFiDirectPage());
                                    break;
                            }
                        }
                    }
                }
            }
            return true; // Event was consumed
        }
    }
}
