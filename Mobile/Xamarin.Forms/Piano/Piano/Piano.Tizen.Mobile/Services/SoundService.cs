/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Piano.Models;
using System.Threading.Tasks;
using Piano.Tizen.Mobile.Services;
using Tizen.Applications;
using Tizen.Multimedia;

[assembly: Xamarin.Forms.Dependency(typeof(SoundService))]

namespace Piano.Tizen.Mobile.Services
{
    /// <summary>
    /// Service that plays sounds.
    /// </summary>
    public class SoundService : ISound
    {
        #region fields

        /// <summary>
        /// Maximum sound index.
        /// </summary>
        private const int MAX_SOUND_INDEX = 13;

        /// <summary>
        /// Indicates if player is initialized.
        /// </summary>
        private bool _initialized;

        /// <summary>
        /// Contains every sounds.
        /// </summary>
        private Player[] _sounds;

        #endregion

        #region methods

        /// <summary>
        /// Initializes sound player.
        /// </summary>
        /// <returns>Task with initialization.</returns>
        public async Task Init()
        {
            if (_initialized)
            {
                return;
            }

            var resPath = Application.Current.DirectoryInfo.Resource;
            _sounds = new Player[MAX_SOUND_INDEX + 1];

            for (var i = 0; i <= MAX_SOUND_INDEX; i++)
            {
                var player = new Player
                {
                    IsLooping = false
                };

                player.SetSource(new MediaUriSource($"file://{resPath}/sounds/{i}.wav"));

                await player.PrepareAsync();

                _sounds[i] = player;
            }

            _initialized = true;
        }

        /// <summary>
        /// Plays sound with given index.
        /// </summary>
        /// <param name="soundIndex">Sound index.</param>
        /// <returns>
        /// Task with bool result.
        /// Result is true if playing was successful, false otherwise.
        /// </returns>
        public async Task<bool> Play(int soundIndex)
        {
            if (!_initialized || soundIndex < 0 || soundIndex > MAX_SOUND_INDEX)
            {
                return false;
            }

            var player = _sounds[soundIndex];

            if (player.State == PlayerState.Idle)
            {
                return false;
            }

            await player.SetPlayPositionAsync(0, false);

            player.Start();

            return true;
        }

        #endregion
    }
}