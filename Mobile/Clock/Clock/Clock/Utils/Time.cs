/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

namespace Clock.Utils
{
    /// <summary>
    /// Defines time info (H,M,S)
    /// </summary>
    public struct Time
    {
        /// <summary>
        /// Hour
        /// </summary>
        public int Hour;

        /// <summary>
        /// Minute
        /// </summary>
        public int Min;

        /// <summary>
        /// Second
        /// </summary>
        public int Sec;

        /// <summary>
        /// Time constructor
        /// </summary>
        /// <param name="h">Hour</param>
        /// <param name="m">Minute</param>
        /// <param name="s">Second</param>
        public Time(int h, int m, int s)
        {
            this.Hour = h;
            this.Min = m;
            this.Sec = s;
        }

        // Override the ToString method:
        public override string ToString()
        {
            return (String.Format("({0}:{1}:{2})", Hour, Min, Sec));
        }
    };
}
