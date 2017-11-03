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
    /// The ContentPage for testing Tizen.Network.Connection C# API
    /// </summary>
    class ConnectionPage : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize ConnectionPage. Add components and events.
        /// </summary>
        private void InitializeComponent()
        {
            // Set title
            Title = "Connection";
            IsVisible = true;

            // Create Label
            Label title = CreateTitle();
            // Create ListView
            ListView list = CreateListView();

            // Add events
            AddEvent(list);

            // Create a Layout
            Content = new StackLayout
            {
                Children =
                {
                    // Add components to the ConnectionPage
                   title,
                   list,
                }
            };
        }

        /// <summary>
        /// Create a new Label component
        /// </summary>
        /// <returns>Label</returns>
        private Label CreateTitle()
        {
            return new Label()
            {
                Text = "Connection Test",
                TextColor = Color.White,
                FontSize = 28,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }

        /// <summary>
        /// Create a new ListView to show Tizen.Network.Connection operations
        /// </summary>
        /// <returns>ListView</returns>
        private ListView CreateListView()
        {
            // Connection operations
            var SourceList = new List<String>()
            {
                // Show the current connected network
                "Current Connection",
                // Show the current Wi-Fi State
                "Wi-Fi State",
                // Show the current Cellular state
                "Cellular State",
                // Show the IPv4 address and the IPv6 address of the current connection
                "IP Address",
                // Show the MAC address of Wi-Fi
                "Wi-Fi MAC Address",
                // Show the proxy address
                "Proxy Address",
                // Show the connection profiles
                "Profile List",
            };
            // Create a ListView
            ListView ListView = new ListView()
            {
                // Set the ItemSource of ListView scanListView
                ItemsSource = SourceList,
            };
            return ListView;
        }

        /// <summary>
        /// Add ItemTapped event to ListView
        /// </summary>
        /// <param name="list">ListView to add event</param>
        private void AddEvent(ListView list)
        {
            // Add ItemTapped event
            list.ItemTapped += async (s, a) =>
            {
                // Item is "Current Connection"
                if (a.Item.ToString().Equals("Current Connection"))
                {
                    // Switch to WiFiResultPage to show the result of ConnectionOperation.CURRENT
                    await Navigation.PushModalAsync(new ConnectionResultPage(ConnectionOperation.CURRENT));
                }
                // Item is "Wi-Fi State"
                else if (a.Item.ToString().Equals("Wi-Fi State"))
                {
                    // Switch to WiFiResultPage to show the result of ConnectionOperation.WIFISTATE
                    await Navigation.PushModalAsync(new ConnectionResultPage(ConnectionOperation.WIFISTATE));
                }
                // Item is "Cellular State"
                else if (a.Item.ToString().Equals("Cellular State"))
                {
                    // Switch to WiFiResultPage to show the result of ConnectionOperation.CELLULARSTATE
                    await Navigation.PushModalAsync(new ConnectionResultPage(ConnectionOperation.CELLULARSTATE));
                }
                // Item is "IP Address"
                else if (a.Item.ToString().Equals("IP Address"))
                {
                    // Switch to WiFiResultPage to show the result of ConnectionOperation.IPADDRESS
                    await Navigation.PushModalAsync(new ConnectionResultPage(ConnectionOperation.IPADDRESS));
                }
                // Item is "Wi-Fi MAC Address"
                else if (a.Item.ToString().Equals("Wi-Fi MAC Address"))
                {
                    // Switch to WiFiResultPage to show the result of ConnectionOperation.WIFIMACADDRESS
                    await Navigation.PushModalAsync(new ConnectionResultPage(ConnectionOperation.WIFIMACADDRESS));
                }
                // Item is "Proxy Address"
                else if (a.Item.ToString().Equals("Proxy Address"))
                {
                    // Switch to WiFiResultPage to show the result of ConnectionOperation.PROXYADDRESS
                    await Navigation.PushModalAsync(new ConnectionResultPage(ConnectionOperation.PROXYADDRESS));
                }
                // Item is "Profile List"
                else if (a.Item.ToString().Equals("Profile List"))
                {
                    // Switch to WiFiResultPage to show the result of ConnectionOperation.PROFILELIST
                    await Navigation.PushModalAsync(new ConnectionResultPage(ConnectionOperation.PROFILELIST));
                }
            };
        }
    }
}
