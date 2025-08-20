/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd
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
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Contacts.Models
{
    /// <summary>
    /// Use DependencyService to get the IContactsAPIs implementation of each native platform
    /// Provide record contents information for the ViewModel.
    /// </summary>
    /// <remarks>
    /// Please refer to Xamarin Dependency Service
    /// https://developer.xamarin.com/guides/xamarin-forms/dependency-service/introduction/
    /// </remarks>
    public sealed class RecordItemProvider
    {
        /// <summary>
        /// Instance for lazy initialization of RecordItemProvider.
        /// </summary>
        private static readonly Lazy<RecordItemProvider> lazy = new Lazy<RecordItemProvider>(() => new RecordItemProvider());

        /// <summary>
        /// A Record Item Provider class instance which provides Record Items.
        /// When it is called for the first time, RecordItemProvider will be created.
        /// </summary>
        public static RecordItemProvider Instance { get => lazy.Value; }

        /// <summary>
        /// Instance of IContactsAPIs for get the implementation of each platform.
        /// </summary>
        private static IContactsAPIs contactsAPIs;

        /// <summary>
        /// A List of the Record item.
        /// </summary>
        private List<RecordItem> itemList;
        /// <summary>
        /// Gets record item list.
        /// </summary>
        public List<RecordItem> ItemList { get => itemList; }

        /// <summary>
        /// Inserts item.
        /// </summary>
        public int Insert(RecordItem item)
        {
            return contactsAPIs.Insert(item);
        }

        /// <summary>
        /// Updates item.
        /// </summary>
        public void Update(RecordItem item)
        {
            contactsAPIs.Update(item);
        }

        /// <summary>
        /// Delete item.
        /// </summary>
        public void Delete(RecordItem item)
        {
            contactsAPIs.Delete(item);
        }

        /// <summary>
        /// Gets all records from database.
        /// </summary>
        public void GetAll()
        {
            itemList = contactsAPIs.GetAll();
        }

        /// <summary>
        /// RecordItemProvider Constructor.
        /// A Constructor which will initialize the RecordItemProvider instance.
        /// </summary>
        public RecordItemProvider()
        {
            contactsAPIs = DependencyService.Get<IContactsAPIs>();
        }
    }
}
