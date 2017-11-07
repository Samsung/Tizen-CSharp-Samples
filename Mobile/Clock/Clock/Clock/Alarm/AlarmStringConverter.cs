/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

namespace Clock.Alarm
{
    public enum StringTypes
    {
        AlarmRepeat,
        AlarmState,
        AlarmTone,
        AlarmType,
        AlarmDateFormat
    }

    /// <summary>
    /// This class translates certain type of binding event value to other type 
    /// </summary>
    internal class AlarmStringConverter
    {
        /// <summary>
        /// Get proper string based on edit type 
        /// </summary>
        /// <param name="editType">Alarm edit type to find proper converting rule</param>
        /// <seealso cref="AlarmEditTypes">
        /// <param name="alarmRecord">Alarm record to check converting rule against</param>
        /// <seealso cref="AlarmRecord">
        /// <returns>Returns converted string</returns>
        internal static string GetSubString(StringTypes editType, AlarmRecord alarmRecord)
        {
            string subStr = "";

            switch (editType)
            {
                case StringTypes.AlarmRepeat:
                    if (alarmRecord.WeekFlag == AlarmWeekFlag.Never)
                    {
                        subStr = "Never";
                    }
                    else if (alarmRecord.WeekFlag == AlarmWeekFlag.AllDays)
                    {
                        subStr = "Everyday";
                    }
                    else
                    {
                        for (int i = 1; i < 7; i++)
                        {
                            int mask = 1 << i;
                            if (((int)alarmRecord.WeekFlag & mask) > 0)
                            {
                                switch (mask)
                                {
                                    case (int)AlarmWeekFlag.Monday:
                                        subStr += "Mon ";
                                        break;
                                    case (int)AlarmWeekFlag.Tuesday:
                                        subStr += "Tue ";
                                        break;
                                    case (int)AlarmWeekFlag.Wednesday:
                                        subStr += "Wed ";
                                        break;
                                    case (int)AlarmWeekFlag.Thursday:
                                        subStr += "Thu ";
                                        break;
                                    case (int)AlarmWeekFlag.Friday:
                                        subStr += "Fri ";
                                        break;
                                    case (int)AlarmWeekFlag.Saturday:
                                        subStr += "Sat ";
                                        break;
                                }
                            }
                        }

                        if (((int)alarmRecord.WeekFlag & 0x1) > 0)
                        {
                            subStr += "Sun ";
                        }
                    }

                    break;
                case StringTypes.AlarmType:
                    if (alarmRecord.AlarmType == AlarmTypes.Sound)
                    {
                        subStr = "Sound";
                    }
                    else if (alarmRecord.AlarmType == AlarmTypes.Vibration)
                    {
                        subStr = "Vibration";
                    }
                    else if (alarmRecord.AlarmType == AlarmTypes.SoundVibration)
                    {
                        subStr = "Vibration and sound";
                    }

                    break;
                case StringTypes.AlarmTone:
                    if (alarmRecord.AlarmToneType == AlarmToneTypes.Default)
                    {
                        subStr = "Default";
                    }
                    else if (alarmRecord.AlarmToneType == AlarmToneTypes.AlarmMp3)
                    {
                        subStr = "alarm.mp3";
                    }
                    else if (alarmRecord.AlarmToneType == AlarmToneTypes.RingtoneSdk)
                    {
                        subStr = "ringtone_sdk.mp3";
                    }

                    break;
                case StringTypes.AlarmState:
                    if (alarmRecord.AlarmState == AlarmStates.Active)
                    {
                        subStr = "Active";
                    }
                    else if (alarmRecord.AlarmState == AlarmStates.Inactive)
                    {
                        subStr = "Inactive";
                    }
                    else if (alarmRecord.AlarmState == AlarmStates.Snooze)
                    {
                        subStr = "Snooze";
                    }

                    break;
                case StringTypes.AlarmDateFormat:
                    if (alarmRecord.AlarmDateFormat == DateFormat.Format24Hour)
                    {
                        subStr = "";
                    }
                    else if (alarmRecord.AlarmDateFormat == DateFormat.Format12HourAM)
                    {
                        subStr = "AM";
                    }
                    else if (alarmRecord.AlarmDateFormat == DateFormat.Format12HourPM)
                    {
                        subStr = "PM";
                    }

                    break;
                default:
                    break;
            }

            return subStr;
        }
    }
}
