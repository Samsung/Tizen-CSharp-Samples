/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Collections.Generic;
using System.Linq;

namespace SquatCounter.Services
{
    /// <summary>
    /// Provides squat counting functionality.
    /// </summary>
    public class SquatCounterService : IDisposable
    {
        private const float Accuracy = 0.030F;
        private const int WindowSize = 10;

        private PressureSensorService _pressureService;
        private Queue<float> _pressureWindow;

        private float _upperThreshold;
        private float _lowerThreshold;
        private bool _valueExceededUpperThreshold;
        private bool _isServiceCalibrated;

        public event EventHandler<int> SquatsUpdated;

        /// <summary>
        /// Indicates how many squats were made.
        /// </summary>
        public int SquatsCount { get; private set; }

        /// <summary>
        /// Initializes SquatCounterService class instance.
        /// </summary>
        public SquatCounterService()
        {
            _pressureWindow = new Queue<float>();

            _pressureService = new PressureSensorService();
            _pressureService.ValueUpdated += PressureSensorUpdated;
        }

        /// <summary>
        /// Starts counting service.
        /// </summary>
        public void Start()
        {
            _pressureService.ValueUpdated += PressureSensorUpdated;
        }

        /// <summary>
        /// Stops counting service.
        /// </summary>
        public void Stop()
        {
            _pressureService.ValueUpdated -= PressureSensorUpdated;
        }

        /// <summary>
        /// Resets squat count.
        /// </summary>
        public void Reset()
        {
            SquatsCount = 0;
            SquatsUpdated.Invoke(this, SquatsCount);
        }

        /// <summary>
        /// Implementation of IDisposable interface.
        /// </summary>
        public void Dispose()
        {
            if (_pressureService != null)
                _pressureService.ValueUpdated -= PressureSensorUpdated;
            _pressureService?.Dispose();
        }

        /// <summary>
        /// Calibrates service.
        /// Calculates treshold from single window.
        /// </summary>
        private void CalibrateService()
        {
            float average = CalculateStableAverage();
            _upperThreshold = average + Accuracy;
            _lowerThreshold = average - Accuracy;
            _isServiceCalibrated = true;
        }

        /// <summary>
        /// Calculates average from single window.
        /// </summary>
        /// <returns>Average value without maximum and minimum value.</returns>
        private float CalculateStableAverage()
        {
            float minPressure = _pressureWindow.Min();
            float maxPressure = _pressureWindow.Max();
            float sum = _pressureWindow.Sum();
            sum = (sum - minPressure - maxPressure) / (WindowSize - 2);

            return sum;
        }

        /// <summary>
        /// Handles execution of ValueUpdated event callback.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="pressure">Pressure value.</param>
        private void PressureSensorUpdated(object sender, float pressure)
        {
            _pressureWindow.Enqueue(pressure);

            if (_pressureWindow.Count < WindowSize)
            {
                return;
            }
            else if (_pressureWindow.Count == WindowSize && !_isServiceCalibrated)
            {
                CalibrateService();
            }

            AnalyzeNewWindow();

            _pressureWindow.Dequeue();
        }

        /// <summary>
        /// Checks if squat occurred.
        /// </summary>
        private void AnalyzeNewWindow()
        {
            float average = CalculateStableAverage();

            if (average <= _upperThreshold && average >= _lowerThreshold && _valueExceededUpperThreshold)
            {
                _valueExceededUpperThreshold = false;
                SquatsCount++;
                SquatsUpdated.Invoke(this, SquatsCount);
            }
            else if (average > _upperThreshold)
            {
                _valueExceededUpperThreshold = true;
            }
        }
    }
}