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

namespace SystemInfo.Utils
{
    /// <summary>
    /// Interface that contains all necessary methods to get information about device features from its name.
    /// </summary>
    public interface IKeyUtil
    {
        /// <summary>
        /// Tries to get bool value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        bool TryGetValueToBool(string key);

        /// <summary>
        /// Tries to get int value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        int TryGetValueToInt(string key);

        /// <summary>
        /// Tries to get double value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        double TryGetValueToDouble(string key);

        /// <summary>
        /// Tries to get string value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        string TryGetValueToString(string key);

        /// <summary>
        /// Tries to get DateTime value of the feature.
        /// </summary>
        /// <param name="key">Name of the feature.</param>
        /// <returns>The value of given feature.</returns>
        DateTime TryGetValueToDateTime(string key);
    }
}