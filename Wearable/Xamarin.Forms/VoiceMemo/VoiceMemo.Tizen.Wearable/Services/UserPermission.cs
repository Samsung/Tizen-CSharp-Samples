/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
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
using System.Threading.Tasks;
using Tizen.Security;
using VoiceMemo.Services;
using VoiceMemo.Tizen.Wearable.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserPermission))]

namespace VoiceMemo.Tizen.Wearable.Services
{
    class UserPermission : IUserPermission
    {
        public UserPermission()
        {
        }

        TaskCompletionSource<bool> tcs;

        /// <summary>
        /// Request user permission for privacy privileges
        /// </summary>
        /// <param name="service">privacy privilege</param>
        /// <returns>true if user consent is gained</returns>
        public async Task<bool> GetPermission(string service)
        {
            try
            {
                // Gets the status of a privacy privilege permission.
                CheckResult result = PrivacyPrivilegeManager.CheckPermission(service);
                switch (result)
                {
                    case CheckResult.Allow:
                        // user consent for privacy privilege is already gained
                        return true;
                    case CheckResult.Deny:
                    case CheckResult.Ask:
                        // User permission request should be required
                        tcs = new TaskCompletionSource<bool>();
                        // Gets the response context for a given privilege.
                        var reponseContext = PrivacyPrivilegeManager.GetResponseContext(service);
                        PrivacyPrivilegeManager.ResponseContext context = null;
                        if (reponseContext.TryGetTarget(out context))
                        {
                            if (context != null)
                            {
                                context.ResponseFetched += Context_ResponseFetched;
                            }
                        }
                        // Try to get the permission for service from a user.
                        PrivacyPrivilegeManager.RequestPermission(service);

                        return await tcs.Task;
                    default:
                        return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[UserPermission] (" + service + ") Error :" + e.Message);
                return false;
            }
        }

        // Invoked when an app user responses for the permission request
        private void Context_ResponseFetched(object sender, RequestResponseEventArgs e)
        {
            if (e.result == RequestResult.AllowForever)
            {
                // User allows an app to grant privacy privilege
                tcs.SetResult(true);
            }
            else
            {
                // User doesn't allow an app to grant privacy privilege
                tcs.SetResult(false);
            }
        }
    }
}
