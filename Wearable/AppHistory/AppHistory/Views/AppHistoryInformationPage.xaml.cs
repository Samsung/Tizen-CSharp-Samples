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

using System;
using System.Collections.Generic;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppHistory
{
    /// <summary>
    /// The ContentPage to show the retrieved application history result
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppHistoryInformationPage : CirclePage
    {
        /// <summary>
        /// Interface for using AppHistory APIs
        /// </summary>
        private static IAppHistoryAPIs appHistoryAPIs;

        private readonly ILog log;

        /// <summary>
        /// Queried result of application history
        /// </summary>
        private List<StatsInfoItem> statsInfo;

        /// <summary>
        /// Constructor of AppHistoryInformationPage
        /// </summary>
        /// <param name="infoType">The type of information to show</param>
        public AppHistoryInformationPage(QueryType infoType)
        {
            log = new LogImplementation();
            try
            {
                InitializeComponent();

                // Initialize StatisticsInfo list
                statsInfo = new List<StatsInfoItem>();

                // Query application history 
                appHistoryAPIs = new AppHistoryImplementation();
                Query(infoType);

                // Set the item source of list view
                appHistoryInformationList.ItemsSource = statsInfo;
            }
            catch (Exception e)
            {
                log.Log(e.Message);
            }
        }

        /// <summary>
        /// Query application history according to its type
        /// </summary>
        /// <param name="queryType">Application history type to query</param>
        private void Query(QueryType queryType)
        {
            try
            {
                switch (queryType)
                {
                    case QueryType.RecentlyUsedApplications:
                        statsInfo = appHistoryAPIs.QueryRecentlyUsedApplications();
                        break;
                    case QueryType.FrequentlyUsedApplications:
                        statsInfo = appHistoryAPIs.QueryFrequentlyUsedApplications();
                        break;
                    case QueryType.BatteryConsumingApplications:
                        statsInfo = appHistoryAPIs.QueryBatteryConsumingApplications();
                        break;
                    default:
                        statsInfo = null;
                        break;
                }
            }
            catch (Exception e)
            {
                log.Log(e.Message);
            }
        }

        /// <summary>
        /// This method will be called when a list item is selected.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
