/*
 * Copyright 2018 Samsung Electronics Co., Ltd. All rights reserved.
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://floralicense.org/license
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using SystemInfo.Tizen.Wearable.Service;
using SystemInfo.Utils;
using Information = Tizen.System.Information;

[assembly: Xamarin.Forms.Dependency(typeof(KeyUtilService))]
namespace SystemInfo.Tizen.Wearable.Service
{
    /// <summary>
    /// Provides methods that allow to obtain information about device features from its name.
    /// </summary>
    public class KeyUtilService : IKeyUtil
    {
        #region methods

        /// <summary>
        /// Tries to get bool value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        public bool TryGetValueToBool(string key)
        {
            Information.TryGetValue(key, out bool value);
            return value;
        }

        /// <summary>
        /// Tries to get int value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        public int TryGetValueToInt(string key)
        {
            Information.TryGetValue(key, out int value);
            return value;
        }

        /// <summary>
        /// Tries to get double value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        public double TryGetValueToDouble(string key)
        {
            Information.TryGetValue(key, out double value);
            return value;
        }

        /// <summary>
        /// Tries to get string value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        public string TryGetValueToString(string key)
        {
            Information.TryGetValue(key, out string value);
            return value;
        }

        /// <summary>
        /// Tries to get DateTime value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        public DateTime TryGetValueToDateTime(string key)
        {
            Information.TryGetValue(key, out DateTime value);
            return value;
        }

        #endregion
    }
}
