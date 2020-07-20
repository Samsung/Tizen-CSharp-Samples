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
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using VoiceMemo.Models;
using VoiceMemo.Resx;
using VoiceMemo.Services;
using Xamarin.Forms;

namespace VoiceMemo.ViewModels
{
    public enum RecordingViewModelState
    {
        Ready,
        Recording,
        Paused,
        PausedForCancel,
        Cancelled,
        Stopped,
    }

    public enum RecordingCommandType
    {
        Record,
        Pause,
        PauseForCancelRequest,
        Resume,
        Cancel,
        Stop
    }

    // The model class for RecordingPage
    public class RecordingPageModel : BasePageModel
    {
        /// <summary>
        /// It works while recording.
        /// </summary>
        private static Stopwatch RecordingWatch;

        // For recording voice
        IAudioRecordService _audioRecordingService;

        public IAudioRecordService AudioRecordingService
        {
            get => _audioRecordingService;
            private set
            {
                SetProperty(ref _audioRecordingService, value, "AudioRecordingService");
            }
        }

        RecordingViewModelState _recordingViewModelState;
        public RecordingViewModelState RecordingViewModelState
        {
            get => _recordingViewModelState;
            private set
            {
                SetProperty(ref _recordingViewModelState, value, "RecordingViewModelState");
            }
        }

        bool _SttOn;
        public bool SttOn
        {
            get
            {
                return _SttOn;
            }

            set
            {
                SetProperty(ref _SttOn, value, "SttOn");
            }
        }

        string _recordTitle;
        public string RecordTitle
        {
            get => _recordTitle;
            set
            {
                SetProperty(ref _recordTitle, value, "RecordTitle");
            }
        }

        string _recordingTime;
        public string RecordingTime
        {
            get => _recordingTime;
            set
            {
                SetProperty(ref _recordingTime, value, "RecordingTime");
            }
        }

        string _toggleImage;
        public string PauseRecordToggleImage
        {
            get => _toggleImage;
            set
            {
                SetProperty(ref _toggleImage, value, "PauseRecordToggleImage");
            }
        }

        double _RecordingProcess;
        public double RecordingProcess
        {
            get
            {
                return _RecordingProcess;
            }

            set
            {
                SetProperty(ref _RecordingProcess, value, "RecordingProcess");
            }
        }

        public ICommand RequestCommand { private set; get; }

        // For restoring information of the created voice media file
        IMediaContentService _ContentService;
        // For
        IDeviceInformation _DeviceInfoService;
        ISpeechToTextService _SttService;
        public int Index;

        double RECORDING_PROGRESSBAR_DELTA;

        bool _isRecordingEffectOn;

        public bool RecordingEffectOn
        {
            get
            {
                return _isRecordingEffectOn;
            }

            set
            {
                SetProperty(ref _isRecordingEffectOn, value, "RecordingEffectOn");
            }
        }

        bool _isTimeFlickeringOn;
        private bool _requestSttStart;
        private bool _requestSttStop;

        public bool TimeFlickeringOn
        {
            get
            {
                return _isTimeFlickeringOn;
            }

            set
            {
                SetProperty(ref _isTimeFlickeringOn, value, "TimeFlickeringOn");
            }
        }

        bool disposing;

        private void RecordingStateCallback(Object o, AudioRecordState prev, AudioRecordState current)
        {
            switch (current)
            {
                case AudioRecordState.Idle:
                    HandleRecorderIdleState(o, prev, current);
                    break;
                case AudioRecordState.Init:
                    HandleRecorderInitState(o, prev, current);
                    break;
                case AudioRecordState.Ready:
                    HandleRecorderReadyState(o, prev, current);
                    break;
                case AudioRecordState.Recording:
                    HandleRecorderRecordingState(o, prev, current);
                    break;
                case AudioRecordState.Paused:
                    HandleRecorderPauseState(o, prev, current);
                    break;
            }
        }

        public void SttStateCallback(Object o, SttState prev, SttState current)
        {
            switch (current)
            {
                case SttState.Created:
                    HandleSttCreatedState(o, prev, current);
                    break;
                case SttState.Ready:
                    HandleSttReadyState(o, prev, current);
                    ((App)App.Current).mainPageModel.availableToRecord = true;
                    break;
                case SttState.Recording:
                    HandleSttRecordingState(o, prev, current);
                    ((App)App.Current).mainPageModel.availableToRecord = false;
                    break;
                case SttState.Processing:
                    HandleSttProcessingState(o, prev, current);
                    ((App)App.Current).mainPageModel.availableToRecord = false;
                    break;
                case SttState.Unavailable:
                    HandleSttUnavailableState(o, prev, current);
                    break;
            }
        }

        private void HandleSttProcessingState(object o, SttState prev, SttState current)
        {
            if (_requestSttStop)
            {
                _requestSttStop = false;
                _SttService.Cancel();
            }
        }

        private void HandleSttUnavailableState(object o, SttState prev, SttState current)
        {
        }

        private void HandleSttRecordingState(object o, SttState prev, SttState current)
        {
            if (_requestSttStop)
            {
                _requestSttStop = false;
                _SttService.Cancel();
            }
        }

        private void HandleSttReadyState(object o, SttState prev, SttState current)
        {
            if (_requestSttStart && !_requestSttStop)
            {
                _requestSttStart = false;
                _SttService.StartStt();
            }
        }

        private void HandleSttCreatedState(object o, SttState prev, SttState current)
        {
            throw new NotImplementedException();
        }

        private void HandleRecorderPauseState(object o, AudioRecordState prev, AudioRecordState current)
        {
            StopTimer();
            StopRecordingEffect();
            if (RecordingViewModelState != RecordingViewModelState.PausedForCancel)
            {
                StartTimeFlickering();
                RecordingViewModelState = RecordingViewModelState.Paused;
                PauseRecordToggleImage = "record_stop_icon.png";
            }
        }

        private void HandleRecorderRecordingState(object o, AudioRecordState prev, AudioRecordState current)
        {
            if (prev == AudioRecordState.Paused) // from pause -> recording
            {
                StopTimeFlickering();
            }

            // Start recording UI on
            StartTimer();
            StartRecordingEffect();
            PauseRecordToggleImage = "recording_icon_pause.png";
            RecordingViewModelState = RecordingViewModelState.Recording;
        }

        private void HandleRecorderReadyState(object o, AudioRecordState prev, AudioRecordState current)
        {

        }

        private void HandleRecorderInitState(object o, AudioRecordState prev, AudioRecordState current)
        {

        }

        private void HandleRecorderIdleState(object o, AudioRecordState prev, AudioRecordState current)
        {
            throw new NotImplementedException();
        }

        public RecordingPageModel(bool sttOn)
        {
            RequestCommand = new Command<RecordingCommandType>(Request);

            if (RecordingWatch == null)
            {
                RecordingWatch = new Stopwatch();
            }

            _audioRecordingService = DependencyService.Get<IAudioRecordService>();
            _audioRecordingService.ViewModel = this;
            _audioRecordingService.RegisterStateCallbacks(new Action<Object, AudioRecordState, AudioRecordState>(RecordingStateCallback));
            AudioRecordingService = _audioRecordingService;
            _DeviceInfoService = DependencyService.Get<IDeviceInformation>(DependencyFetchTarget.GlobalInstance);
            _SttService = DependencyService.Get<ISpeechToTextService>(DependencyFetchTarget.GlobalInstance);
            _SttService.RegisterStateCallbacks(new Action<object, SttState, SttState>(SttStateCallback));
            _ContentService = DependencyService.Get<IMediaContentService>(DependencyFetchTarget.GlobalInstance);
            string.Format("{0:00}", _DeviceInfoService.LastStoredFileIndex);
            Index = _DeviceInfoService.LastStoredFileIndex + 1;
            SttOn = sttOn;
            MessagingCenter.Subscribe<MainPageModel, bool>(this, MessageKeys.SttSupportedChanged, (obj, sttIsOn) =>
            {
                Console.WriteLine("### MessagingCenter ## [RecordingPageModel] just received << MessageKeys.SttSupportedChanged >>  On? :" + sttIsOn);
                SttOn = sttIsOn;
            });
            disposing = false;
        }

        public void Init()
        {
            // This is an entry point to any service 
            // Recorder: ready 
            // Stt: ready if present
            Console.WriteLine("[RecordingPageModel.Init]   SttOn:" + SttOn + ", stt state:" + _SttService.SttState);
            if (!_DeviceInfoService.StorageAvailable)
            {
                // TODO: ContextPopup
                return;
            }

            // initialize page
            RecordingViewModelState = RecordingViewModelState.Ready;
            RecordingTime = "00:00";
            RecordingProcess = 0;
            RecordingWatch.Reset();
            RecordingEffectOn = false;
            TimeFlickeringOn = false;
            RecordTitle = "Memo " + Index.ToString();
            RECORDING_PROGRESSBAR_DELTA = 0.003333;
            _requestSttStart = false;
            _requestSttStop = false;

            if (SttOn)
            {
                switch (_SttService.SttState)
                {
                    case SttState.Created:
                        _requestSttStart = true; // this should be false in Ready callback
                        break;
                    case SttState.Ready:
                        _SttService.StartStt();
                        break;
                    case SttState.Recording:
                    case SttState.Processing:
                        try
                        {
                            Console.WriteLine("[RecordingPageModel.Init] STT state: " + _SttService.SttState + " --> Cancel it");
                            _SttService.Cancel();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("[VC] catch possible error while cancel " + e.Message + ", " + e.Source);
                        }

                        MessagingCenter.Send<RecordingPageModel>(this, MessageKeys.ForcePopRecordingPage);
                        return;
                    case SttState.Unavailable:
                        break;
                }
            }
            else
            {
                RecordTitle = "Voice " + Index.ToString();
                RECORDING_PROGRESSBAR_DELTA = 0.000555556;
            }

            _audioRecordingService.Start(RecordTitle, SttOn);
        }

        void StartTimer()
        {
            // TODO: Need to check recorded time and display time
            RecordingWatch.Start();
            // Any background code that needs to update the user interface
            Device.BeginInvokeOnMainThread(() =>
            {
                // interact with UI elements
                Device.StartTimer(new TimeSpan(0, 0, 0, 1, 0), UpdateRecordingTimeAndProgressBar);
            });
        }

        void StopTimer()
        {
            RecordingWatch.Stop();
        }

        bool UpdateRecordingTimeAndProgressBar()
        {
            //return true to keep the timer running or false to stop it after the current invocation.
            if (_audioRecordingService.State == AudioRecordState.Recording)
            {
                RecordingProcess += RECORDING_PROGRESSBAR_DELTA;

                RecordingTime = string.Format("{0:00}:{1:00}", RecordingWatch.Elapsed.Minutes, RecordingWatch.Elapsed.Seconds);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Request(RecordingCommandType request)
        {
            switch (request)
            {
                case RecordingCommandType.Record:
                    RecordingCommandRequested();
                    break;
                case RecordingCommandType.Pause:
                    PauseCommandRequested();
                    break;
                case RecordingCommandType.PauseForCancelRequest:
                    PauseForCancelCommandRequested();
                    break;
                case RecordingCommandType.Resume:
                    ResumeCommandRequested();
                    break;
                case RecordingCommandType.Cancel:
                    CancelCommandRequested();
                    break;
                case RecordingCommandType.Stop:
                    _ = StopCommandRequested(); // caution: fire-and-forget asynchronous call
                    break;
            }
        }

        private void RecordingCommandRequested()
        {
            // 20180220-vincent: At this point audioRecordingService is either Recording or Paused
            if (_audioRecordingService.State == AudioRecordState.Paused)
            {
                _audioRecordingService.Resume();
            }
        }

        private async Task StopCommandRequested()
        {
            if (RecordingViewModelState == RecordingViewModelState.Stopped)
            {
                return; // if already set to stopped, then do not repeat
            }

            RecordingViewModelState = RecordingViewModelState.Stopped;

            if (!disposing)
            {
                StopTimer();
                StopRecordingEffect();
                StopTimeFlickering();
            }

            Record record = SaveRecording();
            bool ExistText = SttOn && _SttService.SttState == SttState.Recording;
            if (ExistText)
            {
                string SttText = await _SttService.StopAndGetText();
                record.Text = SttText;
                Console.WriteLine("[StopCommandRequested] record.Text : (" + record.Text + ")");
            }
            else if (SttOn)
            {
                // TODO: remove it.
                record.Text = "STT Error? " + _SttService.SttState;
            }

            // Notify that it's time to save Record in database
            MessagingCenter.Send<RecordingPageModel, Record>(this, MessageKeys.SaveVoiceMemoInDB, record);

            if (ExistText &&
                (string.IsNullOrEmpty(record.Text) || record.Text.Equals("") || record.Text.Equals(" ") || record.Text.Equals("  ")))
            {
                Console.WriteLine("[StopCommandRequested]         !!!!!!!!!        resultText is string.IsNullOrEmpty");
                record.Text = AppResources.RecognitionFailed;
            }

            if (disposing)
            {
                DisposeServices();
            }
        }

        Record SaveRecording()
        {
            var filePath = _audioRecordingService.Save();
            // TODO: Handle audio recording failure case
            Index++;
            Record record = _ContentService.GetMediaInfo(filePath, SttOn);
            MessagingCenter.Send<RecordingPageModel, Record>(this, MessageKeys.SaveVoiceMemo, new LatestRecord(record));
            return record;
        }

        private void CancelCommandRequested()
        {
            Console.WriteLine("\n\n  CANCEL ---  AUDIO RECORDING & STT");
            _requestSttStart = false; // initialize stt start request
            if (RecordingViewModelState == RecordingViewModelState.Paused)
            {
                StopTimeFlickering();
            }

            if (SttOn)
            {
                _requestSttStop = true;
                if (_SttService.SttState == SttState.Recording || _SttService.SttState == SttState.Processing)
                {
                    _SttService.Cancel();
                }
                else // created, ready
                {
                    // do not need to cancel but should not start
                    if (_requestSttStart)
                    {
                        _requestSttStart = false; // prevent create->ready (should not start)
                    }
                }
            }

            _audioRecordingService.Cancel();
            RecordingViewModelState = RecordingViewModelState.Cancelled;
        }

        private void ResumeCommandRequested()
        {
            if (RecordingViewModelState == RecordingViewModelState.PausedForCancel)
            {
                Request(RecordingCommandType.Record);
            }
        }

        private void PauseCommandRequested()
        {
            if (_audioRecordingService.State == AudioRecordState.Recording)
            {
                _audioRecordingService.Pause();
                // Any background code that needs to update the user interface
                Device.BeginInvokeOnMainThread(() =>
                {
                    // interact with UI elements
                    Device.StartTimer(new TimeSpan(0, 0, 0, 60, 0), DoAutoSaveAndTerminate);
                });
            }
        }

        bool DoAutoSaveAndTerminate()
        {
            //if (_audioRecordingService.State == AudioRecordState.Paused)
            //{
            //    RequestCommand.Execute(RecordingCommandType.Stop);

            //}
            Console.WriteLine("        Now, save recording and terminate this application. ");
            // Terminate this application
            DependencyService.Get<IAppTerminator>().TerminateApp();
            return false;
        }

        private void PauseForCancelCommandRequested()
        {
            if (_audioRecordingService.State == AudioRecordState.Recording)
            {
                RecordingViewModelState = RecordingViewModelState.PausedForCancel;
                _audioRecordingService.Pause();
            }
        }

        private void StartRecordingEffect()
        {
            RecordingEffectOn = true;
        }

        private void StopRecordingEffect()
        {
            RecordingEffectOn = false;
        }

        private void StartTimeFlickering()
        {
            TimeFlickeringOn = true;
        }

        private void StopTimeFlickering()
        {
            TimeFlickeringOn = false;
        }

        public void Dispose()
        {
            Console.WriteLine("RecordingPageModel.Dispose()   START");

            MessagingCenter.Unsubscribe<MainPageModel, bool>(this, MessageKeys.SttSupportedChanged);
            if (_audioRecordingService.State == AudioRecordState.Recording || _audioRecordingService.State == AudioRecordState.Paused)
            {
                disposing = true;
                RequestCommand.Execute(RecordingCommandType.Stop);
            }
            else
            {
                DisposeServices();
            }
            //////#if STT_ON
            //////            if (SttOn)
            //////            {
            //////                Console.WriteLine("RecordingPageModel.Dispose()   _SttService.Dispose");
            //////                _SttService.Dispose();
            //////            }
            //////#endif
            //////            _ContentService.Destroy();
            //////            _audioRecordingService.Destroy();
            Console.WriteLine("RecordingPageModel.Dispose()   DONE");
        }

        void DisposeServices()
        {
            Console.WriteLine("RecordingPageModel.DisposeServices()   START");
            _audioRecordingService.Destroy();
            _SttService.Dispose();
            _ContentService.Destroy();
            Console.WriteLine("RecordingPageModel.DisposeServices()   DONE");
        }
    }
}
