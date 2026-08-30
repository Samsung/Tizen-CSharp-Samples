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
using System.Threading;
using System.Threading.Tasks;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Binding;

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
    /// The ContentPage to show the result of operation tabbed on CollectionView of ConnectionPage
    /// </summary>
    class ConnectionResultPage : ContentPage
    {
        private TextLabel resultLabel;
        private ScrollableBase profileScrollableList; // For ProfileList operation
        private View profileListContentContainer; // Container for profile items

        private ConnectionOperation operation;
        private TizenConnectionService connectionService; // Added TizenConnectionService field

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="op">ConnectionOperation</param>
        public ConnectionResultPage(ConnectionOperation op)
        {
            AppBar = new AppBar { Title = $"Connection Result: {op}" }; // Dynamic title based on operation
            operation = op;
            connectionService = new TizenConnectionService(); // Initialize TizenConnectionService

            var mainLayoutContainer = Resources.CreateMainLayoutContainer();
            // Use a slightly different padding for this page if needed, or default
            Content = mainLayoutContainer;

            resultLabel = Resources.CreateBodyLabel(""); // Start with empty text
            resultLabel.BackgroundColor = Color.Transparent; // Make background transparent
            resultLabel.MultiLine = true; // Allow multiple lines
            mainLayoutContainer.Add(resultLabel);

            profileListContentContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = new Size2D(0, 8) // Spacing for profile items
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            profileScrollableList = Resources.CreateScrollableList();
            profileScrollableList.HeightSpecification = 0; // Initially hidden
            profileScrollableList.Add(profileListContentContainer);
            mainLayoutContainer.Add(profileScrollableList);

            Operate(op);
        }


        /// <summary>
        /// Event handler when a refresh button is clicked (if added later)
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
                // Clear previous results
                resultLabel.Text = "";
                profileScrollableList.HeightSpecification = 0; // Hide by setting height to 0
                // Clear previous profile items from the container
                while (profileListContentContainer.ChildCount > 0)
                {
                    profileListContentContainer.Remove(profileListContentContainer.GetChildAt(0));
                }

                switch (op)
                {
                    // Show the current connected network
                    case ConnectionOperation.CURRENT:
                        CurrentConnection();
                        break;
                    // Show the current Wi-Fi State
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
                    // Show the MAC address of Wi-Fi
                    case ConnectionOperation.WIFIMACADDRESS:
                        WiFiMACAddress();
                        break;
                    // Show the proxy address
                    case ConnectionOperation.PROXYADDRESS:
                        ProxyAddress();
                        break;
                    // Show the connection profiles
                    case ConnectionOperation.PROFILELIST:
                        // ProfileList is async, so it needs to be awaited
                        _ = ProfileList(); // Fire and forget, or properly await if UI needs to wait
                        break;
                }
            }
            // C# API throws NotSupportedException if the API is not supported
            catch (NotSupportedException)
            {
                resultLabel.Text = "The operation is not supported on this device";
                Tizen.Log.Error("Xaml2NUI", "ConnectionResultPage: NotSupportedException");
            }
            catch (Exception ex)
            {
                resultLabel.Text = ex.ToString();
                Tizen.Log.Error("Xaml2NUI", $"ConnectionResultPage: Exception - {ex.Message}");
            }
        }

        /// <summary>
        /// Show the current connected network
        /// </summary>
        private void CurrentConnection()
        {
            try
            {
                string currentType = connectionService.CurrentType;
                string currentState = connectionService.CurrentState;
                // Check if the type or state indicates no connection
                if (string.IsNullOrEmpty(currentType) || currentType.Equals("None", StringComparison.OrdinalIgnoreCase) ||
                    currentState.Equals("Disconnected", StringComparison.OrdinalIgnoreCase))
                {
                    resultLabel.Text = "No active network connection.";
                }
                else
                {
                    resultLabel.Text = $"Current Connection\nType: {currentType}\nState: {currentState}";
                }
                Tizen.Log.Info("Xaml2NUI", "ConnectionResultPage: CurrentConnection retrieved.");
            }
            catch (Exception ex)
            {
                // Check for a more specific exception if the service throws one when not connected
                // For now, a general exception is caught and assumed to mean 'not connected' or an error.
                resultLabel.Text = "No active network connection or an error occurred.";
                Tizen.Log.Error("Xaml2NUI", $"ConnectionResultPage: CurrentConnection error - {ex.ToString()}");
            }
        }

        /// <summary>
        /// Show the current Wi-Fi State
        /// </summary>
        private void WiFiState()
        {
            try
            {
                string wifiState = connectionService.WiFiState;
                if (wifiState.Equals("Deactivated", StringComparison.OrdinalIgnoreCase) || 
                    wifiState.Equals("Disconnected", StringComparison.OrdinalIgnoreCase) ||
                    wifiState.Equals("Not Available", StringComparison.OrdinalIgnoreCase))
                {
                    resultLabel.Text = "Wi-Fi is not connected or active.";
                }
                else
                {
                    resultLabel.Text = $"Wi-Fi State: {wifiState}";
                }
                Tizen.Log.Info("Xaml2NUI", "ConnectionResultPage: WiFiState retrieved.");
            }
            catch (Exception ex)
            {
                resultLabel.Text = "Wi-Fi state is not available or an error occurred.";
                Tizen.Log.Error("Xaml2NUI", $"ConnectionResultPage: WiFiState error - {ex.ToString()}");
            }
        }

        /// <summary>
        /// Show the current Cellular state
        /// </summary>
        private void CellularState()
        {
            try
            {
                string cellularState = connectionService.CellularState;
                Tizen.Log.Info("Xaml2NUI", $"ConnectionResultPage: Raw cellularState value: '{cellularState}'"); // Log raw value

                // Trim whitespace for comparison
                var trimmedCellularState = cellularState?.Trim();

                if (string.IsNullOrEmpty(trimmedCellularState) ||
                    trimmedCellularState.Equals("Error", StringComparison.OrdinalIgnoreCase) || // Specific check for "Error" string
                    trimmedCellularState.Equals("Deactivated", StringComparison.OrdinalIgnoreCase) ||
                    trimmedCellularState.Equals("Disconnected", StringComparison.OrdinalIgnoreCase) ||
                    trimmedCellularState.Equals("Not Available", StringComparison.OrdinalIgnoreCase) ||
                    trimmedCellularState.Equals("OutOfService", StringComparison.OrdinalIgnoreCase)) // Common for cellular
                {
                    resultLabel.Text = "Cellular is not connected or active.";
                    Tizen.Log.Info("Xaml2NUI", "ConnectionResultPage: Cellular state interpreted as 'not connected'.");
                }
                else
                {
                    resultLabel.Text = $"Cellular State: {cellularState}"; // Display original, non-trimmed value if not an error state
                    Tizen.Log.Info("Xaml2NUI", "ConnectionResultPage: Cellular state displayed as is.");
                }
            }
            catch (Exception ex)
            {
                // Log the detailed exception message to see if it contains "Error"
                string exceptionMessage = ex.Message;
                Tizen.Log.Error("Xaml2NUI", $"ConnectionResultPage: CellularState exception caught. Message: '{exceptionMessage}', StackTrace: {ex.ToString()}");
                
                // Check if the generic exception message itself is what's being shown
                if (exceptionMessage.Contains("Error", StringComparison.OrdinalIgnoreCase))
                {
                    resultLabel.Text = "Cellular is not connected or active (service error).";
                }
                else
                {
                    resultLabel.Text = "Cellular state is not available or an error occurred.";
                }
            }
        }


        /// <summary>
        /// Show the IPv4 address and the IPv6 address of the current connection
        /// </summary>
        private void IPAddress()
        {
            try
            {
                string ipv4Address = connectionService.IPv4Address;
                string ipv6Address = connectionService.IPv6Address;
                resultLabel.Text = $"IPv4 Address: {ipv4Address}\nIPv6 Address: {ipv6Address}";
                Tizen.Log.Info("Xaml2NUI", "ConnectionResultPage: IPAddress retrieved.");
            }
            catch (Exception ex)
            {
                resultLabel.Text = $"Error getting IP address: {ex.Message}";
                Tizen.Log.Error("Xaml2NUI", $"ConnectionResultPage: IPAddress error - {ex.ToString()}");
            }
        }

        /// <summary>
        /// Show the MAC address of Wi-Fi
        /// </summary>
        private void WiFiMACAddress()
        {
            try
            {
                string wifiMACAddress = connectionService.WiFiMACAddress;
                resultLabel.Text = $"Wi-Fi MAC Address: {wifiMACAddress}";
                Tizen.Log.Info("Xaml2NUI", "ConnectionResultPage: WiFiMACAddress retrieved.");
            }
            catch (Exception ex)
            {
                resultLabel.Text = $"Error getting Wi-Fi MAC address: {ex.Message}";
                Tizen.Log.Error("Xaml2NUI", $"ConnectionResultPage: WiFiMACAddress error - {ex.ToString()}");
            }
        }

        /// <summary>
        /// Show the proxy address
        /// </summary>
        private void ProxyAddress()
        {
            try
            {
                string proxyAddress = connectionService.ProxyAddress;
                resultLabel.Text = $"Proxy Address: {proxyAddress}";
                Tizen.Log.Info("Xaml2NUI", "ConnectionResultPage: ProxyAddress retrieved.");
            }
            catch (Exception ex)
            {
                resultLabel.Text = $"Error getting proxy address: {ex.Message}";
                Tizen.Log.Error("Xaml2NUI", $"ConnectionResultPage: ProxyAddress error - {ex.ToString()}");
            }
        }

        /// <summary>
        /// Show the connection profiles
        /// </summary>
        private async Task ProfileList()
        {
            try
            {
                resultLabel.Text = "Getting profile list...";
                Tizen.Log.Info("Xaml2NUI", "ConnectionResultPage: ProfileList fetching.");

                List<String> list = await connectionService.GetProfileListAsync();

                // Update profileListView on the UI thread
                var uiTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
                if (uiTaskScheduler != null)
                {
                    await Task.Factory.StartNew(() =>
                    {
                        if (list != null && list.Count > 0 && !(list.Count == 1 && list[0] == "Error"))
                        {
                            foreach (var profileName in list)
                            {
                                var profileItemContainer = Resources.CreateListItemContainer();
                                profileItemContainer.Padding = AppStyles.ListElementPadding;

                                var profileLabel = Resources.CreateBodyLabel(profileName);
                                
                                profileItemContainer.Add(profileLabel);
                                profileListContentContainer.Add(profileItemContainer);
                            }
                            profileScrollableList.HeightSpecification = LayoutParamPolicies.WrapContent; // Show the list
                            resultLabel.Text = $"Found {list.Count} profiles:"; // Update label text
                        }
                        else
                        {
                            resultLabel.Text = "No profiles found or error occurred.";
                            profileScrollableList.HeightSpecification = 0; // Ensure it's hidden
                        }
                    }, CancellationToken.None, TaskCreationOptions.None, uiTaskScheduler);
                }
                else
                {
                    Tizen.Log.Warn("Xaml2NUI", "ProfileList: No SynchronizationContext found, UI update might be unsafe.");
                    if (list != null && list.Count > 0 && !(list.Count == 1 && list[0] == "Error"))
                    {
                        foreach (var profileName in list)
                        {
                             var profileItemContainer = Resources.CreateListItemContainer();
                             profileItemContainer.Padding = AppStyles.ListElementPadding;

                             var profileLabel = Resources.CreateBodyLabel(profileName);

                             profileItemContainer.Add(profileLabel);
                             profileListContentContainer.Add(profileItemContainer);
                        }
                        profileScrollableList.HeightSpecification = LayoutParamPolicies.WrapContent; // Show the list
                        resultLabel.Text = $"Found {list.Count} profiles:"; // Update label text
                    }
                    else
                    {
                        resultLabel.Text = "No profiles found or error occurred.";
                        profileScrollableList.HeightSpecification = 0; // Ensure it's hidden
                    }
                }
            }
            catch (Exception ex)
            {
                resultLabel.Text = $"Error getting profile list: {ex.Message}";
                Tizen.Log.Error("Xaml2NUI", $"ConnectionResultPage: ProfileList error - {ex.ToString()}");
            }
        }

    }
}
