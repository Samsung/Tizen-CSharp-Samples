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


using SpeechToText.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tizen.Security;
using Tizen.System;
using Log = Tizen.Log;
using MediaContent = Tizen.Content.MediaContent;
using Stt = Tizen.Uix.Stt;

namespace SpeechToText.Model
{

    /// <summary>
    /// Recognition result event arguments class.
    /// Provides actual result (string) and flag indicating
    /// whether result is final or partial only.
    /// </summary>
    class ServiceRecognitionResultEventArgs : IRecognitionResultEventArgs
    {
        #region properties

        /// <summary>
        /// Flag indicating whether result is final or partial.
        /// </summary>
        public bool IsFinal { get; }

        /// <summary>
        /// Recognition result value.
        /// </summary>
        public string Result { get; }

        #endregion

        #region methods

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="isFinal">Flag indicating whether result is final or partial.</param>
        /// <param name="result">Recognition result value.</param>
        public ServiceRecognitionResultEventArgs(bool isFinal, string result)
        {
            IsFinal = isFinal;
            Result = result;
        }

        #endregion
    }

    /// <summary>
    /// Service error event arguments class.
    /// Provides error message.
    /// </summary>
    class ServiceErrorEventArgs : IServiceErrorEventArgs
    {
        #region properties

        /// <summary>
        /// Error value.
        /// </summary>
        public SttError Error { get; }

        #endregion

        #region methods

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="error">Error value.</param>
        public ServiceErrorEventArgs(SttError error)
        {
            Error = error;
        }

        #endregion
    }

    /// <summary>
    /// STT API manager for Tizen platform.
    /// </summary>
    class SpeechToTextApiManager
    {
        #region fields

        /// <summary>
        /// Recorder privilege key.
        /// </summary>
        private const string RECORDER_PRIVILEGE = "http://tizen.org/privilege/recorder";

        /// <summary>
        /// Instance of STT client.
        /// </summary>
        private Stt.SttClient _client;

        /// <summary>
        /// Instance of media content database.
        /// </summary>
        private MediaContent.MediaDatabase _mediaDatabase;

        /// <summary>
        /// The service initialization task.
        /// </summary>
        private TaskCompletionSource<object> _initTask;

        /// <summary>
        /// The check privileges task.
        /// </summary>
        private TaskCompletionSource<bool> _checkPrivilegesTask;

        /// <summary>
        /// Language used for last started recognition.
        /// Used to restart recording after processing phase.
        /// </summary>
        private string _lastUsedLanguage;

        /// <summary>
        /// Recognition type used for last started recognition.
        /// Used to restart recording after processing phase.
        /// </summary>
        private Stt.RecognitionType _lastUsedRecognitionType;

        /// <summary>
        /// Backing field for RecognitionActive property.
        /// </summary>
        private bool _recognitionActive;

        #endregion

        #region properties

        /// <summary>
        /// Event to be invoked when the recognition is done (partial or final).
        /// </summary>
        public event EventHandler<IRecognitionResultEventArgs> RecognitionResult;

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
        /// Flag indicating if service is ready to be used.
        /// </summary>
        public bool Ready
        {
            get
            {
                if (_client == null)
                {
                    return false;
                }

                var state = _client.CurrentState;
                return state != Stt.State.Created && state != Stt.State.Unavailable;
            }
        }

        /// <summary>
        /// A collection of languages supported by the service.
        ///
        /// The language is specified as an ISO 3166 alpha-2 two letter country-code
        /// followed by ISO 639-1 for the two-letter language code.
        /// </summary>
        public IEnumerable<string> SupportedLanguages => Ready ? _client.GetSupportedLanguages() :
            Enumerable.Empty<string>();

        /// <summary>
        /// Default language used by the service.
        ///
        /// The language is specified as an ISO 3166 alpha-2 two letter country-code
        /// followed by ISO 639-1 for the two-letter language code.
        /// </summary>
        public string DefaultLanguage => Ready ? _client.DefaultLanguage : null;

        /// <summary>
        /// A collection of available recognition types.
        /// </summary>
        public IEnumerable<RecognitionType> SupportedRecognitionTypes
        {
            get
            {
                if (!Ready)
                {
                    return Enumerable.Empty<RecognitionType>();
                }

                var result = new List<RecognitionType>();

                foreach (Stt.RecognitionType type in Enum.GetValues(typeof(Stt.RecognitionType)))
                {
                    if (_client.IsRecognitionTypeSupported(type))
                    {
                        result.Add(type.ToPortableRecognitionType());
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Flag indicating if recognition is active.
        /// If recognition is active, the STT client is in recording or processing state.
        /// </summary>
        public bool RecognitionActive
        {
            get => _recognitionActive;
            private set
            {
                if (value == _recognitionActive)
                {
                    return;
                }

                _recognitionActive = value;
                RecognitionActiveStateChanged?.Invoke(this, new EventArgs());
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Handles privilege request response from the privacy privilege manager.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="requestResponseEventArgs">Event arguments.</param>
        private void PrivilegeManagerOnResponseFetched(object sender, RequestResponseEventArgs requestResponseEventArgs)
        {
            if (requestResponseEventArgs.cause == CallCause.Answer)
            {
                _checkPrivilegesTask.SetResult(requestResponseEventArgs.result == RequestResult.AllowForever);
            }
            else
            {
                Log.Error("STT", "Error occurred during requesting   ion");
                _checkPrivilegesTask.SetResult(false);
            }
        }

        /// <summary>
        /// Returns true if all required privileges are granted, false otherwise.
        /// </summary>
        /// <returns>Task with check result.</returns>
        public async Task<bool> CheckPrivileges()
        {
            CheckResult result = PrivacyPrivilegeManager.CheckPermission(RECORDER_PRIVILEGE);
            PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/mediastorage");

            switch (result)
            {
                case CheckResult.Allow:
                    return true;
                case CheckResult.Deny:
                    return false;
                case CheckResult.Ask:
                    PrivacyPrivilegeManager.ResponseContext context = null;
                    PrivacyPrivilegeManager.GetResponseContext(RECORDER_PRIVILEGE)
                        .TryGetTarget(out context);

                    if (context == null)
                    {
                        Log.Error("STT", "Unable to get privilege response context");
                        return false;
                    }

                    _checkPrivilegesTask = new TaskCompletionSource<bool>();

                    context.ResponseFetched += PrivilegeManagerOnResponseFetched;

                    PrivacyPrivilegeManager.RequestPermission(RECORDER_PRIVILEGE);
                    return await _checkPrivilegesTask.Task;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Handles STT client errors.
        /// Invokes similar class event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="errorOccurredEventArgs">Event arguments.</param>
        private void ClientOnErrorOccurred(object sender, Stt.ErrorOccurredEventArgs errorOccurredEventArgs)
        {
            RecognitionActive = _client.CurrentState == Stt.State.Recording ||
                                _client.CurrentState == Stt.State.Processing;
            ServiceError?.Invoke(this, new ServiceErrorEventArgs(
                errorOccurredEventArgs.ErrorValue.ToPortableSttError()));
        }

        /// <summary>
        /// Handles recognition result event from STT client.
        /// Invokes own (class) event with result string and flag indicating
        /// whether result is final or partial.
        /// In case of recognitions error, it invokes recognition error event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="recognitionResultEventArgs">Event arguments.</param>
        private void ClientOnRecognitionResult(object sender,
            Stt.RecognitionResultEventArgs recognitionResultEventArgs)
        {
            if (recognitionResultEventArgs.Result == Stt.ResultEvent.Error)
            {
                RecognitionActive = _client.CurrentState == Stt.State.Recording ||
                                    _client.CurrentState == Stt.State.Processing;
                RecognitionError?.Invoke(this, new EventArgs());

                return;
            }

            RecognitionResult?.Invoke(this,
                new ServiceRecognitionResultEventArgs(
                    recognitionResultEventArgs.Result == Stt.ResultEvent.FinalResult,
                    String.Join(String.Empty, recognitionResultEventArgs.Data)));
        }

        /// <summary>
        /// Initializes media database.
        ///
        /// Creates database instance and initializes connection.
        /// </summary>
        private void InitMediaDatabase()
        {
            _mediaDatabase = new MediaContent.MediaDatabase();
            _mediaDatabase.Connect();

            var storages = StorageManager.Storages.Select(x => x.RootDirectory);

            foreach (var path in storages)
            {
                _mediaDatabase.ScanFolderAsync(path);
            }
        }

        /// <summary>
        /// Handles state change of the STT client.
        ///
        /// Finishes the initialization task if the client is ready.
        /// Restarts recording if state changed from processing to ready.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="stateChangedEventArgs">Event arguments.</param>
        private void ClientOnStateChanged(object sender, Stt.StateChangedEventArgs stateChangedEventArgs)
        {
            if (stateChangedEventArgs.Current != Stt.State.Ready)
            {
                return;
            }

            if (stateChangedEventArgs.Previous == Stt.State.Created && _initTask != null)
            {
                _initTask.SetResult(null);
            }

            if (stateChangedEventArgs.Previous == Stt.State.Processing && RecognitionActive)
            {
                _client.Start(_lastUsedLanguage, _lastUsedRecognitionType);
            }
        }

        /// <summary>
        /// Initializes the service.
        /// </summary>
        /// <returns>The initialization task.</returns>
        public async Task Init()
        {
            _client = new Stt.SttClient();

            _client.StateChanged += ClientOnStateChanged;
            _client.RecognitionResult += ClientOnRecognitionResult;
            _client.ErrorOccurred += ClientOnErrorOccurred;

            InitMediaDatabase();

            _initTask = new TaskCompletionSource<object>();

            _client.Prepare();
            await _initTask.Task;
            _initTask = null;
        }

        /// <summary>
        /// Sets the silence detection.
        /// </summary>
        /// <param name="mode">Mode to set (Auto, True, False).</param>
        public void SetSilenceDetection(SilenceDetection mode)
        {
            _client.SetSilenceDetection(mode.ToNativeSilenceDetection());
        }

        /// <summary>
        /// Sets the sound to start recording. Sound file type should be .wav type.
        /// If null value is specified, the sound is unset.
        /// </summary>
        /// <param name="filePath">File path to set.</param>
        public void SetStartSound(string filePath)
        {
            try
            {
                if (filePath == null)
                {
                    _client.UnsetStartSound();
                }
                else
                {
                    _client.SetStartSound(filePath);
                }
            }
            catch (Exception e)
            {
                Log.Error("STT", "Unable to change start sound: " + e.Message);
            }
        }

        /// <summary>
        /// Sets the sound to stop recording. Sound file type should be .wav type.
        /// If null value is specified, the sound is unset.
        /// </summary>
        /// <param name="filePath">File path to set.</param>
        public void SetStopSound(string filePath)
        {
            try
            {
                if (filePath == null)
                {
                    _client.UnsetStopSound();
                }
                else
                {
                    _client.SetStopSound(filePath);
                }
            }
            catch (Exception e)
            {
                Log.Error("STT", "Unable to change stop sound: " + e.Message);
            }
        }

        /// <summary>
        /// Returns a collection of sound files (paths) which can be used
        /// as start and end sounds for STT client.
        /// </summary>
        /// <returns>A collection of sounds (paths).</returns>
        public IEnumerable<string> GetAvailableStartEndSounds()
        {
            var result = new List<string>();
            try
            {
                var command = new MediaContent.MediaInfoCommand(_mediaDatabase);
                var reader = command.SelectMedia(new MediaContent.SelectArguments()
                {
                    FilterExpression = string.Format(
                        "{0}='audio/wav' OR {0}='audio/x-wav'",
                        MediaContent.MediaInfoColumns.MimeType),
                    SortOrder = MediaContent.MediaInfoColumns.DisplayName + " ASC"
                });

                while (reader.Read())
                {
                    var info = reader.Current as MediaContent.AudioInfo;

                    if (info == null)
                    {
                        continue;
                    }

                    result.Add(info.Path);
                }
            }
            catch (Exception e)
            {
                Log.Debug("STT", e.Message + " " + e.GetType());
            }

            return result;
        }

        /// <summary>
        /// Starts the recognition with specified language and recognition type.
        /// </summary>
        /// <param name="language">Language code.</param>
        /// <param name="recognitionType">Recognition type to be used for recognition.</param>
        public void Start(string language, RecognitionType recognitionType)
        {
            if (_client.CurrentState != Stt.State.Ready)
            {
                return;
            }

            _lastUsedLanguage = language;
            _lastUsedRecognitionType = recognitionType.ToNativeRecognitionType();

            RecognitionActive = true;

            _client.Start(_lastUsedLanguage, _lastUsedRecognitionType);
        }

        /// <summary>
        /// Stops the recognition (recording).
        /// If STT client is in processing state, it will be finished.
        /// </summary>
        public void Stop()
        {
            if (_client.CurrentState == Stt.State.Recording)
            {
                _client.Stop();
            }

            RecognitionActive = false;
        }

        /// <summary>
        /// The service destructor.
        /// </summary>
        ~SpeechToTextApiManager()
        {
            _mediaDatabase.Disconnect();
            _client?.Dispose();
        }

        #endregion
    }
}
