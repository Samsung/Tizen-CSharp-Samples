﻿/*
* Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using Tizen.Sensor;

namespace Sensor.Tizen.Mobile.Sensors
{
    /// <summary>
    /// SleepMonitor Adapter class
    /// </summary>
    class SleepMonitorAdapter : ISensorAdapter
    {
        private SleepMonitor sensor = null;
        EventHandler<SleepMonitorDataUpdatedEventArgs> handler = null;

        /// <summary>
        /// Constructor of SleepMonitorAdapter.
        /// </summary>
        public SleepMonitorAdapter()
        {
            sensor = new SleepMonitor
            {
                Interval = 100,
                PausePolicy = SensorPausePolicy.None
            };
        }

        /// <summary>
        /// Destructor of SleepMonitorAdapter.
        /// </summary>
        ~SleepMonitorAdapter()
        {
            sensor.Dispose();
        }

        /// <summary>
        /// Gets sensor information.
        /// </summary>
        /// <returns>Sensor information.</returns>
        public SensorInfo GetSensorInfo() => new SensorInfo()
        {
            Type = SensorTypes.SleepMonitorType,
            Name = sensor.Name,
            Vendor = sensor.Vendor,
            MinRange = sensor.MinValue,
            MaxRange = sensor.MaxValue,
            Resolution = sensor.Resolution,
            MinInterval = sensor.MinInterval,
        };

        /// <summary>
        /// Starts sensor and registers listener to a sensor
        /// </summary>
        /// <param name="listener">Event handler to listen sensor events</param>
        public void Start(EventHandler<SensorEventArgs> listener)
        {
            handler = (sender, e) =>
            {
                listener?.Invoke(this,
                    new SensorEventArgs(new List<float>() { (int)e.SleepState }));
            };

            sensor.DataUpdated += handler;
            sensor.Start();
        }

        /// <summary>
        /// Stops sensor and unregisters the listener from a sensor
        /// </summary>
        /// <param name="listener">Event handler registered</param>
        public void Stop(EventHandler<SensorEventArgs> listener)
        {
            sensor.Stop();
            sensor.DataUpdated -= handler;
        }
    }
}
