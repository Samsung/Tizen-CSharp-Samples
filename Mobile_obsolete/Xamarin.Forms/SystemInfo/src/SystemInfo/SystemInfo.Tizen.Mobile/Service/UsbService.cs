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
using SystemInfo.Model.Usb;
using SystemInfo.Tizen.Mobile.Service;
using SystemSettings = Tizen.System.SystemSettings;

[assembly: Xamarin.Forms.Dependency(typeof(UsbService))]

namespace SystemInfo.Tizen.Mobile.Service
{
    /// <summary>
    /// Provides methods that allow to obtain information about USB services.
    /// </summary>
    public class UsbService : IUsb
    {
        /// <summary>
        /// Indicates if the USB debugging is enabled.
        /// </summary>
        public bool UsbDebuggingEnabled => SystemSettings.UsbDebuggingEnabled;

        /// <summary>
        /// Event invoked when USB debugging state has changed.
        /// </summary>
        public event EventHandler<UsbDebuggingEventArgs> UsbDebuggingChanged;

        /// <summary>
        /// Starts observing USB information for changes.
        /// </summary>
        /// <remarks>
        /// UsbDebuggingChanged event will be never invoked before calling this method.
        /// </remarks>
        public void StartListening()
        {
            SystemSettings.UsbDebuggingSettingChanged +=
                (s, e) => { UsbDebuggingChanged?.Invoke(s, new UsbDebuggingEventArgs(e.Value)); };
        }
    }
}