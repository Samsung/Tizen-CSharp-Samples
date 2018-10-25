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
using Xamarin.Forms;

namespace MetalDetector.Models
{
    /// <summary>
    /// Class defining metal detector model which allow the application to use properties
    /// describing metal detector state.
    /// </summary>
    public class MetalDetectorModel
    {
        #region fields

        /// <summary>
        /// Magnetometer service which allows to obtain the magnetometer data.
        /// </summary>
        private IMagnetometerService _iMagnetometerService;

        /// <summary>
        /// Max signal strength value.
        /// </summary>
        private const int MAX_SIGNAL_STRENGTH = 5000;

        #endregion

        #region properties

        /// <summary>
        /// Notifies about changes in metal detector data.
        /// </summary>
        public event EventHandler Updated;

        /// <summary>
        /// Indicates if the magnetometer sensor detects something in its range.
        /// </summary>
        public bool IsInRange { get; private set; }

        /// <summary>
        /// Strength level value (in range from 0 to 9).
        /// </summary>
        public int RelativeSignalStrength { get; private set; }

        /// <summary>
        /// Strength level value.
        /// </summary>
        public int SignalStrength { get; private set; }

        /// <summary>
        /// Rotation value of the metal detector indicator.
        /// </summary>
        public double Rotation { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// MagnetometerModel class constructor.
        /// Initializes the model.
        /// </summary>
        public MetalDetectorModel()
        {
            _iMagnetometerService = DependencyService.Get<IMagnetometerService>();

            _iMagnetometerService.Updated += ServiceOnUpdated;
        }

        /// <summary>
        /// Starts metal detector.
        /// </summary>
        public void Start()
        {
            _iMagnetometerService.Start();
        }

        /// <summary>
        /// Stops metal detector.
        /// </summary>
        public void Stop()
        {
            _iMagnetometerService.Stop();
        }

        /// <summary>
        /// Returns true if the metal detector is supported, false otherwise.
        /// </summary>
        /// <returns>Flag indicating whether metal detector is supported or not.</returns>
        public bool IsSupported()
        {
            return _iMagnetometerService.IsSupported();
        }

        /// <summary>
        /// Handles "Updated" event of the magnetometer service.
        /// Updates public properties that can be used outside the class.
        /// Invokes "Updated" event.
        /// </summary>
        /// <param name="sender">Object firing the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ServiceOnUpdated(object sender, IMagnetometerDataUpdatedArgs e)
        {
            float x = e.X;
            float y = e.Y;
            float z = e.Z;

            SignalStrength = (int)CalculateSignalStrength(x, y, z);
            RelativeSignalStrength = CalculateRelativeSignalStrength(SignalStrength, 9);
            Rotation = CalculateRotation(x, y);
            IsInRange = (x <= -1 || x >= 1) || (y <= -1 || y >= 1);

            Updated?.Invoke(this, null);
        }

        /// <summary>
        /// Converts the number value expressed in radians to degrees.
        /// </summary>
        /// <param name="value">Value in radians.</param>
        /// <returns>Value in degrees.</returns>
        private double ToDegrees(double value)
        {
            return value * 180 / Math.PI;
        }

        /// <summary>
        /// Returns rotation value in degrees
        /// based on electromagnetic strength of x and y axis vectors.
        /// </summary>
        /// <param name="x">Value of vector x.</param>
        /// <param name="y">Value of vector y.</param>
        /// <returns>Rotation value in degrees.</returns>
        private double CalculateRotation(float x, float y)
        {
            return -ToDegrees(Math.Atan2(x, -y));
        }

        /// <summary>
        /// Returns resultant vector signal strength created from signal strength
        /// vectors for each axis.
        /// </summary>
        /// <param name="x">Value of vector x.</param>
        /// <param name="y">Value of vector y.</param>
        /// <param name="z">Value of vector z.</param>
        /// <returns>Resultant vector signal strength.</returns>
        private double CalculateSignalStrength(float x, float y, float z)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }

        /// <summary>
        /// Returns signal strength relative to maximal signal strength in
        /// interval[0, max]. Uses logarithmic scale.
        /// </summary>
        /// <param name="value">Signal strength.</param>
        /// <param name="max">Max value that can be returned.</param>
        /// <returns>Signal strength.</returns>
        private int CalculateRelativeSignalStrength(double value, int max)
        {
            return (int)Math.Ceiling(value > MAX_SIGNAL_STRENGTH ?
                max : Math.Log(value) / Math.Log(MAX_SIGNAL_STRENGTH) * max);
        }

        #endregion
    }
}
