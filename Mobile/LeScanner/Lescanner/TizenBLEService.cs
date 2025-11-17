using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; // For Encoding
using System.Threading.Tasks;
using Tizen.Network.Bluetooth;

namespace Lescanner
{
    /// <summary>
    /// Event arguments for when a device name is retrieved.
    /// </summary>
    public class DeviceNameEventArgs : EventArgs
    {
        public string DeviceAddress { get; }
        public string DeviceName { get; }

        public DeviceNameEventArgs(string deviceAddress, string deviceName)
        {
            DeviceAddress = deviceAddress;
            DeviceName = deviceName;
        }
    }

    /// <summary>
    /// Service class to handle Bluetooth Low Energy operations.
    /// Encapsulates Tizen.Network.Bluetooth logic.
    /// </summary>
    public class TizenBLEService
    {
        // Event to notify when a new device is discovered
        public event EventHandler<AdapterLeScanResultChangedEventArgs> DeviceDiscovered;
        // Event to notify when GATT connection state changes
        public event EventHandler<GattConnectionStateChangedEventArgs> GattConnectionStateChanged;
        // Event to notify when service discovery is complete
        public event EventHandler<IEnumerable<string>> ServicesDiscovered;
        // Event to notify when device name is retrieved after connection
        public event EventHandler<DeviceNameEventArgs> DeviceNameRetrieved;

        private BluetoothGattClient _gattClient;
        private bool _isScanning = false;
        private string _currentConnectedDeviceAddress; // To store address of the connected device for name fetching

        /// <summary>
        /// Gets the address of the currently connected device.
        /// </summary>
        public string CurrentConnectedDeviceAddress => _currentConnectedDeviceAddress;

        /// <summary>
        /// Gets the currently discovered services for the connected device.
        /// </summary>
        /// <returns>List of service UUIDs, or empty list if not connected or no services discovered.</returns>
        public IEnumerable<string> GetCurrentServices()
        {
            if (_gattClient == null)
            {
                return new List<string>();
            }

            try
            {
                var services = _gattClient.GetServices();
                if (services != null)
                {
                    return services.Select(s => s.Uuid).ToList();
                }
            }
            catch (Exception ex)
            {
                Tizen.Log.Error(Constants.LOG_TAG, $"Error getting current services: {ex.Message}");
            }

            return new List<string>();
        }

        // Standard UUIDs for Generic Access service and Device Name characteristic
        private const string GENERIC_ACCESS_SERVICE_UUID = "00001800-0000-1000-8000-00805f9b34fb";
        private const string DEVICE_NAME_CHARACTERISTIC_UUID = "00002a00-0000-1000-8000-00805f9b34fb";

        /// <summary>
        /// Checks if Bluetooth is enabled on the device.
        /// </summary>
        /// <returns>True if Bluetooth is enabled, false otherwise.</returns>
        public bool IsBluetoothEnabled()
        {
            try
            {
                return BluetoothAdapter.IsBluetoothEnabled;
            }
            catch (Exception ex)
            {
                Tizen.Log.Error(Constants.LOG_TAG, $"Error checking Bluetooth status: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Starts the LE scan process.
        /// </summary>
        public async Task StartLeScanAsync()
        {
            if (_isScanning)
            {
                Tizen.Log.Info(Constants.LOG_TAG, "Already scanning.");
                return;
            }

            if (!IsBluetoothEnabled())
            {
                Tizen.Log.Warn(Constants.LOG_TAG, "Bluetooth is not enabled. Cannot start scan.");
                return;
            }

            try
            {
                _isScanning = true;
                BluetoothAdapter.ScanResultChanged += OnScanResultChanged;
                BluetoothAdapter.StartLeScan();
                Tizen.Log.Info(Constants.LOG_TAG, "LE Scan started.");
            }
            catch (Exception ex)
            {
                Tizen.Log.Error(Constants.LOG_TAG, $"Error starting LE scan: {ex.Message}. The Bluetooth adapter may be in a bad state. Please try restarting Bluetooth or the device.");
                _isScanning = false;
                BluetoothAdapter.ScanResultChanged -= OnScanResultChanged;
            }
        }

        /// <summary>
        /// Stops the LE scan process.
        /// </summary>
        public async Task StopLeScanAsync()
        {
            if (!_isScanning)
            {
                Tizen.Log.Info(Constants.LOG_TAG, "Not currently scanning.");
                return;
            }

            try
            {
                BluetoothAdapter.StopLeScan();
                Tizen.Log.Info(Constants.LOG_TAG, "LE Scan stopped.");
            }
            catch (Exception ex)
            {
                Tizen.Log.Error(Constants.LOG_TAG, $"Error stopping LE scan: {ex.Message}");
            }
            finally
            {
                _isScanning = false;
                BluetoothAdapter.ScanResultChanged -= OnScanResultChanged;
            }
        }

        /// <summary>
        /// Event handler for BluetoothAdapter.ScanResultChanged.
        /// </summary>
        private void OnScanResultChanged(object sender, AdapterLeScanResultChangedEventArgs e)
        {
            DeviceDiscovered?.Invoke(this, e);
        }

        private bool _isConnecting = false; // Track connection state

        /// <summary>
        /// Initiates a GATT connection to the specified device address.
        /// </summary>
        /// <param name="deviceAddress">The Bluetooth address of the device.</param>
        /// <returns>True if connection initiation was successful, false otherwise.</returns>
        public async Task<bool> ConnectGattAsync(string deviceAddress)
        {
            if (_isConnecting)
            {
                Tizen.Log.Warn(Constants.LOG_TAG, "Connection already in progress. Please wait.");
                return false;
            }

            if (_gattClient != null)
            {
                Tizen.Log.Warn(Constants.LOG_TAG, "A GATT client already exists. Disconnecting first.");
                await DisconnectGattAsync();
                
                // Add a small delay to ensure the disconnect is fully processed
                await Task.Delay(500);
            }

            try
            {
                _isConnecting = true;
                _currentConnectedDeviceAddress = deviceAddress; // Store for name fetching
                _gattClient = BluetoothGattClient.CreateClient(deviceAddress);
                if (_gattClient == null)
                {
                    Tizen.Log.Error(Constants.LOG_TAG, "Failed to create GATT client.");
                    _currentConnectedDeviceAddress = null;
                    _isConnecting = false;
                    return false;
                }
                _gattClient.ConnectionStateChanged += OnGattConnectionStateChanged;
                await _gattClient.ConnectAsync(false); // false for direct connection
                Tizen.Log.Info(Constants.LOG_TAG, $"GATT connection initiated to {deviceAddress}.");
                return true;
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Operation now in progress"))
            {
                // This specific error often means a connection attempt is already underway.
                // Do not dispose of the client yet, let the ConnectionStateChanged event determine the final state.
                Tizen.Log.Warn(Constants.LOG_TAG, $"GATT connection to {deviceAddress} reported 'Operation now in progress'. Awaiting ConnectionStateChanged event. Message: {ex.Message}");
                // The client might still be valid, so we don't dispose it here.
                // Return false to indicate the ConnectAsync call itself had issues.
                _isConnecting = false;
                return false;
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Operation already done"))
            {
                Tizen.Log.Warn(Constants.LOG_TAG, $"GATT connection to {deviceAddress} reported 'Operation already done'. The connection might already be established. Message: {ex.Message}");
                _isConnecting = false;
                return false;
            }
            catch (Exception ex)
            {
                Tizen.Log.Error(Constants.LOG_TAG, $"Error connecting GATT to {deviceAddress}: {ex.Message}");
                _gattClient?.Dispose();
                _gattClient = null;
                _currentConnectedDeviceAddress = null;
                _isConnecting = false;
                return false;
            }
        }

        /// <summary>
        /// Disconnects the current GATT client with a timeout.
        /// Event is unsubscribed at the beginning of the process to prevent interference with new connections,
        /// and to ensure the handler is not attached to the client when it's disposed.
        /// </summary>
        public async Task DisconnectGattAsync()
        {
            if (_gattClient == null)
            {
                Tizen.Log.Info(Constants.LOG_TAG, "No GATT client to disconnect.");
                return;
            }

            var clientToDispose = _gattClient;
            
            try
            {
                // Set to null first to prevent reconnection attempts during disconnect
                _gattClient = null;
                _currentConnectedDeviceAddress = null;

                // Unsubscribe first to prevent this handler from being called with a disposed object later,
                // and to avoid issues when creating a new client.
                clientToDispose.ConnectionStateChanged -= OnGattConnectionStateChanged;
                Tizen.Log.Info(Constants.LOG_TAG, "Unsubscribed from ConnectionStateChanged event. Proceeding with disconnect.");

                var disconnectTask = clientToDispose.DisconnectAsync();
                var timeoutTask = Task.Delay(3000); // Reduced to 3 seconds

                if (await Task.WhenAny(disconnectTask, timeoutTask) == timeoutTask)
                {
                    Tizen.Log.Warn(Constants.LOG_TAG, "GATT disconnect timed out after 3 seconds. Forcing disposal.");
                }
                else
                {
                    Tizen.Log.Info(Constants.LOG_TAG, "GATT disconnected.");
                }
            }
            catch (Exception ex)
            {
                Tizen.Log.Error(Constants.LOG_TAG, $"Error during GATT disconnect: {ex.Message}");
            }
            finally
            {
                try
                {
                    clientToDispose?.Dispose();
                }
                catch (Exception ex)
                {
                    Tizen.Log.Error(Constants.LOG_TAG, $"Error disposing GATT client: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Event handler for GattClient.ConnectionStateChanged.
        /// </summary>
        private void OnGattConnectionStateChanged(object sender, GattConnectionStateChangedEventArgs e)
        {
            // Robustness check: Ensure the sender is the currently active client.
            // This prevents processing stale events from a client that was just disposed.
            if (_gattClient == null || sender != _gattClient)
            {
                Tizen.Log.Warn(Constants.LOG_TAG, "Received ConnectionStateChanged event for a stale or null client. Ignoring.");
                return;
            }

            // Robustness check: Ensure event args are valid.
            if (e == null)
            {
                Tizen.Log.Warn(Constants.LOG_TAG, "ConnectionStateChanged event received with null args. Ignoring.");
                return;
            }

            Tizen.Log.Info(Constants.LOG_TAG, $"ConnectionStateChanged: IsConnected = {e.IsConnected}, RemoteAddress = {_gattClient.RemoteAddress}");
            GattConnectionStateChanged?.Invoke(this, e);

            if (e.IsConnected)
            {
                _currentConnectedDeviceAddress = _gattClient.RemoteAddress; // Update address on successful connection
                _isConnecting = false; // Clear connecting flag on successful connection
                Tizen.Log.Info(Constants.LOG_TAG, "GATT connected. Discovering services and fetching name.");
                DiscoverServices();
                _ = FetchDeviceNameAsync(); // Fire and forget
            }
            else
            {
                Tizen.Log.Info(Constants.LOG_TAG, "GATT disconnected.");
                _isConnecting = false; // Clear connecting flag on disconnect
                // Do not set _gattClient to null here, DisconnectGattAsync handles it.
                // Only clear the connected address.
                _currentConnectedDeviceAddress = null;
            }
        }


        /// <summary>
        /// Fetches the device name by reading the Device Name characteristic from the Generic Access service.
        /// Based on TizenFX source, ReadValueAsync returns a Task<bool> and the value is updated on the characteristic object.
        /// </summary>
        private async Task FetchDeviceNameAsync()
        {
            if (_gattClient == null || string.IsNullOrEmpty(_currentConnectedDeviceAddress))
            {
                Tizen.Log.Warn(Constants.LOG_TAG, "GATT client or device address is null. Cannot fetch name.");
                return;
            }

            try
            {
                BluetoothGattService genericAccessService = _gattClient.GetService(GENERIC_ACCESS_SERVICE_UUID);
                if (genericAccessService != null)
                {
                    Tizen.Log.Info(Constants.LOG_TAG, "Found Generic Access service.");
                    BluetoothGattCharacteristic deviceNameCharacteristic = genericAccessService.GetCharacteristic(DEVICE_NAME_CHARACTERISTIC_UUID);
                    if (deviceNameCharacteristic != null)
                    {
                        Tizen.Log.Info(Constants.LOG_TAG, "Found Device Name characteristic. Initiating read...");
                        // The ReadValueAsync method will complete when the read operation is done.
                        // The value of the characteristic will be updated internally.
                        bool readSuccess = await _gattClient.ReadValueAsync(deviceNameCharacteristic);

                        if (readSuccess)
                        {
                            Tizen.Log.Info(Constants.LOG_TAG, "ReadValueAsync reported success. Checking characteristic value.");
                            if (deviceNameCharacteristic.Value != null)
                            {
                                string deviceName = Encoding.UTF8.GetString(deviceNameCharacteristic.Value).TrimEnd('\0');
                                Tizen.Log.Info(Constants.LOG_TAG, $"Successfully fetched device name: {deviceName}");
                                DeviceNameRetrieved?.Invoke(this, new DeviceNameEventArgs(_currentConnectedDeviceAddress, deviceName));
                            }
                            else
                            {
                                Tizen.Log.Warn(Constants.LOG_TAG, "ReadValueAsync reported success, but characteristic value is null.");
                                DeviceNameRetrieved?.Invoke(this, new DeviceNameEventArgs(_currentConnectedDeviceAddress, null));
                            }
                        }
                        else
                        {
                            Tizen.Log.Error(Constants.LOG_TAG, "ReadValueAsync failed to read the Device Name characteristic.");
                            DeviceNameRetrieved?.Invoke(this, new DeviceNameEventArgs(_currentConnectedDeviceAddress, null));
                        }
                    }
                    else
                    {
                        Tizen.Log.Warn(Constants.LOG_TAG, "Device Name characteristic not found.");
                        DeviceNameRetrieved?.Invoke(this, new DeviceNameEventArgs(_currentConnectedDeviceAddress, null));
                    }
                }
                else
                {
                    Tizen.Log.Warn(Constants.LOG_TAG, "Generic Access service not found.");
                    DeviceNameRetrieved?.Invoke(this, new DeviceNameEventArgs(_currentConnectedDeviceAddress, null));
                }
            }
            catch (Exception ex)
            {
                Tizen.Log.Error(Constants.LOG_TAG, $"Error fetching device name: {ex.Message}");
                DeviceNameRetrieved?.Invoke(this, new DeviceNameEventArgs(_currentConnectedDeviceAddress, null));
            }
        }


        /// <summary>
        /// Discovers services offered by the connected GATT server.
        /// </summary>
        private void DiscoverServices()
        {
            if (_gattClient == null)
            {
                Tizen.Log.Warn(Constants.LOG_TAG, "GATT client is null. Cannot discover services.");
                return;
            }

            try
            {
                IEnumerable<BluetoothGattService> services = _gattClient.GetServices();
                var uuids = new List<string>();
                if (services != null)
                {
                    foreach (var service in services)
                    {
                        uuids.Add(service.Uuid);
                        Tizen.Log.Info(Constants.LOG_TAG, $"Discovered service UUID: {service.Uuid}");
                    }
                }
                ServicesDiscovered?.Invoke(this, uuids);
            }
            catch (Exception ex)
            {
                Tizen.Log.Error(Constants.LOG_TAG, $"Error discovering services: {ex.Message}");
                ServicesDiscovered?.Invoke(this, new List<string>()); // Notify with empty list on error
            }
        }

    }
}
