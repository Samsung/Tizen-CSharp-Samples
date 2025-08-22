/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

using System.Collections.Generic;
using Alarms.ViewModels;

namespace Alarms.Models
{
    /// <summary>
    /// Service to maintain alarms in the system.
    /// </summary>
    public interface IAlarmListService
    {
        #region methods

        /// <summary>
        /// Gets list of currently set alarms by the application.
        /// </summary>
        /// <returns>List of alarms.</returns>
        List<AlarmInfoViewModel> GetAlarmList();

        /// <summary>
        /// Sets new alarm.
        /// </summary>
        /// <param name="alarmInfo">Alarm object.</param>
        /// <returns>Created Alarm ID.</returns>
        int SetAlarm(AlarmInfoViewModel alarmInfo);

        /// <summary>
        /// Removes alarm passed in object reference.
        /// </summary>
        /// <param name="alarmReference">Alarm object.</param>
        void RemoveAlarm(object alarmReference);

        #endregion
    }
}