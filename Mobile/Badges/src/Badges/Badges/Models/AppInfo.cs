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

namespace Badges.Models
{
    /// <summary>
    /// Class for sharing needed information about application.
    /// </summary>
    public class AppInfo
    {
        #region fields

        /// <summary>
        /// Application displayed name.
        /// </summary>
        public string AppName;

        /// <summary>
        /// Application ID (for future identifying particular application).
        /// </summary>
        public string AppId;

        /// <summary>
        /// Flag that determines if the application is available for badge counter modification by user.
        /// </summary>
        public bool IsAvailable;

        /// <summary>
        /// Current system badge counter value for application.
        /// </summary>
        public double BadgeCounter;

        #endregion fields

        #region methods

        /// <summary>
        /// Constructor to set new class object values.
        /// </summary>
        /// <param name="appName">Saved in <see cref="AppName"/> field</param>
        /// <param name="appId">Saved in <see cref="AppId"/> field</param>
        /// <param name="isAvailable">Saved in <see cref="IsAvailable"/> field</param>
        /// <param name="badgeCounter">Saved in <see cref="BadgeCounter"/> field</param>
        public AppInfo(string appName, string appId, bool isAvailable, double badgeCounter)
        {
            AppName = appName;
            AppId = appId;
            IsAvailable = isAvailable;
            BadgeCounter = badgeCounter;
        }

        #endregion methods
    }
}