/*
 * Copyright (c) 2021 Samsung Electronics Co., Ltd. All rights reserved.
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

using System;
using System.Globalization;

namespace Alarms.Models
{
    /// <summary>
    /// Flags for weekdays repetition flags.
    /// </summary>
    public class DaysOfWeekFlags
    {
        #region properties

        /// <summary>
        /// Monday select flag property.
        /// </summary>
        public bool Monday { get; set; }

        /// <summary>
        /// Tuesday select flag property.
        /// </summary>
        public bool Tuesday { get; set; }

        /// <summary>
        /// Wednesday select flag property.
        /// </summary>
        public bool Wednesday { get; set; }

        /// <summary>
        /// Thursday select flag property.
        /// </summary>
        public bool Thursday { get; set; }

        /// <summary>
        /// Friday select flag property.
        /// </summary>
        public bool Friday { get; set; }

        /// <summary>
        /// Saturday select flag property.
        /// </summary>
        public bool Saturday { get; set; }

        /// <summary>
        /// Sunday select flag property.
        /// </summary>
        public bool Sunday { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Generates string with list of selected days of week.
        /// </summary>
        /// <returns>List of selected days of week.</returns>
        public override string ToString()
        {
            var ci = DateTimeFormatInfo.CurrentInfo;
            string ret = Monday ? ci.GetDayName(DayOfWeek.Monday) : null;
            ret = Tuesday ? (ret == null ? "" : ret + ", ") + ci.GetDayName(DayOfWeek.Tuesday) : ret;
            ret = Wednesday ? (ret == null ? "" : ret + ", ") + ci.GetDayName(DayOfWeek.Wednesday) : ret;
            ret = Thursday ? (ret == null ? "" : ret + ", ") + ci.GetDayName(DayOfWeek.Thursday) : ret;
            ret = Friday ? (ret == null ? "" : ret + ", ") + ci.GetDayName(DayOfWeek.Friday) : ret;
            ret = Saturday ? (ret == null ? "" : ret + ", ") + ci.GetDayName(DayOfWeek.Saturday) : ret;
            ret = Sunday ? (ret == null ? "" : ret + ", ") + ci.GetDayName(DayOfWeek.Sunday) : ret;
            return ret ?? "";
        }

        /// <summary>
        /// Checks if any of day flags is set.
        /// </summary>
        /// <returns>True if any of day flags is set, false otherwise.</returns>
        public bool IsAny()
        {
            return Monday || Tuesday || Wednesday || Thursday || Friday || Saturday || Sunday;
        }

        #endregion
    }
}