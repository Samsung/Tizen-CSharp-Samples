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

namespace Calendar.Models
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
        /// A string for representing the datetime of the record.
        /// </summary>
        public string DisplayTime { get; set; }
        /// <summary>
        /// A string for representing the summary of the record.
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// A string for representing the location of the record.
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// A string for representing the description of the record.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// A datetime for representing the start of the record.
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// A datetime for representing the end of the record.
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// A boolean for representing whether datetime is allday or not.
        /// </summary>
        public bool IsAllday { get; set; }
        /// <summary>
        /// A integer for representing the reminder index.
        /// </summary>
        public int Reminder { get; set; }
        /// <summary>
        /// A integer for representing the recurrence index.
        /// </summary>
        public int Recurrence { get; set; }
        /// <summary>
        /// A integer for recurrence until type.
        /// </summary>
        public int UntilType { get; set; }
        /// <summary>
        /// A integer for recurrence until count.
        /// </summary>
        public int UntilCount { get; set; }
        /// <summary>
        /// A datetime for recurrence until range.
        /// </summary>
        public DateTime UntilTime { get; set; }
        /// <summary>
        /// A integer for representing the priority index.
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// A integer for representing the sensitivity index.
        /// </summary>
        public int Sensitivity { get; set; }
        /// <summary>
        /// A integer for representing the status index.
        /// </summary>
        public int Status { get; set; }
    }
}
