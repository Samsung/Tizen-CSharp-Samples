/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd. All rights reserved.
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

namespace Preference.Interfaces
{
    /// <summary>
    /// Application service interface.
    /// </summary>
    public interface IPreferenceService
    {
        #region methods

        /// <summary>
        /// Checks if provided key is stored.
        /// </summary>
        /// <param name="key">Key name to check.</param>
        /// <returns>True if key exists. False otherwise.</returns>
        bool Contains(string key);

        /// <summary>
        /// Stores key with value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="value">Value.</param>
        void Set(string key, object value);

        /// <summary>
        /// Gets value of provided key.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="key">Key name.</param>
        /// <returns>Stored value.</returns>
        object Get<T>(string key);

        #endregion
    }
}
