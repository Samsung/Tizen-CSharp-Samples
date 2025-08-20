/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
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

namespace Pedometer.Privilege
{
    /// <summary>
    /// Class which stores list of application privileges and manages their status.
    /// This is singleton. Instance is accessible via <see cref="Instance" /> property.
    /// </summary>
    public sealed class PrivilegeManager
    {
        #region fields

        /// <summary>
        /// The healthinfo privilege key.
        /// </summary>
        private const string HEALTHINFO_PRIVILEGE = "http://tizen.org/privilege/healthinfo";

        /// <summary>
        /// List of privileges required by the application.
        /// </summary>
        private readonly List<PrivilegeItem> _privilegeItems = new List<PrivilegeItem>
            {
                new PrivilegeItem(HEALTHINFO_PRIVILEGE),
            };

        /// <summary>
        /// Backing field of the Instance property.
        /// </summary>
        private static PrivilegeManager _instance;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked when all privileges have been checked.
        /// </summary>
        public event EventHandler PrivilegesChecked;

        /// <summary>
        /// Privilege manager instance.
        /// </summary>
        public static PrivilegeManager Instance
        {
            get => _instance ?? (_instance = new PrivilegeManager());
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes PrivilegeManager instance.
        /// </summary>
        private PrivilegeManager()
        {
        }

        /// <summary>
        /// Checks whether all privileges have been checked or not.
        /// </summary>
        /// <returns>True if all privileges have been checked, false otherwise.</returns>
        private bool AllPermissionsChecked()
        {
            return _privilegeItems.All(item => item.Checked);
        }

        /// <summary>
        /// Checks whether all permissions have been granted or not.
        /// </summary>
        /// <returns>True if all permissions have been granted, false otherwise.</returns>
        public bool AllPermissionsGranted()
        {
            return _privilegeItems.All(item => item.Granted);
        }

        /// <summary>
        /// Checks whether all privileges have been checked or not.
        /// If so, invokes PrivilegesChecked event.
        /// </summary>
        private void AllPrivilegesChecked()
        {
            if (AllPermissionsChecked())
            {
                PrivilegesChecked?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Starts process of checking application privileges.
        /// </summary>
        public void CheckAllPrivileges()
        {
            foreach (var item in _privilegeItems)
            {
                PrivilegeCheck(item.Privilege);
            }
        }

        /// <summary>
        /// Handles privilege request response.
        /// </summary>
        /// <param name="sender">Event sender. Not used.</param>
        /// <param name="e">Event arguments.</param>
        private void PPM_RequestResponse(object sender, RequestResponseEventArgs e)
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
                };
            }

            AllPrivilegesChecked();
        }

        /// <summary>
        /// Checks a selected privilege. Requests a privilege if not set.
        /// </summary>
        /// <param name="privilege">The privilege to check.</param>
        private void PrivilegeCheck(string privilege)
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
                    PrivacyPrivilegeManager.GetResponseContext(privilege)
                        .TryGetTarget(out PrivacyPrivilegeManager.ResponseContext context);

                    if (context != null)
                    {
                        context.ResponseFetched += PPM_RequestResponse;
                    }

                    PrivacyPrivilegeManager.RequestPermission(privilege);
                    PrivacyPrivilegeManager.GetResponseContext(privilege).TryGetTarget(out context);

                    break;
            }

            AllPrivilegesChecked();
        }

        /// <summary>
        /// Sets a privilege flag to a given value.
        /// </summary>
        /// <param name="privilege">The privilege to modify.</param>
        /// <param name="value">Indicates whether the permission has been granted.</param>
        private void SetPermission(string privilege, bool value)
        {
            _privilegeItems.FirstOrDefault(p => p.Privilege == privilege).GrantPermission(value);
        }

        #endregion
    }
}
