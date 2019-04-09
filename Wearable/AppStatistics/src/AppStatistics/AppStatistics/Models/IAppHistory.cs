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
using AppStatistics.Utils;
using System.Collections.Generic;

namespace AppStatistics.Models
{
    /// <summary>
    /// Interface to use Tizen.Context.AppHistory API.
    /// Responsible for providing data about the applications.
    /// </summary>
    public interface IAppHistory
    {
        #region methods

        /// <summary>
        /// Gets list of the applications with their battery consumption since the device was fully charged.
        /// </summary>
        /// <returns>Information about applications battery consumption since the device was fully charged.</returns>
        List<ApplicationStatisticsItem> QueryBatteryConsumingApplications();

        /// <summary>
        /// Gets list of the applications usage history.
        /// </summary>
        /// <param name="option">Specifies which range should be considered.</param>
        /// <returns>List of applications usage history.</returns>
        List<ApplicationStatisticsItem> QueryApplicationsUsageHistory(Range option);

        #endregion
    }
}