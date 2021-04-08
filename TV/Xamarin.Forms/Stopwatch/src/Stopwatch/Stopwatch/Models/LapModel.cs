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

namespace Stopwatch.Models
{
    /// <summary>
    /// Model class describing stopwatch lap.
    /// It allows to pause, resume and finish the lap.
    /// The lap has own number and provides property which
    /// returns elapsed time.
    /// </summary>
    class LapModel
    {
        #region fields

        /// <summary>
        /// Flag indicating if lap is paused.
        /// </summary>
        private bool _paused;

        /// <summary>
        /// Flag indicating if lap is finished.
        /// Finished lap cannot be resumed.
        /// Private backing field for "Finished" property.
        /// </summary>
        private bool _finished;

        /// <summary>
        /// Lap number (backing field for "Number" property).
        /// </summary>
        private int _number;

        /// <summary>
        /// Time elapsed to last pause.
        /// This value is updated every time lap is paused.
        /// Used to calculate lap elapsed time.
        /// </summary>
        private TimeSpan _elapsedTimeToLastPause;

        /// <summary>
        /// The time of last lap resume (or lap start if lap was never paused).
        /// This value is updated every time lap is resumed.
        /// Used to calculate lap elapsed time.
        /// </summary>
        private DateTime _lastResumeTime;

        #endregion

        #region properties

        /// <summary>
        /// Lap number.
        /// </summary>
        public int Number => _number;

        /// <summary>
        /// Lap elapsed time.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get
            {
                if (_finished || _paused)
                {
                    return _elapsedTimeToLastPause;
                }

                return _elapsedTimeToLastPause.Add(DateTime.Now - _lastResumeTime);
            }
        }

        /// <summary>
        /// Flag indicating if lap is finished.
        /// Finished lap cannot be resumed.
        /// </summary>
        public bool Finished => _finished;

        #endregion

        #region methods

        /// <summary>
        /// The lap constructor.
        /// Creates lap with specified number and initial time.
        /// It allows to create finished lap.
        /// Created lap is already running (if not finished).
        /// </summary>
        /// <param name="number">Lap number.</param>
        /// <param name="initialTime">Lap initial time.</param>
        /// <param name="finished">Flag indicating if lap is finished (false by default).</param>
        public LapModel(int number, TimeSpan initialTime, bool finished = false)
        {
            _elapsedTimeToLastPause = initialTime;
            _finished = finished;
            _lastResumeTime = DateTime.Now;
            _number = number;
        }

        /// <summary>
        /// Pauses the lap.
        /// </summary>
        public void Pause()
        {
            if (_finished || _paused)
            {
                return;
            }

            _elapsedTimeToLastPause = ElapsedTime;
            _paused = true;
        }

        /// <summary>
        /// Resumes the lap.
        /// </summary>
        public void Resume()
        {
            if (_finished || !_paused)
            {
                return;
            }

            _lastResumeTime = DateTime.Now;
            _paused = false;
        }

        /// <summary>
        /// Makes the lap finished.
        /// </summary>
        public void Finish()
        {
            if (_finished)
            {
                return;
            }

            _elapsedTimeToLastPause = ElapsedTime;
            _finished = true;
        }

        #endregion
    }
}