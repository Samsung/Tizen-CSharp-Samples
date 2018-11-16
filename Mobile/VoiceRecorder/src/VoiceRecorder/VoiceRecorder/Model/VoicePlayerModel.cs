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
using System.Collections.ObjectModel;
using VoiceRecorder.Utils;
using Xamarin.Forms;

namespace VoiceRecorder.Model
{
    /// <summary>
    /// VoicePlayerModel class.
    /// Provides methods to control the media player.
    /// </summary>
    public class VoicePlayerModel
    {
        #region fields

        /// <summary>
        /// Instance of the VoicePlayerService class.
        /// </summary>
        private IVoicePlayerService _service;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked when the selected file is deleted.
        /// </summary>
        public event EventHandler ItemDeleted;

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
        /// Initializes VoicePlayerModel class instance.
        /// </summary>
        public VoicePlayerModel()
        {
            _service = DependencyService.Get<IVoicePlayerService>();
            _service.VoiceStreamPositionSought += VoiceStreamPositionSoughtEventHandler;
            _service.VoicePlayerStarted += VoicePlayerStartedEventHandler;
            _service.VoicePlayerPaused += VoicePlayerPausedEventHandler;
            _service.VoicePlayerStopped += VoicePlayerStoppedEventHandler;
            _service.ErrorOccurred += ErrorOccurredEventHandler;
        }

        /// <summary>
        /// Initializes the service.
        /// </summary>
        public void Init()
        {
            _service.Init();
        }

        /// <summary>
        /// Handles "ErrorOccurred" of the IVoicePlayerService object.
        /// Invokes "ErrorOccurred" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoicePlayerService class.</param>
        /// <param name="errorMessage">Error message.</param>
        private void ErrorOccurredEventHandler(object sender, string errorMessage)
        {
            ErrorOccurred?.Invoke(this, errorMessage);
        }

        /// <summary>
        /// Returns the position of the playing stream.
        /// The position is given in tenth of the second.
        /// </summary>
        /// <returns>The position of the playing stream.</returns>
        public int GetCurrentPlayerPosition()
        {
            return _service.GetCurrentPlayerPosition();
        }

        /// <summary>
        /// Pauses the stream.
        /// </summary>
        public void PauseVoicePlaying()
        {
            _service.PauseVoicePlaying();
        }

        /// <summary>
        /// Prepares the player.
        /// </summary>
        /// <param name="pathToRecording">Media source for the player.</param>
        public void PrepareVoicePlayer(string pathToRecording)
        {
            _service.PrepareVoicePlayer(pathToRecording);
        }

        /// <summary>
        /// Changes the position of the playing stream.
        /// </summary>
        /// <param name="factor">Decides whether the stream is fast-forwarded or rewinded.</param>
        public void SeekVoiceStreamPosition(int factor)
        {
            _service.SeekVoiceStreamPosition(factor);
        }

        /// <summary>
        /// Starts playing the stream.
        /// </summary>
        public void StartVoicePlaying()
        {
            _service.StartVoicePlaying();
        }

        /// <summary>
        /// Unprepares the player.
        /// </summary>
        public void UnprepareVoicePlaying()
        {
            _service.UnprepareVoicePlaying();
        }

        /// <summary>
        /// Handles "ItemDeleted" of the IVoicePlayerService object.
        /// Invokes "ItemDeleted" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoicePlayerService class.</param>
        /// <param name="e">Contains event data.</param>
        private void ItemDeletedEventHandler(object sender, EventArgs e)
        {
            ItemDeleted?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Handles "VoicePlayerPaused" of the IVoicePlayerService object.
        /// Invokes "VoicePlayerPaused" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoicePlayerService class.</param>
        /// <param name="e">Contains event data.</param>
        private void VoicePlayerPausedEventHandler(object sender, EventArgs e)
        {
            VoicePlayerPaused?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Handles "VoicePlayerStarted" of the IVoicePlayerService object.
        /// Invokes "VoicePlayerStarted" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoicePlayerService class.</param>
        /// <param name="e">Contains event data.</param>
        private void VoicePlayerStartedEventHandler(object sender, EventArgs e)
        {
            VoicePlayerStarted?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Handles "VoicePlayerStopped" of the IVoicePlayerService object.
        /// Invokes "VoicePlayerStopped" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoicePlayerService class.</param>
        /// <param name="e">Contains event data.</param>
        private void VoicePlayerStoppedEventHandler(object sender, EventArgs e)
        {
            VoicePlayerStopped?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Handles "VoiceStreamPositionSought" of the IVoicePlayerService object.
        /// Invokes "VoiceStreamPositionSought" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoicePlayerService class.</param>
        /// <param name="e">Contains event data.</param>
        private void VoiceStreamPositionSoughtEventHandler(object sender, EventArgs e)
        {
            VoiceStreamPositionSought?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
