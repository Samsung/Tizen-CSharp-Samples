﻿/*
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

            // Create a Layout
            Content = new StackLayout
            {
                Children =
                {
                   title,
                   scanButton,
                   scanListView,
                }
            };

            // Add DeviceDiscovered events
            wifidirect.DeviceDiscovered += OnDiscovered; 
        }

        /// <summary>
        /// Event handler when button is clicked
        /// </summary>
        /// /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void OnClicked(object sender, EventArgs e)
        {
            // Start to discover WiFiDirect devices
            if (scanButton.Text.Equals("Start Scan"))
            {
                // Update Current operation state
                scanButton.Text = "Stop Scanning";
                // Start to discover
                wifidirect.StartScan();
            }
            // Stop to discover WiFiDirect devices
            else
            {
                // Update Current operation state
                scanButton.Text = "Start Scan";
                // Stop to discover
                wifidirect.StopScan();
            }            
        }

        /// <summary>
        /// Event handler when WiFiDirect devices are clicked
        /// </summary>
        /// /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void OnDiscovered(object sender, DiscoveryEventArgs e)
        {
            log.Log("OnDiscovered");
            foreach (var item in e.deviceList)
            {
                log.Log(">> Found " + item);
            }
            // Show the discovered WiFiDirect devices
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
                Text = "WiFiDirect Test",
                TextColor = Color.White,
                FontSize = 28,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }
    }
}
