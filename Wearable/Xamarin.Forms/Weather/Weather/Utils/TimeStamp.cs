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

using System;

namespace Weather.Utils
{
    /// <summary>
    /// Class that is responsible for converting timestamp.
    /// </summary>
    public static class TimeStamp
    {
        #region methods

        /// <summary>
        /// Converts timestamp to "DateTime" object.
        /// </summary>
        /// <param name="utcTimeStamp">UTC timestamp.</param>
        /// <returns>"DateTime" object.</returns>
        public static DateTime Convert(ulong utcTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(utcTimeStamp);
        }

        /// <summary>
        /// Converts timestamp to "DateTime" object.
        /// </summary>
        /// <param name="utcTimeStamp">UTC timestamp.</param>
        /// <returns>"DateTime" object.</returns>
        public static DateTime Convert(long utcTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(utcTimeStamp);
        }

        #endregion
    }
}