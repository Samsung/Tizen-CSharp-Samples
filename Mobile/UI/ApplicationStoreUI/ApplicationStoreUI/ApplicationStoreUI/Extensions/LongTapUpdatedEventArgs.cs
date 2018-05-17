/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Xamarin.Forms;

namespace ApplicationStoreUI.Extensions
{
    /// <summary>
    /// Arguments for the LongTapUpdated event.
    /// </summary>
    public class LongTapUpdatedEventArgs : EventArgs
    {
        public LongTapUpdatedEventArgs(GestureStatus status, double timestamp)
        {
            Status = status;
            TimeStamp = timestamp;
        }

        /// <summary>
        /// Gets the timestamp(millisecond).
        /// </summary>
        public double TimeStamp { get; }

        /// <summary>
        /// Gets the status that indicates whether the gesture started earlier has finished or got canceled.
        /// </summary>
        public GestureStatus Status { get; }
    }
}
