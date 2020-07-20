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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using VoiceMemo.Models;
using VoiceMemo.Resx;
using VoiceMemo.Services;
using Xamarin.Forms;

namespace VoiceMemo.ViewModels
{
    // The model class for MainPage
    public class MainPageModel : BasePageModel
    {
        // Collection of Records
        ObservableCollection<Record> _Records;
        // Language for STT recognition
        string _CurrentLanguage;
        // Speech-To-Text Service
        ISpeechToTextService _SttService;
        // Media Content Service to get the path of audio record file
        IMediaContentService _ContentService;
        // App data service to store / restore app data
        IAppDataService _AppDataService;
        // indicate whether it is possible to record voice
        public bool availableToRecord;

        public MainPageModel()
        {
            if (Records == null)
            {
                Records = new ObservableCollection<Record>();
            }

            Init();

            RegisterForImportantEvents();
        }

        // Initialize
        void Init()
        {
            IsCheckable = false;
            SttEnabled = true;
            MainLabelText = AppResources.StandByTitleA;
            availableToRecord = true;
        }

        // Subscribe to get notified when some events occur.
        // 1. When a new voice memo is saved in an internal storage
        // 2. When a voice memo is deleted from an internal storage
        // 3. When it's checked whether user consent is obtained
        void RegisterForImportantEvents()
        {
            // You can get notified whenever a new voice memo record is created.
            // At this point of time, information(record) about that voice memo is needed to store
            MessagingCenter.Subscribe<RecordingPageModel, Record>(this, MessageKeys.SaveVoiceMemo, (obj, item) =>
            {
                // Add it to the collection of records
                var record = ((LatestRecord)item).Record;
                Records.Add(record);
            });

            // You can get notified whenever translated text via speech-to-text service is ready
            // Now, we can update Record to database
            MessagingCenter.Subscribe<RecordingPageModel, Record>(this, MessageKeys.SaveVoiceMemoInDB, async (obj, record) =>
            {
                // add it to database
                await App.Database.SaveItemAsync(record);
            });

            // You can get notified whenever a voice memo record is deleted from a storage
            MessagingCenter.Subscribe<Page, Record>(this, MessageKeys.DeleteVoiceMemo, async (obj, item) =>
            {
                var record = item as Record;
                for (int i = Records.Count - 1; i >= 0; i--)
                {
                    if (Records[i]._id == record._id)
                    {
                        // Delete record from database
                        await App.Database.DeleteItemAsync(Records[i]);
                        // Delete audio file from internal storage
                        _ContentService.RemoveMediaFile(Records[i].Path);
                        // Delete it from collection of records
                        Records.RemoveAt(i);
                    }
                }
            });

            // You can get notified when an app user allows this app to use recorder and internal storage.
            MessagingCenter.Subscribe<App, bool>(this, MessageKeys.UserPermission, async (obj, item) =>
            {
                // Restore recorded voice memos
                List<Record> tmp = await App.Database.GetItemsAsync();
                for (int i = 0; i < tmp.Count; i++)
                {
                    Records.Add(tmp[i]);
                }
                // Speech-To-Text Service
                GetSttService();

                // Media Content Service
                if (_ContentService == null)
                {
                    _ContentService = DependencyService.Get<IMediaContentService>(DependencyFetchTarget.GlobalInstance);
                }
            });
        }

        /// <summary>
        /// Unsubscribe from notifications
        /// </summary>
        void UnregisterForImportantEvents()
        {
            MessagingCenter.Unsubscribe<RecordingPageModel, Record>(this, MessageKeys.SaveVoiceMemo);
            MessagingCenter.Unsubscribe<Page, Record>(this, MessageKeys.DeleteVoiceMemo);
            MessagingCenter.Unsubscribe<App, bool>(this, MessageKeys.UserPermission);
        }

        public void Dispose()
        {
            Console.WriteLine("### MainPageModel.Dispose()  -----_SttService?.Dispose()----");
            UnregisterForImportantEvents();
            if (SelectedItemIndex != null)
            {
                Console.WriteLine("### MainPageModel.Dispose() SAVE Language : " + SelectedItemIndex.Lang);
                _AppDataService.SetValue(LANGUAGE_FOR_STT, SelectedItemIndex.Lang);
            }
            
            _SttService?.Dispose();
            _ContentService?.Destroy();
        }

        /// <summary>
        /// Collection of Records
        /// </summary>
        public ObservableCollection<Record> Records
        {
            get
            {
                return _Records;
            }

            set
            {
                bool changed = SetProperty(ref _Records, value, "Records");

                Console.WriteLine(" ##########  Records  changed? " + changed);
                if (changed && Records.Count == 0)
                {
                    Console.WriteLine(" ##########  Records.Count is 0!!");
                }
            }
        }

        // Language for STT recognition
        public string CurrentLanguage
        {
            get
            {
                return _CurrentLanguage;
            }

            set
            {
                if (SetProperty(ref _CurrentLanguage, value, "CurrentLanguage"))
                {
                    Console.WriteLine("Language for STT service has been changed. So update _SttService's CurrentSttLanguage. : " + CurrentLanguage);
                    if (_SttService != null)
                    {
                        _SttService.CurrentSttLanguage = CurrentLanguage;
                    }
                }
            }
        }

        SttLanguage _SelectedItemIndex;
        public SttLanguage SelectedItemIndex
        {
            get
            {
                return _SelectedItemIndex;
            }

            set
            {
                SetProperty(ref _SelectedItemIndex, value, "SelectedItemIndex");
            }
        }

        public ObservableCollection<SttLanguage> Languages { get; set; }

        public static readonly BindableProperty GetSttServiceCommandProperty =
        BindableProperty.Create("GetSttServiceCommand", typeof(Command), typeof(MainPageModel), default(Command));
        public ICommand GetSttServiceCommand => new Command(GetSttService);

        public static readonly BindableProperty SttOnOffCommandProperty =
            BindableProperty.Create("SttOnOffCommand", typeof(Command), typeof(MainPageModel), default(Command));

        public ICommand SttOnOffCommand => new Command(SttOnOff);

        void GetSttService()
        {
            if (_SttService == null)
            {
                Console.WriteLine(" GetSttService()  ------------1-  CurrentLanguage : " + CurrentLanguage);
                _SttService = DependencyService.Get<ISpeechToTextService>(DependencyFetchTarget.GlobalInstance);
                Languages = new ObservableCollection<SttLanguage>();

                // TODO: how to check if the current language is supported by STT engine.
                //CultureInfo _cultureInfo = new CultureInfo(CurrentLanguage);
                //RegionInfo _regionInfo = new RegionInfo(CurrentLanguage.Replace("_", "-"));
                //Languages.Add(new SttLanguage(CurrentLanguage, "Automatic", Regex.Replace(_cultureInfo.DisplayName, @"\t|\n|\r", "")));

                _AppDataService = DependencyService.Get<IAppDataService>();
                var dafaultLang = "en_US";
                // Restore the selected language for STT or use the STT service's current language
                if (_AppDataService.Contain(LANGUAGE_FOR_STT))
                {
                    dafaultLang = _AppDataService.GetValue(LANGUAGE_FOR_STT);
                    Console.WriteLine(" IAppDataService.GetValue =>  language for stt  : " + dafaultLang);
                }
                else
                {
                    dafaultLang = _SttService.CurrentSttLanguage;
                    Console.WriteLine(" IAppDataService no value =>  CurrentSttLanguage  : " + dafaultLang);
                }
                // For updating STT service's CurrentSttLanguage
                CurrentLanguage = dafaultLang;

                foreach (var lang in _SttService.GetInstalledLanguages())
                {
                    CultureInfo cultureInfo = new CultureInfo(lang);
                    RegionInfo regionInfo = new RegionInfo(lang.Replace("_", "-"));
                    var stt = new SttLanguage(lang, cultureInfo.DisplayName, regionInfo.EnglishName);
                    Languages.Add(stt);
                    if (lang == dafaultLang)
                    {
                        stt.IsOn = true;
                        SelectedItemIndex = stt;
                    }
                }

                Console.WriteLine(" GetSttService()  ------------2-   " + _SttService.GetHashCode());
            }
        }

        // STT feature usability
        // in case of true : while recording voice, you can convert your voice to text (Speech to text)
        // in case of false : just record voice.
        bool _sttEnabled;
        public bool SttEnabled
        {
            get
            {
                return _sttEnabled;
            }

            set
            {
                SetProperty(ref _sttEnabled, value, "SttEnabled");
            }
        }

        void SttOnOff(object sender)
        {
            Console.WriteLine(" StandByPageModel.SttOnOff() : " + SttEnabled);
            if (SttEnabled)
            {
                SttEnabled = false;
                MainLabelText = AppResources.StandByTitleB;
            }
            else
            {
                SttEnabled = true;
                MainLabelText = AppResources.StandByTitleA;
            }

            MessagingCenter.Send<MainPageModel, bool>(this, MessageKeys.SttSupportedChanged, SttEnabled);
        }

        // main label text : voice memo or voice recorder
        string _mainlabeltext;
        public string MainLabelText
        {
            get
            {
                return _mainlabeltext;
            }

            set
            {
                SetProperty(ref _mainlabeltext, value, "MainLabelText");
            }
        }

        // Update the text of main label, depending on enabling or disabling stt service
        public override void UpdateText()
        {
            if (SttEnabled)
            {
                // voice memo
                MainLabelText = AppResources.StandByTitleA;
            }
            else
            {
                // voice recorder
                MainLabelText = AppResources.StandByTitleB;
            }
        }

        ///////////////////////
        const string SelectAll = "Select all";
        const string DeselectAll = "Deselect all";

        bool _isCheckable;
        public bool IsCheckable
        {
            get
            {
                return _isCheckable;
            }

            set
            {
                bool result = SetProperty(ref _isCheckable, value, "IsCheckable");
                if (result)
                {
                    Console.WriteLine("-----IsCheckable  : it's changed  ---> " + IsCheckable);
                    if (!IsCheckable)
                    {
                        foreach (var record in Records)
                        {
                            record.Checked = false;
                        }
                    }
                }
            }
        }

        bool _popupVisibility;
        public bool PopupVisibility
        {
            get
            {
                return _popupVisibility;
            }

            set
            {
                SetProperty(ref _popupVisibility, value, "PopupVisibility");
            }
        }

        int _checkedNamesCount;
        public int CheckedNamesCount
        {
            get
            {
                return _checkedNamesCount;
            }

            set
            {
                bool changed = SetProperty(ref _checkedNamesCount, value, "CheckedNamesCount");
                if (changed)
                {
                    UpdateSelectOptionMessage();
                }
            }
        }

        string _selectOptionMessage1;
        public string SelectOptionMessage1
        {
            get
            {
                return _selectOptionMessage1;
            }

            set
            {
                SetProperty(ref _selectOptionMessage1, value, "SelectOptionMessage1");
            }
        }

        string _selectOptionMessage2;
        public string SelectOptionMessage2
        {
            get
            {
                return _selectOptionMessage2;
            }

            set
            {
                SetProperty(ref _selectOptionMessage2, value, "SelectOptionMessage2");
            }
        }

        void OnLongClick()
        {
            Console.WriteLine("OnLongClick() is invoked!!");
            if (!IsCheckable)
            {
                IsCheckable = true;
            }
        }

        void OnCheckedCounterClicked()
        {
            Console.WriteLine("OnCheckedCounterClicked() is invoked!!");
            PopupVisibility = true;

            Console.WriteLine("Checked is clicked!!!");
        }

        void SelectOption1Job()
        {
            bool r = CheckedNamesCount < Records.Count;
            Console.WriteLine("CheckedNamesCount : " + CheckedNamesCount + " vs. CheckableNames.Count: " + Records.Count);
            foreach (var x in Records)
            {
                x.Checked = r;
            }
        }

        void SelectOption2Job()
        {
            Console.WriteLine("CheckedNamesCount : " + CheckedNamesCount + " vs. CheckableNames.Count: " + Records.Count);
            if (CheckedNamesCount > 0 && CheckedNamesCount != Records.Count)
            {
                foreach (var x in Records)
                {
                    x.Checked = false;
                }
            }
        }

        void UpdateSelectOptionMessage()
        {
            SelectOptionMessage1 = CheckedNamesCount < Records.Count ? SelectAll : DeselectAll;
            SelectOptionMessage2 = CheckedNamesCount != 0 && CheckedNamesCount != Records.Count ? DeselectAll : "";
        }

        async void DeleteRecords()
        {
            Console.WriteLine("   ########  DeleteRecords ");
            for (int i = Records.Count - 1; i >= 0; i--)
            {
                if (Records[i].Checked)
                {
                    await App.Database.DeleteItemAsync(Records[i]);
                    _ContentService.RemoveMediaFile(Records[i].Path);
                    Records.RemoveAt(i);
                }
            }

            IsCheckable = false;
            CheckedNamesCount = 0;
        }

        void ChangeToDeleteMode()
        {
            IsCheckable = true;
        }

        public ICommand SelectCommand1 => new Command(SelectOption1Job);
        public ICommand SelectCommand2 => new Command(SelectOption2Job);
        public ICommand DeleteRecordsCommand => new Command(DeleteRecords);
        public ICommand ChangeToDeleteModeCommand => new Command(ChangeToDeleteMode);

        public static readonly BindableProperty SelectModeActionButtonPressedCommandProperty =
            BindableProperty.Create("SelectModeActionButtonPressedCommand", typeof(Command), typeof(MainPageModel), default(Command));
        public ICommand SelectModeActionButtonPressedCommand => new Command(OnCheckedCounterClicked);

        public static readonly BindableProperty LongClickCommandProperty =
            BindableProperty.Create("LongClickCommand", typeof(Command), typeof(MainPageModel), default(Command));
        public ICommand LongClickCommand => new Command(OnLongClick);

        private const string LANGUAGE_FOR_STT = "language_for_stt";
    }
}
