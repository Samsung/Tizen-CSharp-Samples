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
using System.Threading.Tasks;

namespace RecorderSample
{
    /// <summary>
    /// Defines methods and properties that control the media player to play recorded media.
    /// </summary>
    public interface IMediaPlayer
    {
        /// <summary>
        /// Prepares the player with the specified path.
        /// </summary>
        /// <param name="path">The source path.</param>
        /// <returns>A task that represents the asynchronous decode operation.</returns>
        Task Prepare(string path);

        /// <summary>
        /// Starts playback.
        /// </summary>
        void Play();

        /// <summary>
        /// Stops playback.
        /// </summary>
        void Stop();

        /// <summary>
        /// Sets the view.
        /// </summary>
        /// <param name="nativeView">The native for display.</param>
        void SetDisplay(object nativeView);

        /// <summary>
        /// Returns a value indicating the player is playing media.
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// Returns the duration of media.
        /// </summary>
        int Duration { get; }

        /// <summary>
        /// Returns the current playing position.
        /// </summary>
        int Position { get; }

        /// <summary>
        /// Occurs when the player is stopped.
        /// </summary>
        event EventHandler Stopped;
    }
}
