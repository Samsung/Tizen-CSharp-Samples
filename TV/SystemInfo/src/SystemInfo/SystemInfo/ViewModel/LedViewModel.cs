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

using SystemInfo.Model.Led;
using SystemInfo.Utils;

namespace SystemInfo.ViewModel
{
    /// <summary>
    /// ViewModel class for LED page.
    /// </summary>
    public class LedViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// Properties of device's LED.
        /// </summary>
        public static readonly string[] Properties = {"Max Brightness", "Brightness"};

        /// <summary>
        /// Local storage of device's LED properties.
        /// </summary>
        private ListItem _itemList;

        #endregion

        #region properties

        /// <summary>
        /// Gets or sets device's LED properties.
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
        public LedViewModel()
        {
            var led = new Led();

            string[] initialValues =
            {
                led.MaxBrightness.ToString(),
                led.Brightness.ToString()
            };

            ItemList = ListUtils.CreateItemsList(Properties, initialValues, ListItemType.Standard);

            led.LedChanged += OnLedChanged;
        }

        /// <summary>
        /// Updates LED's brightness.
        /// </summary>
        /// <param name="s">Object that sent event.</param>
        /// <param name="e">Event's argument.</param>
        private void OnLedChanged(object s, LedEventArgs e)
        {
            ItemList["Brightness"] = e.Brightness.ToString();
        }

        #endregion
    }
}