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
using System.Collections;
using System.Collections.Generic;
using Tizen.Security;
using Contacts.Models;

namespace Contacts.Tizen.Port
{
    /// <summary>
    /// Represents the Security APIs for connecting Sample Contact app.
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

        /// <summary>
        /// Check privacy privilege and if need to ask for user, send request for PPM.
        /// </summary>
        public void CheckPrivilege()
        {
            // Make array list for requesting privacy privilege
            // Contacts need 2 privilege, contact read and account write.
            ArrayList PrivilegeList = new ArrayList();
            PrivilegeList.Add("http://tizen.org/privilege/contact.read");
            PrivilegeList.Add("http://tizen.org/privilege/contact.write");

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
        }

        /// <summary>
        /// PPM request response call back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void PPM_RequestResponse(object sender, RequestResponseEventArgs e)
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

