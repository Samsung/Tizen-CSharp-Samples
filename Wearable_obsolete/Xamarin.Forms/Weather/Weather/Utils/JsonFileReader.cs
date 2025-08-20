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
using System.Reflection;
using Newtonsoft.Json;

namespace Weather.Utils
{
    /// <summary>
    /// Class that reads JSON file and converts it to given type.
    /// </summary>
    /// <typeparam name="T">JSON object type.</typeparam>
    public class JsonFileReader<T>
    {
        #region fields

        /// <summary>
        /// Namespace of the file.
        /// </summary>
        private readonly string _fileNameSpace;

        /// <summary>
        /// File name.
        /// </summary>
        private readonly string _fileName;

        #endregion

        #region properties

        /// <summary>
        /// File content in T format.
        /// </summary>
        /// <remarks>Before calling Read methods, it is always null.</remarks>
        public T Result { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor, which allows to specify file location and name.
        /// </summary>
        /// <param name="fileNameSpace">Namespace of the file.</param>
        /// <param name="fileName">Name of the file.</param>
        public JsonFileReader(string fileNameSpace, string fileName)
        {
            _fileNameSpace = fileNameSpace;
            _fileName = fileName;
        }

        /// <summary>
        /// Reads the file.
        /// </summary>
        public virtual void Read()
        {
            var assembly = typeof(JsonFileReader<T>).GetTypeInfo().Assembly;

            using (var stream = assembly.GetManifestResourceStream(_fileNameSpace + _fileName))
            using (var streamReader = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamReader))
            {
                var serializer = JsonSerializer.Create();
                Result = serializer.Deserialize<T>(reader);
            }
        }

        #endregion
    }
}