using System.Collections.Generic;
using Workout.Models.Settings;

namespace Workout.Services.Settings
{
    /// <summary>
    /// Provides settings related data.
    /// This is singleton. Instance accessible via <see cref = Instance></cref> property.
    /// </summary>
    public class SettingsService
    {
        #region fields

        /// <summary>
        /// Dictionary of distance properties.
        /// </summary>
        private readonly Dictionary<DistanceUnit, Distance> _distanceDictionary;

        /// <summary>
        /// Backing field of Instance property.
        /// </summary>
        private static SettingsService _instance;

        #endregion

        #region properties

        /// <summary>
        /// SettingsService instance accessor.
        /// </summary>
        public static SettingsService Instance
        {
            get => _instance ?? (_instance = new SettingsService());
        }

        /// <summary>
        /// Current distance properties.
        /// </summary>
        public Distance Distance => _distanceDictionary[DistanceUnit.Km];

        /// <summary>
        /// Current age.
        /// </summary>
        public int Age { get; } = 40;

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public SettingsService()
        {
            _distanceDictionary = new Dictionary<DistanceUnit, Distance>
            {
                {DistanceUnit.Km, new Distance() { Unit = "km", UnitToKmRatio = 1 }},
                {DistanceUnit.Mile, new Distance() { Unit = "mi.", UnitToKmRatio = 1.609344 }}
            };
        }

        #endregion
    }
}
