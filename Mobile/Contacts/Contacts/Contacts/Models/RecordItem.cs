/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

namespace Contacts.Models
{
    /// <summary>
    /// A class for representing the information related to the stored record.
    /// </summary>
    public class RecordItem
    {
        /// <summary>
        /// A integer for representing the record index.
        /// </summary>
        public int Index { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Company { get; set; }
        public string Note { get; set; }
        public string DisplayName { get; set; }
        public int Event { get; set; }
    }
}
