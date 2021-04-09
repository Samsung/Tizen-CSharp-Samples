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
using VoiceMemo.ViewModels;

namespace VoiceMemo.Services
{
    /// <summary>
    /// Audio recording state
    /// </summary>
    public enum AudioRecordState
    {
        Init,
        Idle,
        Ready,
        Recording,
        Paused,
    }

    /// <summary>
    /// Interface to use DependencyService
    /// Platform-specific functionality : to record voice
    /// </summary>
    public interface IAudioRecordService
    {
        /// <summary>
        /// Get the state of audio recording
        /// </summary>
        AudioRecordState State { get; }
        /// <summary>
        /// Start recording voice
        /// </summary>
        /// <param name="filepath">file path to store audio file</param>
        /// <param name="sttOn">indicate that Speech-To-Text service is on or not</param>
        /// <returns>bool</returns>
        Task<bool> Start(string filepath, bool sttOn);
        /// <summary>
        /// Pause recording voice
        /// </summary>
        void Pause();
        /// <summary>
        /// Cancel recording voice
        /// </summary>
        void Cancel();
        /// <summary>
        /// Resume recording voice
        /// </summary>
        void Resume();
        /// <summary>
        /// Stop recording and save the voice recording file
        /// </summary>
        /// <returns>file path to save audio</returns>
        string Save();
        /// <summary>
        /// Destroy Audio Record Service
        /// </summary>
        void Destroy();
        /// <summary>
        /// Get voice recording level
        /// </summary>
        /// <returns>volume level</returns>
        double GetRecordingLevel();

        /// <summary>
        /// Register state callback methods
        /// </summary>
        /// <param name="callback">callback functions</param>
        void RegisterStateCallbacks(Action<Object, AudioRecordState, AudioRecordState> callback);

        // The page model class for RecordingPage
        RecordingPageModel ViewModel { get; set; }
    }
}
