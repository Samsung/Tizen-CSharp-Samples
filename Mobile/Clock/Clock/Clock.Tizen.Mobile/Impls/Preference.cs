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
using Clock.Interfaces;
using Clock.Tizen.Mobile.Impls;
using Native = Tizen.Applications;
using System;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(Preference))]

namespace Clock.Tizen.Mobile.Impls
{
    /// <summary>
    /// The enumeration of item key to get/set in preference
    /// </summary>
    enum ItemKeyType
    {
        /// <summary>
        /// Identifier for city of location
        /// </summary>
        CITY = 1,
        /// <summary>
        /// Identifier for country of location
        /// </summary>
        COUNTRY,
        /// <summary>
        /// Identifier for offset of location
        /// </summary>
        OFFSET,
        /// <summary>
        /// Identifier for timezone path of location
        /// </summary>
        TIMEZONE_PATH
    }

    /// <summary>
    /// The Preference class to manage application preference data
    /// </summary>
    class Preference : IPreference
    {
        /// <summary>
        /// The Preference constructor
        /// </summary>
        public Preference()
        {
        }

        /// <summary>
        /// Saves a list of cities which an user chooses
        /// </summary>
        /// <param name="user_location">a list of cities</param>
        public void Save(LocationObservableCollection user_location)
        {
            if (Native.Preference.Contains("DOTNET_WORLDCLOCK_LIST_SIZE") || Native.Preference.Contains("DOTNET_WORLDCLOCK_MAP_CURRENT_TIMEZONE"))
            {
                Native.Preference.RemoveAll();
            }

            if (user_location.Count != 0)
            {
                Native.Preference.Set("DOTNET_WORLDCLOCK_LIST_SIZE", user_location.Count);
                int no = 0;
                foreach (Location l in user_location)
                {
                    SaveItem(l, no++);
                }

                user_location.Clear();
            }
        }

        /// <summary>
        /// Loads a list of cities
        /// </summary>
        /// <param name="user_location">a list of cities which an user selected before</param>
        /// It's passed by reference
        public void Load(ref LocationObservableCollection user_location)
        {
            int listSize;
            bool existing = Native.Preference.Contains("DOTNET_WORLDCLOCK_LIST_SIZE");
            if (!existing)
            {
                return;
            }

            listSize = Native.Preference.Get<int>("DOTNET_WORLDCLOCK_LIST_SIZE");
            if (user_location.Count != 0)
            {
                return;
            }

            for (int i = 0; i < listSize; i++)
            {
                Location l = LoadItem(i);
                user_location.Add(l);
            }
        }

        /// <summary>
        /// Loads a location at the specified index
        /// </summary>
        /// <param name="i">the index to load</param>
        /// <returns>a loaded location</returns>
        public Location LoadItem(int i)
        {
            string city = Native.Preference.Get<string>(GetItemKey(ItemKeyType.CITY, i));
            string country = Native.Preference.Get<string>(GetItemKey(ItemKeyType.COUNTRY, i));
            int offset = Native.Preference.Get<int>(GetItemKey(ItemKeyType.OFFSET, i));
            Location l = new Location(city, country, offset, 0, 0);
            return l;
        }

        private string GetItemKey(ItemKeyType type, int no)
        {
            string s = "C#_ITEM_" + no.ToString() + "_";
            switch (type)
            {
                case ItemKeyType.CITY:
                    s += "CITY";
                    break;
                case ItemKeyType.COUNTRY:
                    s += "COUNTRY";
                    break;
                case ItemKeyType.OFFSET:
                    s += "OFFSET";
                    break;
                case ItemKeyType.TIMEZONE_PATH:
                    s += "TIMEZONE_PATH";
                    break;
                default:
                    return null;
            }

            return s;
        }

        /// <summary>
        /// Saves a location information
        /// </summary>
        /// <param name="item">a location to save</param>
        /// <param name="i">index</param>
        public void SaveItem(Location item, int i)
        {
            Native.Preference.Set(GetItemKey(ItemKeyType.CITY, i), item.Name);
            Native.Preference.Set(GetItemKey(ItemKeyType.COUNTRY, i), item.Country);
            Native.Preference.Set(GetItemKey(ItemKeyType.OFFSET, i), item.GmtOffset);
        }

        /// <summary>
        /// Sets an integer value as the preference data
        /// </summary>
        /// <param name="key">The name of the key to save</param>
        /// <param name="value">The integer value to save for the specified key</param>
        public void SetInt(string key, int value)
        {
            Native.Preference.Set(key, value);
        }

        /// <summary>
        /// Gets an integer value from the preference data
        /// </summary>
        /// <param name="key">The name of the key to retrieve</param>
        /// <returns>int value relevant to the given key</returns>
        public int GetInt(string key)
        {
            int val = -1;
            try
            {
                val = Native.Preference.Get<int>(key);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[Preference.GetInt] exception:" + ex.Message);
            }

            return val;
        }

        /// <summary>
        /// Checks whether the given key exists in the preference data
        /// </summary>
        /// <param name="key">The name of the key to check existence</param>
        /// <returns>returns true if the key exists in the preference data; otherwise, returns false.</returns>
        public bool Exist(string key)
        {
            return Native.Preference.Contains(key);
        }
    }
}
