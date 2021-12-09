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

namespace Piano.Models
{
    /// <summary>
    /// Interface containing methods of sound player.
    /// </summary>
    public interface ISound
    {
        #region methods

        /// <summary>
        /// Initializes sound player.
        /// </summary>
        /// <returns>Task with initialization.</returns>
        Task Init();

        /// <summary>
        /// Plays sound with given index.
        /// </summary>
        /// <param name="soundIndex">Sound index.</param>
        /// <returns>
        /// Task with bool result.
        /// Result is true if playing was successful, false otherwise.
        /// </returns>
        Task<bool> Play(int soundIndex);

        #endregion
    }
}