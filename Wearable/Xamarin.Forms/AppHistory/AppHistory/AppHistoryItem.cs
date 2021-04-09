// Copyright 2018 Samsung Electronics Co., Ltd
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
        /// <param name="infoType">The type of information to display when the item is clicked</param\>
        public AppHistoryItem(string title, string detail, QueryType infoType)
        {
            Title = title;
            Detail = detail;
            InfoType = infoType;
        }

        /// <summary>
        /// Gets or sets the name of application history item to present
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets detailed conditions of the application history to query
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// Gets or sets type of information to retrieve
        /// </summary>
        public QueryType InfoType { get; set; }
    }
}
