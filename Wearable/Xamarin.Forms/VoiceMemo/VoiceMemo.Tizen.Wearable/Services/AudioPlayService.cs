/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Threading.Tasks;
using Tizen.Multimedia;
using VoiceMemo.Services;
using VoiceMemo.Tizen.Wearable.Services;
using VoiceMemo.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioPlayService))]

namespace VoiceMemo.Tizen.Wearable.Services
{
    /// <summary>
    /// AudioPlayerService
    /// The main role is playing the recorded voice memo.
    /// Use Tizen.Multimedia.Player
    /// </summary>
    class AudioPlayService : IAudioPlayService
    {
        // Constructor
        public AudioPlayService()
        {
            if (audioVolume == null)
            {
                audioVolume = AudioManager.VolumeController;
                _state = AudioPlayState.Init;
            }

            // Create a player
            if (player == null)
            {
                player = new Player();
                if (player.State == PlayerState.Idle)
                {
                    _state = StateConvert(player.State);
                    MessagingCenter.Send<IAudioPlayService, AudioPlayState>(this, MessageKeys.PlayerStateChanged, StateConvert(player.State));
                }

                player.Volume = (float)Volume / GetMaxVolume();
                player.PlaybackCompleted += Player_PlaybackCompleted;
                player.PlaybackInterrupted += Player_PlaybackInterrupted;
                player.ErrorOccurred += Player_ErrorOccurred;
            }

            if (player.State == PlayerState.Ready)
            {
                _state = StateConvert(player.State);
                MessagingCenter.Send<IAudioPlayService, AudioPlayState>(this, MessageKeys.PlayerStateChanged, StateConvert(player.State));
            }
        }

        /// <summary>
        /// Initialize Player
        /// When PlaybackPage is shown, 
        /// </summary>
        /// <param name="path">audio file path to play</param>
        /// <returns>Task</returns>
        public async Task Init(string path)
        {
            player.SetSource(new MediaUriSource(path));
            await player.PrepareAsync();
            Start();
        }

        AudioVolume audioVolume;
        Player player;
        AudioPlayState _state;
        /// <summary>
        /// The state of audio play
        /// </summary>
        public AudioPlayState State
        {
            get
            {
                return _state;
            }
        }

        // Indicate that Mute is on or off
        public bool Muted
        {
            get { return player.Muted; }
            set
            {
                if (player.Muted != value)
                {
                    player.Muted = value;
                }
            }
        }
        /// <summary>
        /// Media volume
        /// </summary>
        public int Volume
        {
            get
            {
                return audioVolume.Level[AudioVolumeType.Media];
            }
        }
        /// <summary>
        /// Make volume level up
        /// </summary>
        public void IncreaseVolume()
        {
            // if volume level is already max, ignore it.
            if (audioVolume.Level[AudioVolumeType.Media] == audioVolume.MaxLevel[AudioVolumeType.Media])
            {
                return;
            }

            AudioManager.VolumeController.Level[AudioVolumeType.Media] = AudioManager.VolumeController.Level[AudioVolumeType.Media] + 1;
        }
        /// <summary>
        /// Make volume level down
        /// </summary>
        public void DecreaseVolume()
        {
            // ignore when volume level has already reached zero
            if (audioVolume.Level[AudioVolumeType.Media] == 0)
            {
                return;
            }

            AudioManager.VolumeController.Level[AudioVolumeType.Media] = AudioManager.VolumeController.Level[AudioVolumeType.Media] - 1;
        }
        /// <summary>
        /// Get the maximum volume value
        /// </summary>
        /// <returns>maximum volume value</returns>
        public int GetMaxVolume()
        {
            return audioVolume.MaxLevel[AudioVolumeType.Media];
        }
        /// <summary>
        /// Register callback to get notified when the volume has been changed
        /// </summary>
        public void RegisterVolumeChangedCallback()
        {
            audioVolume.Changed += AudioVolume_Changed;
        }
        // <summary>
        /// Unregister callback to get notified when the volume has been changed
        /// </summary>
        public void UnregisterVolumeChangedCallback()
        {
            audioVolume.Changed -= AudioVolume_Changed;
        }
        /// <summary>
        /// Start to play voice audio file
        /// </summary>
        public void Start()
        {
            if (player.State == PlayerState.Ready || player.State == PlayerState.Paused)
            {
                // Player's state will be "Playing"
                player.Start();
                if (player.State == PlayerState.Playing)
                {
                    _state = StateConvert(player.State);
                    MessagingCenter.Send<IAudioPlayService, AudioPlayState>(this, MessageKeys.PlayerStateChanged, StateConvert(player.State));
                }
            }
        }
        /// <summary>
        /// Pause to play audio file
        /// </summary>
        public void Pause()
        {
            if (player.State == PlayerState.Playing)
            {
                // Player's state will be "Paused"
                player.Pause();
                if (player.State == PlayerState.Paused)
                {
                    _state = StateConvert(player.State);
                    MessagingCenter.Send<IAudioPlayService, AudioPlayState>(this, MessageKeys.PlayerStateChanged, StateConvert(player.State));
                }

                Console.WriteLine(" AudioPlayService.Pause() : ", (player.State != PlayerState.Paused) ? "Failed to Pause()" : "Successfully finish to Pause()");
            }
        }

        /// <summary>
        /// If you want to leave PlaybackPage, you need to 
        /// </summary>
        public void Stop()
        {
            if (player.State == PlayerState.Playing || player.State == PlayerState.Paused)
            {
                // Player's state will be "Ready"
                player.Stop();
                if (player.State == PlayerState.Ready)
                {
                    _state = StateConvert(player.State);
                    MessagingCenter.Send<IAudioPlayService, AudioPlayState>(this, MessageKeys.PlayerStateChanged, StateConvert(player.State));
                }
            }
            // Player's state will be "Idle"
            player.Unprepare();
            if (player.State == PlayerState.Idle)
            {
                _state = StateConvert(player.State);
                MessagingCenter.Send<IAudioPlayService, AudioPlayState>(this, MessageKeys.PlayerStateChanged, StateConvert(player.State));
            }
        }
        /// <summary>
        /// Destroy Audio Play Service
        /// </summary>
        public void Destroy()
        {
            if (player.State == PlayerState.Playing || player.State == PlayerState.Paused)
            {
                Stop();
            }

            player.Dispose();
        }
        /// <summary>
        /// Invoked when audio volume level has been changed
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">VolumeChangedEventArgs</param>
        private void AudioVolume_Changed(object sender, VolumeChangedEventArgs e)
        {
            //MessagingCenter.Send<IPlayService, int>(this, "VOLUME_CHANGE", e.Level);
            VolumeChanged?.Invoke(this, new AudioVolumeChangedEventArgs(e.Level));
        }
        /// <summary>
        /// Invoked when an error occurs at playing
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">PlayerErrorOccurredEventArgs</param>
        private void Player_ErrorOccurred(object sender, PlayerErrorOccurredEventArgs e)
        {
            Console.WriteLine(" Player_ErrorOccurred  :" + e.Error);
        }
        /// <summary>
        /// Invoked when an interruption occurs
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">PlaybackInterruptedEventArgs</param>
        private void Player_PlaybackInterrupted(object sender, PlaybackInterruptedEventArgs e)
        {
            Console.WriteLine(" Player_PlaybackInterrupted  :" + e.Reason);
        }
        /// <summary>
        /// Invoked when playing audio is done
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void Player_PlaybackCompleted(object sender, EventArgs e)
        {
            Stop();
            AudioPlayFinished?.Invoke(this, e);
        }
        /// <summary>
        /// player's state converter
        /// </summary>
        /// <param name="state">PlayerState</param>
        /// <returns>AudioPlayState</returns>
        AudioPlayState StateConvert(PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Idle:
                    return AudioPlayState.Idle;
                case PlayerState.Paused:
                    return AudioPlayState.Paused;
                case PlayerState.Playing:
                    return AudioPlayState.Playing;
                case PlayerState.Preparing:
                    return AudioPlayState.Preparing;
                case PlayerState.Ready:
                    return AudioPlayState.Ready;
                default:
                    return AudioPlayState.Init;
            }
        }
        /// <summary>
        /// Called whenever volume level has been changed
        /// </summary>
        public event EventHandler<AudioVolumeChangedEventArgs> VolumeChanged;
        /// <summary>
        /// Called when playing audio file is done
        /// </summary>
        public event EventHandler AudioPlayFinished;
    }
}
