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
using Contacts.Models;
using Contacts.Views;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    /// <summary>
    /// A class for ViewModel of ListPage.
    /// </summary>
    /// <seealso cref="Views.ListPage"/>
    public class ListPageViewModel : ViewModelBase
    {
        /// <summary>
        /// A RecordItem list for displaying Items in ListPage.
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
            RecordItemProvider.Instance.GetAll();
            RecordList = RecordItemProvider.Instance.ItemList;
        }

        /// <summary>
        /// A Constructor of ListPageViewModel.
        /// </summary>
        public ListPageViewModel()
        {
            UpdateListCommand = new Command(() => UpdateList());
        }
    }
}
