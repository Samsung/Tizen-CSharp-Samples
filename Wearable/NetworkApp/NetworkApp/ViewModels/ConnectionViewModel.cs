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

namespace NetworkApp.Views
{
    /// <summary>
    /// ConnectionViewModel class
    /// </summary>
    class ConnectionViewModel : ViewModels.ViewModelBase
    {
        /// <summary>
        /// Holds the method which returns connection info.
        /// </summary>
        private ConnectionInfo _info = new ConnectionInfo();

        /// <summary>
        /// Holds current connection status
        /// </summary>
        public string CurrentConnectionLabel => 
            Format("Current Connection", _info.GetCurrentConnection);

        /// <summary>
        /// Holds Wi-Fi status
        /// </summary>
        public string WifiStateLabel =>
            Format("Wi-Fi state", _info.GetWifiState);

        /// <summary>
        /// Holds Cellular status
        /// </summary>
        public string CellularStateLabel =>
            Format("Cellular state", _info.GetCellularState);

        /// <summary>
        /// Holds IPv4 and IPv6 addresses
        /// </summary>
        public string IPAddressLabel =>
            Format("IP address", _info.GetIPAddresses);

        /// <summary>
        /// Holds Wi-Fi MAC address
        /// </summary>
        public string WifiMacAddressLabel =>
            Format("Wi-Fi Mac address", _info.GetWifiMacAddress);

        /// <summary>
        /// Holds Proxy address
        /// </summary>
        public string ProxyAddressLabel =>
            Format("Proxy address", _info.GetProxyAddress);

        /// <summary>
        /// Helper method to properly format labels from ConnectionInfo for 
        /// showing on screen.
        /// </summary>
        /// <param name="title">Title label</param>
        /// <param name="function">Method which returns expected data to show</param>
        /// <returns>Returns string with title with required info in new line</returns>
        private string Format(string title, Func<string> function)
        {
            string titleLabel = title + ": \n";

            try
            {
                return titleLabel + function.Invoke();
            }
            catch (NotSupportedException e)
            {
                Logger.Log(e.Message);
                return titleLabel + "Not supported";
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
                return titleLabel + e.Message;
            }
        }
    }
}