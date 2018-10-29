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
using System.Timers;

namespace RotaryTimer.Models
{
    /// <summary>
    /// Provides time values storage.
    /// This is singleton. Instance is accessible via <see cref="TimerModel.Instance">Instance</see></cref> property.
    /// </summary>
    public sealed class TimerModel
    {
        #region fields

        /// <summary>
        /// System timer instance.
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Main model instance.
        /// </summary>
        private static TimerModel _instance;

        /// <summary>
        /// Running timer indicator.
        /// </summary>
        private bool _isTimerRunning;

        /// <summary>
        /// Timer start date.
        /// </summary>
        private DateTime _startTime;

        /// <summary>
        /// Number of seconds for which the timer is set.
        /// </summary>
        private int _timerSecondsInitial;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked on every timer's tick.
        /// </summary>
        public event EventHandler TimerElapsed;

        /// <summary>
        /// Event invoked when timer finished.
        /// </summary>
        public event EventHandler TimerCompleted;

        /// <summary>
        /// Event invoked when timer is stopped.
        /// </summary>
        public event EventHandler TimerStopped;

        /// <summary>
        /// Hours value.
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Minutes value.
        /// </summary>
        public int Minutes { get; set; }

        /// <summary>
        /// Seconds value.
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// Main model instance.
        /// </summary>
        public static TimerModel Instance
        {
            get => _instance ?? (_instance = new TimerModel());
        }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor.
        /// </summary>
        private TimerModel()
        {
            _timer = new Timer(100);
            _timer.Elapsed += TimerElapsedHandler;
        }

        /// <summary>
        /// Sets time values to initial.
        /// </summary>
        public void SetInitialTime()
        {
            Seconds = _timerSecondsInitial % 60;
            Minutes = (int)Math.Floor((float)_timerSecondsInitial / 60) % 60;
            Hours = (int)Math.Floor((float)_timerSecondsInitial / 3600);
        }

        /// <summary>
        /// Calculates remaining time values.
        /// </summary>
        public void CalculateTimeValues()
        {
            long secondsElapsed = (DateTime.UtcNow.Ticks - _startTime.Ticks) / TimeSpan.TicksPerSecond;
            long secondsRemaining = _timerSecondsInitial - secondsElapsed;

            if (secondsRemaining < 0)
            {
                StopTimer();
                TimerCompleted?.Invoke(this, null);
            }

            Seconds = (int)(secondsRemaining % 60);
            Minutes = (int)Math.Floor((float)secondsRemaining / 60) % 60;
            Hours = (int)Math.Floor((float)secondsRemaining / 3600);
        }

        /// <summary>
        /// Handles timer elapsed event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void TimerElapsedHandler(object sender, EventArgs e)
        {
            CalculateTimeValues();

            if (!_isTimerRunning)
            {
                _timer.Stop();
            }

            TimerElapsed?.Invoke(this, null);
        }

        /// <summary>
        /// Starts timer.
        /// </summary>
        public void StartTimer()
        {
            _startTime = DateTime.UtcNow;
            _timerSecondsInitial = Hours * 3600 + Minutes * 60 + Seconds;

            _timer.Start();

            _isTimerRunning = true;
        }

        /// <summary>
        /// Stops timer.
        /// </summary>
        public void StopTimer()
        {
            _isTimerRunning = false;
            TimerStopped?.Invoke(this, null);
        }

        #endregion
    }
}
