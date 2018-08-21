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

using NetworkApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tizen.Network.Connection;
using Tizen.Network.WiFi;

namespace NetworkApp.Tizen.Mobile
{
    /// <summary>
    /// WiFiApiManager class for managing Wi-Fi API calls
    /// </summary>
    public class WiFiApiManager
    {
        /// <summary>
        /// List that contains the result of latest scan
        /// </summary>
        private IEnumerable<WiFiAP> _apList = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WiFiApiManager"/> class
        /// </summary>
        public WiFiApiManager()
        {
            _apList = new List<WiFiAP>();
        }

        /// <summary>
        /// Calls WiFiManager.ActivateAsync() to turn on Wi-Fi interface
        /// </summary>
        /// <returns>Task to do ActivateAsync</returns>
        public async Task Activate()
        {
            Logger.Log("Activate()");
            await WiFiManager.ActivateAsync();
        }

        /// <summary>
        /// Calls WiFiManager.DeactivateAsync() to turn off Wi-Fi interface
        /// </summary>
        /// <returns>Task to do DeactivateAsync</returns>
        public Task Deactivate()
        {
            Logger.Log("Deactivate()");
            return WiFiManager.DeactivateAsync();
        }

        /// <summary>
        /// Calls WiFiManager.ScanAsync() to scan for APs
        /// </summary>
        /// <returns>Task to do ScanAsync</returns>
        public Task Scan()
        {
            Logger.Log("Scan()");
            return WiFiManager.ScanAsync();
        }

        /// <summary>
        /// Calls WiFiManager.GetFoundAPs() to get scan result and return a list 
        /// that contains Wi-Fi AP information
        /// </summary>
        /// <returns>APInfo list with scan results</returns>
        public List<APInfo> ScanResult()
        {
            Logger.Log("ScanResult()");

            try
            {
                _apList = WiFiManager.GetFoundAPs();

                List<APInfo> apInfoList = new List<APInfo>();
                foreach (var item in _apList)
                {
                    apInfoList.Add(new APInfo
                    {
                        Name = item.NetworkInformation.Essid,
                        State = item.NetworkInformation.ConnectionState.ToString(),
                    });
                }

                return apInfoList;
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString());
                return null;
            }
        }

        /// <summary>
        /// Calls WiFiAP.ConnectAsync() to connect the Wi-Fi AP        
        /// </summary>
        /// <param name="essid">ESSID of Wi-Fi AP to connect</param>
        /// <param name="password">Password of Wi-Fi AP to connect</param>
        /// <returns>Task to do ConnectAsync</returns>
        public Task Connect(string essid, string password)
        {
            Logger.Log("Connect() " + essid + " " + password);
            // Get a WiFiAP instance with essid from Wi-Fi AP list
            WiFiAP ap = FindAP(essid);
            // Set password
            if (password.Length > 0)
            {
                ap.SecurityInformation.SetPassphrase(password);
            }
            return ap.ConnectAsync();
        }

        /// <summary>
        /// Calls WiFiAP.DisconnectAsync() to disconnect the Wi-Fi AP        
        /// </summary>
        /// <param name="essid">ESSID of Wi-Fi AP to disconnect</param>
        /// <returns>Task to do DisconnectAsync</returns>
        public Task Disconnect(string essid)
        {
            Logger.Log("Disconnect() " + essid);
            // Get a WiFiAP instance with essid from Wi-Fi AP list and disconnect it by calling Tizen C# API
            return FindAP(essid).DisconnectAsync();
        }

        /// <summary>
        /// Calls WiFiAP.ForgetAP() to forget the Wi-Fi AP
        /// </summary>
        /// <param name="essid">ESSID of Wi-Fi AP to forget</param>
        public void Forget(string essid)
        {
            Logger.Log("Forget() " + essid);
            WiFiAP ap = FindAP(essid);
            if (ap == null)
            {
                Logger.Log("Can't find AP " + essid);
                return;
            }

            ap.ForgetAP();
            ap.Refresh();
        }

        /// <summary>
        /// Check if Wi-Fi is ON 
        /// </summary>
        /// <returns>True if Wi-Fi is ON, false otherwise</returns>
        public bool IsActive()
        {
            return WiFiManager.IsActive;
        }

        /// <summary>
        /// Gets the ESSID of the AP device is connected to
        /// </summary>
        /// <returns>ESSID of the connected Wi-Fi AP</returns>
        public string ConnectedAP()
        {
            return WiFiManager.GetConnectedAP()?.NetworkInformation.Essid ?? null;
        }

        /// <summary>
        /// Gets the SecurityType of given Access Point
        /// </summary>
        /// <param name="apName">Access Point name</param>
        /// <returns>Wi-Fi Security Type</returns>
        public WiFiSecurityType GetAPSecurityType(string apName)
        {
            WiFiAP ap = FindAP(apName);

            if (ap == null)
            {
                Logger.Log("AP: " + apName + " not found!");
                return WiFiSecurityType.None;
            }
            return ap.SecurityInformation.SecurityType;
        }

        /// <summary>
        /// Checks if info about given Access Point is stored in
        /// Wi-Fi Configurations
        /// </summary>
        /// <param name="apName">Access Point name</param>
        /// <returns>True if Access Point is stored, false otherwise</returns>
        public bool IsAPInfoStored(string apName)
        {
            try
            {
                var configurations = WiFiManager.GetWiFiConfigurations();
                return configurations.Any(c => c.Name == apName);
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Finds a WiFiAP instance        
        /// </summary>
        /// <param name="essid">ESSID of Wi-Fi AP to find from _apList</param>
        /// <returns>WiFiAP instance with the ESSID</returns>
        private WiFiAP FindAP(string essid)
        {
            Logger.Log("FindAP() " + essid);
            ScanResult();
            foreach (var item in _apList)
            {
                Logger.Log("Found AP\t" + item.NetworkInformation.Essid);
                if (item.NetworkInformation.Essid.Equals(essid))
                {
                    return item;
                }
            }

            return null;
        }
    }
}
