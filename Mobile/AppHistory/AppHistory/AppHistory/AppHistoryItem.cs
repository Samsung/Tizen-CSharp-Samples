/*
* Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

namespace AppHistory
{
    /// <summary>
    /// An internal class to show application history item list
    /// </summary>
    internal class AppHistoryItem
    {
        /// <summary>
        /// Constructor of AppHistoryItem
        /// </summary>
        /// <param name="title">The name of application history item</param>
        /// <param name="detail">Detailed conditions of the application history to query</param>
        public AppHistoryItem(string title, string detail)
        {
            Title = title;
            Detail = detail;
        }

        /// <summary>
        /// The name of application history item to present
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Detailed conditions of the application history to query
        /// </summary>
        public string Detail { get; set; }
    }
}
