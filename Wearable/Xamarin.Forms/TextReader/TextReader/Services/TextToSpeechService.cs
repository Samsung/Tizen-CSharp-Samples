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
using System.Linq;
using TextReader.Models;
using TextReader.TizenWearable.Services;
using Tizen.Uix.Tts;

[assembly: Xamarin.Forms.Dependency(typeof(TextToSpeechService))]
namespace TextReader.TizenWearable.Services
{
    /// <summary>
    /// Tizen text-to-speech service which allows to synthesize a text into speech.
    /// </summary>
    class TextToSpeechService : ITextToSpeechService
    {
        #region consts

        /// <summary>
        /// Default voice language.
        /// </summary>
        private const string DEFAULT_VOICE_LANGUAGE = "en_US";

        #endregion

        #region fields

        /// <summary>
        /// Instance of TTS client.
        /// </summary>
        private readonly TtsClient _client;

        /// <summary>
        /// Currently synthesized text.
        /// </summary>
        private string _text;

        /// <summary>
        /// Currently used voice instance.
        /// </summary>
        private SupportedVoice _currentVoice;

        #endregion

        #region properties

        /// <summary>
        /// Event fired when the utterance is completed.
        /// </summary>
        public event EventHandler UtteranceCompleted;

        /// <summary>
        /// Flag indicating if service is ready to use.
        /// The service requires "Init" method to be called before use.
        /// </summary>
        public bool Ready
        {
            get
            {
                var state = _client.CurrentState;
                return state != State.Created && state != State.Unavailable;
            }
        }

        /// <summary>
        /// Currently used text.
        /// If text is changed, the current utterance is stopped immediately
        /// and new one starts then.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                if (!Ready)
                {
                    return;
                }

                bool playingBeforeChange = Playing;

                _client.Stop();
                _client.AddText(
                    value,
                    _currentVoice.Language,
                    (int)_currentVoice.VoiceType,
                    _client.GetSpeedRange().Normal);

                _text = value;

                if (playingBeforeChange)
                {
                    _client.Play();
                }
            }
        }

        /// <summary>
        /// Flag indicating if text is read by the TTS engine.
        /// </summary>
        public bool Playing => _client.CurrentState == State.Playing;

        #endregion

        #region methods

        /// <summary>
        /// The service constructor.
        /// </summary>
        public TextToSpeechService()
        {
            _client = new TtsClient();
            _client.UtteranceCompleted += ClientOnUtteranceCompleted;
        }

        /// <summary>
        /// Handles utterance completed event on TTS client.
        /// Invokes UtteranceCompleted event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="utteranceEventArgs">Event arguments.</param>
        private void ClientOnUtteranceCompleted(object sender,
            UtteranceEventArgs utteranceEventArgs)
        {
            UtteranceCompleted?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Initializes the service.
        /// Service can be used when the "Ready" property returns "true" value.
        /// </summary>
        public void Init()
        {
            if (Ready)
            {
                return;
            }

            if (_client.DefaultVoice.Language == DEFAULT_VOICE_LANGUAGE)
            {
                _currentVoice = _client.DefaultVoice;
            }
            else
            {
                _currentVoice = _client.GetSupportedVoices().
                    FirstOrDefault(voice => voice.Language == DEFAULT_VOICE_LANGUAGE) ??
                    _client.DefaultVoice;
            }

            _client.Prepare();
        }

        /// <summary>
        /// Starts or resumes the current utterance.
        /// </summary>
        public void Play()
        {
            if (!Ready)
            {
                return;
            }

            _client.Play();
        }

        /// <summary>
        /// Pauses the current utterance.
        /// </summary>
        public void Pause()
        {
            if (!Ready)
            {
                return;
            }

            _client.Pause();
        }

        /// <summary>
        /// Stops and clears the current utterance.
        /// </summary>
        public void Stop()
        {
            if (!Ready)
            {
                return;
            }

            _client.Stop();
        }

        #endregion
    }
}
