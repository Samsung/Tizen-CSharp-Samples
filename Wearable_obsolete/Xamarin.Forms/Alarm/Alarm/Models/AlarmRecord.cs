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

using System;
using System.ComponentModel;

namespace Alarm.Models
{
    [Flags]
    public enum AlarmWeekFlag
    {
        /// <summary>
        /// Identifier for Never
        /// </summary>
        Never = 0x00,

        /// <summary>
        /// Identifier for Sunday.
        /// </summary>
        Sunday = 0x01,

        /// <summary>
        /// Identifier for Monday.
        /// </summary>
        Monday = 0x02,

        /// <summary>
        /// Identifier for Tuesday.
        /// </summary>
        Tuesday = 0x04,

        /// <summary>
        /// Identifier for Wednesday.
        /// </summary>
        Wednesday = 0x08,

        /// <summary>
        /// Identifier for Thursday.
        /// </summary>
        Thursday = 0x10,

        /// <summary>
        /// Identifier for Friday.
        /// </summary>
        Friday = 0x20,

        /// <summary>
        /// Identifier for Saturday.
        /// </summary>
        Saturday = 0x40,

        /// <summary>
        /// All Days of the Week.
        /// </summary>
        AllDays = Sunday | Monday | Tuesday | Wednesday | Thursday | Friday | Saturday,

        /// <summary>
        /// Only Weekdays
        /// </summary>
        WeekDays = Monday | Tuesday | Wednesday | Thursday | Friday
    }

    /// <summary>
    /// The enumeration of AlarmStates
    /// </summary>
    public enum AlarmStates
    {
        /// <summary>
        /// Identifier for Snooze
        /// </summary>
        Snooze,
        /// <summary>
        /// Identifier for Active
        /// </summary>
        Active,
        /// <summary>
        /// Identifier for Inactive
        /// </summary>
        Inactive,
    }

    public class AlarmRecord : INotifyPropertyChanged
    {
        private DateTime _scheduledDateTime;
        /// <summary>
        /// Gets/Sets ScheduledDateTime
        /// </summary>
        public DateTime ScheduledDateTime
        {
            get
            {
                return _scheduledDateTime;
            }

            set
            {
                if (_scheduledDateTime != value)
                {
                    _scheduledDateTime = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ScheduledDateTime"));
                }
            }
        }

        private AlarmWeekFlag _weekFlag;
        /// <summary>
        /// Gets/Sets the day of week when an alarm recurs
        /// </summary>
        public AlarmWeekFlag WeekFlag
        {
            get
            {
                return _weekFlag;
            }

            set
            {
                if (_weekFlag != value)
                {
                    _weekFlag = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WeekFlag"));
                }
            }
        }

        private AlarmStates _alarmstate;
        /// <summary>
        /// Gets/Sets the state of alarm
        /// </summary>
        public AlarmStates AlarmState
        {
            get { return _alarmstate; }
            set
            {
                if (_alarmstate != value)
                {
                    _alarmstate = value;
                    OnPropertyChanged("AlarmState");
                }
            }
        }

        /// <summary>
        /// Gets/Sets alarm creation time
        /// It is used as a Key in AlarmRecordDictionary.
        /// </summary>
        public TimeSpan DateCreated { get; set; }

        /// <summary>
        /// Gets/Sets system alarm ID
        /// </summary>
        public int NativeAlarmID { get; set; }


        private bool _isSerialized;

        /// <summary>
        /// Gets/Sets whether the AlarmRecord object has been serialized
        /// This indicates whether it is newly created or not.
        /// </summary>
        public bool IsSerialized
        {
            get
            {
                return _isSerialized;
            }

            set
            {
                if (_isSerialized != value)
                {
                    _isSerialized = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsSerialized"));
                }
            }
        }

        ///<summary>
        ///Event that is raised when the properties of AlarmRecord change
        ///</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the AlarmRecord class
        /// </summary>
        public AlarmRecord()
        {
        }

        /// <summary>
        /// Converts the string of the creation date time
        /// </summary>
        /// <returns> Returns a string representation of value of the alarm creation time</returns>
        public string GetUniqueIdentifier()
        {
            return DateCreated.ToString();
        }

        /// <summary>
        /// Return a string that represents the current AlarmRecord object.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return "Alarm " + ScheduledDateTime
                + ", NativeID[" + NativeAlarmID + "]"
                + ", (" + WeekFlag + ")"
                + ", (" + DateCreated + ")"
                + ", (" + AlarmState + ")"
                + ", (" + IsSerialized + ")";
        }

        /// <summary>
        /// Sets the default value for AlarmRecord object
        /// </summary>
        public void SetDefault()
        {
            /// Sets current time to scheduled date time (Alarm creation time)
            ScheduledDateTime = System.DateTime.Now;

            /// Sets Never to Repeat weekday flag
            WeekFlag = AlarmWeekFlag.AllDays;

            /// This field is used to uniquely identify alarm by created time
            DateCreated = ScheduledDateTime.TimeOfDay;

            AlarmState = AlarmStates.Active;

            /// This indicates whether it is newly created or not.
            /// This value should set to false at default creation time
            IsSerialized = false;
        }

        /// <summary>
        /// Deep copy alarm record
        /// </summary>
        /// <param name="bindableAlarmRecord">Alarm record to deep copy</param>
        /// <seealso cref="AlarmRecord">
        public void DeepCopy(AlarmRecord Record, bool includeUID = true)
        {
            // All properties should be copied to this alarm record object
            ScheduledDateTime = Record.ScheduledDateTime;
            WeekFlag = Record.WeekFlag;
            AlarmState = Record.AlarmState;
            IsSerialized = Record.IsSerialized;
            if (includeUID)
            {
                DateCreated = Record.DateCreated;
                NativeAlarmID = Record.NativeAlarmID;
            }
        }

        /// <summary>
        /// When property is changed, registered event callback is invoked
        /// </summary>
        /// <param name="propertyName">Property name of the change event occured</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}