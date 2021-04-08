/*
 * Copyright 2019 Samsung Electronics Co., Ltd. All rights reserved.
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

using System.Collections.Generic;
using System.Linq;

namespace TextReader.Models
{
    /// <summary>
    /// Model class which provides input data (text) for the reader.
    /// </summary>
    public class DataModel
    {
        #region fields

        /// <summary>
        /// Reader's input text as an array of paragraphs.
        /// </summary>
        private readonly string[] _paragraphs =
        {
            "Tizen is an open and flexible operating system built from the ground up",
            "to address the needs of all stakeholders of the mobile and connected device ecosystem,",
            "including device manufacturers, mobile operators, " +
                "app developers and independent software vendors (ISVs).",
            "With Tizen, a device manufacturer can begin with one of these profiles " +
                "and modify it to serve their own needs,",
            "or use the Tizen Common base to develop a new profile to meet the memory,",
            "processing and power requirements of any device and quickly bring it to market."
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
