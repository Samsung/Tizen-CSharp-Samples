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

using System;
using Xamarin.Forms;

namespace Clock.Alarm
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
    /// This class defines alarm repeat setting page. 
    /// It inherits from ContentPage. 
    /// Users can select which weekday they want to get this alarm
    /// </summary>
    public class AlarmRepeatPage : ContentPage
    {
        /// <summary>
        /// Alarm repeat cell for all days
        /// </summary>
        internal AlarmRepeatCell allDays;
        /// <summary>
        /// Alarm repeat cell for Monday
        /// </summary>
        internal AlarmRepeatCell mon;
        /// <summary>
        /// Alarm repeat cell for Tuesday
        /// </summary>
        internal AlarmRepeatCell tue;
        /// <summary>
        /// Alarm repeat cell for Wednesday
        /// </summary>
        internal AlarmRepeatCell wed;
        /// <summary>
        /// Alarm repeat cell for Thursday
        /// </summary>
        internal AlarmRepeatCell thur;
        /// <summary>
        /// Alarm repeat cell for Friday
        /// </summary>
        internal AlarmRepeatCell fri;
        /// <summary>
        /// Alarm repeat cell for Saturday
        /// </summary>
        internal AlarmRepeatCell sat;
        /// <summary>
        /// Alarm repeat cell for Sunday
        /// </summary>
        internal AlarmRepeatCell sun;

        /// <summary>
        /// Constructor to draw UI for this alarm repeat setting
        /// </summary>
        public AlarmRepeatPage()
        {
            Draw();
        }

        /// <summary>
        /// Sets repeat cell to each repeat type
        /// </summary>
        internal void Draw()
        {
            Title = "Repeat Weekly";
            allDays = new AlarmRepeatCell(AlarmWeekFlag.AllDays);
            mon = new AlarmRepeatCell(AlarmWeekFlag.Monday);
            tue = new AlarmRepeatCell(AlarmWeekFlag.Tuesday);
            wed = new AlarmRepeatCell(AlarmWeekFlag.Wednesday);
            thur = new AlarmRepeatCell(AlarmWeekFlag.Thursday);
            fri = new AlarmRepeatCell(AlarmWeekFlag.Friday);
            sat = new AlarmRepeatCell(AlarmWeekFlag.Saturday);
            sun = new AlarmRepeatCell(AlarmWeekFlag.Sunday);
            TableRoot root = new TableRoot("RepeatView")
            {
                new TableSection()
                {
                    allDays,
                    mon,
                    tue,
                    wed,
                    thur,
                    fri,
                    sat,
                    sun
                }
            };
            TableView table = new TableView(root);
            this.Content = new StackLayout
            {
                Spacing = 0,
                Children =
                {
                    table
                }
            };
        }

        /// <summary>
        /// This method is called when this page instance is moving front (created previously)
        /// </summary>
        internal void Update()
        {
            Draw();
        }
    }
}
