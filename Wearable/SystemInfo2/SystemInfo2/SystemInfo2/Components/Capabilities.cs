//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using Xamarin.Forms;
using Tizen.System;
using Tizen;
using TInformation = Tizen.System.Information;

namespace SystemInfo.Components
{
    /// <summary>
    /// An easy to access fascade for Tizen.System.Information with information like flight-mode status or GPS availability. 
    /// </summary>
    public class Capabilities :  BindableObject
    {
        #region properties

        /// <summary>
        /// Returns the number of Vibrators available in the system
        /// </summary>
        public int VibratorsCount => Tizen.System.Vibrator.NumberOfVibrators;

        /// <summary>
        /// Indicates if the 3G data network is enabled.
        /// </summary>
        public bool Is3GDataNetworkEnabled
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
        public bool IsWifiNotificationEnabled
        {
            get
            {
                try
                {
                    return SystemSettings.NetworkWifiNotificationEnabled;
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
        public bool IsFlighModeEnabled
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
        /// Indicates if GPS is available
        /// </summary>
        public bool Gps
        {
            get
            {
                bool Gps;
                TInformation.TryGetValue<bool>("http://tizen.org/feature/location.gps", out Gps);
                return Gps;
            }
        }

        /// <summary>
        /// Indicates if WPS is available (Wifi based positioning)
        /// </summary>
        public bool Wps
        {
            get
            {
                bool Wps;
                TInformation.TryGetValue<bool>("http://tizen.org/feature/location.wps", out Wps);
                return Wps;
            }
        }

        /// <summary>
        /// Indicates if Bluetooth is available
        /// </summary>
        public bool Bluetooth
        {
            get
            {
                bool Bluetooth;
                TInformation.TryGetValue<bool>("http://tizen.org/feature/network.bluetooth", out Bluetooth);
                return Bluetooth;
            }
        }

        /// <summary>
        /// Indicates if Nfc is available
        /// </summary>
        public bool Nfc
        {
            get
            {
                bool Nfc;
                TInformation.TryGetValue<bool>("http://tizen.org/feature/network.nfc", out Nfc);
                return Nfc;
            }
        }

        #endregion


        /// <summary>
        /// Ensure events on SystemSettings changes are handled
        /// </summary>
        public Capabilities()
        {
            SystemSettings.Data3GNetworkSettingChanged += On3gNetworkChanged;
            SystemSettings.NetworkFlightModeSettingChanged += OnFlightModeChanged;
        }

        /// <summary>
        /// Cleanup the event handlers
        /// </summary>
        ~Capabilities()
        {
            SystemSettings.Data3GNetworkSettingChanged -= On3gNetworkChanged;
            SystemSettings.NetworkFlightModeSettingChanged -= OnFlightModeChanged;
        }

        private void On3gNetworkChanged (Object sender, Data3GNetworkSettingChangedEventArgs args )
        {
            OnPropertyChanged(nameof(Is3GDataNetworkEnabled));
            
        }

        private void OnFlightModeChanged(Object sender, NetworkFlightModeSettingChangedEventArgs args)
        {
            OnPropertyChanged(nameof(IsFlighModeEnabled));
        }

        private void OnWifiNotificationsChanged(Object sender, NetworkWifiNotificationSettingChangedEventArgs args)
        {
            OnPropertyChanged(nameof(IsWifiNotificationEnabled));
        }
    }
}
