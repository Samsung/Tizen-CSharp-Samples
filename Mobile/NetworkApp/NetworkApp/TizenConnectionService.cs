using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen.Network.Connection;

namespace NetworkApp
{
    /// <summary>
    /// Tizen-specific Connection service implementation for the Xaml2NUI project.
    /// This class directly uses Tizen.Network.Connection APIs.
    /// </summary>
    public class TizenConnectionService
    {
        private IEnumerable<ConnectionProfile> profileList;

        /// <summary>
        /// Constructor
        /// </summary>
        public TizenConnectionService()
        {
        }

        /// <summary>
        /// The type of the current profile for data connection (Disconnected, Wi-Fi, Cellular, etc.)
        /// </summary>
        public String CurrentType
        {
            get
            {
                try
                {
                    // Call Tizen C# API
                    // Check if CurrentConnection is null, which can happen if disconnected
                    var currentConn = ConnectionManager.CurrentConnection;
                    if (currentConn == null)
                    {
                        return "Not Connected";
                    }
                    return currentConn.Type.ToString();
                }
                catch (System.InvalidOperationException ex) when (ex.Message.Contains("not initialized") || ex.Message.Contains("not available"))
                {
                    Console.WriteLine($"[TizenConnectionService] CurrentType not available: {ex.Message}");
                    return "Not Available";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TizenConnectionService] CurrentType get failed: {ex.Message}");
                    return "Error"; // Fallback for unexpected errors
                }
            }
        }

        /// <summary>
        /// The state of the current profile for data connection
        /// </summary>
        public String CurrentState
        {
            get
            {
                try
                {
                    // Call Tizen C# API
                    var currentConn = ConnectionManager.CurrentConnection;
                    if (currentConn == null)
                    {
                        return "Not Connected";
                    }
                    return currentConn.State.ToString();
                }
                catch (System.InvalidOperationException ex) when (ex.Message.Contains("not initialized") || ex.Message.Contains("not available"))
                {
                    Console.WriteLine($"[TizenConnectionService] CurrentState not available: {ex.Message}");
                    return "Not Available";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TizenConnectionService] CurrentState get failed: {ex.Message}");
                    return "Error";
                }
            }
        }

        /// <summary>
        /// The state of the Wi-Fi connection
        /// </summary>
        public String WiFiState
        {
            get
            {
                try
                {
                    // Call Tizen C# API
                    return ConnectionManager.WiFiState.ToString();
                }
                catch (System.InvalidOperationException ex) when (ex.Message.Contains("not initialized") || ex.Message.Contains("not available"))
                {
                    Console.WriteLine($"[TizenConnectionService] WiFiState not available: {ex.Message}");
                    return "Not Available";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TizenConnectionService] WiFiState get failed: {ex.Message}");
                    return "Error";
                }
            }
        }

        /// <summary>
        /// The state of the Cellular connection
        /// </summary>
        public String CellularState
        {
            get
            {
                try
                {
                    // Call Tizen C# API
                    return ConnectionManager.CellularState.ToString();
                }
                catch (Exception ex) // Catch any exception to prevent raw error from showing
                {
                    Console.WriteLine($"[TizenConnectionService] CellularState get failed: {ex.Message}");
                    // Check for common error messages to provide more specific feedback
                    if (ex.Message.Contains("not supported"))
                    {
                        return "Not Supported";
                    }
                    if (ex.Message.Contains("not available") || ex.Message.Contains("not initialized"))
                    {
                        return "Not Available";
                    }
                    return "Error"; // Generic fallback for other exceptions
                }
            }
        }

        /// <summary>
        /// The IPv4 address of the current connection
        /// </summary>
        public String IPv4Address
        {
            get
            {
                try
                {
                    // Call Tizen C# API
                    var ipAddress = ConnectionManager.GetIPAddress(AddressFamily.IPv4);
                    return ipAddress?.ToString() ?? "N/A";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TizenConnectionService] IPv4Address get failed: {ex.Message}");
                    return "Error";
                }
            }
        }

        /// <summary>
        /// The IPv6 address of the current connection
        /// </summary>
        public String IPv6Address
        {
            get
            {
                try
                {
                    // Call Tizen C# API
                    var ipAddress = ConnectionManager.GetIPAddress(AddressFamily.IPv6);
                    return ipAddress?.ToString() ?? "N/A";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TizenConnectionService] IPv6Address get failed: {ex.Message}");
                    return "Error";
                }
            }
        }

        /// <summary>
        /// The MAC address of the Wi-Fi
        /// </summary>
        public String WiFiMACAddress
        {
            get
            {
                try
                {
                    // Call Tizen C# API
                    var macAddress = ConnectionManager.GetMacAddress(ConnectionType.WiFi);
                    return macAddress?.ToString() ?? "N/A";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TizenConnectionService] WiFiMACAddress get failed: {ex.Message}");
                    return "Error";
                }
            }
        }

        /// <summary>
        /// The proxy address of the current connection
        /// </summary>
        public String ProxyAddress
        {
            get
            {
                try
                {
                    // Call Tizen C# API
                    var proxyAddress = ConnectionManager.GetProxy(AddressFamily.IPv4);
                    if (proxyAddress == null || string.IsNullOrEmpty(proxyAddress.ToString()))
                    {
                        return "No proxy configured";
                    }
                    return proxyAddress.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TizenConnectionService] ProxyAddress get failed: {ex.Message}");
                    // Check for common error messages
                    if (ex.Message.Contains("not supported"))
                    {
                        return "Not Supported";
                    }
                    if (ex.Message.Contains("not available") || ex.Message.Contains("not initialized"))
                    {
                        return "Not Available";
                    }
                    return "Error"; // Generic fallback
                }
            }
        }

        /// <summary>
        /// Get profile list as a list of profile name
        /// </summary>
        /// <returns>List of profile names</returns>
        public async Task<List<String>> GetProfileListAsync()
        {
            try
            {
                // Get profile list
                await GetProfileListInternalAsync();
                List<String> result = new List<string>();
                if (profileList != null)
                {
                    foreach (var item in profileList)
                    {
                        // Add name of connection profiles to a list
                        result.Add(item.Name);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TizenConnectionService] GetProfileListAsync failed: {ex.Message}");
                return new List<string> { "Error" };
            }
        }

        /// <summary>
        /// Gets the list of the profile internally
        /// </summary>
        private async Task GetProfileListInternalAsync()
        {
            // Call Tizen C# API
            profileList = await ConnectionProfileManager.GetProfileListAsync(ProfileListType.Registered);
        }
    }
}
