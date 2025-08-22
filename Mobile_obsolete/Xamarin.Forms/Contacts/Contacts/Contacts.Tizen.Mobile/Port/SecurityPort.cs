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

using System.Collections.Generic;
using Tizen.Security;
using Contacts.Models;
using System.Threading.Tasks;

namespace Contacts.Tizen.Port
{
    /// <summary>
    /// Represents the Security APIs for connecting Sample Contact app.
    /// </summary>
    public class SecurityPort : ISecurityAPIs
    {
        /// <summary>
        /// Check privacy privilege and if need to ask for user, send request for PPM.
        /// </summary>
        public async Task<bool> CheckPrivilege()
        {
            // Make array list for requesting privacy privilege
            // Contacts need 2 privilege, contact read and account write.
            var privileges = new List<string>
            {
                "http://tizen.org/privilege/contact.read",
                "http://tizen.org/privilege/contact.write"
            };
            // Check and request privacy privilege if app is needed
            foreach (string privilege in privileges)
            {
                CheckResult result = PrivacyPrivilegeManager.CheckPermission(privilege);
                switch (result)
                {
                    case CheckResult.Allow:
                        break;
                    case CheckResult.Deny:
                        return false;
                    case CheckResult.Ask:
                        /// Request permission to user
                        if (PrivacyPrivilegeManager.GetResponseContext(privilege).TryGetTarget(out PrivacyPrivilegeManager.ResponseContext context))
                        {
                            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
                            context.ResponseFetched += (s, e) =>
                            {
                                tcs.SetResult(e.result == RequestResult.AllowForever);
                            };
                            PrivacyPrivilegeManager.RequestPermission(privilege);
                            if (!(await tcs.Task))
                            {
                                return false;
                            }
                        }
                        break;
                }
            }

            return true;
        }
    }
}

