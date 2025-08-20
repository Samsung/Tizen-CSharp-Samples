/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using SystemInfo.Model.Capability;
using SystemInfo.Utils;
using SystemInfo.ViewModel.List;

namespace SystemInfo.ViewModel
{
    /// <summary>
    /// ViewModel class for capabilities page.
    /// </summary>
    public class CapabilitiesViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Properties of device's cameras.
        /// </summary>
        public static readonly string[] Camera = {"Back", "Back flash", "Front", "Front flash"};

        /// <summary>
        /// Properties of device's location services.
        /// </summary>
        public static readonly string[] Location = {"GPS", "WPS"};

        /// <summary>
        /// Properties of device's network services.
        /// </summary>
        public static readonly string[] Network =
        {
            "Bluetooth", "NFC", "3G",
            "Wifi Notification Enabled",
            "Flight Mode Enabled"
        };

        /// <summary>
        /// Local storage of collection of device's capabilities.
        /// </summary>
        private GroupViewModel _groupList;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets grouped collections of device's capabilities.
        /// </summary>
        public GroupViewModel GroupList
        {
            get => _groupList;
            set => SetProperty(ref _groupList, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public CapabilitiesViewModel()
        {
            var capability = new Capability();

            string[] cameraInitialValues =
            {
                capability.BackCamera,
                capability.BackFlash,
                capability.FrontCamera,
                capability.FrontFlash
            };

            string[] locationInitialValues =
            {
                capability.Gps,
                capability.Wps
            };

            string[] networkInitialValues =
            {
                capability.Bluetooth,
                capability.Nfc,
                capability.Network3G.ToString(),
                capability.WifiNotification.ToString(),
                capability.FlightMode.ToString()
            };

            _groupList = new GroupViewModel
            {
                ListUtils.CreateGroupedItemsList(Camera, nameof(Camera), cameraInitialValues),
                ListUtils.CreateGroupedItemsList(Location, nameof(Location), locationInitialValues),
                ListUtils.CreateGroupedItemsList(Network, nameof(Network), networkInitialValues),
            };

            capability.Network3GChanged += OnNetwork3GChanged;

            capability.WifiNotificationChanged += OnWifiNotificationChanged;

            capability.FlightModeChanged += OnFlightModeChanged;
        }

        /// <summary>
        /// Updates 3G network status.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnNetwork3GChanged(object s, CapabilityChangedEventArgs e)
        {
            GroupList[nameof(Network)]["3G"] = e.Value.ToString();
        }

        /// <summary>
        /// Updates Wi-Fi notifications status.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnWifiNotificationChanged(object s, CapabilityChangedEventArgs e)
        {
            GroupList[nameof(Network)]["Wifi Notification Enabled"] = e.Value.ToString();
        }

        /// <summary>
        /// Updates flight mode status.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnFlightModeChanged(object s, CapabilityChangedEventArgs e)
        {
            GroupList[nameof(Network)]["Flight Mode Enabled"] = e.Value.ToString();
        }

        #endregion
    }
}