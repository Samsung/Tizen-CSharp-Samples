
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

using ServiceDiscovery.Model;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ServiceDiscovery.ViewModels
{
    class RegisterPageViewModel : ViewModelBase
    {

        #region fields

        /// <summary>
        /// String containing service type.
        /// </summary>
        private string serviceType;

        /// <summary>
        /// String containing service name.
        /// </summary>
        private string serviceName;

        /// <summary>
        /// Value representing port number.
        /// </summary>
        private int? portNumber;

        /// <summary>
        /// Flag indicating whether service can be registered.
        /// </summary>
        private bool canRegister;

        /// <summary>
        /// Reference to the object of the ServiceDiscoveryModel class.
        /// </summary>
        private ServiceDiscoveryModel serviceDiscoveryModel;

        #endregion fields 

        #region properties

        /// <summary>
        /// Property providing service type.
        /// </summary>
        public string ServiceType
        {
            set
            {
                SetProperty(ref serviceType, value);
                CanRegister = IsRegisterable();
            }
            get { return serviceType; }
        }

        /// <summary>
        /// Property providing service name.
        /// </summary>
        public string ServiceName
        {
            set
            {
                SetProperty(ref serviceName, value);
                CanRegister = IsRegisterable();
            }
            get { return serviceName; }
        }

        /// <summary>
        /// Property providing string representation of port number.
        /// </summary>
        public string PortNumber
        {
            set
            {
                if (value == string.Empty)
                {
                    portNumber = null;
                }
                else if (int.TryParse(value, out int port))
                {
                    if (port < 0 || port > 65535)
                    {
                        CreateAlert("Error", "Port number must be in range between 0 and 65535");
                    }
                    else
                    {
                        portNumber = port;
                    }
                }
                CanRegister = IsRegisterable();
                OnPropertyChanged("PortNumber");
            }
            get { return portNumber?.ToString() ?? String.Empty; }
        }

        /// <summary>
        /// Property informing whether service can be registered.
        /// </summary>
        public bool CanRegister
        {
            set { SetProperty(ref canRegister, value); }
            get { return canRegister; }
        }

        /// <summary>
        /// Registers service.
        /// </summary>
        public ICommand RegisterServiceCommand { get; set; }

        #endregion properties

        #region methods

        /// <summary>
        /// RegisterPageViewModel constructor.
        /// </summary>
        public RegisterPageViewModel()
        {
            serviceDiscoveryModel = new ServiceDiscoveryModel();

            RegisterServiceCommand = new Command(RegisterService);
        }

        /// <summary>
        /// Function determines whether all fields have been filled.
        /// </summary>
        /// <returns> Flag indicating whether service can be registered. </returns>
        public bool IsRegisterable()
        {
            return !String.IsNullOrWhiteSpace(ServiceType) &&
                   !String.IsNullOrWhiteSpace(ServiceName) &&
                   portNumber.HasValue;
        }

        /// <summary>
        /// Function registers DNSSD service
        /// </summary>
        public void RegisterService()
        {
            // check port number
            // port number should be an integer value between 0 and 65535
            if (!portNumber.HasValue)
            {
                CreateAlert("Error", "Invalid Port Number");
                return;
            }

            try
            {
                // Actually register DNS-SD service
                serviceDiscoveryModel.RegisterDnssdService(ServiceType, ServiceName, portNumber.Value);
            }
            catch (Exception)
            {
                // Service type is a form _<service protocol>._<transport protocol>
                // Transport protocol must be tcp or udp
                CreateAlert("Error", "service type must be in form of _<protocol>._tcp or _<protocol>._udp");
                return;
            }

            CreateAlert("Success", "Service is Registered");
        }

        #endregion methods

    }
}
