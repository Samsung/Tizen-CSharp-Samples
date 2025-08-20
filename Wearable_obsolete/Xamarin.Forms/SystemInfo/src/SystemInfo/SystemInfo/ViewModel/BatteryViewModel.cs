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
using SystemInfo.Model.Battery;
using SystemInfo.Utils;

namespace SystemInfo.ViewModel
{
    /// <summary>
    /// ViewModel class for battery page.
    /// </summary>
    public class BatteryViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Properties of device's battery.
        /// </summary>
        public static readonly string[] Properties = { "Percent", "Is Charging", "State" };

        /// <summary>
        /// Data source.
        /// </summary>
        private readonly Battery _battery;

        /// <summary>
        /// Local storage of collection of battery's properties.
        /// </summary>
        private ListItem _itemList;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets collection of battery's properties.
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
        public BatteryViewModel()
        {
            _battery = new Battery();
            ItemList = CreateItems();

            _battery.BatteryChanged += (s, e) =>
            {
                ItemList["Percent"] = e.BatteryLevel.ToString();
                ItemList["Is Charging"] = e.IsCharging.ToString();
                ItemList["State"] = e.BatteryLevelStatus.ToString();
            };
        }

        /// <summary>
        /// Creates item list with battery's properties.
        /// </summary>
        /// <returns>List of list's items.</returns>
        private ListItem CreateItems()
        {
            string[] initialValues =
            {
                _battery.Level.ToString(), _battery.IsCharging.ToString(), _battery.BatteryLevelStatus.ToString()
            };

            ListItemType[] itemTypes = { ListItemType.WithProgress, ListItemType.Standard, ListItemType.Standard };

            return ListUtils.CreateItemsList(Properties, initialValues, itemTypes);
        }

        #endregion
    }
}