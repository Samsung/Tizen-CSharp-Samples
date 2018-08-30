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
using Xamarin.Forms;

namespace WeatherWatch.Services
{
    /// <summary>
    /// UserPermission class
    /// It provides a way to get an user consent for privacy privilege
    /// </summary>
    public class UserPermission
    {
        TaskCompletionSource<bool> tcs;
        int GrantedPermission;

        /// <summary>
        /// Request user permission for privacy privileges
        /// </summary>
        /// <param name="service">privacy privilege</param>
        /// <returns>true if user consent is gained</returns>
        public async Task<bool> GetPermission(string service)
        {
            try
            {
                GrantedPermission = -1;
                // Gets the status of a privacy privilege permission.
                CheckResult result = PrivacyPrivilegeManager.CheckPermission(service);
                switch (result)
                {
                    case CheckResult.Allow:
                        // user consent for privacy privilege is already gained
                        Console.WriteLine("\n\n\nuser consent for privacy privilege is already gained\n\n");
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

                        // Check if permission is granted or not every second
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Device.StartTimer(new TimeSpan(0, 0, 0, 1, 0), CheckPermission);
                        });

                        return await tcs.Task;
                    default:
                        return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[UserPermission] (" + service + ") Error :" + e.Message + ", " + e.StackTrace + ", " + e.InnerException);
                return false;
            }
        }

        bool CheckPermission()
        {
            // In case that an app user doesn't give permission yet.
            if (GrantedPermission == -1)
            {
                return true;
            }

            // In case that an app user gives permission.
            // Denied
            if (GrantedPermission == 0)
            {
                tcs.SetResult(false);
            }
            // Allowed
            else
            {
                tcs.SetResult(true);
            }

            return false;
        }

        // Invoked when an app user responses for the permission request
        private void Context_ResponseFetched(object sender, RequestResponseEventArgs e)
        {
            if (e.result == RequestResult.AllowForever)
            {
                // User allows an app to grant privacy privilege
                GrantedPermission = 1;
            }
            else
            {
                // User doesn't allow an app to grant privacy privilege
                GrantedPermission = 0;
            }
        }
    }
}