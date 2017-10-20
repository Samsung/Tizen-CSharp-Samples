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
using Tizen.Network.WiFi;


[assembly: Xamarin.Forms.Dependency(implementorType: typeof(WiFiImplementation))]

namespace NetworkApp.Tizen.Mobile
{
    /// <summary>
    /// Implementation class of IWiFi interface
    /// </summary>
    public class WiFiImplementation : IWiFi
    {
        /// List that contains the result of latest scan
        IEnumerable<WiFiAP> apList = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public WiFiImplementation()
        {
            apList = new List<WiFiAP>();
        }

        /// <summary>
        /// Call WiFiManager.ActivateAsync() to turn on WiFi
        /// </summary>
        /// <returns>Task to do ActivateAsync</returns>
        public async Task Activate()
        {
            LogImplementation.DLog("Activate");
            // Call Tizen C# API
            await WiFiManager.ActivateAsync();
        }

        /// <summary>
        /// Call WiFiManager.DeactivateAsync() to turn off WiFi
        /// </summary>
        /// <returns>Task to do DeactivateAsync</returns>
        public Task Deactivate()
        {
            LogImplementation.DLog("Deactivate");
            // Call Tizen C# API
            return WiFiManager.DeactivateAsync();
        }

        /// <summary>
        /// Call WiFiManager.ScanAsync() to scan
        /// </summary>
        /// <returns>Task to do ScanAsync</returns>
        public Task Scan()
        {
            LogImplementation.DLog("Scan");
            // Call Tizen C# API
            return WiFiManager.ScanAsync();
        }

        /// <summary>
        /// Call WiFiManager.GetFoundAPs() to get scan result and return a list that contains APInfo
        /// </summary>
        /// <returns>scan result by a list of APInfo</returns>
        public List<APInfo> ScanResult()
        {
            LogImplementation.DLog("ScanResult");
            try
            {
                // Call Tizen C# API
                apList = WiFiManager.GetFoundAPs();
                // return a list that contains WiFi ESSIDs
                return GetAPList();
            }
            catch (Exception e)
            {
                LogImplementation.DLog(e.ToString());
            }

            return null;
        }

        /// <summary>
        /// Call WiFiAP.ConnectAsync() to connect the WiFiAP        
        /// </summary>
        /// <param name="essid">ESSID of AP to connect</param>
        /// <param name="password">password of AP to connect</param>
        /// <returns>Task to do ConnectAsync</returns>
        public Task Connect(String essid, String password)
        {
            LogImplementation.DLog("Connect: " + essid + " " + password);
            // Get a WiFiAP instance with essid from apList
            WiFiAP ap = FindAP(essid);
            // Set password
            if (password.Length > 0)
            {
                ap.SecurityInformation.SetPassphrase(password);
            }
            // Call Tizen C# API
            return ap.ConnectAsync();
        }

        /// <summary>
        /// Call WiFiAP.DisconnectAsync() to disconnect the WiFiAP        
        /// </summary>
        /// <param name="essid">ESSID of AP to disconnect</param>
        /// <returns>Task to do DisconnectAsync</returns>
        public Task Disconnect(String essid)
        {
            LogImplementation.DLog("Disconnect: " + essid);
            // Get a WiFiAP instance with essid from apList and disconnect it by calling Tizen C# API
            return FindAP(essid).DisconnectAsync();
        }

        /// <summary>
        /// Call WiFiAP.ForgetAP() to forget the WiFiAP
        /// </summary>
        /// <param name="essid">ESSID of AP to forget</param>
        public void Forget(String essid)
        {
            LogImplementation.DLog("Forget: " + essid);
            WiFiAP ap = FindAP(essid);
            if (ap == null)
            {
                LogImplementation.DLog("Can't find AP " + essid);
            }

            ap.ForgetAP();
        }

        /// <summary>
        /// Find a WiFiAP instance        
        /// </summary>
        /// <param name="essid">ESSID of AP to find from apList</param>
        /// <returns>WiFiAP instance with essid</returns>
        private WiFiAP FindAP(String essid)
        {
            LogImplementation.DLog("FindAP: " + essid);
            ScanResult();
            foreach (var item in apList)
            {
                LogImplementation.DLog("Found AP\t" + item.NetworkInformation.Essid);
                if (item.NetworkInformation.Essid.Equals(essid))
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Check if WiFi is powered on 
        /// </summary>
        /// <returns>True if WIFi is on. False, otherwise</returns>
        public bool IsActive()
        {
            // Call Tizen C# API
            return WiFiManager.IsActive;
        }

        /// <summary>
        /// Get the ESSID of the AP that this device is connected to
        /// </summary>
        /// <returns>ESSID of the connected AP</returns>
        public String ConnectedAP()
        {
            // Call Tizen C# API
            return WiFiManager.GetConnectedAP().NetworkInformation.Essid;
        }

        /// <summary>
        /// Get a list of scanned APs
        /// </summary>
        /// <returns>List of APInfo</returns>
        public List<APInfo> GetAPList()
        {
            List<APInfo> apInfoList = new List<APInfo>();
            foreach (var item in apList)
            {
                // Store the AP information to APInfo class
                apInfoList.Add(new APInfo(item.NetworkInformation.Essid, item.NetworkInformation.ConnectionState.ToString()));
            }

            return apInfoList;
        }
    }
}
