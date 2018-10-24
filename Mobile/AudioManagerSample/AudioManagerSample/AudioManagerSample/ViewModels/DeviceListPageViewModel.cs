/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace AudioManagerSample
{
    /// <summary>
    /// ViewModel for DeviceListPage.
    /// </summary>
    class DeviceListPageViewModel : BaseViewModel
    {
        private Page usbConfPage;

        private IEnumerable<DeviceItem> _items;
        public IEnumerable<DeviceItem> Items
        {
            get => _items;
            set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged(nameof(Items));

                    ConfigButtonEnable = false;
                }
            }
        }

        private DeviceItem _selectedItem;
        public DeviceItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;

                    OnPropertyChanged(nameof(SelectedItem));

                    if (_selectedItem.IsUsbOutputDevice)
                        _configButtonEnable = true;
                    else
                        _configButtonEnable = false;

                    OnPropertyChanged(nameof(ConfigButtonEnable));
                }
            }
        }

        private bool _configButtonEnable;
        public bool ConfigButtonEnable
        {
            get => _configButtonEnable;
            set
            {
                if (_configButtonEnable != value)
                {
                    _configButtonEnable = value;

                    OnPropertyChanged(nameof(ConfigButtonEnable));
                }
            }
        }

        public DeviceListPageViewModel()
        {
            NavigateCommand = new Command<Type>(async (Type pageType) =>
            {
                usbConfPage = (Page)Activator.CreateInstance(pageType);

                usbConfPage.BindingContext = new UsbConfigurePageViewModel(_selectedItem);

                await Application.Current.MainPage.Navigation.PushAsync(usbConfPage);
            });

            AMController.DeviceConnectionChanged += OnConnectionChanged;
            AMController.DeviceRunningChanged += OnRunningChanged;
        }

        public ICommand NavigateCommand { private set; get; }

        protected IAudioManagerController AMController => DependencyService.Get<IAudioManagerController>();

        private void OnConnectionChanged(object sender, DeviceConnectionChangedEventArgs e)
        {
            ConnectionChangedDeviceLabel = e.Device.Type + "(ID:" + e.Device.Id + ") is " + (e.IsConnected ? "connected" : "disconnected");

            Items = DependencyService.Get<IAudioManagerController>().GetConnectedDevices();
        }

        private void OnRunningChanged(object sender, DeviceRunningChangedEventArgs e)
        {
            Log.Debug(e.Device.Type + "(ID:" + e.Device.Id + ")'s state changed to [" + e.Device.State + "]");

            Items = DependencyService.Get<IAudioManagerController>().GetConnectedDevices();
        }

        private string _connectionChangedDeviceLabel;
        public string ConnectionChangedDeviceLabel
        {
            get => _connectionChangedDeviceLabel;
            set
            {
                if (_connectionChangedDeviceLabel != value)
                {
                    _connectionChangedDeviceLabel = value;

                    OnPropertyChanged(nameof(ConnectionChangedDeviceLabel));

                    Log.Debug(value);
                }
            }
        }

        public override void OnPopped()
        {
            base.OnPopped();

            AMController.DeviceConnectionChanged -= OnConnectionChanged;
            AMController.DeviceRunningChanged -= OnRunningChanged;
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            Items = DependencyService.Get<IAudioManagerController>().GetConnectedDevices();
        }
    }
}