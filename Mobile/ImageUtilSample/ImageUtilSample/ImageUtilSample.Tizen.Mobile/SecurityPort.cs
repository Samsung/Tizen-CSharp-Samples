using System;
using Tizen.Security;

namespace ImageUtilSample.Tizen.Mobile
{
    public class SecurityPort : IImageUtilSecurity
    {
        /// <summary>
        /// Used to check privilege.
        /// </summary>
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
