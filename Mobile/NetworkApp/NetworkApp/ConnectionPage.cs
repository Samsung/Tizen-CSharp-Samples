using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Binding; // Required for DataTemplate and more advanced binding with CollectionView

namespace NetworkApp
{
    /// <summary>
    /// Tizen NUI ContentPage for Network Connection functionalities,
    /// migrated from the Xamarin.Forms ConnectionPage.
    /// </summary>
        public class ConnectionPage : ContentPage
        {
            private TextLabel titleLabel;

            /// <summary>
            /// Constructor for ConnectionPage.
            /// </summary>
            public ConnectionPage()
            {
                // AppBar (equivalent to Xamarin.Forms Title)
                AppBar = new AppBar
                {
                    Title = "Connection" // From ConnectionPage.Title = "Connection"
                };

                // Initialize UI components and layout
                InitializeComponents();
            }

        /// <summary>
        /// Initializes the UI components and layout for the page.
        /// This method is equivalent to the Xamarin.Forms page's InitializeComponent.
        /// </summary>
        private void InitializeComponents()
        {
            var mainLayoutContainer = Resources.CreateMainLayoutContainer();

            titleLabel = Resources.CreateTitleLabel("Connection Test");
            mainLayoutContainer.Add(titleLabel);

            var stringSourceList = new List<string>
            {
                "Current Connection", "Wi-Fi State", "Cellular State", "IP Address",
                "Wi-Fi MAC Address", "Proxy Address", "Profile List"
            };

            var listContentContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = new Size2D(0, 12) // Spacing between list items
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            foreach (var itemText in stringSourceList)
            {
                var itemContainer = Resources.CreateListItemContainer();
                itemContainer.Padding = AppStyles.ListElementPadding;

                var itemLabel = Resources.CreateHeaderLabel(itemText);
                itemLabel.Name = itemText; 
                
                itemContainer.Add(itemLabel);
                itemContainer.TouchEvent += OnItemTapped; // Attach event to container
                listContentContainer.Add(itemContainer);
            }

            var scrollableList = Resources.CreateScrollableList();
            scrollableList.HeightSpecification = 700; 
            scrollableList.Add(listContentContainer);
            mainLayoutContainer.Add(scrollableList);

            Content = mainLayoutContainer;
        }

        /// <summary>
        /// Handles the tap event on a list item container.
        /// </summary>
        /// <param name="source">The View container that was tapped.</param>
        /// <param name="e">Touch event arguments.</param>
        /// <returns>True if the event was consumed, false otherwise.</returns>
        private bool OnItemTapped(object source, TouchEventArgs e)
        {
            // We only want to react to the touch start (or end) to avoid multiple actions.
            // A simple check for TouchState.Down is often enough for a "tap" feel.
            if (e.Touch.GetState(0) == PointStateType.Down)
            {
                var tappedContainer = source as View;
                if (tappedContainer != null && tappedContainer.ChildCount > 0)
                {
                    var tappedLabel = tappedContainer.GetChildAt(0) as TextLabel;
                    if (tappedLabel != null)
                    {
                        var selectedItemText = tappedLabel.Name; // Retrieve the text stored in the Name property
                        Tizen.Log.Info("ConnectionPage", $"Item tapped: {selectedItemText}");

                        ConnectionOperation operation = ConnectionOperation.CURRENT; // Default operation

                        switch (selectedItemText)
                        {
                            case "Current Connection":
                                operation = ConnectionOperation.CURRENT;
                                break;
                            case "Wi-Fi State":
                                operation = ConnectionOperation.WIFISTATE;
                                break;
                            case "Cellular State":
                                operation = ConnectionOperation.CELLULARSTATE;
                                break;
                            case "IP Address":
                                operation = ConnectionOperation.IPADDRESS;
                                break;
                            case "Wi-Fi MAC Address":
                                operation = ConnectionOperation.WIFIMACADDRESS;
                                break;
                            case "Proxy Address":
                                operation = ConnectionOperation.PROXYADDRESS;
                                break;
                            case "Profile List":
                                operation = ConnectionOperation.PROFILELIST;
                                break;
                        }

                        var navigator = NUIApplication.GetDefaultWindow().GetDefaultNavigator();
                        navigator.Push(new ConnectionResultPage(operation));
                    }
                }
            }
            return true; // Event was consumed
        }
    }
}
