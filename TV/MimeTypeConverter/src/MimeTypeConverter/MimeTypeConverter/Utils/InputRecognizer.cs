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
using System.Text.RegularExpressions;

namespace MimeTypeConverter.Utils
{
    /// <summary>
    /// Provides method for recognizing string content type.
    /// </summary>
    public class InputRecognizer
    {
        #region fields

        /// <summary>
        /// MIME type matching pattern.
        /// </summary>
        private String MIME_TYPE_REGEX_PATTERN = "^(?=[-a-z]{1,127}/[-\\.a-z0-9]{1,127}$)[a-z]+(-[a-z]+)*/[a-z0-9]+([-\\.][a-z0-9]+)*$";

        /// <summary>
        /// Extension type matching pattern.
        /// </summary>
        private String EXTENSION_REGEX_PATTERN = "^[a-zA-Z0-9]+$";

        #endregion

        #region methods

        /// <summary>
        /// Recognizes provided input.
        /// </summary>
        /// <param name="input">Input to recognize.</param>
        /// <returns>Recognized type.</returns>
        public InputType GetType(String input)
        {
            InputType inputType = InputType.Unknown;

            if (new Regex(MIME_TYPE_REGEX_PATTERN, RegexOptions.IgnoreCase).IsMatch(input))
            {
                inputType = InputType.MIMEType;
            }
            else if (new Regex(EXTENSION_REGEX_PATTERN, RegexOptions.IgnoreCase).IsMatch(input))
            {
                inputType = InputType.Extension;
            }

            return inputType;
        }

        #endregion
    }
}