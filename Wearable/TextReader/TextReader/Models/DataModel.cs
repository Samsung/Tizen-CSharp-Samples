/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using System.Linq;

namespace TextReader.Models
{
    /// <summary>
    /// Model class which provides input data (text) for the reader.
    /// </summary>
    class DataModel
    {
        #region fields

        /// <summary>
        /// Reader's input text as an array of paragraphs.
        /// </summary>
        private readonly string[] _paragraphs =
        {
            "Welcome to Tizen .NET!",

            "Tizen .NET is an exciting new way to develop applications for the Tizen operating" +
                " system, running on 50 million Samsung devices, including TVs, wearables," +
                " mobile, and many other IoT devices around the world.",

            "The existing Tizen frameworks are either C-based with no advantages of a managed" +
                " runtime, or HTML5-based with fewer features and lower performance than" +
                " the C-based solution.",

            "With Tizen .NET, you can use the C# programming language and the Common Language" +
                " Infrastructure standards and benefit from a managed runtime for faster" +
                " application development, and efficient, secure code execution."
        };

        #endregion

        #region properties

        /// <summary>
        /// Reader's input text as a list of paragraphs.
        /// </summary>
        public List<string> Paragraphs => _paragraphs.ToList();

        #endregion
    }
}
