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
using NetworkApp.Views;
using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;

namespace NetworkApp.ViewModels
{
    /// <summary>
    /// ViewModel for WiFiPage
    /// </summary>
    class ConnectionProfileViewModel : ViewModelBase
    {
        /// <summary>
        /// INavigation instance to allow push new pages from a ViewModel
        /// </summary>
        private INavigation _navigation;

        /// <summary>
        /// String with currently connected network name
        /// </summary>
        private string _connectedNetwork;

        /// <summary>
        /// Holds the method which returns connection info.
        /// </summary>
        private ConnectionProfileInfo _info = new ConnectionProfileInfo();

        /// <summary>
        /// Initializes a new instance of the <see cref="WiFiViewModel"/> class
        /// </summary>
        /// <param name="navigation">Navigation instance</param>
        public ConnectionProfileViewModel(INavigation navigation)
        {
            _navigation = navigation;

            PushProfileListPageCommand = new Command(PushProfileListPage);

            RefreshBindings();
        }


        /// <summary>
        /// Gets or sets the text with info about currently connected network
        /// </summary>
        public string CurrentConnectionLabel
        {
            get
            {
                if (_connectedNetwork == null)
                {
                    return "Disconnected";
                }
                else
                {
                    return "Current Connection:\n" + _connectedNetwork + "\nInterface: " + _info.GetCurrentProfileInterfaceName();
                }
            }

            set
            {
                SetProperty(ref _connectedNetwork, value);
            }
        }

        
        /// <summary>
        /// Gets or sets the command to execute when scan networks button is clicked
        /// </summary>
        public Command PushProfileListPageCommand { get; set; }

        
        /// <summary>
        /// Refreshes bindings on a page
        /// </summary>
        public void RefreshBindings()
        {
            try
            {                
                _connectedNetwork = _info.GetCurrentProfile();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
            }

            OnPropertyChanged(nameof(CurrentConnectionLabel));
        }


        /// <summary>
        /// Pushes new NetworkListPage
        /// </summary>
        private async void PushProfileListPage()
        {
            try
            {
                await _navigation.PushModalAsync(new ProfileListPage());
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
            }

            OnPropertyChanged(nameof(CurrentConnectionLabel));
        }
    }
}
