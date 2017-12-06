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
        /// Register DNS-SD Service
        /// </summary>
        /// <param name="type">Service Type</param>
        /// <param name="name">Service Name</param>
        /// <param name="port">Port Number</param>
        public void RegisterDNSSDService(string type, string name, int port)
        {
            using (DnssdService service = new DnssdService(type))
            {
                // Set service properties
                service.Name = name;
                service.Port = port;

                // Register the service
                service.RegisterService();
            }
        }

        /// <summary>
        /// Start to discover DNS-SD Service
        /// It operates within local network
        /// </summary>
        /// <param name="type">Service type</param>
        public void DiscoverDNSSDService(string type)
        {
            // Create a DnssdBrowser that is in charge of the discovery
            DnssdBrowser browser = new DnssdBrowser(type);
            // Add event handler
            browser.ServiceFound += EventHandlerDNSSDServiceFound;
            // Start to discover DNS-SD services
            browser.StartDiscovery();
        }

        /// <summary>
        /// Event handler when a DNS-SD service is found
        /// Service informations such as state, name, type, port, ip address are delivered
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
