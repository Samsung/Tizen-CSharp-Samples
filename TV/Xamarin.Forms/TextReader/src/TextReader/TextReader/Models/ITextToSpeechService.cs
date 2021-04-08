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

namespace TextReader.Models
{
    /// <summary>
    /// Interface of text-to-speech service which allows to synthesize a text into speech.
    /// </summary>
    public interface ITextToSpeechService
    {
        #region properties

        /// <summary>
        /// Event fired when the utterance is completed.
        /// </summary>
        event EventHandler UtteranceCompleted;

        /// <summary>
        /// Flag indicating if service is ready to use.
        /// The service requires "Init" method to be called before use.
        /// </summary>
        bool Ready { get; }

        /// <summary>
        /// Currently used text.
        /// If text is changed, the current utterance is stopped immediately
        /// and new one stars then.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Flag indicating if text is read by the TTS engine.
        /// </summary>
        bool Playing { get; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes the service.
        /// Service can be used when the "Ready" property returns "true" value.
        /// </summary>
        void Init();

        /// <summary>
        /// Starts or resumes the current utterance.
        /// </summary>
        void Play();

        /// <summary>
        /// Pauses the current utterance.
        /// </summary>
        void Pause();

        /// <summary>
        /// Stops and clears the current utterance.
        /// </summary>
        void Stop();

        #endregion
    }
}
