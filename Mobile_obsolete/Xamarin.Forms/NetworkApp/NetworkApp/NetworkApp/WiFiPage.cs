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
    /// The ContentPage for testing Tizen.Network.WiFi C# API
    /// </summary>
    class WiFiPage : ContentPage
    {
        ILog log = DependencyService.Get<ILog>();

        /// <summary>
        /// Constructor
        /// </summary>
        public WiFiPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize WiFiPage. Add components and events.
        /// </summary>
        private void InitializeComponent()
        {
            // Set title
            Title = "Wi-Fi";
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
                Text = "Wi-Fi Test",
                TextColor = Color.White,
                FontSize = 28,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }

        /// <summary>
        /// Create new ListView to show Tizen.Network.WiFi operations
        /// </summary>
        /// <returns>ListView</returns>
        private ListView CreateListView()
        {
            // Wi-Fi operations
            var SourceList = new List<String>()
            {
                // Activate Wi-Fi
                "Activate",
                // Deactivate Wi-Fi
                "Deactivate",
                // Scan
                "Scan",
                // Connect to the selected Wi-Fi AP
                "Connect",
                // Disconnect the selected Wi-Fi AP
                "Disconnect",
                // Forget the selected Wi-Fi AP
                "Forget",
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
                try
                {
                    // Item is "Activate"
                    if (a.Item.ToString().Equals("Activate"))
                    {
                        // Switch to WiFiResultPage to show the result of WiFiOperation.ACTIVATE
                        await Navigation.PushModalAsync(new WiFiResultPage(WiFiOperation.ACTIVATE));
                    }
                    // Item is "Deactivate"
                    else if (a.Item.ToString().Equals("Deactivate"))
                    {
                        // Switch to WiFiResultPage to show the result of WiFiOperation.ACTIVATE
                        await Navigation.PushModalAsync(new WiFiResultPage(WiFiOperation.DEACTIVATE));
                    }
                    // Item is "Scan"
                    else if (a.Item.ToString().Equals("Scan"))
                    {
                        // Switch to WiFiResultPage to show the result of WiFiOperation.SCAN
                        await Navigation.PushModalAsync(new WiFiResultPage(WiFiOperation.SCAN));
                    }
                    // Item is "Connect"
                    else if (a.Item.ToString().Equals("Connect"))
                    {
                        // Switch to WiFiResultPage to show the result of WiFiOperation.CONNECT
                        await Navigation.PushModalAsync(new WiFiResultPage(WiFiOperation.CONNECT));
                    }
                    // Item is "Disconnect"
                    else if (a.Item.ToString().Equals("Disconnect"))
                    {
                        // Switch to WiFiResultPage to show the result of WiFiOperation.DISCONNECT
                        await Navigation.PushModalAsync(new WiFiResultPage(WiFiOperation.DISCONNECT));
                    }
                    // Item is "Forget"
                    else if (a.Item.ToString().Equals("Forget"))
                    {
                        // Switch to WiFiResultPage to show the result of WiFiOperation.FORGET
                        await Navigation.PushModalAsync(new WiFiResultPage(WiFiOperation.FORGET));
                    }

                }
                catch (Exception e)
                {
                    log.Log("AddEvent " + e.ToString());
                }
            };
        }
    }
}
