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

using Badges.Interfaces;
using Xamarin.Forms;

namespace Badges.Models
{
    /// <summary>
    /// Class to call platform specific methods to obtain application list and modify badge counter.
    /// </summary>
    internal class AppListModel
    {
        /// <summary>
        /// Obtains application list.
        /// </summary>
        /// <param name="applicationListCb">Delegate called on every obtained application.</param>
        public static void GetAppList(AddAppToListDelegate applicationListCb)
        {
            DependencyService.Get<IAppListService>().GetAppList(applicationListCb);
        }

        /// <summary>
        /// Sets badge counter for specified application.
        /// </summary>
        /// <param name="appId">Application's ID.</param>
        /// <param name="badgeCount">New Badge counter value to set.</param>
        public static void SetBadge(string appId, int badgeCount)
        {
            DependencyService.Get<IAppListService>().SetBadge(appId, badgeCount);
        }
    }
}
