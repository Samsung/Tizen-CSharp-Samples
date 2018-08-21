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
    /// A class to show application history query result
    /// </summary>
    public class StatsInfoItem
    {
        /// <summary>
        /// Constructor of StatsInfoItem
        /// </summary>
        /// <param name="name">The name of the application</param>
        /// <param name="information">Detailed information of the query result</param>
        public StatsInfoItem(string name, string information)
        {
            Name = name;
            Information = information;
        }

        /// <summary>
        /// Gets or sets the name of the application
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets detailed information of the query result
        /// </summary>
        public string Information { get; set; }
    }
}
