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
using System.Linq;
using System.Threading.Tasks;
using Tizen.Multimedia;
using Tizen.System;
using SystemStorage = Tizen.System.Storage;
using VoiceMemo.Services;
using VoiceMemo.Tizen.Wearable.Services;
using VoiceMemo.ViewModels;
using Xamarin.Forms;
using System.IO;
using Tizen.Wearable.CircularUI.Forms;

[assembly: Dependency(typeof(AudioRecordService))]

namespace VoiceMemo.Tizen.Wearable.Services
{
    /// <summary>
    /// AudioRecordService
    /// The main role is recording voice.
    /// Use Tizen.Multimedia and Tizen.System classes
    /// </summary>
    class AudioRecordService : IAudioRecordService
    {
        // Time limit of stt recording is 5 minutes.
        public const int STT_RECORDING_TIME_LIMIT = 5 * 60;
        // Time limit of recording is 30 minutes.
        public const int VOICE_ONLY_RECORDING_TIME_LIMIT = 30 * 60;

        // based on document(https://developer.tizen.org/development/guides/.net-application/media-and-camera/media-recording)
        const int AMC_CODEC_AUDIO_SAMPLE_RATE = 8000;
        AudioRecorder _recorder;
        // based on document (https://developer.tizen.org/development/guides/.net-application/media-and-camera/media-recording)
        // The following file formats are supported:
        // Audio: M4A and AMR
        const RecorderFileFormat AudioFileFormat = RecorderFileFormat.Amr;
        public static int numbering;
        //string StoragePath;
        AudioRecordState _state;
        string audioStoragePath;
        string filepath;
        private Action<Object, AudioRecordState, AudioRecordState> stateCallbacks;
        /// <summary>
        /// Get the state of audio recording
        /// </summary>
        public AudioRecordState State
        {
            get
            {
                return _state;
            }
        }

        public AudioRecordService()
        {
            _state = AudioRecordState.Init;
            numbering = 0;

            // Create an audio recorder
            if (_recorder == null)
            {
                // find out the available audio codec and file format
                RecorderAudioCodec AudioCodec = RecorderAudioCodec.Amr;
                foreach (RecorderAudioCodec codec in Recorder.GetSupportedAudioCodecs())
                {
                    //Console.WriteLine("RecorderAudioCodec : " + codec);
                    foreach (RecorderFileFormat format in codec.GetSupportedFileFormats())
                    {
                        //Console.WriteLine("  RecorderFileFormat : " + format);
                        if (format == AudioFileFormat)
                        {
                            AudioCodec = codec;
                            break;
                        }
                    }
                }
                //

                Console.WriteLine("+++  Selected AudioCodec : " + AudioCodec + " Format : " + AudioFileFormat);
                // 2017-12-08: Todo need to roll back..
                //_recorder = new AudioRecorder(AudioCodec, AudioFileFormat);
                //RecorderAudioCodec audioCodec, RecorderFileFormat fileFormat
                _recorder = new AudioRecorder(AudioCodec, AudioFileFormat);
                _recorder.StateChanged += AudioRecorder_StateChanged;
                _recorder.RecordingStatusChanged += AudioRecorder_RecordingStatusChanged;
                _recorder.RecordingLimitReached += AudioRecorder_RecordingLimitReached;
                _recorder.ErrorOccurred += AudioRecorder_ErrorOccurred;
                AudioRecorder.DeviceStateChanged += AudioRecorder_DeviceStateChanged;
                _recorder.Interrupting += AudioRecorder_Interrupting;
                //audioRecorder.ApplyAudioStreamPolicy(new AudioStreamPolicy(AudioStreamType.Media));
                //audioRecorder.AudioChannels = 2;
                _recorder.AudioDevice = RecorderAudioDevice.Mic;
                _recorder.AudioBitRate = 128000;
                _recorder.AudioSampleRate = AMC_CODEC_AUDIO_SAMPLE_RATE;

                if (_recorder.State != RecorderState.Idle)
                {
                    Console.WriteLine("AudioRecordService() : Invalid State (" + _recorder.State + ")" + "...may retry?");
                }

                _recorder.Prepare();
                if (_recorder.State != RecorderState.Ready)
                {
                    Console.WriteLine("AudioRecordService() : Invalid State (" + _recorder.State + ")" + "...may retry?");
                }

                try
                {
                    SystemStorage internalStorage = StorageManager.Storages.Where(s => s.StorageType == StorageArea.Internal).FirstOrDefault();
                    var SoundsDir = internalStorage.GetAbsolutePath(DirectoryType.Sounds);
                    audioStoragePath = Path.Combine(SoundsDir, "DotnetVoiceMemo");
                    // Create directory to save audio files
                    Directory.CreateDirectory(audioStoragePath);
                }
                catch (Exception e)
                {
                    Toast.DisplayText(e.Message + " - DotnetVoiceMemo directory creation failed.");
                }
            }
        }

        // The model class object for RecordingPage
        RecordingPageModel _viewModel;
        public RecordingPageModel ViewModel
        {
            get
            {
                return _viewModel;
            }

            set
            {
                if (_viewModel != value)
                {
                    _viewModel = value;
                }
            }
        }

        ~AudioRecordService()
        {
            Console.WriteLine("                 @@@ ~AudioRecordService() ");
            Destroy();
        }

        /// <summary>
        /// Start recording voice
        /// </summary>
        /// <param name="title">file path to store audio file</param>
        /// <param name="sttOn">indicate that Speech-To-Text service is on or not</param>
        /// <returns>Task<bool></returns>
        public Task<bool> Start(string title, bool sttOn)
        {
            return Task.Run(() =>
            {
                if (sttOn)
                {
                    // Recording time limits 5 minutes when speech-to-text function is enabled
                    _recorder.TimeLimit = STT_RECORDING_TIME_LIMIT;
                }
                else
                {
                    // Recording time limits 30 minutes when speech-to-text function is disabled
                    _recorder.TimeLimit = VOICE_ONLY_RECORDING_TIME_LIMIT;
                }

                try
                {
                    filepath = Path.Combine(audioStoragePath, title + "_T_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".amr");
                    _recorder.Start(filepath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(" _recorder.Start() Exception : " + e.Message);
                    HandleError("[VC] Start", e);
                    // TODO: Phase 2
                    // Toast Popup Message should be shown.
                    // Message : Recording failed.
                }

                return true;
            });
        }
        /// <summary>
        /// Pause recording voice
        /// </summary>
        public void Pause()
        {
            if (_recorder.State != RecorderState.Recording)
            {
                Console.WriteLine("AudioRecordService.Pause : Invalid State (" + _recorder.State + ")");
                return;
            }

            try
            {
                _recorder.Pause();
                if (_recorder.State != RecorderState.Paused)
                {
                    Console.WriteLine("    AudioRecordService.Pause state error");
                }
            }
            catch (Exception e)
            {
                HandleError("Pause", e);
            }
        }
        /// <summary>
        /// Resume recording voice
        /// </summary>
        public void Resume()
        {
            if (_recorder.State != RecorderState.Paused)
            {
                Console.WriteLine("AudioRecordService.Resume : Invalid State (" + _recorder.State + ")");
                return;
            }

            try
            {
                _recorder.Resume();
                if (_recorder.State != RecorderState.Recording)
                {
                    Console.WriteLine("    AudioRecordService.Resume state error");
                }
            }
            catch (Exception e)
            {
                HandleError("Resume", e);
            }
        }
        /// <summary>
        /// Cancel recording voice
        /// </summary>
        public void Cancel()
        {
            if (_recorder.State == RecorderState.Idle || _recorder.State == RecorderState.Ready)
            {
                Console.WriteLine("AudioRecordService.Cancel : No need to cancel in State (" + _recorder.State + ") ");
                return;
            }

            try
            {
                _recorder.Cancel();
                if (_recorder.State != RecorderState.Ready)
                {
                    Console.WriteLine("    AudioRecordService.Cancel state error");
                }
            }
            catch (Exception e)
            {
                HandleError("Cancel", e);
            }
        }
        /// <summary>
        /// Stop recording and save the voice recording file
        /// </summary>
        /// <returns>file path to save audio file</returns>
        public string Save()
        {
            if ((_recorder.State == RecorderState.Idle) || (_recorder.State == RecorderState.Ready))
            {
                Console.WriteLine("AudioRecordService.Save : No need to cancel in State (" + _recorder.State + ") ");
                return null;
            }

            try
            {
                // Stop recording and save the result in file
                _recorder.Commit();
                if (_recorder.State != RecorderState.Ready)
                {
                    Console.WriteLine("    AudioRecordService.Save state error");
                    // TODO : Handle error case
                    Toast.DisplayText("Fail to save audio file because of recording fw issue.");
                }

                string name = filepath.Substring(filepath.IndexOf(' '));
                string final = name.Substring(0, name.IndexOf("_T_"));
                //string final = name.Substring(name.IndexOf("_T_"), name.Length - name.IndexOf("_T_"));
                numbering = Convert.ToInt32(final);
                Console.WriteLine("   ######  Voice File name - " + name + " --> final : " + final + " , numbering : " + numbering);
            }
            catch (Exception e)
            {
                HandleError("Commit", e);
            }

            return filepath;
        }

        public void Destroy()
        {
            try
            {
                // release resources
                _recorder.StateChanged -= AudioRecorder_StateChanged;
                _recorder.RecordingStatusChanged -= AudioRecorder_RecordingStatusChanged;
                _recorder.RecordingLimitReached -= AudioRecorder_RecordingLimitReached;
                _recorder.ErrorOccurred -= AudioRecorder_ErrorOccurred;
                AudioRecorder.DeviceStateChanged -= AudioRecorder_DeviceStateChanged;
                _recorder.Interrupting -= AudioRecorder_Interrupting;
                _recorder.Unprepare();
                if (_recorder.State != RecorderState.Idle)
                {
                    Console.WriteLine("   ########################################");
                    Console.WriteLine("    AudioRecordService.Destroy state error");
                    Console.WriteLine("   ########################################");
                }

                Console.WriteLine("    AudioRecordService.Destroy() " + _recorder.State);
                _recorder.Dispose();
            }
            catch (Exception e)
            {
                HandleError("Destroy", e);
            }
        }
        /// <summary>
        /// Get voice recording level
        /// </summary>
        /// <returns>volume level</returns>
        public double GetRecordingLevel()
        {
            return _recorder.GetPeakAudioLevel();
        }

        void HandleError(string method, Exception e)
        {
            switch (_recorder.State)
            {
                case RecorderState.Idle:
                    Console.WriteLine("[RecordingService.HandleError in " + method + "()] State:Idle " + e.Message);
                    break;
                case RecorderState.Paused:
                    Console.WriteLine("[RecordingService.HandleError in " + method + "()] State:Paused " + e.Message);
                    Cancel();
                    break;
                case RecorderState.Ready:
                    Console.WriteLine("[RecordingService.HandleError in " + method + "()] State:Ready " + e.Message);
                    break;
                case RecorderState.Recording:
                    Console.WriteLine("[RecordingService.HandleError in " + method + "()] State:Recording " + e.Message);
                    Cancel();
                    break;
                default:
                    break;
            }
        }

        private void AudioRecorder_Interrupting(object sender, RecorderInterruptingEventArgs e)
        {
            Console.WriteLine("");
            Console.WriteLine("[AudioRecorder_Interrupting] : " + e.Reason);
        }

        private void AudioRecorder_ErrorOccurred(object sender, RecordingErrorOccurredEventArgs e)
        {
            Console.WriteLine("");
            Console.WriteLine("[AudioRecorder_ErrorOccurred] : " + e.Error + ", Type: " + e.GetType() + ", " + e.ToString());
            // TODO: Phase 2
            // Toast Popup Message should be shown.
            // Message : Recording failed.
        }

        private void AudioRecorder_RecordingLimitReached(object sender, RecordingLimitReachedEventArgs e)
        {
            Console.WriteLine("[AudioRecorder_RecordingLimitReached] type: " + e.Type);
            // Request stop to save voice recording
            ViewModel.RequestCommand.Execute(RecordingCommandType.Stop);
        }

        private void AudioRecorder_RecordingStatusChanged(object sender, RecordingStatusChangedEventArgs e)
        {
            //Console.WriteLine("[AudioRecorder_RecordingStatusChanged] ElapsedTime:" + e.ElapsedTime + ", FileSize:" + e.FileSize);
        }

        private void AudioRecorder_DeviceStateChanged(object sender, RecorderDeviceStateChangedEventArgs e)
        {
            Console.WriteLine("");
            Console.WriteLine("[AudioRecorder_DeviceStateChanged] deviceState : " + e.DeviceState);
            Console.WriteLine("");
        }

        private void AudioRecorder_StateChanged(object sender, RecorderStateChangedEventArgs e)
        {
            Console.WriteLine("[AudioRecorder_StateChanged] " + e.PreviousState + " --> " + e.CurrentState);
            switch (e.CurrentState)
            {
                case RecorderState.Idle:
                    _state = AudioRecordState.Idle;
                    break;
                case RecorderState.Paused:
                    _state = AudioRecordState.Paused;
                    break;
                case RecorderState.Ready:
                    _state = AudioRecordState.Ready;
                    break;
                case RecorderState.Recording:
                    _state = AudioRecordState.Recording;
                    break;
            }

            stateCallbacks(sender, (AudioRecordState)e.PreviousState, (AudioRecordState)e.CurrentState);
        }
              
        public void RegisterStateCallbacks(Action<Object, AudioRecordState, AudioRecordState> callback)
        {
            stateCallbacks = callback;
        }
    }
}
