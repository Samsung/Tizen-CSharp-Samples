
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ServiceDiscovery.ViewModels
{
    public class DiscoveryPageViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// String containing searched service type.
        /// </summary>
        private string serviceType;

        /// <summary>
        /// Flag indicating whether service discovery is running.
        /// </summary>
        private bool isDiscovering;

        /// <summary>
        /// String containing title.
        /// </summary>
        private string titleText;

        /// <summary>
        /// String containing text describing action changing discovery state.
        /// </summary>
        private string actionText;

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
                OnPropertyChanged("CanScan");
            }
            get { return serviceType; }
        }

        /// <summary>
        /// Property indicating whether scan can be started.
        /// </summary>
        public bool CanScan
        {
            get { return !String.IsNullOrWhiteSpace(ServiceType); }
        }

        /// <summary>
        /// Property indicating whether service discovery is running.
        /// </summary>
        public bool IsDiscovering
        {
            set
            {
                SetProperty(ref isDiscovering, value);
                OnPropertyChanged("IsNotDiscovering");
            }
            get { return isDiscovering; }
        }

        /// <summary>
        /// Property indicating whether service discovery is not running.
        /// </summary>
        public bool IsNotDiscovering
        {
            get { return !IsDiscovering; }
        }

        /// <summary>
        /// Property providing title text.
        /// </summary>
        public string TitleText
        {
            set { SetProperty(ref titleText, value); }
            get { return titleText; }
        }

        /// <summary>
        /// Property providing text describing action changing discovery state.
        /// </summary>
        public string ActionText
        {
            set { SetProperty(ref actionText, value); }
            get { return actionText; }
        }

        /// <summary>
        /// Starts or stops service discovery.
        /// </summary>
        public ICommand ChangeDiscoveryStateCommand { get; set; }

        /// <summary>
        /// List of detected services.
        /// </summary>
        public ObservableCollection<DnssdService> Services { get; set; }

        #endregion properties

        #region methods

        /// <summary>
        /// DiscoveryPageViewModel constructor.
        /// </summary>
        public DiscoveryPageViewModel()
        {
            ActionText = "Start search";
            TitleText = "Service type";
            serviceDiscoveryModel = new ServiceDiscoveryModel();

            Services = new ObservableCollection<DnssdService>();

            ChangeDiscoveryStateCommand = new Command(ChangeDiscoveryState);
        }

        /// <summary>
        /// Starts or stops service discovery.
        /// </summary>
        private void ChangeDiscoveryState()
        {
            if (IsDiscovering)
            {
                StopDiscovery();
            }
            else
            {
                StartDiscovery();
            }
        }

        /// <summary>
        /// Function starts service discovery.
        /// </summary>
        private void StartDiscovery()
        {
            IsDiscovering = true;

            // Clear found services
            Services.Clear();

            // Update texts for labels to match 
            ActionText = "Stop search";
            TitleText = "Found services";

            // Add event handler
            // It is invoked when a DNS-SD service is found
            serviceDiscoveryModel.DnssdServiceFound += OnServiceFound;

            try
            {
                // Start discovering DNS-SD services with the same service type
                serviceDiscoveryModel.StartDiscoverDnssdService(ServiceType);

            }
            catch (Exception)
            {
                CreateAlert("Alert", "Unexpected error");
            }
        }

        /// <summary>
        /// Function stops service discovery
        /// </summary>
        private void StopDiscovery()
        {
            IsDiscovering = false;

            ActionText = "Start search";
            TitleText = "Service type";

            try
            {
                // Remove event handler
                serviceDiscoveryModel.DnssdServiceFound -= OnServiceFound;

                // Stop discovering DNS-SD services
                serviceDiscoveryModel.StopDiscoverDnssdService();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Event handler when a service is found
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void OnServiceFound(object sender, DnssdDiscoveryEventArgs e)
        {
            if (!Services.Any((s) => s.Name == e.service.Name))
                Services.Add(e.service);
        }

        #endregion methods

    }
}
