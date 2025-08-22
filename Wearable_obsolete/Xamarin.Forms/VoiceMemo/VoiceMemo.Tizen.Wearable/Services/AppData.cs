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

using System;
using Tizen.Applications;
using VoiceMemo.Services;
using VoiceMemo.Tizen.Wearable.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppDataService))]

namespace VoiceMemo.Tizen.Wearable.Services
{
    class AppDataService : IAppDataService
    {
        readonly object _locker;
        public AppDataService()
        {
            _locker = new object();
        }

        /// <summary>
        /// Check if the specified key exists or not
        /// </summary>
        /// <param name="key">key string</param>
        /// <returns>true if it exists</returns>
        public bool Contain(string key)
        {
            return Preference.Contains(key);
        }
        /// <summary>
        /// Get value for the specific key
        /// </summary>
        /// <param name="key">key string</param>
        /// <returns>value string</returns>
        public string GetValue(string key)
        {
            lock (_locker)
            {
                try
                {
                    return Preference.Get<string>(key);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Preference.GetValue] key : " + key + ", exception:" + ex.Message);
                    return null;
                }
            }
        }
        /// <summary>
        /// Set key-value pair
        /// </summary>
        /// <param name="key">key to save</param>
        /// <param name="value">value to save</param>
        public void SetValue(string key, string value)
        {
            lock (_locker)
            {
                try
                {
                    Preference.Set(key, value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Preference.SetValue] key : " + key + ", exception:" + ex.Message);
                }
            }
        }
    }
}
