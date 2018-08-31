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

namespace SpeechToText.Model
{
    /// <summary>
    /// STT recognition types values enum.
    /// </summary>
    public enum RecognitionType
    {
        Free,
        Partial,
        Map,
        Search,
        WebSearch
    }

    /// <summary>
    /// STT silence detection values enum.
    /// </summary>
    public enum SilenceDetection
    {
        Auto,
        True,
        False
    }

    /// <summary>
    /// STT service error.
    /// </summary>
    public enum SttError
    {
        EngineNotFound,
        InProgressToProcessing,
        InProgressToReady,
        InProgressToRecording,
        InvalidLanguage,
        InvalidParameter,
        InvalidState,
        IoError,
        NoSpeech,
        NotSupported,
        NotSupportedFeature,
        OperationFailed,
        OutOfMemory,
        OutOfNetwork,
        PermissionDenied,
        RecorderBusy,
        RecordingTimedOut,
        ServiceReset,
        TimedOut,
        None
    }

}
