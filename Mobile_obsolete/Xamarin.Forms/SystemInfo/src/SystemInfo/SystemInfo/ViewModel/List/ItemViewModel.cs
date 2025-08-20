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

using System.Windows.Input;
using SystemInfo.Utils;

namespace SystemInfo.ViewModel.List
{
    /// <summary>
    /// ViewModel class for single list's item.
    /// </summary>
    public class ItemViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Local storage of item's description.
        /// </summary>
        private string _description;

        #endregion

        #region properties

        /// <summary>
        /// Gets item's title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets item's type.
        /// </summary>
        public ListItemType ListItemType { get; }

        /// <summary>
        /// Gets or sets item's description.
        /// </summary>
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        /// <summary>
        /// Gets command executed when item is tapped.
        /// </summary>
        public ICommand OnTap { get; }

        #endregion

        #region methods

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="title">Title of the item.</param>
        /// <param name="onTap">On tap command.</param>
        /// <param name="description">Description of the item.</param>
        /// <param name="listItemType">Type of the item.</param>
        public ItemViewModel(string title, ICommand onTap, string description = "",
            ListItemType listItemType = ListItemType.Standard)
        {
            Title = title;
            Description = description;
            OnTap = onTap;
            ListItemType = listItemType;
        }

        #endregion
    }
}