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
using SystemInfo.Model.Vibrator;
using SystemInfo.Utils;

namespace SystemInfo.ViewModel
{
    /// <summary>
    /// ViewModel class for vibrator page.
    /// </summary>
    public class VibratorViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Properties of device's vibrators.
        /// </summary>
        public static readonly string[] Properties = { "Number of Vibrators" };

        /// <summary>
        /// Local storage of collection of device's vibrators properties.
        /// </summary>
        private ListItem _itemList;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets device's vibrators properties.
        /// </summary>
        public ListItem ItemList
        {
            get => _itemList;
            set => SetProperty(ref _itemList, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// Default class constructor.
        /// </summary>
        public VibratorViewModel()
        {
            var vibrator = new Vibrator();

            string[] initialValues =
            {
                vibrator.NumberOfVibrators.ToString()
            };

            ItemList = ListUtils.CreateItemsList(Properties, initialValues, ListItemType.Standard);
        }

        #endregion
    }
}