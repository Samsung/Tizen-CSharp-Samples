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
using System.Collections.Generic;
using AppStatistics.Utils;
using Xamarin.Forms;

namespace AppStatistics.Models
{
    /// <summary>
    /// Responsible for providing information about applications and their statistics.
    /// </summary>
    public class ApplicationStatisticsModel
    {
        #region fields

        /// <summary>
        /// Instance of application history service.
        /// </summary>
        private IAppHistory _appHistory;

        #endregion

        #region methods

        /// <summary>
        /// Initializes class.
        /// </summary>
        public ApplicationStatisticsModel()
        {
            _appHistory = DependencyService.Get<IAppHistory>();
        }

        /// <summary>
        /// Allows to get list with the statistics of the applications.
        /// </summary>
        /// <param name="option">Specifies which range should be considered.</param>
        /// <returns>List of the applications statistics.</returns>
        public List<ApplicationStatisticsItem> GetApplicationStatistics(Range option)
        {
            List<ApplicationStatisticsItem> items = new List<ApplicationStatisticsItem>();

            if (option == Range.Battery)
            {
                items = _appHistory.QueryBatteryConsumingApplications();
            }
            else
            {
                items = _appHistory.QueryApplicationsUsageHistory(option);
            }

            return items;
        }

        #endregion
    }
}
