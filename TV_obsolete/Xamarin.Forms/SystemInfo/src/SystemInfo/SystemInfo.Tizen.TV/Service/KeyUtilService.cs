/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd. All rights reserved.
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

using System;
using SystemInfo.Tizen.TV.Service;
using SystemInfo.Utils;
using static Tizen.System.Information;


[assembly: Xamarin.Forms.Dependency(typeof(KeyUtilService))]

namespace SystemInfo.Tizen.TV.Service
{
    /// <summary>
    /// Provides methods that allow to obtain information about device features from its name.
    /// </summary>
    public class KeyUtilService : IKeyUtil
    {
        /// <summary>
        /// Tries to get bool value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        public bool TryGetValueToBool(string key)
        {
            TryGetValue(key, out bool value);
            return value;
        }

        /// <summary>
        /// Tries to get int value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        public int TryGetValueToInt(string key)
        {
            TryGetValue(key, out int value);
            return value;
        }

        /// <summary>
        /// Tries to get double value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        public double TryGetValueToDouble(string key)
        {
            TryGetValue(key, out double value);
            return value;
        }

        /// <summary>
        /// Tries to get string value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        public string TryGetValueToString(string key)
        {
            TryGetValue(key, out string value);
            return value;
        }

        /// <summary>
        /// Tries to get DateTime value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        public DateTime TryGetValueToDateTime(string key)
        {
            TryGetValue(key, out DateTime value);
            return value;
        }
    }
}