/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Linq;
using Tizen.Security;

namespace FindPlace.Tizen.Wearable.Services.Privilege
{
    /// <summary>
    /// Class stores list of app privileges and manages their status.
    /// </summary>
    public static class PrivilegeManager
    {
        #region fields

        /// <summary>
        /// The localization privilege key.
        /// </summary>
        private const string LocationPrivilege = "http://tizen.org/privilege/location";

        /// <summary>
        /// List of privileges required by the application.
        /// </summary>
        private static readonly List<PrivilegeItem> _privilegeItems = new List<PrivilegeItem>()
        {
            new PrivilegeItem(LocationPrivilege)
        };

        #endregion

        #region property

        /// <summary>
        /// Event invoked when all privileges have been checked.
        /// </summary>
        public static event EventHandler PrivilegesChecked;

        #endregion

        #region methods

        /// <summary>
        /// Starts process of checking application privileges.
        /// </summary>
        public static void CheckAllPrivileges()
        {
            _privilegeItems.ForEach(p => CheckPrivilege(p.Privilege));
        }

        /// <summary>
        /// Checks whether all privileges have been granted or not.
        /// </summary>
        /// <returns>True if all permissions have been granted, false otherwise.</returns>
        public static bool AllPermissionsGranted()
        {
            return _privilegeItems.All(p => p.Granted);
        }

        /// <summary>
        /// Checks whether all privileges have been checked or not.
        /// If so, invokes PrivilegesChecked event.
        /// </summary>
        public static void AllPrivilegesChecked()
        {
            bool areAllPermissionsChecked = _privilegeItems.All(p => p.Checked);

            if (areAllPermissionsChecked)
            {
                PrivilegesChecked?.Invoke(null, null);
            }
        }

        /// <summary>
        /// Checks a selected privilege. Requests a privilege if not set.
        /// </summary>
        /// <param name="privilege">The privilege to check.</param>
        private static void CheckPrivilege(string privilege)
        {
            switch (PrivacyPrivilegeManager.CheckPermission(privilege))
            {
                case CheckResult.Allow:
                    SetPermission(privilege, true);
                    break;
                case CheckResult.Deny:
                    SetPermission(privilege, false);
                    break;
                case CheckResult.Ask:
                    PrivacyPrivilegeManager.RequestPermission(privilege);
                    PrivacyPrivilegeManager.GetResponseContext(privilege)
                        .TryGetTarget(out PrivacyPrivilegeManager.ResponseContext context);

                    if (context != null)
                    {
                        context.ResponseFetched += PrivilegeRequestResponse;
                    }

                    break;
            }

            AllPrivilegesChecked();
        }

        /// <summary>
        /// Handles privilege request response.
        /// </summary>
        /// <param name="sender">Event sender. Not used.</param>
        /// <param name="e">Event arguments.</param>
        private static void PrivilegeRequestResponse(object sender, RequestResponseEventArgs e)
        {
            if (e.cause == CallCause.Answer)
            {
                switch (e.result)
                {
                    case RequestResult.AllowForever:
                        SetPermission(e.privilege, true);
                        break;
                    case RequestResult.DenyForever:
                    case RequestResult.DenyOnce:
                        SetPermission(e.privilege, false);
                        break;
                }
            }

            AllPrivilegesChecked();
        }

        /// <summary>
        /// Sets a privilege flag to a given value.
        /// </summary>
        /// <param name="privilege">The privilege to modify.</param>
        /// <param name="value">Indicates whether the permission has been granted.</param>
        private static void SetPermission(string privilege, bool value)
        {
            _privilegeItems.FirstOrDefault(p => p.Privilege == privilege).GrantPermission(value);
        }

        #endregion
    }
}
