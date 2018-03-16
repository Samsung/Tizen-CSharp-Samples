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

using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using MediaContent.Navigation;
using MediaContent.Models;
using System;
using MediaContent.Constants;
using System.Linq;

namespace MediaContent.ViewModels
{
    /// <summary>
    /// MediaContentViewModel class.
    /// It implements INotifyPropertyChanged interface.
    /// Provides commands and methods responsible for application view model state.
    /// </summary>
    public class MediaContentViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// An instance of DatabaseManager class.
        /// </summary>
        private DatabaseManager _databaseManager;

        /// <summary>
        /// An instance of StorageManager class.
        /// </summary>
        private StorageManager _storageManager;

        /// <summary>
        /// A collection of available storages.
        /// </summary>
        private IEnumerable<StorageInfo> _storageItems;

        /// <summary>
        /// A collection of available files which meet the terms
        /// of filtering.
        /// </summary>
        private IEnumerable<FileInfo> _fileItems;

        /// <summary>
        /// A collection of selected storages' names.
        /// </summary>
        private IEnumerable<string> _selectedStorageItems;

        /// <summary>
        /// List of storages' root directories.
        /// </summary>
        private List<string> _storageRootDirectories;

        /// <summary>
        /// Flag indicating if new update of files can be initiated.
        /// </summary>
        private bool _canInitiateNewUpdate = true;

        #endregion

        #region properties

        /// <summary>
        /// An instance of PageNavigation class.
        /// </summary>
        public PageNavigation AppPageNavigation { private set; get; }

        /// <summary>
        /// Property consists of a collection of available storages.
        /// </summary>
        public IEnumerable<StorageInfo> StorageItems
        {
            get { return _storageItems; }
            set { SetProperty(ref _storageItems, value); }
        }

        /// <summary>
        /// Property consists of a collection of available files which
        /// meet the terms of filtering.
        /// </summary>
        public IEnumerable<FileInfo> FileItems
        {
            get { return _fileItems; }
            set { SetProperty(ref _fileItems, value); }
        }

        /// <summary>
        /// Property consists of a collection of selected storages' names.
        /// </summary>
        public IEnumerable<string> SelectedStorageItems
        {
            get { return _selectedStorageItems; }
            set { SetProperty(ref _selectedStorageItems, value); }
        }

        /// <summary>
        /// An instance of FileInfo class.
        /// </summary>
        public FileInfo SelectedFile { get; set; }

        /// <summary>
        /// Command responsible for changing application's page.
        /// </summary>
        public ICommand FileInformationCommand { get; private set; }

        /// <summary>
        /// Command responsible for updating collection of files.
        /// </summary>
        public ICommand UpdateFileItemsCommand { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Executes FileInformationCommand command.
        /// Navigates to the file information page by executing NavigateToCommand command.
        /// </summary>
        /// <param name="obj">New page type.</param>
        private void ShowFileInformation(object obj)
        {
            AppPageNavigation.NavigateToCommand.Execute(obj);
        }

        /// <summary>
        /// Executes UpdateFileItemsCommand command.
        /// Updates a collection of available files' names.
        /// </summary>
        /// <param name="mediaContentType">Type of files to update.</param>
        private async void UpdateFileItems(string mediaContentType)
        {
            try
            {
                CanInitiateNewUpdate(false);
                MediaContentType type = (MediaContentType)Enum.Parse(typeof(MediaContentType), mediaContentType);
                _databaseManager.Connect();
                await _databaseManager.ScanFolderAsync(_storageRootDirectories);
                FileItems = _databaseManager.GetFileItems(SelectedStorageItems, type);
            }
            finally
            {
                CanInitiateNewUpdate(true);
                _databaseManager.Disconnect();
            }
        }

        /// <summary>
        /// Returns true if command updating file items can be executed, false otherwise.
        /// </summary>
        /// <param name="mediaContentType">Type of files to update.</param>
        /// <returns>True if command can be executed, false otherwise.</returns>
        private bool CanExecuteUpdateFileItems(string mediaContentType)
        {
            return _canInitiateNewUpdate;
        }

        /// <summary>
        /// Blocks or unblocks initiating a new files update.
        /// </summary>
        /// <param name="value">True if initiating should be blocked, false otherwise.</param>
        private void CanInitiateNewUpdate(bool value)
        {
            if (value == _canInitiateNewUpdate)
            {
                return;
            }

            _canInitiateNewUpdate = value;
            ((Command)UpdateFileItemsCommand).ChangeCanExecute();
        }

        /// <summary>
        /// MediaContentViewModel class constructor.
        /// </summary>
        /// <param name="pageNavigation">An instance of PageNavigation class.</param>
        public MediaContentViewModel(PageNavigation pageNavigation)
        {
            SelectedStorageItems = Enumerable.Empty<string>();

            _databaseManager = new DatabaseManager();
            _storageManager = new StorageManager();

            AppPageNavigation = pageNavigation;
            FileInformationCommand = new Command(ShowFileInformation);
            UpdateFileItemsCommand = new Command<string>(UpdateFileItems, CanExecuteUpdateFileItems);

            _storageRootDirectories = new List<string>();

            StorageItems = _storageManager.GetStorageItems();
            foreach (var storage in StorageItems)
            {
                _storageRootDirectories.Add(storage.RootDirectory);
            }
        }

        #endregion
    }
}
