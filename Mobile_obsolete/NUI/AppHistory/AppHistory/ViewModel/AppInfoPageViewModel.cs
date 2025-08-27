/*
 * Copyright (c) 2023 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Tizen.Applications;
using Tizen.Context.AppHistory;

namespace AppHistory.ViewModel
{
    public enum ListType
    {
        Recently,
        Frequently,
        Battery
    }
    class AppInfoPageViewModel
    {
        public ObservableCollection<AppInfo> Source { get; private set; } = new ObservableCollection<AppInfo>();

        public AppInfoPageViewModel(ListType listType)
        {
            switch (listType)
            {
                case ListType.Recently:
                    QueryRecentlyUsedApplications();
                    break;
                case ListType.Frequently:
                    QueryFrequentlyUsedApplications();
                    break;
                case ListType.Battery:
                    QueryBatteryConsumingApplications();
                    break;
            }
        }

        void QueryRecentlyUsedApplications()
        {
            // Create an UsageStatistics
            var usageStats = new UsageStatistics(UsageStatistics.SortOrderType.LastLaunchTimeNewest);
            // Query top 5 recently used applications during the last 5 hours
            var usageStatsResult = usageStats.Query(DateTime.Now.AddHours(-5), DateTime.Now, 5);

            foreach (var record in usageStatsResult)
            {
                var appInfo = new ApplicationInfo(record.AppId);

                string name = (!appInfo.Label.Equals(string.Empty)) ? appInfo.Label : record.AppId;
                string info = "LastLaunchTime: " + record.LastLaunchTime + "\r\n";
                info += "; LaunchCount: " + record.LaunchCount + "\r\n";
                info += "; Duration: " + record.Duration + " secs";

                // Add each record to the source list
                Source.Add(new AppInfo(name, info));

                appInfo.Dispose();
            }
        }

        void QueryFrequentlyUsedApplications()
        {
            // Create an UsageStatistics instance
            var usageStats = new UsageStatistics(UsageStatistics.SortOrderType.LaunchCountMost);
            // Query top 10 frequently used applications during the last 3 days
            var usageStatsResult = usageStats.Query(DateTime.Now.AddDays(-3), DateTime.Now, 10);

            foreach (var record in usageStatsResult)
            {
                var appInfo = new ApplicationInfo(record.AppId);

                string name = (!appInfo.Label.Equals(string.Empty)) ? appInfo.Label : record.AppId;
                string info = "LaunchCount: " + record.LaunchCount + "\r\n";
                info += "; LastLaunchTime: " + record.LastLaunchTime + "\r\n";
                info += "; Duration: " + record.Duration + " secs";

                // Add each record to the Source list
                Source.Add(new AppInfo(name, info));

                appInfo.Dispose();
            }
        }

        void QueryBatteryConsumingApplications()
        {
            // Create a BatteryStatistics instance
            var batteryStats = new BatteryStatistics();
            // Query top 10 battery consuming applications since the last time when the device has fully charged
            var batteryStatsResult = batteryStats.Query(BatteryStatistics.GetLastFullyChargedTime(), DateTime.Now, 10);

            foreach (var record in batteryStatsResult)
            {
                var appInfo = new ApplicationInfo(record.AppId);

                string name = (!appInfo.Label.Equals(string.Empty)) ? appInfo.Label : record.AppId;
                string info = "Consumption: " + record.Consumption + " %";

                // Add each record to the Source list
                Source.Add(new AppInfo(name, info));

                appInfo.Dispose();
            }
        }
    }

    class AppInfo : INotifyPropertyChanged
    {
        private string name;
        private string information;

        public string Name
        {
            get => name;
        }
        public string Information
        {
            get => information;
        }

        public AppInfo(string _name, string _information)
        {
            name = _name;
            information = _information;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}