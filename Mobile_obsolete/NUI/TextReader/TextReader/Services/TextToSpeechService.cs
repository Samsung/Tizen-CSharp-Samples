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
using Tizen.Uix.Tts;

namespace TextReader.TizenMobile.Services
{
    /// <summary>
    /// Tizen text-to-speech service which allows to synthesize a text into speech.
    /// </summary>
    class TextToSpeechService 
    {
        #region consts

        /// <summary>
        /// Default voice language.
        /// </summary>
        private const string DEFAULT_VOICE_LANGUAGE = "en_US";

        /// <summary>
        /// Paragraphs That will be processed.
        /// </summary>
        private readonly string[] _paragraphs =
{
            "Welcome to Tizen .NET!",

            "Tizen .NET is an exciting new way to develop applications for the Tizen operating" +
                " system, running on 50 million Samsung devices, including TVs, wearables," +
                " mobile, and many other IoT devices around the world.",

            "The existing Tizen frameworks are either C-based with no advantages of a managed" +
                " runtime, or HTML5-based with fewer features and lower performance than" +
                " the C-based solution.",

            "With Tizen .NET, you can use the C# programming language and the Common Language" +
                " Infrastructure standards and benefit from a managed runtime for faster" +
                " application development, and efficient, secure code execution."
        };

        #endregion

        #region fields

        /// <summary>
        /// Instance of TTS client.
        /// </summary>
        private readonly TtsClient _client;

        /// <summary>
        /// Flag change when paragraph added.
        /// </summary>
        private bool _addedflag;

        /// <summary>
        /// Current Index paragraph.
        /// </summary>
        private int _currentindex;

        /// <summary>
        /// Flag for the current paragraph repeat.
        /// </summary>
        private bool _currentunit;

        /// <summary>
        /// Flag for the all paragraph repeat.
        /// </summary>
        private bool _allunits;

        /// <summary>
        /// Currently synthesized text.
        /// </summary>
        private string _text;

        /// <summary>
        /// Currently used voice instance.
        /// </summary>
        private SupportedVoice _currentVoice;

        /// <summary>
        /// Action to be called in the UI as a function when the paragraph reading starts.
        /// </summary>
        private Action<int> OnStart;

        /// <summary>
        /// Action to be called in the UI as a function when the paragraph reading ends.
        /// </summary>
        private Action<int> OnFinish;

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
        /// Validation for the paragraph index.
        /// </summary>
        /// <param name="paragraphIndex">paragraph index.</param>
        private void Validcounter(ref int paragraphIndex) {
            if (paragraphIndex >= 4) { paragraphIndex = 0; return; }
            if (paragraphIndex < 0) { paragraphIndex = 3; }
        }

        /// <summary>
        /// Reset currentIndex and repeatType .
        /// </summary>
        public void Reset()
        {
            try
            {
                _currentindex = -1;
                _addedflag = false;
                _allunits = false;
                _currentunit = false;
                _client.Stop();             
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Setting current paragraph index.
        /// </summary>
        /// <param name="currentUnit">paragraph current index.</param>
        public void SetCurrentUnit(int currentUnit) 
        {
            _currentindex = currentUnit;
            _addedflag = false;

            if (Playing)
            {
                _client.Stop();
                _client.AddText(_paragraphs[_currentindex],
                                _currentVoice.Language,
                                (int)_currentVoice.VoiceType,
                                _client.GetSpeedRange().Normal);
                _addedflag = true;
                _client.Play();
            }
        }

        /// <summary>
        /// Setting repeat type.
        /// </summary>
        /// <param name="repeatType">repeat type.</param>
        public void SetRepeat(int repeatType) {
            try
            {
                if (repeatType == 3)
                {
                    _currentindex++;
                    Validcounter(ref _currentindex);
                    _addedflag = false;

                    if (Playing)
                    {
                        _client.Stop();
                        _client.AddText(_paragraphs[_currentindex],
                                        _currentVoice.Language,
                                        (int)_currentVoice.VoiceType,
                                        _client.GetSpeedRange().Normal);
                        _addedflag = true;
                        _client.Play();      
                        OnStart(_currentindex);
                    }
                }
                else if (repeatType == 4)
                {
                    _currentindex--;
                    Validcounter(ref _currentindex);
                    _addedflag = false;

                    if (Playing)
                    {
                        _client.Stop();
                        _client.AddText(_paragraphs[_currentindex],
                                        _currentVoice.Language,
                                        (int)_currentVoice.VoiceType,
                                        _client.GetSpeedRange().Normal);
                        _client.Play();
                        _addedflag = true;
                        OnStart(_currentindex);
                    }
                }
                else if (repeatType == 1)
                {
                    _allunits = true;
                }
                else if (repeatType == 2)
                {
                    _currentunit = true;
                }
                else if (repeatType == -2)
                {
                    _currentunit = false;
                }
                else if (repeatType == -1) {
                    _allunits = false; 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// The service constructor.
        /// </summary>
        public TextToSpeechService(Action<int> OnStart, Action<int> OnFinish)
        {
            this.OnStart = OnStart;
            this.OnFinish = OnFinish;
            _client = new TtsClient();
            _client.UtteranceCompleted += ClientOnUtteranceCompleted;
            _client.UtteranceStarted += Client_UtteranceStarted;
            _currentindex = -1;
            _currentunit = false;
            _allunits = false;
            _addedflag = false;
        }

        private void Client_UtteranceStarted(object sender, UtteranceEventArgs e)
        {
            OnStart(_currentindex);
        }

        /// <summary>
        /// Handles utterance completed event on TTS client.
        /// Invokes UtteranceCompleted event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="utteranceEventArgs">Event arguments.</param>
        private void ClientOnUtteranceCompleted(object sender, UtteranceEventArgs utteranceEventArgs)
        {
            UtteranceCompleted?.Invoke(this, new EventArgs());
            try
            {
                _currentindex++;
                _addedflag = false;

                if (_currentunit)
                {
                    _currentindex--;
                }
                else if ((_currentindex > 3 || _currentindex < 0) && _allunits) { _currentindex = 0; }

                if (_currentindex <= 3 && _currentindex >= 0)
                {
                    _client.AddText(_paragraphs[_currentindex],
                                    _currentVoice.Language,
                                    (int)_currentVoice.VoiceType,
                                    _client.GetSpeedRange().Normal);
                    _addedflag = true;
                }
                OnFinish(_currentindex);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

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
            if(_currentindex != -1 &&!_addedflag)
            {
                Validcounter(ref _currentindex);
                _client.AddText( _paragraphs[_currentindex],
                                _currentVoice.Language,
                                (int)_currentVoice.VoiceType,
                                _client.GetSpeedRange().Normal);
                _addedflag = true;
            }
            if(!Ready)
            { 
               return;
            }

            try
            {
                _client.Play();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        /// <summary>
        /// Getting the current paragraph index.
        /// </summary>
        public int GetCurrentParagraph() { return _currentindex; }

        /// <summary>
        /// Pauses the current utterance.
        /// </summary>
        public void Pause()
        {
            if (!Ready)
            {
                return;
            }
            if (Playing)
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

        /// <summary>
        /// Dispose and kill object.
        /// </summary>
        public void Dispose()
        {
            try
            {
                _client.Dispose();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion
    }
}
