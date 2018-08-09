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

using TizenPreference = Tizen.Applications.Preference;

namespace Preference.Models
{
    /// <summary>
    /// PreferenceModel class.
    /// </summary>
    class PreferenceModel
    {

        #region methods

        /// <summary>
        /// Checks if provided key is stored.
        /// </summary>
        /// <param name="key">Key name to check.</param>
        /// <returns>Returns true if key is stored. False otherwise.</returns>
        public bool Contains(string key)
        {
            return TizenPreference.Contains(key);
        }

        /// <summary>
        /// Store key with provided value.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <param name="value">Value to assign to provided key.</param>
        public void Set(string key, object value)
        {
            if (Contains(key))
            {
                TizenPreference.Remove(key);
            }

            TizenPreference.Set(key, value);
        }

        /// <summary>
        /// Returns value assigned to the key.
        /// </summary>
        /// <param name="key">Key name.</param>
        /// <returns>Returns value assigned to requested key. If key doesn't exist and requested type is string, empty string is returned.
        /// If key doesn't exist and requested type is not a string, default value for requested type is returned</returns>
        public object Get<T>(string key)
        {
            object value = default(T);

            if (Contains(key))
            {
                value = TizenPreference.Get<T>(key);
            }

            if (typeof(T) == typeof(string) && value == null)
            {
                value = string.Empty;
            }

            return value;
        }

        #endregion
    }
}
