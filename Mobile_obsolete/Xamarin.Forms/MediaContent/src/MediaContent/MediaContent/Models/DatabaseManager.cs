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

﻿using MediaContent.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaContent.Models
{
    /// <summary>
    /// DatabaseManager class.
    /// Provides methods responsible for connecting to the database, disconnecting from
    /// the database, scanning directories and getting information about media content.
    /// </summary>
    class DatabaseManager
    {
        #region fields

        /// <summary>
        /// An instance of class that implements IDatabaseManager interface.
        /// </summary>
        IDatabaseManager _service;

        #endregion

        #region methods

        /// <summary>
        /// DatabaseManager class constructor.
        /// </summary>
        public DatabaseManager()
        {
            _service = DependencyService.Get<IDatabaseManager>();
        }

        /// <summary>
        /// Connects to the database.
        /// </summary>
        public void Connect()
        {
            _service.Connect();
        }

        /// <summary>
        /// Disconnects from the database.
        /// </summary>
        public void Disconnect()
        {
            _service.Disconnect();
        }

        /// <summary>
        /// Scans directories given as a parameter.
        /// </summary>
        /// <param name="rootDirectoryItems">Path to directories which are to be scanned.</param>
        /// <returns>Scanning directories task.</returns>
        public Task ScanFolderAsync(IEnumerable<string> rootDirectoryItems)
        {
            return _service.ScanFolderAsync(rootDirectoryItems);
        }

        /// <summary>
        /// Gets files' information.
        /// </summary>
        /// <param name="storageIdItems">The collection of storages' IDs which are to be analyzed.</param>
        /// <param name="type">The type of file's content.</param>
        /// <returns>A collection of file's information items which meet the terms of filtering.</returns>
        public IEnumerable<FileInfo> GetFileItems(IEnumerable<string> storageIdItems, MediaContentType type)
        {
            return _service.GetFileItems(storageIdItems, type);
        }

        #endregion
    }
}
