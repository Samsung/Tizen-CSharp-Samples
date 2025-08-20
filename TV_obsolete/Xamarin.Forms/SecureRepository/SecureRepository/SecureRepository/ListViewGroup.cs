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
 * @brief       This file contains class used in ListView grupping (UI)
 */

namespace SecureRepository
{
    using System.Collections.Generic;

    /// <summary>
    /// Enumeration for item type.
    /// </summary>
    public enum AliasType
    {
        /// <summary>
        /// Represents a data.
        /// </summary>
        Data,

        /// <summary>
        /// Represents a Key.
        /// </summary>
        Key,

        /// <summary>
        /// Represents a Certificate.
        /// </summary>
        Certificate
    }

    /// <summary>
    /// class responsible for holding item list (UI).
    /// </summary>
    public class ListViewGroup : List<Item>
    {
        /// <summary>
        /// Initializes a new instance of the ListViewGroup class.
        /// </summary>
        /// <param name="aliasType">item type.</param>
        public ListViewGroup(AliasType aliasType)
        {
            switch (aliasType)
            {
                case AliasType.Certificate:
                    this.Title = "Certificate";
                    this.ShortName = "C";
                    break;
                case AliasType.Key:
                    this.Title = "Key";
                    this.ShortName = "K";
                    break;
                case AliasType.Data:
                    this.Title = "Data";
                    this.ShortName = "D";
                    break;
            }
        }

        /// <summary>
        /// Gets first letter of item type.
        /// </summary>
        public string ShortName { get; }

        /// <summary>
        /// Gets item alias.
        /// </summary>
        public string Title { get; }
    }
}
