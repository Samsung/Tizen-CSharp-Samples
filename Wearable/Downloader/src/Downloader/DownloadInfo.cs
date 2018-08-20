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

namespace Downloader
{
    /// <summary>
    /// An internal class to show download information list  
    /// </summary>
    internal class DownloadInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">field name</param>
        /// <param name="value">field value</param>
        public DownloadInfo(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// The field name of download information list
        /// </summary>
        public string Name { private set; get; }

        /// <summary>
        /// The field value of download information list
        /// </summary>
        public string Value { set; get; }
    }
}