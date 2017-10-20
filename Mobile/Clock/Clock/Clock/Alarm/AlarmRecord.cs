/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Clock.Interfaces;
using Clock.Styles;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// The AlarmRecord class
    /// </summary>
    public class AlarmRecord : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets/Sets whether the AlarmRecord object has been serialized
        /// This indicates whether it is newly created or not.
        /// </summary>
        private bool _isSerialized;

        /// <summary>
        /// Mute or not
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

        private DateFormat _alarmDateFormat;
        /// <summary>
        /// Alarm's date format
        /// </summary>
        public DateFormat AlarmDateFormat
        {
            get
            {
                return _alarmDateFormat;
            }

            set
            {
                if (_alarmDateFormat != value)
                {
                    _alarmDateFormat = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AlarmDateFormat"));
                }
            }
        }

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

        /// <summary>
        /// Gets/Sets whether alarm repeats or not
        /// </summary>
        private bool _repeat;
        public bool Repeat
        {
            get { return _repeat; }
            set
            {
                if (_repeat != value)
                {
                    _repeat = value;
                    CheckDateLabelVisibility();
                    OnPropertyChanged("Repeat");
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
                    //WeekdayRepeatText = GetFormatted(WeekFlag, AlarmState < AlarmStates.Inactive ? true : false);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WeekFlag"));
                    if (_weekFlag == AlarmWeekFlag.Never)
                    {
                        Repeat = false;
                    }
                    else
                    {
                        Repeat = true;
                    }
                }
            }
        }

        private FormattedString _weekdayRepeatText;
        /// <summary>
        /// Gets/Sets a formatted string about weekday alarm repeat information
        /// </summary>
        public FormattedString WeekdayRepeatText
        {
            get
            {
                return _weekdayRepeatText;
            }

            set
            {
                if (_weekdayRepeatText != value)
                {
                    _weekdayRepeatText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WeekdayRepeatText"));
                }
            }
        }

        private AlarmTypes _alarmType;
        /// <summary>
        /// Gets/Sets alarm type
        /// </summary>
        public AlarmTypes AlarmType
        {
            get
            {
                return _alarmType;
            }

            set
            {
                if (_alarmType != value)
                {
                    _alarmType = value;
                    if (_alarmType == AlarmTypes.Vibration)
                    {
                        Volume = 0;
                    }

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AlarmType"));
                }
            }
        }

        private Double _volume;
        /// <summary>
        /// Gets/Sets alarm volume
        /// </summary>
        public Double Volume
        {
            get
            {
                return _volume;
            }

            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    if (_volume != 0)
                    {
                        // Update IsMute value
                        if (IsMute)
                        {
                            IsMute = false;
                        }

                        // Update AlarmType value
                        if (AlarmType == AlarmTypes.Vibration)
                        {
                            AlarmType = AlarmTypes.Sound;
                        }
                    }
                    else
                    {
                        // Update IsMute value
                        if (!IsMute)
                        {
                            IsMute = true;
                        }

                        // Update AlarmType value
                        if (AlarmType == AlarmTypes.Sound)
                        {
                            AlarmType = AlarmTypes.Vibration;
                        }
                    }

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Volume"));
                }
            }
        }

        private bool _isMute;

        /// <summary>
        /// Mute or not
        /// </summary>
        public bool IsMute
        {
            get
            {
                return _isMute;
            }

            set
            {
                if (_isMute != value)
                {

                    _isMute = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsMute"));
                }
            }
        }

        private AlarmToneTypes _alarmTone;
        /// <summary>
        /// Gets/Sets the tone of alarm
        /// </summary>
        public AlarmToneTypes AlarmToneType
        {
            get
            {
                return _alarmTone;
            }

            set
            {
                if (_alarmTone != value)
                {
                    _alarmTone = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AlarmToneType"));
                }
            }
        }

        /// <summary>
        /// Gets/Sets whether alarm snoozes or not
        /// </summary>
        private bool _snooze;
        public bool Snooze
        {
            get { return _snooze; }
            set
            {
                if (_snooze != value)
                {
                    _snooze = value;
                    OnPropertyChanged("Snooze");
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
                    if (_alarmstate == AlarmStates.Inactive || value == AlarmStates.Inactive)
                    {
                        WeekdayRepeatText = GetFormatted(WeekFlag, value < AlarmStates.Inactive ? true : false);
                    }

                    _alarmstate = value;
                    OnPropertyChanged("AlarmState");
                }
            }
        }

        private string _alarmName;
        /// <summary>
        /// Gets/Sets the name of alarm
        /// </summary>
        public string AlarmName
        {
            get
            {
                return _alarmName;
            }

            set
            {
                if (_alarmName != value)
                {
                    _alarmName = value;
                    CheckDateLabelVisibility();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AlarmName"));
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

        /// <summary>
        /// Flag to delete or not
        /// </summary>
        private bool _delete;
        public bool Delete
        {
            get { return _delete; }
            set
            {
                if (_delete != value)
                {
                    _delete = value;
                    OnPropertyChanged("Delete");
                }
            }
        }

        private void CheckDateLabelVisibility()
        {
            if (AlarmName == "" && !Repeat)
            {
                IsVisibleDateLabel = true;
            }
            else
            {
                IsVisibleDateLabel = false;
            }
        }

        private bool _isVisibleDateLabel;
        public bool IsVisibleDateLabel
        {
            get { return _isVisibleDateLabel; }
            set
            {
                if (_isVisibleDateLabel != value)
                {
                    _isVisibleDateLabel = value;
                    OnPropertyChanged("IsVisibleDateLabel");
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
        /// Return a string that represents the current CityRecord object.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return "Alarm[" + DateCreated + "] " + ScheduledDateTime
                + ", Name <" + AlarmName + ">"
                + ", NativeID[" + NativeAlarmID + "]"
                + ", (" + WeekFlag + ")"
                + ", (" + AlarmState + ")";
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
        /// Sets the default value for AlarmRecord object
        /// </summary>
        public void SetDefault()
        {
            /// Sets current time to scheduled date time (Alarm creation time)
            ScheduledDateTime = System.DateTime.Now;

            // 24-hour format or not (AM or PM)
            AlarmDateFormat = ((App)Application.Current).Is24hourFormat ? DateFormat.Format24Hour : DateFormat.Format12HourAM;
            if (((App)Application.Current).Is24hourFormat)
            {
                AlarmDateFormat = DateFormat.Format24Hour;
            }
            else
            {
                if (ScheduledDateTime.ToString("tt").Equals("AM"))
                {
                    AlarmDateFormat = DateFormat.Format12HourAM;
                }
                else
                {
                    AlarmDateFormat = DateFormat.Format12HourPM;
                }
            }

            /// Sets Never to Repeat weekday flag
            WeekFlag = AlarmWeekFlag.Never;

            /// Sets WeekdayRepeatText
            WeekdayRepeatText = GetFormatted(WeekFlag, true);

            /// Sets repeat to false
            Repeat = false;

            /// Sets default alarm type to sound
            AlarmType = AlarmTypes.Sound;

            /// Sets volume to 0.1
            Volume = 0.7;

            IsMute = false;

            /// Sets default alarm to alarm mp3
            AlarmToneType = AlarmToneTypes.Default;

            /// Sets default state to active
            AlarmState = AlarmStates.Snooze;
            ///// Sets snooze to true
            Snooze = true;

            /// Sets alarm name to empty
            AlarmName = "";

            /// This indicates whether it is newly created or not.
            /// This value should set to false at default creation time
            IsSerialized = false;

            /// This field is used to uniquely identify alarm by created time
            DateCreated = ScheduledDateTime.TimeOfDay;
        }

        /// <summary>
        /// Deep copy alarm record
        /// </summary>
        /// <param name="bindableAlarmRecord">Alarm record to deep copy</param>
        /// <seealso cref="AlarmRecord">
        public void DeepCopy(AlarmRecord Record, bool includeUID = true)
        {
            // All properties should be copied to this alarm record object
            IsSerialized = Record.IsSerialized;
            ScheduledDateTime = Record.ScheduledDateTime;
            AlarmDateFormat = Record.AlarmDateFormat;
            Repeat = Record.Repeat;
            WeekFlag = Record.WeekFlag;
            WeekdayRepeatText = Record.WeekdayRepeatText;
            AlarmType = Record.AlarmType;
            Volume = Record.Volume;
            IsMute = Record.IsMute;
            AlarmToneType = Record.AlarmToneType;
            Snooze = Record.Snooze;
            AlarmState = Record.AlarmState;
            AlarmName = Record.AlarmName;
            if (includeUID)
            {
                DateCreated = Record.DateCreated;
                NativeAlarmID = Record.NativeAlarmID;
            }

            Delete = Record.Delete;
            IsVisibleDateLabel = Record.IsVisibleDateLabel;
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

        /// <summary>
        /// Gets week day string
        /// </summary>
        /// <param name="i">weekday in integer (1 is Mon, 6 is Sat, 0 is Sun).</param>
        /// <returns>Returns string about week day</returns>
        private string GetWeekDay(int i)
        {
            string s = "";
            switch (i)
            {
                case 1:
                    s = "M ";
                    break;
                case 2:
                    s = "T ";
                    break;
                case 3:
                    s = "W ";
                    break;
                case 4:
                    s = "T ";
                    break;
                case 5:
                    s = "F ";
                    break;
                case 6:
                    s = "S ";
                    break;
                case 0:
                    s = "S ";
                    break;
            }

            return s;
        }

        /// <summary>
        /// Gets formatted string based on week flag and state (active or inactive)
        /// </summary>
        /// <param name="weekFlag">bit flag to indicate alarm repeat days </param>
        /// <param name="isNormal">whether it is active or inactive (true or false)</param>
        /// <returns>Returns formatted string for alarm time information</returns>
        internal FormattedString GetFormatted(AlarmWeekFlag weekFlag, bool isNormal)
        {
            FormattedString newStr = new FormattedString();

            for (int i = 1; i <= 7; i++) // Mon to Saturday
            {
                if (i == 7)
                {
                    i = 0;
                }

                int mask = 1 << i;
                if (((int)weekFlag & mask) > 0)
                {
                    if (isNormal)
                    {
                        newStr.Spans.Add(new Span
                        {
                            ForegroundColor = Color.FromHex("FF59B03A"),
                            Text = GetWeekDay(i),
                            FontSize = CommonStyle.GetDp(32)
                        });
                    }
                    else
                    {
                        newStr.Spans.Add(new Span
                        {
                            ForegroundColor = Color.FromHex("6659B03A"),
                            Text = GetWeekDay(i),
                            FontSize = CommonStyle.GetDp(32)
                        });
                    }
                }
                else
                {
                    if (isNormal)
                    {
                        newStr.Spans.Add(new Span
                        {
                            ForegroundColor = Color.FromHex("FFB3B3B3"),
                            Text = GetWeekDay(i),
                            FontSize = CommonStyle.GetDp(32)
                        });
                    }
                    else
                    {
                        newStr.Spans.Add(new Span
                        {
                            ForegroundColor = Color.FromHex("66B3B3B3"),
                            Text = GetWeekDay(i),
                            FontSize = CommonStyle.GetDp(32)
                        });
                    }
                }

                if (i == 0)
                {
                    break;
                }
            }

            return newStr;
        }

        public void PrintProperty()
        {
            DependencyService.Get<ILog>().Debug("----  PrintProperty :" + this);
            DependencyService.Get<ILog>().Debug("DateCreated: " + DateCreated + ",   ScheduledDateTime : " + ScheduledDateTime);
            DependencyService.Get<ILog>().Debug("NativeAlarmID : " + NativeAlarmID + ", Repeat? : " + Repeat + ", WeekFlag : " + WeekFlag);
            DependencyService.Get<ILog>().Debug("AlarmName : (" + AlarmName + "),   IsVisibleDateLabel : " + IsVisibleDateLabel);
            DependencyService.Get<ILog>().Debug("AlarmType : " + AlarmType + ",  Volume : " + Volume + ",  IsMute : " + IsMute + ", Delete : " + Delete);
            DependencyService.Get<ILog>().Debug("AlarmToneType : " + AlarmToneType + ",  AlarmState : " + AlarmState + ",   Snooze : " + Snooze);
            DependencyService.Get<ILog>().Debug("IsSerialized : " + IsSerialized + ",  AlarmDateFormat : " + AlarmStringConverter.GetSubString(StringTypes.AlarmDateFormat, this));
            DependencyService.Get<ILog>().Debug("WeekdayRepeatText : " + WeekdayRepeatText);
            if (WeekdayRepeatText != null && WeekdayRepeatText.Spans != null)
            {
                for (int i = 0; i < 7; i++)
                {
                    DependencyService.Get<ILog>().Debug(i + " : " + WeekdayRepeatText.Spans[i].Text + " : " + WeekdayRepeatText.Spans[i].ForegroundColor);
                }
            }
        }
    }
}
