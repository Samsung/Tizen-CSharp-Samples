using System;
using Tizen.Location;
using Workout.Services;
using Workout.Services.Settings;

namespace Workout.Models
{
    /// <summary>
    /// Provides Location related data.
    /// </summary>
    public class LocationModel
    {
        #region fields

        /// <summary>
        /// Latest received location.
        /// </summary>
        private Location _lastLocation;

        /// <summary>
        /// Current location data.
        /// </summary>
        private LocationData _locationData;

        /// <summary>
        /// Instance of LocationService.
        /// </summary>
        private readonly LocationService _locationService;

        /// <summary>
        /// Flag inditacing whether the distance can be calculated.
        /// </summary>
        private bool _canCalculateDistance;

        /// <summary>
        /// Flag inditacing whether the distance calculation is locked.
        /// </summary>
        private bool _isDistanceCalculationLocked;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked on data update.
        /// </summary>
        public event EventHandler<LocationUpdatedEventArgs> Updated;

        #endregion

        #region methods

        /// <summary>
        /// Handles "LocationChanged" event of LocationService.
        /// Adds distance between location provided as event argument and previous location.
        /// Triggers update event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="location">New location.</param>
        private void OnLocationChanged(object sender, Location location)
        {
            if (_canCalculateDistance && !_isDistanceCalculationLocked)
            {
                _locationData.Distance += location.GetDistanceTo(_lastLocation) / SettingsService.Instance.Distance.UnitToKmRatio;
            }
            else
            {
                _canCalculateDistance = true;
            }

            _lastLocation = location;

            EmitUpdate();
        }

        /// <summary>
        /// Handles "ServiceStateChanged" event of LocationService.
        /// Updates stored state.
        /// Triggers update event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="serviceState">New service state.</param>
        private void OnServiceStateChanged(object sender, ServiceState serviceState)
        {
            _locationData.IsGPSEnabled = serviceState == ServiceState.Enabled;

            EmitUpdate();
        }

        /// <summary>
        /// Sets starting data values.
        /// </summary>
        private void SetInitialData()
        {
            _locationData = new LocationData
            {
                Distance = 0,
                IsGPSEnabled = false
            };
        }

        /// <summary>
        /// Emits event with current location data.
        /// </summary>
        private void EmitUpdate()
        {
            Updated?.Invoke(this, new LocationUpdatedEventArgs(_locationData));
        }

        /// <summary>
        /// Initializes class instance.
        /// Sets initial LocationData.
        /// </summary>
        public LocationModel()
        {
            _locationService = LocationService.Instance;

            SetInitialData();

            _locationService.LocationChanged += OnLocationChanged;
            _locationService.ServiceStateChanged += OnServiceStateChanged;
        }

        /// <summary>
        /// Stops locator.
        /// </summary>
        public void Stop()
        {
            _locationService.Stop();
        }

        /// <summary>
        /// Resets model data.
        /// </summary>
        public void Reset()
        {
            _canCalculateDistance = false;
            _locationData.Distance = 0;

            UnlockMeasurement();
        }

        /// <summary>
        /// Unlocks distance measurement.
        /// </summary>
        public void UnlockMeasurement()
        {
            _isDistanceCalculationLocked = false;
        }

        /// <summary>
        /// Locks distance measurement.
        /// </summary>
        public void LockMeasurement()
        {
            _isDistanceCalculationLocked = true;
        }

        #endregion
    }
}
