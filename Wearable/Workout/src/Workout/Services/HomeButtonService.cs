using ElmSharp;
using System;

namespace Workout.Services
{
    /// <summary>
    /// Allows the app to be notified about the home button action.
    /// </summary>
    public class HomeButtonService
    {
        #region fields

        /// <summary>
        /// An instance of EcoreEvent class.
        /// </summary>
        private readonly EcoreEvent<EcoreKeyEventArgs> _ecoreKeyDown = new EcoreEvent<EcoreKeyEventArgs>(EcoreEventType.KeyDown, EcoreKeyEventArgs.Create);

        #endregion

        #region properties

        /// <summary>
        /// Event emitted on home button key down.
        /// </summary>
        public event EventHandler HomeButtonKeyDown;

        #endregion

        #region methods

        /// <summary>
        /// Handles "On" event of the EcoreEvent object.
        /// Invokes HomeButtonKeyDown if the home button key is pressed (code 124).
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="keyEvent">Pressed key event data.</param>
        private void OnEcoreKeyDown(object sender, EcoreKeyEventArgs keyEvent)
        {
            if (keyEvent.KeyCode == 124)
            {
                HomeButtonKeyDown?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Initializes HomeButtonService class instance.
        /// </summary>
        public HomeButtonService()
        {
            _ecoreKeyDown.On += OnEcoreKeyDown;
        }

        #endregion
    }
}
