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
using System.Globalization;
using Xamarin.Forms;

namespace SpeechToText.Converters
{
    /// <summary>
    /// Converter class to convert STT service error to human readable message.
    /// </summary>
    class SttErrorToDisplayMessage : IValueConverter
    {
        #region methods

        /// <summary>
        /// Converts STT service error to human readable message.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }

            switch ((SttError)value)
            {
                case SttError.EngineNotFound:
                    return "No available engine.";
                case SttError.InProgressToProcessing:
                    return "Progress to processing state is not finished.";
                case SttError.InProgressToReady:
                    return "Progress to ready state is not finished.";
                case SttError.InProgressToRecording:
                    return "Progress to recording state is not finished.";
                case SttError.InvalidLanguage:
                    return "Invalid language.";
                case SttError.InvalidParameter:
                    return "Invalid parameter.";
                case SttError.InvalidState:
                    return "Invalid state.";
                case SttError.IoError:
                    return "I/O error.";
                case SttError.NoSpeech:
                    return "No speech while recording.";
                case SttError.NotSupported:
                    return "STT not supported.";
                case SttError.NotSupportedFeature:
                    return "Not supported feature of current engine.";
                case SttError.OperationFailed:
                    return "Operation failed.";
                case SttError.OutOfMemory:
                    return "Out of memory.";
                case SttError.OutOfNetwork:
                    return "Out of network.";
                case SttError.PermissionDenied:
                    return "Permission denied.";
                case SttError.RecorderBusy:
                    return "Device or resource busy.";
                case SttError.RecordingTimedOut:
                    return "Recording timed out.";
                case SttError.ServiceReset:
                    return "Service reset.";
                case SttError.TimedOut:
                    return "No answer from the STT service.";
                default:
                    return "Unknown error.";
            }
        }

        /// <summary>
        /// Converts back STT service error message to corresponding error value.
        ///
        /// Not required by the application (not implemented).
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Converter parameter.</param>
        /// <param name="culture">Culture information.</param>
        /// <returns>Converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
