/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
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

using AudioIOSample.Tizen.Mobile;
using Tizen;
using Tizen.Security;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioIOSecurity))]
namespace AudioIOSample.Tizen.Mobile
{
    class AudioIOSecurity : IAudioIOSecurity
    {
        /// <summary>
        /// Check privilege.
        /// </summary>
        /// <param name="privilege">The string for privilege.</param>
        public void CheckPrivilege(string privilege)
        {
            CheckResult result = PrivacyPrivilegeManager.CheckPermission(privilege);
            switch (result)
            {
                case CheckResult.Allow:
                    Log.Info("AudioIOSample", $"Privilege \"{privilege}\" : allowed.");
                    /// Privilege can be used
                    break;
                case CheckResult.Deny:
                    Log.Info("AudioIOSample", $"Privilege \"{privilege}\" : denied.");
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
