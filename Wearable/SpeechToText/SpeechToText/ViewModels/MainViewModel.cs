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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TextReader.ViewModels;
using Tizen;
using Xamarin.Forms;

namespace SpeechToText.ViewModels
{
    /// <summary>
    /// The application's main view model class.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Private backing field for SoundOn property.
        /// </summary>
        private bool _wizardSoundOn;

        /// <summary>
        /// Private backing field for WizardStartSound property.
        /// Contains temporary value for start sound used in the sounds wizard.
        /// </summary>
        private string _wizardStartSound;

        /// <summary>
        /// Private backing field for WizardEndSound property.
        /// Contains temporary value for end sound used in the sounds wizard.
        /// </summary>
        private string _wizardEndSound;

        /// <summary>
        /// An instance of the STT model.
        /// </summary>
        private SpeechToTextModel _sttModel;

        /// <summary>
        /// Private backing field for AvailableStartEndSounds property.
        /// </summary>
        private IEnumerable<string> _availableStartEndSounds;

        /// <summary>
        /// Private backing field for ResultText property.
        /// </summary>
        private string _resultText;

        /// <summary>
        /// Private backing field for ServiceError property.
        /// </summary>
        private SttError _serviceError;

        #endregion

        #region properties

        /// <summary>
        /// The application's navigation class instance.
        /// </summary>
        public INavigation Navigation { get; set; }

        /// <summary>
        /// Command which allows to navigate to another page (specified in command parameter).
        /// </summary>
        public ICommand NavigateCommand { get; private set; }

        /// <summary>
        /// Command which allows to navigate to previous page.
        /// </summary>
        public ICommand NavigateBackCommand { get; private set; }

        /// <summary>
        /// Command which allows to navigate to settings page.
        /// </summary>
        public ICommand NavigateToSettingsCommand { get; private set; }

        /// <summary>
        /// Command which allows to change the language of the STT client.
        /// </summary>
        public ICommand ChangeLanguageCommand { get; private set; }

        /// <summary>
        /// Command which allows to change recognition type of the STT client.
        /// </summary>
        public ICommand ChangeRecognitionTypeCommand { get; private set; }

        /// <summary>
        /// Command which allows to change recognition type of the STT client.
        /// </summary>
        public ICommand ChangeSilenceDetectionCommand { get; private set; }

        /// <summary>
        /// Command which initializes sounds settings wizard (initial value, sounds list data).
        /// </summary>
        public ICommand InitSoundsWizardCommand { get; private set; }

        /// <summary>
        /// Command which updates available start/end sounds list.
        /// </summary>
        public ICommand UpdateAvailableStartEndSoundsCommand { get; private set; }

        /// <summary>
        /// Command which updates start sound value (sounds wizard).
        /// </summary>
        public ICommand WizardUpdateStartSoundCommand { get; private set; }

        /// <summary>
        /// Command which updates end sound value (sounds wizard).
        /// </summary>
        public ICommand WizardUpdateEndSoundCommand { get; private set; }

        /// <summary>
        /// Command which saves sound settings.
        /// </summary>
        public ICommand WizardSaveSoundSettingsCommand { get; private set; }

        /// <summary>
        /// Command which starts the recognition.
        /// </summary>
        public ICommand RecognitionStartCommand { get; private set; }

        /// <summary>
        /// Command which pauses the recognition.
        /// </summary>
        public ICommand RecognitionPauseCommand { get; private set; }

        /// <summary>
        /// Command which stops the recognition.
        /// </summary>
        public ICommand RecognitionStopCommand { get; private set; }

        /// <summary>
        /// Command which clears the recognition results.
        /// </summary>
        public ICommand ClearResultCommand { get; private set; }

        /// <summary>
        /// Command which shows message about recognition fail.
        /// The command is injected into view model.
        /// </summary>
        public ICommand RecognitionFailedInfoCommand { get; set; }

        /// <summary>
        /// Command which shows message about service error.
        /// The command is injected into view model.
        /// </summary>
        public ICommand ServiceErrorInfoCommand { get; set; }

        /// <summary>
        /// Command which shows message about unavailability of settings.
        /// The command is injected into view model.
        /// </summary>
        public ICommand SettingsUnavailableInfoCommand { get; set; }

        /// <summary>
        /// Command which shows message about denied privilege (application close).
        /// The command is injected into view model.
        /// </summary>
        public ICommand PrivilegeDeniedInfoCommand { get; set; }

        /// <summary>
        /// Command which handles confirmation of privilege denied dialog.
        /// </summary>
        public ICommand PrivilegeDeniedConfirmedCommand { get; set; }

        /// <summary>
        /// Recognition result text.
        /// </summary>
        public string ResultText
        {
            get => _resultText;
            private set => SetProperty(ref _resultText, value);
        }

        /// <summary>
        /// Flag indicating if recognition is active.
        /// </summary>
        public bool RecognitionActive => _sttModel.RecognitionActive;

        /// <summary>
        /// A collection of languages supported by the service.
        ///
        /// The language is specified as an ISO 3166 alpha-2 two letter country-code
        /// followed by ISO 639-1 for the two-letter language code.
        /// </summary>
        public IEnumerable<string> SupportedLanguages => _sttModel.SupportedLanguages;

        /// <summary>
        /// Current STT client language (code).
        /// </summary>
        public string Language
        {
            get => _sttModel.Language;
            private set
            {
                _sttModel.Language = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A collection of available recognition types.
        /// </summary>
        public IEnumerable<RecognitionType> SupportedRecognitionTypes =>
            _sttModel.SupportedRecognitionTypes;

        /// <summary>
        /// Current STT client recognition type.
        /// </summary>
        public RecognitionType RecognitionType
        {
            get => _sttModel.RecognitionType;
            private set
            {
                _sttModel.RecognitionType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Current STT client silence detection mode.
        /// </summary>
        public SilenceDetection SilenceDetection
        {
            get => _sttModel.SilenceDetection;
            private set
            {
                _sttModel.SilenceDetection = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A collection of sound files (paths) which can be used
        /// as start and end sounds for STT client.
        /// </summary>
        public IEnumerable<string> AvailableStartEndSounds
        {
            get { return _availableStartEndSounds; }
            private set { SetProperty(ref _availableStartEndSounds, value); }
        }

        /// <summary>
        /// Flag indicating if STT client sounds (start, end) are turn on (wizard value).
        /// </summary>
        public bool SoundOn => _sttModel.SoundOn;

        /// <summary>
        /// Wizard value for sound on option.
        /// </summary>
        public bool WizardSoundOn
        {
            get => _wizardSoundOn;
            set => SetProperty(ref _wizardSoundOn, value);
        }

        /// <summary>
        /// Wizard value for the start sound.
        /// </summary>
        public string WizardStartSound
        {
            get => _wizardStartSound;
            set => SetProperty(ref _wizardStartSound, value);
        }

        /// <summary>
        /// Wizard value for the end sound.
        /// </summary>
        public string WizardEndSound
        {
            get => _wizardEndSound;
            set => SetProperty(ref _wizardEndSound, value);
        }

        /// <summary>
        /// Service error value.
        /// Property updated when service error occurs.
        /// </summary>
        public SttError ServiceError
        {
            get => _serviceError;
            set => SetProperty(ref _serviceError, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// The view model constructor.
        /// </summary>
        public MainViewModel()
        {
            System.Console.WriteLine($"MainViewModel ctor");
            try
            {
                _sttModel = new SpeechToTextModel(Application.Current.Properties);
                _sttModel.ResultChanged += SttModelOnResultChanged;
                _sttModel.RecognitionActiveStateChanged += SttModelOnRecognitionActiveStateChanged;
                _sttModel.RecognitionError += SttModelOnRecognitionError;
                _sttModel.ServiceError += SttModelOnServiceError;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ex.Message: {ex.Message} \n {ex.StackTrace}");
            }

            InitCommands();
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <returns>The initialization task.</returns>
        public async Task Init()
        {
            var priviligesGranted = await _sttModel.CheckPrivileges();

            if (!priviligesGranted)
            {
                Device.StartTimer(TimeSpan.Zero, () =>
                {
                    PrivilegeDeniedInfoCommand?.Execute(null);
                    // return false to run the timer callback only once
                    return false;
                });

                return;
            }

            await _sttModel.Init();
        }

        /// <summary>
        /// Initializes view model's commands.
        /// </summary>
        private void InitCommands()
        {
            NavigateCommand = new Command<Type>(ExecuteNavigate);
            NavigateBackCommand = new Command(ExecuteNavigateBack);
            NavigateToSettingsCommand = new Command<Type>(ExecuteNavigateToSettings);
            ChangeLanguageCommand = new Command<string>(ExecuteChangeLanguage);
            ChangeRecognitionTypeCommand = new Command<RecognitionType>(ExecuteChangeRecognitionType);
            ChangeSilenceDetectionCommand = new Command<SilenceDetection>(ExecuteChangeSilenceDetection);
            InitSoundsWizardCommand = new Command<Type>(ExecuteInitSoundsWizard);
            UpdateAvailableStartEndSoundsCommand = new Command(ExecuteUpdateAvailableStartEndSounds);
            WizardUpdateStartSoundCommand = new Command<string>(ExecuteWizardUpdateStartSound);
            WizardUpdateEndSoundCommand = new Command<string>(ExecuteWizardUpdateEndSound);
            WizardSaveSoundSettingsCommand = new Command(ExecuteWizardSaveSoundSettings);
            RecognitionStartCommand = new Command(ExecuteRecognitionStart);
            RecognitionPauseCommand = new Command(ExecuteRecognitionPause);
            RecognitionStopCommand = new Command(ExecuteRecognitionStop);
            ClearResultCommand = new Command(ExecuteClearResult);
            PrivilegeDeniedConfirmedCommand = new Command(ExecutePrivilegeDeniedConfirmed);
        }

        /// <summary>
        /// Handles execution of navigate command.
        ///
        /// Changes current page to that specified in command parameter.
        /// </summary>
        /// <param name="pageType">Target page class.</param>
        private void ExecuteNavigate(Type pageType)
        {
            Page page = (Page)Activator.CreateInstance(pageType);
            Navigation?.PushModalAsync(page);
        }

        /// <summary>
        /// Handles execution of navigate back command.
        ///
        /// Changes current page to previous one.
        /// </summary>
        private void ExecuteNavigateBack()
        {
            Navigation?.PopModalAsync();
        }

        /// <summary>
        /// Handles execution of settings navigation command.
        ///
        /// Navigates to settings page if there is no ongoing recognition process,
        /// shows proper message (dialog) otherwise.
        /// </summary>
        /// <param name="pageType">Page class to navigate to.</param>
        private void ExecuteNavigateToSettings(Type pageType)
        {
            try
            {
                if (!_sttModel.Ready)
                {
                    return;
                }

                if (RecognitionActive)
                {
                    SettingsUnavailableInfoCommand?.Execute(null);
                }
                else
                {
                    ExecuteNavigate(pageType);
                }
            }
            catch (Exception e)
            {
                Log.Debug("STT", e.Message + " " + e.GetType());
            }
        }

        /// <summary>
        /// Handles execution of change language command.
        ///
        /// Updates current STT client language.
        /// </summary>
        /// <param name="language">Language to set.</param>
        private void ExecuteChangeLanguage(string language)
        {
            Language = language;

            ExecuteNavigateBack();
        }

        /// <summary>
        /// Handles execution of change recognition type command.
        ///
        /// Updates current STT client recognition type.
        /// </summary>
        /// <param name="type">Recognition type to set.</param>
        private void ExecuteChangeRecognitionType(RecognitionType type)
        {
            RecognitionType = type;

            ExecuteNavigateBack();
        }

        /// <summary>
        /// Handles execution of change silence detection command.
        ///
        /// Updates STT client silence detection value.
        /// </summary>
        /// <param name="value">Silence detection value to set.</param>
        private void ExecuteChangeSilenceDetection(SilenceDetection value)
        {
            SilenceDetection = value;

            ExecuteNavigateBack();
        }

        /// <summary>
        /// Handles execution of init sounds wizard command.
        ///
        /// Sets initial values for wizard options and navigates
        /// to a page specified in the command parameter.
        /// </summary>
        /// <param name="pageType">Target page class.</param>
        private void ExecuteInitSoundsWizard(Type pageType)
        {
            WizardSoundOn = SoundOn;
            WizardStartSound = _sttModel.StartSound;
            WizardEndSound = _sttModel.EndSound;

            ExecuteNavigate(pageType);
        }

        /// <summary>
        /// Handles execution of update available start/end sounds command.
        ///
        /// Triggers updating of available sounds collection.
        /// </summary>
        private void ExecuteUpdateAvailableStartEndSounds()
        {
            // initial list contains empty element
            var initialList = new string[] { null };

            AvailableStartEndSounds = initialList.Concat(
                _sttModel.GetAvailableStartEndSounds());
        }

        /// <summary>
        /// Handles execution of wizard update start sound command.
        ///
        /// Updates wizard value for start sound.
        /// </summary>
        /// <param name="value">Value to set.</param>
        private void ExecuteWizardUpdateStartSound(string value)
        {
            WizardStartSound = value;

            ExecuteNavigateBack();
        }

        /// <summary>
        /// Handles execution of wizard update end sound command.
        ///
        /// Updates wizard value for end sound.
        /// </summary>
        /// <param name="value">Value to set.</param>
        private void ExecuteWizardUpdateEndSound(string value)
        {
            WizardEndSound = value;

            ExecuteNavigateBack();
        }

        /// <summary>
        /// Handles execution of save sound settings command.
        ///
        /// Updates STT model sound settings.
        /// </summary>
        private void ExecuteWizardSaveSoundSettings()
        {
            _sttModel.StartSound = WizardStartSound;
            _sttModel.EndSound = WizardEndSound;
            _sttModel.SoundOn = WizardSoundOn;
            OnPropertyChanged(nameof(SoundOn));

            //ExecuteNavigateBack();
        }

        /// <summary>
        /// Handles execution of recognition start command.
        ///
        /// Calls model to start the recognition.
        /// </summary>
        private void ExecuteRecognitionStart()
        {
            if (!_sttModel.Ready)
            {
                return;
            }

            _sttModel.Start();
        }

        /// <summary>
        /// Handles execution of recognition pause command.
        ///
        /// Calls model to pause the recognition.
        /// </summary>
        private void ExecuteRecognitionPause()
        {
            if (!_sttModel.Ready)
            {
                return;
            }

            _sttModel.Pause();
        }

        /// <summary>
        /// Handles execution of recognition stop command.
        ///
        /// Calls model to stop the recognition.
        /// </summary>
        private void ExecuteRecognitionStop()
        {
            if (!_sttModel.Ready)
            {
                return;
            }

            _sttModel.Stop();
        }

        /// <summary>
        /// Handles execution of recognition result clear command.
        ///
        /// Calls model to clear the result.
        /// </summary>
        private void ExecuteClearResult()
        {
            if (!_sttModel.Ready)
            {
                return;
            }

            _sttModel.Clear();
        }

        /// <summary>
        /// Handles execution of command which occurs when user confirms privilege denied dialog.
        /// Closes the application.
        /// </summary>
        private void ExecutePrivilegeDeniedConfirmed()
        {
            try
            {
                global::Tizen.Applications.Application.Current.Exit();
            }
            catch (Exception)
            {
                global::Tizen.Log.Error("STT", "Unable to close the application");
            }
        }

        /// <summary>
        /// Handles recognition result change event from the model.
        /// Updates "ResultText" property.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void SttModelOnResultChanged(object sender, EventArgs eventArgs)
        {
            ResultText = _sttModel.Result;
        }

        /// <summary>
        /// Handles recognition active state change event from the model.
        /// Triggers "RecognitionActive" property change callback.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void SttModelOnRecognitionActiveStateChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged(nameof(RecognitionActive));
        }

        /// <summary>
        /// Handles recognition error.
        /// Executes command which shows proper message.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="eventArgs">Event arguments.</param>
        private void SttModelOnRecognitionError(object sender, EventArgs eventArgs)
        {
            RecognitionFailedInfoCommand?.Execute(null);
        }

        /// <summary>
        /// Handles STT service error.
        /// Executes command which shows proper message.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="serviceErrorEventArgs">Event arguments.</param>
        private void SttModelOnServiceError(object sender, IServiceErrorEventArgs serviceErrorEventArgs)
        {
            ServiceError = serviceErrorEventArgs.Error;

            Device.StartTimer(TimeSpan.Zero, () =>
            {
                ServiceErrorInfoCommand?.Execute(null);
                // return false to run the timer callback only once
                return false;
            });
        }

        #endregion
    }
}
