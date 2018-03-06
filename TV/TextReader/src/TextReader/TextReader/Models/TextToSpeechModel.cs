/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Xamarin.Forms;

namespace TextReader.Models
{
    /// <summary>
    /// Model class which handles synthesizing text into speech.
    /// </summary>
    class TextToSpeechModel
    {
        #region properties

        /// <summary>
        /// Event fired when the utterance is completed.
        /// </summary>
        public event EventHandler UtteranceCompleted ;

        /// <summary>
        /// Flag indicating if service is ready to use.
        /// The model requires "Init" method to be called before use.
        /// </summary>
        public bool Ready => DependencyService.Get<ITextToSpeechService>().Ready;

        /// <summary>
        /// Currently used text.
        /// If text is changed, the current utterance is stopped immediately
        /// and new one starts then.
        /// </summary>
        public string Text
        {
            get { return DependencyService.Get<ITextToSpeechService>().Text; }
            set { DependencyService.Get<ITextToSpeechService>().Text = value; }
        }

        /// <summary>
        /// Flag indicating if text is read by the TTS engine.
        /// </summary>
        public bool Playing => DependencyService.Get<ITextToSpeechService>().Playing;

        #endregion

        #region methods

        /// <summary>
        /// The model class constructor.
        /// </summary>
        public TextToSpeechModel()
        {
            var service = DependencyService.Get<ITextToSpeechService>();

            service.UtteranceCompleted += ServiceOnUtteranceCompleted;
        }

        /// <summary>
        /// Handles utterance completed event from the service.
        /// Invokes UtteranceCompleted event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void ServiceOnUtteranceCompleted(object sender, EventArgs eventArgs)
        {
            UtteranceCompleted?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Initializes the model.
        /// The model instance can be used when the "Ready" property returns "true" value.
        /// </summary>
        public void Init()
        {
            DependencyService.Get<ITextToSpeechService>().Init();
        }

        /// <summary>
        /// Starts or resumes the current utterance.
        /// </summary>
        public void Play()
        {
            DependencyService.Get<ITextToSpeechService>().Play();
        }

        /// <summary>
        /// Pauses the current utterance.
        /// </summary>
        public void Pause()
        {
            DependencyService.Get<ITextToSpeechService>().Pause();
        }

        /// <summary>
        /// Stops and clears the current utterance.
        /// </summary>
        public void Stop()
        {
            DependencyService.Get<ITextToSpeechService>().Stop();
        }

        #endregion
    }
}
