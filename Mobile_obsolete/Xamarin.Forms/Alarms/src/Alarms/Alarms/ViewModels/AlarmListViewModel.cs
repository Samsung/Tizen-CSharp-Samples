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
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Alarms.Views;
using Xamarin.Forms;
using System.Collections.Specialized;

namespace Alarms.ViewModels
{
    /// <summary>
    /// View model for list of currently set alarms.
    /// </summary>
    public class AlarmListViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Currently shown alarm list.
        /// </summary>
        private ObservableCollection<AlarmInfoViewModel> _alarmList = new ObservableCollection<AlarmInfoViewModel>();

        /// <summary>
        /// Current state of "Select all" button.
        /// </summary>
        private bool _selectAllState;

        /// <summary>
        /// Alarm selected for edition.
        /// </summary>
        private AlarmInfoViewModel _selectedAlarm;

        /// <summary>
        /// Backing field for the <see cref="IsAnyAlarmSelected"/> property.
        /// </summary>
        private bool _isAnyAlarmSelected;

        /// <summary>
        /// Backing field for the <see cref="OnResultReceivedCommand"/> property.
        /// </summary>
        private Command _onResultReceivedCommand;

        #endregion

        #region properties

        /// <summary>
        /// Currently selected alarm information.
        /// </summary>
        public AlarmInfoViewModel SelectedAlarm
        {
            get => null;    // we don't want item to remain selected
            set
            {
                SetProperty(ref _selectedAlarm, value);
                if (value != null)
                {
                    EditAlarm();
                }
            }
        }

        /// <summary>
        /// Property for "Select all" state maintenance.
        /// </summary>
        public bool SelectAllState
        {
            get => _selectAllState;
            set
            {
                if (!SetProperty(ref _selectAllState, value))
                {
                    return;
                }

                SelectAllAlarms(value);
            }
        }

        /// <summary>
        /// Alarm list items for selection.
        /// </summary>
        public ObservableCollection<AlarmInfoViewModel> AlarmListItems
        {
            get
            {
                RefreshAlarmList();
                return _alarmList;
            }
        }

        /// <summary>
        /// Command for selected alarms deleting.
        /// </summary>
        public Command DeleteAlarmsCommand { get; }

        /// <summary>
        /// Indicates if any alarm on the list is selected.
        /// </summary>
        public bool IsAnyAlarmSelected
        {
            get => _isAnyAlarmSelected;
            set => SetProperty(ref _isAnyAlarmSelected, value);
        }

        /// <summary>
        /// Property for Yes/No dialog.
        /// </summary>
        public Command YesNoDialogCommand { get; set; }

        /// <summary>
        /// Property for OK dialog.
        /// </summary>
        public Command OKDialogCommand { get; set; }

        /// <summary>
        /// Property for adding new alarm.
        /// </summary>
        public Command AddNewAlarmCommand { get; set; }

        /// <summary>
        /// Command which refreshes list of alarms.
        /// </summary>
        public Command RefreshCommand { get; }

        /// <summary>
        /// Command executed on dialog result received.
        /// </summary>
        public Command OnResultReceivedCommand
        {
            get => _onResultReceivedCommand;
            set => SetProperty(ref _onResultReceivedCommand, value);
        }

        /// <summary>
        /// Actual dialog result.
        /// </summary>
        public bool DialogResult { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// Initializes all provided commands.
        /// Assigns handler of the SelectionChanged event of the AlarmInfo class.
        /// </summary>
        public AlarmListViewModel()
        {
            DeleteAlarmsCommand = new Command(DeleteAlarms);
            OnResultReceivedCommand = new Command(DeleteAlarmOnResponse);
            RefreshCommand = new Command(ExecuteRefresh);
            AddNewAlarmCommand = new Command(AddNewAlarm);

            AlarmInfoViewModel.SelectionChanged += SelectionChanged;
            _alarmList.CollectionChanged += OnAlarmListCollectionChanged;
        }

        /// <summary>
        /// Class destructor.
        /// Removes local callback and disposes alarm list refresh timer.
        /// </summary>
        ~AlarmListViewModel()
        {
            AlarmInfoViewModel.SelectionChanged -= SelectionChanged;
        }

        /// <summary>
        /// Alarm list collection changed event handler.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void OnAlarmListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                RetrieveSelection();
            }
        }

        /// <summary>
        /// Retrieves selection from model.
        /// </summary>
        public void RetrieveSelection()
        {
            foreach (AlarmInfoViewModel item in _alarmList)
            {
                foreach (AlarmListElementInfo element in AlarmListModel.Instance.AlarmsList)
                {
                    if (item.AlarmId == element.AlarmID)
                    {
                        item.IsSelected = element.IsSelected;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Updates alarm list observable collection with current list of alarms.
        /// </summary>
        public void RefreshAlarmList()
        {
            var currentAlarmList = DependencyService.Get<IAlarmListService>().GetAlarmList();

            // Remove non-existing alarms.
            for (int i = 0; i < _alarmList.Count;)
            {
                if (!currentAlarmList.Any(alarm => alarm.AlarmId == _alarmList[i].AlarmId))
                {
                    _alarmList.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }

            // Add new alarms.
            foreach (var alarmInfo in currentAlarmList)
            {
                if (_alarmList.All(alarm => alarm.AlarmId != alarmInfo.AlarmId))
                {
                    _alarmList.Add(alarmInfo);
                }
            }

            if (_alarmList.Count == 0)
            {
                DependencyService.Get<IPageNavigation>().GoToNoAlarmsPage();
            }

            SortAlarmsByDate();
        }

        /// <summary>
        /// Sorts alarm list in ascending date order.
        /// </summary>
        private void SortAlarmsByDate()
        {
            var sortedList = _alarmList.OrderBy(alarmInfo => alarmInfo.Date).ToList();

            int i = 0;
            int oldIndex;
            foreach (var item in sortedList)
            {
                oldIndex = _alarmList.IndexOf(item);

                if (i != oldIndex)
                {
                    _alarmList.Move(oldIndex, i);
                }

                i++;
            }
        }

        /// <summary>
        /// Saves selection to model.
        /// </summary>
        private void SaveSelection()
        {
            foreach (AlarmInfoViewModel item in _alarmList)
            {
                if (item != _selectedAlarm)
                {
                    AlarmListModel.Instance.AddElement(item.AlarmId, item.IsSelected);
                }
                else
                {
                    AlarmListModel.Instance.AddElement(-1, item.IsSelected);
                }
            }
        }

        /// <summary>
        /// Selects or deselects all alarms.
        /// </summary>
        /// <param name="newValue">New select value.</param>
        private void SelectAllAlarms(bool newValue)
        {
            foreach (var alarmInfo in _alarmList)
            {
                alarmInfo.IsSelected = newValue;
            }
        }

        /// <summary>
        /// Navigates to the AppSelectPage.
        /// </summary>
        private void AddNewAlarm()
        {
            AlarmListModel.Instance.ClearList();
            SaveSelection();

            DependencyService.Get<IPageNavigation>().GoToAppSelectPage();
        }

        /// <summary>
        /// Edits currently selected alarm.
        /// </summary>
        private void EditAlarm()
        {
            AlarmListModel.Instance.ClearList();
            SaveSelection();

            NewAlarmViewModel vm = new NewAlarmViewModel()
            {
                IsNew = false,
                AlarmReference = _selectedAlarm.AlarmReference,
                SelectedApplication = _selectedAlarm.AppInfo,
                AlarmDate = _selectedAlarm.Date,
                AlarmTime = new TimeSpan(_selectedAlarm.Date.Hour, _selectedAlarm.Date.Minute,
                _selectedAlarm.Date.Second),
                IsRepeatEnabled = _selectedAlarm.IsRepeatEnabled,
                SelectedDays = _selectedAlarm.DaysFlags,
                AlarmDelayHours = _selectedAlarm.Delay / (60 * 60),
                AlarmDelayMinutes = (_selectedAlarm.Delay / 60) % 60,
            };
            vm.SelectedAlarmType = _selectedAlarm.Delay > 0
                ? NewAlarmViewModel.ALARM_TYPE_AFTER_DELAY
                : NewAlarmViewModel.ALARM_TYPE_AT_DATE;
        }

        /// <summary>
        /// Depending on the value of the IsAnyAlarmSelected property
        /// it executes OKDialogCommand or YesNoDialogCommand command.
        /// </summary>
        private void DeleteAlarms()
        {
            if (!IsAnyAlarmSelected)
            {
                OKDialogCommand?.Execute(null);
                return;
            }

            YesNoDialogCommand?.Execute(null);
        }

        /// <summary>
        /// Method executed when delete dialog response is received.
        /// </summary>
        private void DeleteAlarmOnResponse()
        {
            if (!DialogResult)
            {
                return;
            }

            foreach (var alarmInfo in _alarmList)
            {
                if (alarmInfo.IsSelected)
                {
                    DependencyService.Get<IAlarmListService>().RemoveAlarm(alarmInfo.AlarmReference);
                }
            }

            RefreshAlarmList();
        }

        /// <summary>
        /// Event handler called on alarm selection change.
        /// </summary>
        /// <param name="sender">Selection object.</param>
        /// <param name="e">Event arguments.</param>
        public void SelectionChanged(object sender, EventArgs e)
        {
            bool areAllSelected = true;
            bool isAnySelected = false;

            foreach (var alarmInfo in _alarmList)
            {
                if (alarmInfo.IsSelected)
                {
                    isAnySelected = true;
                }
                else
                {
                    areAllSelected = false;
                }
            }

            IsAnyAlarmSelected = isAnySelected;
            SetProperty(ref _selectAllState, areAllSelected, "SelectAllState");
        }

        /// <summary>
        /// Handles execution of refresh command.
        /// Refreshes list of alarms.
        /// </summary>
        private void ExecuteRefresh()
        {
            RefreshAlarmList();
        }

        #endregion
    }
}