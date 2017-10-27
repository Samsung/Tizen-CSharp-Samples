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
using Xamarin.Forms;

namespace Clock.Alarm
{
    /// <summary>
    /// AlarmEditTimePickerCell class
    /// It is a custom cell for setting alarm time
    /// </summary>
    public class AlarmEditTimePickerCell : Cell
    {
        /// <summary>
        /// Backing store for the Time bindable property.
        /// </summary>
        public static readonly BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(TimeSpan), typeof(AlarmEditTimePickerCell), new TimeSpan(0), BindingMode.TwoWay,
            (bindable, value) =>
            {
                var time = (TimeSpan)value;
                return time.TotalHours < 24 && time.TotalMilliseconds >= 0;
            });

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alarmRecordData">alarmRecordData</param>
        public AlarmEditTimePickerCell(AlarmRecord alarmRecordData)
        {
            Time = alarmRecordData.ScheduledDateTime.TimeOfDay;
        }

        /// <summary>
        /// Gets or sets the Time of TimePicker
        /// This is a bindable property.
        /// </summary>
        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }
    }
}
