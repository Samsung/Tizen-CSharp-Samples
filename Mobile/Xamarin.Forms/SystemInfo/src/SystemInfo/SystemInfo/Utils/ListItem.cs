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

using System.Collections.ObjectModel;
using System.Linq;
using SystemInfo.ViewModel.List;

namespace SystemInfo.Utils
{
    /// <summary>
    /// Observable collection of items. It allows to build groups in ListView.
    /// </summary>
    public class ListItem : ObservableCollection<ItemViewModel>
    {
        #region properties

        /// <summary>
        /// Gets or sets group name.
        /// </summary>
        public string GroupName { get; set; } = "";

        /// <summary>
        /// Gets or sets single item's description by its title.
        /// </summary>
        /// <param name="s">Title of the item.</param>
        /// <returns>Item's description.</returns>
        public string this[string s]
        {
            get => GetItemByTitle(s).Description;
            set => GetItemByTitle(s).Description = value;
        }

        #endregion

        #region methods

        /// <summary>
        /// Search for specific item by its title.
        /// </summary>
        /// <param name="title">Item's title.</param>
        /// <returns>If item with given title is found, function returns that item, otherwise returns null.</returns>
        private ItemViewModel GetItemByTitle(string title)
        {
            return this.FirstOrDefault(item => item.Title.Equals(title));
        }

        #endregion
    }
}