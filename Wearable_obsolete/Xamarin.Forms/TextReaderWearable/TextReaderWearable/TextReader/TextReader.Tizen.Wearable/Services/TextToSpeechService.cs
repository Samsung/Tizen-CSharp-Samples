/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
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
using TextReader.Tizen.Wearable.Services;
using Tizen.Uix.Tts;
using Xamarin.Forms;

[assembly: Dependency(typeof(TextToSpeechService))]

namespace TextReader.Tizen.Wearable.Services
{
    /// <summary>
    /// Tizen text-to-speech service which allows synthesizing text into speech.
    /// </summary>
    public class TextToSpeechService : ITextToSpeechService, IDisposable
    {
        #region consts

        /// <summary>
        /// Default voice language.
        /// </summary>
        private const string DefaultVoiceLanguage = "en_US";

        /// <summary>
        /// Tag for logging errors.
        /// </summary>
        private const string LogTag = "TTS";

        #endregion

        #region fields

        /// <summary>
        /// Instance of TTS client.
        /// </summary>
        private TtsClient _client;

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
        /// Event invoked when the TTS state is changed to ready.
        /// </summary>
        public event EventHandler StateChangedToReady;

        /// <summary>
        /// Event fired when the utterance is completed.
        /// </summary>
        public event EventHandler UtteranceCompleted;

        /// <summary>
        /// Flag indicating if the service is ready to use.
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
        /// If the text is changed, the current utterance is stopped immediately
        /// and then starts the new one.
        /// </summary>
        public string Text
        {
            get => _text;
            set
            {
                if (Ready)
                {
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
        }

        /// <summary>
        /// Flag indicating if the text is being read by the TTS engine.
        /// </summary>
        public bool Playing => _client.CurrentState == State.Playing;

        #endregion

        #region methods

        /// <summary>
        /// Initializes the class instance.
        /// </summary>
        public TextToSpeechService()
        {
        }

        /// <summary>
        /// Handles state changed event of TTS client.
        /// Invokes StateChangedToReady event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="stateChangedEventArgs">Event arguments.</param>
        private void ClientOnStateChanged(object sender, StateChangedEventArgs stateChangedEventArgs)
        {
            if (stateChangedEventArgs.Current == State.Ready)
            {
                StateChangedToReady?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Handles utterance completed event on TTS client.
        /// Invokes UtteranceCompleted event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="utteranceEventArgs">Event arguments.</param>
        private void ClientOnUtteranceCompleted(object sender, UtteranceEventArgs utteranceEventArgs)
        {
            UtteranceCompleted?.Invoke(this, null);
        }

        /// <summary>
        /// Creates instance of the text-to-speech client.
        /// </summary>
        public void Create()
        {
            _client = new TtsClient();

            _client.UtteranceCompleted += ClientOnUtteranceCompleted;
            _client.StateChanged += ClientOnStateChanged;
        }

        /// <summary>
        /// Releases resources used by the text-to-speech client.
        /// </summary>
        public void Dispose()
        {
            _client.UtteranceCompleted -= ClientOnUtteranceCompleted;
            _client.StateChanged -= ClientOnStateChanged;

            _client.Dispose();
        }

        /// <summary>
        /// Initializes the service.
        /// Service can be used when the "Ready" property returns "true" value.
        /// </summary>
        public void Init()
        {
            if (!Ready)
            {
                if (_client.DefaultVoice.Language == DefaultVoiceLanguage)
                {
                    _currentVoice = _client.DefaultVoice;
                }
                else
                {
                    _currentVoice = _client.GetSupportedVoices()
                        .FirstOrDefault(voice => voice.Language == DefaultVoiceLanguage) ?? _client.DefaultVoice;
                }

                _client.Prepare();
            }
        }

        /// <summary>
        /// Starts or resumes the current utterance.
        /// </summary>
        public void Play()
        {
            if (Ready)
            {
                try
                {
                    _client.Play();
                }
                catch (Exception ex)
                {
                    global::Tizen.Log.Error(LogTag, ex.Message);
                }
            }
        }

        /// <summary>
        /// Pauses the current utterance.
        /// </summary>
        public void Pause()
        {
            if (Ready)
            {
                try
                {
                    _client.Pause();
                }
                catch (Exception ex)
                {
                    global::Tizen.Log.Error(LogTag, ex.Message);
                }
            }
        }

        /// <summary>
        /// Stops and clears the current utterance.
        /// </summary>
        public void Stop()
        {
            if (Ready)
            {
                try
                {
                    _client.Stop();
                }
                catch (Exception ex)
                {
                    global::Tizen.Log.Error(LogTag, ex.Message);
                }
            }
        }

        #endregion
    }
}
