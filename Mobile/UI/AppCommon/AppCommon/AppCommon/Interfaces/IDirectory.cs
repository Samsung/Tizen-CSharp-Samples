/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace AppCommon.Interfaces
{
    /// <summary>
    /// Directory interface implemented in native
    /// </summary>
    public interface IDirectory
    {
        /// <summary>
        /// Returns the names of files (including their paths) in the specified directory.
        /// </summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not
        /// case-sensitive.
        /// </param>
        /// <returns>An array of the full names (including paths) for the files in the specified directory,
        /// or an empty array if no files are found.
        /// </returns>
        string[] GetFiles(string path);

        /// <summary>
        /// Determines whether the given path refers to an existing directory on disk.
        /// </summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not
        /// case-sensitive.
        /// </param>
        /// <returns> true if path refers to an existing directory; false if the directory does not
        /// exist or an error occurs when trying to determine if the specified file exists.
        /// </returns>
        bool Exists(string path);
    }
}