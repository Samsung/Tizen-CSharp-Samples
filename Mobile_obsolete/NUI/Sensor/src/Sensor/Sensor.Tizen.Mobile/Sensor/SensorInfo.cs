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
    /// Sensor information class
    /// </summary>
    public class SensorInfo
    {
        /// <summary>
        /// Sensor Type.
        /// </summary>
        public string Type { get; set; } = "Undefined Sensor";

        /// <summary>
        /// Sensor model name.
        /// </summary>
        public string Name { get; set; } = "No Name";

        /// <summary>
        /// Sensor vendor name.
        /// </summary>
        public string Vendor { get; set; } = "No Vendor";

        /// <summary>
        /// Minimum range of sensor value.
        /// </summary>
        public float MinRange { get; set; } = 0;

        /// <summary>
        /// Maximum range of sensor value.
        /// </summary>
        public float MaxRange { get; set; } = 0;

        /// <summary>
        /// Resolution of sensor value.
        /// </summary>
        public float Resolution { get; set; } = 0;

        /// <summary>
        /// Minimum interval which application can set.
        /// </summary>
        public float MinInterval { get; set; } = 0;

        /// <summary>
        /// Sensor status.
        /// </summary>
        public string Status { get; set; } = "Permission Allowed";
    }
}
