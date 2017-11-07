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

using NetworkApp.Tizen.Mobile;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen.Network.Connection;
using Tizen.Network.WiFi;


[assembly: Xamarin.Forms.Dependency(typeof(ConnectionImplementation))]

namespace NetworkApp.Tizen.Mobile
{
    /// <summary>
    /// Implementation class of IConnection interface
    /// </summary>
    class ConnectionImplementation : IConnection
    {
        /// List that contains connection profiles
        IEnumerable<ConnectionProfile> profileList;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionImplementation()
        {
        }

        /// <summary>
        /// The type of the current profile for data connection (Disconnected, Wi-Fi, Cellular, etc.)
        /// </summary>
        public String CurrentType
        {
            get
            {
                // Call Tizen C# API
                return ConnectionManager.CurrentConnection.Type.ToString();
            }
        }

        /// <summary>
        /// The state of the current profile for data connection
        /// </summary>
        public String CurrentState
        {
            get
            {
                // Call Tizen C# API
                return ConnectionManager.CurrentConnection.State.ToString();
            }
        }

        /// <summary>
        /// The state of the Wi-Fi connection
        /// </summary>
        public String WiFiState
        {
            get
            {
                // Call Tizen C# API
                return ConnectionManager.WiFiState.ToString();
            }
        }

        /// <summary>
        /// The state of the Cellular connection
        /// </summary>
        public String CellularState
        {
            get
            {
                // Call Tizen C# API
                return ConnectionManager.CellularState.ToString();
            }
        }

        /// <summary>
        /// The IPv4 address of the current connection
        /// </summary>
        public String IPv4Address
        {
            get
            {
                // Call Tizen C# API
                return ConnectionManager.GetIPAddress(AddressFamily.IPv4).ToString();
            }
        }

        /// <summary>
        /// The IPv6 address of the current connection
        /// </summary>
        public String IPv6Address
        {
            get
            {
                // Call Tizen C# API
                return ConnectionManager.GetIPAddress(AddressFamily.IPv6).ToString();
            }
        }

        /// <summary>
        /// The MAC address of the Wi-Fi
        /// </summary>
        public String WiFiMACAddress
        {
            get
            {
                // Call Tizen C# API
                return ConnectionManager.GetMacAddress(ConnectionType.WiFi).ToString();
            }
        }

        /// <summary>
        /// The proxy address of the current connection
        /// </summary>
        public String ProxyAddress
        {
            get
            {
                // Call Tizen C# API
                return ConnectionManager.GetProxy(AddressFamily.IPv4).ToString();
            }
        }

        /// <summary>
        /// Get profile list as a list of profile name
        /// </summary>
        /// <returns>Task to call GetProfileList</returns>
        public Task<List<String>> ProfileList()
        {
            var task = new TaskCompletionSource<List<String>>();
            List<String> result = new List<string>();
            // Get profile list
            GetProfileList();
            foreach (var item in profileList)
            {
                // Add name of connection profiles to a list
                result.Add(item.Name);
            }

            task.SetResult(result);
            return task.Task;
        }

        /// <summary>
        /// Gets the list of the profile
        /// </summary>
        private async void GetProfileList()
        {
            // Call Tizen C# API
            profileList = await ConnectionProfileManager.GetProfileListAsync(ProfileListType.Registered);
        }
    }
}
