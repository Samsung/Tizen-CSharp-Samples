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

using System.Threading.Tasks;

namespace Weather.Utils
{
    /// <summary>
    /// Provides functionality to get response from Web API.
    /// </summary>
    /// <typeparam name="T">The type of expected object from API.</typeparam>
    public interface IRequest<T>
    {
        #region properties

        /// <summary>
        /// URI of the Web service.
        /// </summary>
        string RequestUri { get; }

        #endregion

        #region methods

        /// <summary>
        /// Adds parameter to the URI.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        void AddParameter(string name, string value);

        /// <summary>
        /// Sends HTTP request using GET method.
        /// </summary>
        /// <returns>Response from service.</returns>
        Task<T> Get();

        #endregion
    }
}