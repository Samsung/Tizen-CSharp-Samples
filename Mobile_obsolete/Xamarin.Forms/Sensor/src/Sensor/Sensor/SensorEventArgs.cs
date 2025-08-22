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

using System;
using System.Collections.Generic;
using System.Text;

namespace Sensor
{
    /// <summary>
    /// The sensor changed event arguments class is used for storing the data returned by a sensor.
    /// </summary>
    public class SensorEventArgs
    {
        /// <summary>
        /// The pedometer state.
        /// </summary>
        public enum PedometerState
        {
            /// <summary>
            /// Unknown.
            /// </summary>
            Unknown = -1,

            /// <summary>
            /// Stop state.
            /// </summary>
            Stop,

            /// <summary>
            /// Walking state.
            /// </summary>
            Walk,

            /// <summary>
            /// Running state.
            /// </summary>
            Run
        }

        /// <summary>
        /// The sleep monitor state.
        /// </summary>
        public enum SleepMonitorState
        {
            /// <summary>
            /// Unknown.
            /// </summary>
            Unknown = -1,

            /// <summary>
            /// The wake state.
            /// </summary>
            Wake,

            /// <summary>
            /// The sleeping state.
            /// </summary>
            Sleep
        }

        /// <summary>
        /// The proximity sensor state.
        /// </summary>
        public enum ProximityState
        {
            /// <summary>
            /// Unknown.
            /// </summary>
            Unknown = -1,

            /// <summary>
            /// Near sate.
            /// </summary>
            Near = 0,

            /// <summary>
            /// Far state.
            /// </summary>
            Far = 5
        }

        /// <summary>
        /// The detector sensor state.
        /// </summary>
        public enum DetectorState
        {
            /// <summary>
            /// Unknown.
            /// </summary>
            Unknown = -1,

            /// <summary>
            /// None sate.
            /// </summary>
            NotDetected = 0,

            /// <summary>
            /// Detected state.
            /// </summary>
            Detected = 1
        }

        /// <summary>
        /// Constructor of SensorEventArgs.
        /// </summary>
        /// <param name="values">Sensor values</param>
        public SensorEventArgs(List<float> values)
        {
            Values = values;
        }

        /// <summary>
        /// Sensor event values.
        /// </summary>
        /// <value> values </value>
        public List<float> Values { get; private set; }
    }
}
