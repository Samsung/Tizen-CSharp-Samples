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
        /// <summary>
        /// A string for representing the display name of the record.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// A string for representing the first name of the record.
        /// </summary>
        public string First { get; set; }
        /// <summary>
        /// A string for representing the last name of the record.
        /// </summary>
        public string Last { get; set; }
        /// <summary>
        /// A string for representing the phone number of the record.
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// A string for representing the email of the record.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// A string for representing the url of the record.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// A string for representing the company of the record.
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// A string for representing the note of the record.
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// A string for representing the event of the record.
        /// </summary>
        public int Event { get; set; }
    }
}
