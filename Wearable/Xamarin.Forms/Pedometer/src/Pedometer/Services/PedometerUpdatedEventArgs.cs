/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd. All rights reserved.
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

namespace Pedometer.Services
{
    /// <summary>
    /// Represents arguments of event triggered when pedometer changes data
    /// </summary>
    public class PedometerUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// Calories burned
        /// </summary>
        public int Calories { get; private set; }

        /// <summary>
        /// Steps made
        /// </summary>
        public int Steps { get; private set; }

        /// <summary>
        /// Average speed
        /// </summary>
        public int SpeedAverage { get; private set; }

        /// <summary>
        /// Distance covered
        /// </summary>
        public int Distance { get; private set; }

        /// <summary>
        /// Initializes PedometerUpdatedEventArgs class instance
        /// </summary>
        /// <param name="calories">Calories burned</param>
        /// <param name="steps">Steps made</param>
        /// <param name="speedAverage">Average speed</param>
        /// <param name="distance">Distance covered</param>
        public PedometerUpdatedEventArgs(int calories, int steps, int speedAverage, int distance)
        {
            Calories = calories;
            Steps = steps;
            SpeedAverage = speedAverage;
            Distance = distance;
        }
    }
}