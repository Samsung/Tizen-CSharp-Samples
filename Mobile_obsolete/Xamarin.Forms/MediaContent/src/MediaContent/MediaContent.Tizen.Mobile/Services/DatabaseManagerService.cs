/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
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

﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen.Content.MediaContent;
using TizenSystem = Tizen.System;
using MediaContent.Models;
using MediaContent.Tizen.Mobile.Services;
using System.Text;
using System;
using Tizen;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseManagerService))]
namespace MediaContent.Tizen.Mobile.Services
{
    /// <summary>
    /// DatabaseManagerService class.
    /// Provides methods responsible for connecting to the database, disconnecting from
    /// the database, scanning directories and getting information about media content.
    /// </summary>
    class DatabaseManagerService : IDatabaseManager
    {
        #region fields

        /// <summary>
        /// Sample log tag for Media Content application.
        /// </summary>
        private const string SAMPLE_LOG_TAG =
            "MediaContentSample";

        /// <summary>
        /// Media Database error message template.
        /// </summary>
        private const string SELECT_STORAGE_MEDIA_FAIL_MSG =
            "Selecting media information from storage {0} using condition {1} failed.";

        /// <summary>
        /// Directory condition template.
        /// </summary>
        private const string DIRECTORY_CONDITION =
            " AND {0} LIKE '{1}%'";

        /// <summary>
        /// An instance of MediaDatabase class.
        /// </summary>
        private MediaDatabase _mediaDatabase;

        #endregion

        #region methods

        /// <summary>
        /// DatabaseManagerService class constructor.
        /// </summary>
        public DatabaseManagerService()
        {
            _mediaDatabase = new MediaDatabase();
        }

        /// <summary>
        /// DatabaseManagerService class destructor.
        /// </summary>
        ~DatabaseManagerService()
        {
            _mediaDatabase.Dispose();
        }

        /// <summary>
        /// Connects to the database.
        /// </summary>
        public void Connect()
        {
            _mediaDatabase.Connect();
        }

        /// <summary>
        /// Disconnects from the database.
        /// </summary>
        public void Disconnect()
        {
            _mediaDatabase.Disconnect();
        }

        /// <summary>
        /// Converts 'MediaInfo' object to 'FileInfo' object.
        /// </summary>
        /// <param name="mediaInfo">'MediaInfo' object which is to be converted.</param>
        /// <returns>Created 'FileInfo' object.</returns>
        private FileInfo MediaInfoToFileInfo(MediaInfo mediaInfo)
        {
            return new FileInfo
            {
                Altitude = mediaInfo.Altitude,
                DateAdded = mediaInfo.DateAdded,
                DateModified = mediaInfo.DateModified,
                Description = mediaInfo.Description,
                DisplayName = mediaInfo.DisplayName,
                FileSize = mediaInfo.FileSize,
                Id = mediaInfo.Id,
                IsDrm = mediaInfo.IsDrm,
                IsFavorite = mediaInfo.IsFavorite,
                Latitude = mediaInfo.Latitude,
                Longitude = mediaInfo.Longitude,
                MediaType = mediaInfo.MediaType.ToString(),
                MimeType = mediaInfo.MimeType,
                Path = mediaInfo.Path,
                Rating = mediaInfo.Rating,
                ThumbnailPath = mediaInfo.ThumbnailPath,
                Timeline = mediaInfo.Timeline,
                Title = mediaInfo.Title
            };
        }

        /// <summary>
        /// Gets files' information.
        /// </summary>
        /// <param name="storageIdItems">The collection of storages' IDs which are to be analyzed.</param>
        /// <param name="type">The type of file's content.</param>
        /// <returns>A collection of file's information items which meet the terms of filtering.</returns>
        public IEnumerable<FileInfo> GetFileItems(IEnumerable<string> storageIdItems, Constants.MediaContentType type)
        {
            List<FileInfo> fileItems = new List<FileInfo>();

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("MEDIA_TYPE = ");
            stringBuilder.Append((int)type);

            foreach (var storageId in storageIdItems)
            {
                var selectArgs = new SelectArguments
                {
                    FilterExpression = stringBuilder.ToString() + String.Format(DIRECTORY_CONDITION, MediaInfoColumns.Path, storageId)
                };

                try
                {
                    var mediaInfoCommand = new MediaInfoCommand(_mediaDatabase);
                    var selectedMedia = mediaInfoCommand.SelectMedia(selectArgs);

                    while (selectedMedia.Read())
                    {
                        fileItems.Add(MediaInfoToFileInfo(selectedMedia.Current));
                    }
                }
                catch (Exception e)
                {
                    if (e is ArgumentNullException || e is InvalidOperationException || e is ObjectDisposedException)
                    {
                        Log.Error(SAMPLE_LOG_TAG, e.Message);
                    }
                    else if (e is MediaDatabaseException)
                    {
                        Log.Warn(SAMPLE_LOG_TAG, String.Format(SELECT_STORAGE_MEDIA_FAIL_MSG, storageId, selectArgs.FilterExpression));
                    }
                    else
                    {
                        Log.Warn(SAMPLE_LOG_TAG, e.Message);
                        throw;
                    }
                }
            }

            return fileItems;
        }

        /// <summary>
        /// Scans directories given as a parameter.
        /// </summary>
        /// <param name="rootDirectoryItems">Path to directories which are to be scanned.</param>
        /// <returns>Scanning directories task.</returns>
        public Task ScanFolderAsync(IEnumerable<string> rootDirectoryItems)
        {
            var storageItems = TizenSystem.StorageManager.Storages;

            List<Task> scanTasks = new List<Task>();

            foreach (var rootDirectory in rootDirectoryItems)
            {
                scanTasks.Add(_mediaDatabase.ScanFolderAsync(rootDirectory));
            }

            return Task.WhenAll(scanTasks);
        }

        #endregion
    }
}
