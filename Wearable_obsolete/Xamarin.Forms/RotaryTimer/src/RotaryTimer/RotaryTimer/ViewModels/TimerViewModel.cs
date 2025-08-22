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
using RotaryTimer.Interfaces;
using RotaryTimer.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace RotaryTimer.ViewModels
{
    /// <summary>
    /// Provides timer view abstraction.
    /// </summary>
    public class TimerViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Backing field for Hours property.
        /// </summary>
        private int _hours;

        /// <summary>
        /// Backing field for Minutes property.
        /// </summary>
        private int _minutes;

        /// <summary>
        /// Backing field for Seconds property.
        /// </summary>
        private int _seconds;

        /// <summary>
        /// Backing field for Duration property.
        /// </summary>
        private int _duration;

        /// <summary>
        /// Application main model.
        /// </summary>
        private TimerModel _model = TimerModel.Instance;

        /// <summary>
        /// Backing field for IsRunning property.
        /// </summary>
        private bool _isRunning;

        /// <summary>
        /// Backing field for HasEnded property;
        /// </summary>
        private bool _hasEnded;

        #endregion

        #region properties

        /// <summary>
        /// Command stopping the timer.
        /// </summary>
        public ICommand StopTimerCommand { get; }

        /// <summary>
        /// Command switching to timer setting view.
        /// </summary>
        public ICommand SetTimerCommand { get; }

        /// <summary>
        /// Counter for hours. Set in range 0-23.
        /// </summary>
        public int Hours
        {
            get => _hours;
            set => SetProperty(ref _hours, value);
        }

        /// <summary>
        /// Counter for minutes. Set in range 0-59.
        /// </summary>
        public int Minutes
        {
            get => _minutes;
            set => SetProperty(ref _minutes, value);
        }

        /// <summary>
        /// Counter for seconds. Set in range 0-59.
        /// </summary>
        public int Seconds
        {
            get => _seconds;
            set => SetProperty(ref _seconds, value);
        }

        /// <summary>
        /// Duration in milliseconds.
        /// </summary>
        public int Duration
        {
            get => _duration;
            set => SetProperty(ref _duration, value);
        }

        /// <summary>
        /// Timer state indicator.
        /// </summary>
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        /// <summary>
        /// Flag indicating whether timer has ended or not.
        /// </summary>
        public bool HasEnded
        {
            get => _hasEnded;
            set => SetProperty(ref _hasEnded, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor.
        /// Initializes a new instance of the TimerViewModel class.
        /// </summary>
        public TimerViewModel()
        {
            Hours = _model.Hours;
            Minutes = _model.Minutes;
            Seconds = _model.Seconds;

            StopTimerCommand = new Command(ExecuteStopTimerCommand);
            SetTimerCommand = new Command(ExecuteSetTimerCommand);

            _model.TimerCompleted += OnTimerCompleted;

            Duration = (Hours * 3600 + Minutes * 60 + Seconds) * 1000;

            StartTimer();
        }

        /// <summary>
        /// Handles timer completed event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void OnTimerCompleted(object sender, EventArgs e)
        {
            HasEnded = true;

            DetachListener();
            _model.SetInitialTime();

            ShowSettingPage();
        }

        /// <summary>
        /// Timer tick event handler.
        /// Updates current time values.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Timer tick event arguments.</param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            Hours = _model.Hours;
            Minutes = _model.Minutes;
            Seconds = _model.Seconds;
        }

        /// <summary>
        /// Starts timer.
        /// </summary>
        private void StartTimer()
        {
            IsRunning = true;
            HasEnded = false;

            _model.TimerElapsed += OnTimerTick;
            _model.StartTimer();
        }

        /// <summary>
        /// Removes timer tick listener.
        /// </summary>
        private void DetachListener()
        {
            IsRunning = false;

            _model.TimerElapsed -= OnTimerTick;
            _model.TimerCompleted -= OnTimerCompleted;
        }

        /// <summary>
        /// Stops timer.
        /// Navigates to previous page.
        /// </summary>
        public void StopTimer()
        {
            DetachListener();

            _model.Hours = Hours;
            _model.Minutes = Minutes;
            _model.Seconds = Seconds;
            _model.StopTimer();

            ShowSettingPage();
        }

        /// <summary>
        /// Shows settings page.
        /// </summary>
        public void ShowSettingPage()
        {
            DependencyService.Get<IViewNavigation>().ShowPreviousPage();
        }

        /// <summary>
        /// Handles execution of SetTimerCommand.
        /// Stops timer.
        /// Shows timer setting page.
        /// </summary>
        private void ExecuteSetTimerCommand()
        {
            _model.SetInitialTime();

            DetachListener();
            ShowSettingPage();
        }

        /// <summary>
        /// Handles execution of StopTimerCommand.
        /// Stops timer.
        /// </summary>
        private void ExecuteStopTimerCommand()
        {
            StopTimer();
        }

        #endregion
    }
}
