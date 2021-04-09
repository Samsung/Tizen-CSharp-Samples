//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System.Net;
using System.Net.Http;

namespace Weather.Utils
{
    /// <summary>
    /// Exception that is thrown on HTTP errors.
    /// </summary>
    public class HttpException : HttpRequestException
    {
        #region properties

        /// <summary>
        /// HTTP status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        #endregion

        #region methods

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="code">HTTP status code of error.</param>
        public HttpException(HttpStatusCode code)
        {
            StatusCode = code;
        }

        #endregion
    }
}