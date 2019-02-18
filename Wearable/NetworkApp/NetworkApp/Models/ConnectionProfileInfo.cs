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

using System.Collections.Generic;
using Tizen.Network.Connection;

namespace NetworkApp.Models
{
    /// <summary>
    /// ConnectionInfo class for getting prepared 
    /// strings with connection info from API
    /// </summary>
    class ConnectionProfileInfo
    {
        /// <summary>
        /// List that contains the result of latest profile list
        /// </summary>
        IEnumerable<ConnectionProfile> profileInfoList;

        /// <summary>
        /// List that contains the result of latest profile list to be shown in List
        /// </summary>
        List<ProfileInfo> viewList;

        /// <summary>
        /// Currently connected Connection Profile
        /// </summary>
        ConnectionProfile currProfile;


        /// <summary>
        /// Gets name about currently connected profile
        /// </summary>
        /// <returns>String with name of currently connected profile</returns>
        public string GetCurrentProfile()
        {
            currProfile = ConnectionProfileManager.GetCurrentProfile();
            if (currProfile == null)
            {
                Logger.Log("There is no current profile");
                return "";
            }

            return currProfile.Name;
        }

        /// <summary>
        /// Gets interface name about currently connected profile
        /// </summary>
        /// <returns>String with interface name of currently connected profile</returns>
        public string GetCurrentProfileInterfaceName()
        {
            currProfile = ConnectionProfileManager.GetCurrentProfile();
            if (currProfile == null)
            {
                Logger.Log("There is no current profile");
                return "";
            }

            return currProfile.InterfaceName;
        }

        /// <summary>
        /// Calls GetProfileListAsync() to get profile list and return a list 
        /// that contains Connection Profile information
        /// </summary>
        /// <returns>ProfileInfo list</returns>
        public List<ProfileInfo> GetProfileList()
        {
            GetProfileListAsync();
            viewList = new List<ProfileInfo>();
            foreach (var p in profileInfoList)
            {
                viewList.Add(new ProfileInfo
                {
                    ProfileName = p.Name,
                    Type = p.Type.ToString(),
                    State = p.GetState(AddressFamily.IPv4).ToString(),
                    Name = p.Type.Equals(ConnectionProfileType.Cellular) ? p.Name + " (" + ((CellularProfile)p).ServiceType.ToString() + ")" : p.Name,
                });
            }

            return viewList;
        }

        /// <summary>
        /// Calls ConnectionProfileManager.GetProfileListAsync() to get profile list
        /// and create a list that contains Connection Profile information
        /// </summary>
        /// <returns>ProfileInfo list</returns>
        private async void GetProfileListAsync()
        {
            profileInfoList = await ConnectionProfileManager.GetProfileListAsync(ProfileListType.Registered);            
        }

        /// <summary>
        /// Finds a ProfileInfo instance        
        /// </summary>
        /// <param name="Name">Name of Connection Profile to find from profileInfoList</param>
        /// <returns>ProfileInfo instance with the Name</returns>
        private ConnectionProfile FindProfile(string Name)
        {
            string ProfileName = null;
            foreach (var n in viewList)
            {
                if (n.Name.Equals(Name))
                {
                    ProfileName = n.ProfileName;
                    break;
                }
            }

            if (ProfileName != null)
            {
                foreach (var p in profileInfoList)
                {
                    if (p.Name.Equals(ProfileName))
                    {
                        return p;
                    }
                }
            }

            Logger.Log("FindProfile > There is no profile with name " + Name);
            return null;
        }

        /// <summary>
        /// Gets info about Type of the Connection Profile
        /// </summary>
        /// <param name="Name">Name of Connection Profile</param>
        /// <returns>String with Type of the Connection Profile</returns>
        public string GetProfileType(string Name)
        {
            ConnectionProfile info = FindProfile(Name);
            if (info == null)
            {
                Logger.Log("There is no profile with name " + Name);
                return "";
            }

            Logger.Log("GetProfileType of " + Name);
            return info.Type.ToString();
        }

        /// <summary>
        /// Gets info about State of the Connection Profile
        /// </summary>
        /// <param name="Name">Name of Connection Profile</param>
        /// <returns>String with State of the Connection Profile</returns>
        public string GetProfileState(string Name)
        {
            ConnectionProfile info = FindProfile(Name);
            if (info == null)
            {
                Logger.Log("There is no profile with name " + Name);
                return "";
            }

            Logger.Log("GetProfileType of " + Name);
            return info.GetState(AddressFamily.IPv4).ToString();
        }

        /// <summary>
        /// Gets IP Address of Connection Profile
        /// </summary>
        /// /// <param name="Name">Name of Connection Profile</param>
        /// <returns>IP Address</returns>
        public string GetIPAddress(string Name)
        {
            ConnectionProfile info = FindProfile(Name);
            if (info == null)
            {
                Logger.Log("There is no profile with name " + Name);
                return "";
            }

            return info.IPv4Settings.IP.ToString();
        }

        /// <summary>
        /// Gets IP Config Type of Connection Profile
        /// </summary>
        /// /// <param name="Name">Name of Connection Profile</param>
        /// <returns>IP Config Type</returns>
        public string GetIPConfigType(string Name)
        {
            ConnectionProfile info = FindProfile(Name);
            if (info == null)
            {
                Logger.Log("There is no profile with name " + Name);
                return "";
            }

            return info.IPv4Settings.IPConfigType.ToString();
        }

        /// <summary>
        /// Gets subnet mask of Connection Profile
        /// </summary>
        /// /// <param name="Name">Name of Connection Profile</param>
        /// <returns>Subnet mask</returns>
        public string GetSubnetMask(string Name)
        {
            ConnectionProfile info = FindProfile(Name);
            if (info == null)
            {
                Logger.Log("There is no profile with name " + Name);
                return "";
            }

            return info.IPv4Settings.SubnetMask.ToString();
        }

        /// <summary>
        /// Gets Gateway Address of Connection Profile
        /// </summary>
        /// /// <param name="Name">Name of Connection Profile</param>
        /// <returns>Gateway Address</returns>
        public string GetGatewayAddress(string Name)
        {
            ConnectionProfile info = FindProfile(Name);
            if (info == null)
            {
                Logger.Log("There is no profile with name " + Name);
                return "";
            }

            return info.IPv4Settings.Gateway.ToString();
        }

        /// <summary>
        /// Gets DNS Address of Connection Profile
        /// </summary>
        /// /// <param name="Name">Name of Connection Profile</param>
        /// <returns>DNS Address</returns>
        public string GetDnsAddress(string Name)
        {
            ConnectionProfile info = FindProfile(Name);
            if (info == null)
            {
                Logger.Log("There is no profile with name " + Name);
                return "";
            }

            return info.IPv4Settings.Dns1.ToString();
        }

        /// <summary>
        /// Gets DNS Config Type of Connection Profile
        /// </summary>
        /// /// <param name="Name">Name of Connection Profile</param>
        /// <returns>DNS Config Type</returns>
        public string GetDnsConfigType(string Name)
        {
            ConnectionProfile info = FindProfile(Name);
            if (info == null)
            {
                Logger.Log("There is no profile with name " + Name);
                return "";
            }

            return info.IPv4Settings.DnsConfigType.ToString();
        }

        /// <summary>
        /// Gets Proxy Address of Connection Profile
        /// </summary>
        /// /// <param name="Name">Name of Connection Profile</param>
        /// <returns>Proxy Address</returns>
        public string GetProxyAddress(string Name)
        {
            ConnectionProfile info = FindProfile(Name);
            if (info == null)
            {
                Logger.Log("There is no profile with name " + Name);
                return "";
            }

            return info.ProxyAddress.ToString();
        }

        /// <summary>
        /// Gets Proxy Type of Connection Profile
        /// </summary>
        /// /// <param name="Name">Name of Connection Profile</param>
        /// <returns>Proxy Type</returns>
        public string GetProxyType(string Name)
        {
            ConnectionProfile info = FindProfile(Name);
            if (info == null)
            {
                Logger.Log("There is no profile with name " + Name);
                return "";
            }

            return info.ProxyType.ToString();
        }

        /// <summary>
        /// Gets APN Type if the Connection Profile is a Cellular profile
        /// </summary>
        /// /// <param name="Name">Name of Connection Profile</param>
        /// <returns>APN Type</returns>
        public string GetApnType(string Name)
        {
            ConnectionProfile info = FindProfile(Name);
            if (info == null)
            {
                Logger.Log("There is no profile with name " + Name);
                return "";
            }

            if (info.Type.Equals(ConnectionProfileType.Cellular))
            {
                return ((CellularProfile)info).Apn.ToString();
            }

            return "";
        }
        
    }
}
