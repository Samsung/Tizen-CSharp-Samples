/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using Xamarin.Forms;

namespace Badges.ViewModels
{
    /// <summary>
    /// Class to maintain application list item on the application list view.
    /// </summary>
    internal class AppListItemViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// BadgeCounter property's back-field.
        /// </summary>
        private double _badgeCounter;

        /// <summary>
        /// BadgeCounterString property's back-field.
        /// </summary>
        private string _badgeCounterString = "0";

        #endregion fields

        #region properties

        /// <summary>
        /// Application's ID.
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// Application's name.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Availability of the application to be modified.
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Badge counter string used to display.
        /// </summary>
        public string BadgeCounterString
        {
            get => _badgeCounterString;
            set => SetProperty(ref _badgeCounterString, value);
        }

        /// <summary>
        /// Actual badge counter value.
        /// </summary>
        public double BadgeCounter
        {
            get => _badgeCounter;
            set
            {
                if (_badgeCounter != value)
                {
                    _badgeCounter = value;
                    BadgeCounterString = value <= 99 ? value.ToString() : "99+";
                }
            }
        }

        #endregion properties
    }
}
