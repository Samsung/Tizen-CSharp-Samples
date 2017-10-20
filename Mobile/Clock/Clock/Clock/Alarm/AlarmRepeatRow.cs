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

using Clock.Controls;
using Clock.Styles;
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// Actual drawing of each alarm repeat setting page is done in this class
    /// It inherits from RelativeLayout
    /// </summary>
    internal class AlarmRepeatRow : RelativeLayout
    {
        /// <summary>
        /// Sets main label which shows week day label
        /// </summary>
        private Label mainLabel;

        /// <summary>
        /// CheckBox which users can check to indicate alarm repeat
        /// </summary>
        internal CheckBox weekdayCheckbox;

        private string mainStr;

        /// <summary>
        /// CheckBox which users can check to indicate alarm repeat
        /// </summary>
        /// <param name="weekFlag">Week flag to indicate which week flag this row will show</param>
        /// <seealso cref="AlarmWeekFlag">
        public AlarmRepeatRow(AlarmWeekFlag weekFlag)
        {
            if (mainLabel != null)
            {
                return;
            }

            HeightRequest = 120;
            VerticalOptions = LayoutOptions.Start;
            switch (weekFlag)
            {
                case AlarmWeekFlag.AllDays:
                    mainStr = "Everyday";
                    break;
                case AlarmWeekFlag.Monday:
                    mainStr = "Every Monday";
                    break;
                case AlarmWeekFlag.Tuesday:
                    mainStr = "Every Tuesday";
                    break;
                case AlarmWeekFlag.Wednesday:
                    mainStr = "Every Wednesday";
                    break;
                case AlarmWeekFlag.Thursday:
                    mainStr = "Every Thursday";
                    break;
                case AlarmWeekFlag.Friday:
                    mainStr = "Every Friday";
                    break;
                case AlarmWeekFlag.Saturday:
                    mainStr = "Every Saturday";
                    break;
                case AlarmWeekFlag.Sunday:
                    mainStr = "Every Sunday";
                    break;
                default:
                    mainStr = "";
                    break;
            }

            // day of the week to repeat
            mainLabel = new Label
            {
                HeightRequest = 54,
                Style = AlarmStyle.T023,
                Text = mainStr,
            };
            // to meet To meet thin attribute for font, need to use custom feature
            FontFormat.SetFontWeight(mainLabel, FontWeight.Light);
            Children.Add(mainLabel,
                    Constraint.RelativeToParent((parent) => { return 32; }),
                    Constraint.RelativeToParent((parent) => { return (120 - 54) / 2; }));

            /// Adds CheckBox in this row
            weekdayCheckbox = new CheckBox
            {
                HeightRequest = 50,
                WidthRequest = 50,
            };

            /// Checks whether this weekFlag indicates this CheckBox should turn on or not
            if (IsTurnOn(AlarmModel.BindableAlarmRecord.WeekFlag, weekFlag))
            {
                weekdayCheckbox.IsChecked = true;
            }

            /// When CheckBox is checked, needs to perform proper action based on circumstance
            /// For example if all days is checked out then all other CheckBox should also checked out
            weekdayCheckbox.Checked += (s, e) =>
            {
                CheckBox c = (CheckBox)s;
                var obj = this.Parent;
                AlarmRepeatPage currentPage;
                while (true)
                {
                    if (obj.GetType() == typeof(AlarmRepeatPage))
                    {
                        currentPage = (AlarmRepeatPage)obj;
                        break;
                    }
                    else
                    {
                        obj = obj.Parent;
                    }
                }

                if (weekFlag != AlarmWeekFlag.AllDays)
                {
                    Update(AlarmModel.BindableAlarmRecord, weekFlag, e.Value); // closure warning (GC prevent)
                    if (e.Value == false)
                    {
                        ((AlarmRepeatRow)(currentPage.allDays.View)).weekdayCheckbox.IsChecked = false; // if any off turn off everyday
                    }
                    else
                    {
                        if (AlarmModel.BindableAlarmRecord.WeekFlag == AlarmWeekFlag.AllDays)
                        {
                            ((AlarmRepeatRow)(currentPage.allDays.View)).weekdayCheckbox.IsChecked = true; // if any off turn off everyday
                        }
                    }
                }
                else
                {
                    if (e.Value == false)
                    {
                        ((AlarmRepeatRow)(currentPage.mon.View)).weekdayCheckbox.IsChecked = false;
                        ((AlarmRepeatRow)(currentPage.tue.View)).weekdayCheckbox.IsChecked = false;
                        ((AlarmRepeatRow)(currentPage.wed.View)).weekdayCheckbox.IsChecked = false;
                        ((AlarmRepeatRow)(currentPage.thur.View)).weekdayCheckbox.IsChecked = false;
                        ((AlarmRepeatRow)(currentPage.fri.View)).weekdayCheckbox.IsChecked = false;
                        ((AlarmRepeatRow)(currentPage.sat.View)).weekdayCheckbox.IsChecked = false;
                        ((AlarmRepeatRow)(currentPage.sun.View)).weekdayCheckbox.IsChecked = false;
                    }
                    else
                    {
                        ((AlarmRepeatRow)(currentPage.mon.View)).weekdayCheckbox.IsChecked = true;
                        ((AlarmRepeatRow)(currentPage.tue.View)).weekdayCheckbox.IsChecked = true;
                        ((AlarmRepeatRow)(currentPage.wed.View)).weekdayCheckbox.IsChecked = true;
                        ((AlarmRepeatRow)(currentPage.thur.View)).weekdayCheckbox.IsChecked = true;
                        ((AlarmRepeatRow)(currentPage.fri.View)).weekdayCheckbox.IsChecked = true;
                        ((AlarmRepeatRow)(currentPage.sat.View)).weekdayCheckbox.IsChecked = true;
                        ((AlarmRepeatRow)(currentPage.sun.View)).weekdayCheckbox.IsChecked = true;
                    }
                }
            };

            Children.Add(weekdayCheckbox,
                Constraint.RelativeToParent((parent) => (parent.X + parent.Width - (50 + 32))),
                Constraint.RelativeToParent((parent) => { return (120 - 50) / 2; }));
        }

        /// <summary>
        /// Update the row based on newly changed week flag
        /// </summary>
        /// <param name="alarmData">The alarm record to update alarm week flag</param>
        /// <seealso cref="AlarmRecord">
        /// <param name="changed">The changed alarm week flag</param>
        /// <seealso cref="AlarmWeekFlag">
        /// <param name="value">This input is currently not used but defined for future use</param>
        private void Update(AlarmRecord alarmData, AlarmWeekFlag changed, bool value)
        {
            if (value)
            {
                AlarmModel.BindableAlarmRecord.WeekFlag = alarmData.WeekFlag | changed;
            }
            else
            {
                AlarmModel.BindableAlarmRecord.WeekFlag = alarmData.WeekFlag & ~changed;
            }
        }

        /// <summary>
        /// Checks whether this CheckBox should turn on or off
        /// </summary>
        /// <param name="alarmRecordWeekFlag">The alarm record week flag</param>
        /// <seealso cref="AlarmWeekFlag">
        /// <param name="thisCellWeekFlag">The week flag for this cell</param>
        /// <seealso cref="AlarmWeekFlag">
        /// <param name="value">Returns whether this cell CheckBox should turn on or off</param>
        private bool IsTurnOn(AlarmWeekFlag alarmRecordWeekFlag, AlarmWeekFlag thisCellWeekFlag)
        {
            if (thisCellWeekFlag == AlarmWeekFlag.AllDays)
            {
                if (alarmRecordWeekFlag == thisCellWeekFlag)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (((int)alarmRecordWeekFlag & (int)thisCellWeekFlag) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
