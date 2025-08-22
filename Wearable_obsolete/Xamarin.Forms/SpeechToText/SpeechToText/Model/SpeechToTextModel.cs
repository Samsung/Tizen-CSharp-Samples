//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SpeechToText.Model
{
    /// <summary>
    /// The model class handling speech-to-text logic.
    /// </summary>
    class SpeechToTextModel
    {
        #region fields

        /// <summary>
        /// Key used to store STT language in application state.
        /// </summary>
        private static readonly string STATE_SETTINGS_LANGUAGE_KEY = "language";

        /// <summary>
        /// Key used to store STT language in application state.
        /// </summary>
        private static readonly string STATE_SETTINGS_RECOGNITION_TYPE_KEY = "recognition_type";

        /// <summary>
        /// Key used to store STT silence detection mode in application state.
        /// </summary>
        private static readonly string STATE_SETTINGS_SILENCE_DETECTION_KEY = "silence_detection";

        /// <summary>
        /// Key used to store sounds status (on/off) in application state.
        /// </summary>
        private static readonly string STATE_SETTINGS_SOUND_ON_KEY = "sound_on";

        /// <summary>
        /// Key used to store start sound path for STT client in application state.
        /// </summary>
        private static readonly string STATE_SETTINGS_START_SOUND_KEY = "start_sound";

        /// <summary>
        /// Key used to store end sound path for STT client in application state.
        /// </summary>
        private static readonly string STATE_SETTINGS_END_SOUND_KEY = "end_sound";

        /// <summary>
        /// Private backing field for Language property.
        /// </summary>
        private string _language;

        /// <summary>
        /// Private backing field for RecognitionType property.
        /// </summary>
        private RecognitionType _recognitionType;

        /// <summary>
        /// Private backing field for SilenceDetection property.
        /// </summary>
        private SilenceDetection _silenceDetection;

        /// <summary>
        /// Dictionary holding model state (persistent).
        /// </summary>
        private IDictionary<string, object> _state;

        /// <summary>
        /// An instance of the STT service.
        /// </summary>
        private readonly SpeechToTextApiManager _sttService;

        /// <summary>
        /// Private backing field for SoundOn property.
        /// </summary>
        private bool _soundOn;

        /// <summary>
        /// Private backing field for StartSound property.
        /// </summary>
        private string _startSound;

        /// <summary>
        /// Private backing field for EndSound property.
        /// </summary>
        private string _endSound;

        /// <summary>
        /// A list with service's recognition results.
        /// The last item is updated when the service delivers new partial result.
        /// If service sends final result, the last item is "closed" and new item is added.
        /// The model's final result is a concatenation of all list items.
        /// </summary>
        private List<string> _results = new List<string>();

        /// <summary>
        /// Flag indicating if recognition was stopped.
        /// Stopped recognition cannot be continued (only restarted).
        /// </summary>
        private bool _recognitionStopped;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked when the recognition result was changed.
        /// </summary>
        public event EventHandler<EventArgs> ResultChanged;

        /// <summary>
        /// Event invoked when recognition state was changed (on/off).
        /// </summary>
        public event EventHandler<EventArgs> RecognitionActiveStateChanged;

        /// <summary>
        /// Event invoked when STT service error occurs.
        /// Event arguments contains detailed information about the error.
        /// </summary>
        public event EventHandler<IServiceErrorEventArgs> ServiceError;

        /// <summary>
        /// Event invoked when recognition error occurs.
        /// </summary>
        public event EventHandler<EventArgs> RecognitionError;

        /// <summary>
        /// A collection of languages supported by the speech-to-text model.
        ///
        /// The language is specified as an ISO 3166 alpha-2 two letter country-code
        /// followed by ISO 639-1 for the two-letter language code.
        /// </summary>
        public IEnumerable<string> SupportedLanguages => _sttService.SupportedLanguages;

        /// <summary>
        /// Flag indicating if model is ready for processing speech and changing settings.
        /// </summary>
        public bool Ready => _sttService.Ready;

        /// <summary>
        /// Current STT model language (code).
        /// </summary>
        public string Language
        {
            get => _language;
            set
            {
                _state[STATE_SETTINGS_LANGUAGE_KEY] = value;
                _language = value;
                Application.Current.SavePropertiesAsync();
            }
        }

        /// <summary>
        /// A collection of available recognition types.
        /// </summary>
        public IEnumerable<RecognitionType> SupportedRecognitionTypes =>
            _sttService.SupportedRecognitionTypes;

        /// <summary>
        /// Current STT model recognition type.
        /// </summary>
        public RecognitionType RecognitionType
        {
            get => _recognitionType;
            set
            {
                _state[STATE_SETTINGS_RECOGNITION_TYPE_KEY] = value;
                _recognitionType = value;
                Application.Current.SavePropertiesAsync();
            }
        }

        /// <summary>
        /// Current STT model silence detection mode.
        /// </summary>
        public SilenceDetection SilenceDetection
        {
            get => _silenceDetection;
            set
            {
                _state[STATE_SETTINGS_SILENCE_DETECTION_KEY] = value;
                _silenceDetection = value;
                _sttService.SetSilenceDetection(value);
                Application.Current.SavePropertiesAsync();
            }
        }

        /// <summary>
        /// Flag indicating if model sounds are on.
        /// </summary>
        public bool SoundOn
        {
            get => _soundOn;
            set
            {
                _soundOn = value;

                if (_soundOn)
                {
                    if (_startSound != null)
                    {
                        _sttService.SetStartSound(_startSound);
                    }

                    if (_endSound != null)
                    {
                        _sttService.SetStopSound(_endSound);
                    }
                }
                else
                {
                    _sttService.SetStartSound(null);
                    _sttService.SetStopSound(null);
                }

                _state[STATE_SETTINGS_SOUND_ON_KEY] = _soundOn;
                Application.Current.SavePropertiesAsync();
            }
        }

        /// <summary>
        /// Sound to start recording.
        /// </summary>
        public string StartSound
        {
            get => _startSound;
            set
            {
                _startSound = value;
                _sttService.SetStartSound(SoundOn ? _startSound : null);
                _state[STATE_SETTINGS_START_SOUND_KEY] = _startSound;
                Application.Current.SavePropertiesAsync();
            }
        }

        /// <summary>
        /// Sound to end recording.
        /// </summary>
        public string EndSound
        {
            get => _endSound;
            set
            {
                _endSound = value;
                _sttService.SetStopSound(SoundOn ? _endSound : null);
                _state[STATE_SETTINGS_END_SOUND_KEY] = _endSound;
                Application.Current.SavePropertiesAsync();
            }
        }

        /// <summary>
        /// The recognition result (string).
        /// </summary>
        public string Result => String.Join(" ", _results).Trim();

        /// <summary>
        /// Flag indicating if recognition is active.
        /// </summary>
        public bool RecognitionActive => _sttService.RecognitionActive;

        #endregion

        #region methods

        /// <summary>
        /// The model constructor.
        /// </summary>
        /// <param name="state">Persistent storage used to save state.</param>
        public SpeechToTextModel(IDictionary<string, object> state)
        {
            _state = state;
            _sttService = new SpeechToTextApiManager();
            _sttService.RecognitionResult += SttServiceOnRecognitionResult;
            _sttService.RecognitionActiveStateChanged += SttServiceOnRecognitionActiveStateChanged;
            _sttService.RecognitionError += SttServiceOnRecognitionError;
            _sttService.ServiceError += SttServiceOnServiceError;
        }

        /// <summary>
        /// Returns true if all required privileges are granted, false otherwise.
        /// </summary>
        /// <returns>Task with check result.</returns>
        public async Task<bool> CheckPrivileges()
        {
            return await _sttService.CheckPrivileges();
        }

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <returns>Initialization task.</returns>
        public async Task Init()
        {
            await _sttService.Init();

            RestoreState();
        }

        /// <summary>
        /// Handles STT service error event.
        /// Invokes own (class) similar event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="serviceErrorEventArgs">Event arguments.</param>
        private void SttServiceOnServiceError(object sender, IServiceErrorEventArgs serviceErrorEventArgs)
        {
            ServiceError?.Invoke(this, serviceErrorEventArgs);
        }

        /// <summary>
        /// Handles recognition error event.
        /// Invokes own (class) similar event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void SttServiceOnRecognitionError(object sender, EventArgs eventArgs)
        {
            RecognitionError?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Restores state of the model (settings).
        /// </summary>
        private void RestoreState()
        {
            Language = _state.ContainsKey(STATE_SETTINGS_LANGUAGE_KEY) ?
                (string)_state[STATE_SETTINGS_LANGUAGE_KEY] : _sttService.DefaultLanguage;

            RecognitionType = _state.ContainsKey(STATE_SETTINGS_RECOGNITION_TYPE_KEY)
                ? (RecognitionType)_state[STATE_SETTINGS_RECOGNITION_TYPE_KEY]
                : _sttService.SupportedRecognitionTypes.FirstOrDefault();

            SilenceDetection = _state.ContainsKey(STATE_SETTINGS_SILENCE_DETECTION_KEY)
                ? (SilenceDetection)_state[STATE_SETTINGS_SILENCE_DETECTION_KEY]
                : SilenceDetection.Auto;

            StartSound = _state.ContainsKey(STATE_SETTINGS_START_SOUND_KEY)
                ? (string)_state[STATE_SETTINGS_START_SOUND_KEY]
                : null;

            EndSound = _state.ContainsKey(STATE_SETTINGS_END_SOUND_KEY)
                ? (string)_state[STATE_SETTINGS_END_SOUND_KEY]
                : null;

            SoundOn = _state.ContainsKey(STATE_SETTINGS_SOUND_ON_KEY) &&
                (bool)_state[STATE_SETTINGS_SOUND_ON_KEY];
        }

        /// <summary>
        /// Returns a collection of sound files (paths) which can be used
        /// as start and end sounds for STT client.
        /// </summary>
        /// <returns>A collection of sounds (paths).</returns>
        public IEnumerable<string> GetAvailableStartEndSounds()
        {
            return _sttService.GetAvailableStartEndSounds();
        }

        /// <summary>
        /// Handles recognition result event from the service.
        /// Updates recognition result list.
        /// </summary>
        /// <param name="sender">Event sender (service).</param>
        /// <param name="recognitionResultEventArgs">Event arguments.</param>
        private void SttServiceOnRecognitionResult(object sender, IRecognitionResultEventArgs recognitionResultEventArgs)
        {
            if (_results.Count == 0)
            {
                _results.Add(recognitionResultEventArgs.Result.Trim());
            }
            else
            {
                _results[_results.Count - 1] = recognitionResultEventArgs.Result.Trim();
            }

            if (recognitionResultEventArgs.IsFinal)
            {
                if (_results[_results.Count - 1].Length > 0)
                {
                    _results[_results.Count - 1] += ".";
                }

                _results.Add("");
            }

            ResultChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Handles recognition active state change event.
        /// Fires own (class) similar event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void SttServiceOnRecognitionActiveStateChanged(object sender, EventArgs eventArgs)
        {
            RecognitionActiveStateChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Starts the recognition.
        /// </summary>
        public void Start()
        {
            if (_recognitionStopped)
            {
                Clear();
                _recognitionStopped = false;
            }

            _sttService.Start(Language, RecognitionType);
        }

        /// <summary>
        /// Pauses the recognition.
        /// </summary>
        public void Pause()
        {
            _sttService.Stop();
        }

        /// <summary>
        /// Stops the recognition.
        /// Locks recognition start until current result is cleared.
        /// </summary>
        public void Stop()
        {
            _sttService.Stop();
            _recognitionStopped = true;
        }

        /// <summary>
        /// Clears recognition result.
        /// Unlocks recognition start.
        /// </summary>
        public void Clear()
        {
            _results.Clear();
            ResultChanged?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
