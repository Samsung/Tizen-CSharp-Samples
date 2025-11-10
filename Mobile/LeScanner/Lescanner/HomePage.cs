using System;
using Tizen.NUI;
using Tizen.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace Lescanner
{
    /// <summary>
    /// The home page for the LE Scanner application.
    /// Contains a button to initiate BLE scanning.
    /// </summary>
    class HomePage : ContentPage
    {
        private TizenBLEService _bleService;
        private Navigator _navigator;
        private Button _scanButton;
        private TextLabel _statusLabel;

        /// <summary>
        /// Constructor for HomePage.
        /// </summary>
        /// <param name="bleService">The BLE service instance.</param>
        /// <param name="navigator">The navigator for page navigation.</param>
        public HomePage(TizenBLEService bleService, Navigator navigator)
        {
            _bleService = bleService;
            _navigator = navigator;
            AppBar = new AppBar { Title = "LE Scanner" };
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the UI components for the page.
        /// </summary>
        private void InitializeComponent()
        {
            var mainLayoutContainer = Resources.CreateMainLayoutContainer();
            Content = mainLayoutContainer;

            var titleLabel = Resources.CreateTitleLabel("Bluetooth LE Scanner");
            mainLayoutContainer.Add(titleLabel);

            _scanButton = Resources.CreatePrimaryButton("BLE Scan");
            _scanButton.Clicked += OnScanButtonClicked;
            mainLayoutContainer.Add(_scanButton);

            _statusLabel = Resources.CreateDetailLabel("Tap 'BLE Scan' to start.");
            _statusLabel.BackgroundColor = Color.Transparent; // Ensure background is transparent
            mainLayoutContainer.Add(_statusLabel);
        }

        /// <summary>
        /// Handles the click event for the BLE Scan button.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event arguments.</param>
        private void OnScanButtonClicked(object sender, ClickedEventArgs e)
        {
            Tizen.Log.Info("LescannerHomePage", "BLE Scan button clicked.");
            if (_bleService.IsBluetoothEnabled())
            {
                _statusLabel.Text = "Tap 'BLE Scan' to start.";
                // Navigate to DeviceListPage. The page will handle starting the scan.
                _navigator.Push(new DeviceListPage(_bleService, _navigator));
            }
            else
            {
                _statusLabel.Text = "Please turn on Bluetooth.";
                // Optionally, show a more persistent message or a Toast.
                Tizen.Log.Warn("LescannerHomePage", "Bluetooth is not enabled.");
            }
        }
    }
}
