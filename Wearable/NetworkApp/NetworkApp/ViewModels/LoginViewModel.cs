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
using System;
using Tizen.Network.Connection;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace NetworkApp.ViewModels
{
    /// <summary>
    /// ViewModel for LoginPage
    /// </summary>
    class LoginViewModel : ViewModelBase
    {
        /// <summary>
        /// Instance of WiFiApiManager
        /// </summary>
        private readonly WiFiApiManager _wifi = new WiFiApiManager();

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class
        /// </summary>
        /// <param name="apName">Name of AP to login</param>
        public LoginViewModel(string apName)
        {
            APName = apName;
            ConnectClickedCommand = new Command(ConnectClickedAsync);
            ForgetClickedCommand = new Command(ForgetClicked);
        }

        /// <summary>
        /// Gets or sets Access Point ESSID to login
        /// </summary>
        public string APName { get; set; }

        /// <summary>
        /// Indicates if info about current Access Point is stored
        /// </summary>
        public bool IsStored => _wifi.IsAPInfoStored(APName); // TODO: Not working now

        /// <summary>
        /// Holds placeholder for password entry.
        /// </summary>
        public string Placeholder => IsStored ? "AP info is stored" : "Password";

        /// <summary>
        /// Indicates if password is required for current Access Point
        /// </summary>
        public bool IsPasswordRequired => _wifi.GetAPSecurityType(APName) != WiFiSecurityType.None;

        /// <summary>
        /// Gets or sets text to show on connectButton
        /// </summary>
        public string ConnectButtonText { get; set; } = "Connect";

        /// <summary>
        /// Gets or sets password user entered in popup Entry
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets command executed after connect button click
        /// </summary>
        public Command ConnectClickedCommand { get; set; }

        /// <summary>
        /// Gets or sets command executed after forget button click
        /// </summary>
        public Command ForgetClickedCommand { get; set; }

        /// <summary>
        /// Connects to choosen Access Point with password entered by user
        /// </summary>
        private async void ConnectClickedAsync()
        {
            try
            {
                ConnectButtonText = "Connecting...";
                RefreshBindings();
                await _wifi.Connect(APName, Password);
                // No exception, so wifi connected
                Toast.DisplayText("Connected!");
                RefreshBindings();
            }
            catch (Exception ex)
            {
                Toast.DisplayText("Connection failed");
                Logger.Log(ex.Message);
            }

            Password = null;
            ConnectButtonText = "Connect";
            RefreshBindings();
        }

        /// <summary>
        /// Forgets current Access Point
        /// </summary>
        private void ForgetClicked()
        {
            try
            {
                _wifi.Forget(APName);
                RefreshBindings();
                Toast.DisplayText("Forgotten!");
            }
            catch (Exception ex)
            {
                Toast.DisplayText("Forget failed!");
                Logger.Log(ex.Message);
            }
        }

        /// <summary>
        /// Refreshes bindings on the page
        /// </summary>
        private void RefreshBindings()
        {
            OnPropertyChanged(nameof(ConnectButtonText));
            OnPropertyChanged(nameof(IsStored));
            OnPropertyChanged(nameof(Placeholder));
            OnPropertyChanged(nameof(Password));
        }
    }
}