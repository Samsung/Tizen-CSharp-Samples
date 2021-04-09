/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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

using ImageViewer.Models;
using ImageViewer.ViewModels;
using ImageViewer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tizen.Content.MediaContent;
using Tizen.System;
using Xamarin.Forms;

namespace MediaContent.ViewModels
{
    /// <summary>
    /// ViewModel class for MainList Page
    /// </summary>
    class MainListPageViewModel : ViewModelBase
    {
        private const string SAMPLE_LOG_TAG = "ImageViewer";

        /// <summary>
        /// Chosen MediaType for query of Media contents
        /// </summary>
        private readonly MediaType _mediaType;

        /// <summary>
        /// Paths of available storages
        /// </summary>
        private IEnumerable<string> _storagesPaths;

        /// <summary>
        /// MediaDatabase instance
        /// </summary>
        private MediaDatabase _mediaDB;

        /// <summary>
        /// Indicates if page initialization is in progress
        /// </summary>
        private bool _inProgress = false;

        /// <summary>
        /// Indicates if FoundFiles collection is empty
        /// </summary>
        private bool _isFoundFilesEmpty = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainListPageViewModel"/> class
        /// </summary>
        /// <param name="navigation">Navigation instance</param>
        public MainListPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _mediaType = MediaType.Image;
            FindFilesAsync();
        }

        /// <summary>
        /// Gets the Navigation instance to push new pages properly
        /// </summary>
        public INavigation Navigation { get; }

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// initialization is in progress
        /// </summary>
        public bool InitializationInProgress
        {
            get => _inProgress;
            set
            {
                SetProperty(ref _inProgress, value);
            }
        }

        /// <summary>
        /// Gets or sets the collection of ImageFileInfo of found files 
        /// </summary>
        public IList<ImageFileModel> FoundFiles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether no files were found
        /// </summary>
        public bool IsFoundFilesEmpty
        {
            get => _isFoundFilesEmpty;
            set
            {
                SetProperty(ref _isFoundFilesEmpty, value);
                OnPropertyChanged(nameof(IsFoundFilesNotEmpty));
            }
        }

        /// <summary>
        /// Gets a value indicating whether any files were found
        /// </summary>
        public bool IsFoundFilesNotEmpty => !IsFoundFilesEmpty;

        /// <summary>
        /// Command which pushes new FileInfoPage. Execute when item on ListView is tapped.
        /// </summary>
        public Command PushFileDetailsPageCommand => new Command(PushFileDetailsPage);

        /// <summary>
        /// Scans storages and sets FoundFiles property with ImageFileInfo of found files
        /// </summary>
        public async void FindFilesAsync()
        {
            _storagesPaths = StorageManager.Storages.Select(x => x.RootDirectory);

            try
            {
                _mediaDB = new MediaDatabase();
                _mediaDB.Connect();

                InitializationInProgress = true;

                await ScanStoragesAsync();

                FoundFiles = GetFilesMediaInfo();

                OnPropertyChanged(nameof(FoundFiles));

                await Task.Delay(1000); // Used for better UX. Ensure activity indicator to show properly
            }
            catch (Exception e)
            {
                Tizen.Log.Error(SAMPLE_LOG_TAG, e.Message + " " + e.GetType());
            }
            finally
            {
                _mediaDB.Disconnect();
                InitializationInProgress = false;
            }

            if (FoundFiles == null || FoundFiles.Count == 0)
            {
                IsFoundFilesEmpty = true;
            }
            else
            {
                IsFoundFilesEmpty = false;
            }
        }

        /// <summary>
        /// Scans all available storages
        /// </summary>
        /// <returns>Task that will complete when all storages 
        /// scans have completed</returns>
        private Task ScanStoragesAsync()
        {
            List<Task> scanTasks = new List<Task>();

            foreach (var path in _storagesPaths)
            {
                scanTasks.Add(_mediaDB.ScanFolderAsync(path));
            }

            return Task.WhenAll(scanTasks);
        }

        /// <summary>
        /// Gets file ImageFileModel from scanned storages
        /// </summary>
        /// <returns>IList of ImageFileModel</returns>
        private IList<ImageFileModel> GetFilesMediaInfo()
        {
            IList<ImageFileModel> files = new ObservableCollection<ImageFileModel>();

            foreach (var path in _storagesPaths)
            {
                var selectArgs = new SelectArguments
                {
                    FilterExpression = $"MEDIA_TYPE = {(int)_mediaType} AND MEDIA_PATH LIKE '{path}%'"
                };

                try
                {
                    var mediaInfoCommand = new MediaInfoCommand(_mediaDB);
                    var selectedMedia = mediaInfoCommand.SelectMedia(selectArgs);

                    while (selectedMedia.Read())
                    {
                        var fileInfo = new ImageFileModel
                        {
                            Info = selectedMedia.Current,
                            DetailsPageCommand = new Command<MediaInfo>(PushFileDetailsPage)
                        };

                        files.Add(fileInfo);
                    }
                }
                catch (Exception e)
                {
                    Tizen.Log.Error(SAMPLE_LOG_TAG, e.Message + " " + e.GetType());
                }
            }

            return files;
        }

        /// <summary>
        /// Pushes new FileInfoPage for the selected file
        /// </summary>
        /// <param name="file">Selected file with info to show</param>
        private async void PushFileDetailsPage(object file)
        {
            await Navigation.PushModalAsync(new FileInfoPage(file));
        }
    }
}
