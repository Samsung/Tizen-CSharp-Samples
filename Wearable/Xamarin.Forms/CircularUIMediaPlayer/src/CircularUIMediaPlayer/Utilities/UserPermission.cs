/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Threading.Tasks;
using Tizen;
using Tizen.Security;

namespace CircularUIMediaPlayer.Utilities
{
    /// <summary>
    /// UserPermission class
    /// To use functionality that requires privacy privilege,
    ///  - Check if we have a permission or not
    ///  - Request a permission if we don't have a permission
    /// </summary>
    public class UserPermission
    {
        private TaskCompletionSource<bool> tcs;

        public UserPermission()
        {
        }

        /// <summary>
        /// Check if a permission is granted and request a permission if it's not granted
        /// </summary>
        /// <param name="privilege">privilege name</param>
        /// <returns>bool</returns>
        public async Task<bool> CheckAndRequestPermission(string privilege)
        {
            try
            {
                // Gets the status of a privacy privilege permission.
                CheckResult result = PrivacyPrivilegeManager.CheckPermission(privilege);
                Log.Debug(Utility.LOG_TAG, "State: " + result.ToString());
                switch (result)
                {
                    case CheckResult.Allow:
                        // Permission has already been granted
                        // Privilege can be used
                        return true;
                    case CheckResult.Deny:
                        // Privilege can't be used
                        Log.Debug(Utility.LOG_TAG, "In this case, " + privilege + " privilege is not available until the user changes the permission state from the Settings application.");
                        return false;
                    case CheckResult.Ask:
                        // Permission is not granted so we need to request the permission
                        PrivacyPrivilegeManager.ResponseContext context;
                        PrivacyPrivilegeManager.GetResponseContext(privilege).TryGetTarget(out context);
                        if (context == null)
                        {
                            return false;
                        }

                        tcs = new TaskCompletionSource<bool>();
                        context.ResponseFetched += PPMResponseHandler;
                        // Request a permission
                        // Calling it brings up a system dialog that an app user can decide to approve or deny it.
                        PrivacyPrivilegeManager.RequestPermission(privilege);
                        return await tcs.Task;
                    default:
                        return false;
                }
            }
            catch (Exception e)
            {
                // Handle exception
                Log.Error(Utility.LOG_TAG, "An error occurred. : " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Invoked when the user responds to your app's permission request
        /// We can get the user response through it
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">RequestResponseEventArgs</param>
        public void PPMResponseHandler(object sender, RequestResponseEventArgs e)
        {
            if (e.cause == CallCause.Error)
            {
                /// Handle errors
                Log.Error(Utility.LOG_TAG, "An error occurred while requesting an user permission");
                tcs.SetResult(false);
                return;
            }

            // Now, we can check if the permission is granted or not
            switch (e.result)
            {
                case RequestResult.AllowForever:
                    // Permission is granted.
                    // We can do this permission-related task we want to do.
                    Log.Debug(Utility.LOG_TAG, "Response: RequestResult.AllowForever");
                    tcs.SetResult(true);
                    break;
                case RequestResult.DenyForever:
                case RequestResult.DenyOnce:
                    // Functionality that depends on this permission will not be available
                    Log.Debug(Utility.LOG_TAG, "Response: RequestResult." + e.result.ToString());
                    tcs.SetResult(false);
                    break;
            }
        }
    }
}
