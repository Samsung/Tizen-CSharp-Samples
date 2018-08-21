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
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetworkApp.Views
{
    /// <summary>
    /// RootPage class
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RootPage : CirclePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootPage"/> class
        /// </summary>
        public RootPage()
        {
            InitializeComponent();
            mainListView.ItemsSource = new List<string>
            {
                "Connection test",
                "Wi-Fi test",
            };
            RotaryFocusObject = mainListView;
        }

        /// <summary>
        /// Event raised when item on mainListView is tapped.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">Event arguments</param>
        private void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            switch (e.Item.ToString())
            {
                case "Connection test":
                    Navigation.PushModalAsync(new ConnectionPage());
                    return;
                case "Wi-Fi test":
                    Navigation.PushModalAsync(new WifiPage());
                    return;
            }
        }
    }
}