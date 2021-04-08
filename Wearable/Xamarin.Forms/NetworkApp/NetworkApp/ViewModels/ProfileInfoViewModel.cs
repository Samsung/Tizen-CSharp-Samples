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

namespace NetworkApp.ViewModels
{
    /// <summary>
    /// ViewModel for ProfileInfoVIew
    /// </summary>
    class ProfileInfoViewModel : ViewModelBase
    {
        /// <summary>
        /// Instance of ConnectionInfo
        /// </summary>
        private readonly ConnectionProfileInfo info = new ConnectionProfileInfo();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileInfoViewModel"/> class
        /// </summary>
        /// <param name="profileName">Name of Connection Profile to be shown</param>
        public ProfileInfoViewModel(string profileName)
        {
            ProfileName = profileName;
            info.GetProfileList();
        }

        /// <summary>
        /// Gets or sets Name of Connection Profile
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets Type of Connection Profile
        /// </summary>
        public string ProfileType
        {
            get
            {
                string type = info.GetProfileType(ProfileName);
                if (type.Equals("Cellular"))
                {
                    string apn = info.GetApnType(ProfileName);
                    if (apn.Equals(""))
                    {
                        return "Type: " + type;
                    }

                    return "Type: " + type + "\nAPN: " + info.GetApnType(ProfileName);
                }

                return "Type: " + type;
            }
        }

        /// <summary>
        /// Gets or sets State of Connection Profile
        /// </summary>
        public string ProfileState
        {
            get
            {
                return "State: " + info.GetProfileState(ProfileName);
            }
        }

        /// <summary>
        /// Gets or sets IP Address of Connection Profile
        /// </summary>
        public string IPAddress
        {
            get
            {
                return "IP: " + info.GetIPAddress(ProfileName);
            }
        }

        /// <summary>
        /// Gets or sets IP Config Type of Connection Profile
        /// </summary>
        public string IPConfigType
        {
            get
            {
                return "IP Config Type: " + info.GetIPConfigType(ProfileName);
            }
        }

        /// <summary>
        /// Gets or sets subnet mask of Connection Profile
        /// </summary>
        public string SubnetMask
        {
            get
            {
                return "SubnetMask: " + info.GetSubnetMask(ProfileName);
            }
        }

        /// <summary>
        /// Gets or sets Gateway Address of Connection Profile
        /// </summary>
        public string GatewayAddress
        {
            get
            {
                return "Gateway: " + info.GetGatewayAddress(ProfileName);
            }
        }

        /// <summary>
        /// Gets or sets DNS Address of Connection Profile
        /// </summary>
        public string DNSAddress
        {
            get
            {
                return "DNS: " + info.GetDnsAddress(ProfileName);
            }
        }

        /// <summary>
        /// Gets or sets DNS Config Type of Connection Profile
        /// </summary>
        public string DNSConfigType
        {
            get
            {
                return "DNS Config Type: " + info.GetDnsConfigType(ProfileName);
            }
        }

        /// <summary>
        /// Gets or sets Proxy Address of Connection Profile
        /// </summary>
        public string ProxyAddress
        {
            get
            {
                return "Proxy Address: " + info.GetProxyAddress(ProfileName);
            }
        }

        /// <summary>
        /// Gets or sets Proxy Type of Connection Profile
        /// </summary>
        public string ProxyType
        {
            get
            {
                return "Proxy Type: " + info.GetProxyType(ProfileName);
            }
        }

    }
}
