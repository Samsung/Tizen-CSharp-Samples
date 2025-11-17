using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading; // Added for SynchronizationContext
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;
using Tizen.Network.Bluetooth; // For GattConnectionStateChangedEventArgs

namespace Lescanner
{
    /// <summary>
    /// Page to display a list of discovered service UUIDs for a connected BLE device.
    /// </summary>
    class UuidListPage : ContentPage
    {
        private TizenBLEService _bleService;
        private Navigator _navigator;
        private string _deviceAddress;
        private string _deviceDisplayName; // Added to store the fetched display name
        private TextLabel _statusLabel;
        private ScrollableBase _uuidScrollableList;
        private View _uuidListContentContainer;
        private SynchronizationContext _uiContext; // Added for main thread dispatching

        /// <summary>
        /// Constructor for UuidListPage.
        /// </summary>
        /// <param name="bleService">The BLE service instance.</param>
        /// <param name="navigator">The navigator for page navigation.</param>
        /// <param name="deviceAddress">The address of the connected device.</param>
        /// <param name="displayName">The display name (address or name) of the connected device.</param>
        public UuidListPage(TizenBLEService bleService, Navigator navigator, string deviceAddress, string displayName)
        {
            _bleService = bleService;
            _navigator = navigator;
            _deviceAddress = deviceAddress;
            _deviceDisplayName = displayName ?? deviceAddress; // Fallback to address if displayName is null
            _uiContext = SynchronizationContext.Current; // Capture main UI thread context
            AppBar = new AppBar { Title = $"UUIDs for {_deviceDisplayName}" }; // Use display name
            InitializeComponent();

            // Subscribe to BLE service events
            _bleService.GattConnectionStateChanged += OnGattConnectionStateChanged;
            _bleService.ServicesDiscovered += OnServicesDiscovered;
            
            // Check if we're already connected and services might already be discovered
            CheckExistingConnectionAndServices();
        }

        /// <summary>
        /// Override Dispose to properly unsubscribe from events
        /// </summary>
        protected override void Dispose(DisposeTypes type)
        {
            try
            {
                if (_bleService != null)
                {
                    _bleService.GattConnectionStateChanged -= OnGattConnectionStateChanged;
                    _bleService.ServicesDiscovered -= OnServicesDiscovered;
                    Tizen.Log.Info(Constants.LOG_TAG, "Unsubscribed from BLE service events.");
                }
            }
            catch (Exception ex)
            {
                Tizen.Log.Error(Constants.LOG_TAG, $"Error unsubscribing from BLE service events: {ex.Message}");
            }
            
            base.Dispose(type);
        }

        /// <summary>
        /// Initializes the UI components for the page.
        /// </summary>
        private void InitializeComponent()
        {
            var mainLayoutContainer = Resources.CreateMainLayoutContainer();
            Content = mainLayoutContainer;

            _statusLabel = Resources.CreateDetailLabel($"Connecting to {_deviceDisplayName}..."); // Use display name
            _statusLabel.BackgroundColor = Color.Transparent;
            mainLayoutContainer.Add(_statusLabel);

            _uuidListContentContainer = new View
            {
                Layout = new LinearLayout
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    CellPadding = new Size2D(0, AppStyles.LayoutCellPadding.Height) // Use style for spacing
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
            };

            _uuidScrollableList = Resources.CreateScrollableList();
            _uuidScrollableList.Add(_uuidListContentContainer);
            mainLayoutContainer.Add(_uuidScrollableList);
        }


        /// <summary>
        /// Event handler for GATT connection state changes.
        /// </summary>
        private void OnGattConnectionStateChanged(object sender, GattConnectionStateChangedEventArgs e)
        {
            // Ensure UI updates happen on the main thread
            _uiContext.Post(_ => // Changed from NUIApplication.GetDefaultWindow().Post
            {
                if (e.IsConnected)
                {
                    _statusLabel.Text = $"Connected to {_deviceDisplayName}. Fetching services..."; // Use display name
                    Tizen.Log.Info(Constants.LOG_TAG, $"GATT connected to {_deviceAddress} ({_deviceDisplayName}).");
                }
                else
                {
                    _statusLabel.Text = $"Disconnected from {_deviceDisplayName}."; // Use display name
                    Tizen.Log.Warn(Constants.LOG_TAG, $"GATT disconnected from {_deviceAddress} ({_deviceDisplayName}).");
                    // Optionally, navigate back or show an error.
                }
            }, null); // Added null for state parameter
        }

        /// <summary>
        /// Event handler for when services are discovered.
        /// </summary>
        private void OnServicesDiscovered(object sender, IEnumerable<string> uuids)
        {
            // Ensure UI updates happen on the main thread
            _uiContext.Post(_ => // Changed from NUIApplication.GetDefaultWindow().Post
            {
                Tizen.Log.Info(Constants.LOG_TAG, $"Services discovered event received for {_deviceDisplayName}. Count: {uuids?.Count() ?? 0}");
                ClearUuidList();
                if (uuids != null && uuids.Any())
                {
                    _statusLabel.Text = $"Found {uuids.Count()} services for {_deviceDisplayName}:"; // Use display name
                    foreach (var uuid in uuids)
                    {
                        AddUuidToList(uuid);
                    }
                }
                else
                {
                    _statusLabel.Text = $"No services found for {_deviceDisplayName}."; // Use display name
                }
            }, null); // Added null for state parameter
        }

        private void ClearUuidList()
        {
            while (_uuidListContentContainer.ChildCount > 0)
            {
                _uuidListContentContainer.Remove(_uuidListContentContainer.GetChildAt(0));
            }
        }

        private void AddUuidToList(string uuid)
        {
            var uuidItemContainer = Resources.CreateListItemContainer();
            uuidItemContainer.Padding = AppStyles.ListElementPadding;

            var uuidLabel = Resources.CreateBodyLabel(uuid);
            uuidLabel.TextColor = AppStyles.TextColorSecondary; // Use secondary color for UUIDs

            uuidItemContainer.Add(uuidLabel);
            _uuidListContentContainer.Add(uuidItemContainer);
        }

        /// <summary>
        /// Checks if we're already connected to the device and services have been discovered.
        /// This handles the case where navigation happens after services are already discovered.
        /// </summary>
        private void CheckExistingConnectionAndServices()
        {
            try
            {
                string currentConnectedAddress = _bleService.CurrentConnectedDeviceAddress;
                
                if (!string.IsNullOrEmpty(currentConnectedAddress) && currentConnectedAddress == _deviceAddress)
                {
                    Tizen.Log.Info(Constants.LOG_TAG, $"Already connected to {_deviceAddress}. Checking for existing services.");
                    
                    // Get current services
                    var existingServices = _bleService.GetCurrentServices();
                    
                    if (existingServices != null && existingServices.Any())
                    {
                        Tizen.Log.Info(Constants.LOG_TAG, $"Found {existingServices.Count()} existing services for {_deviceAddress}.");
                        _uiContext.Post(_ => 
                        {
                            ClearUuidList();
                            _statusLabel.Text = $"Found {existingServices.Count()} services for {_deviceDisplayName}:";
                            foreach (var uuid in existingServices)
                            {
                                AddUuidToList(uuid);
                            }
                        }, null);
                    }
                    else
                    {
                        Tizen.Log.Info(Constants.LOG_TAG, $"No existing services found for {_deviceAddress}. Waiting for service discovery.");
                        _uiContext.Post(_ => 
                        {
                            _statusLabel.Text = $"Connected to {_deviceDisplayName}. Discovering services...";
                        }, null);
                    }
                }
                else
                {
                    Tizen.Log.Info(Constants.LOG_TAG, $"Not connected to {_deviceAddress} or connected to different device. Current: {currentConnectedAddress}");
                    _uiContext.Post(_ => 
                    {
                        _statusLabel.Text = $"Connecting to {_deviceDisplayName}...";
                    }, null);
                }
            }
            catch (Exception ex)
            {
                Tizen.Log.Error(Constants.LOG_TAG, $"Error checking existing connection: {ex.Message}");
                _uiContext.Post(_ => 
                {
                    _statusLabel.Text = $"Error checking connection status.";
                }, null);
            }
        }
    }
}
