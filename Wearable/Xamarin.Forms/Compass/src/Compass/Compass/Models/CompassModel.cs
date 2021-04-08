/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Compass.Utils;
using System;
using Xamarin.Forms;

namespace Compass.Models
{
    /// <summary>
    /// Provides methods to obtain the compass data.
    /// </summary>
    public class CompassModel
    {
        #region fields

        /// <summary>
        /// Calculates compass deviation.
        /// </summary>
        private DeviationCalculator _deviationCalculator;

        /// <summary>
        /// Stores platform specific service class instance obtained with dependency injection.
        /// Allows to obtain orientation sensor's data.
        /// </summary>
        private IOrientationSensorService _orientationSensorService;

        /// <summary>
        /// Event handler for the compass data updated event.
        /// </summary>
        /// <param name="sender">Instance of the object which invoked event.</param>
        /// <param name="compassDeviation">Compass deviation.</param>
        /// <param name="compassDirection">Compass direction.</param>
        public delegate void CompassDataUpdatedEventHandler(object sender, float compassDeviation,
            CompassDirections compassDirection);

        #endregion

        #region properties

        /// <summary>
        /// Event invoked whenever compass state is updated.
        /// </summary>
        public event CompassDataUpdatedEventHandler CompassDataUpdated;

        /// <summary>
        /// Event invoked if the compass is not supported.
        /// </summary>
        public event EventHandler NotSupported;

        #endregion

        #region methods

        /// <summary>
        /// CompassModel class constructor.
        /// Initializes the model.
        /// </summary>
        public CompassModel()
        {
            InitDeviationCalculator();
            InitOrientationSensor();
        }

        /// <summary>
        /// Handles "OrientationSensorDataUpdated" event of the IOrientationSensorService object.
        /// Adds a new measurement to deviation calculator.
        /// </summary>
        /// <param name="sender">Instance of the object which invoked the event.</param>
        /// <param name="azimuth">The current azimuth.</param>
        private void OnCompassDataUpdated(object sender, float azimuth)
        {
            _deviationCalculator.Add(azimuth);
        }

        /// <summary>
        /// Handles "OrientationSensorDataUpdated" event of the sensor service.
        /// Invokes "CompassDataUpdated" event.
        /// </summary>
        /// <param name="sender">Instance of the object which invoked the event.</param>
        /// <param name="azimuth">Calculated deviation.</param>
        private void OnDeviationCalculated(object sender, float azimuth)
        {
            CompassDataUpdated?.Invoke(this, azimuth, DirectionIndicator.GetCompassDirection(azimuth));
        }

        /// <summary>
        /// Initializes deviation calculator.
        /// </summary>
        private void InitDeviationCalculator()
        {
            _deviationCalculator = new DeviationCalculator();
            _deviationCalculator.DeviationCalculated += OnDeviationCalculated;
        }

        /// <summary>
        /// Initializes orientation sensor.
        /// </summary>
        private void InitOrientationSensor()
        {
            _orientationSensorService = DependencyService.Get<IOrientationSensorService>();
            _orientationSensorService.OrientationSensorDataUpdated += OnCompassDataUpdated;
            _orientationSensorService.NotSupported += OnNotSupported;
            _orientationSensorService.Init();
        }

        /// <summary>
        /// Handles "NotSupported" event of the sensor service.
        /// Invokes "NotSupported" event.
        /// </summary>
        /// <param name="sender">Instance of the object which invoked the event.</param>
        /// <param name="eventArgs">Event data.</param>
        private void OnNotSupported(object sender, EventArgs eventArgs)
        {
            NotSupported?.Invoke(this, null);
        }

        /// <summary>
        /// Starts the compass.
        /// </summary>
        public void Start()
        {
            _orientationSensorService.Start();
        }

        #endregion
    }
}
