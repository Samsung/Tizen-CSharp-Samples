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

using ChronographWatch.Converters;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChronographWatch
{
    /// <summary>
    /// State is present a current status of StopWatch
    /// </summary>
    public enum State
    {
        //started measuring state
        Started,
        //paused measuring state
        Paused,
        //measuring stopped state. watch mode state.
        Stopped
    }

    /// <summary>
    /// ViewModel class of Watch Application
    /// </summary>
    public class WatchViewModel : INotifyPropertyChanged
    {
        DateTime _time;
        TimeSpan _elapsedTime;
        bool _isAmbientMode;
        bool _ambientModeDisabled;
        State _state;
        Mode _mode;

        /// <summary>
        /// Constructor of  WatchViewModel class
        /// </summary>
        public WatchViewModel()
        {
            IsAmbientMode = false;
            Mode = Mode.Watch;
            State = State.Stopped;
        }

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
        /// Gets/Sets AmbientMode
        /// </summary>
        public bool IsAmbientMode
        {
            get
            {
                return _isAmbientMode;
            }

            set
            {
                if (_isAmbientMode == value)
                {
                    return;
                }

                _isAmbientMode = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets/Sets AmbientMode disabled state
        /// </summary>
        public bool AmbientModeDisabled
        {
            get
            {
                return _ambientModeDisabled;
            }

            set
            {
                if (_ambientModeDisabled == value)
                {
                    return;
                }

                _ambientModeDisabled = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets/Sets state of stop watch
        /// </summary>
        public State State
        {
            get
            {
                return _state;
            }

            set
            {
                if (_state == value)
                {
                    return;
                }

                _state = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets/Sets mode of chronograph watch.
        /// Mode value is 'Watch' or 'Chronograph'
        /// </summary>
        public Mode Mode
        {
            get
            {
                return _mode;
            }

            set
            {
                if (_mode == value)
                {
                    return;
                }

                _mode = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets/Sets elapsed time of stop watch
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                if (_elapsedTime == value)
                {
                    return;
                }

                _elapsedTime = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets stop watch second hand rotation degree
        /// </summary>
        public double StopWatchSecRotation
        {
            get => _elapsedTime.Seconds * 6 + (_elapsedTime.Milliseconds / 1000.0  * 6.0);
        }

        /// <summary>
        /// Gets stop watch 30 minute hand rotation degree
        /// </summary>
        public double StopWatchMin30Rotation
        {
            get => _elapsedTime.Minutes * 12 + _elapsedTime.Seconds / 5.0;
        }

        /// <summary>
        /// Gets stop watch 12 hour hand rotation degree
        /// </summary>
        public double StopWatchHr12Rotation
        {
            get => (_elapsedTime.Hours >= 12 ? _elapsedTime.Hours - 12 : _elapsedTime.Hours) * 30 + _elapsedTime.Minutes / 2.0;
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
