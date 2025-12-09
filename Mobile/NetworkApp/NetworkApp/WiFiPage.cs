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
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Binding;

namespace NetworkApp
{
    /// <summary>
    /// The ContentPage for testing Tizen.Network.WiFi C# API
    /// </summary>
    class WiFiPage : ContentPage
    {
        // ILog log = DependencyService.Get<ILog>(); // To be handled if a similar service is available in Tizen NUI

        private TextLabel titleLabel;

        /// <summary>
        /// Constructor
        /// </summary>
        public WiFiPage()
        {
            AppBar = new AppBar { Title = "Wi-Fi" };
            InitializeComponent();
        }

        /// <summary>
        /// Initialize WiFiPage. Add components and events.
        /// </summary>
        private void InitializeComponent()
        {
            var mainLayoutContainer = Resources.CreateMainLayoutContainer();

            titleLabel = Resources.CreateTitleLabel("Wi-Fi Test");
            mainLayoutContainer.Add(titleLabel);

            var wifiOperationsList = new List<string>
            {
                "Activate",
                "Deactivate",
                "Scan",
                "Connect",
                "Disconnect",
                "Forget",
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

            foreach (var opText in wifiOperationsList)
            {
                var itemContainer = Resources.CreateListItemContainer();
                itemContainer.Padding = AppStyles.ListElementPadding;

                var itemLabel = Resources.CreateHeaderLabel(opText);
                itemLabel.Name = opText; // Store operation text for identification
                
                itemContainer.Add(itemLabel);
                // Attach the TouchEvent to the container to ensure the whole item is tappable
                itemContainer.TouchEvent += OnWiFiItemTapped; 
                listContentContainer.Add(itemContainer);
            }

            var scrollableList = Resources.CreateScrollableList();
            // Give scrollable list a more defined height to make it scrollable if content is long
            scrollableList.HeightSpecification = 700; 
            scrollableList.Add(listContentContainer);
            mainLayoutContainer.Add(scrollableList);

            Content = mainLayoutContainer;
        }

        /// <summary>
        /// Handles the tap event on a Wi-Fi operation item container.
        /// </summary>
        /// <param name="source">The View container that was tapped.</param>
        /// <param name="e">Touch event arguments.</param>
        /// <returns>True if the event was consumed, false otherwise.</returns>
        private bool OnWiFiItemTapped(object source, TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Down)
            {
                var tappedContainer = source as View;
                if (tappedContainer != null && tappedContainer.ChildCount > 0)
                {
                    // We assume the first child is the TextLabel holding the operation name
                    var tappedLabel = tappedContainer.GetChildAt(0) as TextLabel;
                    if (tappedLabel != null)
                    {
                        var selectedItemText = tappedLabel.Name;
                        Tizen.Log.Info("WiFiPage", $"Item tapped: {selectedItemText}");

                        WiFiResultPage.WiFiOperation operation = WiFiResultPage.WiFiOperation.Activate; // Default

                        switch (selectedItemText)
                        {
                            case "Activate":
                                operation = WiFiResultPage.WiFiOperation.Activate;
                                break;
                            case "Deactivate":
                                operation = WiFiResultPage.WiFiOperation.Deactivate;
                                break;
                            case "Scan":
                                operation = WiFiResultPage.WiFiOperation.Scan;
                                break;
                            case "Connect":
                                operation = WiFiResultPage.WiFiOperation.Connect;
                                break;
                            case "Disconnect":
                                operation = WiFiResultPage.WiFiOperation.Disconnect;
                                break;
                            case "Forget":
                                operation = WiFiResultPage.WiFiOperation.Forget;
                                break;
                        }
                        
                        var navigator = NUIApplication.GetDefaultWindow().GetDefaultNavigator();
                        navigator.Push(new WiFiResultPage(operation));
                    }
                }
            }
            return true; // Event was consumed
        }
    }
}
