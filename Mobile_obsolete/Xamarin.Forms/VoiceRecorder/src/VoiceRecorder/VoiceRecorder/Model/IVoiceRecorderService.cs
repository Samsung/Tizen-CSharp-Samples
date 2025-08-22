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

namespace VoiceRecorder.Model
{
    /// <summary>
    /// Delegate for the recorder file format setting update event.
    /// </summary>
    /// <param name="sender">Instance of the object which invokes event.</param>
    /// <param name="newValue">New value of the recorder file format setting.</param>
    public delegate void RecorderFileFormatUpdatedDelegate(object sender, FileFormatType newValue);

    /// <summary>
    /// Delegate for the recorder recording quality setting update event.
    /// </summary>
    /// <param name="sender">Instance of the object which invokes event.</param>
    /// <param name="newValue">New value of the recorder recording quality setting.</param>
    public delegate void RecorderAudioBitRateUpdatedDelegate(object sender, AudioBitRateType newValue);

    /// <summary>
    /// Delegate to handle events whenever error occurred.
    /// </summary>
    /// <param name="sender">Instance of the object which invokes event.</param>
    /// <param name="errorMessage">Error message.</param>
    public delegate void ErrorOccurredDelegate(object sender, string errorMessage);

    /// <summary>
    /// Delegate for a new recording saved event.
    /// </summary>
    /// <param name="sender">Instance of the object which invokes event.</param>
    /// <param name="recordingData">Information about the recording.</param>
    public delegate void RecordingDataDelegate(object sender, string recordingData);

    /// <summary>
    /// Recorder service interface.
    /// </summary>
    public interface IVoiceRecorderService
    {
        #region properties

        /// <summary>
        /// Event invoked when the file format is changed.
        /// </summary>
        event RecorderFileFormatUpdatedDelegate FileFormatUpdated;

        /// <summary>
        /// Event invoked when the recording quality is changed.
        /// </summary>
        event RecorderAudioBitRateUpdatedDelegate RecordingQualityUpdated;

        /// <summary>
        /// Event invoked whenever recorder error occurred.
        /// </summary>
        event ErrorOccurredDelegate ErrorOccurred;

        /// <summary>
        /// Event invoked whenever a new recording is saved.
        /// </summary>
        event RecordingDataDelegate RecordingSaved;

        /// <summary>
        /// Event invoked when the recording is paused.
        /// </summary>
        event EventHandler VoiceRecorderPaused;

        /// <summary>
        /// Event invoked when the recording is resumed.
        /// </summary>
        event EventHandler VoiceRecorderResumed;

        /// <summary>
        /// Event invoked when the recording is started.
        /// </summary>
        event RecordingDataDelegate VoiceRecorderStarted;

        /// <summary>
        /// Event invoked when the recording is stopped.
        /// </summary>
        event EventHandler VoiceRecorderStopped;

        #endregion

        #region methods

        /// <summary>
        /// Initializes the service.
        /// Required privileges need to be checked first.
        /// </summary>
        void Init();

        /// <summary>
        /// Cancels recording.
        /// </summary>
        void CancelVoiceRecording();

        /// <summary>
        /// Pauses recording.
        /// </summary>
        void PauseVoiceRecording();

        /// <summary>
        /// Restarts the recorder.
        /// </summary>
        void Restart();

        /// <summary>
        /// Updates recording file format.
        /// </summary>
        /// <param name="item">New file format.</param>
        void UpdateRecorderFileFormat(FileFormatType item);

        /// <summary>
        /// Updates recording quality.
        /// </summary>
        /// <param name="item">New recording quality.</param>
        void UpdateRecordingQuality(AudioBitRateType item);

        /// <summary>
        /// Starts recording.
        /// </summary>
        void StartVoiceRecording();

        /// <summary>
        /// Stops recording.
        /// </summary>
        void StopVoiceRecording();

        /// <summary>
        /// Updates the recorder stereo setting.
        /// </summary>
        /// <param name="isStereo">Flag indicating if the recorder is in the stereo mode.</param>
        void UpdateRecorderStereo(bool isStereo);

        #endregion
    }
}
