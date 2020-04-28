/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Tizen.Network.Bluetooth;
using System.Threading.Tasks;

namespace Lescanner
{
    /// <summary>
    /// The LE Scanner application.
    /// It will scan for LE devices around and discover the services
    /// it supports after GATT connection.
    /// The application is designed in a three Navigation Page.
    /// 1. Home Page   -> Consists BLE scan button. OnClick, it starts scanning for
    ///                   BLE devices around.
    /// 2. Device Page -> Consists list of devices discovered via BLE scan.
    ///                   OnTapped, it will issue GATT connect to that device.
    /// 3. UUID page   -> Consists list of service uuids the device supports.
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// HomePage is a Navigation Page contains page title
        /// and BLE Scan button.
        /// </summary>
        public NavigationPage HomePage;
        /// <summary>
        /// DevicePage is also a Navigation Page contains list
        /// of discovered LE devices.
        /// </summary>
        public NavigationPage DevicePage;
        /// <summary>
        /// UuidPage is also a Navigation Page contains list
        /// of UUIDs discovered in remote LE device.
        /// </summary>
        public NavigationPage UuidPage;

        /// <summary>
        /// ScanBtn is a Button which onclicked
        /// starts LE scan.
        /// </summary>
        public Button ScanBtn;
        /// <summary>
        /// DeviceListView that shows discovered devices
        /// in a vertically scrolling list.
        /// </summary>
        public ListView DeviceListView;
        /// <summary>
        /// DeviceList contains the discovered device objects
        /// and its address.
        /// </summary>
        public List<Device> DeviceList = new List<Device>();
        /// <summary>
        /// UuidListView that shows discovered UUIDs
        /// in a vertically scrolling list.
        /// </summary>
        public ListView UuidListView;
        /// <summary>
        /// UuidList contains the discovered UUIDs.
        /// </summary>
        public List<string> UuidList = new List<string>();
        /// <summary>
        /// ScanLabel shows a small animation while
        /// LE scan is ongoing.
        /// </summary>
        public Label ScanLabel = new Label();
        /// <summary>
        /// GattLabel shows a small animation while
        /// Gatt Connection is in progress.
        /// </summary>
        public Label GattLabel = new Label();

        /// <summary>
        /// GattClient object is created after
        /// GATT connection is established.
        /// </summary>
        public BluetoothGattClient GattClient = null;
        /// <summary>
        /// StateChanged_flag is a boolean value.
        /// It indicates whether GATT connection
        /// succeeded or not.
        /// </summary>
        public bool StateChanged_flag = false;

        /// <summary>
        /// Device class to store devices discovered during
        /// LE scan.
        /// </summary>
        public class Device
        {
            /// <summary>
            /// Ledevice holds the discovered remote
            /// device object.
            /// </summary>
            public BluetoothLeDevice Ledevice;
            /// <summary>
            /// Address contains the discovered remote
            /// object bluetooth address.
            /// </summary>
            public string Address { get; set; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// Creates a StackLayout with BLE Scan button on a ContentPage.
        /// Then creates a new NavigationPage using the ContentPage, because
        /// NaviagtionPage manages navigation and user-experience of a
        /// stack of other pages. And then sets this NavigationPages a
        /// MainPage.
        /// </summary>
        public App()
        {
            ScanBtn = new Button
            {
                Text = "BLE Scan",
                BackgroundColor = Color.Aqua,
                HorizontalOptions = LayoutOptions.Center,
            };
            ScanBtn.Clicked += OnScanButtonClicked;

            ContentPage content = new ContentPage
            {
                Title = "LE Scanner",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        ScanBtn,
                    }
                },
            };

            HomePage = new NavigationPage(content);
            MainPage = HomePage;
        }

        /// <summary>
        /// Whenever any new device is found, this callback will be called.
        /// It will add the discovered device into devicelist.
        /// It loops through the previously discovered devices and
        /// insert the device only if it not there.
        /// </summary>
        /// <param name="sender">object</param>
        /// /// <param name="e">AdapterLeScanResultChangedEventArgs</param>
        public void ScanResultEventHandler(object sender, AdapterLeScanResultChangedEventArgs e)
        {
            if (!e.DeviceData.Equals(null))
            {
                bool Duplicate = false;
                Device device = new Device();

                device.Address = e.DeviceData.RemoteAddress;
                device.Ledevice = e.DeviceData;

                // Checks for duplicate device address.
                foreach (Device dev in DeviceList)
                {
                    if (dev.Address.Equals(device.Address))
                    {
                        Duplicate = true;
                    }
                }

                if (!Duplicate)
                {
                    DeviceList.Add(device);
                }
            }
        }

        /// <summary>
        /// This function adds 30s delay for scan to complete.
        /// Inaddition to that, using ScanLabel it manages
        /// the small animation during the LE scan.
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
        /// This function starts and stops the LE scan.
        /// Before starting the LE scan, it registers the
        /// event handler for ScanResultChanged, which is
        /// called whenever any LE device found.
        /// Then it waits for 30s and after device discovery,
        /// it stops the LE scan.
        /// Finally it unregisters the
        /// event handler for ScanResultChanged.
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
        /// This function is called when "BLE Scan" button is clicked.
        /// It checks for BT status and notify the user
        /// "Please turn on Bluetooth" incase the BT state is OFF.
        /// If BT state is ON, it proceeds to scan LE devices and
        /// start rendering the device page.
        /// </summary>
        /// <param name="s">object</param>
        /// <param name="e">EventArgs</param>
        void OnScanButtonClicked(object s, EventArgs e)
        {
            try
            {
                if (!BluetoothAdapter.IsBluetoothEnabled)
                {
                    // Toast.DisplayText("Please turn on Bluetooth.");
                }
                else
                {
                    RenderDevicePage();
                }
            }
            catch (NotSupportedException)
            {
                // Not supported!
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Creates a StackLayout with DeviceListView and ScanLabel
        /// on a ContentPage. Then creates a new NavigationPage using
        /// the ContentPage, because NaviagtionPage manages navigation
        /// and user-experience of a stack of other pages.And then
        /// it pushes this NaviagationPage on top of MainPage.
        /// After pushing, LE scan is initiated. Once it is
        /// completed it creates a new page in which all the
        /// discoverd devices are populated in list view.
        /// </summary>
        public async void RenderDevicePage()
        {
            if (DeviceListView == null)
            {
                CreateDeviceListView();
            }

            ScanLabel.Text = "Scanning";
            ScanLabel.FontAttributes = FontAttributes.Bold;
            ScanLabel.FontSize = 24;

            ContentPage content = new ContentPage
            {
                Title = "Device List",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        ScanLabel,
                        DeviceListView,
                    }
                },
            };

            DevicePage = new NavigationPage(content);
            await MainPage.Navigation.PushAsync(DevicePage);
            await LeScanSetup();
            ScanLabel.Text = "Scan completed";
            DeviceListView.ItemsSource = null;
            DeviceListView.ItemsSource = DeviceList;
            // Toast.DisplayText("Tap device address to initiate GATT connect.");
        }

        /// <summary>
        /// Creates a new ListView for listing discoverd devices.
        /// Here list of LEDevice objects is given as ItemSource to ListView.
        /// In that Address property is binded to label, which will be shown
        /// in the UI.
        /// </summary>
        public void CreateDeviceListView()
        {
            DeviceListView = new ListView
            {
                Margin = new Thickness(0, 0, 0, 0),
                HeightRequest = 500,
                RowHeight = 120,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            DeviceListView.ItemsSource = DeviceList;
            DeviceListView.ItemTemplate = new DataTemplate(() =>
            {
                Label nameLabel = new Label();
                nameLabel.SetBinding(Label.TextProperty, "Address");
                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Padding = new Thickness(0, 5),
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new StackLayout
                            {
                                VerticalOptions = LayoutOptions.Center,
                                Spacing = 0,
                                Children =
                                {
                                    nameLabel,
                                }
                            }
                        }
                    }
                };
            });
            DeviceListView.ItemTapped += DeviceTapped;
        }

        /// <summary>
        /// This function adds a delay and waits until
        /// GATT connection is successful. Inaddtion to
        /// that it manages a small animation using
        /// GattLabel during the GATT connection.
        /// </summary>
        /// <returns>Task</returns>
        public async Task WaitStateChangedFlag()
        {
            int count = 0;
            while (true)
            {
                await Task.Delay(3000);
                if (count % 4 == 0)
                {
                    GattLabel.Text = "GattConnect initiated";
                }

                GattLabel.Text += ".";
                count++;
                if (StateChanged_flag)
                {
                    break;
                }

                if (count == 10)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// This function does all post processing.
        /// First it unregisters the event handler registered
        /// for GattConnectionStateChanged. Then it iniates GATT
        /// disconnect. Finally it destroys the GATT Client.
        /// </summary>
        /// <param name="leDevice">BluetoothLeDevice</param>
        public void GattClientExit(BluetoothLeDevice leDevice)
        {
            if (!leDevice.Equals(null))
            {
                GattClient.ConnectionStateChanged -= Device_GattConnectionStateChanged;
                GattClient.DisconnectAsync();
                leDevice = null;
                GattClient.Dispose();
                GattClient = null;
            }

            StateChanged_flag = false;
        }

        /// <summary>
        /// This function is called when any discovered device
        /// is tapped from devicelistview. First it creates a StackLayout
        /// with UuidListView and GattLabel on a ContentPage.
        /// Then creates a new NavigationPage using the ContentPage,
        /// because NaviagtionPage manages navigation and user-experience
        /// of a stack of other pages. And then it pushes this NaviagationPage
        /// on top of MainPage. Once the page is rendered it registers for the
        /// GattConnectionStateChanged event handler. Then it issues
        /// GATT connection to the tapped device. It waits until GATT connection
        /// is successfull, then it fetches all the service uuids it supports
        /// and renders it in the new page. Finally it does GATT disconnect and
        /// all postprocessing.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ItemTappedEventArgs</param>
        public async void DeviceTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                Device device = e.Item as Device;

                if (UuidListView == null)
                {
                    CreateUUIDListView();
                }

                ContentPage content = new ContentPage
                {
                    Title = "UUID List",
                    Content = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.Center,
                        Children =
                        {
                                GattLabel,
                                UuidListView,
                        }
                    },
                };

                GattLabel.Text = "GattConnect initiated";
                GattLabel.FontAttributes = FontAttributes.Bold;
                GattLabel.FontSize = 24;

                UuidPage = new NavigationPage(content);
                await MainPage.Navigation.PushAsync(UuidPage);

                GattClient = BluetoothGattClient.CreateClient(device.Address);
                await GattClient.ConnectAsync(false);
                GattClient.ConnectionStateChanged += Device_GattConnectionStateChanged;

                await WaitStateChangedFlag();

                GattLabel.Text = "GattConnected";
                IEnumerable<BluetoothGattService> srv_list;
                srv_list = GattClient.GetServices();

                foreach (BluetoothGattService srv in srv_list)
                {
                    UuidList.Add(srv.Uuid);
                }

                UuidListView.ItemsSource = null;
                UuidListView.ItemsSource = UuidList;
                GattClientExit(device.Ledevice);
            }
        }

        /// <summary>
        /// Creates a new ListView for listing UUIDs.
        /// Here list of uuids(string) is given as ItemSource to ListView.
        /// In that uuid string is binded to label, which will be shown
        /// in UI.
        /// </summary>
        public void CreateUUIDListView()
        {
            UuidListView = new ListView
            {
                Margin = new Thickness(0, 0, 0, 0),
                HeightRequest = 500,
                RowHeight = 120,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            UuidListView.ItemsSource = UuidList;
            UuidListView.ItemTemplate = new DataTemplate(() =>
            {
                Label uuidLabel = new Label();
                uuidLabel.SetBinding(Label.TextProperty, ".");
                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Padding = new Thickness(0, 5),
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new StackLayout
                            {
                                VerticalOptions = LayoutOptions.Center,
                                Spacing = 0,
                                Children =
                                {
                                    uuidLabel,
                                }
                            }
                        }
                    }
                };
            });
        }

        /// <summary>
        /// This function is called when any change in GATT connection,
        /// either GATT connected or GATT disconnected.
        /// It will set the falg to true only if it connected.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">GattConnectionStateChangedEventArgs</param>
        private void Device_GattConnectionStateChanged(object sender, GattConnectionStateChangedEventArgs e)
        {
            if (e.IsConnected == true)
            {
                StateChanged_flag = true;
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
}
