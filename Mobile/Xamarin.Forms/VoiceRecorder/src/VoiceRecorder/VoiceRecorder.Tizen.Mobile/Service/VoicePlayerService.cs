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
using System.Runtime.CompilerServices;
using Tizen.Multimedia;
using VoiceRecorder.Model;
using VoiceRecorder.Tizen.Mobile.Service;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(VoicePlayerService))]

namespace VoiceRecorder.Tizen.Mobile.Service
{
    /// <summary>
    /// VoicePlayerService class.
    /// Allows to control the playback of the file and its removal.
    /// Implements IVoicePlayerService interface.
    /// </summary>
    public class VoicePlayerService : IVoicePlayerService
    {
        #region fields

        /// <summary>
        /// Number the player position has to be changed by (in milliseconds).
        /// </summary>
        private const int LENGTH_TO_SKIP = 5000;

        /// <summary>
        /// Divider to turn milliseconds into tenths of a second.
        /// </summary>
        private const int TO_TENTH_OF_SECONDS_DIVIDER = 100;

        /// <summary>
        /// Player class instance.
        /// </summary>
        private Player _player;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked whenever player error occurred.
        /// </summary>
        public event ErrorOccurredDelegate ErrorOccurred;

        /// <summary>
        /// Event invoked when the stream is paused.
        /// </summary>
        public event EventHandler VoicePlayerPaused;

        /// <summary>
        /// Event invoked when the stream starts playing.
        /// </summary>
        public event EventHandler VoicePlayerStarted;

        /// <summary>
        /// Event invoked when the stream is stopped.
        /// </summary>
        public event EventHandler VoicePlayerStopped;

        /// <summary>
        /// Event invoked when the position of the playing stream is changed.
        /// </summary>
        public event EventHandler VoiceStreamPositionSought;

        #endregion

        #region methods

        /// <summary>
        /// Initializes the Player.
        /// </summary>
        public void Init()
        {
            _player = new Player();
            _player.PlaybackCompleted += OnPlaybackCompleted;
        }

        /// <summary>
        /// Returns the position of the playing stream.
        /// The position is given in tenth of the second.
        /// </summary>
        /// <returns>The position of the playing stream.</returns>
        public int GetCurrentPlayerPosition()
        {
            return _player.GetPlayPosition() / TO_TENTH_OF_SECONDS_DIVIDER;
        }

        /// <summary>
        /// Pauses the stream.
        /// Invokes "VoicePlayerPaused" to other application's modules.
        /// </summary>
        public void PauseVoicePlaying()
        {
            try
            {
                _player.Pause();
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return;
            }

            VoicePlayerPaused?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Prepares the player.
        /// </summary>
        /// <param name="pathToRecording">Media source for the player.</param>
        public async void PrepareVoicePlayer(string pathToRecording)
        {
            try
            {
                var mediaUriSource = new MediaUriSource(pathToRecording);
                _player.SetSource(mediaUriSource);
                await _player.PrepareAsync();
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }
        }

        /// <summary>
        /// Changes the position of the playing stream.
        /// Invokes "VoiceStreamPositionSought" to other application's modules.
        /// </summary>
        /// <param name="factor">Decides whether the stream is fast-forwarded or rewinded.</param>
        public void SeekVoiceStreamPosition(int factor)
        {
            try
            {
                int nextPosition = _player.GetPlayPosition() + factor * LENGTH_TO_SKIP;

                if (nextPosition >= _player.StreamInfo.GetDuration())
                {
                    OnPlaybackCompleted(this, new EventArgs());
                    return;
                }

                if (nextPosition < 0)
                {
                    nextPosition = 0;
                }

                _player.SetPlayPositionAsync(nextPosition, false);
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return;
            }

            VoiceStreamPositionSought?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Starts playing the stream.
        /// Invokes "VoicePlayerStarted" to other application's modules.
        /// </summary>
        public void StartVoicePlaying()
        {
            try
            {
                _player.Start();
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return;
            }

            VoicePlayerStarted?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Unprepares the player.
        /// Invokes "VoicePlayerStopped" to other application's modules.
        /// </summary>
        public void UnprepareVoicePlaying()
        {
            _player.Unprepare();
            VoicePlayerStopped?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Unprepares the player.
        /// Invokes "VoicePlayerStopped" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the object which invokes event.</param>
        /// <param name="eventArgs">Contains event data.</param>
        private void OnPlaybackCompleted(object sender, EventArgs eventArgs)
        {
            try
            {
                _player.Stop();
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return;
            }

            VoicePlayerStopped?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Notifies user about the error.
        /// Invokes "ErrorOccurred" to other application's modules.
        /// </summary>
        /// <param name="errorMessage">Message of a thrown error.</param>
        private void ErrorHandler(string errorMessage, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Error(Program.LogTag, errorMessage, file, func, line);
            ErrorOccurred?.Invoke(this, errorMessage);
        }

        #endregion
    }
}
