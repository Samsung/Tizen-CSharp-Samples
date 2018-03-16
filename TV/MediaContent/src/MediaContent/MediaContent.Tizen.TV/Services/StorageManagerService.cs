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

using MediaContent.Models;
using MediaContent.Tizen.TV.Services;
using System.Collections.Generic;
using Tizen.System;

[assembly: Xamarin.Forms.Dependency(typeof(StorageManagerService))]
namespace MediaContent.Tizen.TV.Services
{
    /// <summary>
    /// StorageManagerService class.
    /// Provides method responsible for getting information about all external and internal storages.
    /// </summary>
    class StorageManagerService : IStorageManager
    {
        #region methods

        /// <summary>
        /// Converts 'Storage' object to 'StorageInfo' object.
        /// </summary>
        /// <param name="storage">'Storage' object which is to be converted.</param>
        /// <returns>Created 'StorageInfo' object.</returns>
        private StorageInfo StorageToStorageInfo(Storage storage)
        {
            return new StorageInfo
            {
                RootDirectory = storage.RootDirectory
            };
        }

        /// <summary>
        /// Gets storages' information.
        /// </summary>
        /// <returns>A collection of storage's information items.</returns>
        public IEnumerable<StorageInfo> GetStorageItems()
        {
            var storageItems = StorageManager.Storages;

            List<StorageInfo> storageInfoItems = new List<StorageInfo>();

            foreach (var storage in storageItems)
            {
                storageInfoItems.Add(StorageToStorageInfo(storage));
            }

            return storageInfoItems;
        }

        #endregion
    }
}
