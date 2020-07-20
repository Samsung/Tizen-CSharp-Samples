using System;
using Tizen.Location;

namespace Workout.Services
{
    /// <summary>
    /// Provides access to Location API.
    /// This is singleton. Instance accessible via <see cref = Instance></cref> property.
    /// </summary>
    public sealed class LocationService
    {
        #region fields

        /// <summary>
        /// Backing field of Instance property.
        /// </summary>
        private static LocationService _instance;

        /// <summary>
        /// The time interval between callback updates. Should be in the range of 1~120 seconds.
        /// </summary>
        private const int _gpsCallbackInterval = 5;

        /// <summary>
        /// Locator instance.
        /// </summary>
        private readonly Locator _locator;

        #endregion

        #region properties

        /// <summary>
        /// LocationService instance accessor.
        /// </summary>
        public static LocationService Instance
        {
            get => _instance ?? (_instance = new LocationService());
        }

        /// <summary>
        /// Event emitted on location change.
        /// </summary>
        public event EventHandler<Location> LocationChanged;

        /// <summary>
        /// Event emitted on service state change.
        /// </summary>
        public event EventHandler<ServiceState> ServiceStateChanged;

        /// <summary>
        /// Event emitted on location setting change.
        /// </summary>
        public event EventHandler<bool> SettingChanged;

        #endregion

        #region methods

        /// <summary>
        /// Sets service listeners.
        /// </summary>
        private void AttachEvents()
        {
            _locator.ServiceStateChanged += (sender, args) => ServiceStateChanged?.Invoke(this, args.ServiceState);
            _locator.LocationChanged += (sender, args) => LocationChanged?.Invoke(this, args.Location);
            _locator.SettingChanged += (sender, args) => SettingChanged?.Invoke(this, args.IsEnabled);
        }

        /// <summary>
        /// Initializes LocationService class instance.
        /// </summary>
        private LocationService()
        {
            _locator = new Locator(LocationType.Hybrid)
            {
                Interval = _gpsCallbackInterval
            };

            AttachEvents();
        }

        /// <summary>
        /// Returns information about GPS location state.
        /// </summary>
        public bool IsGPSLocationEnabled()
        {
            return LocatorHelper.IsEnabledType(LocationType.Hybrid);
        }

        /// <summary>
        /// Starts Locator.
        /// </summary>
        public void Start()
        {
            _locator.Start();
        }

        /// <summary>
        /// Stops Locator.
        /// </summary>
        public void Stop()
        {
            _locator.Stop();
        }

        #endregion
    }
}
