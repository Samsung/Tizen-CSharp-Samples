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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tizen.Applications;
using Badges.Interfaces;
using Badges.Models;
using Badges.Tizen.Mobile;

[assembly: Xamarin.Forms.Dependency(typeof(AppList))]

namespace Badges.Tizen.Mobile
{
    /// <summary>
    /// Class to obtain application list from the Tizen OS and modify badge counter.
    /// </summary>
    internal class AppList : IAppListService
    {
        #region methods

        /// <summary>
        /// Returns information about all available applications supplemented with badge counter data and flag indicates
        /// whether app was installed by current user or not.
        /// </summary>
        /// <param name="applicationListCb">Delegate to call on every application retrieval.</param>
        public async void GetAppList(AddAppToListDelegate applicationListCb)
        {
            var myCert = PackageManager.GetPackage(Application.Current.ApplicationInfo.PackageId)?
                .Certificates[CertificateType.Author].Signer;
            IEnumerable<ApplicationInfo> installedApplications =
                await ApplicationManager.GetInstalledApplicationsAsync();

            foreach (ApplicationInfo tizenAppInfo in installedApplications)
            {
                if (tizenAppInfo.IsNoDisplay)
                {
                    continue;
                }

                Package pkg;
                try
                {
                    pkg = PackageManager.GetPackage(tizenAppInfo.PackageId);
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error getting application {tizenAppInfo.ApplicationId} information: {e.Message}");
                    continue;
                }

                var appSignerCert = pkg?.Certificates[CertificateType.Author].Signer;
                if (appSignerCert == null)
                {
                    continue;
                }

                bool isEnabled = appSignerCert.Equals(myCert);
                var appInfo = new AppInfo(
                    appName: tizenAppInfo.Label,
                    appId: tizenAppInfo.ApplicationId,
                    isAvailable: isEnabled,
                    badgeCounter: 0);
                try
                {
                    Badge badge = BadgeControl.Find(appInfo.AppId);
                    if (badge != null)
                    {
                        appInfo.BadgeCounter = Convert.ToDouble(badge.Count);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Error getting badge for {appInfo.AppId}: {e.Message}");
                }

                applicationListCb(appInfo);
            }
        }

        /// <summary>
        /// Set badge counter for application with given id.
        /// </summary>
        /// <param name="appId">ID of the application.</param>
        /// <param name="badgeCount">New counter value.</param>
        public void SetBadge(string appId, int badgeCount)
        {
            try
            {
                BadgeControl.Add(new Badge(appId, badgeCount, true));
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error setting badge for {appId}: {e.Message}");
            }
        }

        #endregion methods
    }
}
