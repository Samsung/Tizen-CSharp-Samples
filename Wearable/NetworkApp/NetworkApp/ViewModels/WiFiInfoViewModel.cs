//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using NetworkApp.Models;
using System;
using Xamarin.Forms;

namespace NetworkApp.ViewModels
{
    class WiFiInfoViewModel : ViewModelBase
    {
        /// <summary>
        /// INavigation instance to allow push new pages from a ViewModel
        /// </summary>
        private INavigation _navigation;

        /// <summary>
        /// Instance of WiFiApiManager
        /// </summary>
        private readonly WiFiApiManager _wifi = new WiFiApiManager();

        /// <summary>
        /// String with currently connected network name
        /// </summary>
        private string _connectedAP;

        /// <summary>
        /// Initializes a new instance of the <see cref="WiFiInfoViewModel"/> class
        /// </summary>
        /// <param name="navigation">Navigation instance</param>
        public WiFiInfoViewModel(INavigation navigation)
        {
            _wifi = new WiFiApiManager();
            _navigation = navigation;

            try
            {
                _wifi.GetConnectedAP();
            }
            catch (NotSupportedException)
            {
                _connectedAP = null;
            }

            RefreshBindings();
        }

        /// <summary>
        /// Gets or sets the text with info about currently connected AP
        /// </summary>
        public string CurrentAP
        {
            get
            {
                if (_connectedAP == null)
                {
                    return "Disconnected";
                }
                else
                {
                    return "Connected: " + _connectedAP;
                }
            }

            set
            {
                SetProperty(ref _connectedAP, value);
            }
        }

        /// <summary>
        /// Gets the text with Connection State of Wi-Fi
        /// </summary>
        public string ConnectionState
        {
            get
            {
                if (_connectedAP == null)
                {
                    return "";
                }
                else
                {
                    return "State: " + _wifi.GetConnectionState();
                }
            }
        }

        /// <summary>
        /// Gets the text with Mac Address of Wi-Fi interface
        /// </summary>
        public string MacAddress
        {
            get
            {
                if (_connectedAP == null)
                {
                    return "";
                }
                else
                {
                    return "MAC Address: " + _wifi.GetMacAddress();
                }
            }
        }

        /// <summary>
        /// Refreshes bindings on a page
        /// </summary>
        public void RefreshBindings()
        {
            try
            {
                _connectedAP = _wifi.ConnectedAP();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
            }

            OnPropertyChanged(nameof(CurrentAP));
        }

        /// <summary>
        /// Gets the text with IP Address about currently connected AP
        /// </summary>
        public string IPAddress
        {
            get
            {
                return _wifi.GetIPAddress();
            }
        }

        /// <summary>
        /// Gets the text with Subnet Mask about currently connected AP
        /// </summary>
        public string SubnetMask
        {
            get
            {
                return _wifi.GetSubnetMask();
            }
        }

        /// <summary>
        /// Gets the text with DNS Address about currently connected AP
        /// </summary>
        public string DNSAddress
        {
            get
            {
                return _wifi.GetDNSAddress();
            }
        }

        /// <summary>
        /// Gets the text with Gateway Address about currently connected AP
        /// </summary>
        public string GatewayAddress
        {
            get
            {
                return _wifi.GetGatewayAddress();
            }
        }

        /// <summary>
        /// Gets the text with ESSID about currently connected AP
        /// </summary>
        public string ESSID
        {
            get
            {
                return _wifi.GetEssid();
            }
        }

        /// <summary>
        /// Gets the text with BSSID about currently connected AP
        /// </summary>
        public string BSSID
        {
            get
            {
                return _wifi.GetBssid();
            }
        }

        /// <summary>
        /// Gets the text with Max Speed about currently connected AP
        /// </summary>
        public string MaxSpeed
        {
            get
            {
                return _wifi.GetMaxSpeed();
            }
        }

        /// <summary>
        /// Gets the text with RSSI about currently connected AP
        /// </summary>
        public string RSSI
        {
            get
            {
                return _wifi.GetRssi();
            }
        }
    }
}
