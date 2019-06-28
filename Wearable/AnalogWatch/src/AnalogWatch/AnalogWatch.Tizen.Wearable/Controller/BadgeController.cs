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
using Tizen.Applications;

namespace AnalogWatch.Tizen.Wearable.Controller
{
    /// <summary>
    /// Class used to handle the badge counters.
    /// </summary>
    public class BadgeController
    {
        #region properties

        /// <summary>
        /// Event invoked when the status of the badge changes.
        /// </summary>
        public event EventHandler<BadgeEventArgs> BadgeStatusChanged;

        #endregion properties

        #region methods

        /// <summary>
        /// Class constructor. The constructor is used to initialize the badge counters.
        /// </summary>
        public BadgeController()
        {
            try
            {
                if (!(BadgeControl.GetBadges() is List<Badge> badges))
                {
                    global::Tizen.Log.Error(((Program)Application.Current).LogTag, "No badges found");
                    return;
                }
            }
            catch (Exception e)
            {
                global::Tizen.Log.Error(((Program)Application.Current).LogTag, "BadgeControl exception: " + e.Message);
            }

            BadgeControl.Changed += BadgeControlOnChanged;
        }

        /// <summary>
        /// Handles the 'Changed' BadgeControl event.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="badgeEventArgs">Structure containing the badge data.</param>
        private void BadgeControlOnChanged(object sender, BadgeEventArgs badgeEventArgs)
        {
            BadgeStatusChanged?.Invoke(this, badgeEventArgs);
        }

        #endregion methods
    }
}