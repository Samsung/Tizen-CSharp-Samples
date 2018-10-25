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

namespace MetalDetector.Models
{
    /// <summary>
    /// Interface of magnetometer service which allows to obtain the magnetometer data.
    /// </summary>
    public interface IMagnetometerService
    {
        #region properties

        /// <summary>
        /// Notifies about magnetometer data update.
        /// </summary>
        event EventHandler<IMagnetometerDataUpdatedArgs> Updated;

        #endregion

        #region methods

        /// <summary>
        /// Returns true if the magnetometer is supported, false otherwise.
        /// </summary>
        /// <returns>Flag indicating whether magnetometer is supported or not.</returns>
        bool IsSupported();

        /// <summary>
        /// Starts magnetometer sensor.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops magnetometer sensor.
        /// </summary>
        void Stop();

        #endregion
    }
}
