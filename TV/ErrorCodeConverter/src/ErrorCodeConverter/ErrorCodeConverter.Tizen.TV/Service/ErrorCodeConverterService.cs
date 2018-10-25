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
using ErrorCodeConverter.Tizen.TV.Service;
using Tizen.Internals.Errors;
using ErrorCodeConverter.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(ErrorCodeConverterService))]
namespace ErrorCodeConverter.Tizen.TV.Service
{
    /// <summary>
    /// Platform specific class for converting error codes to error messages.
    /// </summary>
    public class ErrorCodeConverterService : IErrorCodeConverterService
    {
        #region methods

        /// <summary>
        /// Gets error message for provided error code.
        /// </summary>
        /// <param name="code">Error code.</param>
        /// <returns>Error message.</returns>
        public string GetMessageFromCode(int code)
        {
            return ErrorFacts.GetErrorMessage(code);
        }

        #endregion
    }
}
