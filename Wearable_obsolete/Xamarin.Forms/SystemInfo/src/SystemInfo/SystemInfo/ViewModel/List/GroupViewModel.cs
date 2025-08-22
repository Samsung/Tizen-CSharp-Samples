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
using System.Collections.ObjectModel;
using System.Linq;
using SystemInfo.Utils;

namespace SystemInfo.ViewModel.List
{
    /// <summary>
    /// ViewModel class for list of grouped items.
    /// </summary>
    public class GroupViewModel : ObservableCollection<ListItem>
    {
        #region properties

        /// <summary>
        /// Gets single group by its title.
        /// </summary>
        /// <param name="s">Title of the group.</param>
        /// <returns>If group with given title is found, function returns that group, otherwise returns null.</returns>
        public ListItem this[string s] => GetGroupByTitle(s);

        #endregion

        #region methods

        /// <summary>
        /// Gets group from its title.
        /// </summary>
        /// <param name="groupName">Group title.</param>
        /// <returns>If group with given title is found, function returns that group, otherwise returns null.</returns>
        private ListItem GetGroupByTitle(string groupName)
        {
            return this.FirstOrDefault(item => item.GroupName.Equals(groupName));
        }

        #endregion
    }
}