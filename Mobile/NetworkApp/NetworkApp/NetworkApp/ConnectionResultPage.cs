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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NetworkApp
{
    /// <summary>
    /// Enumeration for ConnectionOperation
    /// </summary>
    enum ConnectionOperation
    {
        CURRENT,
        WIFISTATE,
        CELLULARSTATE,
        IPADDRESS,
        WIFIMACADDRESS,
        PROXYADDRESS,
        PROFILELIST,
    }

    /// <summary>
    /// The ContentPage to show the result of operation tabbed on ListView of ConnectionPage
    /// </summary>
    class ConnectionResultPage : ContentPage
    {
        Editor result;

        ConnectionOperation operation;
        IConnection connection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="op">ConnectionOperation</param>
        public ConnectionResultPage(ConnectionOperation op)
        {
            result = CreateEditor();

            // Create a Layout
            Content = new StackLayout
            {
                Children =
                {
                   result,
                }
            };

            connection = DependencyService.Get<IConnection>();
            operation = op;
            Operate(op);
        }

        /// <summary>
        /// Create an Editor instance 
        /// </summary>
        /// <returns>Editor</returns>
        private Editor CreateEditor()
        {
            return new Editor()
            {
                BackgroundColor = Color.White,
                FontSize = 20,
                IsEnabled = false,
            };
        }

        /// <summary>
        /// Event handler when button is clicked
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        public void OnClicked(object sender, EventArgs e)
        {
            Operate(operation);
        }

        /// <summary>
        /// Operate for each requested ConnectionOperation
        /// </summary>
        /// <param name="op">ConnectionOperation</param>
        public void Operate(ConnectionOperation op)
        {
            try
            {
                switch (op)
                {
					// Show the current connected network
                    case ConnectionOperation.CURRENT:
                        CurrentConnection();
                        break;
					// Show the current WiFi State
                    case ConnectionOperation.WIFISTATE:
                        WiFiState();
                        break;
                    // Show the current Cellular state
					case ConnectionOperation.CELLULARSTATE:
                        CellularState();
                        break;
                    // Show the IPv4 address and the IPv6 address of the current connection
					case ConnectionOperation.IPADDRESS:
                        IPAddress();
                        break;
                    // Show the MAC address of WiFi
					case ConnectionOperation.WIFIMACADDRESS:
                        WiFiMACAddress();
                        break;
                    // Show the proxy address
					case ConnectionOperation.PROXYADDRESS:
                        ProxyAddress();
                        break;
                    // Show the connection profiles
					case ConnectionOperation.PROFILELIST:
                        ProfileList();
                        break;
                }
            }
            catch (Exception e)
            {
                result.Text = e.ToString();
            }
        }

        /// <summary>
        /// Show the current connected network
        /// </summary>
        private void CurrentConnection()
        {
            // Update Current operation state
            result.Text = "Current Connection";
            // Network type
            result.Text += "\nType: " + connection.CurrentType;
            // Network state
            result.Text += "\nState: " + connection.CurrentState;

        }

        /// <summary>
        /// Show the current WiFi State
        /// </summary>
        private void WiFiState()
        {
            // Update Current operation state
            result.Text = "WiFi State: " + connection.WiFiState;
        }

        /// <summary>
        /// Show the current Cellular state
        /// </summary>
        private void CellularState()
        {
            // Update Current operation state
            result.Text = "Cellular State: " + connection.CellularState;
        }


        /// <summary>
        /// Show the IPv4 address and the IPv6 address of the current connection
        /// </summary>
        private void IPAddress()
        {
            // Update Current operation state
            // IPv4 address
            result.Text = "IPv4 Address: " + connection.IPv4Address;
            // IPv6 address
            result.Text += "\nIPv6 Address: " + connection.IPv6Address;
        }

        /// <summary>
        /// Show the MAC address of WiFi
        /// </summary>
        private void WiFiMACAddress()
        {
            // Update Current operation state
            result.Text = "WiFi MAC Address: " + connection.WiFiMACAddress;
        }

        /// <summary>
        /// Show the proxy address
        /// </summary>
        private void ProxyAddress()
        {
            // Update Current operation state
            result.Text = "Proxy Address: " + connection.ProxyAddress;
        }

        /// <summary>
        /// Show the connection profiles
        /// </summary>
        private async void ProfileList()
        {
            // Update Current operation state
            result.Text = "Get profile list";
            // Get a list of connectio profiles
            List<String> list = await connection.ProfileList();
            // Update profileListView
            ListView profileListView = new ListView()
            {
                ItemsSource = list,
            };
            // Add profileListView
            ((StackLayout)Content).Children.Add(profileListView);
        }
    }
}
