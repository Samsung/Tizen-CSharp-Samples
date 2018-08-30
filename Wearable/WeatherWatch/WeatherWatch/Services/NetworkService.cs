/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Tizen.Network.Connection;
using Tizen.System;
using WeatherWatch.PageModels;

namespace WeatherWatch.Services
{
    /// <summary>
    /// NetworkService class
    /// </summary>
    public class NetworkService
    {
        private WeatherWatchPageModel _viewModel;
        bool isWiFiSupported, isTelephonySupported, isBTTetheringSupported, isEthernetSupported;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_vm">WeatherWatchPageModel</param>
        public NetworkService(WeatherWatchPageModel _vm)
        {
            _viewModel = _vm;

            Information.TryGetValue("http://tizen.org/feature/network.wifi", out isWiFiSupported);
            Information.TryGetValue("http://tizen.org/feature/network.telephony", out isTelephonySupported);
            Information.TryGetValue("http://tizen.org/feature/network.tethering.bluetooth", out isBTTetheringSupported);
            Information.TryGetValue("http://tizen.org/feature/network.ethernet", out isEthernetSupported);
            Console.WriteLine("Network feature : wifi : " + isWiFiSupported);
            Console.WriteLine("Network feature : telephony : " + isTelephonySupported);
            Console.WriteLine("Network feature : tethering.bluetooth : " + isBTTetheringSupported);
            Console.WriteLine("Network feature : network.ethernet : " + isEthernetSupported);

            ConnectionItem connection = ConnectionManager.CurrentConnection;
            Console.WriteLine("connection type : " + connection.Type + ", State: " + connection.State);
        }

        /// <summary>
        /// whether any network connection is available or not
        /// </summary>
        public bool IsConnected
        {
            get
            {
                try
                {
                    ConnectionItem connection = ConnectionManager.CurrentConnection;
                    Console.WriteLine("[NetworkService.IsConnected] connection type:" + connection.Type + ", state: " + connection.State);

                    ConnectionProfile profile = ConnectionProfileManager.GetCurrentProfile();
                    if (profile != null)
                    {
                        Console.WriteLine("[NetworkService.IsConnected] CurrentProfile type:" + profile.Type + ", name:" + profile.Name);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("[NetworkService.IsConnected] " + e.Message + ", " + e.StackTrace + ", " + e.InnerException);
                }

                return CheckConnection();

            }
        }

        /// <summary>
        /// Check whether any network connection is available or not
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckConnection()
        {
            bool _isConnected = false;

            try
            {
                if (isBTTetheringSupported && (ConnectionManager.BluetoothState == ConnectionState.Connected))
                {
                    Console.WriteLine("[NetworkService.CheckConnection()] BluetoothState:" + ConnectionManager.BluetoothState);
                    _isConnected = true;
                }

                if (isTelephonySupported && (ConnectionManager.CellularState == CellularState.Connected))
                {
                    Console.WriteLine("[NetworkService.CheckConnection()] CellularState :" + ConnectionManager.CellularState);
                    _isConnected = true;
                }

                if (isEthernetSupported && (ConnectionManager.EthernetCableState == EthernetCableState.Attached && ConnectionManager.EthernetState == ConnectionState.Connected))
                {
                    Console.WriteLine("[NetworkService.CheckConnection()] EthernetCableState:" + ConnectionManager.EthernetCableState);
                    Console.WriteLine("[NetworkService.CheckConnection()] EthernetState:" + ConnectionManager.EthernetState);
                    _isConnected = true;
                }

                if (isWiFiSupported  && (ConnectionManager.WiFiState == ConnectionState.Connected))
                {
                    Console.WriteLine("[NetworkService.CheckConnection()] WiFiState:" + ConnectionManager.WiFiState);
                    _isConnected = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[NetworkService.CheckConnection()] " + e.Message + ", " + e.StackTrace + ", " + e.InnerException);
            }

            return _isConnected;
        }

        //public void RegisterEvent()
        //{
        //    ConnectionManager.IPAddressChanged += ConnectionManager_IPAddressChanged;
        //    ConnectionManager.ConnectionTypeChanged += ConnectionManager_ConnectionTypeChanged;
        //}

        //public void UnregisterEvent()
        //{
        //    ConnectionManager.IPAddressChanged -= ConnectionManager_IPAddressChanged;
        //    ConnectionManager.ConnectionTypeChanged -= ConnectionManager_ConnectionTypeChanged;
        //}

        //private void ConnectionManager_ConnectionTypeChanged(object sender, ConnectionTypeEventArgs e)
        //{
        //    Console.WriteLine("[ConnectionManager_ConnectionTypeChanged] type : " + connection.Type + ", State: " + connection.State);
        //}

        //private void ConnectionManager_IPAddressChanged(object sender, AddressEventArgs e)
        //{
        //    Console.WriteLine("[ConnectionManager_IPAddressChanged] IPv4: " + e.IPv4Address + ", IPv6: " + e.IPv6Address);
        //}
    }
}
