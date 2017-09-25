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
 * @brief       This file contains interface for connecting ViewModel with Model
 */

namespace SecureRepository
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for platform dependent MainModel.
    /// </summary>
    public interface IMainInterface
    {
        /// <summary>
        /// Gets Decrypted text.
        /// </summary>
        string DecryptedText
        {
            get;
        }

        /// <summary>
        /// Gets Encrypted text.
        /// </summary>
        string EncryptedText
        {
            get;
        }

        /// <summary>
        /// Gets Item List.
        /// </summary>
        List<ListViewGroup> ItemList
        {
            get;
        }

        /// <summary>
        /// Adds Items to Managers.
        /// </summary>
        void Add();

        /// <summary>
        /// Encrypts then Decrypts data.
        /// </summary>
        void EncryptDecrypt();

        /// <summary>
        /// Gets Selected Item Prefix.
        /// </summary>
        /// <param name="selectedItem">Currently selected item in UI.</param>
        /// <returns>Selected Item prefix.</returns>
        string GetSelectedItemPrefix(Item selectedItem);

        /// <summary>
        /// Gets Selected Item type.
        /// </summary>
        /// <param name="selectedItem">Currently selected item in UI.</param>
        /// <returns>Selected Item type.</returns>
        string GetSelectedItemType(Item selectedItem);

        /// <summary>
        /// Removes all Items from Managers.
        /// </summary>
        void Remove();
    }
}
