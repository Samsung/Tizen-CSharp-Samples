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

using System;
using System.Globalization;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// Alarm value converter class.
    /// This class convert bindable source value to proper target value
    /// </summary>
    public class AlarmValueConverter : IValueConverter
    {
        /// <summary>
        /// Converting source value to target value
        /// </summary>
        /// <param name="value">Source object</param>
        /// <seealso cref="System.object">
        /// <param name="targetType">The target type to convert</param>
        /// <seealso cref="Type">
        /// <param name="CultureInfo">The culture info</param>
        /// <seealso cref="CultureInfo">
        /// <returns>Returns converted string</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(AlarmTypes))
            {
                AlarmTypes modelValue = (AlarmTypes)value;
                string s = "";

                switch (modelValue)
                {
                    case AlarmTypes.Sound:
                        s = "Sound";
                        break;
                    case AlarmTypes.Vibration:
                        s = "Vibration";
                        break;
                    case AlarmTypes.SoundVibration:
                        s = "Vibration and sound";
                        break;
                }

                return s;
            }
            else if (value.GetType() == typeof(AlarmToneTypes))
            {
                AlarmToneTypes modelValue = (AlarmToneTypes)value;
                string s = "";

                switch (modelValue)
                {
                    case AlarmToneTypes.Default:
                        s = "Default";
                        break;
                    case AlarmToneTypes.AlarmMp3:
                        s = "alarm.mp3";
                        break;
                    case AlarmToneTypes.RingtoneSdk:
                        s = "ringtone_sdk.mp3";
                        break;
                }

                return s;
            }
            else if (value.GetType() == typeof(AlarmWeekFlag))
            {
                string s = "";
                AlarmWeekFlag flag = (AlarmWeekFlag)value;
                if (flag == AlarmWeekFlag.Never)
                {
                    s = "Never";
                }
                else if (flag == AlarmWeekFlag.AllDays)
                {
                    s = "Everyday";
                }
                else
                {
                    for (int i = 1; i < 7; i++)
                    {
                        int mask = 1 << i;
                        if (((int)flag & mask) > 0)
                        {
                            switch (mask)
                            {
                                case (int)AlarmWeekFlag.Monday:
                                    s += "Mon ";
                                    break;
                                case (int)AlarmWeekFlag.Tuesday:
                                    s += "Tue ";
                                    break;
                                case (int)AlarmWeekFlag.Wednesday:
                                    s += "Wed ";
                                    break;
                                case (int)AlarmWeekFlag.Thursday:
                                    s += "Thu ";
                                    break;
                                case (int)AlarmWeekFlag.Friday:
                                    s += "Fri ";
                                    break;
                                case (int)AlarmWeekFlag.Saturday:
                                    s += "Sat ";
                                    break;
                            }
                        }
                    }

                    if (((int)flag & 0x1) > 0)
                    {
                        s += "Sun ";
                    }
                }

                return s;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Converting back source value to target value
        /// This method is not being used in this app.
        /// </summary>
        /// <param name="value">Source object</param>
        /// <seealso cref="System.object">
        /// <param name="targetType">The target type to convert</param>
        /// <seealso cref="Type">
        /// <param name="CultureInfo">The culture info</param>
        /// <seealso cref="CultureInfo">
        /// <returns>Returns converted string</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
