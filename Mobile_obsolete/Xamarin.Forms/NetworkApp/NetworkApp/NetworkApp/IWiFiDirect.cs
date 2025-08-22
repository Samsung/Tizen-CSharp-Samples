/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkApp
{
    /// <summary>
    /// Interface to call Tizen.Network.WiFiDirect API
    /// </summary>
    public interface IWiFiDirect
    {
        /// <summary>
        /// The event to deliver the Tizen.Network.WiFiDirect DiscoveryStateChanged event to application
        /// </summary>
        event EventHandler<DiscoveryEventArgs> DeviceDiscovered;

        void StartScan();

        void StopScan();
    }

    /// <summary>
    /// The EventArgs for the DeviceDiscovered event
    /// </summary>
    public class DiscoveryEventArgs : EventArgs
    {
        public List<String> deviceList;
        public DiscoveryEventArgs(List<String> list)
        {
            deviceList = list;
        }
    }
}
