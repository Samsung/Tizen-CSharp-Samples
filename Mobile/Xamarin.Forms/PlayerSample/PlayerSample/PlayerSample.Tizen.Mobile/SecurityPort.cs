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
using Tizen.Security;

namespace PlayerSample.Tizen.Mobile
{
    /// <summary>
    /// Represents the Security APIs for connecting Sample Calendar app.
    /// </summary>
    public class SecurityPort : IMediaPlayerSecurity
    {
        /// <summary>
        /// Used to check privilege.
        /// </summary>
        /// <param name="privilege">The string for privilege.</param>
        public void CheckPrivilege(string privilege)
        {
            CheckResult result = PrivacyPrivilegeManager.CheckPermission(privilege);
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
                    PrivacyPrivilegeManager.RequestPermission(privilege);
                    break;
            }
        }
    }
}

