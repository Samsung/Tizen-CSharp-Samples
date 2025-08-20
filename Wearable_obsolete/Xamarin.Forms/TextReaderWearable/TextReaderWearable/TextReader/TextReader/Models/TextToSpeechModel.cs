/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
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
using Xamarin.Forms;

namespace TextReader.Models
{
    /// <summary>
    /// Model class which handles synthesizing text into speech.
    /// This is singleton. Instance is accessible via <see cref="Instance">Instance</see></cref> property.
    /// </summary>
    public sealed class TextToSpeechModel : IDisposable
    {
        #region fields

        /// <summary>
        /// Backing field of Instance property.
        /// </summary>
        private static TextToSpeechModel _instance;

        /// <summary>
        /// Instance of the TextToSpeechService class.
        /// </summary>
        private ITextToSpeechService _service;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked when the model is initialized.
        /// </summary>
        public event EventHandler Initialized;

        /// <summary>
        /// Event fired when the utterance is completed.
        /// </summary>
        public event EventHandler UtteranceCompleted;

        /// <summary>
        /// Flag indicating if the service is ready to use.
        /// The model requires "Init" method to be called before use.
        /// </summary>
        public bool Ready => _service.Ready;

        /// <summary>
        /// Currently used text.
        /// If the text is changed, the current utterance is stopped immediately
        /// and then starts new one.
        /// </summary>
        public string Text
        {
            get => _service.Text;
            set => _service.Text = value;
        }

        /// <summary>
        /// Flag indicating if the text is being read by the TTS engine.
        /// </summary>
        public bool Playing => _service.Playing;

        /// <summary>
        /// Flag indicating if pause/play animation is running.
        /// </summary>
        public bool AnimationRunning { get; set; }

        /// <summary>
        /// MainModel instance accessor.
        /// </summary>
        public static TextToSpeechModel Instance
        {
            get => _instance ?? (_instance = new TextToSpeechModel());
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes the class instance.
        /// </summary>
        private TextToSpeechModel()
        {
            _service = DependencyService.Get<ITextToSpeechService>();
        }

        /// <summary>
        /// Handles state changed to 'ready' event of the service.
        /// Invokes Initialized event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void ServiceOnStateChangedToReady(object sender, EventArgs eventArgs)
        {
            _service.StateChangedToReady -= ServiceOnStateChangedToReady;
            Initialized?.Invoke(this, null);
        }

        /// <summary>
        /// Handles utterance completed event from the service.
        /// Invokes UtteranceCompleted event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void ServiceOnUtteranceCompleted(object sender, EventArgs eventArgs)
        {
            UtteranceCompleted?.Invoke(this, null);
        }

        /// <summary>
        /// Creates instance of the service.
        /// </summary>
        public void Create()
        {
            _service.Create();
        }

        /// <summary>
        /// Releases resources used by the service.
        /// </summary>
        public void Dispose()
        {
            _service.Dispose();
        }

        /// <summary>
        /// Initializes the model.
        /// The model instance can be used when the "Ready" property returns "true" value.
        /// </summary>
        public void Init()
        {
            _service.Init();
            _service.StateChangedToReady += ServiceOnStateChangedToReady;
            _service.UtteranceCompleted += ServiceOnUtteranceCompleted;
        }

        /// <summary>
        /// Starts or resumes the current utterance.
        /// </summary>
        public void Play()
        {
            _service.Play();
        }

        /// <summary>
        /// Pauses the current utterance.
        /// </summary>
        public void Pause()
        {
            _service.Pause();
        }

        /// <summary>
        /// Stops and clears the current utterance.
        /// </summary>
        public void Stop()
        {
            _service.Stop();
        }

        #endregion
    }
}
