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

using NetworkApp.Tizen.Mobile;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen.Network.WiFiDirect;

[assembly: Xamarin.Forms.Dependency(implementorType: typeof(WiFiDirectImplementation))]

namespace NetworkApp.Tizen.Mobile
{
    /// <summary>
    /// Implementation class of IWiFiDirect interface
    /// </summary>
    class WiFiDirectImplementation : IWiFiDirect
    {
        // The event to deliver the Tizen.Network.WiFiDirect DiscoveryStateChanged event to application
        public event EventHandler<DiscoveryEventArgs> DeviceDiscovered;

        public WiFiDirectImplementation()
        {
        }

        public void StartScan()
        {
            LogImplementation.DLog("StartScan()");
            // Check if the Wi-Fi Direct is deactivated
            if (WiFiDirectManager.State == WiFiDirectState.Deactivated)
            {
                // Add DeviceStateChanged event
                WiFiDirectManager.DeviceStateChanged += EventHandlerDeviceStateChanged;
                // Call Tizen C# API
                WiFiDirectManager.Activate();
            }
            else
            {
                // Start to discover Wi-Fi Direct devices
                StartDiscovery();
            }

        }

        private void StartDiscovery()
        {
            // Add DiscoveryStateChanged event
            WiFiDirectManager.DiscoveryStateChanged += EventHandlerDiscoveryStateChanged;
            LogImplementation.DLog("StartDiscovery()");
            // Call Tizen C# API
            WiFiDirectManager.StartDiscovery(false, 0);
            LogImplementation.DLog("StartDiscovery() done");
        }

        public void StopScan()
        {
            LogImplementation.DLog("StopScan()");
            // Call Tizen C# API
            WiFiDirectManager.CancelDiscovery();
            // Remove DiscoveryStateChanged event
            WiFiDirectManager.DiscoveryStateChanged -= EventHandlerDiscoveryStateChanged;
        }

        private void EventHandlerDiscoveryStateChanged(object s, DiscoveryStateChangedEventArgs e)
        {
            LogImplementation.DLog("EventHandlerDiscoveryStateChanged");
            if (e.DiscoveryState == WiFiDirectDiscoveryState.Found)
            {
                LogImplementation.DLog("Found");
                // Get the found Wi-Fi Direct peer list
                IEnumerable<WiFiDirectPeer> peerList = WiFiDirectManager.GetDiscoveredPeers();
                List<String> deviceList = new List<String>();
                foreach (WiFiDirectPeer peer in peerList)
                {
                    LogImplementation.DLog("Peer " + peer.Name);
                    // Add name of the found device to device list
                    deviceList.Add(peer.Name);
                }
                // Generate the DeviceDiscovered event
                DiscoveryEventArgs de = new DiscoveryEventArgs(deviceList);
                DeviceDiscovered(s, de);
            }
        }

        private void EventHandlerDeviceStateChanged(object s, DeviceStateChangedEventArgs e)
        {
            // Check if the device is activated
            if (e.DeviceState == WiFiDirectDeviceState.Activated)
            {
                LogImplementation.DLog("Activated");
                // Start to discover
                StartDiscovery();
            }
        }
    }
}
