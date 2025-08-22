/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd
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
using Calendar.Models;
using Calendar.Views;
using Xamarin.Forms;

namespace Calendar.ViewModels
{
    /// <summary>
    /// A class for ViewModel of MonthPage.
    /// </summary>
    /// <seealso cref="Views.MonthPage"/>
    public class MonthPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the MonthDateTime.
        /// </summary>
        public DateTime MonthDateTime
        {
            get => CacheData.CurrentDateTime;
            private set
            {
                CacheData.CurrentDateTime = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A command for launching next button method.
        /// This command gets executed when Button is clicked.
        /// </summary>
        private Command nextButtonCommand;
        /// <summary>
        /// Gets or sets the nextButtonCommand.
        /// </summary>
        public Command NextButtonCommand
        {
            get => nextButtonCommand;
            set
            {
                nextButtonCommand = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A command for launching prevous button method.
        /// This command gets executed when Button is clicked.
        /// </summary>
        private Command prevButtonCommand;
        /// <summary>
        /// Gets or sets the prevButtonCommand.
        /// </summary>
        public Command PrevButtonCommand
        {
            get => prevButtonCommand;
            set
            {
                prevButtonCommand = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A RecordItem list for displaying Items in MonthPage.
        /// </summary>
        private List<RecordItem> recordList;

        /// <summary>
        /// Gets or sets the recordList.
        /// </summary>
        public List<RecordItem> RecordList
        {
            get => recordList;
            private set
            {
                recordList = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A command for update list.
        /// </summary>
        private Command updateListCommand;

        /// <summary>
        /// Gets or sets the updateListCommand.
        /// </summary>
        public Command UpdateListCommand
        {
            get => updateListCommand;
            set
            {
                updateListCommand = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A method for getting RecordItem list from RecordItemProvider.
        /// </summary>
        private void UpdateList()
        {
            MonthDateTime = CacheData.CurrentDateTime;
            //RecordItemProvider.Instance.GetMonthRecords(MonthDateTime);
            //RecordList = RecordItemProvider.Instance.ItemList;
        }

        /// <summary>
        /// A Constructor of MonthPageViewModel.
        /// </summary>
        public MonthPageViewModel()
        {
            CacheData.CurrentDateTime = DateTime.Now;

            PrevButtonCommand = new Command(() =>
            {
                CacheData.CurrentDateTime = CacheData.CurrentDateTime.AddDays(-1);
                UpdateList();
            });
            NextButtonCommand = new Command(() =>
            {
                CacheData.CurrentDateTime = CacheData.CurrentDateTime.AddDays(1);
                UpdateList();
            });
            UpdateListCommand = new Command(() => UpdateList());
        }
    }
}
