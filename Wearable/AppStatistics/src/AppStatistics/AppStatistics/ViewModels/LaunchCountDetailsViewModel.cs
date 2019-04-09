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
using AppStatistics.Models;

namespace AppStatistics.ViewModels
{
    /// <summary>
    /// View model class responsible for information about the application statistics.
    /// </summary>
    class LaunchCountDetailsViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Name of the application.
        /// </summary>
        private string _name;

        /// <summary>
        /// Number of launches.
        /// </summary>
        private int _launchCount;

        /// <summary>
        /// Time and date of the last launch.
        /// </summary>
        private string _lastLaunchTime;

        /// <summary>
        /// Duration of the application usage.
        /// </summary>
        private int _duration;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets name of the application.
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        /// Gets or sets number of launches.
        /// </summary>
        public int LaunchCount
        {
            get => _launchCount;
            set => SetProperty(ref _launchCount, value);
        }

        /// <summary>
        /// Gets or sets time and date of the last launch.
        /// </summary>
        public string LastLaunchTime
        {
            get => _lastLaunchTime;
            set => SetProperty(ref _lastLaunchTime, value);
        }

        /// <summary>
        /// Gets or sets duration of the application usage.
        /// </summary>
        public int Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class.
        /// </summary>
        /// <param name="application">Specifies the application item which should be displayed.</param>
        public LaunchCountDetailsViewModel(ApplicationStatisticsItem application)
        {
            Name = application.Name;
            LaunchCount = application.LaunchCount;
            LastLaunchTime = application.LastLaunchTime;
            Duration = application.Duration;
        }

        #endregion
    }
}
