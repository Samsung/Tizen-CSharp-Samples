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
using System.Collections.ObjectModel;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Network.Connection;

namespace NetworkApp.Views
{
    /// <summary>
    /// NetworkListPage class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileListPage : CirclePage
    {
        /// <summary>
        /// Message showing during Access Points scan
        /// </summary>
        private static string s_scanningMessage = "Scanning...";

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileListPage"/> class
        /// </summary>
        public ProfileListPage()
        {
            InitializeComponent();
            RotaryFocusObject = listView;
            SetNetworkListAsync();
            listView.ItemsSource = ProfileList;
        }

        /// <summary>
        /// Gets or sets Observable Collection holding found Profiles
        /// </summary>
        public ObservableCollection<string> ProfileList { get; set; } = new ObservableCollection<string> { s_scanningMessage };

        /// <summary>
        /// Scans for APs and sets NetworkList with scan result.
        /// </summary>
        private void SetNetworkListAsync()
        {
            try
            {
                ConnectionProfileInfo info = new ConnectionProfileInfo();

                var profileList = info.GetProfileList();

                ProfileList.Clear();
                foreach (var n in profileList)
                {
                    ProfileList.Add(n.Name);
                }
            }
            catch (NotSupportedException e)
            {
                Toast.DisplayText("Not supported");
                Logger.Log(e.Message);
            }
            catch (Exception e)
            {
                Toast.DisplayText("Retrieving list failed!");
                Logger.Log(e.Message);
            }
        }

        /// <summary>
        /// Event handler called when selected network is tapped.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Arguments of event</param>
        private void NetworkTapped(object sender, ItemTappedEventArgs e)
        {
            if (ReferenceEquals(e.Item, s_scanningMessage))
            {
                return;
            }

            Navigation.PushModalAsync(new ProfileInfoPage(e.Item.ToString()));
        }
    }
}
