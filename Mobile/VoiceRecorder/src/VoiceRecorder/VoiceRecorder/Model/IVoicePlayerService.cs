/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
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

namespace VoiceRecorder.Model
{
    /// <summary>
    /// Player service interface.
    /// </summary>
    public interface IVoicePlayerService
    {
        #region properties

        /// <summary>
        /// Event invoked whenever player error occurred.
        /// </summary>
        event ErrorOccurredDelegate ErrorOccurred;

        /// <summary>
        /// Event invoked when the stream is paused.
        /// </summary>
        event EventHandler VoicePlayerPaused;

        /// <summary>
        /// Event invoked when the stream starts playing.
        /// </summary>
        event EventHandler VoicePlayerStarted;

        /// <summary>
        /// Event invoked when the stream is stopped.
        /// </summary>
        event EventHandler VoicePlayerStopped;

        /// <summary>
        /// Event invoked when the position of the playing stream is changed.
        /// </summary>
        event EventHandler VoiceStreamPositionSought;

        #endregion

        #region methods

        /// <summary>
        /// Initializes the service.
        /// </summary>
        void Init();

        /// <summary>
        /// Returns the position of the playing stream.
        /// The position is given in tenth of the second.
        /// </summary>
        /// <returns>The position of the playing stream.</returns>
        int GetCurrentPlayerPosition();

        /// <summary>
        /// Pauses the stream.
        /// </summary>
        void PauseVoicePlaying();

        /// <summary>
        /// Prepares the player.
        /// </summary>
        /// <param name="pathToRecording">Media source for the player.</param>
        void PrepareVoicePlayer(string pathToRecording);

        /// <summary>
        /// Changes the position of the playing stream.
        /// </summary>
        /// <param name="factor">Decides whether the stream is fast-forwarded or rewinded.</param>
        void SeekVoiceStreamPosition(int factor);

        /// <summary>
        /// Starts playing the stream.
        /// </summary>
        void StartVoicePlaying();

        /// <summary>
        /// Unprepares the player.
        /// </summary>
        void UnprepareVoicePlaying();

        #endregion
    }
}
