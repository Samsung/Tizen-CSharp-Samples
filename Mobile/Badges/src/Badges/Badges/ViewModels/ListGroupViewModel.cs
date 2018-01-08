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
using System.Collections.ObjectModel;

namespace Badges.ViewModels
{
    /// <summary>
    /// Class for grouped list model.
    /// </summary>
    internal class ListGroupViewModel : ObservableCollection<AppListItemViewModel>
    {
        /// <summary>
        /// Name of the group.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Short name of the group used for indexing.
        /// </summary>
        public String ShortName { get; set; }
    }
}