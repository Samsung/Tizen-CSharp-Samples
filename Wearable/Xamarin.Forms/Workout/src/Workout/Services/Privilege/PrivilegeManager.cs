using System;
using System.Collections.Generic;
using System.Linq;
using Tizen.Security;

namespace Workout.Services.Privilege
{
    /// <summary>
    /// Class which stores list of application privileges and manages their status.
    /// This is singleton. Instance is accessible via <see cref="Instance" /> property.
    /// </summary>
    public sealed class PrivilegeManager
    {
        #region fields

        /// <summary>
        /// Privilege for using Heart Rate Monitor.
        /// </summary>
        private const string _healthinfoPrivilege = "http://tizen.org/privilege/healthinfo";

        /// <summary>
        /// Privilege for using Location.
        /// </summary>
        private const string _locationPrivilege = "http://tizen.org/privilege/location";

        /// <summary>
        /// List of privileges required by the application.
        /// </summary>
        private readonly List<PrivilegeItem> _privilegeItems = new List<PrivilegeItem>
        {
            new PrivilegeItem(_healthinfoPrivilege),
            new PrivilegeItem(_locationPrivilege)
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
        /// Checks whether all privileges have been checked or not.
        /// </summary>
        /// <returns>True if all privileges have been checked, false otherwise.</returns>
        private bool AllPermissionsChecked()
        {
            return _privilegeItems.All(x => x.Checked);
        }

        /// <summary>
        /// Checks whether all privileges have been checked or not.
        /// If so, invokes PrivilegesChecked event.
        /// </summary>
        private void AllPrivilegesChecked()
        {
            if (AllPermissionsChecked())
            {
                PrivilegesChecked?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles privilege request response.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="requestResponse">Request response data.</param>
        private void PPM_RequestResponse(object sender, RequestResponseEventArgs requestResponse)
        {
            if (requestResponse.cause == CallCause.Answer)
            {
                switch (requestResponse.result)
                {
                    case RequestResult.AllowForever:
                        SetPermission(requestResponse.privilege, true);
                        break;
                    case RequestResult.DenyForever:
                    case RequestResult.DenyOnce:
                        SetPermission(requestResponse.privilege, false);
                        break;
                }
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
            _privilegeItems.Find(p => p.Privilege == privilege)?.GrantPermission(value);
        }

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        private PrivilegeManager()
        {
        }

        /// <summary>
        /// Checks whether all permissions have been granted or not.
        /// </summary>
        /// <returns>True if all permissions have been granted, false otherwise.</returns>
        public bool AllPermissionsGranted()
        {
            return _privilegeItems.All(x => x.Granted);
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

        #endregion
    }
}
