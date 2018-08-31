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
using Native = Tizen.Uix.Stt;
using Portable = SpeechToText.Model;

namespace SpeechToText.Model
{
    /// <summary>
    /// Static class containing enums extensions.
    /// </summary>
    internal static class EnumExtensions
    {
        #region methods

        /// <summary>
        /// Converts STT client recognition type enum value to portable equivalent.
        /// </summary>
        /// <param name="type">Type to convert.</param>
        /// <returns>Converted enum value.</returns>
        internal static Portable.RecognitionType ToPortableRecognitionType(this Native.RecognitionType type)
        {
            switch (type)
            {
                case Native.RecognitionType.Free:
                    return Portable.RecognitionType.Free;
                case Native.RecognitionType.Partial:
                    return Portable.RecognitionType.Partial;
                case Native.RecognitionType.Map:
                    return Portable.RecognitionType.Map;
                case Native.RecognitionType.Search:
                    return Portable.RecognitionType.Search;
                case Native.RecognitionType.WebSearch:
                    return Portable.RecognitionType.WebSearch;
            }

            throw new ArgumentException("Invalid value of recognition type");
        }

        /// <summary>
        /// Converts portable silence detection mode enum value to native equivalent.
        /// </summary>
        /// <param name="mode">Mode to convert.</param>
        /// <returns>Converted enum value.</returns>
        internal static Native.SilenceDetection ToNativeSilenceDetection(this Portable.SilenceDetection mode)
        {
            switch (mode)
            {
                case Portable.SilenceDetection.Auto:
                    return Native.SilenceDetection.Auto;
                case Portable.SilenceDetection.False:
                    return Native.SilenceDetection.False;
                case Portable.SilenceDetection.True:
                    return Native.SilenceDetection.True;
            }

            throw new ArgumentException("Invalid value of silence detection");
        }

        /// <summary>
        /// Converts portable recognition type enum value to native equivalent.
        /// </summary>
        /// <param name="type">Type to convert.</param>
        /// <returns>Converted enum value.</returns>
        internal static Native.RecognitionType ToNativeRecognitionType(this Portable.RecognitionType type)
        {
            switch (type)
            {
                case Portable.RecognitionType.Free:
                    return Native.RecognitionType.Free;
                case Portable.RecognitionType.Partial:
                    return Native.RecognitionType.Partial;
                case Portable.RecognitionType.Map:
                    return Native.RecognitionType.Map;
                case Portable.RecognitionType.Search:
                    return Native.RecognitionType.Search;
                case Portable.RecognitionType.WebSearch:
                    return Native.RecognitionType.WebSearch;
            }

            throw new ArgumentException("Invalid value of recognition type");
        }

        /// <summary>
        /// Converts native STT service error (enum) to portable equivalent.
        /// </summary>
        /// <param name="error">Error to convert.</param>
        /// <returns>Converted enum value.</returns>
        internal static Portable.SttError ToPortableSttError(this Native.Error error)
        {
            switch (error)
            {
                case Native.Error.EngineNotFound:
                    return Portable.SttError.EngineNotFound;
                case Native.Error.InProgressToProcessing:
                    return Portable.SttError.InProgressToProcessing;
                case Native.Error.InProgressToReady:
                    return Portable.SttError.InProgressToReady;
                case Native.Error.InProgressToRecording:
                    return Portable.SttError.InProgressToRecording;
                case Native.Error.InvalidLanguage:
                    return Portable.SttError.InvalidLanguage;
                case Native.Error.InvalidParameter:
                    return Portable.SttError.InvalidParameter;
                case Native.Error.InvalidState:
                    return Portable.SttError.InvalidState;
                case Native.Error.IoError:
                    return Portable.SttError.IoError;
                case Native.Error.NoSpeech:
                    return Portable.SttError.NoSpeech;
                case Native.Error.NotSupported:
                    return Portable.SttError.NotSupported;
                case Native.Error.NotSupportedFeature:
                    return Portable.SttError.NotSupportedFeature;
                case Native.Error.None:
                    return Portable.SttError.None;
                case Native.Error.OperationFailed:
                    return Portable.SttError.OperationFailed;
                case Native.Error.OutOfMemory:
                    return Portable.SttError.OutOfMemory;
                case Native.Error.OutOfNetwork:
                    return Portable.SttError.OutOfNetwork;
                case Native.Error.PermissionDenied:
                    return Portable.SttError.PermissionDenied;
                case Native.Error.RecorderBusy:
                    return Portable.SttError.RecorderBusy;
                case Native.Error.RecordingTimedOut:
                    return Portable.SttError.RecordingTimedOut;
                case Native.Error.ServiceReset:
                    return Portable.SttError.ServiceReset;
                case Native.Error.TimedOut:
                    return Portable.SttError.TimedOut;
            }

            throw new ArgumentException("Invalid value of STT error");
        }

        #endregion
    }
}
