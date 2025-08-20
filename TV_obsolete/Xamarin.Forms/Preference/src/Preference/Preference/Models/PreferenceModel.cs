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

using Preference.Interfaces;
using Xamarin.Forms;

namespace Preference.Models
{
    /// <summary>
    /// PreferenceModel class.
    /// </summary>
    class PreferenceModel
    {
        #region fields

        private readonly IPreferenceService _model = DependencyService.Get<IPreferenceService>();

        #endregion

        #region methods

        /// <summary>
        /// Returns boolean value that indicates that provided key is stored.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <returns>True if key is stored, false otherwise.</returns>
        public bool Contains(string key)
        {
            return _model.Contains(key);
        }

        /// <summary>
        /// Sets key with provided value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="value">Value.</param>
        public void Set(string key, object value)
        {
            _model.Set(key, value);
        }

        /// <summary>
        /// Gets value of stored key or null if key not exist.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <typeparam name="T">Type of stored value.</param>
        /// <returns>Value assigned to the key.</returns>
        public object Get<T>(string key)
        {
            return _model.Get<T>(key);
        }

        #endregion
    }
}
