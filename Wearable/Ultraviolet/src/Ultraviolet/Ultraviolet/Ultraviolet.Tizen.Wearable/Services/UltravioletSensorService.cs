/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

﻿using System;
using Tizen.Sensor;
using Ultraviolet.Enums;
using Ultraviolet.Interfaces;
using Ultraviolet.Tizen.Wearable.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(UltravioletSensorService))]
namespace Ultraviolet.Tizen.Wearable.Services
{
    /// <summary>
    /// Ultraviolet service class.
    /// </summary>
    public class UltravioletSensorService : IUltravioletSensorService
    {
        /// <summary>
        /// Raised whenever uv level changes.
        /// </summary>
        public event EventHandler<UvLevel> UvLevelUpdated;

        /// <summary>
        /// Ultraviolet sensor object.
        /// </summary>
        private readonly UltravioletSensor _ultravioletSensor;

        /// <summary>
        /// Initializes ultraviolet sensor service.
        /// </summary>
        public UltravioletSensorService()
        {
            _ultravioletSensor = new UltravioletSensor();
        }

        /// <summary>
        /// Starts receiving data from sensor.
        /// </summary>
        public void Start()
        {
            _ultravioletSensor.DataUpdated += OnDataUpdated;
            _ultravioletSensor.Start();
        }

        /// <summary>
        /// Stops receiving data from sensor.
        /// </summary>
        public void Stop()
        {
            _ultravioletSensor.DataUpdated -= OnDataUpdated;
            _ultravioletSensor.Stop();
            _ultravioletSensor.Dispose();
        }

        /// <summary>
        /// Handles DataUpdated event from ultraviolet sensor.
        /// </summary>
        /// <param name="sender">Object which invoked the event.</param>
        /// <param name="e">Ultraviolet sensor data.</param>
        private void OnDataUpdated(object sender, UltravioletSensorDataUpdatedEventArgs e)
        {
            UvLevel level = UvLevel.None;
            if (e.UltravioletIndex < 3.0)
            {
                level = UvLevel.Low;
            }
            else if (e.UltravioletIndex < 6.0)
            {
                level = UvLevel.Moderate;
            }
            else if (e.UltravioletIndex < 8.0)
            {
                level = UvLevel.High;
            }
            else if (e.UltravioletIndex < 11.0)
            {
                level = UvLevel.VeryHigh;
            }
            else
            {
                level = UvLevel.Extreme;
            }

            UvLevelUpdated?.Invoke(this, level);
        }
    }
}
