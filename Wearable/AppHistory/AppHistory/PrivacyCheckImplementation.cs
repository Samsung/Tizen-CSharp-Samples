// Copyright 2018 Samsung Electronics Co., Ltd
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Tizen.Security;

namespace AppHistory
{

    /// <summary>
    /// Implementation class of IPrivacyCheck interface
    /// </summary>
    public class PrivacyCheckImplementation : IPrivacyCheck
    {
        private readonly ILog log;

        public PrivacyCheckImplementation()
        {
            log = new LogImplementation();
        }

        /// <summary>
        /// Check permission about http://tizen.org/privilege/apphistory.read privilege
        /// </summary>
        public void CheckPermission()
        {
            try
            {
                CheckResult result = PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/apphistory.read");

                switch (result)
                {
                    case CheckResult.Allow:
                        break;
                    case CheckResult.Deny:
                        break;
                    case CheckResult.Ask:
                        PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/apphistory.read");
                        break;
                }
            }
            catch (Exception e)
            {
                log.Log(e.Message.ToString());
            }
        }
    }
}
