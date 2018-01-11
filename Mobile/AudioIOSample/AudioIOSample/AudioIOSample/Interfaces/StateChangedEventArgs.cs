/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

namespace AudioIOSample
{
    /// <summary>
    /// Provides data for the <see cref="IPlaybackController.StateChanged"/> event and
    /// the <see cref="ICaptureController.StateChanged"/> event.
    /// </summary>
    public class StateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the StateChangedEventArgs class.
        /// </summary>
        /// <param name="newState">New state.</param>
        public StateChangedEventArgs(State newState)
        {
            State = newState;
        }

        /// <summary>
        /// Gets the new state.
        /// </summary>
        public State State { get; }
    }
}
