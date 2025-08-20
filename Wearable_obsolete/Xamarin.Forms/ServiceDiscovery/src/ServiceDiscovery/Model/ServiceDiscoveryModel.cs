
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

using System;
using Tizen.Network.Nsd;

namespace ServiceDiscovery.Model
{
    class ServiceDiscoveryModel
    {
        /// <summary>
        /// The event to deliver the Tizen.Network.Nsd ServiceFound event to application
        /// </summary>
        public event EventHandler<DnssdDiscoveryEventArgs> DnssdServiceFound;

        /// <summary>
        /// DnssdBrowser that is in charge of the discovery
        /// </summary>
        private DnssdBrowser browser = null;

        /// <summary>
        /// Register DNS-SD Service
        /// </summary>
        /// <param name="type">Service Type</param>
        /// <param name="name">Service Name</param>
        /// <param name="port">Port Number</param>
        public void RegisterDnssdService(string type, string name, int port)
        {
            var service = new Tizen.Network.Nsd.DnssdService(type)
            {
                // Set service properties
                Name = name,
                Port = port
            };

            // Register the service
            service.RegisterService();
        }

        /// <summary>
        /// Start to discover DNS-SD Service
        /// It operates within local network
        /// </summary>
        /// <param name="type">Service type</param>
        public void StartDiscoverDnssdService(string type)
        {
            // Create a DnssdBrowser that is in charge of the discovery
            browser = new DnssdBrowser(type);
            // Add event handler
            browser.ServiceFound += EventHandlerDnssdServiceFound;
            // Start to discover DNS-SD services
            browser.StartDiscovery();
        }

        /// <summary>
        /// Stop discovering DNS-SD Service
        /// </summary>
        public void StopDiscoverDnssdService()
        {
            // Add event handler
            browser.ServiceFound -= EventHandlerDnssdServiceFound;
            // Start to discover DNS-SD services
            browser.StopDiscovery();
        }

        /// <summary>
        /// Event handler when a DNS-SD service is found
        /// Service information such as state, name, type, port, IP address are delivered
        /// </summary>
        /// <param name="s">Event sender</param>
        /// <param name="e">Event argument</param>
        private void EventHandlerDnssdServiceFound(object s, DnssdServiceFoundEventArgs e)
        {
            // Create a new DnssdDiscoveryEventArgs to deliver the event to application
            var service = new DnssdService(e.Service.Name, e.Service.Type, e.Service.Port, e.Service.IP.IPv4Address, e.Service.IP.IPv6Address);
            DnssdDiscoveryEventArgs de = new DnssdDiscoveryEventArgs(service);
            // Deliver the event to application
            DnssdServiceFound(s, de);
        }
    }

    /// <summary>
    /// The EventArgs for the ServiceFound event
    /// </summary>
    public class DnssdDiscoveryEventArgs : EventArgs
    {
        // Service Informations
        public DnssdService service;

        /// <summary>
        /// Event argument that contains the found service information
        /// </summary>
        /// <param name="service"> Service data </param>
        public DnssdDiscoveryEventArgs(DnssdService service)
        {
            this.service = service;
        }
    }
}
