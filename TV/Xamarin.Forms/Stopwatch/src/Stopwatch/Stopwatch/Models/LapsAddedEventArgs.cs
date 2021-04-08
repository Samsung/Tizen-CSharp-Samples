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

namespace Stopwatch.Models
{
    /// <summary>
    /// Event arguments class for laps added event.
    /// Provides access to added laps list.
    /// </summary>
    class LapsAddedEventArgs : EventArgs
    {
        #region properties

        /// <summary>
        /// List of added laps (lap models).
        /// </summary>
        public List<LapModel> Laps { get; }

        #endregion

        #region methods

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="laps">List of added laps (lap models).</param>
        public LapsAddedEventArgs(List<LapModel> laps)
        {
            Laps = laps;
        }

        #endregion
    }
}