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

namespace NetworkApp
{
    /// <summary>
    /// The ContentPage for testing Tizen.Network.WiFiDirect C# API
    /// </summary>
    class WiFiDirectPage : ContentPage
    {
        private Button scanButton;
        private ScrollableBase scanScrollableList;
        private View scanListContentContainer;
        private TextLabel resultLabel;

        private TizenWiFiDirectService wifiDirectService;

        /// <summary>
        /// Constructor
        /// </summary>
        public WiFiDirectPage()
        {
            AppBar = new AppBar { Title = "WiFiDirect" };
            InitializeComponent();
        }

        /// <summary>
        /// Initialize WiFiDirectPage. Add components and events.
        /// </summary>
        private void InitializeComponent()
        {
            wifiDirectService = new TizenWiFiDirectService();

            var mainLayoutContainer = Resources.CreateMainLayoutContainer();
            // WiFiDirectPage can use the default light theme from AppStyles

            var titleLabel = Resources.CreateTitleLabel("Wi-Fi Direct Test");
            mainLayoutContainer.Add(titleLabel);

            scanButton = Resources.CreatePrimaryButton("Start Scan");
            scanButton.Clicked += OnClicked;
            mainLayoutContainer.Add(scanButton);

            if (!wifiDirectService.IsInitialized)
            {
                Tizen.Log.Error("Xaml2NUI", "WiFiDirectPage: TizenWiFiDirectService failed to initialize.");
                // resultLabel will be created below, set text after creation
                scanButton.IsEnabled = false; 
            }
            else
            {
                wifiDirectService.DevicesUpdated += OnDevicesUpdated;
                Tizen.Log.Info("Xaml2NUI", "WiFiDirectPage: TizenWiFiDirectService initialized successfully.");
            }

            scanListContentContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = new Size2D(0, 8) // Less vertical padding for device list items
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            scanScrollableList = Resources.CreateScrollableList();
            scanScrollableList.Add(scanListContentContainer);
            mainLayoutContainer.Add(scanScrollableList);

            resultLabel = Resources.CreateDetailLabel(""); // Start with empty text
            resultLabel.BackgroundColor = Color.Transparent; // Make background transparent
            resultLabel.HorizontalAlignment = HorizontalAlignment.Center; // Center status text
            if (!wifiDirectService.IsInitialized)
            {
                 resultLabel.Text = "Wi-Fi Direct service could not be initialized. It may not be supported or enabled on this device.";
            }
            mainLayoutContainer.Add(resultLabel);

            Content = mainLayoutContainer;
        }

        /// <summary>
        /// Event handler when button is clicked
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (scanButton.Text.Equals("Start Scan"))
                {
                    if (wifiDirectService.StartScan())
                    {
                        scanButton.Text = "Stop Scanning";
                        resultLabel.Text = "Scanning for devices...";
                        // ClearDeviceList is no longer needed as UpdateDeviceList handles clearing.
                        // Explicitly clear by sending an empty list if desired, or rely on first DevicesUpdated event.
                        // For now, we rely on the first DevicesUpdated event to clear/update the list.
                        UpdateDeviceList(new List<WiFiDirectDevice>()); // Clear list immediately
                    }
                    else
                    {
                        resultLabel.Text = "Failed to start scan.";
                    }
                }
                else // "Stop Scanning"
                {
                    if (wifiDirectService.StopScan())
                    {
                        scanButton.Text = "Start Scan";
                        resultLabel.Text = "Scan stopped.";
                    }
                    else
                    {
                        resultLabel.Text = "Failed to stop scan.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultLabel.Text = $"Error: {ex.Message}";
                Tizen.Log.Error("Xaml2NUI", $"WiFiDirectPage: OnClicked Exception - {ex.ToString()}");
            }
        }

        /// <summary>
        /// Event handler when the list of discovered devices is updated via TizenWiFiDirectService
        /// </summary>
        /// <param name="sender">Event sender (TizenWiFiDirectService)</param>
        /// <param name="e">Event argument containing the list of discovered devices</param>
        private void OnDevicesUpdated(object sender, WiFiDirectDeviceEventArgs e)
        {
            Tizen.Log.Info("Xaml2NUI", $"WiFiDirectPage: Devices updated. Count: {e.Devices.Count}");
            UpdateDeviceList(e.Devices);
        }

        private void UpdateDeviceList(List<WiFiDirectDevice> devices)
        {
            // Clear existing items from the UI list
            while (scanListContentContainer.ChildCount > 0)
            {
                scanListContentContainer.Remove(scanListContentContainer.GetChildAt(0));
            }

            if (devices != null && devices.Count > 0)
            {
                foreach (var device in devices)
                {
                    var deviceContainer = Resources.CreateListItemContainer();
                    deviceContainer.Padding = AppStyles.ListElementPadding;

                    var deviceLabel = Resources.CreateBodyLabel(device.ToString());
                    
                    deviceContainer.Add(deviceLabel);
                    scanListContentContainer.Add(deviceContainer);
                }
                scanScrollableList.HeightSpecification = LayoutParamPolicies.WrapContent; // Show the list
                resultLabel.Text = $"Found {devices.Count} device(s)."; // Update status
            }
            else
            {
                scanScrollableList.HeightSpecification = 0; // Hide the list if no devices
                if (!resultLabel.Text.Equals("Scanning for devices..."))
                {
                    resultLabel.Text = "No devices found.";
                }
            }
        }
    }
}
