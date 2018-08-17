// Copyright 2018 Samsung Electronics Co., Ltd
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppHistory
{
    /// <summary>
    /// Enumeration for application history query type
    /// </summary>
    public enum QueryType
    {
        RecentlyUsedApplications,
        FrequentlyUsedApplications,
        BatteryConsumingApplications,
    }

    /// <summary>
    /// The main page of the appHistory application
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppHistoryMain : CirclePage
    {
        private readonly PrivacyCheckImplementation privacyCheckImplementation;

        /// <summary>
        /// Constructor of AppHistoryMain class
        /// </summary>
        public AppHistoryMain()
        {
            InitializeComponent();

            CreateListView();

            privacyCheckImplementation = new PrivacyCheckImplementation();
            privacyCheckImplementation.CheckPermission();
        }

        /// <summary>
        /// Create a new list view to present application history items.
        /// </summary>
        private void CreateListView()
        {
            List<AppHistoryItem> historyItems = new List<AppHistoryItem>
            {
                new AppHistoryItem(
                    "Top 5 recently used applications",
                    "during the last 5 hours",
                    QueryType.RecentlyUsedApplications),
                new AppHistoryItem(
                    "Top 10 frequently used applications",
                    "during the last 3 days",
                    QueryType.FrequentlyUsedApplications),
                new AppHistoryItem(
                    "Top 10 battery consuming applications",
                    "since the last time when the device was fully charged",
                    QueryType.BatteryConsumingApplications)
            };
            appHistoryMainList.ItemsSource = historyItems;
        }

        /// <summary>
        /// This method will be called when a list item is selected.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            LogImplementation log = new LogImplementation();

            if (e.SelectedItem == null)
            {
                return;
            }

            ((CircleListView)sender).SelectedItem = null;

            // Push corresponding AppHistoryInformationPage
            // to present application history query result according to selected item
            await Navigation.PushAsync(
                new AppHistoryInformationPage((e.SelectedItem as AppHistoryItem).InfoType));
        }
    }
}
