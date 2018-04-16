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

using ServiceDiscovery.Tizen.Mobile;
using System;
using Tizen.Network.Nsd;

[assembly: Xamarin.Forms.Dependency(typeof(ServiceDiscoveryImplementation))]

namespace ServiceDiscovery.Tizen.Mobile
{
    /// <summary>
    /// Implementation class of IServiceDiscovery interface
    /// </summary>
    class ServiceDiscoveryImplementation : IServiceDiscovery
    {
        /// <summary>
        /// The event to deliver the Tizen.Network.Nsd ServiceFound event to application
        /// </summary>
        public event EventHandler<DNSSDDiscoveryEventArgs> DNSSDServiceFound;

        /// <summary>
        /// DnssdBrowser that is in charge of the discovery
        /// </summary>
        DnssdBrowser browser = null;

        /// <summary>
        /// Register DNS-SD Service
        /// </summary>
        /// <param name="type">Service Type</param>
        /// <param name="name">Service Name</param>
        /// <param name="port">Port Number</param>
        public void RegisterDNSSDService(string type, string name, int port)
        {
            DnssdService service = new DnssdService(type);

            // Set service properties
            service.Name = name;
            service.Port = port;

            // Register the service
            service.RegisterService();
        }

        /// <summary>
        /// Start to discover DNS-SD Service
        /// It operates within local network
        /// </summary>
        /// <param name="type">Service type</param>
        public void StartDiscoverDNSSDService(string type)
        {
            // Create a DnssdBrowser that is in charge of the discovery
            browser = new DnssdBrowser(type);
            // Add event handler
            browser.ServiceFound += EventHandlerDNSSDServiceFound;
            // Start to discover DNS-SD services
            browser.StartDiscovery();
        }

        /// <summary>
        /// Stop discovering DNS-SD Service
        /// </summary>
        public void StopDiscoverDNSSDService()
        {
            // Add event handler
            browser.ServiceFound -= EventHandlerDNSSDServiceFound;
            // Start to discover DNS-SD services
            browser.StopDiscovery();
        }

        /// <summary>
        /// Event handler when a DNS-SD service is found
        /// Service informations such as state, name, type, port, IP address are delivered
        /// </summary>
        /// <param name="s">Event sender</param>
        /// <param name="e">Event argument</param>
        private void EventHandlerDNSSDServiceFound(object s, DnssdServiceFoundEventArgs e)
        {
            // Create a new DNSSDDiscoveryEventArgs to deliver the event to application
            DNSSDDiscoveryEventArgs de = new DNSSDDiscoveryEventArgs(e.State.ToString(), e.Service.Name, e.Service.Type,e.Service.Port, e.Service.IP.IPv4Address, e.Service.IP.IPv6Address);
            // Deliver the event to application
            DNSSDServiceFound(s, de);
        }
    }
}
