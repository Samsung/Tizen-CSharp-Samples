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

using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather.Utils
{
    /// <summary>
    /// Class responsible for sending HTTP requests.
    /// </summary>
    /// <typeparam name="T">The type of expected object from API.</typeparam>
    public class Request<T> : IRequest<T>
    {
        #region fields

        /// <summary>
        /// HTTP client.
        /// </summary>
        private readonly HttpClient _httpClient;

        private bool _isFirstParameter = true;

        #endregion

        #region properties

        /// <summary>
        /// URI of the Web service.
        /// </summary>
        public string RequestUri { get; protected set; }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor that allows to set API key and address of the server.
        /// </summary>
        /// <param name="address">Server address.</param>
        public Request(string address)
        {
            _httpClient = new HttpClient();
            RequestUri = address;
        }

        /// <summary>
        /// Adds parameter to the URI.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        public void AddParameter(string name, string value)
        {
            if (_isFirstParameter)
            {
                RequestUri += $"?{name}={value}";
                _isFirstParameter = false;
            }
            else
            {
                RequestUri += $"&{name}={value}";
            }
        }

        /// <summary>
        /// Sends HTTP request using GET method.
        /// </summary>
        /// <returns>Response from service.</returns>
        public async Task<T> Get()
        {
            var response = await _httpClient.GetAsync(RequestUri);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpException(response.StatusCode);
            }

            return ReadStream(await response.Content.ReadAsStreamAsync());
        }

        /// <summary>
        /// Gets stream from HTTP client and deserializes it to the object.
        /// </summary>
        /// <param name="stream">Stream from client.</param>
        /// <returns>
        /// Returns type T object with response.
        /// If deserializing was not successful returns default value of type T.
        /// </returns>
        private static T ReadStream(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamReader))
            {
                var serializer = JsonSerializer.Create();
                return serializer.Deserialize<T>(reader);
            }
        }

        #endregion
    }
}