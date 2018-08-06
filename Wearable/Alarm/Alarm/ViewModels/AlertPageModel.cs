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

using Alarm.Implements;
using Alarm.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Alarm.ViewModels
{
    public class AlertPageModel : BasePageModel
    {
        private AlarmRecord _alarmRecord;

        /// <summary>
        /// Alarm record to display in Alert Page
        /// </summary>
        public AlarmRecord Record
        {
            get
            {
                return _alarmRecord;
            }

            set
            {
                SetProperty(ref _alarmRecord, value, "Record");
            }
        }

        /// <summary>
        /// AlertPageModel constructor with AlarmRecord
        /// </summary>
        /// <param name="record">Record to be shown</param>
        public AlertPageModel(AlarmRecord record = null)
        {
            Record = record;
        }

        public void StartAlert()
        {
            AlarmNativeHandler.PlaySound();
            AlarmNativeHandler.StartVibration();
        }

        /// <summary>
        /// Delete current AlarmRecord
        /// </summary>
        public void Dismiss()
        {
            AlarmNativeHandler.StopSound();
            AlarmNativeHandler.StopVibration();

            if (_alarmRecord !=  null)
            {
                AlarmModel.DeleteAlarm(_alarmRecord);
            }
        }

    }
}
