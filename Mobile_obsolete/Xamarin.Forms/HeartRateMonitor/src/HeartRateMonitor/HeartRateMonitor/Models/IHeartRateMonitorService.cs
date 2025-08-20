/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Threading.Tasks;

namespace HeartRateMonitor.Models
{
    /// <summary>
    /// IHeartRateMonitorService interface class.
    /// It defines methods and properties
    /// that should be implemented by HeartRateMonitorService class.
    /// </summary>
    public interface IHeartRateMonitorService
    {
        #region properties

        /// <summary>
        /// HeartRateMonitorDataChanged event.
        /// Notifies UI about heart rate value update.
        /// </summary>
        event EventHandler HeartRateMonitorDataChanged;

        /// <summary>
        /// HeartRateSensorNotSupported event.
        /// Notifies application about lack of heart rate sensor.
        /// </summary>
        event EventHandler HeartRateSensorNotSupported;

        #endregion

        #region methods

        /// <summary>
        /// Initializes HeartRateMonitorService class.
        /// </summary>
        void Init();

        /// <summary>
        /// Returns current heart rate value provided by the Tizen Sensor API.
        /// </summary>
        /// <returns>Current heart rate value provided by the Tizen Sensor API.</returns>
        int GetHeartRate();

        /// <summary>
        /// Starts notification about changes of heart rate value.
        /// </summary>
        void StartHeartRateMonitor();

        /// <summary>
        /// Stops notification about changes of heart rate value.
        /// </summary>
        void StopHeartRateMonitor();

        /// <summary>
        /// Returns true if all required privileges are granted, false otherwise.
        /// </summary>
        /// <returns>Task with check result.</returns>
        Task<bool> CheckPrivileges();

        #endregion
    }
}