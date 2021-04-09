/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
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

namespace VoiceMemo.Services
{
    // Interface to use DependencyService
    // Platform-specific functionality : to get/restore app data
    public interface IAppDataService
    {
        /// <summary>
        /// Get value for the specific key
        /// </summary>
        /// <param name="key">key string</param>
        /// <returns>value string</returns>
        string GetValue(string key);
        /// <summary>
        /// Set key-value pair
        /// </summary>
        /// <param name="key">key to save</param>
        /// <param name="value">value to save</param>
        void SetValue(string key, string value);
        /// <summary>
        /// Check if the specified key exists or not
        /// </summary>
        /// <param name="key">key string</param>
        /// <returns>true if it exists</returns>
        bool Contain(string key);
    }
}
