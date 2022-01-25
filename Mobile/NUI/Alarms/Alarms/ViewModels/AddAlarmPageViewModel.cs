/*
 * Copyright (c) 2022 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Tizen.NUI.Binding;

namespace Alarms.ViewModels
{
    public class AddAlarmPageViewModel : INotifyPropertyChanged
    {
        private string date;
        private string time;
        private DateTime localDate;
        public ICommand DateMonthUp { get; private set; }
        public ICommand DateDayUp { get; private set; }
        public ICommand DateYearUp { get; private set; }
        public ICommand DateMonthDown { get; private set; }
        public ICommand DateDayDown { get; private set; }
        public ICommand DateYearDown { get; private set; }
        public ICommand TimeHourUp { get; private set; }
        public ICommand TimeMinuteUp { get; private set; }
        public ICommand TimeHourDown { get; private set; }
        public ICommand TimeMinuteDown { get; private set; }
        public AddAlarmPageViewModel()
        {
            localDate = DateTime.Now;
            date = localDate.ToString("MM/dd/yy");
            time = localDate.ToString("HH:mm");
            DateMonthUp = new Command(() => RaisedDateMonth());
            DateDayUp = new Command(() => RaisedDateDays());
            DateYearUp = new Command(() => RaisedDateYears());
            DateMonthDown = new Command(() => DecreasedDateMonth());
            DateDayDown = new Command(() => DecreasedDateDays());
            DateYearDown = new Command(() => DecreasedDateYears());
            TimeHourUp = new Command(() => RaisedTimeHours());
            TimeMinuteUp = new Command(() => RaisedTimeMinutes());
            TimeHourDown = new Command(() => DecreasedTimeHours());
            TimeMinuteDown = new Command(() => DecreasedTimeMinutes());
        }

        public string Date
        {
            get => date;
            set
            {
                if (date != value)
                {
                    date = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Time
        {
            get => time;
            set
            {
                if (time != value)
                {
                    time = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DateTime LocalDate
        {
            get => localDate;
            set
            {
                if (localDate != value)
                {
                    localDate = value;
                    RaisePropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisedDateMonth()
        {
            localDate = localDate.AddMonths(1);
            Date = localDate.ToString("MM/dd/yy");
        }

        private void DecreasedDateMonth()
        {
            localDate = localDate.AddMonths(-1);
            Date = localDate.ToString("MM/dd/yy");
        }

        private void RaisedDateDays()
        {
            localDate = localDate.AddDays(1);
            Date = localDate.ToString("MM/dd/yy");
        }

        private void DecreasedDateDays()
        {
            localDate = localDate.AddDays(-1);
            Date = localDate.ToString("MM/dd/yy");
        }

        private void RaisedDateYears()
        {
            localDate = localDate.AddYears(1);
            Date = localDate.ToString("MM/dd/yy");
        }

        private void DecreasedDateYears()
        {
            localDate = localDate.AddYears(-1);
            Date = localDate.ToString("MM/dd/yy");
        }

        private void RaisedTimeHours()
        {
            localDate = localDate.AddHours(1);
            Time = localDate.ToString("HH:mm");
        }

        private void DecreasedTimeHours()
        {
            localDate = localDate.AddHours(-1);
            Time = localDate.ToString("HH:mm");
        }

        private void RaisedTimeMinutes()
        {
            localDate = localDate.AddMinutes(1);
            Time = localDate.ToString("HH:mm");
        }

        private void DecreasedTimeMinutes()
        {
            localDate = localDate.AddMinutes(-1);
            Time = localDate.ToString("HH:mm");
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
