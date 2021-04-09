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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TextReader.Models;
using TextReader.Utils;
using Xamarin.Forms;

namespace TextReader.ViewModels
{
    /// <summary>
    /// Provides abstraction of the main view.
    /// </summary>
    public class TextReaderViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Indicates next paragraph.
        /// </summary>
        private const int NextParagraphIndicator = 1;

        /// <summary>
        /// Indicates previous paragraph.
        /// </summary>
        private const int PreviousParagraphIndicator = -1;

        /// <summary>
        /// Backing field for Paragraphs property.
        /// </summary>
        private List<ParagraphViewModel> _paragraphs;

        /// <summary>
        /// Backing field for ActiveParagraph property.
        /// </summary>
        private ParagraphViewModel _activeParagraph;

        /// <summary>
        /// Data model class instance.
        /// Provides text to create paragraphs' view models.
        /// </summary>
        private readonly DataModel _dataModel;

        /// <summary>
        /// Model class instance used to synthesize text into speech.
        /// </summary>
        private readonly TextToSpeechModel _ttsModel;

        /// <summary>
        /// Backing field for Playing property.
        /// </summary>
        private bool _playing;

        #endregion

        #region properties

        /// <summary>
        /// A command which changes the active paragraph to either previous or next one
        /// depending on the command parameter.
        /// </summary>
        public ICommand ChangeUnitCommand { get; private set; }

        /// <summary>
        /// A command which stops the current utterance on returning from text reader page.
        /// </summary>
        public ICommand ReturnFromTextReaderPageCommand { get; private set; }

        /// <summary>
        /// A command which toggles the play state (playing, paused).
        /// </summary>
        public ICommand TogglePlayStateCommand { get; private set; }

        /// <summary>
        /// Active paragraph view model.
        /// Changing the value will change the current utterance text.
        /// </summary>
        public ParagraphViewModel ActiveParagraph
        {
            get => _activeParagraph;
            set
            {
                if (_ttsModel.Ready && _activeParagraph != value)
                {
                    if (_activeParagraph != null)
                    {
                        _activeParagraph.Active = false;
                    }

                    value.Active = true;
                    _ttsModel.Text = value.Text;

                    SetProperty(ref _activeParagraph, value);
                }
            }
        }

        /// <summary>
        /// List of paragraphs' view models.
        /// Each paragraph contains "text" property and "active" property indicating
        /// if it is an active one.
        /// </summary>
        public List<ParagraphViewModel> Paragraphs
        {
            get => _paragraphs;
        }

        /// <summary>
        /// Flag indicating if the text reader is playing.
        /// </summary>
        public bool Playing
        {
            get => _playing;
            private set => SetProperty(ref _playing, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Initializes a view model instance.
        /// </summary>
        public TextReaderViewModel()
        {
            _dataModel = new DataModel();
            _ttsModel = TextToSpeechModel.Instance;

            _ttsModel.Create();

            _ttsModel.UtteranceCompleted += TtsModelOnUtteranceCompleted;
            _ttsModel.Initialized += TtsModelOnInitialized;

            InitCommands();
            InitParagraphs();

            _playing = false;

            MessagingCenter.Subscribe<Application>(this, "sleep", (sender) =>
            {
                _ttsModel.Pause();
            });

            MessagingCenter.Subscribe<Application>(this, "resume", (sender) =>
            {
                if (Playing)
                {
                    _ttsModel.Play();
                }
            });

            _ttsModel.Init();
        }

        /// <summary>
        /// Initializes view model's commands.
        /// </summary>
        private void InitCommands()
        {
            ChangeUnitCommand = new Command(ExecuteChangeUnit);
            ReturnFromTextReaderPageCommand = new Command(ExecuteReturnFromTextReaderPage);
            TogglePlayStateCommand = new Command(ExecuteTogglePlayState);
        }

        /// <summary>
        /// Initializes paragraphs view models.
        /// Uses data model to obtain text for the reader.
        /// </summary>
        private void InitParagraphs()
        {
            _paragraphs = new List<ParagraphViewModel>();

            _paragraphs = _dataModel.Paragraphs
                .Select(item => new ParagraphViewModel(item, false))
                .ToList();
        }

        /// <summary>
        /// Handles initialized event of the TTS model.
        /// Sets an active paragraph.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void TtsModelOnInitialized(object sender, EventArgs eventArgs)
        {
            _ttsModel.Initialized -= TtsModelOnInitialized;

            ActiveParagraph = _paragraphs[0];
        }

        /// <summary>
        /// Handles UtteranceCompleted event of the TTS model.
        /// Selects the next active paragraph.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void TtsModelOnUtteranceCompleted(object sender, EventArgs eventArgs)
        {
            if (Paragraphs.IndexOf(ActiveParagraph) + NextParagraphIndicator >= _paragraphs.Count)
            {
                _ttsModel.Stop();
                ActiveParagraph = _paragraphs[0];
            }
            else
            {
                ExecuteChangeUnit(UtteranceChangeType.Next);
            }

            Playing = _ttsModel.Playing;
        }

        /// <summary>
        /// Changes active unit command handler.
        /// </summary>
        /// <param name="utteranceChangeType">
        /// Indicates whether to change the active unit to the next or previous one.
        /// </param>
        private void ExecuteChangeUnit(object utteranceChangeType)
        {
            if (_ttsModel.Ready)
            {
                if (utteranceChangeType is UtteranceChangeType utteranceChange)
                {
                    int activeParagraphIndex = Paragraphs.IndexOf(ActiveParagraph);

                    if (utteranceChange == UtteranceChangeType.Next)
                    {
                        if (activeParagraphIndex + NextParagraphIndicator < _paragraphs.Count)
                        {
                            ActiveParagraph = _paragraphs[activeParagraphIndex + NextParagraphIndicator];
                        }
                    }
                    else if (utteranceChange == UtteranceChangeType.Previous)
                    {
                        if (activeParagraphIndex + PreviousParagraphIndicator >= 0)
                        {
                            ActiveParagraph = _paragraphs[activeParagraphIndex + PreviousParagraphIndicator];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles returning from text reader page.
        /// Uses TTS model to stop the utterance.
        /// </summary>
        private void ExecuteReturnFromTextReaderPage()
        {
            if (Playing)
            {
                _ttsModel.Stop();
            }

            MessagingCenter.Unsubscribe<Application>(this, "sleep");
            MessagingCenter.Unsubscribe<Application>(this, "resume");

            _ttsModel.UtteranceCompleted -= TtsModelOnUtteranceCompleted;

            _ttsModel.Dispose();
        }

        /// <summary>
        /// Toggles play state command handler.
        /// Uses TTS model to play or pause the utterance.
        /// </summary>
        private void ExecuteTogglePlayState()
        {
            if (_ttsModel.Ready && !_ttsModel.AnimationRunning)
            {
                if (Playing)
                {
                    _ttsModel.Pause();
                }
                else
                {
                    _ttsModel.Play();
                }

                Playing = _ttsModel.Playing;
            }
        }

        #endregion
    }
}
