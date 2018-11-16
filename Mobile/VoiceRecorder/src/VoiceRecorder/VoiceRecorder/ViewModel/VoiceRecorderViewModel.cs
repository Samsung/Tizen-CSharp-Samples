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
using System.Collections.ObjectModel;
using System.Linq;
using VoiceRecorder.Model;
using VoiceRecorder.Utils;
using VoiceRecorder.View;
using Xamarin.Forms;

namespace VoiceRecorder.ViewModel
{
    /// <summary>
    /// VoiceRecorderViewModel class.
    /// Provides commands and methods responsible for application view model state.
    /// </summary>
    public class VoiceRecorderViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Maximum recording length.
        /// </summary>
        private const int MAX_RECORDING_LENGTH = 3000;

        /// <summary>
        /// Number of images used to show progress while recording and playing.
        /// </summary>
        private const int NUMBER_OF_PROGRESS_IMAGES = 8;

        /// <summary>
        /// Progress images naming template.
        /// </summary>
        private const string PROGRESS_IMAGE_TEMPLATE = "progress{0}.png";

        /// <summary>
        /// Timer interval (in milliseconds).
        /// </summary>
        private const int TIMER_PERIOD = 100;

        /// <summary>
        /// Divider to turn tenths of a second into seconds.
        /// </summary>
        private const int TO_SECONDS_DIVIDER = 10;

        /// <summary>
        /// Divider to turn seconds into minuets.
        /// </summary>
        private const int TO_MINUTES_DIVIDER = 60;

        /// <summary>
        /// Private backing field for IsPlaying property.
        /// </summary>
        private bool _isPlaying;

        /// <summary>
        /// Private backing field for IsRecording property.
        /// </summary>
        private bool _isRecording;

        /// <summary>
        /// Private backing field for IsStereo property.
        /// </summary>
        private bool _isStereo = true;

        /// <summary>
        /// Indicates last remembered position of the playing voice stream.
        /// </summary>
        private int _lastPlayerPosition;

        /// <summary>
        /// Private backing field for RecordingLength property.
        /// </summary>
        private int _recordingLength;

        /// <summary>
        /// Private backing field for CurrentFileFormat property.
        /// </summary>
        private FileFormatType _currentFileFormat;

        /// <summary>
        /// Private backing field for CurrentRecordingQuality property.
        /// </summary>
        private AudioBitRateType _currentRecordingQuality;

        /// <summary>
        /// Path to the sample which is being recorded at the moment.
        /// If the recording service is inactive, the value is null.
        /// </summary>
        private string _currentRecordingPath;

        /// <summary>
        /// Private backing field for ErrorMessage property.
        /// </summary>
        private string _errorMessage;

        /// <summary>
        /// Path to the file to delete.
        /// </summary>
        private string _fileToDelete;

        /// <summary>
        /// Private backing field for LastSavedRecordingName property.
        /// </summary>
        private string _lastSavedRecordingName;

        /// <summary>
        /// Private backing field for PlayingTime property.
        /// </summary>
        private string _playingTime;

        /// <summary>
        /// Private backing field for ProgressImage property.
        /// </summary>
        private string _progressImage;

        /// <summary>
        /// Private backing field for RecordingTime property.
        /// </summary>
        private string _recordingTime;

        private IRecordingFilesReader _recordingFilesReader;

        /// <summary>
        /// An instance of VoicePlayerModel class.
        /// </summary>
        private VoicePlayerModel _voicePlayerModel;

        /// <summary>
        /// An instance of VoiceRecorderModel class.
        /// </summary>
        private VoiceRecorderModel _voiceRecorderModel;

        /// <summary>
        /// Private backing field for ReturnFromPlayerPageCommand property.
        /// </summary>
        private Command _returnFromPlayerPageCommand;

        /// <summary>
        /// Private backing field for CancelRecordingCommand property.
        /// </summary>
        private Command _cancelRecordingCommand;

        /// <summary>
        /// Private backing field for ChangePageCommand property.
        /// </summary>
        private Command _changePageCommand;

        /// <summary>
        /// Private backing field for DeleteConfirmCommand property.
        /// </summary>
        private Command _deleteConfirmCommand;

        /// <summary>
        /// Private backing field for DeleteItemCommand property.
        /// </summary>
        private Command _deleteItemCommand;

        /// <summary>
        /// Private backing field for FastForwardVoiceStreamCommand property.
        /// </summary>
        private Command _fastForwardVoiceStreamCommand;

        /// <summary>
        /// Private backing field for GoToPlayerCommand property.
        /// </summary>
        private Command _goToPlayerCommand;

        /// <summary>
        /// Private backing field for PauseRecordingCommand property.
        /// </summary>
        private Command _pauseRecordingCommand;

        /// <summary>
        /// Private backing field for RewindVoiceStreamCommand property.
        /// </summary>
        private Command _rewindVoiceStreamCommand;

        /// <summary>
        /// Private backing field for StartPlayingCommand property.
        /// </summary>
        private Command _startPlayingCommand;

        /// <summary>
        /// Private backing field for StartRecordingCommand property.
        /// </summary>
        private Command _startRecordingCommand;

        /// <summary>
        /// Private backing field for StopRecordingCommand property.
        /// </summary>
        private Command _stopRecordingCommand;

        /// <summary>
        /// Private backing field for UpdateFileFormatCommand property.
        /// </summary>
        private Command _updateFileFormatCommand;

        /// <summary>
        /// Private backing field for UpdateRecordingQualityCommand property.
        /// </summary>
        private Command _updateRecordingQualityCommand;

        /// <summary>
        /// Private backing field for RecordingsCollection property.
        /// </summary>
        private ObservableCollection<RecordedFileListItem> _recordingsCollection;

        /// <summary>
        /// Private backing field for SelectedRecordFile property.
        /// </summary>
        private RecordedFileListItem _selectedRecordFile;

        /// <summary>
        /// Private backing field for SettingsCollection property.
        /// </summary>
        private ObservableCollection<SettingsItem> _settingsCollection;

        /// <summary>
        /// Enumerator that contains options to control the player position.
        /// </summary>
        private enum SeekPlayerPosition
        {
            /// <summary>
            /// Indicates the stream fast-forwarding.
            /// </summary>
            FastForward = 1,

            /// <summary>
            /// Indicates the stream rewinding.
            /// </summary>
            Rewind = -1
        }

        #endregion

        #region properties

        /// <summary>
        /// Flag indicating if the stream is playing.
        /// </summary>
        public bool IsPlaying
        {
            get => _isPlaying;
            set => SetProperty(ref _isPlaying, value);
        }

        /// <summary>
        /// Flag indicating if the stream is recording.
        /// </summary>
        public bool IsRecording
        {
            get => _isRecording;
            set => SetProperty(ref _isRecording, value);
        }

        /// <summary>
        /// Flag indicating if the recorder records in stereo mode.
        /// </summary>
        public bool IsStereo
        {
            get => _isStereo;
            set
            {
                SetProperty(ref _isStereo, value);
                _voiceRecorderModel.UpdateRecorderStereo(_isStereo);
            }
        }

        /// <summary>
        /// Length of the recording voice stream.
        /// </summary>
        public int RecordingLength
        {
            get => _recordingLength;
            set => SetProperty(ref _recordingLength, value);
        }

        /// <summary>
        /// Name of the file format used by the recorder.
        /// </summary>
        public FileFormatType CurrentFileFormat
        {
            get => _currentFileFormat;
            set => SetProperty(ref _currentFileFormat, value);
        }

        /// <summary>
        /// Name of the recording quality used by the recorder.
        /// </summary>
        public AudioBitRateType CurrentRecordingQuality
        {
            get => _currentRecordingQuality;
            set => SetProperty(ref _currentRecordingQuality, value);
        }

        /// <summary>
        /// Message of an error which has occurred.
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        /// <summary>
        /// Name of the last saved recording.
        /// </summary>
        public string LastSavedRecordingName
        {
            get => _lastSavedRecordingName;
            set => SetProperty(ref _lastSavedRecordingName, value);
        }

        /// <summary>
        /// Indicates how much time of the playing stream left (in form minutes:seconds).
        /// </summary>
        public string PlayingTime
        {
            get => _playingTime;
            set => SetProperty(ref _playingTime, value);
        }

        /// <summary>
        /// Name of an image used to present the recording/playing progress.
        /// </summary>
        public string ProgressImage
        {
            get => _progressImage;
            set => SetProperty(ref _progressImage, value);
        }

        /// <summary>
        /// Indicates length of the recording stream (in form minutes:seconds).
        /// </summary>
        public string RecordingTime
        {
            get => _recordingTime;
            set => SetProperty(ref _recordingTime, value);
        }

        /// <summary>
        /// Command which is executed when back button is pressed on player page.
        /// </summary>
        public Command ReturnFromPlayerPageCommand
        {
            get => _returnFromPlayerPageCommand;
            set => _returnFromPlayerPageCommand = value;
        }

        /// <summary>
        /// Command which cancels the recording.
        /// </summary>
        public Command CancelRecordingCommand
        {
            get => _cancelRecordingCommand;
            set => _cancelRecordingCommand = value;
        }

        /// <summary>
        /// Command for changing page to the one specified in parameter.
        /// </summary>
        public Command ChangePageCommand
        {
            get => _changePageCommand;
            set => _changePageCommand = value;
        }

        /// <summary>
        /// Command which deletes file.
        /// </summary>
        public Command DeleteConfirmCommand
        {
            get => _deleteConfirmCommand;
            set => _deleteConfirmCommand = value;
        }

        /// <summary>
        /// Command which sets file to delete.
        /// </summary>
        public Command DeleteItemCommand
        {
            get => _deleteItemCommand;
            set => _deleteItemCommand = value;
        }

        /// <summary>
        /// Command which shows delete confirmation pop-up.
        /// The command is injected into view model.
        /// </summary>
        public Command DeleteShowAlertCommand { get; set; }

        /// <summary>
        /// Command which fast-forwads the playing stream.
        /// </summary>
        public Command FastForwardVoiceStreamCommand
        {
            get => _fastForwardVoiceStreamCommand;
            set => _fastForwardVoiceStreamCommand = value;
        }

        /// <summary>
        /// Command which changes page to the player page with a selected recording (specified in command parameter).
        /// </summary>
        public Command GoToPlayerCommand
        {
            get => _goToPlayerCommand;
            set => _goToPlayerCommand = value;
        }

        /// <summary>
        /// Command which pauses the recording.
        /// </summary>
        public Command PauseRecordingCommand
        {
            get => _pauseRecordingCommand;
            set => _pauseRecordingCommand = value;
        }

        /// <summary>
        /// Command which rewinds the playing stream.
        /// </summary>
        public Command RewindVoiceStreamCommand
        {
            get => _rewindVoiceStreamCommand;
            set => _rewindVoiceStreamCommand = value;
        }

        /// <summary>
        /// Command which shows message about saved recording.
        /// The command is injected into view model.
        /// </summary>
        public Command SavedRecordingInfoCommand { get; set; }

        /// <summary>
        /// Command which starts playing the stream.
        /// </summary>
        public Command StartPlayingCommand
        {
            get => _startPlayingCommand;
            set => _startPlayingCommand = value;
        }

        /// <summary>
        /// Command which starts recording the stream.
        /// </summary>
        public Command StartRecordingCommand
        {
            get => _startRecordingCommand;
            set => _startRecordingCommand = value;
        }

        /// <summary>
        /// Command which stops the recording.
        /// </summary>
        public Command StopRecordingCommand
        {
            get => _stopRecordingCommand;
            set => _stopRecordingCommand = value;
        }

        /// <summary>
        /// Command which updates the file format setting of the recorder.
        /// </summary>
        public Command UpdateFileFormatCommand
        {
            get => _updateFileFormatCommand;
            set => _updateFileFormatCommand = value;
        }

        /// <summary>
        /// Command which updates the recording quality setting of the recorder.
        /// </summary>
        public Command UpdateRecordingQualityCommand
        {
            get => _updateRecordingQualityCommand;
            set => _updateRecordingQualityCommand = value;
        }

        /// <summary>
        /// Command which changes page to main application page.
        /// </summary>
        public Command WelcomePageCommand { get; set; }

        /// <summary>
        /// Recording file selected from the list.
        /// </summary>
        public RecordedFileListItem SelectedRecordFile
        {
            get => _selectedRecordFile;
            set => SetProperty(ref _selectedRecordFile, value);
        }

        /// <summary>
        /// List of recording files.
        /// </summary>
        public ObservableCollection<RecordedFileListItem> RecordingsCollection
        {
            get => _recordingsCollection;
            set => SetProperty(ref _recordingsCollection, value);
        }

        /// <summary>
        /// List of settings possible to change.
        /// </summary>
        public ObservableCollection<SettingsItem> SettingsCollection
        {
            get => _settingsCollection;
            set => SetProperty(ref _settingsCollection, value);
        }

        /// <summary>
        /// Command which shows message about occurred error.
        /// The command is injected into view model.
        /// </summary>
        public Command ErrorInfoCommand { get; set; }

        /// <summary>
        /// Command which shows message about denied privilege (application close).
        /// The command is injected into view model.
        /// </summary>
        public Command PrivilegeDeniedInfoCommand { get; set; }

        /// <summary>
        /// Command which handles confirmation of privilege denied dialog.
        /// </summary>
        public Command PrivilegeDeniedConfirmedCommand { get; set; }

        /// <summary>
        /// Command which executes command with file format options pop-up.
        /// </summary>
        public Command SettingFileFormatPopupCommand { get; set; }

        /// <summary>
        /// Command which shows pop-up with file format setting options.
        /// The command is injected into view model.
        /// </summary>
        public Command ShowSettingFileFormatPopupCommand { get; set; }

        /// <summary>
        /// Command which executes command with recording quality options pop-up.
        /// </summary>
        public Command SettingRecordingQualityPopupCommand { get; set; }

        /// <summary>
        /// Command which shows pop-up with recording quality setting options.
        /// The command is injected into view model.
        /// </summary>
        public Command ShowSettingRecordingQualityPopupCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        public VoiceRecorderViewModel()
        {
            InitCommands();
            InitSettings();
            InitVoiceRecorder();
            InitVoicePlayer();
            InitRecordingFilesReader();
            RecordingTime = CurrentTimerValue(RecordingLength);
        }

        /// <summary>
        /// Initializes RecordingFilesReader.
        /// </summary>
        private void InitRecordingFilesReader()
        {
            _recordingFilesReader = DependencyService.Get<IRecordingFilesReader>();
            _recordingFilesReader.ErrorOccurred += OnErrorOccurred;
            _recordingFilesReader.ItemDeleted += OnItemDeleted;
            UpdateListOfRecordings();
        }

        /// <summary>
        /// Initializes Settings.
        /// </summary>
        private void InitSettings()
        {
            SettingsCollection = new ObservableCollection<SettingsItem>
            {
                new SettingsItem(SettingsItemType.Stereo),
                new SettingsItem(SettingsItemType.RecordingQuality, SettingRecordingQualityPopupCommand),
                new SettingsItem(SettingsItemType.FileFormat, SettingFileFormatPopupCommand)
            };
        }

        /// <summary>
        /// Initializes the Voice Recorder.
        /// </summary>
        private void InitVoiceRecorder()
        {
            _voiceRecorderModel = new VoiceRecorderModel();
            _voiceRecorderModel.VoiceRecorderPaused += OnRecorderPaused;
            _voiceRecorderModel.VoiceRecorderResumed += OnRecorderResumed;
            _voiceRecorderModel.VoiceRecorderStarted += OnRecorderStarted;
            _voiceRecorderModel.VoiceRecorderStopped += OnRecorderStopped;
            _voiceRecorderModel.FileFormatUpdated += OnRecorderFileFormatUpdated;
            _voiceRecorderModel.RecordingQualityUpdated += OnRecordingQualityUpdated;
            _voiceRecorderModel.ErrorOccurred += OnErrorOccurred;
            _voiceRecorderModel.RecordingSaved += OnRecordingSaved;
            _voiceRecorderModel.Init();
        }

        /// <summary>
        /// Initializes the Player.
        /// </summary>
        private void InitVoicePlayer()
        {
            _lastPlayerPosition = 0;
            _voicePlayerModel = new VoicePlayerModel();
            _voicePlayerModel.VoiceStreamPositionSought += OnVoiceStreamPositionSought;
            _voicePlayerModel.ItemDeleted += OnItemDeleted;
            _voicePlayerModel.VoicePlayerPaused += OnPlayerPaused;
            _voicePlayerModel.VoicePlayerStarted += OnPlayerStarted;
            _voicePlayerModel.VoicePlayerStopped += OnPlayerStopped;
            _voicePlayerModel.ErrorOccurred += OnErrorOccurred;
            _voicePlayerModel.Init();
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        private void InitCommands()
        {
            SettingFileFormatPopupCommand = new Command(ExecuteSettingFileFormatPopupCommand);
            SettingRecordingQualityPopupCommand = new Command(ExecuteSettingRecordingQualityPopupCommand);
            ChangePageCommand = new Command(ExecuteChangePageCommand);
            StartRecordingCommand = new Command(ExecuteStartRecordingCommand);
            PauseRecordingCommand = new Command(ExecutePauseRecordingCommand);
            StopRecordingCommand = new Command(ExecuteStopRecordingCommand, CanStopRecording);
            CancelRecordingCommand = new Command(ExecuteCancelRecordingCommand, CanStopRecording);
            DeleteConfirmCommand = new Command(ExecuteDeleteConfirmCommand);
            DeleteItemCommand = new Command(ExecuteDeleteItemCommand);
            FastForwardVoiceStreamCommand = new Command(ExecuteFastForwardVoiceStreamCommand);
            GoToPlayerCommand = new Command(ExecuteGoToPlayerCommand);
            RewindVoiceStreamCommand = new Command(ExecuteRewindVoiceStreamCommand);
            StartPlayingCommand = new Command(ExecuteStartPlayingCommand);
            UpdateFileFormatCommand = new Command(ExecuteUpdateFileFormatCommand);
            UpdateRecordingQualityCommand = new Command(ExecuteUpdateRecordingQualityCommand);
            ReturnFromPlayerPageCommand = new Command(ExecuteReturnFromPlayerPageCommand);
            PrivilegeDeniedConfirmedCommand = new Command(ExecutePrivilegeDeniedConfirmed);
        }

        /// <summary>
        /// Handles "ErrorOccurred" event.
        /// Notifies user about error.
        /// If recording is in progress, it resets the recorder.
        /// </summary>
        /// <param name="sender">Instance of the class which invoked the event.</param>
        /// <param name="errorMessage">Error message.</param>
        public void OnErrorOccurred(object sender, string errorMessage)
        {
            ErrorMessage = errorMessage;

            Device.StartTimer(TimeSpan.Zero, () =>
            {
                ErrorInfoCommand?.Execute(null);
                // return false to run the timer callback only once
                return false;
            });

            if (_recordingLength > 0)
            {
                _voiceRecorderModel.Restart();
            }
        }

        /// <summary>
        /// Handles "ItemDeleted" event of the VoicePlayerModel class.
        /// Calls UpdateListOfRecordings method.
        /// </summary>
        /// <param name="sender">Instance of the VoicePlayerModel class.</param>
        /// <param name="e">Contains event data.</param>
        private void OnItemDeleted(object sender, EventArgs e)
        {
            UpdateListOfRecordings();
        }

        /// <summary>
        /// Handles "VoicePlayerPaused" event of the VoicePlayerModel class.
        /// Sets IsPlaying property value to false.
        /// </summary>
        /// <param name="sender">Instance of the VoicePlayerModel class.</param>
        /// <param name="e">Contains event data.</param>
        private void OnPlayerPaused(object sender, EventArgs e)
        {
            IsPlaying = false;
        }

        /// <summary>
        /// Handles "VoicePlayerStarted" event of the VoicePlayerModel class.
        /// Sets IsPlaying property value to true.
        /// </summary>
        /// <param name="sender">Instance of the VoicePlayerModel class.</param>
        /// <param name="e">Contains event data.</param>
        private void OnPlayerStarted(object sender, EventArgs e)
        {
            IsPlaying = true;
        }

        /// <summary>
        /// Handles "VoicePlayerStopped" event of the VoicePlayerModel class.
        /// Sets initial values of stream playing options.
        /// </summary>
        /// <param name="sender">Instance of the VoicePlayerModel class.</param>
        /// <param name="e">Contains event data.</param>
        private void OnPlayerStopped(object sender, EventArgs e)
        {
            IsPlaying = false;
            PlayingTime = CurrentTimerValue(SelectedRecordFile.Length);
            ProgressImage = string.Format(PROGRESS_IMAGE_TEMPLATE, "");
            _lastPlayerPosition = 0;
        }

        /// <summary>
        /// Handles "FileFormatUpdated" event of the VoiceRecorderModel class.
        /// Updates value of CurrentFileFormat property.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderModel class.</param>
        /// <param name="newValue">New value of the recorder file format setting.</param>
        private void OnRecorderFileFormatUpdated(object sender, FileFormatType newValue)
        {
            CurrentFileFormat = newValue;
        }

        /// <summary>
        /// Handles "RecordingQualityUpdated" event of the VoiceRecorderModel class.
        /// Updates value of CurrentRecordingQuality property.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderModel class.</param>
        /// <param name="newValue">New value of the recorder recording quality setting.</param>
        private void OnRecordingQualityUpdated(object sender, AudioBitRateType newValue)
        {
            CurrentRecordingQuality = newValue;
        }

        /// <summary>
        /// Handles "RecordingSaved" event of the VoiceRecorderModel class.
        /// Notifies user about saved recording.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderModel class.</param>
        /// <param name="recordingName">Name of a saved recording.</param>
        private void OnRecordingSaved(object sender, string recordingName)
        {
            LastSavedRecordingName = recordingName;
            _currentRecordingPath = null;

            Device.StartTimer(TimeSpan.Zero, () =>
            {
                SavedRecordingInfoCommand?.Execute(null);
                // return false to run the timer callback only once
                return false;
            });
        }

        /// <summary>
        /// Handles "VoiceRecorderPaused" event of the VoiceRecorderModel class.
        /// Sets IsRecording property value to false.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderModel class.</param>
        /// <param name="e">Contains event data.</param>
        private void OnRecorderPaused(object sender, EventArgs e)
        {
            IsRecording = false;
        }

        /// <summary>
        /// Handles "VoiceRecorderResumed" event of the VoiceRecorderModel class.
        /// Sets IsRecording property value to true.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderModel class.</param>
        /// <param name="e">Contains event data.</param>
        private void OnRecorderResumed(object sender, EventArgs e)
        {
            IsRecording = true;
        }

        /// <summary>
        /// Handles "VoiceRecorderStarted" event of the VoiceRecorderModel class.
        /// Sets IsRecording property value to true.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderModel class.</param>
        /// <param name="currentRecordingPath">Path to the sample which is being recorded at the moment.</param>
        private void OnRecorderStarted(object sender, string currentRecordingPath)
        {
            _currentRecordingPath = currentRecordingPath;
            IsRecording = true;
        }

        /// <summary>
        /// Handles "VoiceRecorderStopped" event of the VoiceRecorderModel class.
        /// Sets initial values of recording options.
        /// Calls UpdateListOfRecordings method.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderModel class.</param>
        /// <param name="e">Contains event data.</param>
        private void OnRecorderStopped(object sender, EventArgs e)
        {
            IsRecording = false;
            RecordingLength = 0;
            RecordingTime = CurrentTimerValue(RecordingLength);
            ProgressImage = string.Format(PROGRESS_IMAGE_TEMPLATE, "");
            UpdateListOfRecordings();
        }

        /// <summary>
        /// Handles "VoiceStreamPositionSought" event of the VoicePlayerModel class.
        /// Updates progress image and information about playing stream new position.
        /// </summary>
        /// <param name="sender">Instance of the VoiceRecorderModel class.</param>
        /// <param name="e">Contains event data.</param>
        private void OnVoiceStreamPositionSought(object sender, EventArgs e)
        {
            var currentPlayerPosition = _voicePlayerModel.GetCurrentPlayerPosition();
            PlayingTime = CurrentTimerValue(SelectedRecordFile.Length - currentPlayerPosition);
            _lastPlayerPosition = currentPlayerPosition / TO_SECONDS_DIVIDER;

            if (currentPlayerPosition == 0)
            {
                ProgressImage = string.Format(PROGRESS_IMAGE_TEMPLATE, "");
            }
            else
            {
                ProgressImage = string.Format(PROGRESS_IMAGE_TEMPLATE,
                    currentPlayerPosition / TO_SECONDS_DIVIDER % NUMBER_OF_PROGRESS_IMAGES);
            }
        }

        /// <summary>
        /// Checks whether "CancelRecordingCommand" and "StopRecordingCommand"
        /// can be executed or not.
        /// </summary>
        /// <returns>True if length of the recording is greater than 0, false otherwise.</returns>
        private bool CanStopRecording()
        {
            return RecordingLength != 0;
        }

        /// <summary>
        /// Transforms tenths of a second into minutes:seconds form.
        /// </summary>
        /// <param name="currentLength">Value in tenths of a second which is transformed.</param>
        /// <returns>Transformed value in form minutes:seconds</returns>
        private string CurrentTimerValue(int currentLength)
        {
            return (currentLength / TO_SECONDS_DIVIDER / TO_MINUTES_DIVIDER).ToString("00") + ":" +
                (currentLength / TO_SECONDS_DIVIDER % TO_MINUTES_DIVIDER).ToString("00");
        }

        /// <summary>
        /// Handles execution of "ReturnFromPlayerPageCommand".
        /// Sets IsPlaying property value to false.
        /// Calls model to unprepare the player.
        /// </summary>
        private void ExecuteReturnFromPlayerPageCommand()
        {
            IsPlaying = false;
            _voicePlayerModel.UnprepareVoicePlaying();
        }

        /// <summary>
        /// Handles execution of "CancelRecordingCommand".
        /// Calls model to cancel recording.
        /// </summary>
        private void ExecuteCancelRecordingCommand()
        {
            _voiceRecorderModel.CancelVoiceRecording();
        }

        /// <summary>
        /// Handles execution of "ChangePageCommand".
        /// Changes current page to that specified in command parameter.
        /// </summary>
        /// <param name="pageType">Target page class.</param>
        private void ExecuteChangePageCommand(object pageType)
        {
            if (RecordingLength != 0)
            {
                ExecuteCancelRecordingCommand();
            }

            if (IsPlaying)
            {
                ExecuteStartPlayingCommand();
            }

            Page nextPage = (Page)Activator.CreateInstance((Type)pageType);
            nextPage.BindingContext = this;
            Application.Current.MainPage.Navigation.PushAsync(nextPage);
        }

        /// <summary>
        /// Handles execution of "DeleteConfirmCommand".
        /// Deletes recording.
        /// </summary>
        private void ExecuteDeleteConfirmCommand()
        {
            _recordingFilesReader.DeleteItem(_fileToDelete);
        }

        /// <summary>
        /// Handles execution of "DeleteItemCommand".
        /// Sets file to delete.
        /// </summary>
        /// <param name="selectedItem">Recording selected from the list.</param>
        private void ExecuteDeleteItemCommand(object selectedItem)
        {
            if (selectedItem is RecordedFileListItem recordFile)
            {
                _fileToDelete = recordFile.Path;
                DeleteShowAlertCommand?.Execute(null);
            }
        }

        /// <summary>
        /// Handles execution of "FastForwardVoiceStreamCommand".
        /// Calls model to fast-forward the playing stream.
        /// </summary>
        private void ExecuteFastForwardVoiceStreamCommand()
        {
            _voicePlayerModel.SeekVoiceStreamPosition((int)SeekPlayerPosition.FastForward);
        }

        /// <summary>
        /// Handles execution of "GoToPlayerCommand".
        /// Changes page to the player page of a selected recording (specified in command parameter).
        /// Sets initial values of the player.
        /// Calls UpdateListOfRecordings method.
        /// </summary>
        /// <param name="selectedItem">Recording selected from the list.</param>
        private void ExecuteGoToPlayerCommand(object selectedItem)
        {
            if (selectedItem is RecordedFileListItem recordFile)
            {
                SelectedRecordFile = recordFile;
                PlayingTime = CurrentTimerValue(SelectedRecordFile.Length);
                _voicePlayerModel.PrepareVoicePlayer(SelectedRecordFile.Path);
                ExecuteChangePageCommand(DependencyService.Get<IPageResolver>().VoicePlayerPage.GetType());
                UpdateListOfRecordings();
            }
        }

        /// <summary>
        /// Handles execution of "PauseRecordingCommand".
        /// Calls model to pause recording.
        /// </summary>
        private void ExecutePauseRecordingCommand()
        {
            _voiceRecorderModel.PauseVoiceRecording();
        }

        /// <summary>
        /// Handles execution of command which occurs when user confirms privilege denied dialog.
        /// Closes the application.
        /// </summary>
        private void ExecutePrivilegeDeniedConfirmed()
        {
            DependencyService.Get<IApplicationService>()?.Close();
        }

        /// <summary>
        /// Handles execution of "RewindVoiceStreamCommand".
        /// Calls model to rewind the playing stream.
        /// </summary>
        private void ExecuteRewindVoiceStreamCommand()
        {
            _voicePlayerModel.SeekVoiceStreamPosition((int)SeekPlayerPosition.Rewind);
        }

        /// <summary>
        /// Handles execution of "StartPlayingCommand".
        /// Calls model to start or pause voice playing.
        /// </summary>
        private void ExecuteStartPlayingCommand()
        {
            if (!IsPlaying)
            {
                _voicePlayerModel.StartVoicePlaying();
                int decimalPartOfPlayerPosition;

                Device.StartTimer(TimeSpan.FromMilliseconds(TIMER_PERIOD), () =>
                {
                    if (IsPlaying)
                    {
                        decimalPartOfPlayerPosition =
                            _voicePlayerModel.GetCurrentPlayerPosition() / TO_SECONDS_DIVIDER;

                        if (_lastPlayerPosition != decimalPartOfPlayerPosition)
                        {
                            _lastPlayerPosition = decimalPartOfPlayerPosition;
                            ProgressImage = string.Format(PROGRESS_IMAGE_TEMPLATE,
                                decimalPartOfPlayerPosition % NUMBER_OF_PROGRESS_IMAGES);
                            PlayingTime = CurrentTimerValue(
                                SelectedRecordFile.Length - _voicePlayerModel.GetCurrentPlayerPosition());
                        }
                    }

                    return IsPlaying;
                });
            }
            else
            {
                _voicePlayerModel.PauseVoicePlaying();
            }
        }

        /// <summary>
        /// Handles execution of "StartRecordingCommand".
        /// Calls model to start, stop, pause or resume voice recording.
        /// Starts recording if voice stream is not being recorded.
        /// Pauses recording if voice stream is being recorded.
        /// Resumes recording if voice stream recording is paused.
        /// Stops recording if the maximum length of the recording is reached.
        /// </summary>
        private void ExecuteStartRecordingCommand()
        {
            if (!IsRecording)
            {
                _voiceRecorderModel.StartVoiceRecording();

                Device.StartTimer(TimeSpan.FromMilliseconds(TIMER_PERIOD), () =>
                {
                    if (RecordingLength == MAX_RECORDING_LENGTH)
                    {
                        ExecuteStopRecordingCommand();
                    }

                    if (IsRecording)
                    {
                        RecordingLength++;

                        if (RecordingLength % TO_SECONDS_DIVIDER == 0)
                        {
                            ProgressImage = string.Format(PROGRESS_IMAGE_TEMPLATE,
                                _recordingLength / TO_SECONDS_DIVIDER % NUMBER_OF_PROGRESS_IMAGES);
                            RecordingTime = CurrentTimerValue(RecordingLength);
                        }
                    }

                    return IsRecording;
                });
            }
            else
            {
                _voiceRecorderModel.PauseVoiceRecording();
            }
        }

        /// <summary>
        /// Handles execution of "StopRecordingCommand".
        /// Calls model to stop recording.
        /// </summary>
        private void ExecuteStopRecordingCommand()
        {
            _voiceRecorderModel.StopVoiceRecording();
        }

        /// <summary>
        /// Handles execution of "UpdateFileFormatCommand".
        /// Calls model to update recorder file format setting.
        /// </summary>
        /// <param name="param">New file format value to set.</param>
        private void ExecuteUpdateFileFormatCommand(object param)
        {
            if (param is FileFormatType item)
            {
                _voiceRecorderModel.UpdateRecorderFileFormat(item);
            }
        }

        /// <summary>
        /// Handles execution of "UpdateRecordingQualityCommand".
        /// Calls model to update recorder recording quality setting.
        /// </summary>
        /// <param name="param">New recording quality value to set.</param>
        private void ExecuteUpdateRecordingQualityCommand(object param)
        {
            if (param is AudioBitRateType item)
            {
                _voiceRecorderModel.UpdateRecordingQuality(item);
            }
        }

        /// <summary>
        /// Gets list of recordings.
        /// Omits a sample which is being recorded at the moment.
        /// </summary>
        private void UpdateListOfRecordings()
        {
            ObservableCollection<RecordedFileListItem> recordingFilesList = _recordingFilesReader.GetListOfFiles(
                GoToPlayerCommand, DeleteItemCommand);
            recordingFilesList.Remove(recordingFilesList.FirstOrDefault(p => p.Path == _currentRecordingPath));
            RecordingsCollection = recordingFilesList;
        }

        /// <summary>
        /// Handles execution of "SettingFileFormatPopupCommand".
        /// Shows pop-up with file format setting options.
        /// </summary>
        /// <param name="sender">Instance of an object which calls the method.</param>
        private void ExecuteSettingFileFormatPopupCommand(object sender)
        {
            ShowSettingFileFormatPopupCommand?.Execute(sender);
        }

        /// <summary>
        /// Handles execution of "SettingRecordingQualityPopupCommand".
        /// Shows pop-up with recorder quality setting options.
        /// </summary>
        /// <param name="sender">Instance of an object which calls the method.</param>
        private void ExecuteSettingRecordingQualityPopupCommand(object sender)
        {
            ShowSettingRecordingQualityPopupCommand?.Execute(sender);
        }

        #endregion
    }
}
