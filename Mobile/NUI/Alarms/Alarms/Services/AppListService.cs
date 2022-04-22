/*
 * Copyright (c) 2022 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Text;
using Tizen.Applications;
using Alarms.Models;

namespace Alarms.Services
{
    public static class AppListService
    {
        public static List<AppInfo> GetAppList()
        {
            List<AppInfo> appList = new List<AppInfo>();
            IEnumerable<Package> packageList = PackageManager.GetPackages();
            foreach (Package pkg in packageList)
            {
                var list = pkg.GetApplications();
                foreach (var app in list)
                {
                    if (!app.IsNoDisplay)
                    {
                        appList.Add(new AppInfo(app.Label, app.ApplicationId));
                    }
                }
            }
            return appList;
        }
    }
}