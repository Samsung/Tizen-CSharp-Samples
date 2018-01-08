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
using Stopwatch.Models;

namespace Stopwatch.ViewModels
{
    /// <summary>
    /// View model class of the stopwatch lap.
    /// </summary>
    class LapViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Model of the lap.
        /// </summary>
        private LapModel _lap;

        /// <summary>
        /// Elapsed time of the lap.
        /// Backing field for ElapsedTime property.
        /// </summary>
        private TimeSpan _elapsedTime;

        #endregion

        #region properties

        /// <summary>
        /// Lap number.
        /// </summary>
        public int Number => _lap.Number;

        /// <summary>
        /// Elapsed time of the lap.
        /// Updated when Update method is called.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            set => SetProperty(ref _elapsedTime, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// The class constructor.
        /// Creates new lap view model based on specified lap model.
        /// </summary>
        /// <param name="lap">Lap model instance.</param>
        public LapViewModel(LapModel lap)
        {
            _lap = lap;
            _elapsedTime = lap.ElapsedTime;
        }

        /// <summary>
        /// Updates lap elapsed time.
        /// </summary>
        public void Update()
        {
            if (_lap.Finished)
            {
                return;
            }

            ElapsedTime = _lap.ElapsedTime;
        }

        #endregion
    }
}
