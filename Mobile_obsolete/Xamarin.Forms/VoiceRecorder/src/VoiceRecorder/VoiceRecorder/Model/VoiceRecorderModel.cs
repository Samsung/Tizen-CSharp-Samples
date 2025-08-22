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
using VoiceRecorder.Utils;
using Xamarin.Forms;

namespace VoiceRecorder.Model
{
    /// <summary>
    /// VoiceRecorderModel class.
    /// Provides methods to control the recorder.
    /// </summary>
    public class VoiceRecorderModel
    {
        #region fields

        /// <summary>
        /// Instance of the VoiceRecorderService class.
        /// </summary>
        private IVoiceRecorderService _service;

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
        /// Initializes VoiceRecorderModel class instance.
        /// </summary>
        public VoiceRecorderModel()
        {
            _service = DependencyService.Get<IVoiceRecorderService>();
            _service.VoiceRecorderPaused += VoiceRecorderPausedEventHandler;
            _service.VoiceRecorderResumed += VoiceRecorderResumedEventHandler;
            _service.VoiceRecorderStarted += VoiceRecorderStartedEventHandler;
            _service.VoiceRecorderStopped += VoiceRecorderStoppedEventHandler;
            _service.FileFormatUpdated += FileFormatUpdatedEventHandler;
            _service.RecordingQualityUpdated += RecordingQualityUpdatedEventHandler;
            _service.ErrorOccurred += ErrorOccurredEventHandler;
            _service.RecordingSaved += RecordingSavedEventHandler;
        }

        /// <summary>
        /// Initializes the service.
        /// </summary>
        public void Init()
        {
            _service.Init();
        }

        /// <summary>
        /// Cancels recording.
        /// </summary>
        public void CancelVoiceRecording()
        {
            _service.CancelVoiceRecording();
        }

        /// <summary>
        /// Pauses recording.
        /// </summary>
        public void PauseVoiceRecording()
        {
            _service.PauseVoiceRecording();
        }

        /// <summary>
        /// Restarts the recorder.
        /// </summary>
        public void Restart()
        {
            _service.Restart();
        }

        /// <summary>
        /// Updates recorder file format setting.
        /// </summary>
        /// <param name="item">New file format value to set.</param>
        public void UpdateRecorderFileFormat(FileFormatType item)
        {
            _service.UpdateRecorderFileFormat(item);
        }

        /// <summary>
        /// Updates recording quality setting.
        /// </summary>
        /// <param name="item">New recording quality value to set.</param>
        public void UpdateRecordingQuality(AudioBitRateType item)
        {
            _service.UpdateRecordingQuality(item);
        }

        /// <summary>
        /// Starts recording.
        /// </summary>
        public void StartVoiceRecording()
        {
            _service.StartVoiceRecording();
        }

        /// <summary>
        /// Stops recording.
        /// </summary>
        public void StopVoiceRecording()
        {
            _service.StopVoiceRecording();
        }

        /// <summary>
        /// Updates the recorder stereo setting.
        /// </summary>
        /// <param name="isStereo">Flag indicating if the recorder is in the stereo mode.</param>
        public void UpdateRecorderStereo(bool isStereo)
        {
            _service.UpdateRecorderStereo(isStereo);
        }

        /// <summary>
        /// Handles "FileFormatUpdated" of the IVoiceRecorderService object.
        /// Invokes "FileFormatUpdated" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderService class.</param>
        /// <param name="newValue">New value of the recorder file format setting.</param>
        private void FileFormatUpdatedEventHandler(object sender, FileFormatType newValue)
        {
            FileFormatUpdated?.Invoke(sender, newValue);
        }

        /// <summary>
        /// Handles "RecordingQualityUpdated" of the IVoiceRecorderService object.
        /// Invokes "RecordingQualityUpdated" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderService class.</param>
        /// <param name="newValue">New value of the recorder recording quality setting.</param>
        private void RecordingQualityUpdatedEventHandler(object sender, AudioBitRateType newValue)
        {
            RecordingQualityUpdated?.Invoke(this, newValue);
        }

        /// <summary>
        /// Handles "ErrorOccurred" of the IVoiceRecorderService object.
        /// Invokes "ErrorOccurred" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderService class.</param>
        /// <param name="errorMessage">Error message.</param>
        private void ErrorOccurredEventHandler(object sender, string errorMessage)
        {
            ErrorOccurred?.Invoke(this, errorMessage);
        }

        /// <summary>
        /// Handles "ErrorOccurred" of the IVoiceRecorderService object.
        /// Invokes "ErrorOccurred" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderService class.</param>
        /// <param name="recordingName">Name of a saved recording.</param>
        private void RecordingSavedEventHandler(object sender, string recordingName)
        {
            RecordingSaved?.Invoke(this, recordingName);
        }

        /// <summary>
        /// Handles "VoiceRecorderPaused" of the IVoiceRecorderService object.
        /// Invokes "VoiceRecorderPaused" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderService class.</param>
        /// <param name="e">Contains event data.</param>
        private void VoiceRecorderPausedEventHandler(object sender, EventArgs e)
        {
            VoiceRecorderPaused?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Handles "VoiceRecorderResumed" of the IVoiceRecorderService object.
        /// Invokes "VoiceRecorderResumed" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderService class.</param>
        /// <param name="e">Contains event data.</param>
        private void VoiceRecorderResumedEventHandler(object sender, EventArgs e)
        {
            VoiceRecorderResumed?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Handles "VoiceRecorderStarted" of the IVoiceRecorderService object.
        /// Invokes "VoiceRecorderStarted" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderService class.</param>
        /// <param name="currentRecordingPath">Path to the sample which is being recorded at the moment.</param>
        private void VoiceRecorderStartedEventHandler(object sender, string currentRecordingPath)
        {
            VoiceRecorderStarted?.Invoke(this, currentRecordingPath);
        }

        /// <summary>
        /// Handles "VoiceRecorderStopped" of the IVoiceRecorderService object.
        /// Invokes "VoiceRecorderStopped" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderService class.</param>
        /// <param name="e">Contains event data.</param>
        private void VoiceRecorderStoppedEventHandler(object sender, EventArgs e)
        {
            VoiceRecorderStopped?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
