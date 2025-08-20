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
using Tizen.Context.AppHistory;
using Tizen.Applications;

namespace AppHistory
{
    /// <summary>
    /// Implementation class of IAppHistoryAPIs interface
    /// </summary>
    public class AppHistoryImplementation : IAppHistoryAPIs
    {
        private readonly ILog log;

        /// <summary>
        /// Constructor of AppHistoryImplementation
        /// </summary>
        public AppHistoryImplementation()
        {
            log = new LogImplementation();
        }

        /// <summary>
        /// Query top 5 recently used applications during the last 5 hours
        /// </summary>
        /// <returns>List of application history information</returns>
        public List<StatsInfoItem> QueryRecentlyUsedApplications()
        {
            try
            {
                // Create a UsageStatistics
                var usageStats = new UsageStatistics(UsageStatistics.SortOrderType.LastLaunchTimeNewest);
                
                // Query top 5 recently used applications during the last 5 hours
                var usageStatsResult = usageStats.Query(DateTime.Now.AddHours(-5), DateTime.Now, 5);

                List<StatsInfoItem> result = new List<StatsInfoItem>();
                foreach (var record in usageStatsResult)
                {
                    var appInfo = new ApplicationInfo(record.AppId);

                    string name = (!appInfo.Label.Equals(string.Empty)) ? appInfo.Label : record.AppId;
                    string info = "LastLaunchTime:\r\n" + record.LastLaunchTime + "\r\n";
                    info += "LaunchCount: " + record.LaunchCount + "\r\n";
                    info += "Duration: " + record.Duration + " secs";

                    // Add each record to the result list
                    result.Add(new StatsInfoItem(name, info));

                    appInfo.Dispose();
                }

                return result;
            }
            catch (Exception e)
            {
                log.Log(e.Message.ToString());
            }

            return null;
        }

        /// <summary>
        /// Query top 10 frequently used applications during the last 3 days
        /// </summary>
        /// <returns>List of application history information</returns>
        public List<StatsInfoItem> QueryFrequentlyUsedApplications()
        {
            try
            {
                // Create an UsageStatistics instance
                var usageStats = new UsageStatistics(UsageStatistics.SortOrderType.LaunchCountMost);
                
                // Query top 10 frequently used applications during the last 3 days
                var usageStatsResult = usageStats.Query(DateTime.Now.AddDays(-3), DateTime.Now, 10);

                List<StatsInfoItem> result = new List<StatsInfoItem>();
                foreach (var record in usageStatsResult)
                {
                    var appInfo = new ApplicationInfo(record.AppId);

                    string name = (!appInfo.Label.Equals(string.Empty)) ? appInfo.Label : record.AppId;
                    string info = "LaunchCount: " + record.LaunchCount + "\r\n";
                    info += "LastLaunchTime:\r\n" + record.LastLaunchTime + "\r\n";
                    info += "Duration: " + record.Duration + " secs";
                    
                    // Add each record to the result list
                    result.Add(new StatsInfoItem(name, info));

                    appInfo.Dispose();
                }

                return result;
            }
            catch (Exception e)
            {
                log.Log(e.Message.ToString());
            }

            return null;
        }

        /// <summary>
        /// Query top 10 battery consuming applications since the last time when the device was fully charged
        /// </summary>
        /// <returns>List of application history information</returns>
        public List<StatsInfoItem> QueryBatteryConsumingApplications()
        {
            try
            {
                // Create a BatteryStatistics instance
                var batteryStats = new BatteryStatistics();
                
                // Query top 10 battery consuming applications since the last time when the device was fully charged
                var batteryStatsResult = batteryStats.Query(BatteryStatistics.GetLastFullyChargedTime(), DateTime.Now, 10);

                List<StatsInfoItem> result = new List<StatsInfoItem>();
                foreach (var record in batteryStatsResult)
                {
                    var appInfo = new ApplicationInfo(record.AppId);

                    string name = (!appInfo.Label.Equals(string.Empty)) ? appInfo.Label : record.AppId;
                    string info = "Consumption: " + record.Consumption + " %";

                    // Add each record to the result list
                    result.Add(new StatsInfoItem(name, info));

                    appInfo.Dispose();
                }

                return result;
            }
            catch (Exception e)
            {
                log.Log(e.Message.ToString());
            }

            return null;
        }
    }
}
