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
using Xamarin.Forms;

namespace NetworkApp
{
    /// <summary>
    /// The ContentPage for testing Tizen.Network.WiFiDirect C# API
    /// </summary>
    class WiFiDirectPage : ContentPage
    {
        Button scanButton;
        ListView scanListView;

        IWiFiDirect wifidirect = DependencyService.Get<IWiFiDirect>();
        ILog log = DependencyService.Get<ILog>();

        // On an exception occurs, the result shows the message of exception
        Label result;

        /// <summary>
        /// Constructor
        /// </summary>
        public WiFiDirectPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize WiFiDirectPage. Add components and events.
        /// </summary>
        private void InitializeComponent()
        {
            // Set title message
            Title = "WiFiDirect";
            IsVisible = true;

            // Create components
            // Create Label
            Label title = CreateTitle();
            // Create Button
            scanButton = new Button()
            {
                // Set text to be shown on Button
                Text = "Start Scan",
            };

            // Add Clicked events
            scanButton.Clicked += OnClicked;

            scanListView = new ListView();

            result = new Label()
            {
                BackgroundColor = Color.White,
                FontSize = 20,
            };

            // Create a Layout
            Content = new StackLayout
            {
                Children =
                {
                   title,
                   scanButton,
                   scanListView,
                   result,
                }
            };

            try
            {
                // Add DeviceDiscovered events
                wifidirect.DeviceDiscovered += OnDiscovered;
            }
            // C# API throws NotSupportedException if the API is not supported
            catch (NotSupportedException)
            {
                result.Text = "The operation is not supported on this device";
            }
        }

        /// <summary>
        /// Event handler when button is clicked
        /// </summary>
        /// /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void OnClicked(object sender, EventArgs e)
        {
            try
            {
                // Start to discover Wi-Fi Direct devices
                if (scanButton.Text.Equals("Start Scan"))
                {
                    // Update Current operation state
                    scanButton.Text = "Stop Scanning";
                    // Start to discover
                    wifidirect.StartScan();
                }
                // Stop to discover Wi-Fi Direct devices
                else
                {
                    // Update Current operation state
                    scanButton.Text = "Start Scan";
                    // Stop to discover
                    wifidirect.StopScan();
                }
            }
            // C# API throws NotSupportedException if the API is not supported
            catch (NotSupportedException)
            {
                result.Text = "The operation is not supported on this device";
            }
        }

        /// <summary>
        /// Event handler when a found device is clicked
        /// </summary>
        /// /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void OnDiscovered(object sender, DiscoveryEventArgs e)
        {
            log.Log("OnDiscovered()");
            foreach (var item in e.deviceList)
            {
                log.Log(">> Found " + item);
            }
            // Show the discovered Wi-Fi Direct devices
            scanListView.ItemsSource = e.deviceList;
        }

        /// <summary>
        /// Create a new Label component
        /// </summary>
        /// <returns>Label</returns>
        private Label CreateTitle()
        {
            return new Label()
            {
                Text = "Wi-Fi Direct Test",
                TextColor = Color.White,
                FontSize = 28,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }
    }
}
