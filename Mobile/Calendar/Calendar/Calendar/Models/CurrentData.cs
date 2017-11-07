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
    /// A class for representing the information pointing the cache data.
    /// </summary>
    public static class CacheData
    {
        /// <summary>
        /// A datetime which view representing in the Month view.
        /// </summary>
        private static DateTime currentDateTime;
        /// <summary>
        /// Gets or sets the currentDateTime.
        /// </summary>
        public static DateTime CurrentDateTime
        {
            get => currentDateTime;
            set
            {
                currentDateTime = value;
            }
        }
    }
}
