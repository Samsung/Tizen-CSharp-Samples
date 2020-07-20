namespace Workout.Services.Privilege
{
    /// <summary>
    /// Class describing a privilege status.
    /// </summary>
    public class PrivilegeItem
    {
        #region properties

        /// <summary>
        /// The privilege key.
        /// </summary>
        public string Privilege { get; }

        /// <summary>
        /// Flag indicating whether the permission is granted or not.
        /// </summary>
        public bool Granted { get; private set; }

        /// <summary>
        /// Flag indicating whether the permission has been checked or not.
        /// </summary>
        public bool Checked { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        /// <param name="privilege">Privilege key.</param>
        public PrivilegeItem(string privilege)
        {
            Privilege = privilege;
        }

        /// <summary>
        /// Sets the flag indicating whether permission has been granted or not.
        /// </summary>
        /// <param name="value">True if permission has been granted, false otherwise.</param>
        public void GrantPermission(bool value)
        {
            Granted = value;
            Checked = true;
        }

        #endregion
    }
}
