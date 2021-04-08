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

namespace Compass.Models
{
    /// <summary>
    /// Orientation sensor service interface.
    /// </summary>
    public interface IOrientationSensorService
    {
        #region properties

        /// <summary>
        /// Event invoked whenever orientation sensor's data is updated.
        /// </summary>
        event EventHandler<float> OrientationSensorDataUpdated;

        /// <summary>
        /// Event invoked if the orientation sensor is not supported.
        /// </summary>
        event EventHandler NotSupported;

        #endregion

        #region methods

        /// <summary>
        /// Initializes the service.
        /// </summary>
        void Init();

        /// <summary>
        /// Starts orientation sensor.
        /// </summary>
        void Start();

        #endregion
    }
}
