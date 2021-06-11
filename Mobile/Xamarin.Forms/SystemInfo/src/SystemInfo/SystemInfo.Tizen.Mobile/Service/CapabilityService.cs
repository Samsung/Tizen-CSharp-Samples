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
using SystemInfo.Model.Capability;
using SystemInfo.Tizen.Mobile.Service;
using Tizen;
using SystemSettings = Tizen.System.SystemSettings;
using SystemInformation = Tizen.System.Information;

[assembly: Xamarin.Forms.Dependency(typeof(CapabilityService))]

namespace SystemInfo.Tizen.Mobile.Service
{
    /// <summary>
    /// Provides methods that allow to obtain information about device's capabilities.
    /// </summary>
    public class CapabilityService : ICapability
    {
        private const string NetworkWifiFeatureKey = "http://tizen.org/feature/network.wifi";

        #region properties

        /// <summary>
        /// Indicates if the 3G data network is enabled.
        /// </summary>
        public bool Network3G
        {
            get
            {
                try
                {
                    return SystemSettings.Data3GNetworkEnabled;
                }
                catch (NotSupportedException e)
                {
                    Log.Warn("SystemInfo", e.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Indicates if WiFi related notifications are enabled on the device.
        /// </summary>
        public bool WifiNotification
        {
            get
            {
                try
                {
                    if (SystemInformation.TryGetValue<bool>(NetworkWifiFeatureKey, out bool isSupported) && isSupported)
                    {
                        return SystemSettings.NetworkWifiNotificationEnabled;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (NotSupportedException e)
                {
                    Log.Warn("SystemInfo", e.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// Indicates if the device is in the flight mode.
        /// </summary>
        public bool FlightMode
        {
            get
            {
                try
                {
                    return SystemSettings.NetworkFlightModeEnabled;
                }
                catch (NotSupportedException e)
                {
                    Log.Warn("SystemInfo", e.Message);
                    return false;
                }
            }
        }

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
        /// Starts observing capabilities information for changes.
        /// </summary>
        /// <remarks>
        /// Capabilities' events will be never invoked before calling this method.
        /// </remarks>
        public void StartListening()
        {
            try
            {
                SystemSettings.Data3GNetworkSettingChanged +=
                    (s, e) => { Network3GChanged?.Invoke(s, new CapabilityChangedEventArgs(e.Value)); };
            }
            catch (NotSupportedException e)
            {
                Log.Warn("SystemInfo", e.Message);
            }

            try
            {
                if (SystemInformation.TryGetValue<bool>(NetworkWifiFeatureKey, out bool isSupported) && isSupported)
                {
                    SystemSettings.NetworkWifiNotificationSettingChanged +=
                        (s, e) => { WifiNotificationChanged?.Invoke(s, new CapabilityChangedEventArgs(e.Value)); };
                }
            }
            catch (NotSupportedException e)
            {
                Log.Warn("SystemInfo", e.Message);
            }

            try
            {
                SystemSettings.NetworkFlightModeSettingChanged +=
                    (s, e) => { FlightModeChanged?.Invoke(s, new CapabilityChangedEventArgs(e.Value)); };
            }
            catch (NotSupportedException e)
            {
                Log.Warn("SystemInfo", e.Message);
            }
        }

        #endregion
    }
}