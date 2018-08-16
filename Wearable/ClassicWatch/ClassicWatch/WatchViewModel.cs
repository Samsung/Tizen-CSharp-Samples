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
using System.Runtime.CompilerServices;

namespace ClassicWatch
{
    /// <summary>
    /// ViewModel class of Watch Application
    /// </summary>
    public class WatchViewModel : INotifyPropertyChanged
    {
        DateTime _time;

        /// <summary>
        /// Gets/Sets current time
        /// </summary>
        public DateTime Time
        {
            get => _time;
            set
            {
                if (_time == value)
                {
                    return;
                }

                _time = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets hour hand rotation degree
        /// </summary>
        public double HourRotation
        {
            get => (_time.Hour >= 12 ? _time.Hour - 12 : _time.Hour) * 30 + _time.Minute / 2.0;
        }

        /// <summary>
        /// Gets minute hand rotation degree
        /// </summary>
        public double MinuteRotation
        {
            get => _time.Minute * 6 + _time.Second / 10.0 ;
        }

        /// <summary>
        /// Gets second hand rotation degree
        /// </summary>
        public double SecondRotation
        {
            get => _time.Second * 6;
        }


        /// <summary>
        /// Gets month hand rotation degree
        /// </summary>
        public double MonthRotation
        {
            get => _time.Month * 30;
        }

        /// <summary>
        /// Gets day hand rotation degree
        /// </summary>
        public double DayRotation
        {
            get => ((_time.DayOfWeek - DayOfWeek.Sunday) * (360 / 7));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called to notify that a change of property happened
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
