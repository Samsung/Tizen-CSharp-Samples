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
using System.Linq;
using System.Threading.Tasks;
using MusicPlayer.Models;
using MusicPlayer.Tizen.TV.Services;
using Tizen.Multimedia;

[assembly: Xamarin.Forms.Dependency(typeof(PlayerService))]
namespace MusicPlayer.Tizen.TV.Services
{
    /// <summary>
    /// The service class handling loading and playback of audio file.
    /// </summary>
    class PlayerService : IPlayerService
    {
        #region fields

        /// <summary>
        /// An instance of TizenFX API player.
        /// Handles actual playback operations.
        /// </summary>
        private Player _player;

        /// <summary>
        /// Snapshot of the playback state.
        /// Used to determine if state was changed since last check (change event invoking).
        /// </summary>
        private bool _lastPlayState = false;

        /// <summary>
        /// Event invoked when playback state was changed.
        /// Current state of playback is provided by "Playing" property.
        /// </summary>
        public event EventHandler PlayStateChanged;

        /// <summary>
        /// Event invoked when playback was completed.
        /// </summary>
        public event EventHandler PlaybackCompleted;

        #endregion

        #region properties

        /// <summary>
        /// Indicates if there is ongoing playback.
        /// </summary>
        public bool Playing => _lastPlayState;

        /// <summary>
        /// Current playback position (in milliseconds).
        /// In case of no loaded file, 0 value is returned.
        /// </summary>
        public int PlaybackPosition
        {
            get
            {
                if (_player.State != PlayerState.Idle && _player.State != PlayerState.Preparing)
                {
                    return _player.GetPlayPosition();
                }

                return 0;
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes a new instance of the service class.
        /// </summary>
        public PlayerService()
        {
            _player = new Player();
            _player.PlaybackCompleted += PlayerOnPlaybackCompleted;
        }

        /// <summary>
        /// Asynchronously loads specified audio file (by path) into player.
        /// The playback can be automatically started by setting "play" parameter (false by default).
        /// </summary>
        /// <param name="path">Path to the audio file.</param>
        /// <param name="play">Indicates if playback should be automatically started as soon as file is loaded.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result indicates if operation completes successfully.</returns>
        public async Task<bool> Load(string path, bool play = false)
        {
            try
            {
                if (_player.State == PlayerState.Playing)
                {
                    _player.Stop();
                }

                _player.Unprepare();

                _player.IsLooping = false;

                _player.SetSource(new MediaUriSource("file://" + path));

                await _player.PrepareAsync();
                await _player.SetPlayPositionAsync(0, false);

                if (play)
                {
                    _player.Start();
                }

                CheckPlayStateChange();

                return true;
            }
            catch (Exception)
            {
                CheckPlayStateChange();
                return false;
            }
        }

        /// <summary>
        /// Asynchronously starts/resumes the playback.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task Play()
        {
            return Task.Run(() =>
            {
                _player.Start();
                CheckPlayStateChange();
            });
        }

        /// <summary>
        /// Asynchronously pauses the playback.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public Task Pause()
        {
            return Task.Run(() =>
            {
                _player.Pause();
                CheckPlayStateChange();
            });
        }

        /// <summary>
        /// Asynchronously sets position of the playback (milliseconds).
        /// </summary>
        /// <param name="msec">The value indicating a desired position in milliseconds.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task SeekTo(int msec)
        {
            await _player.SetPlayPositionAsync(msec, false);
            CheckPlayStateChange();
        }

        /// <summary>
        /// Handles playback completion event from internal player (TizenFX API).
        /// Updates playback state and invokes own (class) playback completion event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void PlayerOnPlaybackCompleted(object sender, EventArgs eventArgs)
        {
            CheckPlayStateChange();
            PlaybackCompleted?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Checks if playback state (internal player) has changed.
        /// If so, it updates state snapshot and invokes state change event.
        /// </summary>
        private void CheckPlayStateChange()
        {
            bool playing = _player.State == PlayerState.Playing;
            if (_lastPlayState != playing)
            {
                _lastPlayState = playing;
                PlayStateChanged?.Invoke(this, new EventArgs());
            }
        }

        #endregion
    }
}
