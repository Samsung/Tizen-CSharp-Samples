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

using Tizen.Network.Connection;

namespace NetworkApp.Models
{
    /// <summary>
    /// ConnectionInfo class for getting prepared 
    /// strings with connection info from API
    /// </summary>
    class ConnectionInfo
    {
        /// <summary>
        /// Gets info about current connection
        /// </summary>
        /// <returns>String with current connection type and status</returns>
        public string GetCurrentConnection()
        {
            return "Type: " + ConnectionManager.CurrentConnection.Type.ToString() + "\n" +
            "Status: " + ConnectionManager.CurrentConnection.State.ToString();
        }

        /// <summary>
        /// Gets info about Wi-Fi state
        /// </summary>
        /// <returns>String with Wi-Fi state</returns>
        public string GetWifiState()
        {
            return ConnectionManager.WiFiState.ToString();
        }

        /// <summary>
        /// Gets info about Cellular state
        /// </summary>
        /// <returns>String with cellular state</returns>
        public string GetCellularState()
        {
            return ConnectionManager.CellularState.ToString();
        }

        /// <summary>
        /// Gets info about IPv4 and IPv6 device addresses
        /// </summary>
        /// <returns>String with IP addresses</returns>
        public string GetIPAddresses()
        {
            return "IPv4 Address: " + ConnectionManager.GetIPAddress(AddressFamily.IPv4).ToString() + "\n" +
            "IPv6 Address: " + ConnectionManager.GetIPAddress(AddressFamily.IPv6).ToString();
        }
        
        /// <summary>
        /// Gets info about Wi-Fi MAC address
        /// </summary>
        /// <returns>String with Wi-Fi MAC Address</returns>
        public string GetWifiMacAddress()
        {
            return ConnectionManager.GetMacAddress(ConnectionType.WiFi).ToString();
        }

        /// <summary>
        /// Gets info about Proxy address
        /// </summary>
        /// <returns>String with proxy address</returns>
        public string GetProxyAddress()
        {
            return ConnectionManager.GetProxy(AddressFamily.IPv4).ToString();
        }
    }
}
