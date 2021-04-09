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

namespace ServiceDiscovery
{
    /// <summary>
    /// ContentPage to operate DNS-SD Service Discovery
    /// </summary>
    class DiscoveryPage : ContentPage
    {
        // Data structure
        /// <summary>
        /// Dictionary to store found services
        /// </summary>
        private Dictionary<string, DNSSDService> serviceMap = new Dictionary<string, DNSSDService>();

        // Components
        /// <summary>
        /// TableView to input service type
        /// </summary>
        private TableView inputView;
        /// <summary>
        /// Entry to input the type of DNS-SD Service
        /// </summary>
        private EntryCell typeEntryCell;
        /// <summary>
        /// TableView to show found services
        /// </summary>
        private TableView resultView;
        /// <summary>
        /// Button to start discovery
        /// </summary>
        Button discoveryButton;

        /// <summary>
        /// Constructor
        /// </summary>
        public DiscoveryPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize Page
        /// Add components and events
        /// </summary>
        private void InitializeComponent()
        {
            Title = "DNS-SD Service Discovery";

            // Create components
            // Create inputView
            inputView = CreateTableView(130);

            // Create typeEntryCell to be added to inputView
            typeEntryCell = CreateEntryCell("Service Type", "Enter service type. e.g. _http._tcp");

            // Add typeEntryCell to inputView
            inputView.Root.Add(new TableSection
            {
                typeEntryCell,
            });

            discoveryButton = new Button()
            {
                Text = "Start to discover",
            };

            // Create resultView
            resultView = CreateTableView(1000);

            // Add components
            Content = new StackLayout
            {
                Children =
                {
                    inputView,
                    discoveryButton,
                    resultView,
                }
            };

            // Add events
            discoveryButton.Clicked += OnClicked;
        }


        /// <summary>
        /// Create an EntryCell instance
        /// </summary>
        /// <param name="label">Label of EntryCell</param>
        /// <param name="placeholder">Placeholder of EntryCell</param>
        /// <returns>EntryCell</returns>
        private EntryCell CreateEntryCell(string label, string placeholder)
        {
            return new EntryCell()
            {
                Height = 200,
                Label = label + ": ",
                Placeholder = placeholder,
            };
        }

        /// <summary>
        /// Create an TextCell instance
        /// </summary>
        /// <param name="text">Text of TextCell</param>
        /// <param name="detail">Placeholder of TextCell</param>
        /// <returns>TextCell</returns>
        private TextCell CreateTextCell(string text, string detail)
        {
            return new TextCell()
            {
                Text = text + ": ",
                Detail = detail,
            };
        }
        /// <summary>
        /// Create a TableView.
        /// It contains EntryCells.
        /// </summary>
        /// <param name="height">HeightRequest</param>
        /// <returns>TableView</returns>
        private TableView CreateTableView(int height)
        {
            return new TableView()
            {
                HeightRequest = height,
                Intent = TableIntent.Form,
                Margin = new Thickness(10, 10),
                VerticalOptions = LayoutOptions.Start,
                Root = new TableRoot(),
            };
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
                if (discoveryButton.Text.Equals("Start to discover"))
                {
                    // Disable input
                    inputView.IsEnabled = false;
                    // Change text of button
                    discoveryButton.Text = "Stop";
                    string type = typeEntryCell.Text;

                    // Clear found services
                    serviceMap.Clear();
                    // Clear result for found services
                    resultView.Root.Clear();

                    // Add event handler
                    // It is invoked when a DNS-SD service is found
                    DependencyService.Get<IServiceDiscovery>().DNSSDServiceFound += OnServiceFound;

                    // Start to discover DNS-SD services
                    // with the same service type
                    DependencyService.Get<IServiceDiscovery>().StartDiscoverDNSSDService(type);
                }
                else
                {
                    // Enable input
                    inputView.IsEnabled = true;
                    // Change text of button
                    discoveryButton.Text = "Start to discover";

                    // Remove event handler
                    DependencyService.Get<IServiceDiscovery>().DNSSDServiceFound -= OnServiceFound;

                    // Stop discovering DNS-SD services
                    DependencyService.Get<IServiceDiscovery>().StopDiscoverDNSSDService();

                    // Check found services
                    CheckFoundServices();
                }
            }
            catch (Exception ex)
            {
                // Enable input
                inputView.IsEnabled = true;
                discoveryButton.IsEnabled = true;
                DisplayAlert("Unexpected Error", ex.Message.ToString(), "OK");
            }
        }

        /// <summary>
        /// Event handler when a service is found
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void OnServiceFound(object sender, DNSSDDiscoveryEventArgs e)
        {
            // Create a new service instance
            DNSSDService service = new DNSSDService(e.name, e.type, e.port, e.ipv4, e.ipv6);
            AddService(service);
        }

        /// <summary>
        /// Add a found service.
        /// If it is new one, add it to the map for services
        /// </summary>
        /// <param name="service">A found service</param>
        private void AddService(DNSSDService service)
        {
            if (!serviceMap.ContainsKey(service.name))
            {
                AddServiceToResult(service);
            }
        }

        /// <summary>
        /// Show a new found service in result list
        /// </summary>
        /// <param name="service">A new found service</param>
        private void AddServiceToResult(DNSSDService service)
        {
            TableSection section = AddServiceTableSection(service);
            resultView.Root.Add(section);
        }

        /// <summary>
        /// Create a new TableSection for a new found service
        /// </summary>
        /// <param name="service">A new found service</param>
        /// <returns>TableSection</returns>
        private TableSection AddServiceTableSection(DNSSDService service)
        {
            // Service information is added as TableSection
            // IP address and Port number are shown
            return new TableSection(service.name)
            {
                CreateTextCell("IPv4 Address", service.ipv4.ToString()),
                CreateTextCell("IPv6 Address", service.ipv6.ToString()),
                CreateTextCell("Port Number", service.port.ToString()),
            };
        }

        /// <summary>
        /// Check if a found service exists
        /// </summary>
        private void CheckFoundServices()
        {
            // if the number of found services is zero
            if (serviceMap.Count == 0)
            {
                // Show No Service Found message
                TableSection section = new TableSection("No Service Found")
                {
                };
                resultView.Root.Add(section);
            }
        }
    }

    internal class DNSSDService
    {
        internal string name;
        internal string type;
        internal int port;
        internal System.Net.IPAddress ipv4;
        internal System.Net.IPAddress ipv6;
        internal DNSSDService(string name, string type, int port, System.Net.IPAddress ipv4, System.Net.IPAddress ipv6)
        {
            this.name = name;
            this.type = type;
            this.port = port;
            this.ipv4 = ipv4;
            this.ipv6 = ipv6;
        }
    }
}
