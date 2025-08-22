/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
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

namespace SystemInfo.Model.Usb
{
    /// <summary>
    /// Class that holds information about USB.
    /// </summary>
    public class Usb
    {
        #region fields

        /// <summary>
        /// Service that provides information about USB.
        /// </summary>
        private readonly IUsb _service;

        #endregion

        #region properties

        /// <summary>
        /// Indicates if the USB debugging is enabled.
        /// </summary>
        public bool UsbDebuggingEnabled => _service.UsbDebuggingEnabled;

        /// <summary>
        /// Indicates if the device supports the USB client or accessory mode.
        /// </summary>
        public string Host => KeyUtil.GetKeyValue<bool>("http://tizen.org/feature/usb.accessory");

        /// <summary>
        /// Indicates if the device supports the USB host mode.
        /// </summary>
        public string ClientAccessory => KeyUtil.GetKeyValue<bool>("http://tizen.org/feature/usb.host");

        /// <summary>
        /// Event invoked when USB debugging state has changed
        /// </summary>
        public event EventHandler<UsbDebuggingEventArgs> UsbDebuggingChanged;

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public Usb()
        {
            _service = DependencyService.Get<IUsb>();

            _service.UsbDebuggingChanged += (s, e) => { UsbDebuggingChanged?.Invoke(s, e); };

            _service.StartListening();
        }

        #endregion
    }
}