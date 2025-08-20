//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System.Collections.Generic;

namespace SpeechToText.Views
{
    /// <summary>
    /// The class representing a group of settings items (with title).
    /// </summary>
    class SettingsItemsGroup : List<SettingsItem>
    {
        #region properties

        /// <summary>
        /// Group title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// List of group items.
        /// </summary>
        public List<SettingsItem> GroupItems => this;

        #endregion

        #region methods

        /// <summary>
        /// The group constructor.
        /// </summary>
        /// <param name="items">Items array</param>
        public SettingsItemsGroup(SettingsItem[] items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        #endregion
    }
}
