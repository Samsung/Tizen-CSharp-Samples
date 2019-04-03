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
    /// Defines methods and properties for playback.
    /// </summary>
    public interface IPlaybackController
    {
        /// <summary>
        /// Starts playback.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops playback.
        /// </summary>
        void Stop();

        /// <summary>
        /// Pauses playback.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes playback.
        /// </summary>
        void Resume();

        /// <summary>
        /// Occurs when the state of the audio playback changes.
        /// </summary>
        event EventHandler<StateChangedEventArgs> StateChanged;
    }
}
