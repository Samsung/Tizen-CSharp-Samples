/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using Xamarin.Forms;
using RotaryTimer.Interfaces;
using RotaryTimer.Utils;
using RotaryTimer.Models;
using System.Windows.Input;

namespace RotaryTimer.ViewModels
{
    /// <summary>
    /// Provides setting view abstraction.
    /// </summary>
    public class SetTimeViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Maximum hours value.
        /// </summary>
        private const int MAX_HOURS = 23;

        /// <summary>
        /// Maximum minutes value.
        /// </summary>
        private const int MAX_MINUTES = 59;

        /// <summary>
        /// Maximum seconds value.
        /// </summary>
        private const int MAX_SECONDS = 59;

        /// <summary>
        /// Backing field for SettingMode property.
        /// </summary>
        private SettingMode _settingMode;

        /// <summary>
        /// Backing field for Hours property.
        /// </summary>
        private int _hours = 0;

        /// <summary>
        /// Backing field for Minutes property.
        /// </summary>
        private int _minutes = 0;

        /// <summary>
        /// Backing field for Seconds property.
        /// </summary>
        private int _seconds = 0;

        /// <summary>
        /// Backing field for TimerSetting property.
        /// </summary>
        private TimerSetting _timerSetting;

        /// <summary>
        /// Application main model.
        /// </summary>
        private TimerModel _model = TimerModel.Instance;

        /// <summary>
        /// Backing field for IsSet property.
        /// </summary>
        private bool _isSet;

        #endregion

        #region properties

        /// <summary>
        /// Hours value. Set in range 0-23.
        /// </summary>
        public int Hours
        {
            get => _hours;
            set => SetProperty(ref _hours, Math.Max(0, Math.Min(value, MAX_HOURS)));
        }

        /// <summary>
        /// Minutes value. Set in range 0-59.
        /// </summary>
        public int Minutes
        {
            get => _minutes;
            set => SetProperty(ref _minutes, Math.Max(0, Math.Min(value, MAX_MINUTES)));
        }

        /// <summary>
        /// Seconds value. Set in range 0-59.
        /// </summary>
        public int Seconds
        {
            get => _seconds;
            set => SetProperty(ref _seconds, Math.Max(0, Math.Min(value, MAX_SECONDS)));
        }

        /// <summary>
        /// Current setting mode.
        /// </summary>
        public SettingMode SettingMode
        {
            get => _settingMode;
            private set
            {
                SetProperty(ref _settingMode, value);
                UpdateTimerSettings();
            }
        }

        /// <summary>
        /// Command starting the timer.
        /// </summary>
        public ICommand StartTimerCommand { get; }

        /// <summary>
        /// Command changing setting mode.
        /// </summary>
        public ICommand SetModeCommand { get; }

        /// <summary>
        /// Command updating time value.
        /// </summary>
        public ICommand UpdateValueCommand { get; }

        /// <summary>
        /// Command resetting timer.
        /// </summary>
        public ICommand ResetTimerCommand { get; }

        /// <summary>
        /// Command setting values.
        /// </summary>
        public ICommand RetrieveValuesCommand { get; }

        /// <summary>
        /// Current time values and setting mode.
        /// </summary>
        public TimerSetting TimerSetting
        {
            get => _timerSetting;
            set => SetProperty(ref _timerSetting, value);
        }

        /// <summary>
        /// Indicates whether any of the settings is different from zero.
        /// </summary>
        public bool IsSet
        {
            get => _isSet;
            set => SetProperty(ref _isSet, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor.
        /// Initializes a new instance of the SetTimeViewModel class.
        /// </summary>
        public SetTimeViewModel()
        {
            UpdateTimerSettings();

            StartTimerCommand = new Command(ExecuteStartTimerCommand, () => IsSet);
            SetModeCommand = new Command<SettingMode>(ExecuteSetModeCommand);
            UpdateValueCommand = new Command<int>(ExecuteUpdateValueCommand);
            ResetTimerCommand = new Command(ExecuteResetTimerCommand);
            RetrieveValuesCommand = new Command(ExecuteRetrieveValuesCommand);

            _model.TimerStopped += (s, e) =>
            {
                RetrieveValues();
            };
        }

        /// <summary>
        /// Updates timer setting property.
        /// </summary>
        private void UpdateTimerSettings()
        {
            TimerSetting = new TimerSetting(Hours, Minutes, Seconds, SettingMode);
            IsSet = (Hours + Minutes + Seconds) > 0;
        }

        /// <summary>
        /// Retrieves stored values.
        /// </summary>
        private void RetrieveValues()
        {
            Hours = _model.Hours;
            Minutes = _model.Minutes;
            Seconds = _model.Seconds;

            UpdateTimerSettings();
        }

        /// <summary>
        /// Retrieving stored time values.
        /// </summary>
        private void ExecuteRetrieveValuesCommand()
        {
            RetrieveValues();
        }

        /// <summary>
        /// Starting the timer.
        /// </summary>
        private void ExecuteStartTimerCommand()
        {
            _model.Hours = Hours;
            _model.Minutes = Minutes;
            _model.Seconds = Seconds;

            DependencyService.Get<IViewNavigation>().ShowTimerPage();
        }

        /// <summary>
        /// Sets setting mode.
        /// </summary>
        /// <param name="mode">New setting mode.</param>
        private void ExecuteSetModeCommand(SettingMode mode)
        {
            SettingMode = mode;
        }

        /// <summary>
        /// Updates time value.
        /// </summary>
        /// <param name="change">Change value.</param>
        private void UpdateValue(int change)
        {
            switch (_settingMode)
            {
                case SettingMode.HOURS:
                    Hours += change;
                    break;
                case SettingMode.MINUTES:
                    Minutes += change;
                    break;
                case SettingMode.SECONDS:
                    Seconds += change;
                    break;
            }

            UpdateTimerSettings();
        }

        /// <summary>
        /// Updates value.
        /// </summary>
        /// <param name="change">Change value.</param>
        private void ExecuteUpdateValueCommand(int change)
        {
            UpdateValue(change);
        }

        /// <summary>
        /// Resets time values.
        /// </summary>
        private void ExecuteResetTimerCommand()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;

            UpdateTimerSettings();
        }

        #endregion
    }
}
