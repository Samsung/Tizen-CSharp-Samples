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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TextReader.Models;
using Xamarin.Forms;

namespace TextReader.ViewModels
{
    /// <summary>
    /// Text Reader view model class.
    /// Provides abstraction of the main view.
    /// </summary>
    public class TextReaderViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// List of paragraphs view models.
        /// Each paragraph contains "text" property and "active" property indicating
        /// if it is active one.
        /// </summary>
        private List<ParagraphViewModel> _paragraphs;

        /// <summary>
        /// Reference to the currently active paragraph's view model.
        /// </summary>
        private ParagraphViewModel _activeParagraph;

        /// <summary>
        /// Data model class instance.
        /// Provides text to create paragraph view models.
        /// </summary>
        private readonly DataModel _dataModel;

        /// <summary>
        /// Model class instance used to synthesize text into speech.
        /// </summary>
        private readonly TextToSpeechModel _ttsModel;

        /// <summary>
        /// Flag indicating if repeat unit option is active.
        /// </summary>
        private bool _repeatUnitActive;

        /// <summary>
        /// Flag indicating if repeat all option is active.
        /// </summary>
        private bool _repeatAllActive;

        /// <summary>
        /// Flag indicating if text reader is playing.
        /// </summary>
        private bool _playing;

        /// <summary>
        /// Playing flag saved state.
        /// Used to restore the state of the reader.
        /// </summary>
        private bool _playingSavedState;

        #endregion

        #region properties

        /// <summary>
        /// A command which initializes reset process (confirmation dialog).
        /// </summary>
        public ICommand ResetCommand { get; private set; }

        /// <summary>
        /// A command which toggles the repeat unit option state (on/off).
        /// </summary>
        public ICommand ToggleRepeatUnitCommand { get; private set; }

        /// <summary>
        /// A command which toggles the repeat all option state (on/off).
        /// </summary>
        public ICommand ToggleRepeatAllCommand { get; private set; }

        /// <summary>
        /// A command which changes the active paragraph to the previous one.
        /// </summary>
        public ICommand GoToPreviousUnitCommand { get; private set; }

        /// <summary>
        /// A command which changes the active paragraph to the next one.
        /// </summary>
        public ICommand GoToNextUnitCommand { get; private set; }

        /// <summary>
        /// A command which toggles the play state (playing, paused).
        /// </summary>
        public ICommand TogglePlayStateCommand { get; private set; }

        /// <summary>
        /// Command which shows reset confirmation dialog.
        /// The command is injected into view model.
        /// </summary>
        public ICommand ResetConfirmationDialogCommand { get; set; }

        /// <summary>
        /// A command which resets the application state.
        /// </summary>
        public ICommand ResetConfirmedCommand { get; private set; }

        /// <summary>
        /// Active paragraph view model.
        /// Changing the value will change the current utterance text.
        /// </summary>
        public ParagraphViewModel ActiveParagraph
        {
            get { return _activeParagraph; }
            set
            {
                if (!_ttsModel.Ready)
                {
                    return;
                }

                if (_activeParagraph == value)
                {
                    return;
                }

                if (_activeParagraph != null)
                {
                    _activeParagraph.Active = false;
                }

                if (value != null)
                {
                    value.Active = true;
                    _ttsModel.Text = value.Text;
                }
                else
                {
                    _ttsModel.Stop();
                    Playing = _ttsModel.Playing;
                }

                SetProperty(ref _activeParagraph, value);
                ((Command)ResetCommand).ChangeCanExecute();
            }
        }

        /// <summary>
        /// List of paragraphs view models.
        /// Each paragraph contains "text" property and "active" property indicating
        /// if it is active one.
        /// </summary>
        public List<ParagraphViewModel> Paragraphs
        {
            get { return _paragraphs; }
        }

        /// <summary>
        /// Flag indicating if repeat unit option is active.
        /// </summary>
        public bool RepeatUnitActive
        {
            get { return _repeatUnitActive; }
            private set { SetProperty(ref _repeatUnitActive, value); }
        }

        /// <summary>
        /// Flag indicating if repeat all option is active.
        /// </summary>
        public bool RepeatAllActive
        {
            get { return _repeatAllActive; }
            private set { SetProperty(ref _repeatAllActive, value); }
        }

        /// <summary>
        /// Flag indicating if text reader is playing.
        /// </summary>
        public bool Playing
        {
            get { return _playing; }
            private set { SetProperty(ref _playing, value); }
        }

        #endregion

        #region methods

        /// <summary>
        /// TextReaderViewModel class constructor.
        /// </summary>
        public TextReaderViewModel()
        {
            _dataModel = new DataModel();
            _ttsModel = new TextToSpeechModel();

            InitCommands();
            InitParagraphs();

            _repeatUnitActive = false;
            _repeatAllActive = false;
            _playing = false;

            _ttsModel.UtteranceCompleted += TtsModelOnUtteranceCompleted;
            _ttsModel.Init();
        }

        /// <summary>
        /// Handles UtteranceCompleted event of the TTS model.
        /// Depending on the repeat unit and repeat all options,
        /// selects proper next active paragraph.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void TtsModelOnUtteranceCompleted(object sender, EventArgs eventArgs)
        {
            if (RepeatUnitActive && ActiveParagraph != null)
            {
                _ttsModel.Text = ActiveParagraph.Text;
            }
            else if (_paragraphs.IndexOf(ActiveParagraph) != _paragraphs.Count - 1)
            {
                // go to next unit if not last one
                ExecuteGoToNextUnit();
            }
            else
            {
                ActiveParagraph = RepeatAllActive ? _paragraphs[0] : null;
            }

            Playing = _ttsModel.Playing;
        }

        /// <summary>
        /// Initializes view model's commands.
        /// </summary>
        private void InitCommands()
        {
            ResetCommand = new Command(ExecuteReset, CanExecuteReset);
            ToggleRepeatUnitCommand = new Command(ExecuteToggleRepeatUnit);
            ToggleRepeatAllCommand = new Command(ExecuteToggleRepeatAll);
            GoToPreviousUnitCommand = new Command(ExecuteGoToPreviousUnit);
            GoToNextUnitCommand = new Command(ExecuteGoToNextUnit);
            TogglePlayStateCommand = new Command(ExecuteTogglePlayState);
            ResetConfirmedCommand = new Command<bool>(ExecuteResetConfirmed);
        }

        /// <summary>
        /// Initializes paragraphs view models.
        /// Uses data model to obtain text for the reader.
        /// </summary>
        private void InitParagraphs()
        {
            _paragraphs = new List<ParagraphViewModel>();

            _paragraphs = _dataModel.Paragraphs.
                Select(item => new ParagraphViewModel(item, false))
                .ToList();

            ActiveParagraph = null;
        }

        /// <summary>
        /// Returns true if reset command can be executed, false otherwise.
        /// </summary>
        /// <returns>True if reset command can be executed, false otherwise</returns>
        private bool CanExecuteReset()
        {
            return ActiveParagraph != null;
        }

        /// <summary>
        /// Reset command handler.
        /// Saves the state of the playing and shows confirmation dialog.
        /// </summary>
        private void ExecuteReset()
        {
            _playingSavedState = Playing;

            if (Playing)
            {
                _ttsModel.Pause();
            }

            Playing = _ttsModel.Playing;

            ResetConfirmationDialogCommand?.Execute(null);
        }

        /// <summary>
        /// Reset confirmed command handler.
        /// Restores default state of the reader if user confirmed reset,
        /// restores only playing state otherwise.
        /// </summary>
        /// <param name="confirmed">Confirmation result.</param>
        private void ExecuteResetConfirmed(bool confirmed)
        {
            if (confirmed)
            {
                ActiveParagraph = null;
                RepeatAllActive = false;
                RepeatUnitActive = false;
            }
            else
            {
                if (!Playing && _playingSavedState)
                {
                    _ttsModel.Play();
                }

                Playing = _ttsModel.Playing;
            }
        }

        /// <summary>
        /// Toggle repeat unit command handler.
        /// Toggles the state of repeat unit option.
        /// </summary>
        private void ExecuteToggleRepeatUnit()
        {
            RepeatUnitActive = !RepeatUnitActive;
        }

        /// <summary>
        /// Toggle repeat all command handler.
        /// Toggles the state of the repeat all option.
        /// </summary>
        private void ExecuteToggleRepeatAll()
        {
            RepeatAllActive = !RepeatAllActive;
        }

        /// <summary>
        /// Go to previous unit command handler.
        /// </summary>
        private void ExecuteGoToPreviousUnit()
        {
            if (!_ttsModel.Ready || ActiveParagraph == null)
            {
                return;
            }

            int activeUnitIndex = _paragraphs.IndexOf(ActiveParagraph);

            ActiveParagraph = activeUnitIndex - 1 < 0 ?
                null : _paragraphs[activeUnitIndex - 1];
        }

        /// <summary>
        /// Go to next unit command handler.
        /// </summary>
        private void ExecuteGoToNextUnit()
        {
            if (!_ttsModel.Ready)
            {
                return;
            }

            int activeUnitIndex = _paragraphs.IndexOf(ActiveParagraph);

            ActiveParagraph = activeUnitIndex + 1 >= _paragraphs.Count ?
                _paragraphs[0] : _paragraphs[activeUnitIndex + 1];

        }

        /// <summary>
        /// Toggle play state command handler.
        /// Uses TTS model to play or pause the utterance.
        /// </summary>
        private void ExecuteTogglePlayState()
        {
            if (!_ttsModel.Ready)
            {
                return;
            }

            if (ActiveParagraph == null)
            {
                ExecuteGoToNextUnit();
            }

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

        #endregion
    }
}
