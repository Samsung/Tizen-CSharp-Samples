/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

using Alarms.Models;
using Alarms.Views;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using Xamarin.Forms;

namespace Alarms.ViewModels
{
    /// <summary>
    /// Main view model for application.
    /// </summary>
    public class NewAlarmViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// After delay alarm type selection item text.
        /// </summary>
        public const string ALARM_TYPE_AFTER_DELAY = "After delay";

        /// <summary>
        /// At specified date alarm type selection item text.
        /// </summary>
        public const string ALARM_TYPE_AT_DATE = "At specified date";

        /// <summary>
        /// Reference object for navigation between pages obtained in constructor using DependencyService.
        /// </summary>
        private readonly IPageNavigation _navigation;

        /// <summary>
        /// Reference to edited alarm. If we are creating new alarm it should remain null value.
        /// </summary>
        public object AlarmReference;

        /// <summary>
        /// Currently selected item reference.
        /// </summary>
        private AppInfo _selectedApplication;

        /// <summary>
        /// Currently selected alarm type.
        /// </summary>
        private string _selectedAlarmType;

        /// <summary>
        /// Backing field for the <see cref="IsRepeatEnabled"/> property.
        /// </summary>
        private bool _isRepeatEnabled = false;

        /// <summary>
        /// Backing field for <see cref="AlarmDate"/> property.
        /// </summary>
        private DateTime _alarmDate;

        /// <summary>
        /// Backing field for the <see cref="AlarmTime"/> property.
        /// </summary>
        private TimeSpan _alarmTime;

        #endregion

        #region properties

        /// <summary>
        /// Indicates if the alarm is created or edited.
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// Delay hours property for binding.
        /// </summary>
        public int AlarmDelayHours { get; set; }

        /// <summary>
        /// Delay minutes property for binding.
        /// </summary>
        public int AlarmDelayMinutes { get; set; }

        /// <summary>
        /// Delay value in seconds for alarm in delay mode.
        /// </summary>
        public int AlarmDelay => AlarmDelayHours * 60 * 60 + AlarmDelayMinutes * 60;

        /// <summary>
        /// Exact date/time for date/time alarm mode.
        /// </summary>
        public DateTime AlarmDateTime => new DateTime(AlarmDate.Year, AlarmDate.Month, AlarmDate.Day, AlarmTime.Hours,
            AlarmTime.Minutes, AlarmTime.Seconds);

        /// <summary>
        /// Chosen date property for the new alarm.
        /// </summary>
        public DateTime AlarmDate
        {
            get => _alarmDate;
            set => SetProperty(ref _alarmDate, value);
        }

        /// <summary>
        /// Chosen time property for the new alarm.
        /// </summary>
        public TimeSpan AlarmTime
        {
            get => _alarmTime;
            set => SetProperty(ref _alarmTime, value);
        }

        /// <summary>
        /// Property with repeated days array.
        /// </summary>
        public DaysOfWeekFlags SelectedDays { get; set; } = new DaysOfWeekFlags();

        /// <summary>
        /// Property for indicating if the repeating option is enabled.
        /// </summary>
        public bool IsRepeatEnabled
        {
            get => _isRepeatEnabled;
            set => SetProperty(ref _isRepeatEnabled, value);
        }

        /// <summary>
        /// Property for application list item selection maintenance.
        /// </summary>
        public AppInfo SelectedApplication
        {
            get => null;    // we don't want item to remain selected
            set
            {
                if (value != null)
                {
                    SetProperty(ref _selectedApplication, value);

                    if (IsNew)
                    {
                        _navigation.GoToAlarmTypeSelectPage(this);
                    }
                }
            }
        }

        /// <summary>
        /// Property with alarm type selection maintenance.
        /// </summary>
        public String SelectedAlarmType
        {
            get => null;    // we don't want item to remain selected
            set
            {
                if (value != null)
                {
                    _selectedAlarmType = value;
                    switch (_selectedAlarmType)
                    {
                        case ALARM_TYPE_AFTER_DELAY:
                            _navigation.GoToDelayAlarmSettingsPage(this);
                            break;

                        case ALARM_TYPE_AT_DATE:
                            _navigation.GoToDateAlarmSettingsPage(this);
                            break;

                        default:
                            throw new AmbiguousMatchException();
                    }

                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Command to save the alarm.
        /// </summary>
        public Command SaveAlarmCommand { get; }

        /// <summary>
        /// Command to set current date on <see cref="AlarmDate"/>.
        /// </summary>
        public Command SetCurrentDateCommand { get; }

        /// <summary>
        /// Command to set current time on <see cref="AlarmTime"/>.
        /// </summary>
        public Command SetCurrentTimeCommand { get; }

        /// <summary>
        /// Application list items for selection.
        /// </summary>
        public ObservableCollection<AppInfo> AppListItems { get; } = new ObservableCollection<AppInfo>();

        /// <summary>
        /// Property for OK dialog (created in View).
        /// </summary>
        public Command OKDialogCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// Initializes all provided commands.
        /// Sets current date and time by using SetCurrentDate and SetCurrentTime methods.
        /// Obtains instance of IPageNavigation interface.
        /// Executes GetAppList method which uses Application API
        /// and returns information about all installed applications.
        /// </summary>
        public NewAlarmViewModel()
        {
            IsNew = true;
            SaveAlarmCommand = new Command(SaveAlarm);
            SetCurrentDateCommand = new Command(SetCurrentDate);
            SetCurrentTimeCommand = new Command(SetCurrentTime);
            SetCurrentDate();
            SetCurrentTime();

            _navigation = DependencyService.Get<IPageNavigation>();
            DependencyService.Get<IAppListService>().GetAppList(AddAppToList);
        }

        /// <summary>
        /// Saves currently configured alarm on the target OS.
        /// </summary>
        private void SaveAlarm()
        {
            AlarmInfoViewModel alarmInfo = new AlarmInfoViewModel
            {
                AlarmReference = AlarmReference,
                AppInfo = _selectedApplication,
            };

            if (_selectedAlarmType == ALARM_TYPE_AT_DATE)
            {
                if (AlarmDateTime < DateTime.Now)
                {
                    OKDialogCommand?.Execute(null);
                    return;
                }

                alarmInfo.Date = AlarmDateTime;
                if (IsRepeatEnabled && SelectedDays.IsAny())
                {
                    alarmInfo.IsRepeatEnabled = true;
                    alarmInfo.DaysFlags = SelectedDays;
                }
            }
            else
            {
                if (AlarmDelay <= 0)
                {
                    OKDialogCommand?.Execute(null);
                    return;
                }

                alarmInfo.Delay = AlarmDelay;
            }

            if (IsNew)
            {
                DependencyService.Get<IAlarmListService>().SetAlarm(alarmInfo);
            }
            else
            {
                AlarmListModel.Instance.ChangeEditedElementID(
                    DependencyService.Get<IAlarmListService>().SetAlarm(alarmInfo));
            }

            _navigation.GoToAlarmListPage();
        }

        /// <summary>
        /// Adds new application to the application list.
        /// </summary>
        /// <param name="appInfo">New application informations.</param>
        private void AddAppToList(AppInfo appInfo)
        {
            var index = 0;
            while (index < AppListItems.Count
                   && string.Compare(AppListItems[index].AppName, appInfo.AppName,
                       StringComparison.OrdinalIgnoreCase) < 0)
            {
                ++index;
            }

            AppListItems.Insert(index, appInfo);
        }

        /// <summary>
        /// Sets current date on AlarmDate property.
        /// </summary>
        private void SetCurrentDate()
        {
            AlarmDate = DateTime.Now;
        }

        /// <summary>
        /// Sets current time on AlarmTime property.
        /// </summary>
        private void SetCurrentTime()
        {
            AlarmTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        #endregion
    }
}
