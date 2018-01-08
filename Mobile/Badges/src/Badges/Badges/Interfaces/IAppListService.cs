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

using Badges.Models;

namespace Badges.Interfaces
{
    /// <summary>
    /// Common interface for getting all applications list and setting application badge counter.
    /// </summary>
    public interface IAppListService
    {
        /// <summary>
        /// Retrieves list of applications installed on device. Application should contain needed information for
        /// identifying it and setting it's badge counter.
        /// </summary>
        /// <param name="applicationListCb">Delegate called for every retrieved application</param>
        void GetAppList(AddAppToListDelegate applicationListCb);

        /// <summary>
        /// Sets badge counter for specified application.
        /// </summary>
        /// <param name="appId">Application ID needed to identify particular application</param>
        /// <param name="badgeCount">New value of badge counter to set</param>
        void SetBadge(string appId, int badgeCount);
    }

    /// <summary>
    /// Delegate used for calling on every retrieved application from system to add it to our application list.
    /// </summary>
    /// <param name="appInfo">Detailed application informations</param>
    public delegate void AddAppToListDelegate(AppInfo appInfo);
}
