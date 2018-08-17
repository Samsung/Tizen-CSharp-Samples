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
using NetworkApp.Tizen.Mobile;
using NetworkApp.Views;
using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace NetworkApp.ViewModels
{
    /// <summary>
    /// ViewModel for WiFiPage
    /// </summary>
    class WiFiViewModel : ViewModelBase
    {
        /// <summary>
        /// INavigation instance to allow push new pages from a ViewModel
        /// </summary>
        private INavigation _navigation;

        /// <summary>
        /// Indicates if Wi-Fi is turned on
        /// </summary>
        private bool _isTurnedOn = false;

        /// <summary>
        /// String with currently connected network name
        /// </summary>
        private string _connectedNetwork;

        /// <summary>
        /// Instance of WiFiApiManager
        /// </summary>
        private readonly WiFiApiManager _wifi = new WiFiApiManager();

        /// <summary>
        /// Initializes a new instance of the <see cref="WiFiViewModel"/> class
        /// </summary>
        /// <param name="navigation">Navigation instance</param>
        public WiFiViewModel(INavigation navigation)
        {
            _wifi = new WiFiApiManager();
            _navigation = navigation;

            ChangeConnectionStatusCommand = new Command(ChangeConnectionStatus);
            PushScanNetworksPageCommand = new Command(PushScanNetworksPage);
            DisconnectCommand = new Command(Disconnect);
            ForgetCommand = new Command(Forget);

            RefreshBindings();
        }

        /// <summary>
        /// Gets or sets value indicating the Wi-Fi state
        /// </summary>
        public bool IsTurnedOn
        {
            get => _isTurnedOn;
            set
            {
                SetProperty(ref _isTurnedOn, value);
                OnPropertyChanged(nameof(Status));
            }
        }

        /// <summary>
        /// Gets Wi-Fi state as string
        /// </summary>
        public string Status => IsTurnedOn ? "ON" : "OFF";

        /// <summary>
        /// Gets or sets the text with info about currently connected network
        /// </summary>
        public string ConnectedNetwork
        {
            get
            {
                if (_connectedNetwork == null)
                    return "Disconnected";
                else
                    return "Connected: " + _connectedNetwork;
            }
            set
            {
                SetProperty(ref _connectedNetwork, value);
            }
        }

        /// <summary>
        /// Indicates if connected to any network
        /// </summary>
        public bool IsConnected => _connectedNetwork == null ? false : true;

        /// <summary>
        /// Gets or sets the command to execute when Wi-Fi status change button is clicked
        /// </summary>
        public Command ChangeConnectionStatusCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to execute when scan networks button is clicked
        /// </summary>
        public Command PushScanNetworksPageCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to execute when disconnect button is clicked
        /// </summary>
        public Command DisconnectCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to execute when forget button is clicked
        /// </summary>
        public Command ForgetCommand { get; set; }

        /// <summary>
        /// Refreshes bindings on a page
        /// </summary>
        public void RefreshBindings()
        {
            try
            {
                IsTurnedOn = _wifi.IsActive();
                _connectedNetwork = _wifi.ConnectedAP();
                Logger.Log("IsTurnedOnError");
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
            }
            OnPropertyChanged(nameof(ConnectedNetwork));
            OnPropertyChanged(nameof(IsConnected));
            OnPropertyChanged(nameof(IsTurnedOn));
            OnPropertyChanged(nameof(Status));
        }

        /// <summary>
        /// Changes wi-fi status
        /// </summary>
        private async void ChangeConnectionStatus()
        {
            try
            {
                if (IsTurnedOn == false)
                {
                    if (!_wifi.IsActive())
                    {
                        await _wifi.Activate();
                    }
                    IsTurnedOn = true;
                }
                else
                {
                    if (_wifi.IsActive())
                    {
                        await _wifi.Deactivate();
                    }
                    IsTurnedOn = false;
                }
            }
            catch (NotSupportedException e)
            {
                Toast.DisplayText("Not supported!");
                Logger.Log(e.Message);
            }
            catch (Exception e)
            {
                Toast.DisplayText("Error occured!");
                Logger.Log(e.Message);
            }
        }

        /// <summary>
        /// Pushes new NetworkListPage
        /// </summary>
        private async void PushScanNetworksPage()
        {
            try
            {
                await _navigation.PushModalAsync(new NetworkListPage());
                _connectedNetwork = _wifi.ConnectedAP();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
            }

            OnPropertyChanged(nameof(ConnectedNetwork));
        }

        /// <summary>
        /// Disconnects from currently connected network
        /// </summary>
        private async void Disconnect()
        {
            try
            {
                await _wifi.Disconnect(_connectedNetwork);
                Logger.Log("Disconnect works");

            }
            catch (Exception e)
            {
                Toast.DisplayText("Disconnect failed!");
                Logger.Log(e.Message);
            }

            RefreshBindings();
        }

        /// <summary>
        /// Deletes the stored information of a currently connected AP
        /// and disconnects it.
        /// </summary>
        private void Forget()
        {
            try
            {
                _wifi.Forget(_connectedNetwork);
                Logger.Log("Forget works");
            }
            catch (Exception e)
            {
                Toast.DisplayText("Forget failed!");
                Logger.Log(e.Message);
            }

            RefreshBindings();
        }
    }
}
