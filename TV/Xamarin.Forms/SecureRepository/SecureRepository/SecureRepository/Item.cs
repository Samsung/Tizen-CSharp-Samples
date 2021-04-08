/*
 *  Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
 *
 *  Contact: Ernest Borowski <e.borowski@partner.samsung.com>
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License
 *
 *
 * @file        Certificates.cs
 * @author      Ernest Borowski (e.borowski@partner.samsung.com)
 * @version     1.0
 * @brief       This file contains class that holds item alias and it`s type (UI)
 */

namespace SecureRepository
{
    /// <summary>
    /// Item stores item alias and it`s type (UI).
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        /// <param name="alias">The name of an item.</param>
        /// <param name="type">The type of an item.</param>
        public Item(string alias, AliasType type)
        {
            this.Alias = alias;
            this.Type = type;
        }

        /// <summary>
        /// Gets item alias.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// Gets item type.
        /// </summary>
        public AliasType Type { get; }
    }
}
