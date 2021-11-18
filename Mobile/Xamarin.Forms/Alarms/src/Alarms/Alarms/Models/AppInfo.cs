/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

namespace Alarms.Models
{
    /// <summary>
    /// Class for sharing information about application.
    /// </summary>
    public class AppInfo
    {
        #region properties

        /// <summary>
        /// Application displayed name.
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// Application ID (for future identifying particular application).
        /// </summary>
        public string AppId { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Constructor to set new class object values.
        /// </summary>
        /// <param name="appName">Saved in <see cref="AppName"/> field.</param>
        /// <param name="appId">Saved in <see cref="AppId"/> field.</param>
        public AppInfo(string appName, string appId)
        {
            AppName = appName;
            AppId = appId;
        }

        #endregion
    }
}
