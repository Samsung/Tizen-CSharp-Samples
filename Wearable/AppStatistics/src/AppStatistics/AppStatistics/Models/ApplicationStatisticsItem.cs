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
namespace AppStatistics.Models
{
    /// <summary>
    /// Stores statistics information about the application.
    /// </summary>
    public class ApplicationStatisticsItem
    {
        #region properties

        /// <summary>
        /// Identification number of the application item.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of the application.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of launches.
        /// </summary>
        public int LaunchCount { get; set; }

        /// <summary>
        /// Used percent of the battery.
        /// </summary>
        public int Battery { get; set; }

        /// <summary>
        /// Duration of the application usage.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Time and date of the last launch.
        /// </summary>
        public string LastLaunchTime { get; set; }

        #endregion
    }
}
