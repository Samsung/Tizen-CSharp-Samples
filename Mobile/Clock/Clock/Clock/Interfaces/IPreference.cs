/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd
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

using Clock.Data;

namespace Clock.Interfaces
{
    /// <summary>
    /// The Preference Interface to manage application preference data
    /// </summary>
    public interface IPreference
    {
        /// <summary>
        /// Saves a list of cities which an user chooses
        /// </summary>
        /// <param name="user_location">a list of cities</param>
        void Save(LocationObservableCollection user_location);
        /// <summary>
        /// Saves a location information
        /// </summary>
        /// <param name="item">a location to save</param>
        /// <param name="i">index</param>
        void SaveItem(Location item, int i);
        /// <summary>
        /// Loads a list of cities
        /// </summary>
        /// <param name="user_location">a list of cities which an user selected before</param>
        /// It's passed by reference
        void Load(ref LocationObservableCollection user_location);
        /// <summary>
        /// Loads a location at the specified index
        /// </summary>
        /// <param name="i">the index to load</param>
        /// <returns>a loaded location</returns>
        Location LoadItem(int i);

        /// <summary>
        /// Sets an integer value as the preference data
        /// </summary>
        /// <param name="key">The name of the key to save</param>
        /// <param name="value">The integer value to save for the specified key</param>
        void SetInt(string key, int value);

        /// <summary>
        /// Gets an integer value from the preference data
        /// </summary>
        /// <param name="key">The name of the key to retrieve</param>
        /// <returns>int value relevant to the given key</returns>
        int GetInt(string key);

        /// <summary>
        /// Checks whether the given key exists in the preference data
        /// </summary>
        /// <param name="key">The name of the key to check existence</param>
        /// <returns>returns true if the key exists in the preference data; otherwise, returns false.</returns>
        bool Exist(string key);
    }
}
