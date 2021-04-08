/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Tizen.Security;
using Calendar.Models;
using System.Collections;
using Calendar.Views;

namespace Calendar.Tizen.Port
{
    /// <summary>
    /// Represents the Security APIs for connecting Sample Calendar app.
    /// </summary>
    public class SecurityPort : ISecurityAPIs
    {
        /// <summary>
        /// Context for privacy privilege manager reponse
        /// </summary>
        static PrivacyPrivilegeManager.ResponseContext context = null;

        /// <summary>
        /// Call back count about request privacy privilege
        /// </summary>
        static int CBCount = 0;

        public event EventHandler<EventArgs> PrivilageAccepted;

        public void privilegeAccepted()
        {
            //MonthPage.
        }

        /// <summary>
        /// Used to check privilege.
        /// </summary>
        /// <returns></returns>
        public bool CheckPrivilege()
        {
            ArrayList PrivilegeList = new ArrayList();
            PrivilegeList.Add("http://tizen.org/privilege/calendar.read");
            PrivilegeList.Add("http://tizen.org/privilege/calendar.write");
            int privilageAcceptedCount = 0;
            // Check and request privacy privilege if app is needed
            foreach (string list in PrivilegeList)
            {
                PrivacyPrivilegeManager.GetResponseContext(list).TryGetTarget(out context);
                if (context != null)
                {
                    ++CBCount;
                    context.ResponseFetched += PPM_RequestResponse;
                }

                CheckResult result = PrivacyPrivilegeManager.CheckPermission(list);
                switch (result)
                {
                    case CheckResult.Allow:
                        /// Privilege can be used
                         privilageAcceptedCount++;
                        break;
                    case CheckResult.Deny:
                        /// Privilege can't be used
                        break;
                    case CheckResult.Ask:
                        /// Request permission to user
                        PrivacyPrivilegeManager.RequestPermission(list);
                        break;
                }
            }
            if (privilageAcceptedCount == PrivilegeList.Count)
                return true;
            return false;
        }

        /// <summary>
        /// PPM request response call back
        /// </summary>
        private void PPM_RequestResponse(object sender, RequestResponseEventArgs e)
        {
            if (e.cause == CallCause.Answer)
            {
                switch (e.result)
                {
                    case RequestResult.AllowForever:
                        Console.WriteLine("User allowed usage of privilege {0} definitely", e.privilege);
                        break;
                    case RequestResult.DenyForever:
                        // If privacy privilege is denied, app is terminated.
                        Console.WriteLine(" User denied usage of privilege {0} definitely", e.privilege);
                        System.Environment.Exit(1);
                        break;
                    case RequestResult.DenyOnce:
                        // If privacy privilege is denied, app is terminated.
                        Console.WriteLine("User denied usage of privilege {0} this time", e.privilege);
                        System.Environment.Exit(1);
                        break;
                };

                --CBCount;
                if (0 == CBCount)
                {
                    PrivilageAccepted.Invoke(null, null);
                    // Remove Callback
                    context.ResponseFetched -= PPM_RequestResponse;
                }
            }
            else
            {
                Console.WriteLine("Error occured during requesting permission for {0}", e.privilege);
            }
        }
    }
}

