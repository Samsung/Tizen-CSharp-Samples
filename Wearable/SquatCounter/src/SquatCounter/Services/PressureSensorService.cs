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
using Tizen.Sensor;

namespace SquatCounter.Services
{
    /// <summary>
    /// Provides pressure value reading functionality.
    /// </summary>
    public class PressureSensorService : IDisposable
    {
        private const string NotSupportedMsg = "Pressure sensor is not supported on your device.";

        private readonly PressureSensor _pressureSensor;

        public event EventHandler<float> ValueUpdated;

        private const int Interval = 10;

        /// <summary>
        /// Initializes PressureSensorService instance.
        /// </summary>
        public PressureSensorService()
        {
            if (PressureSensor.IsSupported == false)
            {
                throw new NotSupportedException(NotSupportedMsg);
            }

            _pressureSensor = new PressureSensor();
            _pressureSensor.DataUpdated += PressureSensorUpdated;
            _pressureSensor.Interval = Interval;
            _pressureSensor.Start();
        }

        /// <summary>
        /// Handles DataUpdated event callback.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void PressureSensorUpdated(object sender, PressureSensorDataUpdatedEventArgs e)
        {
            ValueUpdated?.Invoke(this, e.Pressure);
        }

        /// <summary>
        /// Implements IDisposable interface.
        /// </summary>
        public void Dispose()
        {
            _pressureSensor?.Stop();
            if (_pressureSensor != null)
                _pressureSensor.DataUpdated -= PressureSensorUpdated;
            _pressureSensor?.Dispose();
        }
    }
}
