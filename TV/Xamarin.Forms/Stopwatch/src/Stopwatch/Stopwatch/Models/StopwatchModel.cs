/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Linq;

namespace Stopwatch.Models
{
    /// <summary>
    /// Model class describing stopwatch mechanism.
    /// Provides basic stopwatch functions (pausing, resuming), as well as laps support.
    /// </summary>
    class StopwatchModel
    {
        #region fields

        /// <summary>
        /// Time elapsed to last pause.
        /// This value is updated every time, the stopwatch is paused.
        /// Used to calculate total elapsed time.
        /// </summary>
        private TimeSpan _elapsedTimeToLastPause;

        /// <summary>
        /// The time of last resume (or start if stopwatch was never paused).
        /// This value is updated every time the stopwatch is resumed.
        /// Used to calculate total elapsed time.
        /// </summary>
        private DateTime _lastResumeTime;

        /// <summary>
        /// Flag indicating if stopwatch was started.
        /// Private backing field for Running property.
        /// </summary>
        private bool _running;

        /// <summary>
        /// Flag indicating if stopwatch is paused.
        /// Private backing field for Paused property.
        /// </summary>
        private bool _paused;

        /// <summary>
        /// A list with all registered laps.
        /// </summary>
        private List<LapModel> _laps;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked when new laps were added.
        /// Event arguments provides list of all added laps.
        /// </summary>
        public event EventHandler<LapsAddedEventArgs> LapsAdded;

        /// <summary>
        /// Stopwatch total elapsed time.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get
            {
                if (!_running)
                {
                    return TimeSpan.Zero;
                }

                if (_paused)
                {
                    return _elapsedTimeToLastPause;
                }

                return _elapsedTimeToLastPause.Add(DateTime.Now - _lastResumeTime);
            }
        }

        /// <summary>
        /// Flag indicating if stopwatch is paused.
        /// </summary>
        public bool Paused => _paused;

        /// <summary>
        /// Flag indicating if stopwatch was started.
        /// </summary>
        public bool Running => _running;

        #endregion

        #region methods

        /// <summary>
        /// The stopwatch constructor.
        /// Creates new not running stopwatch with empty laps list.
        /// </summary>
        public StopwatchModel()
        {
            _running = false;
            _paused = true;
            _laps = new List<LapModel>();
        }

        /// <summary>
        /// Starts/resumes the stopwatch.
        /// </summary>
        public void Start()
        {
            if (_running && !_paused)
            {
                return;
            }

            LapModel lastLap = null;

            if (_laps.Count > 0)
            {
                lastLap = _laps.Last();
            }

            if (!_running)
            {
                _elapsedTimeToLastPause = TimeSpan.Zero;
                _running = true;
            }

            _lastResumeTime = DateTime.Now;
            lastLap?.Resume();

            _paused = false;
        }

        /// <summary>
        /// Pauses the stopwatch.
        /// </summary>
        public void Pause()
        {
            if (!_running || _paused)
            {
                return;
            }

            LapModel lastLap = null;

            if (_laps.Count > 0)
            {
                lastLap = _laps.Last();
            }

            _elapsedTimeToLastPause = ElapsedTime;
            lastLap?.Pause();

            _paused = true;
        }

        /// <summary>
        /// Toggles the stopwatch state (paused, running).
        /// </summary>
        public void ToggleStartPause()
        {
            if (_paused || !_running)
            {
                Start();
            }
            else
            {
                Pause();
            }
        }

        /// <summary>
        /// Resets the stopwatch (state, laps).
        /// </summary>
        public void Reset()
        {
            _running = false;
            _paused = true;
            _laps.Clear();
        }

        /// <summary>
        /// Finishes current lap and creates new one (ongoing).
        /// </summary>
        public void Lap()
        {
            if (!_running || _paused)
            {
                return;
            }

            List<LapModel> added = new List<LapModel>();

            if (_laps.Count == 0)
            {
                LapModel firstLap = new LapModel(1, ElapsedTime, true);
                _laps.Add(firstLap);
                added.Add(firstLap);
            }

            if (!_laps.Last().Finished)
            {
                _laps.Last().Finish();
            }

            LapModel lap = new LapModel(_laps.Count + 1, TimeSpan.Zero);
            _laps.Add(lap);
            added.Add(lap);

            LapsAdded?.Invoke(this, new LapsAddedEventArgs(added));
        }

        #endregion
    }
}
