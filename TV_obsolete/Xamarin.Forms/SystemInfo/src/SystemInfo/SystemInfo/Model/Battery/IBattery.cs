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

namespace SystemInfo.Model.Battery
{
    /// <summary>
    /// Interface that contains all necessary methods to get information about battery.
    /// </summary>
    public interface IBattery
    {
        #region properties

        /// <summary>
        /// Gets charging status.
        /// </summary>
        bool IsCharging { get; }

        /// <summary>
        /// Gets battery level.
        /// </summary>
        int BatteryLevel { get; }

        /// <summary>
        /// Gets battery level status.
        /// </summary>
        BatteryLevelStatus BatteryLevelStatus { get; }

        /// <summary>
        /// Event invoked when some information about battery was changed.
        /// </summary>
        event EventHandler<BatteryEventArgs> BatteryInfoChanged;

        #endregion

        #region methods

        /// <summary>
        /// Starts observing battery information for changes.
        /// </summary>
        /// <remarks>
        /// Event BatteryInfoChanged will be never invoked before calling this method.
        /// </remarks>
        void StartListening();

        #endregion
    }
}