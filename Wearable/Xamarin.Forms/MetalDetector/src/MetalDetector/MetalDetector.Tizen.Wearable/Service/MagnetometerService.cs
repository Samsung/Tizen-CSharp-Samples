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
using System;
using MetalDetector.Models;
using MetalDetector.Tizen.Wearable.Service;
using Tizen.Sensor;

[assembly: Xamarin.Forms.Dependency(typeof(MagnetometerService))]
namespace MetalDetector.Tizen.Wearable.Service
{
    /// <summary>
    /// Magnetometer service which allows to obtain the magnetometer data.
    /// </summary>
    class MagnetometerService : IMagnetometerService
    {
        #region fields

        /// <summary>
        /// Magnetometer class instance used for registering callbacks for the magnetometer
        /// and getting the magnetometer data.
        /// </summary>
        private Magnetometer _magnetometer;

        #endregion

        #region properties

        /// <summary>
        /// Notifies about magnetometer data update.
        /// </summary>
        public event EventHandler<IMagnetometerDataUpdatedArgs> Updated;

        #endregion

        #region methods

        /// <summary>
        /// MagnetometerService class constructor.
        /// Creates instance of internal Magnetometer class.
        /// </summary>
        public MagnetometerService()
        {
            _magnetometer = new Magnetometer();
        }

        /// <summary>
        /// Starts the magnetometer sensor.
        /// </summary>
        public void Start()
        {
            if (!IsSupported())
            {
                return;
            }

            if (_magnetometer.IsSensing)
            {
                return;
            }

            _magnetometer.DataUpdated += OnDataUpdated;
            _magnetometer.Start();
        }

        /// <summary>
        /// Stops the magnetometer sensor.
        /// </summary>
        public void Stop()
        {
            if (!IsSupported())
            {
                return;
            }

            if (_magnetometer.IsSensing)
            {
                _magnetometer.DataUpdated -= OnDataUpdated;
                _magnetometer.Stop();
            }
        }

        /// <summary>
        /// Returns true if the magnetometer is supported, false otherwise.
        /// </summary>
        /// <returns>Flag indicating whether magnetometer is supported or not.</returns>
        public bool IsSupported()
        {
            return Magnetometer.IsSupported;
        }

        /// <summary>
        /// Handles "DataUpdated" event of the Magnetometer object.
        /// Invokes "Updated" event.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnDataUpdated(object sender, MagnetometerDataUpdatedEventArgs e)
        {
            Updated?.Invoke(this, new MagnetometerDataUpdatedArgs(e.X, e.Y, e.Z));
        }

        #endregion
    }
}
