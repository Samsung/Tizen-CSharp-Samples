using System;
using System.Collections.Generic;
using Tizen.Uix.Stt;
using Tizen.Security;
using System.Linq;
using Tizen.Applications;
using System.Threading;
using System.Threading.Tasks;
using ElmSharp;
using Tizen.NUI.Components;

namespace SpeechToText.Controllers
{
    public class SttController
    {
        private SttClient _sttClient;
        private string _language = null;
        private State _state;
        private Action<string> _onResultChange;
        private Action _onRecordingState;
        private Action _onReadyState;
        private Action _onCreateState;
        private Action _onProcessingState;
        private Action<string, string, bool> _onErrorAlert;
        private RecognitionType _recognitionType;
        private SilenceDetection _silenceDetection = SilenceDetection.Auto;
        private bool _sounds = false;
        private string _endSound = null;
        private string _startSound = null;

        /// <summary>
        /// Current STT model state (created, ready, recording, processing).
        /// </summary>
        public State State
        {
            get => _state;
        }

        /// <summary>
        /// Current STT model recognition type.
        /// </summary>
        public RecognitionType RecognitionType
        {
            get => _recognitionType;
            set => _recognitionType = value;
        }

        /// <summary>
        /// Current STT model silence detection mode.
        /// </summary>
        public SilenceDetection SilenceDetection
        {
            get => _silenceDetection;
            set
            {
                _silenceDetection = value;
                SetSilenceDetection(_silenceDetection);
            }
        }

        /// <summary>
        /// Flag indicating if model sounds are on.
        /// </summary>
        public bool Sounds
        {
            get => _sounds;
            set
            {
                _sounds = value;
            }
        }

        /// <summary>
        /// Sound path to start recording.
        /// </summary>
        public string StartSound
        {
            get => _startSound;
            set
            {
                _startSound = value;
            }
        }

        /// <summary>
        /// Sound path to end recording.
        /// </summary>
        public string EndSound
        {
            get => _endSound;
            set
            {
                _endSound = value;
            }
        }

        public Action OnProcessingState
        {
            get => _onProcessingState;
            set
            {
                _onProcessingState = value;
            }
        }

        public Action<string> OnResultChange
        {
            set => _onResultChange = value;
        }

        public Action<string, string, bool> OnErrorAlert
        {
            set => _onErrorAlert = value;
        }

        public Action OnReadyState
        {
            set => _onReadyState = value;
        }

        public Action OnCreateState
        {
            set => _onCreateState = value;
        }

        public Action OnRecordingState
        {
            set => _onRecordingState = value;
        }

        /// <summary>
        /// STT model
        /// </summary>
        private SttClient SttClient
        {
            get
            {
                if (_sttClient == null)
                    CreateSttClient();
                return _sttClient;
            }
        }

        /// <summary>
        /// Current STT model language (code).
        /// </summary>
        public string Language
        {
            get => _language;
            set => _language = value;
        }

        public SttController()
        {
            //Initializing the Language to the first on the list
            CreateSttClient();
        }

        /// <summary>
        /// Event when state changes
        /// </summary>
        void SttStateChanged(object sender, StateChangedEventArgs e)
        {
            _state = e.Current;
            switch (_state)
            {
                case State.Unavailable:
                    _onCreateState();
                    break;
                case State.Created:
                    _onCreateState();
                    break;
                case State.Ready:
                    if (Sounds && EndSound != null)
                    {
                        SetStopSound(EndSound);
                    }
                    _onReadyState();
                    _onResultChange(null);
                    break;
                case State.Recording:
                    _onRecordingState();
                    break;
                case State.Processing:
                    _onProcessingState();
                    break;
                default:
                    _onCreateState();
                    break;
            }
        }

        /// Event handler
        void SttEngineChanged(object sender, EngineChangedEventArgs args)
        {
            try
            {
                Language = SttClient.GetSupportedLanguages().FirstOrDefault("");
            }
            catch (Exception e)
            {
                _onErrorAlert("Note", "The engine has been changed, you have to restart the application in order to use it.", true);
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Event to recieve the result when it is done.
        /// </summary>
        private void SttRecognitionResult(object sender, RecognitionResultEventArgs e)
        {
            string result = "";
            foreach (string data in e.Data)
            {
                result += data;
            }
            string trimmedResult = result.Trim();
            _onResultChange(trimmedResult);
        }

        /// <summary>
        /// Initializes and prepares the model.
        /// </summary>
        public void CreateSttClient()
        {
            try
            {
                if (PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/recorder") == CheckResult.Allow)
                {
                    if (_sttClient == null)
                    {
                        _sttClient = new SttClient();
                        _sttClient.StateChanged += SttStateChanged;
                        _sttClient.RecognitionResult += SttRecognitionResult;
                        _sttClient.EngineChanged += SttEngineChanged;
                        _sttClient.ErrorOccurred += SttErrorOccured;
                    }
                    Language = GetSupportedLanguages().First();
                    PrepareSttClient();
                }
            }
            catch (Exception e)
            {
                ReleaseResources();
                _onErrorAlert("Something Went Wrong", "The application did not find any supported languages, Please try again.", false);
            }
        }

        /// <summary>
        /// Gets a list of the supported languages to check whether the language you want is supported.
        /// </summary>
        public List<string> GetSupportedLanguages()
        {
            List<string> list = new();
            if (SttClient == null)
                return list;
            try
            {
                list = (List<string>)SttClient.GetSupportedLanguages();
            }
            catch (Exception e)
            {
                ReleaseResources();
                _onErrorAlert("Something Went Wrong", "The application did not find any supported languages, Please try again.", false);
            }
            return list;
        }

        /// <summary>
        /// Gets a list of the supported engines and the selection of current engines.
        /// Additional features, such as silence detection and partial result, 
        /// are provided by specific engines.
        /// </summary>
        public void GetSupportedEngines()
        {
            if (SttClient == null)
                return;
            try
            {
                List<SupportedEngine> supportedEngines = (List<SupportedEngine>)SttClient.GetSupportedEngines();
            }
            catch (Exception e)
            {
                ReleaseResources();
                _onErrorAlert("Something Went Wrong", "The application did not find any supported engines, Please try again.", false);
            }
        }

        /// <summary>
        /// Connects the background STT daemon.
        /// The state will become ready.
        /// </summary>
        public void PrepareSttClient()
        {
            try
            {
                if (State == State.Created && SttClient != null)
                {
                    SttClient.Prepare();
                }
            }
            catch (Exception e)
            {
                ReleaseResources();
                _onErrorAlert("An Error Occured", "Something Went Wrong, Please try again!", false);
            }
        }

        /// <summary>
        /// Disconnects the background STT daemon.
        /// The state will become created.
        /// </summary>
        public void UnprepareSttClient()
        {
            try
            {
                if (SttClient != null && State != State.Recording && State != State.Processing)
                {
                    SttClient.Unprepare();
                }
            }
            catch (Exception e)
            {
                ReleaseResources();
                _onErrorAlert("An Error Occured", "Something Went Wrong, Please try again!", false);
            }
        }

        /// <summary>
        /// Starts the recognition.
        /// The state will become recording.
        /// </summary>
        public void Start()
        {
            try
            {
                if (State == State.Recording || State == State.Processing)
                {
                    _onErrorAlert("NOTE", "The Application is Currently Processing or Recording your voice!", false);
                    return;
                }
                else if (State == State.Created)
                {
                    PrepareSttClient();
                }
                if (StartSound != null)
                {
                    SttClient.UnsetStartSound();
                }

                if (EndSound != null)
                {
                    SttClient.UnsetStopSound();
                }

                if (Sounds == true && StartSound != null)
                {
                    SetStartSound(StartSound);
                }
                SttClient.Start(_language, _recognitionType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                if (State == State.Processing || State == State.Recording)
                    return;
                _onErrorAlert("An Error Occured", "Something Went Wrong, Please try again!", false);
            }
        }

        /// <summary>
        /// Stops the recognition.
        /// Locks recognition start untill result are done and
        /// the state will become processing,
        /// after processing the state will become ready again.
        /// </summary>
        public void Stop()
        {
            try
            {
                if (State == State.Recording)
                {
                    SttClient.Stop();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _onErrorAlert("An Error Occured", "Something Went Wrong, the application could not stop the recording.", false);
            }
        }

        /// <summary>
        /// Clears recognition result.
        /// Unlocks recognition start.
        /// The state will become ready again.
        /// </summary>
        public void Cancel()
        {
            try
            {
                if (State == State.Recording)
                {
                    SttClient.Cancel();
                }
                if (_onResultChange != null && State != State.Processing)
                    _onResultChange("");
            }
            catch (Exception e)
            {
                _onErrorAlert("Something Went Wrong", "The application could not cancel the recording.", false);
            }
        }

        /// <summary>
        ///  Detects silence when the sound input from the user ends. 
        ///  If the silence detection is enabled, 
        ///  the STT library stops recognition automatically and sends the result.
        /// </summary>
        private void SetSilenceDetection(SilenceDetection type)
        {
            try
            {
                /// Default type is SilenceDetection.Auto
                SttClient.SetSilenceDetection(type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
                Console.WriteLine(filePath ?? "null");
                if (filePath == null)
                {
                    SttClient.UnsetStartSound();
                }
                else
                {
                    SttClient.SetStartSound(filePath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _onErrorAlert("Something Went Wrong", "Unable to change start sound!", false);
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
                Console.WriteLine(filePath ?? "null");
                if (filePath == null)
                {
                    SttClient.UnsetStopSound();
                }
                else
                {
                    SttClient.SetStopSound(filePath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                _onErrorAlert("Something Went Wrong", "Unable to change stop sound!", false);
            }
        }

        /// <summary>
        /// Event to handle errors(string).
        /// </summary>
        void SttErrorOccured(object sender, ErrorOccurredEventArgs e)
        {
            Console.WriteLine(e);
            _onErrorAlert("Something Went Wrong", "Please try again.", false);
        }

        /// <summary>
        ///  Destroys the STT client instance.
        /// </summary>
        public void ReleaseResources()
        {
            try
            {
                _sttClient?.Dispose();
            }
            catch (Exception e)
            {
                _onErrorAlert("Something Went Wrong", "The Application could not release the resources!", false);
            }
        }

        /// <summary>
        ///  Destructor of the SttController.
        /// </summary>
        ~SttController()
        {
            ReleaseResources();
        }
    }
}
