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
using System;
using System.Collections;
using MimeTypeConverter.Models;
using MimeTypeConverter.Tizen.TV.Services;
using Tizen.Content.MimeType;
using Xamarin.Forms;

[assembly: Dependency(typeof(MimeUtilsService))]

namespace MimeTypeConverter.Tizen.TV.Services
{
    /// <summary>
    /// MimeUtilsService class.
    /// Allows to convert a file extension to a MIME type and vice versa.
    /// Implements IMimeUtilsService interface.
    /// </summary>
    public class MimeUtilsService : IMimeUtilsService
    {
        #region properties

        /// <summary>
        /// Event invoked whenever an error occurs.
        /// </summary>
        public event ErrorOccuredDelegate ErrorOccured;

        #endregion

        #region methods

        /// <summary>
        /// Converts given MIME type to a file extension.
        /// </summary>
        /// <param name="mime">The MIME type.</param>
        /// <returns>Collection of file extensions converted from the MIME type.</returns>
        public IEnumerable GetFileExtension(string mime)
        {
            try
            {
                return MimeUtil.GetFileExtension(mime.ToLower());
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }

            return null;
        }

        /// <summary>
        /// Converts given file extension to a MIME type.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>The MIME type converted from the file extension.</returns>
        public string GetMimeType(string fileExtension)
        {
            try
            {
                return MimeUtil.GetMimeType(fileExtension.ToLower());
            }
            catch (Exception exception)
            {
                ErrorHandler(exception.Message);
            }

            return "";
        }

        /// <summary>
        /// Invokes "ErrorOccured" to other application's modules.
        /// </summary>
        /// <param name="errorMessage">Message of a thrown error.</param>
        private void ErrorHandler(string errorMessage)
        {
            ErrorOccured?.Invoke(this, errorMessage);
        }

        #endregion
    }
}