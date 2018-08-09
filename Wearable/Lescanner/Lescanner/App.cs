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



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tizen.Network.Bluetooth;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace Lescanner
{
    /// <summary>
    /// The LE Scanner application.
    /// It scans for LE devices around and discover 
    /// the services they support after GATT connection.
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Contains list of detected LE devices.
        /// </summary>
        public CirclePage DevicePage;
        /// <summary>
        /// Contains list of UUIDs 
        /// detected in remote LE device.
        /// </summary>
        public CirclePage UuidPage;
        /// <summary>
        /// Starts LE scan.
        /// </summary>
        public Button ScanBtn;
        /// <summary>
        /// Shows detected devices in a 
        /// vertically scrolling list.
        /// </summary>
        public CircleListView DeviceListView;
        /// <summary>
        /// Contains detected device 
        /// objects and their addresses.
        /// </summary>
        public List<Device> DeviceList = new List<Device>();
        /// <summary>
        /// Shows detected UUIDs in a 
        /// vertically scrolling list.
        /// </summary>
        public CircleListView UuidListView;
        /// <summary>
        /// Contains the detected UUIDs.
        /// </summary>
        public List<string> UuidList = new List<string>();
        /// <summary>
        /// Shows a small animation while
        /// LE scan is ongoing.
        /// </summary>
        public Label ScanLabel = new Label();
        /// <summary>
        /// Shows a small animation while
        /// Gatt Connection is in progress.
        /// </summary>
        public Label GattLabel = new Label();
        /// <summary>
        /// GattClient object is created after
        /// GATT connection is established.
        /// </summary>
        public BluetoothGattClient GattClient = null;
        /// <summary>
        /// Indicates whether GATT connection 
        /// succeeded or not.
        /// </summary>
        public bool StateChangedFlag = false;

        /// <summary>
        /// Sets initial application page
        /// </summary>
        public App()
        {
            ScanBtn = new Button
            {
                Text = "BLE Scan",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            ScanBtn.Clicked += OnScanButtonClicked;

            var nameLabel = new Label
            {
                Text = "LE Scanner",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            CirclePage content = new CirclePage
            {
                Content = new StackLayout
                {
                    Spacing = 20,
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        nameLabel,
                        ScanBtn
                    }
                },
            };
            NavigationPage.SetHasNavigationBar(content, false);

            MainPage = new NavigationPage(content);
        }

        /// <summary>
        /// Whenever any new device is found, this event handler 
        /// will be called. It will add the detected device 
        /// into DeviceList only if the device wasn't added before.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// /// <param name="e">Event arguments</param>
        public void ScanResultEventHandler(object sender, AdapterLeScanResultChangedEventArgs e)
        {
            if (e.DeviceData != null)
            {
                Device device = new Device
                {
                    Address = e.DeviceData.RemoteAddress,
                    Ledevice = e.DeviceData
                };

                if (!DeviceList.Any(d => d.Address == device.Address))
                {
                    DeviceList.Add(device);
                }
            }
        }

        /// <summary>
        /// Manages the ScanLabel animation during
        /// devices scanning. It also add 30s delay to
        /// complete the scan.
        /// </summary>
        /// <returns>Task</returns>
        public async Task WaitTillScan()
        {
            int count = 0;
            while (true)
            {
                await Task.Delay(3000);
                if (count % 4 == 0)
                {
                    ScanLabel.Text = "Scanning";
                }

                ScanLabel.Text += ".";
                count++;
                if (count == 10)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Starts and stops the LE scan. Calls event handler
        /// for ScanResultChanged whenever any LE device is found.
        /// </summary>
        /// <returns>Task</returns>
        public async Task LeScanSetup()
        {
            BluetoothAdapter.ScanResultChanged += ScanResultEventHandler;

            BluetoothAdapter.StartLeScan();
            await WaitTillScan();

            BluetoothAdapter.StopLeScan();
            await Task.Delay(5000);

            BluetoothAdapter.ScanResultChanged -= ScanResultEventHandler;
        }

        /// <summary>
        /// Called when "BLE Scan" button is clicked.
        /// Checks for BT status and proceeds to scan 
        /// LE devices and rendering the device page.
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Event arguments</param>
        void OnScanButtonClicked(object s, EventArgs e)
        {
            try
            {
                if (!BluetoothAdapter.IsBluetoothEnabled)
                {
                    Toast.DisplayText("Please turn on Bluetooth.");
                }
                else
                {
                    DeviceList.Clear();
                    RenderDevicePage();
                }
            }
            catch (Exception ex)
            {
                Toast.DisplayText("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a CirclePage with list of detected LE devices.
        /// </summary>
        public async void RenderDevicePage()
        {
            if (DeviceListView == null)
            {
                CreateDeviceListView();
            }

            ScanLabel.Text = "Scanning";
            ScanLabel.HorizontalOptions = LayoutOptions.Center;

            CirclePage content = new CirclePage
            {
                RotaryFocusObject = DeviceListView,
                Content = new StackLayout
                {
                    Padding = new Thickness(0, 30),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Children =
                    {
                        ScanLabel,
                        DeviceListView,
                    }
                },
            };
            NavigationPage.SetHasNavigationBar(content, false);

            DevicePage = content;

            await MainPage.Navigation.PushAsync(DevicePage);
            await LeScanSetup();
            ScanLabel.Text = "Scan completed";
            DeviceListView.ItemsSource = null; // Refreshing doesn't work without it
            DeviceListView.ItemsSource = DeviceList;
        }

        /// <summary>
        /// Creates a new ListView for listing detected devices.
        /// </summary>
        public void CreateDeviceListView()
        {
            DeviceListView = new CircleListView
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            DeviceListView.ItemsSource = DeviceList;
            DeviceListView.ItemTemplate = new DataTemplate(() =>
            {
                TextCell textCell = new TextCell();
                textCell.SetBinding(TextCell.TextProperty, "Address");
                return textCell;
            });
            DeviceListView.ItemTapped += DeviceTapped;
        }

        /// <summary>
        /// Waits until GATT connection is successfull adding 
        /// small animation during connecting. Sets 30 second
        /// time limit for connect.
        /// </summary>
        /// <returns>Task</returns>
        public async Task WaitStateChangedFlag()
        {
            int count = 0;
            do
            {
                await Task.Delay(3000);
                if (count % 4 == 0)
                {
                    GattLabel.Text = "GattConnect\n" +
                                       "initiated";
                }

                GattLabel.Text += ".";
                count++;
            } while (count < 10 && !StateChangedFlag);
        }

        /// <summary>
        /// Unregisters GattConnectionStateChanged event handler
        /// and breaks the connection with GATT and GATT Client.
        /// </summary>
        /// <param name="leDevice">Bluetooth LE Device</param>
        public void GattClientExit(BluetoothLeDevice leDevice)
        {
            if (leDevice != null)
            {
                leDevice.GattConnectionStateChanged -= Device_GattConnectionStateChanged;
                leDevice.GattDisconnect();
                leDevice = null;
                GattClient.DestroyClient();
                GattClient = null;
            }

            StateChangedFlag = false;
        }

        ///<summary>
        /// Called after tapping device from DeviceListView.
        /// Creates new CirclePage with list of tapped device
        /// service UUIDs.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        public async void DeviceTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                Device device = e.Item as Device;

                if (UuidListView == null)
                {
                    CreateUUIDListView();
                }
                UuidList.Clear();

                CirclePage content = new CirclePage
                {
                    RotaryFocusObject = UuidListView,
                    Content = new StackLayout
                    {
                        Padding = new Thickness(0, 30),

                        Children =
                        {
                                GattLabel,
                                UuidListView,
                        }
                    },
                };
                NavigationPage.SetHasNavigationBar(content, false);

                GattLabel.Text = "GattConnect\n" +
                                    "initiated";
                GattLabel.HorizontalTextAlignment = TextAlignment.Center;

                UuidPage = content;
                await MainPage.Navigation.PushAsync(UuidPage);

                device.Ledevice.GattConnectionStateChanged += Device_GattConnectionStateChanged;

                GattClient = device.Ledevice.GattConnect(false);
                await WaitStateChangedFlag();

                GattLabel.Text = "GattConnected";

                foreach (BluetoothGattService srv in GattClient.GetServices())
                {
                    UuidList.Add(srv.Uuid);
                }

                UuidListView.ItemsSource = null; // Refreshing doesn't work without it
                UuidListView.ItemsSource = UuidList;
                GattClientExit(device.Ledevice);
            }
        }

        /// <summary>
        /// Creates a new ListView for listing UUIDs.
        /// </summary>
        public void CreateUUIDListView()
        {
            UuidListView = new CircleListView
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            UuidListView.ItemsSource = UuidList;
            UuidListView.ItemTemplate = new DataTemplate(() =>
            {
                var textCell = new TextCell();
                textCell.SetBinding(TextCell.TextProperty, ".");
                return textCell;
            });
        }

        /// <summary>
        /// Called after any change of GATT connection state.
        /// Sets the StateChangedFlag to true if GATT is connected.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void Device_GattConnectionStateChanged(object sender, GattConnectionStateChangedEventArgs e)
        {
            if (e.IsConnected == true)
            {
                StateChangedFlag = true;
            }
        }

        /// <summary>
        /// Called when LE Scanner application terminates.
        /// It happens when LE Scanner application exits via BACK key.
        /// </summary>
        public void Terminate()
        {
            // Handle when your app terminates.
        }

        /// <summary>
        /// Called when LE Scanner application starts.
        /// It happens when LE Scanner application is shown at first.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts.
        }

        /// <summary>
        /// Called when LE Scanner application sleeps.
        /// It happens when LE Scanner application
        /// goes background via HOME or BACK key.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps.
        }

        /// <summary>
        /// Called when LE Scanner application resumes.
        /// It happens when LE Scanner application goes foreground.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes.
        }
    }
    /// <summary>
    /// Device class to store devices detected during
    /// LE scan.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Holds the detected remote device object.
        /// </summary>
        public BluetoothLeDevice Ledevice;
        /// <summary>
        /// Contains the Bluetooth address
        /// of the detected remote object.
        /// </summary>
        public string Address { get; set; }
    }
}
