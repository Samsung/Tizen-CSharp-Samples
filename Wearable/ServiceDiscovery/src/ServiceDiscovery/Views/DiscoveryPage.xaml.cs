﻿
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
using ServiceDiscovery.ViewModels;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ServiceDiscovery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DiscoveryPage : CirclePage
    {
        /// <summary>
        /// Discovery page constructor.
        /// </summary>
        public DiscoveryPage()
        {
            InitializeComponent();

            viewModel.AlertCreated += OnAlertCreated;
            list.ItemSelected += List_ItemSelected;
        }

        /// <summary>
        /// Event handler that creates details page for passed DNSSDService.
        /// </summary>
        /// <param name="sender"> Object that raised event </param>
        /// <param name="e"> Event arguments </param>
        private void List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var service = (DnssdService)e.SelectedItem;

            ((ListView)sender).SelectedItem = null;

            if (service != null)
                Navigation.PushAsync(new DetailsPage(service));
        }

        /// <summary>
        /// Event handler that displays alert.
        /// </summary>
        /// <param name="sender"> Object that raised event </param>
        /// <param name="e"> Event arguments </param>
        private void OnAlertCreated(object sender, AlertCreatedEventArgs e)
        {
            DisplayAlert(e.title, e.message, "OK");
        }

    }
}