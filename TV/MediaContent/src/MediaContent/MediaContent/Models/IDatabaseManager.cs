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
using System.Threading.Tasks;

namespace MediaContent.Models
{
    /// <summary>
    /// IDatabaseManager interface.
    /// </summary>
    public interface IDatabaseManager
    {
        #region methods

        /// <summary>
        /// Connects to the database.
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnects from the database.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Scans directories given as a parameter.
        /// </summary>
        /// <param name="rootDirectoryItems">Path to directories which are to be scanned.</param>
        /// <returns>Scanning directories task.</returns>
        Task ScanFolderAsync(IEnumerable<string> rootDirectoryItems);

        /// <summary>
        /// Gets files' information.
        /// </summary>
        /// <param name="storageIdItems">The collection of storages' IDs which are to be analyzed.</param>
        /// <param name="type">The type of file's content.</param>
        /// <returns>A collection of file's information items which meet the terms of filtering.</returns>
        IEnumerable<FileInfo> GetFileItems(IEnumerable<string> storageIdItems, Constants.MediaContentType type);

        #endregion
    }
}
