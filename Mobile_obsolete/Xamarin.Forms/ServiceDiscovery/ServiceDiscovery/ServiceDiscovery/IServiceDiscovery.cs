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

namespace ServiceDiscovery
{
    /// <summary>
    /// Interface to call Tizen.Network.Nsd
    /// </summary>
    public interface IServiceDiscovery
    {
        /// <summary>
        /// Register DNS-SD Service
        /// </summary>
        /// <param name="type">Service Type</param>
        /// <param name="name">Service Name</param>
        /// <param name="port">Port Number</param>
        void RegisterDNSSDService(string type, string name, int port);

        /// <summary>
        /// Start to discover DNS-SD Service
        /// It operates within local network
        /// </summary>
        /// <param name="type">Service type</param>
        void StartDiscoverDNSSDService(string type);

        /// <summary>
        /// Stop discovering DNS-SD Service
        /// </summary>
        void StopDiscoverDNSSDService();

        /// <summary>
        /// The event to deliver the Tizen.Network.Nsd ServiceFound event to application
        /// </summary>
        event EventHandler<DNSSDDiscoveryEventArgs> DNSSDServiceFound;
    }

    /// <summary>
    /// The EventArgs for the ServiceFound event
    /// </summary>
    public class DNSSDDiscoveryEventArgs : EventArgs
    {
        // Service Informations
        public string state;
        public string name;
        public string type;
        public int port;
        public System.Net.IPAddress ipv4;
        public System.Net.IPAddress ipv6;

        /// <summary>
        /// Event argument that contains the found service information
        /// </summary>
        /// <param name="state">Service state (Available or Unavailable)</param>
        /// <param name="name">Service name</param>
        /// <param name="type">Service type</param>
        /// <param name="port">Port number</param>
        /// <param name="ipv4">IPv4 address</param>
        /// <param name="ipv6">IPv6 address</param>
        public DNSSDDiscoveryEventArgs(string state, string name, string type, int port, System.Net.IPAddress ipv4, System.Net.IPAddress ipv6)
        {
            this.state = state;
            this.name = name;
            this.type = type;
            this.port = port;
            this.ipv4 = ipv4;
            this.ipv6 = ipv6;
        }
    }
}
