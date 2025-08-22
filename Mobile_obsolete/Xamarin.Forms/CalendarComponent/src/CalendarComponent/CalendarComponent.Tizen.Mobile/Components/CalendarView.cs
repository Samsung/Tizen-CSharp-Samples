/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Collections.Generic;
using Xamarin.Forms;

namespace CalendarComponent.Tizen.Mobile.Components
{
    /// <summary>
    /// CalendarView component.
    /// </summary>
    public class CalendarView : View
    {
        #region properties

        /// <summary>
        /// BindableProperty. Identifies the MinimumDate bindable property.
        /// </summary>
        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(CalendarView), new DateTime(1902, 1, 1), coerceValue: (bindable, value) =>
        {
            DateTime dateTime = (DateTime)value;
            if (dateTime.Year < 1902)
            {
                dateTime = dateTime.AddYears(1902 - dateTime.Year);
            }

            return dateTime;
        });

        /// <summary>
        /// BindableProperty. Identifies the MaximumDate bindable property.
        /// </summary>
        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(CalendarView), new DateTime(2037, 12, 31), coerceValue: (bindable, value) =>
        {
            DateTime dateTime = (DateTime)value;
            if (dateTime.Year > 2037)
            {
                dateTime = dateTime.AddYears(2037 - dateTime.Year);
            }

            return dateTime;
        });

        /// <summary>
        /// BindableProperty. Identifies the FirstDayOfWeek bindable property.
        /// </summary>
        public static readonly BindableProperty FirstDayOfWeekProperty = BindableProperty.Create(nameof(FirstDayOfWeek), typeof(DayOfWeek), typeof(CalendarView), DayOfWeek.Sunday);

        /// <summary>
        /// BindableProperty. Identifies the WeekDayNames bindable property.
        /// </summary>
        public static readonly BindableProperty WeekDayNamesProperty = BindableProperty.Create(nameof(WeekDayNames), typeof(IReadOnlyList<string>), typeof(CalendarView), new List<string> { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" }, validateValue: (bindable, value) =>
        {
            List<string> weekDayNames = (List<string>)value;
            int? count = weekDayNames?.Count;
            return count == 7;
        });

        /// <summary>
        /// BindableProperty. Identifies the SelectedDate bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(CalendarView), DateTime.Today, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ((CalendarView)bindable).SelectedDateChanged?.Invoke(bindable, new DateChangedEventArgs((DateTime)oldValue, (DateTime)newValue));
        });

        /// <summary>
        /// Occurs when a date in the calendar is selected.
        /// </summary>
        public event EventHandler<DateChangedEventArgs> SelectedDateChanged;

        /// <summary>
        /// Gets or sets the MinimumDate value.
        /// Can use only year.
        /// Minimum value is 1902, If the value is less than 1902, it is ignored and set to 1902.
        /// </summary>
        public DateTime MinimumDate
        {
            get => (DateTime)GetValue(MinimumDateProperty);
            set => SetValue(MinimumDateProperty, value);
        }

        /// <summary>
        /// Gets or sets the MaximumDate value.
        /// Can use only year.
        /// Maximum value is 2037, If the value is bigger than 2037, it is ignored and set to 2037.
        /// </summary>
        public DateTime MaximumDate
        {
            get => (DateTime)GetValue(MaximumDateProperty);
            set => SetValue(MaximumDateProperty, value);
        }

        /// <summary>
        /// Gets or sets the first day of week.
        /// </summary>
        public DayOfWeek FirstDayOfWeek
        {
            get => (DayOfWeek)GetValue(FirstDayOfWeekProperty);
            set => SetValue(FirstDayOfWeekProperty, value);
        }

        /// <summary>
        /// Gets or sets the weekday names displayed by the calendar.
        /// The number of items in WeekdayNames must be 7.
        /// </summary>
        public IReadOnlyList<string> WeekDayNames
        {
            get => (IReadOnlyList<string>)GetValue(WeekDayNamesProperty);
            set => SetValue(WeekDayNamesProperty, value);
        }

        /// <summary>
        /// Gets or sets the selected date.
        /// </summary>
        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        #endregion
    }
}