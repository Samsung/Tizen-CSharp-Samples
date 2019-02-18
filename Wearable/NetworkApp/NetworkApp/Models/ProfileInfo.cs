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

namespace NetworkApp.Models
{
    /// <summary>
    /// ProfileInfo class for holding Connection Profile
    /// </summary>
    public class ProfileInfo
    {
        /// <summary>
        /// Gets or sets Name of Connection Profile to be shown in View
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Type of Connection Profile
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets State of Connection Profile
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets real Name of connection Profile
        public string ProfileName { get; set; }
        /// </summary>
    }
}
