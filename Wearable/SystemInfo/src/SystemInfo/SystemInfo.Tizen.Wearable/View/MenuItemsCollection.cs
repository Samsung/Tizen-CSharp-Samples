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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SystemInfo.ViewModel.List;

namespace SystemInfo.Tizen.Wearable.View
{
    /// <summary>
    /// Labeled view model observable collection.
    /// </summary>
    public class MenuItemsCollection : ObservableCollection<ItemViewModel>
    {
        #region properties

        /// <summary>
        /// Name for the menu items collection.
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="items">Items collection.</param>
        public MenuItemsCollection(IEnumerable<ItemViewModel> items) : base(items)
        {
        }

        #endregion
    }
}
