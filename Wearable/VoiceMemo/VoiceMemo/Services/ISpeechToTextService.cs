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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VoiceMemo.Services
{
    public enum SttState
    {
        //
        // Summary:
        //     Created state.
        Created = 0,
        //
        // Summary:
        //     Ready state.
        Ready = 1,
        //
        // Summary:
        //     Recording state.
        Recording = 2,
        //
        // Summary:
        //     Processing state.
        Processing = 3,
        //
        // Summary:
        //     Unavailable.
        Unavailable = 4
    }

    /// <summary>
    /// The Speech to Text Service
    /// </summary>
    public interface ISpeechToTextService
    {
        /// <summary>
        /// Get the supported languages.
        /// </summary>
        /// <returns>string collection </returns>
        IEnumerable<string> GetInstalledLanguages();
        /// <summary>
        /// The chosen language for Speech-to-Text service
        /// </summary>
        string CurrentSttLanguage { get; set; }
        /// <summary>
        /// The state of Speech-to-Text service
        /// </summary>
        SttState SttState { get; }
        /// <summary>
        /// Start Speech-to-Text service
        /// It means that it starts speech recognition, converting audio to text
        /// </summary>
        void StartStt();
        /// <summary>
        /// Cancel speech recognition
        /// </summary>
        void Cancel();
        /// <summary>
        /// Stop speech recognition and get the converted text
        /// </summary>
        /// <returns>text</returns>
        Task<string> StopAndGetText();
        /// <summary>
        /// Dispose Speech-to-Text service
        /// </summary>
        void Dispose();

        /// <summary>
        /// Register state callback methods
        /// </summary>
        /// <param name="callback">callback functions</param>
        void RegisterStateCallbacks(Action<Object, SttState, SttState> callback);
    }
}
