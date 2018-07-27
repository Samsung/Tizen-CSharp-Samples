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

namespace VoiceMemo.Services
{
    public class AudioVolumeChangedEventArgs : EventArgs
    {
        public AudioVolumeChangedEventArgs(int level)
        {
            Level = level;
        }

        /// <summary>
        /// Gets the new volume level.
        /// </summary>
        /// <value>The new volume level.</value>
        /// <since_tizen> 3 </since_tizen>
        public int Level { get; }
    }

    public enum AudioPlayState
    {
        Init,
        Idle,
        Ready,
        Playing,
        Preparing,
        Paused,
    }

    /// <summary>
    /// Interface to use DependencyService
    /// Platform-specific functionality : to play voice audio file
    /// </summary>
    public interface IAudioPlayService
    {
        /// <summary>
        /// The state of audio play
        /// </summary>
        AudioPlayState State { get; }
        /// <summary>
        /// Volume
        /// </summary>
        int Volume { get; }
        /// <summary>
        /// Indicate that mute mode is on or off
        /// </summary>
        bool Muted { get; set; }
        /// <summary>
        /// Get the maximum volume value
        /// </summary>
        /// <returns>maximum volume value</returns>
        int GetMaxVolume();
        /// <summary>
        /// Register callback to get notified when the volume has been changed
        /// </summary>
        void RegisterVolumeChangedCallback();
        /// <summary>
        /// Unregister callback to get notified when the volume has been changed
        /// </summary>
        void UnregisterVolumeChangedCallback();
        /// <summary>
        /// Make volume level up
        /// </summary>
        void IncreaseVolume();
        /// <summary>
        /// Make volume level down
        /// </summary>
        void DecreaseVolume();
        /// <summary>
        /// Set the file path to play and initialize audio play service
        /// </summary>
        /// <param name="path">file path to play</param>
        /// <returns>Task</returns>
        Task Init(string path);
        /// <summary>
        /// Start to play voice audio file
        /// </summary>
        void Start();
        /// <summary>
        /// Pause to play audio file
        /// </summary>
        void Pause();
        /// <summary>
        /// Stop to play audio file
        /// </summary>
        void Stop();
        /// <summary>
        /// Destroy Audio Play Service
        /// </summary>
        void Destroy();
        /// <summary>
        /// Called whenever volume level has been changed
        /// </summary>
        event EventHandler<AudioVolumeChangedEventArgs> VolumeChanged;
        /// <summary>
        /// Called when playing audio file is done
        /// </summary>
        event EventHandler AudioPlayFinished;
    }
}
