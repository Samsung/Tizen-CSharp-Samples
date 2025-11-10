using System;
using System.Collections.Generic;
using System.Linq;
using Tizen.Network.WiFiDirect;

namespace NetworkApp
{
    /// <summary>
    /// Tizen-specific Wi-Fi Direct service implementation.
    /// This class directly uses Tizen.Network.WiFiDirect APIs.
    /// </summary>
    public class TizenWiFiDirectService
    {
        private bool isScanning = false;
        private List<WiFiDirectDevice> discoveredDevices = new List<WiFiDirectDevice>();
        public bool IsInitialized { get; private set; } = false;

        // Event to notify when a new device is discovered or the list is updated
        public event EventHandler<WiFiDirectDeviceEventArgs> DevicesUpdated;

        public TizenWiFiDirectService()
        {
            try
            {
                Tizen.Log.Info("TizenWiFiDirectService", "Constructor called. Attempting to initialize.");
                // Subscribe to Tizen's native events
                WiFiDirectManager.DiscoveryStateChanged += OnDiscoveryStateChanged;
                WiFiDirectManager.DeviceStateChanged += OnDeviceStateChanged;
                IsInitialized = true;
                Tizen.Log.Info("TizenWiFiDirectService", "Successfully initialized and subscribed to events.");
            }
            catch (Exception ex)
            {
                Tizen.Log.Error("TizenWiFiDirectService", $"Initialization failed: {ex.Message}. This might be due to Wi-Fi Direct not being supported or enabled on the device.");
                IsInitialized = false;
                // Ensure we are not subscribed if an exception occurred mid-subscription
                try { WiFiDirectManager.DiscoveryStateChanged -= OnDiscoveryStateChanged; } catch { /* ignore */ }
                try { WiFiDirectManager.DeviceStateChanged -= OnDeviceStateChanged; } catch { /* ignore */ }
            }
        }

        /// <summary>
        /// Starts scanning for Wi-Fi Direct devices.
        /// </summary>
        /// <returns>True if scan initiation was successful, false otherwise.</returns>
        public bool StartScan()
        {
            try
            {
                if (isScanning)
                {
                    Tizen.Log.Info("TizenWiFiDirectService", "Scan already in progress.");
                    return true; 
                }

                Tizen.Log.Info("TizenWiFiDirectService", "StartScan called.");
                // Clear previous results
                discoveredDevices.Clear();

                // Check if the Wi-Fi Direct is deactivated
                if (WiFiDirectManager.State == WiFiDirectState.Deactivated)
                {
                    Tizen.Log.Info("TizenWiFiDirectService", "Wi-Fi Direct is deactivated. Activating...");
                    // Activation is asynchronous. Discovery will start in OnDeviceStateChanged.
                    WiFiDirectManager.Activate();
                    // isScanning will be set to true once activation is complete and discovery starts.
                }
                else
                {
                    // Already activated, start discovery directly
                    StartDiscoveryInternal();
                }
                return true;
            }
            catch (Exception ex)
            {
                Tizen.Log.Error("TizenWiFiDirectService", $"StartScan failed: {ex.Message}");
                isScanning = false;
                return false;
            }
        }

        /// <summary>
        /// Stops scanning for Wi-Fi Direct devices.
        /// </summary>
        /// <returns>True if scan stop was successful, false otherwise.</returns>
        public bool StopScan()
        {
            try
            {
                Tizen.Log.Info("TizenWiFiDirectService", "StopScan called.");
                WiFiDirectManager.CancelDiscovery();
                // isScanning will be set to false in OnDiscoveryStateChanged
                return true;
            }
            catch (Exception ex)
            {
                Tizen.Log.Error("TizenWiFiDirectService", $"StopScan failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets the list of currently discovered devices.
        /// </summary>
        /// <returns>A list of WiFiDirectDevice objects.</returns>
        public List<WiFiDirectDevice> GetDiscoveredDevices()
        {
            return new List<WiFiDirectDevice>(discoveredDevices);
        }

        private void StartDiscoveryInternal()
        {
            Tizen.Log.Info("TizenWiFiDirectService", "StartDiscoveryInternal called.");
            try
            {
                WiFiDirectManager.StartDiscovery(false, 0); // synchronous = false, channel = 0 (for all channels)
                isScanning = true; // Set scanning state after initiating discovery
                Tizen.Log.Info("TizenWiFiDirectService", "StartDiscovery called.");
            }
            catch (Exception ex)
            {
                Tizen.Log.Error("TizenWiFiDirectService", $"StartDiscoveryInternal failed: {ex.Message}");
                isScanning = false;
            }
        }

        private void OnDiscoveryStateChanged(object sender, DiscoveryStateChangedEventArgs e)
        {
            Tizen.Log.Info("TizenWiFiDirectService", $"OnDiscoveryStateChanged: {e.DiscoveryState}");
            if (e.DiscoveryState == WiFiDirectDiscoveryState.Found)
            {
                Tizen.Log.Info("TizenWiFiDirectService", "Discovery found peers.");
                UpdateDiscoveredPeers();
            }
            else
            {
                if (isScanning) 
                {
                    Tizen.Log.Info("TizenWiFiDirectService", $"Discovery state changed from 'Found' to '{e.DiscoveryState}'. Setting isScanning to false.");
                    isScanning = false;
                }
            }
        }

        private void OnDeviceStateChanged(object sender, DeviceStateChangedEventArgs e)
        {
            Tizen.Log.Info("TizenWiFiDirectService", $"OnDeviceStateChanged: {e.DeviceState}");
            if (e.DeviceState == WiFiDirectDeviceState.Activated)
            {
                Tizen.Log.Info("TizenWiFiDirectService", "Wi-Fi Direct activated. Starting discovery.");
                StartDiscoveryInternal();
            }
            else if (e.DeviceState == WiFiDirectDeviceState.Deactivated)
            {
                isScanning = false;
                discoveredDevices.Clear();
                DevicesUpdated?.Invoke(this, new WiFiDirectDeviceEventArgs(discoveredDevices)); 
                Tizen.Log.Info("TizenWiFiDirectService", "Wi-Fi Direct deactivated. Scan stopped and list cleared.");
            }
        }
        
        private void UpdateDiscoveredPeers()
        {
            try
            {
                IEnumerable<WiFiDirectPeer> peerList = WiFiDirectManager.GetDiscoveredPeers();
                if (peerList != null)
                {
                    var newDeviceList = peerList.Select(p => new WiFiDirectDevice { Name = p.Name }).ToList();
                    
                    if (!discoveredDevices.SequenceEqual(newDeviceList, new WiFiDirectDeviceComparer()))
                    {
                        discoveredDevices = newDeviceList;
                        Tizen.Log.Info("TizenWiFiDirectService", $"Discovered {discoveredDevices.Count} peers. Notifying UI.");
                        DevicesUpdated?.Invoke(this, new WiFiDirectDeviceEventArgs(discoveredDevices));
                    }
                    else
                    {
                         Tizen.Log.Info("TizenWiFiDirectService", "Peer list has not changed.");
                    }
                }
                else
                {
                    if (discoveredDevices.Any()) 
                    {
                        discoveredDevices.Clear();
                        DevicesUpdated?.Invoke(this, new WiFiDirectDeviceEventArgs(discoveredDevices));
                    }
                    Tizen.Log.Info("TizenWiFiDirectService", "GetDiscoveredPeers returned null.");
                }
            }
            catch (Exception ex)
            {
                Tizen.Log.Error("TizenWiFiDirectService", $"UpdateDiscoveredPeers failed: {ex.Message}");
            }
        }
    }

    // Custom EventArgs for passing a list of discovered devices
    public class WiFiDirectDeviceEventArgs : EventArgs
    {
        public List<WiFiDirectDevice> Devices { get; }

        public WiFiDirectDeviceEventArgs(List<WiFiDirectDevice> devices)
        {
            Devices = devices;
        }
    }

    // Class representing a Wi-Fi Direct device for UI purposes.
    // Maps relevant info from Tizen.Network.WiFiDirect.WiFiDirectPeer
    public class WiFiDirectDevice
    {
        public string Name { get; set; } 

        public override string ToString()
        {
            return Name;
        }
    }

    // Helper class to compare lists of WiFiDirectDevice
    public class WiFiDirectDeviceComparer : IEqualityComparer<WiFiDirectDevice>
    {
        public bool Equals(WiFiDirectDevice x, WiFiDirectDevice y)
        {
            if (object.ReferenceEquals(x, y)) return true;
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;
            return x.Name == y.Name;
        }

        public int GetHashCode(WiFiDirectDevice obj)
        {
            if (obj == null) return 0;
            return obj.Name == null ? 0 : obj.Name.GetHashCode();
        }
    }
}
