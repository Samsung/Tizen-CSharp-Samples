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
using System.Text;
using Xamarin.Forms;

namespace MusicPlayer.Models
{
    /// <summary>
    /// Responsible for managing the logic of the player.
    /// </summary>
    public class PlayerModel
    {
        #region fields

        /// <summary>
        /// Step of single seek operation (in milliseconds).
        /// </summary>
        private const int SEEK_STEP = 5000;

        /// <summary>
        /// Instance of music content service.
        /// </summary>
        private IMusicContentService _musicContentService;

        /// <summary>
        /// Instance of player service.
        /// </summary>
        private IPlayerService _player;

        /// <summary>
        /// Current track.
        /// </summary>
        private Track _currentTrack;

        /// <summary>
        /// Event invoked when playback state is changed.
        /// </summary>
        public event EventHandler PlaybackStateChanged;

        /// <summary>
        /// Event invoked when current track is changed.
        /// </summary>
        public event EventHandler CurrentTrackChanged;

        #endregion

        #region properties

        /// <summary>
        /// Collection of loaded tracks.
        /// </summary>
        public List<Track> Tracks { get; private set; }

        /// <summary>
        /// Current track.
        /// </summary>
        public Track CurrentTrack
        {
            get => _currentTrack;
            private set
            {
                _currentTrack = value;
                CurrentTrackChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Indicates if there is ongoing playback.
        /// </summary>
        public bool Playing
        {
            get => _player.Playing;
        }

        /// <summary>
        /// Current playback position (in milliseconds).
        /// In case of no loaded file, 0 value is returned.
        /// </summary>
        public int PlaybackPosition
        {
            get => _player.PlaybackPosition;
        }

        /// <summary>
        /// Progress of the ongoing track as value from 0 to 1.
        /// </summary>
        public double Progress
        {
            get
            {
                if (CurrentTrack.Duration == 0)
                {
                    return 0;
                }

                return (double)PlaybackPosition / (double)CurrentTrack.Duration;
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes class instance.
        /// </summary>
        public PlayerModel()
        {
            CurrentTrack = new Track(string.Empty, string.Empty, string.Empty, null, string.Empty, 0);
            _musicContentService = DependencyService.Get<IMusicContentService>();
            _player = DependencyService.Get<IPlayerService>();
            _player.PlayStateChanged += OnPlaybackStateChanged;
            _player.PlaybackCompleted += OnPlaybackCompleted;
            Tracks = _musicContentService.GetTracksFromDevice();
            if (Tracks.Count > 0)
            {
                CurrentTrack = Tracks[0];
            }

            if (_player.PlaybackPosition == 0)
            {
                _player.Load(CurrentTrack.Path, false);
            }
        }

        /// <summary>
        /// Changes track to previous.
        /// </summary>
        public void Previous()
        {
            if (Tracks.Count == 0)
            {
                return;
            }

            int index = Tracks.FindIndex(x => x.Path == CurrentTrack.Path);
            if (index - 1 < 0)
            {
                index = Tracks.Count - 1;
            }
            else
            {
                index--;
            }

            SetTrack(index, Playing);
        }

        /// <summary>
        /// Seeks ongoing track backward.
        /// </summary>
        public void Rewind()
        {
            _player.SeekTo(_player.PlaybackPosition - SEEK_STEP);
        }

        /// <summary>
        /// Plays or pauses current track.
        /// </summary>
        public void PlayOrPause()
        {
            if (_player.Playing)
            {
                _player.Pause();
            }
            else
            {
                _player.Play();
            }
        }

        /// <summary>
        /// Seeks ongoing track forward.
        /// </summary>
        public void Forward()
        {
            _player.SeekTo(_player.PlaybackPosition + SEEK_STEP);
        }

        /// <summary>
        /// Changes track to next.
        /// </summary>
        public void Next()
        {
            if (Tracks.Count == 0)
            {
                return;
            }

            int index = Tracks.FindIndex(x => x.Path == CurrentTrack.Path);
            if (index + 1 >= Tracks.Count)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            SetTrack(index, Playing);
        }

        /// <summary>
        /// Changes current track to track with certain index in the tracklist.
        /// </summary>
        /// <param name="index">Index in the tracklist.</param>
        /// <param name="play">Indicates if player should start playback.</param>
        public void SetTrack(int index, bool play)
        {
            CurrentTrack = Tracks[index];
            _player.Load(CurrentTrack.Path, play);
        }

        /// <summary>
        /// Event handler for changing the playback state of player.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnPlaybackStateChanged(object sender, EventArgs e)
        {
            PlaybackStateChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Event handler for completing the playback.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnPlaybackCompleted(object sender, EventArgs e)
        {
            Next();
        }

        #endregion
    }
}
