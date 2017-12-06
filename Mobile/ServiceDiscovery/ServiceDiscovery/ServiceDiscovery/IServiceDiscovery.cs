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
        void DiscoverDNSSDService(string type);

        /// <summary>
        /// The event to deliver the Tizen.Network.Nsd ServiceFound event to application
        /// </summary>
        event EventHandler<DNSSDDiscoveryEventArgs> DNSSDServiceFound;
    }

    /// <summary>
    /// The EventARgs for the ServiceFound event
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
