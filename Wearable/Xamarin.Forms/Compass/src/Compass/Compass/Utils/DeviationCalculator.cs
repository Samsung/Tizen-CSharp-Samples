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

namespace Compass.Utils
{
    /// <summary>
    /// Calculates average compass deviation.
    /// </summary>
    public class DeviationCalculator
    {
        #region fields

        /// <summary>
        /// Number of measurements used to calculate average.
        /// </summary>
        private const int MAX_NUMBER_OF_MEASUREMENTS = 50;

        /// <summary>
        /// Contains measurements to calculate average from.
        /// </summary>
        private float[] _measurements;

        /// <summary>
        /// Number of gathered measurements.
        /// </summary>
        private int _counter;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked when average compass deviation is calculated.
        /// </summary>
        public event EventHandler<float> DeviationCalculated;

        #endregion

        #region methods

        /// <summary>
        /// DeviationCalculator class constructor.
        /// Initializes the class.
        /// </summary>
        public DeviationCalculator()
        {
            _measurements = new float[50];
        }

        /// <summary>
        /// Checks whether all measurements have been gathered.
        /// If so, starts calculating deviation.
        /// </summary>
        private void CheckCounter()
        {
            if (_counter >= MAX_NUMBER_OF_MEASUREMENTS)
            {
                CalculateDeviation();
                _counter = 0;
            }
        }

        /// <summary>
        /// Calculates average compass deviation.
        /// Invokes "DeviationCalculated" event.
        /// </summary>
        private void CalculateDeviation()
        {
            float deviation = 0;

            for (int i = 0; i < _counter; i++)
            {
                deviation += _measurements[i];
            }

            DeviationCalculated?.Invoke(this, deviation / (_counter + 1));
        }

        /// <summary>
        /// Adds a new measurement to the list of measurements.
        /// </summary>
        /// <param name="newMeasurements">New compass deviation measurement.</param>
        public void Add(float newMeasurements)
        {
            _measurements[_counter] = newMeasurements;
            _counter++;
            CheckCounter();
        }

        #endregion
    }
}
