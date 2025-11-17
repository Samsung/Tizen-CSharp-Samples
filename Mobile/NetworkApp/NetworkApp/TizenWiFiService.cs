using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen.Network.WiFi;

// Note: APInfo class should be available or defined in your project.
// It was part of the original Xamarin project (NetworkApp/APInfo.cs).
// If it's not in the Xaml2NUI project, you'll need to add it.
// For now, I'll assume it exists or will be added.

namespace NetworkApp
{
    /// <summary>
    /// Tizen-specific Wi-Fi service implementation for the Xaml2NUI project.
    /// This class directly uses Tizen.Network.WiFi APIs.
    /// </summary>
    public class TizenWiFiService
    {
        private IEnumerable<WiFiAP> apList = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public TizenWiFiService()
        {
            apList = new List<WiFiAP>();
            // TODO: Consider if explicit Wi-Fi state change event handling is needed here,
            // similar to what might have been implicitly managed by Xamarin's DependencyService.
            // For example, WiFiManager.ConnectionStateChanged event, etc.
        }

        /// <summary>
        /// Call WiFiManager.ActivateAsync() to turn on Wi-Fi interface
        /// </summary>
        /// <returns>Task to do ActivateAsync</returns>
        public async Task Activate()
        {
            Console.WriteLine($"[TizenWiFiService] Activate()");
            try
            {
                await WiFiManager.ActivateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TizenWiFiService] Activate failed: {ex.Message}");
                // Consider re-throwing or handling as appropriate for the UI
                throw;
            }
        }

        /// <summary>
        /// Call WiFiManager.DeactivateAsync() to turn off Wi-Fi interface
        /// </summary>
        /// <returns>Task to do DeactivateAsync</returns>
        public async Task Deactivate()
        {
            Console.WriteLine($"[TizenWiFiService] Deactivate()");
            try
            {
                await WiFiManager.DeactivateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TizenWiFiService] Deactivate failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Call WiFiManager.ScanAsync() to scan
        /// </summary>
        /// <returns>Task to do ScanAsync</returns>
        public async Task Scan()
        {
            Console.WriteLine($"[TizenWiFiService] Scan()");
            try
            {
                await WiFiManager.ScanAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TizenWiFiService] Scan failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Call WiFiManager.GetFoundAPs() to get scan result and return a list that contains Wi-Fi AP information
        /// </summary>
        /// <returns>scan result by a list of Wi-Fi AP information</returns>
        public List<APInfo> ScanResult()
        {
            Console.WriteLine($"[TizenWiFiService] ScanResult()");
            try
            {
                apList = WiFiManager.GetFoundAPs();
                return GetAPList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TizenWiFiService] ScanResult failed: {ex.Message}");
                return null; // Or return an empty list
            }
        }

        /// <summary>
        /// Call WiFiAP.ConnectAsync() to connect the Wi-Fi AP
        /// </summary>
        /// <param name="essid">ESSID of Wi-Fi AP to connect</param>
        /// <param name="password">password of Wi-Fi AP to connect</param>
        /// <returns>Task to do ConnectAsync</returns>
        public async Task Connect(String essid, String password)
        {
            Console.WriteLine($"[TizenWiFiService] Connect() ESSID: {essid}, Password: {(string.IsNullOrEmpty(password) ? "N/A" : "****")}");
            WiFiAP ap = FindAP(essid);
            if (ap == null)
            {
                var ex = new ArgumentException($"Cannot find AP with ESSID: {essid}");
                Console.WriteLine($"[TizenWiFiService] Connect failed: {ex.Message}");
                throw ex;
            }

            if (password.Length > 0)
            {
                // The Tizen API may have changed; KeyManagement property is no longer available.
                // SetPassphrase should handle security type internally or ignore password for open networks.
                ap.SecurityInformation.SetPassphrase(password);
            }
            try
            {
                await ap.ConnectAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TizenWiFiService] Connect to {essid} failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Call WiFiAP.DisconnectAsync() to disconnect the Wi-Fi AP
        /// </summary>
        /// <param name="essid">ESSID of Wi-Fi AP to disconnect</param>
        /// <returns>Task to do DisconnectAsync</returns>
        public async Task Disconnect(String essid)
        {
            Console.WriteLine($"[TizenWiFiService] Disconnect() ESSID: {essid}");
            WiFiAP ap = FindAP(essid);
            if (ap == null)
            {
                var ex = new ArgumentException($"Cannot find AP with ESSID: {essid} to disconnect.");
                Console.WriteLine($"[TizenWiFiService] Disconnect failed: {ex.Message}");
                throw ex;
            }
            try
            {
                await ap.DisconnectAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TizenWiFiService] Disconnect from {essid} failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Call WiFiAP.ForgetAP() to forget the Wi-Fi AP
        /// </summary>
        /// <param name="essid">ESSID of Wi-Fi AP to forget</param>
        public void Forget(String essid)
        {
            Console.WriteLine($"[TizenWiFiService] Forget() ESSID: {essid}");
            WiFiAP ap = FindAP(essid);
            if (ap == null)
            {
                Console.WriteLine($"[TizenWiFiService] Forget failed: Can't find AP {essid}");
                return;
            }
            try
            {
                ap.ForgetAP();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TizenWiFiService] Forget {essid} failed: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Find a WiFiAP instance
        /// </summary>
        /// <param name="essid">ESSID of Wi-Fi AP to find from apList</param>
        /// <returns>WiFiAP instance with the ESSID</returns>
        private WiFiAP FindAP(String essid)
        {
            Console.WriteLine($"[TizenWiFiService] FindAP() ESSID: {essid}");
            // Ensure apList is populated if it's empty or stale.
            // The original implementation called ScanResult() here, which might be too slow
            // if called frequently (e.g., for connect/disconnect).
            // Consider if apList should be refreshed by a Scan call initiated by the UI.
            // For now, mirroring original behavior:
            if (apList == null || !apList.GetEnumerator().MoveNext()) // Basic check if list is empty
            {
                 ScanResult(); // This will update apList
            }

            foreach (var item in apList)
            {
                // Ensure NetworkInformation and Essid are not null
                if (item?.NetworkInformation?.Essid != null)
                {
                    Console.WriteLine($"[TizenWiFiService] FindAP() Checking AP\t{item.NetworkInformation.Essid}");
                    if (item.NetworkInformation.Essid.Equals(essid))
                    {
                        return item;
                    }
                }
            }
            Console.WriteLine($"[TizenWiFiService] FindAP() AP with ESSID {essid} not found in current list.");
            return null;
        }

        /// <summary>
        /// Check if Wi-Fi is powered on
        /// </summary>
        /// <returns>True if Wi-Fi is on. False, otherwise</returns>
        public bool IsActive()
        {
            Console.WriteLine($"[TizenWiFiService] IsActive()");
            try
            {
                return WiFiManager.IsActive;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TizenWiFiService] IsActive check failed: {ex.Message}");
                return false; // Default to false on error
            }
        }

        /// <summary>
        /// Get the ESSID of the AP that this device is connected to
        /// </summary>
        /// <returns>ESSID of the connected Wi-Fi AP</returns>
        public String ConnectedAP()
        {
            Console.WriteLine($"[TizenWiFiService] ConnectedAP()");
            try
            {
                WiFiAP connectedAp = WiFiManager.GetConnectedAP();
                return connectedAp?.NetworkInformation?.Essid; // Use null conditional for safety
            }
            catch (Exception ex)
            {
                // This can throw if no AP is connected, depending on Tizen API version/behavior
                Console.WriteLine($"[TizenWiFiService] ConnectedAP failed: {ex.Message}");
                return null; // Or string.Empty
            }
        }

        /// <summary>
        /// Get a list of scanned Wi-Fi APs
        /// </summary>
        /// <returns>List of Wi-Fi AP information</returns>
        public List<APInfo> GetAPList()
        {
            List<APInfo> apInfoList = new List<APInfo>();
            if (apList == null)
            {
                Console.WriteLine("[TizenWiFiService] GetAPList() apList is null.");
                return apInfoList; // Return empty list
            }

            foreach (var item in apList)
            {
                if (item?.NetworkInformation != null)
                {
                    apInfoList.Add(new APInfo(item.NetworkInformation.Essid, item.NetworkInformation.ConnectionState.ToString()));
                }
            }
            return apInfoList;
        }
    }
}
