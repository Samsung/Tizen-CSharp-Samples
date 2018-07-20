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
using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen.Uix.Stt;
using VoiceMemo.Services;
using VoiceMemo.Tizen.Wearable.Services;
using VoiceMemo.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(SpeechToTextService))]

namespace VoiceMemo.Tizen.Wearable.Services
{
    /// <summary>
    /// SpeechToTextService
    /// The main role is speech recognition.
    /// Use Tizen.Uix.Stt
    /// </summary>
    public class SpeechToTextService : ISpeechToTextService
    {
        SttClient _Stt;

        private Action<Object, SttState, SttState> stateCallback;
        public SpeechToTextService()
        {
            try
            {
                if (_Stt == null)
                {
                    _Stt = new SttClient();
                    _Stt.StateChanged += _Stt_StateChanged;
                    _Stt.DefaultLanguageChanged += _Stt_DefaultLanguageChanged;
                    _Stt.EngineChanged += _Stt_EngineChanged;
                    _Stt.ErrorOccurred += _Stt_ErrorOccurred;
                    _Stt.RecognitionResult += _Stt_RecognitionResult;
                    // Expected State : Created
                    CurrentSttLanguage = _Stt.DefaultLanguage;
                    _Stt.Prepare();
                    Console.WriteLine("DefaultLanguage ..." + _Stt.DefaultLanguage + ", Engine : " + _Stt.Engine);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SttClient  Exception: " + e.Message);
                if (e is NotSupportedException)
                {
                    Console.WriteLine("SttClient is not supported...");
                }
                else if (e is UnauthorizedAccessException)
                {
                    Console.WriteLine("UnauthorizedAccessException...");
                }
            }
        }
        /// <summary>
        /// Start Speech-to-Text service
        /// It means that it starts speech recognition, converting audio to text
        /// </summary>
        public void StartStt()
        {
            _Stt.SetSilenceDetection(SilenceDetection.False);
            _Stt.Start(CurrentSttLanguage, RecognitionType.Partial);
        }
        /// <summary>
        /// Cancel speech recognition
        /// </summary>
        public void Cancel()
        {
            Console.WriteLine("\n\n[Cancel] Before SttClient.Cancel(), state: " + _Stt.CurrentState);
            //To cancel the recording without getting the result, use the stt_cancel() function.
            if (_Stt.CurrentState == State.Recording || _Stt.CurrentState == State.Processing)
            {
                _Stt.Cancel();
            }

            Console.WriteLine("[Cancel] After SttClient.Cancel(), state: " + _Stt.CurrentState);
        }

        TaskCompletionSource<string> taskCompletionSource;
        /// <summary>
        /// Stop speech recognition and get the converted text
        /// </summary>
        /// <returns>text</returns>
        public Task<string> StopAndGetText()
        {
            Console.WriteLine("\n\nBefore SttClient.StopAndGetText(), state: " + _Stt.CurrentState);
            if (_Stt.CurrentState == State.Ready)
            {
                return null;
            }

            // Return Task object
            taskCompletionSource = new TaskCompletionSource<string>();
            //To stop the recording and get the recognition result
            // TODO: Find out a better solution.
            switch (_Stt.CurrentState)
            {
                case State.Processing:
                    //requestToStop = true;
                    MessagingCenter.Subscribe<ISpeechToTextService>(this, MessageKeys.CanStopSTT, (obj) =>
                    {
                        _Stt.Stop();
                        MessagingCenter.Unsubscribe<ISpeechToTextService>(this, MessageKeys.CanStopSTT);
                    });
                    break;
                case State.Recording:
                    try
                    {
                        _Stt.Stop();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[StopAndGetText] _Stt.Stop() --> Error e : " + e.Message + ",  " + e.Source + ", " + e.StackTrace + ", " + e.GetType());
                    }

                    break;
                default:
                    break;
            }

            return taskCompletionSource.Task;
        }
        /// <summary>
        /// Dispose Speech-to-Text service
        /// </summary>
        public void Dispose()
        {
            Console.WriteLine("SpeechToTextService.Dispose()");
            try
            {
                if (_Stt == null)
                {
                    return;
                }

                _Stt.DefaultLanguageChanged -= _Stt_DefaultLanguageChanged;
                _Stt.EngineChanged -= _Stt_EngineChanged;
                _Stt.ErrorOccurred -= _Stt_ErrorOccurred;
                _Stt.RecognitionResult -= _Stt_RecognitionResult;
                _Stt.StateChanged -= _Stt_StateChanged;
                _Stt.Unprepare();
                _Stt.Dispose();
                _Stt = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("SpeechToTextService.Dispose() : Error - " + e.Message);  // Invalid Parameters Provided
            }
        }

        /// <summary>
        ///     Get installed languages.
        /// </summary>
        /// <returns>
        ///     The installed language names.
        /// </returns>
        public IEnumerable<string> GetInstalledLanguages()
        {
            return _Stt.GetSupportedLanguages();
        }

        string _currentSttLanguage;

        /// <summary>
        /// The chosen language for Speech-to-Text service
        /// </summary>
        public string CurrentSttLanguage
        {
            get
            {
                return _currentSttLanguage;
            }

            set
            {
                if (_currentSttLanguage != value)
                {
                    _currentSttLanguage = value;
                }
            }
        }
        /// <summary>
        /// The state of Speech-to-Text service
        /// </summary>
        public SttState SttState
        {
            get
            {
                return (SttState)_Stt.CurrentState;
            }
        }

        private void _Stt_RecognitionResult(object sender, RecognitionResultEventArgs arg)
        {
            Console.WriteLine("_Stt_RecognitionResult Message: " + arg.Message);
            Console.WriteLine("_Stt_RecognitionResult Result: " + arg.Result);
            Console.WriteLine("_Stt_RecognitionResult DataCount : " + arg.DataCount);
            Console.WriteLine("_Stt_RecognitionResult Data : " + arg.Data);
            Console.WriteLine(" **** _Stt_RecognitionResult : " + arg.DataCount + ", " + arg.Data + ", " + arg.Message + ", " + arg.Result);
            Console.WriteLine(" **** _Stt_RecognitionResult ---  _Stt.CurrentState : " + _Stt.CurrentState);

            string resultText = "";
            if (arg.Result == ResultEvent.FinalResult && taskCompletionSource != null)
            {
                if (arg.Data != null)
                {
                    foreach (var part in arg.Data)
                    {
                        Console.WriteLine("text : (" + part + ")");
                        resultText += part;
                        Console.WriteLine("resultText : (" + resultText + ")");
                    }

                    if (string.IsNullOrEmpty(resultText))
                    {
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!                resultText is string.IsNullOrEmpty");
                    }

                    //if (resultText.Equals("") || resultText.Equals(" ") || resultText.Equals("  "))
                    //{
                    //    Console.WriteLine("result data is empty. so assign Recognition failed");
                    //    resultText = AppResources.RecognitionFailed;
                    //}

                    taskCompletionSource.SetResult(resultText);
                    //System.InvalidOperationException: An attempt was made to transition a task to a final state when it had already completed.
                    //    at System.ThrowHelper.ThrowInvalidOperationException(ExceptionResource resource)
                    //    at System.Threading.Tasks.TaskCompletionSource`1.SetResult(TResult result)
                    taskCompletionSource = null;
                }
            }
        }

        /// <summary>
        /// Invoked when an error occurs
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ErrorOccurredEventArgs</param>
        private void _Stt_ErrorOccurred(object sender, ErrorOccurredEventArgs e)
        {
            Console.WriteLine(" **** _Stt_ErrorOccurred : (" + e.ErrorMessage + "), " + e.ErrorValue);
            if (taskCompletionSource != null)
            {
                taskCompletionSource.SetResult("Stt Error:" + e.ErrorValue);
            }
        }

        /// <summary>
        /// Invoked when Speech-to-Text engine has been changed
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EngineChangedEventArgs</param>
        private void _Stt_EngineChanged(object sender, EngineChangedEventArgs e)
        {
            Console.WriteLine(" ----_Stt_EngineChanged : " + e.EngineId + ", " + e.Language + ", " + e.NeedCredential + ", " + e.SupportSilence);
        }

        /// <summary>
        /// Invoked when the default language for Speech-to-Text service has been changed
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">DefaultLanguageChangedEventArgs</param>
        private void _Stt_DefaultLanguageChanged(object sender, DefaultLanguageChangedEventArgs e)
        {
            Console.WriteLine(" ---- _Stt_DefaultLanguageChanged : " + e.PreviousLanguage + " ---> " + e.CurrentLanguage);
        }

        /// <summary>
        /// Invoked when the state of Speech-To-Text service has been changed
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">StateChangedEventArgs</param>
        private void _Stt_StateChanged(object sender, StateChangedEventArgs e)
        {
            Console.WriteLine(" ---- _Stt_StateChanged : " + e.Previous + " ---> " + e.Current);
            stateCallback?.Invoke(sender, (SttState)e.Previous, (SttState)e.Current);
        }

        public void RegisterStateCallbacks(Action<object, SttState, SttState> callback)
        {
            stateCallback = callback;
        }
    }
}
