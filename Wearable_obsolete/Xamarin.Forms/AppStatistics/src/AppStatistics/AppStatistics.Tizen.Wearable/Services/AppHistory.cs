/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Collections.Generic;
using AppStatistics.Models;
using Tizen.Context.AppHistory;
using Tizen;
using Tizen.Applications;
using AppStatistics.Tizen.Wearable.Services;
using AppStatistics.Utils;
using Range = AppStatistics.Utils.Range;

[assembly: Xamarin.Forms.Dependency(typeof(AppHistory))]
namespace AppStatistics.Tizen.Wearable.Services
{
    /// <summary>
    /// Implementation class of IAppHistory.
    /// Responsible for providing data about the applications from Tizen.Context.AppHistory API.
    /// </summary>
    class AppHistory : IAppHistory
    {
        #region methods

        /// <summary>
        /// Gets list of the applications with their battery consumption since the device was fully charged.
        /// </summary>
        /// <returns>Information about applications battery consumption since the device was fully charged.</returns>
        public List<ApplicationStatisticsItem> QueryBatteryConsumingApplications()
        {
            try
            {
                var batteryStats = new BatteryStatistics();

                var batteryStatsResult = batteryStats.Query(BatteryStatistics.GetLastFullyChargedTime(), DateTime.Now);

                List<ApplicationStatisticsItem> result = new List<ApplicationStatisticsItem>();

                foreach (var record in batteryStatsResult)
                {
                    using (var appInfo = new ApplicationInfo(record.AppId))
                    {
                        string name = (!appInfo.Label.Equals(string.Empty)) ? appInfo.Label : record.AppId;

                        ApplicationStatisticsItem applicationItem = new ApplicationStatisticsItem()
                        {
                            Name = name,
                            Battery = (int)record.Consumption
                        };

                        result.Add(applicationItem);
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                Log.Error("AppStatistics", e.Message);
            }

            return null;
        }

        /// <summary>
        /// Gets list of the applications usage history.
        /// </summary>
        /// <param name="option">Specifies which range should be considered.</param>
        /// <returns>List of applications usage history.</returns>
        public List<ApplicationStatisticsItem> QueryApplicationsUsageHistory(Range option)
        {
            DateTime range = new DateTime();

            if (option == Range.LastDay)
            {
                range = DateTime.Now.AddDays(-1);
            }
            else if (option == Range.LastWeek)
            {
                range = DateTime.Now.AddDays(-7);
            }
            else if (option == Range.LastMonth)
            {
                range = DateTime.Now.AddMonths(-1);
            }
            else if (option == Range.Livetime)
            {
                range = new DateTime(1970, 1, 1);
            }

            try
            {
                var usageStats = new UsageStatistics(UsageStatistics.SortOrderType.LastLaunchTimeNewest);

                var usageStatsResult = usageStats.Query(range, DateTime.Now);

                List<ApplicationStatisticsItem> result = new List<ApplicationStatisticsItem>();

                int id = 0;

                foreach (var record in usageStatsResult)
                {
                    using (var appInfo = new ApplicationInfo(record.AppId))
                    {
                        string name = (!appInfo.Label.Equals(string.Empty)) ? appInfo.Label : record.AppId;

                        ApplicationStatisticsItem applicationItem = new ApplicationStatisticsItem()
                        {
                            ID = id,
                            Name = name,
                            LastLaunchTime = record.LastLaunchTime.ToString(),
                            LaunchCount = record.LaunchCount,
                            Duration = record.Duration
                        };

                        result.Add(applicationItem);
                        id++;
                    }
                }

                return result;
            }
            catch (Exception e)
            {
                Log.Error("AppStatistics", e.Message);
            }

            return null;
        }

        #endregion
    }
}
