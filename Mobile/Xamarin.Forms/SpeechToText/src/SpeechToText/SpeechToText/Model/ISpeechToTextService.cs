/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeechToText.Model
{
    /// <summary>
    /// Interface of speech-to-text service.
    /// </summary>
    public interface ISpeechToTextService
    {
        #region properties

        /// <summary>
        /// Event invoked when the recognition is done (partial or final).
        /// </summary>
        event EventHandler<IRecognitionResultEventArgs> RecognitionResult;

        /// <summary>
        /// Event invoked when recognition state was changed (on/off).
        /// </summary>
        event EventHandler<EventArgs> RecognitionActiveStateChanged;

        /// <summary>
        /// Event invoked when STT service error occurs.
        /// Event arguments contains detailed information about the error.
        /// </summary>
        event EventHandler<IServiceErrorEventArgs> ServiceError;

        /// <summary>
        /// Event invoked when recognition error occurs.
        /// </summary>
        event EventHandler<EventArgs> RecognitionError;

        /// <summary>
        /// Flag indicating if service is ready to be used.
        /// </summary>
        bool Ready { get; }

        /// <summary>
        /// A collection of languages supported by the service.
        ///
        /// The language is specified as an ISO 3166 alpha-2 two letter country-code
        /// followed by ISO 639-1 for the two-letter language code.
        /// </summary>
        IEnumerable<string> SupportedLanguages { get; }

        /// <summary>
        /// Default language used by the service.
        ///
        /// The language is specified as an ISO 3166 alpha-2 two letter country-code
        /// followed by ISO 639-1 for the two-letter language code.
        /// </summary>
        string DefaultLanguage { get; }

        /// <summary>
        /// A collection of available recognition types.
        /// </summary>
        IEnumerable<RecognitionType> SupportedRecognitionTypes { get; }

        /// <summary>
        /// Flag indicating if recognition is active.
        /// If recognition is active, the STT client is recording or processing
        /// the sentence.
        /// </summary>
        bool RecognitionActive { get; }

        #endregion

        #region methods

        /// <summary>
        /// Returns true if all required privileges are granted, false otherwise.
        /// </summary>
        /// <returns>Task with check result.</returns>
        Task<bool> CheckPrivileges();

        /// <summary>
        /// Initializes the service.
        /// Required privileges need to be checked first.
        /// </summary>
        /// <returns>The initialization task.</returns>
        Task Init();

        /// <summary>
        /// Sets the silence detection.
        /// </summary>
        /// <param name="mode">Mode to set (Auto, True, False).</param>
        void SetSilenceDetection(SilenceDetection mode);

        /// <summary>
        /// Sets the sound to start recording. Sound file type should be .wav type.
        /// If null value is specified, the sound is unset.
        /// </summary>
        /// <param name="filePath">File path to set.</param>
        void SetStartSound(string filePath);

        /// <summary>
        /// Sets the sound to stop recording. Sound file type should be .wav type.
        /// If null value is specified, the sound is unset.
        /// </summary>
        /// <param name="filePath">File path to set.</param>
        void SetStopSound(string filePath);

        /// <summary>
        /// Returns a collection of sound files (paths) which can be used
        /// as start and end sounds for STT client.
        /// </summary>
        /// <returns>A collection of sounds (paths).</returns>
        IEnumerable<string> GetAvailableStartEndSounds();

        /// <summary>
        /// Starts the recognition with specified language and recognition type.
        /// </summary>
        /// <param name="language">Language code.</param>
        /// <param name="recognitionType">Recognition type to be used for recognition.</param>
        void Start(string language, RecognitionType recognitionType);

        /// <summary>
        /// Stops the recognition (recording).
        /// If STT client is in processing phase, it will be finished.
        /// </summary>
        void Stop();

        #endregion
    }
}
