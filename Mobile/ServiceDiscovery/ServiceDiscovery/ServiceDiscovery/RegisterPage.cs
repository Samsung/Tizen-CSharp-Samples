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
using Xamarin.Forms;

namespace ServiceDiscovery
{
    /// <summary>
    /// ContentPage to operate DNS-SD Service Registration
    /// </summary>
    class RegisterPage : ContentPage
    {
        // Components
        /// <summary>
        /// Entry to input the type of DNS-SD Service
        /// </summary>
        private EntryCell typeEntryCell;
        /// <summary>
        /// Entry to input the name of DNS-SD Service
        /// </summary>
        private EntryCell nameEntryCell;
        /// <summary>
        /// Entry to input the port number of DNS-SD Service
        /// </summary>
        private EntryCell portEntryCell;
        /// <summary>
        /// Button to register the service
        /// </summary>
        private Button registerButton;

        /// <summary>
        /// Constructor
        /// </summary>
        public RegisterPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize Page
        /// Add components and events
        /// </summary>
        private void InitializeComponent()
        {
            Title = "DNS-SD Service Registration";

            // Create components
            typeEntryCell = CreateEntryCell("Service Type", "Enter service type. e.g. _http._tcp");
            nameEntryCell = CreateEntryCell("Service Name", "Enter service name");
            portEntryCell = CreateEntryCell("Port Number", "Enter port number");
            portEntryCell.Keyboard = Keyboard.Numeric;
            registerButton = CreateButton("Register");

            // Add components
            Content = new StackLayout
            {
                Children =
                {
                    CreateTableView(),
                    registerButton,
                }
            };

            // Add events
            registerButton.Clicked += OnClicked;
        }

        /// <summary>
        /// Create a TableView.
        /// It contains EntryCells.
        /// </summary>
        /// <returns>TableView</returns>
        private TableView CreateTableView()
        {
            return new TableView()
            {
                HeightRequest = 400,
                Intent = TableIntent.Form,
                Margin = new Thickness(10, 10),
                VerticalOptions = LayoutOptions.Start,
                Root = new TableRoot
                {
                    // TableView contains EntryCells
                    new TableSection
                    {
                        typeEntryCell,
                        nameEntryCell,
                        portEntryCell,
                    },
                }
            };
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
        /// Create a Button instance
        /// </summary>
        /// <param name="text">Label of Button</param>
        /// <returns>Button</returns>
        private Button CreateButton(string text)
        {
            return new Button()
            {
                Text = text,
            };
        }

        /// <summary>
        /// Event handler when button is clicked
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void OnClicked(object sender, EventArgs e)
        {
            string type = typeEntryCell.Text;
            string name = nameEntryCell.Text;
            int port = 0;

            try
            {
                // convert string to integer
                port = Int32.Parse(portEntryCell.Text);
                // check port number
                // port number should be an integer value between 0 and 65535
                if (port < 0 || port > 65535)
                {
                        DisplayAlert("Alert", "Invalid Port Number", "OK");
                        return;
                    }
            }
            catch (FormatException)
            {
                DisplayAlert("Alert", "Invalid Port Number", "OK");
                return;
            }

            try
            {
                // Actually register DNS-SD service
                DependencyService.Get<IServiceDiscovery>().RegisterDNSSDService(type, name, port);
            }
            catch (Exception)
            {
                // Service type is a form _<service protocol>._<transport protocol>
                // Transport protocol must be tcp or udp
                string usage = "The type must be a form of _<protocol>._tcp or _<protocol>._udp.\n"
                    + "protocol must consist of alphabets, numbers and hyphens.";
                DisplayAlert("Invalid Service Type", usage, "OK");
                return;
            }

            registerButton.IsEnabled = false;
            DisplayAlert("Success", "Service is Registered", "OK");
        }
    }
}
