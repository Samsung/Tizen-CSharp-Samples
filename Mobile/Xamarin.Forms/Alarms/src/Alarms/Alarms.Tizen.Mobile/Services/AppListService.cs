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

using System.Collections.Generic;
using Alarms.Models;
using Alarms.Tizen.Mobile.Services;
using Tizen.Applications;

[assembly: Xamarin.Forms.Dependency(typeof(AppListService))]

namespace Alarms.Tizen.Mobile.Services
{
    internal class AppListService : IAppListService
    {
        #region methods

        /// <summary>
        /// Returns information about all available applications which are selectable from application menu.
        /// </summary>
        /// <param name="applicationListCb">Delegate to call on every application retrieval.</param>
        public async void GetAppList(AddAppToListDelegate applicationListCb)
        {
            IEnumerable<ApplicationInfo> installedApplications =
                await ApplicationManager.GetInstalledApplicationsAsync();

            foreach (ApplicationInfo app in installedApplications)
            {
                if (!app.IsNoDisplay)
                {
                    applicationListCb(new AppInfo(app.Label, app.ApplicationId));
                }
            }
        }

        #endregion
    }
}
