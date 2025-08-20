namespace Workout.Services.Privilege
{
    /// <summary>
    /// Allows to check whether all permissions have been granted.
    /// </summary>
    public static class PrivilegeService
    {
        #region methods

        /// <summary>
        /// Returns true if all permissions have been granted, false otherwise.
        /// </summary>
        /// <returns>True if all permissions have been granted, false otherwise.</returns>
        public static bool AllPermissionsGranted()
        {
            return PrivilegeManager.Instance.AllPermissionsGranted();
        }

        #endregion
    }
}
