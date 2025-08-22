/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.Models
{
    /// <summary>
    /// Interface of service handling loading and playback of audio file.
    /// </summary>
    public interface IPlayerService
    {
        #region fields

        /// <summary>
        /// Event invoked when playback state was changed.
        /// Current state of playback is provided by "Playing" property.
        /// </summary>
        event EventHandler PlayStateChanged;

        /// <summary>
        /// Event invoked when playback was completed.
        /// </summary>
        event EventHandler PlaybackCompleted;

        #endregion

        #region properties

        /// <summary>
        /// Indicates if there is ongoing playback.
        /// </summary>
        bool Playing { get; }

        /// <summary>
        /// Current playback position (in milliseconds).
        /// In case of no loaded file, 0 value is returned.
        /// </summary>
        int PlaybackPosition { get; }

        #endregion

        #region methods

        /// <summary>
        /// Asynchronously loads specified audio file (by path) into player.
        /// The playback can be automatically started by setting "play" parameter (false by default).
        /// </summary>
        /// <param name="path">Path to the audio file.</param>
        /// <param name="play">Indicates if playback should be automatically started as soon as file is loaded.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result indicates if operation completes successfully.</returns>
        Task<bool> Load(string path, bool play = false);

        /// <summary>
        /// Asynchronously starts/resumes the playback.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Play();

        /// <summary>
        /// Asynchronously pauses the playback.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task Pause();

        /// <summary>
        /// Asynchronously sets position of the playback (milliseconds).
        /// </summary>
        /// <param name="msec">The value indicating a desired position in milliseconds.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SeekTo(int msec);

        #endregion
    }
}