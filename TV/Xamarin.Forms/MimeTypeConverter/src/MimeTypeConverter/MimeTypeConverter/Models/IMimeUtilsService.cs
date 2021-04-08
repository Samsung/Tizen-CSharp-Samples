/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
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
using System.Collections;

namespace MimeTypeConverter.Models
{
    /// <summary>
    /// Delegate to handle events whenever error occurs.
    /// </summary>
    /// <param name="sender">Instance of the object which invoked the event.</param>
    /// <param name="errorMessage">Error message.</param>
    public delegate void ErrorOccuredDelegate(object sender, string errorMessage);

    /// <summary>
    /// MIME type utils interface.
    /// </summary>
    public interface IMimeUtilsService
    {
        #region properties

        /// <summary>
        /// Event invoked whenever conversion error occurs.
        /// </summary>
        event ErrorOccuredDelegate ErrorOccured;

        #endregion

        #region methods

        /// <summary>
        /// Converts given MIME type to a file extension.
        /// </summary>
        /// <param name="mime">The MIME type.</param>
        /// <returns>Collection of file extensions converted from the MIME type.</returns>
        IEnumerable GetFileExtension(string mime);

        /// <summary>
        /// Converts given file extension to a MIME type.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>The MIME type converted from the file extension.</returns>
        string GetMimeType(string fileExtension);

        #endregion
    }
}