using System;
using Tizen.System;
using Workout.Services;
using Workout.Services.Privilege;

namespace Workout
{
    /// <summary>
    /// Main application class.
    /// </summary>
    public class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        #region fields

        /// <summary>
        /// Stores <see cref="PrivilegeManager" /> class instance.
        /// </summary>
        private readonly PrivilegeManager _privilegeManager = PrivilegeManager.Instance;

        /// <summary>
        /// Stores <see cref="HomeButtonService" /> class instance.
        /// </summary>
        private readonly HomeButtonService _homeButtonService = new HomeButtonService();

        /// <summary>
        /// Flag indicating that the app is in the background on the user demand.
        /// </summary>
        private bool _isAppInBackgroundOnUsersDemand;

        #endregion

        #region methods

        /// <summary>
        /// Handles "PrivilegesChecked" event.
        /// Loads Xamarin application.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="args">Event arguments. Not used.</param>
        private void OnPrivilegesChecked(object sender, EventArgs args)
        {
            _privilegeManager.PrivilegesChecked -= OnPrivilegesChecked;
            LoadApplication(new App());
        }

        /// <summary>
        /// Handles Display "StateChange" event.
        /// Sends launch application request to bring application to the front.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="displayState">Display state.</param>
        private void OnDisplayStateChange(object sender, DisplayStateChangedEventArgs displayState)
        {
            if (displayState.State != DisplayState.Off && !_isAppInBackgroundOnUsersDemand)
            {
                Tizen.Applications.AppControl.SendLaunchRequest(
                    new Tizen.Applications.AppControl
                    {
                        ApplicationId = Current.ApplicationInfo.ApplicationId
                    }
                );
            }
        }

        /// <summary>
        /// Handles "HomeButtonKeyDown" event.
        /// Sets flag indicating that the app is in the background on the user demand.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="args">Event arguments. Not used.</param>
        private void OnHomeButtonKeyDown(object sender, EventArgs args)
        {
            _isAppInBackgroundOnUsersDemand = true;
        }

        /// <summary>
        /// Application entry method.
        /// </summary>
        /// <param name="args">Launch arguments.</param>
        public static void Main(string[] args)
        {
            var app = new Program();

            Xamarin.Forms.Forms.SetFlags("IndicatorView_Experimental");
            Xamarin.Forms.Forms.Init(app);
            Tizen.Wearable.CircularUI.Forms.FormsCircularUI.Init();

            app.Run(args);
        }

        /// <summary>
        /// Handles creation phase of the forms application.
        /// Loads Xamarin application.
        /// </summary>
        protected override void OnCreate()
        {
            base.OnCreate();

            _privilegeManager.PrivilegesChecked += OnPrivilegesChecked;
            _privilegeManager.CheckAllPrivileges();

            _homeButtonService.HomeButtonKeyDown += OnHomeButtonKeyDown;

            Display.StateChanged += OnDisplayStateChange;
        }

        /// <summary>
        /// Handles termination phase of the forms application.
        /// Removes attached event listener.
        /// </summary>
        protected override void OnTerminate()
        {
            Display.StateChanged -= OnDisplayStateChange;

            _homeButtonService.HomeButtonKeyDown -= OnHomeButtonKeyDown;

            base.OnTerminate();
        }

        /// <summary>
        /// Handles resuming phase of the forms application.
        /// Resets flag indicating that the app is in the background on the user demand.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();

            _isAppInBackgroundOnUsersDemand = false;
        }

        #endregion
    }
}
