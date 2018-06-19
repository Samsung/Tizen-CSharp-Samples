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

namespace VoiceMemo.Services
{
    public enum AppState
    {
        Background,
        Foreground,
        Terminated,
        Undefined,
    }

    public interface IDeviceInformation
    {
        /// <summary>
        /// Indicate speech recognition feature is supported or not
        /// </summary>
        bool SpeechRecognitionAvailable { get; }

        int LastStoredFileIndex { get; }
        /// <summary>
        /// Indicate that there is enough storage available to save audio file
        /// </summary>
        bool StorageAvailable { get; }
        /// <summary>
        /// Get the path of database file for this app
        /// </summary>
        /// <param name="filename">Database file name</param>
        /// <returns>The full path  of Database file</returns>
        string GetLocalDBFilePath(string filename);
        /// <summary>
        /// Application's execution state
        /// </summary>
        AppState AppState { get; }
    }
}
