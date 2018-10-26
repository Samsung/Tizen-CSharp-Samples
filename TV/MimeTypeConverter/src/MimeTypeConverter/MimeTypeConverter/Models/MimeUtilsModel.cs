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
using MimeTypeConverter.Utils;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MimeTypeConverter.Models
{
    /// <summary>
    /// MimeUtilsModel class.
    /// </summary>
    public class MimeUtilsModel
    {
        #region fields

        /// <summary>
        /// Input resolver instance.
        /// </summary>
        private InputRecognizer _inputResolver = new InputRecognizer();

        /// <summary>
        /// Stores platform specific service class instance obtained with dependency injection.
        /// </summary>
        private IMimeUtilsService _service;

        #endregion

        #region properties

        /// <summary>
        /// Event invoked whenever an error occurs.
        /// </summary>
        public event ErrorOccuredDelegate ErrorOccured;

        #endregion

        #region methods

        /// <summary>
        /// MimeUtilsModel class constructor.
        /// </summary>
        public MimeUtilsModel()
        {
            _service = DependencyService.Get<IMimeUtilsService>();
            _service.ErrorOccured += ErrorOccuredEventHandler;
        }

        /// <summary>
        /// Handles "ErrorOccured" of the IMimeUtilsService object.
        /// Invokes "ErrorOccured" to other application's modules.
        /// </summary>
        /// <param name="sender">Instance of a class which invoked the event.</param>
        /// <param name="errorMessage">Error message.</param>
        private void ErrorOccuredEventHandler(object sender, string errorMessage)
        {
            ErrorOccured?.Invoke(this, errorMessage);
        }

        /// <summary>
        /// Converts given MIME type to a file extension.
        /// </summary>
        /// <param name="mime">The MIME type.</param>
        /// <returns>Collection of file extensions converted from the MIME type.</returns>
        public IEnumerable<string> GetFileExtension(string mime)
        {
            return (IEnumerable<string>)_service.GetFileExtension(mime);
        }

        /// <summary>
        /// Converts given file extension to a MIME type.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>The MIME type converted from the file extension.</returns>
        public string GetMimeType(string fileExtension)
        {
            return _service.GetMimeType(fileExtension);
        }

        /// <summary>
        /// Extracts first part from MIME type.
        /// </summary>
        /// <param name="mime">MIME type.</param>
        /// <returns>Extracted string.</returns>
        public string GetFileType(string mime)
        {
            Regex rgx = new Regex("^([a-z]+)", RegexOptions.IgnoreCase);

            return rgx.Match(mime).ToString();
        }

        /// <summary>
        /// Converts given string to a MIME types or extensions list.
        /// </summary>
        /// <param name="input">Input to convert.</param>
        /// <returns>List with conversion results.</returns>
        public IEnumerable<string> GetConversionResults(string input)
        {
            InputType inputType = _inputResolver.GetType(input);
            IEnumerable<string> results = new string[] { };

            if (inputType == InputType.Extension)
            {
                results = new string[] { GetMimeType(input) };
            }
            else if (inputType == InputType.MIMEType)
            {
                results = GetFileExtension(input);
            }

            return results;
        }

        #endregion
    }
}
