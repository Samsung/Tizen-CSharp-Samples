/*
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

using Sensor.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sensor
{
    /// <summary>
    /// Interface to manage sensors.
    /// </summary>
    public interface ISensorManager
    {
        /// <summary>
        /// Gets the supported sensor types on the current device.
        /// </summary>
        /// <returns>Supported sensor types</returns>
        List<string> GetSensorTypeList();

        /// <summary>
        /// Gets the specific sensor information.
        /// </summary>
        /// <param name="type">Sensor type</param>
        /// <returns>Sensor information</returns>
        SensorInfo GetSensorInfo(string type);

        /// <summary>
        /// Starts the sensor of the specific type.
        /// </summary>
        /// <param name="type">Sensor type</param>
        /// <param name="listener">Event handler to listen sensor events</param>
        void StartSensor(string type, EventHandler<SensorEventArgs> listener);

        /// <summary>
        /// Stops the sensor of the specific type.
        /// </summary>
        /// <param name="type">Sensor type</param>
        /// <param name="listener">Event handler registered</param>
        void StopSensor(string type, EventHandler<SensorEventArgs> listener);
    }
}
