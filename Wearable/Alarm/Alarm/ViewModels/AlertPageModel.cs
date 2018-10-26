/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
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
    public enum SoundState
    {
        Stop = 0,
        Start = 1,
        Pause = 2,
    }

    public class AlertPageModel : BasePageModel
    {
        private AlarmRecord _alarmRecord;

        private SoundState _alertSoundState;

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

        public SoundState AlertSoundState
        {
            get
            {
                return _alertSoundState;
            }

            set
            {
                SetProperty(ref _alertSoundState, value, "AlertState");
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
            _alertSoundState = SoundState.Start;
        }

        public void PauseAlert()
        {
            AlarmNativeHandler.PauseSound();
            AlarmNativeHandler.StopVibration();
            _alertSoundState = SoundState.Pause;
        }

        public void ResumeAlert()
        {
            AlarmNativeHandler.ResumeSound();
            _alertSoundState = SoundState.Start;
        }

        /// <summary>
        /// Delete current AlarmRecord
        /// </summary>
        public void Dismiss()
        {
            AlarmNativeHandler.StopSound();
            AlarmNativeHandler.StopVibration();
            _alertSoundState = SoundState.Stop;

            if (_alarmRecord !=  null)
            {
                AlarmModel.DeleteAlarm(_alarmRecord);
            }
        }

    }
}
