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
using SystemInfo.Utils;
using SystemInfo.ViewModel.List;

namespace SystemInfo.ViewModel
{
    /// <summary>
    /// ViewModel class for "Other" page.
    /// </summary>
    public class OtherViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// List of content of "Other" page.
        /// </summary>
        public static readonly string[] Properties = {"Capabilities", "Settings"};

        /// <summary>
        /// Local storage of "Other" page content.
        /// </summary>
        private ObservableCollection<ItemViewModel> _propertiesCollection;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets "Other" page content.
        /// </summary>
        public ObservableCollection<ItemViewModel> PropertiesCollection
        {
            get => _propertiesCollection;
            set => SetProperty(ref _propertiesCollection, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public OtherViewModel()
        {
            PropertiesCollection = ListUtils.CreateItemsListWithNavigation(Properties);
        }

        #endregion
    }
}