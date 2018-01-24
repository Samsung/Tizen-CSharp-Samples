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

using System;
using SystemInfo.Utils;
using Xamarin.Forms;

namespace SystemInfo.Model.Capability
{
    /// <summary>
    /// Class that holds information about capabilities.
    /// </summary>
    public class Capability
    {
        #region fields

        /// <summary>
        /// Service that provides information about capabilities.
        /// </summary>
        private readonly ICapability _service;

        #endregion

        #region properties

        /// <summary>
        /// Gets status of back camera.
        /// </summary>
        public string BackCamera => KeyUtil.GetKeyValue<bool>("http://tizen.org/feature/camera.back");

        /// <summary>
        /// Gets status of back flash.
        /// </summary>
        public string BackFlash => KeyUtil.GetKeyValue<bool>("http://tizen.org/feature/camera.back.flash");

        /// <summary>
        /// Gets status of front camera.
        /// </summary>
        public string FrontCamera => KeyUtil.GetKeyValue<bool>("http://tizen.org/feature/camera.front");

        /// <summary>
        /// Gets status of front flash.
        /// </summary>
        public string FrontFlash => KeyUtil.GetKeyValue<bool>("http://tizen.org/feature/camera.front.flash");

        /// <summary>
        /// Gets status of GPS.
        /// </summary>
        public string Gps => KeyUtil.GetKeyValue<bool>("http://tizen.org/feature/location.gps");

        /// <summary>
        /// Gets status of WPS.
        /// </summary>
        public string Wps => KeyUtil.GetKeyValue<bool>("http://tizen.org/feature/location.wps");

        /// <summary>
        /// Gets status of Bluetooth.
        /// </summary>
        public string Bluetooth => KeyUtil.GetKeyValue<bool>("http://tizen.org/feature/network.bluetooth");

        /// <summary>
        /// Gets status of NFC.
        /// </summary>
        public string Nfc => KeyUtil.GetKeyValue<bool>("http://tizen.org/feature/network.nfc");

        /// <summary>
        /// Indicates if the 3G data network is enabled.
        /// </summary>
        public bool Network3G => _service.Network3G;

        /// <summary>
        /// Indicates if WiFi related notifications are enabled on the device.
        /// </summary>
        public bool WifiNotification => _service.WifiNotification;

        /// <summary>
        /// Indicates if the device is in the flight mode.
        /// </summary>
        public bool FlightMode => _service.FlightMode;

        /// <summary>
        /// Event invoked when 3G network was turned on or off.
        /// </summary>
        public event EventHandler<CapabilityChangedEventArgs> Network3GChanged;

        /// <summary>
        /// Event invoked when WiFi related notifications have changed.
        /// </summary>
        public event EventHandler<CapabilityChangedEventArgs> WifiNotificationChanged;

        /// <summary>
        /// Event invoked when flight mode was turned on or off.
        /// </summary>
        public event EventHandler<CapabilityChangedEventArgs> FlightModeChanged;

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public Capability()
        {
            _service = DependencyService.Get<ICapability>();

            _service.Network3GChanged += (s, e) => { Network3GChanged?.Invoke(s, e); };

            _service.WifiNotificationChanged += (s, e) => { WifiNotificationChanged?.Invoke(s, e); };

            _service.FlightModeChanged += (s, e) => { FlightModeChanged?.Invoke(s, e); };

            _service.StartListening();
        }

        #endregion
    }
}