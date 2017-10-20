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
    /// Enumeration for WiFiOperation
    /// </summary>
    enum WiFiOperation
    {
        ACTIVATE,
        DEACTIVATE,
        SCAN,
        CONNECT,
        DISCONNECT,
        FORGET,
    }

    /// <summary>
    /// The ContentPage to show the result of operation tabbed on ListView of WiFIPage
    /// </summary>
    class WiFiResultPage : ContentPage
    {
        Label result;
        Entry apEntry;
        Entry passwordEntry;
        ListView scanListView;

        WiFiOperation operation;
        IWiFi wifi = DependencyService.Get<IWiFi>();
        ILog log = DependencyService.Get<ILog>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="op">WiFiOperation</param>
        public WiFiResultPage(WiFiOperation op)
        {
            // Dlog message
            log.Log("WiFiOpPage " + op);
            operation = op;
            CreateLayout();
        }

        /// <summary>
        /// Create a Layout for each WiFiOperation
        /// </summary>
        private void CreateLayout()
        {
            // Dlog message
            log.Log("CreateLayout");
            switch (operation)
            {
                // Tizen.Network.WiFi.WiFiAP.ConnectAsync()
                case WiFiOperation.CONNECT:
                    // Create a Layout
                    CreateConnectView();
                    break;
                // Tizen.Network.WiFi.WiFiManager.ScanAsync()
                case WiFiOperation.SCAN:
                    // Create a Layout
                    CreateScanResultView();
                    break;
                // Tizen.Network.WiFi.WiFiAP.DisconnectAsync()
                case WiFiOperation.DISCONNECT:
                // Tizen.Network.WiFi.WiFiAP.Forget()
                case WiFiOperation.FORGET:
                    // Create a Layout
                    CreateDisconnectView();                    
                    break;
                // Tizen.Network.WiFi.WiFiManager.ActivateAsync()
                case WiFiOperation.ACTIVATE:
                // Tizen.Network.WiFi.WiFiManager.DeactivateAsync()
                case WiFiOperation.DEACTIVATE:
                default:
                    // Create a Layout
                    CreateActivateView();
                    Operate();
                    break;
            }
        }

        /// <summary>
        /// Create a Layout for ACTIVATE and DEACTIVATE
        /// </summary>
        private void CreateActivateView()
        {
            result = CreateLabel("");
            result.IsEnabled = false;

            // Create a Layout
            Content = new StackLayout
            {
                Children =
                    {
                        result,
                    }
            };
        }

        /// <summary>
        /// Create a Layout for CONNECT
        /// </summary>
        private void CreateConnectView()
        {
            apEntry = CreateEntry("AP");
            passwordEntry = CreateEntry("Password");
            Button connectButton = CreateButton("Connect");
            connectButton.Clicked += OnClicked;
            result = CreateLabel("Select an AP to connect");
            result.IsEnabled = false;
            scanListView = new ListView();

            // Show a list of WiFi AP
            ShowScanList();

            // Add event
            scanListView.ItemTapped += (s, a) =>
            {
                apEntry.Text = GetAPName(a.Item.ToString());
            };

            // Create a Layout
            Content = new StackLayout
            {
                Children =
                {
                    scanListView,
                    apEntry,
                    passwordEntry,
                    connectButton,
                    result,
                }
            };            
        }

        /// <summary>
        /// Create a Layout for SCAN
        /// </summary>
        private void CreateScanResultView()
        {
            result = CreateLabel("");
            result.IsEnabled = false;
            scanListView = new ListView();

            // Create a Layout
            Content = new StackLayout
            {
                Children =
                {
                    result,
                    scanListView,
                }
            };
            Scan();
            ShowScanList();
        }

        /// <summary>
        /// Create a Layout for DISCONNECT and FORGET
        /// </summary>
        private void CreateDisconnectView()
        {
            apEntry = CreateEntry("AP");
            apEntry.Text = ShowConnectedAP();
            Button disconnectButton = CreateButton("Disconnect");
            disconnectButton.Clicked += OnClicked;
            result = CreateLabel("Insert the ESSID of AP");
            result.IsEnabled = false;

            switch (operation)
            {
                case WiFiOperation.DISCONNECT:
                    break;
                case WiFiOperation.FORGET:
                    disconnectButton.Text = "Forget";
                    break;
            }

            // Create a Layout
            Content = new StackLayout
            {
                Children =
                {
                    apEntry,
                    disconnectButton,
                    result,
                }
            };
        }

        /// <summary>
        /// Create a Label instance
        /// </summary>
        /// <param name="str">Text of Label</param>
        /// <returns></returns>
        private Label CreateLabel(String str)
        {
            return new Label()
            {
                BackgroundColor = Color.White,
                FontSize = 20,
                Text = str,
            };
        }

        /// <summary>
        /// Create an Entry instance
        /// </summary>
        /// <param name="str">Placeholder of Entry</param>
        /// <returns>Entry</returns>
        private Entry CreateEntry(String str)
        {
            return new Entry()
            {   
                BackgroundColor = Color.White,
                FontSize = 20,
                Placeholder = str,
            };
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
            };
        }

        /// <summary>
        /// Create a Button instance
        /// </summary>
        /// <param name="text">Text of new Button</param>
        /// <returns>Button</returns>
        private Button CreateButton(String text)
        {
            return new Button()
            {
                Text = text,
            };
        }

        /// <summary>
        /// Show the WiFi AP list as a scan result. The list contains AP name and state
        /// </summary>
        private void ShowScanList()
        {
            // Request to get scan result
            ScanResult();
            var SourceList = new List<String>();
            // Get WiFi AP list as a scan result
            List<APInfo> apList = wifi.GetAPList();
            foreach (var item in apList)
            {
                // Show WiFi AP name and current state
                SourceList.Add(item.Name + " (" + item.State + ")");
            }
            
            // Set the ItemSource of ListView scanListView
            scanListView.ItemsSource = SourceList;
        }

        /// <summary>
        /// Get AP name from item (that contains AP name and state) of scanListView
        /// </summary>
        /// <param name="str">AP information that contains AP name and state</param>
        /// <returns>AP name</returns>
        private String GetAPName(String str)
        {
            char[] del = { '(' };
            return str.Split(del)[0].TrimEnd();
        }

        /// <summary>
        /// Event handler when button is clicked
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        public void OnClicked(object sender, EventArgs e)
        {
            Operate();
        }

        /// <summary>
        /// Operate for each requested WiFiOperation
        /// </summary>
        private void Operate()
        {
            // Dlog message
            log.Log("Operate");
            // Operate for each requested WiFiOperation
            try
            {
                switch (operation)
                {
					// Activate WiFi
                    case WiFiOperation.ACTIVATE:
                        Activate();
                        break;
					// Dectivate WiFi
                    case WiFiOperation.DEACTIVATE:
                        Deactivate();
                        break;
                    // Connet to the selected WiFi AP
					case WiFiOperation.CONNECT:
                        Connect();
                        break;
                    // Disconnect the selected WiFi AP
					case WiFiOperation.DISCONNECT:
                        Disconnect();
                        break;
                    // Forget the selected WiFi AP
					case WiFiOperation.FORGET:
                        Forget();
                        break;
                }
            }
            catch (Exception e)
            {
                log.Log(e.ToString());
                result.Text = e.ToString();
            }
        }

        /// <summary>
        /// Show the current connected WiFi AP
        /// </summary>
        /// <returns>ESSID of the connect WiFi AP</returns>
        private String ShowConnectedAP()
        {
            try
            {
                // Get the current connected WiFi AP
                return wifi.ConnectedAP();
            }
            catch (Exception e)
            {
                // Fail to get the current connected WiFi AP
                log.Log(e.ToString());
                return "No AP connected";
            }
        }

        /// <summary>
        /// Activate WiFi
        /// </summary>
        private async void Activate()
        {
            try
            {
                // Check if WiFi is activated
                if (wifi.IsActive())
                {
                    // Update Current operation state
                    result.Text = "Already activated";
                }
                else
                {
                    // Update Current operation state
                    result.Text = "Try to activate";
                    // Activate WiFi
                    await wifi.Activate();
                    // Update Current operation state
                    result.Text = "Activated";
                }
            }
            catch (Exception e)
            {
                // Fail to activate
                log.Log(e.ToString());
                // Update result to show the failure message
                result.Text = "Fail to activate";
            }
        }

        /// <summary>
        /// Deactivate WiFi
        /// </summary>
        private async void Deactivate()
        {
            try
            {
                // Check if WiFi is activated
                if (wifi.IsActive())
                {
                    // Update Current operation state
                    result.Text = "Try to deactivate";
                    // Deactivate WiFi
                    await wifi.Deactivate();
                    // Update Current operation state
                    result.Text = "Deactivated";
                }
                else
                {
                    // Update Current operation state
                    result.Text = "Already deactivated";
                }
            }
            catch (Exception e)
            {
                // Fail to deactivate
                log.Log(e.ToString());
                // Update result to show the failure message
                result.Text = "Fail to deactivate";
            }
}

        /// <summary>
        /// Scan
        /// </summary>
        private async void Scan()
        {
            try
            {
                // Update Current operation state
                result.Text = "Scanning";
                // Try to scan
                await wifi.Scan();
                // Update Current operation state
                result.Text = "Scan done";
            }
            catch (Exception e)
            {
                // Fail to scan
                log.Log(e.ToString());
                // Update result to show the failure message
                result.Text = "Fail to scan";
            }
        }

        /// <summary>
        /// Show the scan result
        /// </summary>
        private void ScanResult()
        {
            try
            {
                // Try to get scan result
                wifi.ScanResult();
            }
            catch (Exception e)
            {
                // Fail to get scan result
                log.Log(e.ToString());
                // Update result to show the failure message
                result.Text = "Fail to get AP list";
            }
        }

        /// <summary>
        /// Connect to the selected WiFi AP
        /// </summary>
        private async void Connect()
        {
            try
            {
                // Update Current operation state
                result.Text = "Try to connect to " + apEntry.Text;
                // Try to connect to the WiFi AP
                await wifi.Connect(apEntry.Text, passwordEntry.Text);
                // No exception. WiFi is connected.
                // Scan request
                Scan();
                // Get scan result
                ShowScanList();
                // Update Current operation state
                result.Text = "Connected";
            }
            catch (Exception e)
            {
                // Fail to connect
                log.Log(e.ToString());
                // Update result to show the failure message
                result.Text = "Fail to connect";
            }
        }

        /// <summary>
        /// Disconnect the selected WiFi AP
        /// </summary>
        private async void Disconnect()
        {
            try
            {
                // Update Current operation state
                result.Text = "Try to disconnect to " + apEntry.Text;
                // Try to disconnect the WiFi AP
                await wifi.Disconnect(apEntry.Text);
                // Update Current operation state
                result.Text = "Disconnected";
            }
            catch (Exception e)
            {
                // Fail to disconnect
                log.Log(e.ToString());
                // Update result to show the failure message
                result.Text = "Fail to disconnect";
            }
        }

        /// <summary>
        /// Forget the selected WiFi AP
        /// </summary>
        private void Forget()
        {
            try
            {
                // Try to forget the WiFi AP
                wifi.Forget(apEntry.Text);
                // Update Current operation state
                result.Text = "Forgotten";
            }
            catch (Exception e)
            {
                // Fail to forget
                log.Log(e.ToString());
                // Update result to show the failure message
                result.Text = "Fail to forget";
            }
        }
    }
}
