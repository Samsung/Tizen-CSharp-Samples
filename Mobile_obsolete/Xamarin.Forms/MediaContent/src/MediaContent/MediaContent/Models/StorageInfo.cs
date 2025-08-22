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

﻿namespace MediaContent.Models
{
    /// <summary>
    /// StorageInfo class.
    /// Stores information about a storage.
    /// </summary>
    public class StorageInfo
    {
        #region properties

        /// <summary>
        /// Root directory of the storage.
        /// </summary>
        public string RootDirectory { set; get; }

        /// <summary>
        /// Display name of the storage.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return RootDirectory.Substring(RootDirectory.LastIndexOf('/') + 1);
            }
        }

        #endregion
    }
}
