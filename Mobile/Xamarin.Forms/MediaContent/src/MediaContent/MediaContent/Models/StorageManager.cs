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
using Xamarin.Forms;

namespace MediaContent.Models
{
    /// <summary>
    /// StorageManager class.
    /// Provides method responsible for getting information about all external and internal storages.
    /// </summary>
    class StorageManager
    {
        #region fields

        /// <summary>
        /// An instance of class that implements IStorageManager interface.
        /// </summary>
        private IStorageManager _service;

        #endregion

        #region methods

        /// <summary>
        /// StorageManager class constructor.
        /// </summary>
        public StorageManager()
        {
            _service = DependencyService.Get<IStorageManager>();
        }

        /// <summary>
        /// Gets storages' information.
        /// </summary>
        /// <returns>A collection of storage's information items.</returns>
        public IEnumerable<StorageInfo> GetStorageItems()
        {
            return _service.GetStorageItems();
        }

        #endregion
    }
}
