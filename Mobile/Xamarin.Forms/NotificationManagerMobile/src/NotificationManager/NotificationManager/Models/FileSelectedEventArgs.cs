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

using System;

namespace NotificationManager.Models
{
    /// <summary>
    /// FileSelectedEventArgs class.
    /// An instance of this class is passed with FileSelected event.
    /// </summary>
    public class FileSelectedEventArgs : EventArgs
    {
        #region properties

        /// <summary>
        /// Path to the selected file.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Category of the selected file.
        /// </summary>
        public PathCategory Category { get; }

        #endregion

        #region methods

        /// <summary>
        /// FileSelectedEventArgs class constructor.
        /// </summary>
        /// <param name="path">Path to the selected file.</param>
        /// <param name="category">Path category which should be updated.</param>
        public FileSelectedEventArgs(string path, PathCategory category)
        {
            Path = path;
            Category = category;
        }

        #endregion
    }
}