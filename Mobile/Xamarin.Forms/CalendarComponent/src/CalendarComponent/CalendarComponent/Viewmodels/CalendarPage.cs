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
using System.Windows.Input;
using Xamarin.Forms;

namespace CalendarComponent.ViewModels
{
    public class CalendarPageViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Date stored at previous date checking.
        /// </summary>
        private DateTime _lastDate = DateTime.MinValue;

        /// <summary>
        /// Backing field for <see cref="SelectedDate"/>
        /// </summary>
        private DateTime _selectedDate = DateTime.Now;

        /// <summary>
        /// Backing field for <see cref="ImageFilename"/>
        /// </summary>
        private string _imageFilename = "";

        /// <summary>
        /// Backing field for <see cref="DayName"/>
        /// </summary>
        private string _dayName = "";

        #endregion

        #region properties

        /// <summary>
        /// Property bound with text label showing day name.
        /// Bound with text label.
        /// </summary>
        public string DayName
        {
            set => SetProperty(ref _dayName, value);
            get => _dayName;
        }

        /// <summary>
        /// Selected date.
        /// Property bound to SelectedDate property of calendar component.
        /// </summary>
        public DateTime SelectedDate
        {
            set
            {
                SetProperty(ref _selectedDate, value);
                ((Command)GoNextYear).ChangeCanExecute();
                ((Command)GoPrevYear).ChangeCanExecute();
            }

            get => _selectedDate;
        }

        /// <summary>
        /// Image filename.
        /// Property bound to image "Source" property.
        /// </summary>
        public string ImageFilename
        {
            set => SetProperty(ref _imageFilename, value);
            get => _imageFilename;
        }

        #endregion

        #region methods

        /// <summary>
        /// CalendarPageViewModel Constructor.
        /// Defines user commands.
        /// Sets day name and icon.
        /// Initializes timer.
        /// </summary>
        public CalendarPageViewModel()
        {
            // Assigns command increasing date by one year.
            GoNextYear = new Command(execute: () =>
                {
                    SelectedDate = SelectedDate.AddYears(1);
                },
                canExecute: () => SelectedDate.Year < 2038);

            // Assigns command decreasing date by one year.
            GoPrevYear = new Command(execute: () =>
                {
                    SelectedDate = SelectedDate.AddYears(-1);
                },
                canExecute: () => SelectedDate.Year > 1902);

            // Assigns command setting date to current one.
            GoToday = new Command(execute: () =>
            {
                SelectedDate = DateTime.Now;
            });

            UpdateToday();

            Device.StartTimer(TimeSpan.FromMinutes(1), UpdateToday);
        }

        /// <summary>
        /// Checks if day changed since last method execution.
        /// Updates properties with current values if day changed.
        /// </summary>
        /// <returns>Always returns true to keep timer recurring.</returns>
        private bool UpdateToday()
        {
            if (_lastDate.Day != DateTime.Now.Day)
            {
                _lastDate = DateTime.Now;
                DayName = _lastDate.DayOfWeek.ToString().ToUpper();
                ImageFilename = "day_" + _lastDate.Day + ".png";
            }

            return true;
        }

        /// <summary>
        /// Stores "Prev Year" button's command.
        /// </summary>
        public ICommand GoNextYear { get; }

        /// <summary>
        /// Stores "Next Year" button's command.
        /// </summary>
        public ICommand GoPrevYear { get; }

        /// <summary>
        /// Stores "Today" button's command.
        /// </summary>
        public ICommand GoToday { get; }

        #endregion
    }
}