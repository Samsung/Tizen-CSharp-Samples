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
using System.Collections.Generic;
using Tizen.Wearable.CircularUI.Forms.Renderer.Watchface;

namespace AmbientWatch
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        public ClockViewModel(FormsWatchface p)
        {
            AmbientModeDisabled = true;
        }

        DateTime _Time;
        double _Hours, _Minutes, _Seconds;
        bool _AmbientModeDisabled;

        /// <summary>
        /// Hours for an hour hand
        /// </summary>
        public double Hours
        {
            get
            {
                return _Hours;
            }

            set
            {
                SetProperty(ref _Hours, value, "Hours");
            }
        }

        /// <summary>
        /// Minutes for a minute hand
        /// </summary>
        public double Minutes
        {
            get
            {
                return _Minutes;
            }

            set
            {
                SetProperty(ref _Minutes, value, "Minutes");
            }
        }

        /// <summary>
        /// Seconds for a second hand
        /// </summary>
        public double Seconds
        {
            get
            {
                return _Seconds;
            }

            set
            {
                SetProperty(ref _Seconds, value, "Seconds");
            }
        }

        /// <summary>
        /// Check whether a device is in ambient mode or not
        /// According to it, second hand will be shown or not
        /// </summary>
        public bool AmbientModeDisabled
        {
            get
            {
                return _AmbientModeDisabled;
            }

            set
            {
                SetProperty(ref _AmbientModeDisabled, value, "AmbientModeDisabled");
            }
        }
        
        /// <summary>
        /// Time
        /// </summary>
        public DateTime Time
        {
            get => _Time;
            set
            {
                if (_Time == value)
                {
                    return;
                }

                Hours = 30 * (_Time.Hour % 12) + 0.5f * _Time.Minute;
                Minutes = 6 * _Time.Minute + 0.1f * _Time.Second;
                Seconds = 6 * _Time.Second + (0.006f * _Time.Millisecond);
                SetProperty(ref _Time, value, "Time");
            }
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
