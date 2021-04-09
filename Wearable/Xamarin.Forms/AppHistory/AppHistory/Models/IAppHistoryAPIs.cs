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

namespace AppHistory
{
    /// <summary>
    /// Interface to use Tizen.Context.AppHistory API
    /// </summary>
    public interface IAppHistoryAPIs
    {
        /// <summary>
        /// Query top 5 recently used applications during the last 5 hours.
        /// </summary>
        /// <returns>List of application history information</returns>
        List<StatsInfoItem> QueryRecentlyUsedApplications();

        /// <summary>
        /// Query top 10 frequently used applications during the last 3 days.
        /// </summary>
        /// <returns>List of application history information</returns>
        List<StatsInfoItem> QueryFrequentlyUsedApplications();

        /// <summary>
        /// Query top 10 battery consuming applications
        /// since the last time when the device has fully charged.
        /// </summary>
        /// <returns>List of application history information</returns>
        List<StatsInfoItem> QueryBatteryConsumingApplications();
    }
}
