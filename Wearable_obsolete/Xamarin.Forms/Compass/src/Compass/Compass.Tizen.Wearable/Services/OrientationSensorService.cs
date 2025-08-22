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
using Compass.Models;
using Compass.Tizen.Wearable.Services;
using System;
using Tizen.Sensor;

[assembly: Xamarin.Forms.Dependency(typeof(OrientationSensorService))]

namespace Compass.Tizen.Wearable.Services
{
    /// <summary>
    /// Allows to obtain the orientation sensor's data.
    /// Implements IOrientationSensorService interface.
    /// </summary>
    public class OrientationSensorService : IOrientationSensorService
    {
        #region fields

        /// <summary>
        /// Reference to native API orientation sensor instance which allows to obtain sensor's data.
        /// </summary>
        private OrientationSensor _orientationSensor;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked whenever orientation sensor's data is updated.
        /// </summary>
        public event EventHandler<float> OrientationSensorDataUpdated;

        /// <summary>
        /// Event invoked if the orientation sensor is not supported.
        /// </summary>
        public event EventHandler NotSupported;

        #endregion

        #region methods

        /// <summary>
        /// Initializes the service.
        /// </summary>
        public void Init()
        {
            if (!OrientationSensor.IsSupported)
            {
                NotSupported?.Invoke(this, null);
                return;
            }

            _orientationSensor = new OrientationSensor();
            _orientationSensor.Interval = 10;
            _orientationSensor.DataUpdated += OnDataUpdated;
        }

        /// <summary>
        /// Starts the orientation sensor.
        /// </summary>
        public void Start()
        {
            _orientationSensor?.Start();
        }

        /// <summary>
        /// Handles "DataUpdated" event of the orientation sensor.
        /// Invokes "OrientationSensorDataUpdated" event.
        /// </summary>
        /// <param name="sender">Instance of the object which invokes the event.</param>
        /// <param name="eventArgs">Event data.</param>
        private void OnDataUpdated(object sender, OrientationSensorDataUpdatedEventArgs eventArgs)
        {
            OrientationSensorDataUpdated?.Invoke(this, eventArgs.Azimuth);
        }

        #endregion
    }
}
