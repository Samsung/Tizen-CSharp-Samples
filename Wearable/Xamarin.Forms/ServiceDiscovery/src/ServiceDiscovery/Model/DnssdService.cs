
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

namespace ServiceDiscovery.Model
{
    /// <summary>
    /// Class representing DNS-SD Service
    /// </summary>
    public class DnssdService
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Port { get; set; }
        public System.Net.IPAddress IPv4 { get; set; }
        public System.Net.IPAddress IPv6 { get; set; }

        /// <summary>
        /// DnssdService class constructor.
        /// </summary>
        /// <param name="name">Service name</param>
        /// <param name="type">Service type</param>
        /// <param name="port">Port number</param>
        /// <param name="ipv4">IPv4 address</param>
        /// <param name="ipv6">IPv6 address</param>
        internal DnssdService(string name, string type, int port, System.Net.IPAddress ipv4, System.Net.IPAddress ipv6)
        {
            Name = name;
            Type = type;
            Port = port;
            IPv4 = ipv4;
            IPv6 = ipv6;
        }
    }
}
