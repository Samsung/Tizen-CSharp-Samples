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

using System;
using Alarms.Models;

namespace Alarms.ViewModels
{
    /// <summary>
    /// AlarmInfo class.
    /// Alarm settings.
    /// </summary>
    public class AlarmInfoViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Alarm reference for future operations on this alarm.
        /// </summary>
        public object AlarmReference;

        /// <summary>
        /// Unique Id of alarm.
        /// </summary>
        public int AlarmId;

        /// <summary>
        /// Delay after which the alarm application will be started.
        /// </summary>
        public int Delay;

        /// <summary>
        /// Exact date for alarm execution.
        /// </summary>
        public DateTime Date;

        /// <summary>
        /// Alarm "repeat enabled" state flag.
        /// </summary>
        public bool IsRepeatEnabled;

        /// <summary>
        /// Flags for weekdays repetition.
        /// </summary>
        public DaysOfWeekFlags DaysFlags;

        /// <summary>
        /// Backing field for the <see cref="IsSelected"/> property.
        /// </summary>
        private bool _isSelected;

        #endregion

        #region properties

        /// <summary>
        /// An event that clients can use to be notified whenever the selection of any AppInfo object was changed.
        /// </summary>
        public static event EventHandler SelectionChanged;

        /// <summary>
        /// Application which will be executed by the alarm.
        /// </summary>
        public AppInfo AppInfo { get; set; }

        /// <summary>
        /// Indicates if the alarm is selected on alarm list.
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                SetProperty(ref _isSelected, value);
                SelectionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}