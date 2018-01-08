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
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Badges.Models;

namespace Badges.ViewModels
{
    internal class BadgesViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// StepperValue property back-field.
        /// </summary>
        private double _stepperValue;

        /// <summary>
        /// ItemSelected property back-field.
        /// </summary>
        private AppListItemViewModel _itemSelected;

        /// <summary>
        /// ModificationEnabled property back-field.
        /// </summary>
        private bool _modificationEnabled;

        /// <summary>
        /// GroupedItems property back-field.
        /// </summary>
        private ObservableCollection<ListGroupViewModel> _groupedItems = new ObservableCollection<ListGroupViewModel>();

        /// <summary>
        /// Changeable applications list group of list view.
        /// </summary>
        private ListGroupViewModel _changeableListGroup = new ListGroupViewModel
        {
            Name = "Changeable applications",
            ShortName = "changeable"
        };

        /// <summary>
        /// Others applications list group of list view.
        /// </summary>
        private ListGroupViewModel _othersListGroup = new ListGroupViewModel { Name = "Others", ShortName = "others" };

        #endregion fields

        #region properties

        /// <summary>
        /// Observable collection for grouped list view of applications.
        /// </summary>
        public ObservableCollection<ListGroupViewModel> GroupedItems => _groupedItems;

        /// <summary>
        /// Currently selected application list item.
        /// </summary>
        public AppListItemViewModel ItemSelected
        {
            get => _itemSelected;
            set
            {
                if (value == null || !value.IsAvailable)
                {
                    return;
                }

                if (_itemSelected == null)
                {
                    ModificationEnabled = true; // Allowing badges modification
                }

                _itemSelected = value;
                StepperValue = value.BadgeCounter;
            }
        }

        /// <summary>
        /// Indicates if modification mode is enabled.
        /// </summary>
        public bool ModificationEnabled
        {
            get => _modificationEnabled;
            set => SetProperty(ref _modificationEnabled, value);
        }

        /// <summary>
        /// Badge counter stepper value.
        /// </summary>
        public double StepperValue
        {
            get => _stepperValue;
            set => SetProperty(ref _stepperValue, value);
        }

        /// <summary>
        /// Command for applying badge value.
        /// </summary>
        public ICommand ApplyCommand { get; }

        /// <summary>
        /// Command for reseting badge counter value.
        /// </summary>
        public ICommand ResetCommand { get; }

        #endregion properties

        #region methods

        /// <summary>
        /// Constructor with list initialization. Prepares application list with groups, sets buttons commands and
        /// and invokes application list acquiring.
        /// </summary>
        public BadgesViewModel()
        {
            _groupedItems.Add(_changeableListGroup);
            _groupedItems.Add(_othersListGroup);
            ApplyCommand = new Command(ApplyBadges);
            ResetCommand = new Command(ResetBadges);
            ModificationEnabled = false;
            FillAppList();
        }

        /// <summary>
        /// Adds new application to the application list.
        /// </summary>
        /// <param name="appInfo">New application informations.</param>
        private void AddAppToList(AppInfo appInfo)
        {
            var newCell = new AppListItemViewModel
            {
                ApplicationName = appInfo.AppName,
                ApplicationId = appInfo.AppId,
                BadgeCounter = appInfo.BadgeCounter,
                IsAvailable = appInfo.IsAvailable,
            };

            ListGroupViewModel targetListGroup = appInfo.IsAvailable ? _changeableListGroup : _othersListGroup;
            var index = 0;
            while (index < targetListGroup.Count
                   && string.Compare(targetListGroup[index].ApplicationName, newCell.ApplicationName,
                           StringComparison.OrdinalIgnoreCase) < 0)
            {
                ++index;
            }

            targetListGroup.Insert(index, newCell);
        }

        /// <summary>
        /// Fills applications list with current device applications.
        /// </summary>
        public void FillAppList()
        {
            AppListModel.GetAppList(AddAppToList);
        }

        /// <summary>
        /// Applies new badge counter value for currently selected application.
        /// </summary>
        private void ApplyBadges()
        {
            if (_itemSelected == null)
            {
                return;
            }

            _itemSelected.BadgeCounter = _stepperValue;
            AppListModel.SetBadge(_itemSelected.ApplicationId, Convert.ToInt32(_stepperValue));
        }

        /// <summary>
        /// Resets stepper value to application's original badge counter value.
        /// </summary>
        private void ResetBadges()
        {
            StepperValue = _itemSelected.BadgeCounter;
        }

        #endregion methods
    }
}