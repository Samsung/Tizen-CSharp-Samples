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

using System.Threading.Tasks;
using Xamarin.Forms;

namespace Piano.Models
{
    /// <summary>
    /// Model for sound player.
    /// </summary>
    public static class Sound
    {
        #region fields

        /// <summary>
        /// Sound player context.
        /// </summary>
        private static readonly ISound _sound;

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        static Sound()
        {
            _sound = DependencyService.Get<ISound>();
        }

        /// <summary>
        /// Initializes sound player.
        /// </summary>
        /// <returns>Task with initialization.</returns>
        public static Task Init()
        {
            return _sound.Init();
        }

        /// <summary>
        /// Plays sound with given index.
        /// </summary>
        /// <param name="soundIndex">Sound index.</param>
        /// <returns>
        /// Task with bool result.
        /// Result is true if playing was successful, false otherwise.
        /// </returns>
        public static Task<bool> Play(int soundIndex)
        {
            return _sound.Play(soundIndex);
        }

        #endregion
    }
}