using System;
using System.Collections.Generic;
using System.Threading; // Added for SynchronizationContext
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.Network.Bluetooth; // For AdapterLeScanResultChangedEventArgs

namespace Lescanner
{
    /// <summary>
    /// Page to display a list of discovered BLE devices.
    /// </summary>
    class DeviceListPage : ContentPage
    {
        private TizenBLEService _bleService;
        private Navigator _navigator;
        private TextLabel _statusLabel;
        private ScrollableBase _deviceScrollableList;
        private View _deviceListContentContainer;
        // Changed to store TextLabel to update it later
        private Dictionary<string, TextLabel> _discoveredDeviceLabels = new Dictionary<string, TextLabel>();
        private bool _isScanActive = false;
        private SynchronizationContext _uiContext; // Added for main thread dispatching
        private HashSet<string> _navigatedDevices = new HashSet<string>(); // Track devices that have been navigated

        /// <summary>
        /// Constructor for DeviceListPage.
        /// </summary>
        /// <param name="bleService">The BLE service instance.</param>
        /// <param name="navigator">The navigator for page navigation.</param>
        public DeviceListPage(TizenBLEService bleService, Navigator navigator)
        {
            _bleService = bleService;
            _navigator = navigator;
            _uiContext = SynchronizationContext.Current; // Capture main UI thread context
            AppBar = new AppBar { Title = "Device List" };
            InitializeComponent();
            
            // Subscribe to BLE service events
            _bleService.DeviceDiscovered += OnDeviceDiscovered;
            _bleService.GattConnectionStateChanged += OnGattConnectionStateChanged; // For connection feedback
            _bleService.DeviceNameRetrieved += OnDeviceNameRetrieved; // Subscribe to name retrieval event

            // Start scanning process when the page is created.
            StartScanningProcess();
        }

        /// <summary>
        /// Initializes the UI components for the page.
        /// </summary>
        private void InitializeComponent()
        {
            var mainLayoutContainer = Resources.CreateMainLayoutContainer();
            Content = mainLayoutContainer;

            _statusLabel = Resources.CreateDetailLabel("Preparing to scan...");
            _statusLabel.BackgroundColor = Color.Transparent;
            mainLayoutContainer.Add(_statusLabel);

            _deviceListContentContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = new Size2D(0, AppStyles.LayoutCellPadding.Height) // Use style for spacing
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            _deviceScrollableList = Resources.CreateScrollableList();
            _deviceScrollableList.Add(_deviceListContentContainer);
            mainLayoutContainer.Add(_deviceScrollableList);
        }


        private async void StartScanningProcess()
        {
            ClearDeviceList();
            _statusLabel.Text = "Scanning for devices...";
            _isScanActive = true;
            await _bleService.StartLeScanAsync();

            // Simulate a 30-second scan duration like the original app
            await System.Threading.Tasks.Task.Delay(30000);

            if (_isScanActive) // Check if still active (e.g., user didn't navigate away)
            {
                await StopScanningProcessAsync();
            }
        }

        private async System.Threading.Tasks.Task StopScanningProcessAsync()
        {
            if (_isScanActive)
            {
                _isScanActive = false;
                await _bleService.StopLeScanAsync();
                _statusLabel.Text = "Scan completed. Tap a device to connect.";
            }
        }

        private void ClearDeviceList()
        {
            _discoveredDeviceLabels.Clear();
            while (_deviceListContentContainer.ChildCount > 0)
            {
                _deviceListContentContainer.Remove(_deviceListContentContainer.GetChildAt(0));
            }
        }

        /// <summary>
        /// Event handler for when a new device is discovered by the BLE service.
        /// </summary>
        private void OnDeviceDiscovered(object sender, AdapterLeScanResultChangedEventArgs e)
        {
            if (e.DeviceData != null && !string.IsNullOrEmpty(e.DeviceData.RemoteAddress))
            {
                // Ensure UI updates happen on the main thread
                _uiContext.Post(_ =>
                {
                    if (!_discoveredDeviceLabels.ContainsKey(e.DeviceData.RemoteAddress))
                    {
                        string initialName = null;
                        try
                        {
                            // Attempt to get the name from scan data using a default packet type (0).
                            // This might resolve the obsolete warning and provide an early name.
                            initialName = e.DeviceData.GetDeviceName(0);
                        }
                        catch (Exception ex)
                        {
                            Tizen.Log.Warn("LescannerDeviceListPage", $"Error calling GetDeviceName(0) for {e.DeviceData.RemoteAddress}: {ex.Message}. Will use address.");
                        }
                        AddDeviceToList(e.DeviceData.RemoteAddress, initialName); 
                        Tizen.Log.Info("LescannerDeviceListPage", $"Device discovered: {e.DeviceData.RemoteAddress}. Initial name from scan: {initialName ?? "N/A"}");
                    }
                }, null);
            }
        }
        
        private void AddDeviceToList(string deviceAddress, string initialName) // Accept address and initial name
        {
            var deviceItemContainer = Resources.CreateListItemContainer();
            deviceItemContainer.Padding = AppStyles.ListElementPadding;

            // Show initial name if available, otherwise show address.
            string initialText = string.IsNullOrEmpty(initialName) ? deviceAddress : $"{initialName} ({deviceAddress})";
            var deviceLabel = Resources.CreateBodyLabel(initialText);
            
            // Make it look tappable
            deviceLabel.TouchEvent += (s, args) =>
            {
                if (args.Touch.GetState(0) == PointStateType.Down)
                {
                    OnDeviceTapped(deviceAddress);
                }
                return true;
            };

            _discoveredDeviceLabels.Add(deviceAddress, deviceLabel); // Store the label
            deviceItemContainer.Add(deviceLabel);
            _deviceListContentContainer.Add(deviceItemContainer);
        }

        /// <summary>
        /// Event handler for when a device name is retrieved after connection.
        /// This handles navigation to ensure we navigate only once with the proper device name.
        /// </summary>
        private void OnDeviceNameRetrieved(object sender, DeviceNameEventArgs e)
        {
            try
            {
                _uiContext.Post(_ =>
                {
                    try
                    {
                        if (_discoveredDeviceLabels.TryGetValue(e.DeviceAddress, out TextLabel deviceLabel))
                        {
                            if (!string.IsNullOrEmpty(e.DeviceName))
                            {
                                deviceLabel.Text = $"{e.DeviceName} ({e.DeviceAddress})";
                                Tizen.Log.Info("LescannerDeviceListPage", $"Updated UI for {e.DeviceAddress} with name: {e.DeviceName}");
                                
                                // Safely update status label to show device name was updated
                                try
                                {
                                    _statusLabel.Text = $"Updated device name: {e.DeviceName}";
                                }
                                catch (Exception statusEx)
                                {
                                    Tizen.Log.Warn("LescannerDeviceListPage", $"Could not update status label: {statusEx.Message}");
                                }
                            }
                            else
                            {
                                // Name might be null if not found or failed to read, keep address only.
                                Tizen.Log.Info("LescannerDeviceListPage", $"Failed to retrieve name for {e.DeviceAddress}. UI remains as address.");
                            }

                            // Navigate to UUID page only when we have a valid device name (not null)
                            if (!string.IsNullOrEmpty(e.DeviceName) && !_navigatedDevices.Contains(e.DeviceAddress))
                            {
                                _navigatedDevices.Add(e.DeviceAddress);
                                string displayName = deviceLabel.Text;
                                Tizen.Log.Info("LescannerDeviceListPage", $"Device name '{e.DeviceName}' retrieved for {e.DeviceAddress}. Navigating to UUID page with display name: {displayName}");
                                _navigator.Push(new UuidListPage(_bleService, _navigator, e.DeviceAddress, displayName));
                            }
                            else if (string.IsNullOrEmpty(e.DeviceName))
                            {
                                Tizen.Log.Info("LescannerDeviceListPage", $"Device name is null or empty for {e.DeviceAddress}. Waiting for name retrieval before navigation.");
                            }
                            else
                            {
                                Tizen.Log.Info("LescannerDeviceListPage", $"Already navigated for {e.DeviceAddress}. Skipping duplicate navigation.");
                            }
                        }
                        else
                        {
                            Tizen.Log.Warn("LescannerDeviceListPage", $"Received name for {e.DeviceAddress} but it's not in the discovered list (maybe navigated away?).");
                        }
                    }
                    catch (Exception ex)
                    {
                        Tizen.Log.Error("LescannerDeviceListPage", $"Exception in OnDeviceNameRetrieved UI thread for {e.DeviceAddress}: {ex.Message}");
                    }
                }, null);
            }
            catch (Exception ex)
            {
                Tizen.Log.Error("LescannerDeviceListPage", $"Exception in OnDeviceNameRetrieved for {e.DeviceAddress}: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the tap event on a device in the list.
        /// </summary>
        /// <param name="deviceAddress">The address of the tapped device.</param>
        private async void OnDeviceTapped(string deviceAddress)
        {
            try
            {
                Tizen.Log.Info("LescannerDeviceListPage", $"Device tapped: {deviceAddress}. Initiating GATT connection.");
                _statusLabel.Text = $"Connecting to {deviceAddress}...";
                
                // Stop scanning before attempting connection
                if (_isScanActive)
                {
                    await StopScanningProcessAsync();
                }

                // Clear any previous navigation state for this device
                _navigatedDevices.Remove(deviceAddress);

                // Initiate the connection. Navigation will be handled by OnDeviceNameRetrieved.
                bool connectionResult = await _bleService.ConnectGattAsync(deviceAddress);
                
                if (!connectionResult)
                {
                    Tizen.Log.Error("LescannerDeviceListPage", $"Failed to initiate connection to {deviceAddress}");
                    _statusLabel.Text = $"Failed to connect, retrying...";
                }
            }
            catch (Exception ex)
            {
                Tizen.Log.Error("LescannerDeviceListPage", $"Exception in OnDeviceTapped for {deviceAddress}: {ex.Message}");
                _statusLabel.Text = $"Connection error: {ex.Message}";
            }
        }

        /// <summary>
        /// Event handler for GATT connection state changes (for feedback on this page if needed).
        /// </summary>
        private void OnGattConnectionStateChanged(object sender, GattConnectionStateChangedEventArgs e)
        {
            try
            {
                _uiContext.Post(_ =>
                {
                    try
                    {
                        if (e.IsConnected)
                        {
                            _statusLabel.Text = "GATT Connected. Fetching device name...";
                            // Navigation will be handled by OnDeviceNameRetrieved to ensure we have the device name
                        }
                        else
                        {
                            _statusLabel.Text = "GATT Disconnected or connection failed.";
                        }
                    }
                    catch (Exception ex)
                    {
                        Tizen.Log.Error("LescannerDeviceListPage", $"Exception in OnGattConnectionStateChanged UI thread: {ex.Message}");
                    }
                }, null);
            }
            catch (Exception ex)
            {
                Tizen.Log.Error("LescannerDeviceListPage", $"Exception in OnGattConnectionStateChanged: {ex.Message}");
            }
        }
    }
}
