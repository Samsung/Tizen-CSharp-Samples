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

using VoiceMemo.Models;

namespace VoiceMemo.Services
{
    /// <summary>
    /// IMediaContentService interface
    /// </summary>
    public interface IMediaContentService
    {
        /// <summary>
        /// Get Record from media file
        /// </summary>
        /// <param name="FilePath">recorded audio file</param>
        /// <param name="SttOn">stt on/off mode</param>
        /// <returns>Record</returns>
        Record GetMediaInfo(string FilePath, bool SttOn);
        /// <summary>
        /// Delete audio file from internal storage
        /// </summary>
        /// <param name="Path">file path to delete</param>
        /// <returns>true if the file is successfully deleted.</returns>
        bool RemoveMediaFile(string Path);
        void Destroy();
    }
}
