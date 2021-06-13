/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Tizen.Multimedia;
using Tizen.Security;
using VoiceRecorder.Model;
using VoiceRecorder.Tizen.Mobile.Control;
using VoiceRecorder.Tizen.Mobile.Service;
using VoiceRecorder.Utils;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(VoiceRecorderService))]

namespace VoiceRecorder.Tizen.Mobile.Service
{
    /// <summary>
    /// VoiceRecorderService class.
    /// Allows to control voice recorder.
    /// Implements IVoiceRecorderService interface.
    /// </summary>
    public class VoiceRecorderService : IVoiceRecorderService
    {
        #region fields

        /// <summary>
        /// Path to recording files.
        /// </summary>
        private const string PATH_TO_RECORDINGS = "/opt/usr/home/owner/media/Music/VoiceRecorder/";

        /// <summary>
        /// Recorded files' location template.
        /// </summary>
        private const string PATH_TEMPLATE_TO_RECORDINGS = "/opt/usr/home/owner/media/Music/VoiceRecorder/{0}{1}";

        /// <summary>
        /// Dictionary with all possible options of the format and the codec settings.
        /// </summary>
        private readonly Dictionary<FileFormatType, Tuple<RecorderFileFormat, RecorderAudioCodec>>
            FILE_FORMATS_DICTIONARY = new Dictionary<FileFormatType, Tuple<RecorderFileFormat, RecorderAudioCodec>>
            {
                { FileFormatType.MP4, Tuple.Create(RecorderFileFormat.Mp4, RecorderAudioCodec.Aac) },
                { FileFormatType.WAV, Tuple.Create(RecorderFileFormat.Wav, RecorderAudioCodec.Pcm) },
                { FileFormatType.OGG, Tuple.Create(RecorderFileFormat.Ogg, RecorderAudioCodec.Vorbis) }
            };

        /// <summary>
        /// Dictionary with all possible options of the audio bit rate setting.
        /// </summary>
        private readonly Dictionary<AudioBitRateType, int> RECORDING_QUALITY_DICTIONARY =
            new Dictionary<AudioBitRateType, int>
            {
                { AudioBitRateType.High, 256000 },
                { AudioBitRateType.Medium, 128000 },
                { AudioBitRateType.Low, 64000 }
            };

        /// <summary>
        /// Enumerator that contains all possible options of the audio channel setting.
        /// </summary>
        private enum AudioChannelType
        {
            /// <summary>
            /// Mono setting option.
            /// </summary>
            Mono = 1,

            /// <summary>
            /// Stereo setting option.
            /// </summary>
            Stereo = 2
        }

        /// <summary>
        /// AudioRecorder class instance.
        /// </summary>
        private AudioRecorder _recorder;

        /// <summary>
        /// Path to the last recording.
        /// </summary>
        private string _recordingPath;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked when the file format is changed.
        /// </summary>
        public event RecorderFileFormatUpdatedDelegate FileFormatUpdated;

        /// <summary>
        /// Event invoked when the recording quality is changed.
        /// </summary>
        public event RecorderAudioBitRateUpdatedDelegate RecordingQualityUpdated;

        /// <summary>
        /// Event invoked whenever recorder error occurred.
        /// </summary>
        public event ErrorOccurredDelegate ErrorOccurred;

        /// <summary>
        /// Event invoked whenever a new recording is saved.
        /// </summary>
        public event RecordingDataDelegate RecordingSaved;

        /// <summary>
        /// Event invoked when the recording is paused.
        /// </summary>
        public event EventHandler VoiceRecorderPaused;

        /// <summary>
        /// Event invoked when the recording is resumed.
        /// </summary>
        public event EventHandler VoiceRecorderResumed;

        /// <summary>
        /// Event invoked when the recording is started.
        /// </summary>
        public event RecordingDataDelegate VoiceRecorderStarted;

        /// <summary>
        /// Event invoked when the recording is stopped.
        /// </summary>
        public event EventHandler VoiceRecorderStopped;

        #endregion

        #region methods

        /// <summary>
        /// Initializes the Recorder.
        /// Invokes "FileFormatUpdated" and "RecordingQualityUpdated" to other application's modules.
        /// </summary>
        public void Init()
        {
            var bitrateType = AudioBitRateType.Medium;
            var fileFormat = FileFormatType.MP4;
            try
            {
                Directory.CreateDirectory(PATH_TO_RECORDINGS);
                var formatCodec = FILE_FORMATS_DICTIONARY[fileFormat];
                _recorder = new AudioRecorder(formatCodec.Item2, formatCodec.Item1)
                {
                    AudioBitRate = RECORDING_QUALITY_DICTIONARY[bitrateType],
                    AudioChannels = (int)AudioChannelType.Stereo
                };
                _recorder.Prepare();
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return;
            }

            FileFormatUpdated?.Invoke(this, fileFormat);
            RecordingQualityUpdated?.Invoke(this, bitrateType);
        }

        /// <summary>
        /// Cancels recording.
        /// Invokes "VoiceRecorderStopped" to other application's modules.
        /// </summary>
        public void CancelVoiceRecording()
        {
            try
            {
                _recorder.Cancel();

                // prepare-unpreapare required to make the Recorder work after cancel.
                _recorder.Unprepare();
                _recorder.Prepare();
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return;
            }

            VoiceRecorderStopped?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Pauses recording.
        /// Invokes "VoiceRecorderPaused" to other application's modules.
        /// </summary>
        public void PauseVoiceRecording()
        {
            try
            {
                _recorder.Pause();
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return;
            }

            VoiceRecorderPaused?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Restarts the recorder.
        /// Invokes "VoiceRecorderStopped" to other application's modules.
        /// </summary>
        public void Restart()
        {
            try
            {
                if (_recorder.State == RecorderState.Recording)
                {
                    _recorder.Cancel();
                }

                _recorder = new AudioRecorder(_recorder.AudioCodec, _recorder.FileFormat)
                {
                    AudioBitRate = _recorder.AudioBitRate,
                    AudioChannels = _recorder.AudioChannels
                };
                _recorder.Prepare();
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return;
            }

            VoiceRecorderStopped?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Starts recording or calls "ResumeVoiceRecording" method if it was paused.
        /// Invokes "VoiceRecorderStarted" if the recording was started.
        /// </summary>
        public void StartVoiceRecording()
        {
            if (_recorder.State == RecorderState.Paused)
            {
                ResumeVoiceRecording();
                return;
            }

            try
            {
                CreateDirectory();
                _recordingPath = string.Format(PATH_TEMPLATE_TO_RECORDINGS,
                    DateTime.Now.ToString("yyMMdd-HHmmss"), "." + _recorder.FileFormat.ToString().ToLower());
                _recorder.Start(_recordingPath);
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return;
            }

            VoiceRecorderStarted?.Invoke(this, _recordingPath);
        }

        /// <summary>
        /// Stops recording.
        /// Saves the file.
        /// Invokes "VoiceRecorderStopped" to other application's modules.
        /// </summary>
        public void StopVoiceRecording()
        {
            try
            {
                _recorder.Commit();

                using (var databaseConnector = new DatabaseConnector())
                {
                    databaseConnector.ErrorOccurred += OnDatabaseError;
                    databaseConnector.UpdateDatabase();
                }

                RecordingSaved?.Invoke(this, Path.GetFileName(_recordingPath));
                VoiceRecorderStopped?.Invoke(this, new EventArgs());
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }
        }

        /// <summary>
        /// Updates the recorder stereo setting.
        /// </summary>
        /// <param name="isStereo">Flag indicating if the recorder is in the stereo mode.</param>
        public void UpdateRecorderStereo(bool isStereo)
        {
            try
            {
                if (isStereo)
                {
                    _recorder.AudioChannels = (int)AudioChannelType.Stereo;
                }
                else
                {
                    _recorder.AudioChannels = (int)AudioChannelType.Mono;
                }
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }
        }

        /// <summary>
        /// Updates the Recorder file format and codec.
        /// Invokes "FileFormatUpdated" to other application's modules.
        /// </summary>
        /// <param name="item">Setting new value indicator.</param>
        public void UpdateRecorderFileFormat(FileFormatType item)
        {
            var oldCodec = _recorder.AudioCodec;
            var oldFileFormat = _recorder.FileFormat;

            try
            {
                _recorder.Unprepare();
                _recorder.SetFormatAndCodec(FILE_FORMATS_DICTIONARY[item].Item2,
                    FILE_FORMATS_DICTIONARY[item].Item1);
                _recorder.Prepare();
            }
            catch (Exception exception)
            {
                _recorder.SetFormatAndCodec(oldCodec, oldFileFormat);
                _recorder.Prepare();
                ErrorHandler(exception.Message);
                return;
            }

            FileFormatUpdated?.Invoke(this, item);
        }

        /// <summary>
        /// Updates the Recorder audio bit rate.
        /// Invokes "RecordingQualityUpdated" to other application's modules.
        /// </summary>
        /// <param name="item">Setting new value indicator.</param>
        public void UpdateRecordingQuality(AudioBitRateType item)
        {
            try
            {
                _recorder.AudioBitRate = RECORDING_QUALITY_DICTIONARY[item];
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
                return;
            }

            RecordingQualityUpdated?.Invoke(this, item);
        }

        /// <summary>
        /// Creates directory where recordings are saved (if not exist).
        /// </summary>
        private static void CreateDirectory()
        {
            Directory.CreateDirectory(PATH_TO_RECORDINGS);
        }

        /// <summary>
        /// Notifies user about the error.
        /// Invokes "ErrorOccurred" to other application's modules.
        /// </summary>
        /// <param name="errorMessage">Message of a thrown error.</param>
        private void ErrorHandler(string errorMessage, [CallerFilePath] string file = "", [CallerMemberName] string func = "", [CallerLineNumber] int line = 0)
        {
            global::Tizen.Log.Error(Program.LogTag, errorMessage, file, func, line);
            ErrorOccurred?.Invoke(this, errorMessage);
        }

        /// <summary>
        /// Handles "ErrorOccurred" when updating database.
        /// Throws an exception with error message.
        /// </summary>
        /// <param name="sender">Instance of the DatabaseConnector class.</param>
        /// <param name="errorMessage">Message of a thrown error.</param>
        private void OnDatabaseError(object sender, string errorMessage)
        {
            throw new Exception(errorMessage);
        }

        /// <summary>
        /// Resumes recording.
        /// Invokes "VoiceRecorderResumed" to other application's modules.
        /// </summary>
        private void ResumeVoiceRecording()
        {
            _recorder.Resume();
            VoiceRecorderResumed?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
