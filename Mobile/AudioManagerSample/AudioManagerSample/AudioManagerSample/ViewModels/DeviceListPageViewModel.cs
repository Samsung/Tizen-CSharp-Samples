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

using System.Collections.Generic;
using Xamarin.Forms;

namespace AudioManagerSample
{
    /// <summary>
    /// ViewModel for DeviceListPage.
    /// </summary>
    class DeviceListPageViewModel : BaseViewModel
	{
        public IEnumerable<DeviceItem> Items { get; protected set; }

        private DeviceItem _selecetdItem;

        public DeviceItem SelectedItem
        {
            get => _selecetdItem;
            set
            {
                if (_selecetdItem != value)
                {
                    _selecetdItem = value;

                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public DeviceListPageViewModel ()
		{
            Items = DependencyService.Get<IAudioManagerController>().GetConnectedDevices();

            AMController.DeviceConnectionChanged += (s, e) => ConnectionChangedDeviceLabel = e.Device.Type + "(ID:" + e.Device.Id + ") is " + (e.IsConnected ? "connected" : "disconnected");
        }

        protected IAudioManagerController AMController => DependencyService.Get<IAudioManagerController>();

        private string _ConnectionChangedDeviceLabel;
        public string ConnectionChangedDeviceLabel
        {
            get => _ConnectionChangedDeviceLabel;
            set
            {
                _ConnectionChangedDeviceLabel = value;

                OnPropertyChanged(nameof(ConnectionChangedDeviceLabel));

                Items = DependencyService.Get<IAudioManagerController>().GetConnectedDevices();

                OnPropertyChanged(nameof(Items));
            }
        }
    }
}