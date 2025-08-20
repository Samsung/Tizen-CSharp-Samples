/*
* Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using Sensor.Tizen.Mobile;
using System;
using Tizen.Security;

namespace Sensor.Tizen.Mobile
{
    /// <summary>
    /// Check the privacy privilege permission
    /// </summary>
    public static class PrivacyChecker
    {
        /// <summary>
        /// Check whether the application has the specific permission or not.
        /// </summary>
        /// <param name="privilege">Privilege</param>
        public static void CheckPermission(string privilege)
        {
            try
            {
                CheckResult result = PrivacyPrivilegeManager.CheckPermission(privilege);

                switch (result)
                {
                    case CheckResult.Allow:
                        break;
                    case CheckResult.Deny:
                        break;
                    case CheckResult.Ask:
                        // Request permission to user
                        PrivacyPrivilegeManager.RequestPermission(privilege);
                        break;
                }
            }
            catch (Exception e)
            {
                global::Tizen.Log.Error(Log.LogTag, e.Message);
            }
        }
    }
}

